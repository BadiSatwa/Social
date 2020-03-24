using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Social.Infra;

namespace Social.Web.Hosted
{
    public class HostedProjections : IHostedService
    {
        private readonly ILogger<HostedProjections> _logger;
        private readonly ProjectionsDispatcher _dispatcher;

        public HostedProjections(ILogger<HostedProjections> logger, ProjectionsDispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Projections Starting");
            _dispatcher.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Projections Shutting Down");
            _dispatcher.Stop();
            return Task.CompletedTask;
        }
    }
}