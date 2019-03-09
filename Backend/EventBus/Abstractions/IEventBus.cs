using System;
using System.Collections.Concurrent;

using RabbitMQ.Client;
namespace Faction.Common.Backend.EventBus.Abstractions
{
    public interface IEventBus
    {
    IBasicProperties Properties { get; set; }
    BlockingCollection<string> ResponseQueue { get; set; }
    void Initialize();
    void Publish<T>(T TMessage, string replyTo = null, string correlationId = null, bool replyExpected = false);
    void Subscribe<T, TH>() where TH : IEventHandler<T>;
    }
}