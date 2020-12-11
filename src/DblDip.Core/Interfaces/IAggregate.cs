namespace DblDip.Core.Interfaces
{
    public interface IAggregate
    {
        void Apply(object @event);
    }
}
