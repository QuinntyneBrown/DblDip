using System;

namespace ShootQ.Domain.Features.Cards
{
    public record CardDto(Guid CardId, string Name, string Description);
}
