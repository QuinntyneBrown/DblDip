using BuildingBlocks.Core;
using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders
{
    public class UserBuilder
    {
        private User _user;

        public static User WithDefaults(IUsernameAvailabilityCheck usernameAvailabilityCheck)
        {
            return new User("quinntynebrown@gmail.com", "ShootQ", usernameAvailabilityCheck);
        }

        public UserBuilder(string username, string password)
        {
            _user = new User(username, password);
        }

        public User Build()
        {
            return _user;
        }
    }

    public class RoleBuilder
    {
        private User.Role _role;

        public RoleBuilder(string name)
        {
            _role = new User.Role(name);
        }

        public User.Role Build()
        {
            return _role;
        }
    }
}
