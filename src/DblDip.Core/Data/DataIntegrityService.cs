using BuildingBlocks.EventStore;
using DblDip.Core.Models;
using System.Linq;

namespace DblDip.Core.Data
{
    public class DataIntegrityService : IDataIntegrityService
    {
        public bool IsAvailable(string email)
        {
            return true;
        }
    }
}
