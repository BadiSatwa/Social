using System.Threading.Tasks;

namespace Social.Infra.Projections
{
    public interface IProjectionsManager
    {
        Task<T> GetResults<T>(string projection);
        Task UpdateAllProjections();
    }
}