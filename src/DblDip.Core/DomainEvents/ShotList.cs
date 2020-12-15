namespace DblDip.Core.DomainEvents
{
    public record ShotAdded (string Value);
    public record ShotRemoved (string Value);
    public record ShotListCreated (string Value);
    public record ShotListUpdated (string Value);
    public record ShotListRemoved (string Value);
}
