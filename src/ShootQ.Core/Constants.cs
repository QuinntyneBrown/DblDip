using System;

namespace ShootQ.Core
{
    public static class Constants
    {
        public static class Rates
        {
            public static readonly Guid PhotographyRate = new Guid("6af71681-ad24-4341-97e7-a0f654a395b1");
        }

        public static class ClaimTypes
        {
            public static readonly string UserId = nameof(UserId);
        }
    }
}
