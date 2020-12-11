using System;

namespace DblDip.Domain.Features.Invoices
{
    public class InvoiceDto
    {
        public Guid InvoiceId { get; private set; }
        public DateTime? Deleted { get; private set; }
    }
}
