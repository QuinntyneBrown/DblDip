using BuildingBlocks.Abstractions;
using DblDip.Domain.IntegrationEvents;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DblDip.Domain.Sagas
{
    internal class QuoteCreatedSaga : INotificationHandler<QuoteCreated>
    {
        private readonly IAppDbContext _context;

        public QuoteCreatedSaga(IAppDbContext context)
        {
            _context = context;
        }

        public async Task Handle(QuoteCreated notification, CancellationToken cancellationToken)
        {
            
        }
    }
}
