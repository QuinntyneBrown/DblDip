using System.Threading.Tasks;

namespace BuildingBlocks.Core
{
    public interface IUsernameAvailabilityCheck
    {
        bool IsAvailable(string email);
    }
}
