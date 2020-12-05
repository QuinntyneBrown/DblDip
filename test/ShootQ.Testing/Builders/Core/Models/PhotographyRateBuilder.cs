using ShootQ.Core.Models;
using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class PhotographyRateBuilder
    {
        private PhotographyRate _photographyRate;

        public static PhotographyRate WithDefaults()
        {
            return new PhotographyRate((Price)1m);
        }

        public PhotographyRateBuilder()
        {
            throw new NotImplementedException();
        }

        public PhotographyRate Build()
        {
            return _photographyRate;
        }
    }
}
