using System;

namespace DblDip.Domain.Features
{
    public class InvoiceDto
    {
        public Guid InvoiceId { get; init; }
        public DateTime? Deleted { get; init; }
    }
}
