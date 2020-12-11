using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Domain.Features.Admins
{
    public record AdminDto(Guid AdminId, string Name, Email Email);
}
