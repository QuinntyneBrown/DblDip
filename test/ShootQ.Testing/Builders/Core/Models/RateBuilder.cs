using ShootQ.Core.Models;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class RateBuilder
    {
        private Rate _rate;

        public static Rate WithDefaults()
        {
            return new Rate(default, default, default);
        }

        public RateBuilder()
        {
            _rate = WithDefaults();
        }

        public Rate Build()
        {
            return _rate;
        }
    }
}
