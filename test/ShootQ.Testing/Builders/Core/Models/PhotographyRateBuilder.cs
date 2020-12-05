using ShootQ.Core.Models;
using ShootQ.Core.ValueObjects;
using System;
using static ShootQ.Core.Constants.Rates;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class PhotographyRateBuilder
    {
        private Rate _photographyRate;

        public static Rate WithDefaults()
        {
            return new Rate(nameof(PhotographyRate), (Price)100, PhotographyRate);
        }

        public PhotographyRateBuilder()
        {
            throw new NotImplementedException();
        }

        public Rate Build()
        {
            return _photographyRate;
        }
    }
}
