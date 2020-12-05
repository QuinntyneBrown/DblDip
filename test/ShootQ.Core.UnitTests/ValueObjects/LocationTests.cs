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

        [Fact]
        public async Task Should_CalculateDistanceBetweenCloseLocations()
        {
            var unionStation = ShootQ.Core.ValueObjects.Location.Create(-79.377750, 43.645550).Value;
            var eatonCenter = ShootQ.Core.ValueObjects.Location.Create(-79.379288, 43.654919).Value;

            var distance = unionStation.Distance(eatonCenter);

            Assert.Equal(0, distance);
        }
    }
}
