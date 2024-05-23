
namespace BlazorDemineur.Messaging
{
    public interface IMediator<TMessage>
    {
        void Register(Action<TMessage> action);
        void Send(TMessage message);
        void Unregister(Action<TMessage> action);
    }
}