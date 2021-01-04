using DblDip.Core.Models;
using DblDip.Domain.Features.Invoices;

namespace DblDip.Testing.Builders
{
    public class InvoiceDtoBuilder
    {
        private InvoiceDto _invoiceDto;

        public static InvoiceDto WithDefaults()
        {
            return new InvoiceDto();
        }

        public InvoiceDtoBuilder()
        {
            _invoiceDto = WithDefaults();
        }

        public InvoiceDto Build()
        {
            return _invoiceDto;
        }
    }
}
