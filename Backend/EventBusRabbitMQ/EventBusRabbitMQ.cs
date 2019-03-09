using System;
using System.Text;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;

using Faction.Common.Backend.EventBus;
using Faction.Common.Backend.EventBus.Abstractions;

namespace Faction.Common.Backend.EventBus.RabbitMQ
{
  public class EventBusRabbitMQ : IEventBus, IDisposable
  {
    public string EXCHANGE_NAME;
    public string _queueName;

    private readonly IServiceProvider _services;
    private readonly IRabbitMQPersistentConnection _persistentConnection;
    private readonly IEventBusSubscriptionsManager _subsManager;
    private readonly ILogger<EventBusRabbitMQ> _logger;

    private IBasicProperties _properties;
    private BlockingCollection<string> _responseQueue;


    private IModel _consumerChannel;

    IBasicProperties IEventBus.Properties {
      get {
        return _properties;
      }
      set {
        _properties = value;
      }
    }

    BlockingCollection<string> IEventBus.ResponseQueue {
      get {
        return _responseQueue;
      }
      set {
        _responseQueue = value;
      }
    }

    public EventBusRabbitMQ(string exchangeName,
                            string queueName,
                            IRabbitMQPersistentConnection persistentConnection,
                            IEventBusSubscriptionsManager subsManager,
                            IServiceProvider services,
                            ILogger<EventBusRabbitMQ> logger)
    {
      EXCHANGE_NAME = exchangeName;
      _queueName = queueName;
      _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
      _subsManager = subsManager ?? new InMemoryEventBusSubscriptionsManager();
      _consumerChannel = CreateConsumerChannel();
      _services = services;
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _subsManager.OnEventRemoved += SubsManager_OnEventRemoved;
      _responseQueue = new BlockingCollection<string>();
    }

    private void SubsManager_OnEventRemoved(object sender, string eventName)
    {
      if (!_persistentConnection.IsConnected)
      {
        _persistentConnection.TryConnect();
      }

      using (var channel = _persistentConnection.CreateModel())
      {
        channel.QueueUnbind(queue: _queueName,
            exchange: EXCHANGE_NAME,
            routingKey: eventName);

        if (_subsManager.IsEmpty)
        {
          _queueName = string.Empty;
          _consumerChannel.Close();
        }
      }
    }

    public void Publish<T>(T TMessage, string replyTo, string correlationId, bool replyExpected)
    {
      _logger.LogInformation($"Publish called.");
      if (!_persistentConnection.IsConnected)
      {
        _logger.LogInformation($"Reconnecting to Rabbit.");
        _persistentConnection.TryConnect();
      }

      using (var channel = _persistentConnection.CreateModel())
      {
        var eventName = TMessage.GetType()
            .Name;

        channel.ExchangeDeclare(exchange: EXCHANGE_NAME,
                            durable: true,
                            type: "topic");

        var settings = new JsonSerializerSettings
        {
          NullValueHandling = NullValueHandling.Ignore,
          MissingMemberHandling = MissingMemberHandling.Ignore
        };

        var message = JsonConvert.SerializeObject(TMessage, settings);
        _logger.LogInformation($"Sending message: {message}");

        var body = Encoding.UTF8.GetBytes(message);

        var properties = channel.CreateBasicProperties();
        properties.DeliveryMode = 2; // persistent
        properties.Type = eventName;

        string exchange = EXCHANGE_NAME;
        string routingKey = eventName;

        if (!String.IsNullOrEmpty(replyTo))
        {
          exchange = "";
          routingKey = replyTo;
          properties.CorrelationId = correlationId;
        }

        if (replyExpected)
        {
          properties.ReplyTo = _properties.ReplyTo;
          properties.CorrelationId = _properties.CorrelationId;
        }

        _logger.LogInformation($"Sending message to Exchange: {EXCHANGE_NAME} with RoutingKey: {routingKey}");

        channel.BasicPublish(exchange: exchange,
                                routingKey: routingKey,
                                mandatory: true,
                                basicProperties: properties,
                                body: body);
      }
    }

    public void Unsubscribe<T, TH>()
        where TH : IEventHandler<T>
    {
      _subsManager.RemoveSubscription<T, TH>();
    }

