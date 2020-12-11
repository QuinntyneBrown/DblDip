using MediatR;
using ShootQ.Core.Models;

namespace ShootQ.Domain.IntegrationEvents
{
    public record ProfileCreated(Profile Profile) : INotification;
}
