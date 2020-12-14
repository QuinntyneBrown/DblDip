using System;

namespace DblDip.Domain.Features.ShotLists
{
    public class ShotListDto
    {
        public Guid ShotListId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
