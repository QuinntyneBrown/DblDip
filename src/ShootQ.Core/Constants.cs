using System;

namespace ShootQ.Core
{
    public static class Constants
    {
        public static class Rates
        {
            public static readonly Guid PhotographyRate = new Guid("6af71681-ad24-4341-97e7-a0f654a395b1");
            public static readonly Guid TravelRate = new Guid("dd9d8c0e-da38-4b21-a62a-4a1b0505db5f");
            public static readonly Guid ConsulationRate = new Guid("35ab4b3a-2409-47be-a797-991859aa1b36");
        }

        public static class ClaimTypes
        {
            public static readonly string UserId = nameof(UserId);
        }
    }
}
