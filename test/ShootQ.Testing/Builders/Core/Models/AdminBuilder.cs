using ShootQ.Core.Models;
using ShootQ.Core.ValueObjects;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class AdminBuilder
    {
        private Admin _admin;

        public static Admin WithDefaults()
        {
            return new Admin("quinntyne",(Email)"quinntynebrown@gmail.com");
        }

        public AdminBuilder()
        {
            _admin = WithDefaults();
        }

        public Admin Build()
        {
            return _admin;
        }
    }
}
