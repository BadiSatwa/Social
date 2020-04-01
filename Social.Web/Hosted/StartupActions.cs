using System;
using System.Threading;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Social.Infra;
using Social.Infra.Projections;

namespace Social.Web.Hosted
{
    public class StartupActions : IHostedService
    {
        private readonly ILogger<StartupActions> _logger;
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly IServiceProvider _serviceProvider;

        public StartupActions(ILogger<StartupActions> logger, IEventStoreConnection eventStoreConnection, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _eventStoreConnection = eventStoreConnection;
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting StartupActions");

            await _eventStoreConnection.ConnectAsync();

            using var scope = _serviceProvider.CreateScope();

            var projectionsManager = scope.ServiceProvider.GetRequiredService<IProjectionsManager>();
            await projectionsManager.UpdateAllProjections();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StartupActions Shutting Down");

            _eventStoreConnection.Dispose();

            return Task.CompletedTask;
        }
    }
}