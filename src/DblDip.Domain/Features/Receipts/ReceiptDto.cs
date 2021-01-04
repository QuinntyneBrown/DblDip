using System;

namespace DblDip.Domain.Features
{
    public class ReceiptDto
    {
        public Guid ReceiptId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
