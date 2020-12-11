using ShootQ.Core.ValueObjects;
using System;

namespace ShootQ.Core.DomainEvents
{
    public record ProfileCreated (Guid ProfileId, string Name, Email Email, string Type, string DotNetType);
}
