using System.Collections.Generic;

namespace Social.Domain
{
    public abstract class AggregateRoot<TId>
    {
        private readonly List<object> _events = new List<object>();

        public void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _events.Add(@event);
        }

        public TId Id { get; protected set; }

        public IEnumerable<object> GetEvents() => _events;

        public long Version { get; private set; }

        protected abstract bool EnsureValidState();
        
        protected abstract void When(object @event);

        public void Load(IEnumerable<object> events)
        {
            foreach (var @event in events)
            {
                When(@event);
                Version++;
            }
        }

    }
}