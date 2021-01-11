using BuildingBlocks.Core;
using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class UserBuilder
    {
        private User _user;

        public static User WithDefaults(IUsernameAvailabilityCheck usernameAvailabilityCheck)
        {
            return new User("test@gmail.com", "DblDip", usernameAvailabilityCheck);
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

    public class RoleReferenceBuilder
    {
        private RoleReference _role;

        public RoleReferenceBuilder(string name)
        {
            _role = new RoleReference(default, name);
        }

        public RoleReference Build()
        {
            return _role;
        }
    }
}
