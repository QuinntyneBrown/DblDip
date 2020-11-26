using ShootQ.Core.Models;
using System.Threading.Tasks;
using Xunit;

namespace ShootQ.Domain.UnitTests.Models
{
    public class UserTests
    {

        public UserTests()
        {

        }

        [Fact]
        public async Task Should()
        {
            var user = new User("quinntynebrown@gmail.com", "password");

            Assert.NotEqual(default, user.UserId);
        }
    }
}
