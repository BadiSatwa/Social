using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Social.Infra
{
    public interface IEventStore
    {
        IAsyncEnumerable<object> GetEvents(string id);

        Task<T> GetStreamMetadata<T>(string id, string key);

        Task AppendEvents(string id, IEnumerable<object> @events, long expectedVersion);

        Task StoreStreamMetadata<T>(string id, string key, T data);
    }
}