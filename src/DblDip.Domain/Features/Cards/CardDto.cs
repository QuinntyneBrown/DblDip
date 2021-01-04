using System;

namespace DblDip.Domain.Features
{
    public record CardDto(Guid CardId, string Name, string Description);
}
