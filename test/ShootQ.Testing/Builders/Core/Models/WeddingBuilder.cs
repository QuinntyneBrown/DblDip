using ShootQ.Core.Models;
using System;

namespace ShootQ.Testing.Builders.Core.Models
{
    public class WeddingBuilder
    {
        private Wedding _wedding;

        public static Wedding WithDefaults(PhotographyRate photographyRate)
        {
            return new Wedding(Guid.NewGuid(), DateTime.UtcNow, 5, photographyRate.PhotographyRateId);
        }

        public WeddingBuilder()
        {
            throw new NotImplementedException();
        }

        public Wedding Build()
        {
            return _wedding;
        }
    }
}
