using DblDip.Core.Models;

namespace DblDip.Testing.Builders
{
    public class InvoiceBuilder
    {
        private Invoice _invoice;

        public static Invoice WithDefaults()
        {
            return new Invoice();
        }

        public InvoiceBuilder()
        {
            _invoice = WithDefaults();
        }

        public Invoice Build()
        {
            return _invoice;
        }
    }
}
