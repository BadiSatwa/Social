using System.Threading.Tasks;
using Social.Domain;

namespace Social.Application
{
    public interface IAggregateStore
    {
        Task<T> Load<T, TId>(TId id) where T : AggregateRoot<TId>;
        Task Save<T, TId>(T aggregate) where T : AggregateRoot<TId>;
    }
}