using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Social.Infra.Shared;

namespace Social.Infra.EventStore
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreConnection _connection;

        public EventStore(IEventStoreConnection connection)
        {
            _connection = connection;
        }

        public async IAsyncEnumerable<object> GetEvents(string id)
        {
            StreamEventsSlice slice = null;
            do
            {
                slice = await _connection.ReadStreamEventsForwardAsync(id, slice?.LastEventNumber + 1 ?? StreamPosition.Start, 200, false);

                foreach (var @event in slice.Events)
                {
                    var metadata = @event.Event.Metadata.AsString().ToObject<EventMetadata>();
                    var eventType = Type.GetType(metadata.EventType);
                    var domainEvent = @event.Event.Data.AsString().ToObject<object>(eventType);
                    yield return domainEvent;
                }

            } while (!slice.IsEndOfStream);
        }

        public async Task AppendEvents(string id, IEnumerable<object> events, long expectedVersion)
        {
            await _connection.AppendToStreamAsync(id, expectedVersion - 1, CreateEvents());

            IEnumerable<EventData> CreateEvents()
            {
                foreach (var domainEvent in events)
                {
                    var @event = new EventData(
                        Guid.NewGuid(),
                        domainEvent.GetType().Name,
                        true,
                        domainEvent.ToJson().ToBytes(),
                        new EventMetadata(domainEvent.GetType().AssemblyQualifiedName).ToJson().ToBytes()
                    );
                    yield return @event;
                }
            }
        }
    }
}