using ShootQ.Core.Models;
using ShootQ.Domain.Features.Weddings;

namespace ShootQ.Domain.Features
{
    public static class WeddingExtensions
    {
        public static WeddingDto ToDto(this Wedding wedding)
        {
            return new WeddingDto
            {
                WeddingId = wedding.WeddingId,
                Parts = wedding.Parts
            };
        }
    }
}
