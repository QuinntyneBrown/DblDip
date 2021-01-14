using DblDip.Api;
using DblDip.Testing.Factories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DblDip.Testing.Builders
{
    public class MediatorBuilder
    {
        private IMediator _mediator;

        public static IMediator WithDefaults()
        {
            var services = new ServiceCollection();

            Dependencies.Configure(services, ConfigurationFactory.Create());

            return services.BuildServiceProvider().GetRequiredService<IMediator>();
        }

        public MediatorBuilder()
        {
            _mediator = WithDefaults();
        }

        public IMediator Build()
        {
            return _mediator;
        }
    }
}
