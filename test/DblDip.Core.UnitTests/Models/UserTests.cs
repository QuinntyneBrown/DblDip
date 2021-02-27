using DblDip.Core.Models;
using System;
using Xunit;

namespace DblDip.Domain.UnitTests.Models
{
    public class UserTests
    {
        [Fact]
        public void ShouldCreateUser()
        {
            var actual = new User("quinntynebrown@gmail.com", "password");

            Assert.NotEqual(default, actual.UserId);
            Assert.NotEqual(default, actual.Roles);
            Assert.False(actual.PasswordResetRequired);
        }

        [Fact]
        public void ShouldRequirePasswordReset()
        {
            var actual = new User("quinntynebrown@gmail.com");

            Assert.NotEqual(default, actual.UserId);
            Assert.NotEqual(default, actual.Roles);
            Assert.True(actual.PasswordResetRequired);
        }

        [Fact]
        public void ShouldAddRole()
        {
            var actual = new User("quinntynebrown@gmail.com", "password");
            actual.AddRole(Guid.NewGuid(), "Admin");
            Assert.Single(actual.Roles);
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
            var expectedPassword = "Foo";

            var actual = new User("quinntynebrown@gmail.com", "");
            var salt1 = actual.Salt;
            actual.ChangePassword(expectedPassword);
            var salt2 = actual.Salt;

            Assert.Equal(expectedPassword, actual.Password);
            Assert.Equal(salt1, salt2);
        }
    }
}
