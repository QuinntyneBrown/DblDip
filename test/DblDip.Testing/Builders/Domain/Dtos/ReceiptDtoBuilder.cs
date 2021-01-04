using DblDip.Core.Models;
using DblDip.Domain.Features.Receipts;

namespace DblDip.Testing.Builders
{
    public class ReceiptDtoBuilder
    {
        private ReceiptDto _receiptDto;

        public static ReceiptDto WithDefaults()
        {
            return new ReceiptDto();
        }

        public ReceiptDtoBuilder()
        {
            _receiptDto = WithDefaults();
        }

        public ReceiptDto Build()
        {
            return _receiptDto;
        }
    }
}
