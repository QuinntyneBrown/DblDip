using ShootQ.Core.Models;
using System;
using Xunit;

namespace ShootQ.Domain.UnitTests.Models
{
    public class UserTests
    {
        [Fact]
        public void ShouldCreateUser()
        {
            var user = new User("quinntynebrown@gmail.com", "password");

            Assert.NotEqual(default, user.UserId);
            Assert.NotEqual(default, user.Roles);
        }

        [Fact]
        public void ShouldAddRole()
        {
            var user = new User("quinntynebrown@gmail.com", "password");
            user.AddRole(Guid.NewGuid(), "Admin");
            Assert.Single(user.Roles);
        }

        [Fact]
        public void ShouldRemoveRole()
        {
            var user = new User("quinntynebrown@gmail.com", "password");
            user.AddRole(Guid.NewGuid(), "Admin");
            user.RemoveRole(Guid.NewGuid(), "Admin");
            Assert.Empty(user.Roles);
        }

        [Fact]
        public void ShouldChangePassword()
        {
            var user = new User("quinntynebrown@gmail.com", "");
            var salt1 = user.Salt;
            user.ChangePassword("Foo");
            var salt2 = user.Salt;

            Assert.Equal("Foo", user.Password);
            Assert.Equal(salt1, salt2);
        }
    }
}
