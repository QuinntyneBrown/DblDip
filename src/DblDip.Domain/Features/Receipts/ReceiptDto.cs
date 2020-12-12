using System;

namespace DblDip.Domain.Features.Receipts
{
    public class ReceiptDto
    {
        public Guid ReceiptId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
