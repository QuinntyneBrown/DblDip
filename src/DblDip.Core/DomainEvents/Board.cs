using System;

namespace DblDip.Core.DomainEvents
{
    public record BoardCreated (Guid BoardId, string Name);
    public record BoardUpdated (string Value);
    public record BoardRemoved (DateTime Deleted);
}
