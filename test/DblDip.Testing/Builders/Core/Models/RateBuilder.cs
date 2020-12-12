using DblDip.Core.Models;
using DblDip.Core.ValueObjects;
using System;

namespace DblDip.Testing.Builders.Core.Models
{
    public class RateBuilder
    {
        private Rate _rate;

        public static Rate WithDefaults()
        {
            return new Rate("Test", (Price)10, Guid.NewGuid());
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