    public void Dispose()
    {
      if (_consumerChannel != null)
      {
        _consumerChannel.Dispose();
      }

      _subsManager.Clear();
    }
    public void Subscribe<T, TH>()
        where TH : IEventHandler<T>
    {
      var eventName = _subsManager.GetEventKey<T>();

      var containsKey = _subsManager.HasSubscriptionsForEvent(eventName);
      if (!containsKey)
      {
        if (!_persistentConnection.IsConnected)
        {
          _persistentConnection.TryConnect();
        }

        using (var channel = _persistentConnection.CreateModel())
        {
          var queueName = channel.QueueDeclare(queue: _queueName,
                                                  durable: true,
                                                  exclusive: false,
                                                  autoDelete: false,
                                                  arguments: null
                                              ).QueueName; // Shortcut to create a "dynamic" queue for this Consumer
          channel.QueueBind(queue: _queueName,
                              exchange: EXCHANGE_NAME,
                              routingKey: eventName);
        }
      }

      _subsManager.AddSubscription<T, TH>();
    }

    public void Initialize(){
      // Setup Reply queue
      var correlationId = Guid.NewGuid().ToString();
      _properties = _persistentConnection.CreateModel().CreateBasicProperties();
      _properties.CorrelationId = correlationId;

      var channel = _persistentConnection.CreateModel();
      _properties.ReplyTo = channel.QueueDeclare(
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                        ).QueueName; // Shortcut to create a "dynamic" queue for this Consumer
      
      _logger.LogInformation($"Setting up reply queue: {_properties.ReplyTo}");
      var consumer = new EventingBasicConsumer(channel);
      consumer.Received += async (model, ea) =>
      {
        // Catch anything thats a reply to for us, else process it like bulk
        if (ea.BasicProperties.CorrelationId == _properties.CorrelationId)
        {
          _responseQueue.Add(Encoding.UTF8.GetString(ea.Body));
        }
      };
      channel.BasicConsume(queue: _properties.ReplyTo,
                              autoAck: false,
                              consumer: consumer);

      channel.CallbackException += (sender, ea) =>
      {
        _consumerChannel.Dispose();
        _consumerChannel = CreateConsumerChannel();
      };
    }

    private IModel CreateConsumerChannel()
    {
      if (!_persistentConnection.IsConnected)
      {
        _persistentConnection.TryConnect();
      }

      var channel = _persistentConnection.CreateModel();

      channel.ExchangeDeclare(exchange: EXCHANGE_NAME,
                              durable: true,
                              type: "topic");

      var queueName = channel.QueueDeclare(queue: _queueName,
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null).QueueName;

      var consumer = new EventingBasicConsumer(channel);
      consumer.Received += async (model, ea) =>
      {
        // Catch anything thats a reply to for us, else process it like bulk
          if (_properties != null && ea.BasicProperties.CorrelationId == _properties.CorrelationId)
          {
              _responseQueue.Add(Encoding.UTF8.GetString(ea.Body));
          }
          else 
          {
              var eventTypeName = ea.RoutingKey;
              _logger.LogInformation($"received {eventTypeName} event");
              
              var message = Encoding.UTF8.GetString(ea.Body);
              _logger.LogInformation($"Message properties: {ea.BasicProperties.ToString()}");
              
              // BUG: We are dropping (auto Acking) messages that are already queued by the time this service starts
              // messages sent AFTER this service is started are handled fine
              await ProcessEvent(eventTypeName, message, ea.BasicProperties.ReplyTo, ea.BasicProperties.CorrelationId);
              
              // TODO: implement a check to validate the ProcessEvent/Handle was succesful or not befor Ack
              channel.BasicAck(ea.DeliveryTag, false);
              _logger.LogInformation($"ack {eventTypeName} event with deliverytag {ea.DeliveryTag.ToString()}");
          }
        //}
        // catch (Exception e)
        // {
        //   _logger.LogError($"Failed to process RabbitMQ message. Error: {e.Message}");
        // }
      };

      channel.BasicConsume(queue: queueName,
                              autoAck: false,
                              consumer: consumer);

      channel.CallbackException += (sender, ea) =>
      {
        _consumerChannel.Dispose();
        _consumerChannel = CreateConsumerChannel();
      };

      return channel;
    }
    private async Task ProcessEvent(string eventName, string message, string replyTo, string correlationId)
    {
      if (_subsManager.HasSubscriptionsForEvent(eventName))
      {
        // TODO: Figure out what creating a new Scope here actually effects, removing this scope statement
        // results in handler being null as if there are not Event Handlers registered in the DI Container
        using (var scope = _services.CreateScope())
        {
          var subscriptions = _subsManager.GetHandlersForEvent(eventName);
          var settings = new JsonSerializerSettings
          {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
          //PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };
          foreach (var subscription in subscriptions)
          {
            var eventType = _subsManager.GetEventTypeByName(eventName);
            var integrationEvent = JsonConvert.DeserializeObject(message, eventType, settings);
            var handler = _services.GetService(subscription.HandlerType);
            var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
            var method = concreteType.GetMethod("Handle");
            await (Task)method.Invoke(handler, new object[] { integrationEvent, replyTo, correlationId });
          }
        }
      }
    }
  }
}