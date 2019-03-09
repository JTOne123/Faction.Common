using System.Threading.Tasks;

namespace Faction.Common.Backend.EventBus.Abstractions
{
    public interface IEventHandler<T> : IEventHandler 
    {
        Task Handle(T TMessage, string ReplyTo, string CorrelationId);
    }

    public interface IEventHandler
    {
    }
}