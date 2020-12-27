using System;

namespace DblDip.Core.DomainEvents
{
    public record ReceiptCreated (Guid ReceiptId);
    public record ReceiptRemoved (DateTime Deleted);
}
