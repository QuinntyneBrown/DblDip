using System;

namespace ShootQ.Domain.Features.Libraries
{
    public class LibraryDto
    {
        public Guid LibraryId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
