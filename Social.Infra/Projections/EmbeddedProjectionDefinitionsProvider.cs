using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace Social.Infra.Projections
{
    public class EmbeddedProjectionDefinitionsProvider : IProjectionDefinitionsProvider
    {
        private readonly ManifestEmbeddedFileProvider _provider = new ManifestEmbeddedFileProvider(typeof(IProjectionDefinitionsProvider).Assembly);

        public IEnumerable<(string Name, string Query)> GetProjectionDefinitions()
        {
            var files = _provider.GetDirectoryContents("EventStore/ProjectionDefinitions");
            foreach (var file in files)
            {
                using var streamReader = new StreamReader(file.CreateReadStream());
                yield return (Path.GetFileNameWithoutExtension(file.Name), streamReader.ReadToEnd());
            }
        }
    }
}