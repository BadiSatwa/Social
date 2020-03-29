using System;
using System.Linq;
using System.Threading.Tasks;
using Social.Application;
using Social.Domain;

namespace Social.Infra
{
    public class EventsAggregateStore : IAggregateStore
    {
        private const string AggregateRootTypeKey = "aggregate_root_type";  

        private readonly IEventStore _eventStore;

        public EventsAggregateStore(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<T> Load<T, TId>(TId id) where T : AggregateRoot<TId>
        {
            var rootType = await _eventStore.GetStreamMetadata<string>(id.ToString(), AggregateRootTypeKey); //Because inheritance we need to store stream somewhere
            var events = _eventStore.GetEvents(id.ToString());
            var aggregate = (T)Activator.CreateInstance(Type.GetType(rootType, true), true);
            aggregate.Load(await events.ToListAsync());
            return aggregate;
        }

        public async Task Save<T, TId>(T aggregate) where T : AggregateRoot<TId>
        {
            await _eventStore.StoreStreamMetadata(aggregate.Id.ToString(), AggregateRootTypeKey, aggregate.GetType().AssemblyQualifiedName);
            await _eventStore.AppendEvents(aggregate.Id.ToString(), aggregate.GetEvents(), aggregate.Version);
        }
    }
}