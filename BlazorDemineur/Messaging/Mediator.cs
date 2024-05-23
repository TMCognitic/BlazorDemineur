namespace BlazorDemineur.Messaging
{
    public class Mediator<TMessage> : IMediator<TMessage>
    {
        public static readonly Mediator<TMessage> Instance = new ();

        private Action<TMessage>? _actions;

        private Mediator()
        {
            
        }

        public void Register(Action<TMessage> action)
        {
            _actions += action;
        }

        public void Unregister(Action<TMessage> action)
        {
            _actions -= action;
        }

        public void Send(TMessage message)
        {
            Action<TMessage>? actions = _actions;
            actions?.Invoke(message);
        }
    }
}
