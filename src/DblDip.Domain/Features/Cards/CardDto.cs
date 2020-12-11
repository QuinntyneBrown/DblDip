using System;

namespace DblDip.Domain.Features.Cards
{
    public record CardDto(Guid CardId, string Name, string Description);
}
