using System.Collections.Generic;

namespace Social.Infra.Projections
{
    public interface IProjectionDefinitionsProvider
    {
        IEnumerable<(string Name, string Query)> GetProjectionDefinitions();
    }
}