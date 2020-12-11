using System;

namespace DblDip.Domain.Features.Libraries
{
    public class LibraryDto
    {
        public Guid LibraryId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
