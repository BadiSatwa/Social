using System.Threading.Tasks;

namespace Social.Domain.Shared
{
    public interface IMembersLookup
    {
        bool Exists(MemberId member);
    }
}