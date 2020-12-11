using MediatR;
using DblDip.Core.Models;

namespace DblDip.Domain.IntegrationEvents
{
    public record ProfileCreated(Profile Profile) : INotification;
}
