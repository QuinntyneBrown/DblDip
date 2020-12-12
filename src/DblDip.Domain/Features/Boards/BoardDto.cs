using System;

namespace DblDip.Domain.Features.Boards
{
    public class BoardDto
    {
        public Guid BoardId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
