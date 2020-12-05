using System.Threading.Tasks;
using Xunit;

namespace ShootQ.Core.UnitTests.ValueObjects
{
    public class LocationTests
    {

        public LocationTests()
        {

        }

        [Fact]
        public async Task Should_CalculateDistance()
        {
            var location1 = ShootQ.Core.ValueObjects.Location.Create(-79.3860586, 43.6604976).Value;
            var location2 = ShootQ.Core.ValueObjects.Location.Create(-79.3860586, 43.6604976).Value;

            var distance = location1.Distance(location2);

            Assert.Equal(0, distance);
        }
    }
}
