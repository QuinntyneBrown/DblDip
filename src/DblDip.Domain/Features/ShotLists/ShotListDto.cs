using System;

namespace DblDip.Domain.Features.ShotLists
{
    public class ShotListDto
    {
        public Guid ShotListId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
