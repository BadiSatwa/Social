using System;
using System.Net;
using System.Threading.Tasks;
using EventStore.ClientAPI.Common.Log;
using EventStore.ClientAPI.Projections;
using EventStore.ClientAPI.SystemData;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Social.Infra.EventStore;
using Social.Infra.Shared;

namespace Social.Infra.Projections
{
    public class EventStoreProjectionsManager : IProjectionsManager
    {
        private readonly ILogger<EventStoreProjectionsManager> _logger;
        private readonly IProjectionDefinitionsProvider _projectionDefinitions;
        private readonly ProjectionsManager _projectionsManager;
        private readonly UserCredentials _userCredentials;

        public EventStoreProjectionsManager(ILogger<EventStoreProjectionsManager> logger,
            IProjectionDefinitionsProvider projectionDefinitions,
            IOptions<EventStoreOptions> eventStoreOptions)
        {
            _logger = logger;
            _projectionDefinitions = projectionDefinitions;
            _projectionsManager = new ProjectionsManager(new ConsoleLogger(),
                new IPEndPoint(IPAddress.Parse(eventStoreOptions.Value.Address), eventStoreOptions.Value.ProjectionsPort),
                TimeSpan.FromMinutes(1));
            _userCredentials = new UserCredentials(eventStoreOptions.Value.UserName, eventStoreOptions.Value.Password);
        }

        public async Task<T> GetResults<T>(string projection)
        {
            var result = await _projectionsManager.GetResultAsync(projection, _userCredentials);
            return result.ToObject<T>();
        }

        public async Task UpdateAllProjections()
        {
            _logger.LogInformation($"Updating Projections at {DateTime.UtcNow}");

            var definitions = _projectionDefinitions.GetProjectionDefinitions();

            var allProjections = await _projectionsManager.ListAllAsync(_userCredentials);

            foreach (var definition in definitions)
            {
                if (!allProjections.Exists(pd => pd.Name == definition.Name))
                {
                    await _projectionsManager.CreateContinuousAsync(definition.Name, definition.Query, _userCredentials);
                    continue;
                }
                
                var query = await _projectionsManager.GetQueryAsync(definition.Name, _userCredentials);
                if (query != definition.Query)
                {
                    _logger.LogInformation($"Updating Projection {definition.Name}!");
                    await _projectionsManager.UpdateQueryAsync(definition.Name, definition.Query, _userCredentials);
                    await _projectionsManager.ResetAsync(definition.Name, _userCredentials);
                }
            }
        }
    }
}