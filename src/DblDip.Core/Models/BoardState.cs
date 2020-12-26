namespace DblDip.Core.Models
{
    public record BoardState(string Name, int SortOrder, ICollection<TicketState> TicketStates);
}
