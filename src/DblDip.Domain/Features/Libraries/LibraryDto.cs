using System;

namespace DblDip.Domain.Features.Libraries
{
    public class LibraryDto
    {
        public Guid LibraryId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
