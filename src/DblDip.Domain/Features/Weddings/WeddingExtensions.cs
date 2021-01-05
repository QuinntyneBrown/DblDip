using DblDip.Core.Models;
using DblDip.Domain.Features;

namespace DblDip.Domain.Features
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
