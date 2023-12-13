namespace MultiSync.Services
{
    public class EventManager
    {
        private readonly Dictionary<Type, List<Action<object>>> _eventSubscribers = new Dictionary<Type, List<Action<object>>>();

        public void Subscribe<TEvent>(object subscriber, Action<TEvent> handler)
            where TEvent : EventArgs
        {
            var eventType = typeof(TEvent);

            if (!_eventSubscribers.TryGetValue(eventType, out var handlers))
            {
                handlers = new List<Action<object>>();
                _eventSubscribers[eventType] = handlers;
            }

            handlers.Add(obj => handler.Invoke((TEvent)obj));
        }

        public void Publish<TEvent>(TEvent eventArgs)
            where TEvent : EventArgs
        {
            var eventType = typeof(TEvent);

            if (_eventSubscribers.TryGetValue(eventType, out var handlers))
            {
                foreach (var handler in handlers)
                {
                    handler.Invoke(eventArgs);
                }
            }
        }
    }
}
