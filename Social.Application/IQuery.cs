using System.Threading.Tasks;

namespace Social.Application
{
    public interface IQuery<in TArg, TResult>
    {
        Task<TResult> Execute(TArg arg);
    }
}