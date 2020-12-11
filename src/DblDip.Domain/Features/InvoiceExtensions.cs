using DblDip.Core.Models;
using DblDip.Domain.Features.Invoices;

namespace DblDip.Domain.Features
{
    public static class InvoiceExtensions
    {
        public static InvoiceDto ToDto(this Invoice invoice)
        {
            return new InvoiceDto
            {

            };
        }
    }
}
