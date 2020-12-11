using ShootQ.Core.Models;
using ShootQ.Domain.Features.Invoices;

namespace ShootQ.Testing.Builders.Domain.Dtos
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
