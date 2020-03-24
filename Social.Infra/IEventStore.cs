using System.Collections.Generic;
using System.Threading.Tasks;

namespace Social.Infra
{
    public interface IEventStore
    {
        IAsyncEnumerable<object> GetEvents(string id);

        Task AppendEvents(string id, IEnumerable<object> @events, long expectedVersion);
    }
}