using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Social.Infra.Projections;
using Social.Infra.Shared;

namespace Social.Infra
{
    public class ProjectionsDispatcher
    {
        private readonly IEventStoreConnection _connection;
        private readonly ILogger<ProjectionsDispatcher> _logger;
        private readonly IServiceProvider _serviceProvider;

        private EventStoreAllCatchUpSubscription _subscription;

        public ProjectionsDispatcher(IEventStoreConnection connection, ILogger<ProjectionsDispatcher> logger, IServiceProvider serviceProvider)
        {
            _connection = connection;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public void Start()
        {
            var settings = new CatchUpSubscriptionSettings(2000, 500, false, false, "social-subscription");
            _subscription = _connection.SubscribeToAllFrom(Position.Start, settings, EventAppeared);
        }

        private async Task EventAppeared(EventStoreCatchUpSubscription sub, ResolvedEvent @event)
        {
            if (@event.Event.EventType.StartsWith("$")) return;
            
            _logger.LogInformation($"Event Received: {@event.Event.EventType}");
            var domainEvent = @event.ToDomainEvent();
            using var scope = _serviceProvider.CreateScope();
            var projections = scope.ServiceProvider.GetServices<IProjection>();
            await Task.WhenAll(tasks: projections.Select(p => p.Project(domainEvent)).ToArray());
        }

        public void Stop()
        {
            _subscription.Stop();
        }
    }
}