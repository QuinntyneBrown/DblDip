using System;

namespace DblDip.Domain.Features.Boards
{
    public class BoardDto
    {
        public Guid BoardId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
