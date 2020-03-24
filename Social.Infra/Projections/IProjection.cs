using System.Threading.Tasks;

namespace Social.Infra.Projections
{
    public interface IProjection
    {
        Task Project(object @event);
    }
}