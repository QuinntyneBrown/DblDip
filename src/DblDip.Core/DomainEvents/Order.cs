using System;

namespace DblDip.Core.DomainEvents
{
    public record OrderPaid();
    public record OrderCheckedOut();
    public record OrderRemoved (DateTime Deleted);
    public record OrderUpdated;
}
