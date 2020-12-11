namespace ShootQ.Core.DomainEvents
{
    public record YouTubeVideo (string Value);
    public record YouTubeVideoRemoved(string Value);
    public record YouTubeVideoUpdated(string Value);
}
