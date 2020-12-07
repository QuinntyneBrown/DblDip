using BuildingBlocks.Core;
using BuildingBlocks.EventStore;
using ShootQ.Core.Models;
using System.Linq;

namespace ShootQ.Core.Data
{
    public interface IDataIntegrityService : IUsernameAvailabilityCheck
    {

    }
    public class DataIntegrityService : IDataIntegrityService
    {
        private readonly IAggregateSet _aggregateSet;
        public DataIntegrityService(IAggregateSet aggregateSet)
        {
            _aggregateSet = aggregateSet;
        }

        public bool IsAvailable(string email)
            => _aggregateSet.Set<User>()
                .Where(x => ((string)x.Username).ToLower() == email.ToLower()).Any() == false;
    }
}
