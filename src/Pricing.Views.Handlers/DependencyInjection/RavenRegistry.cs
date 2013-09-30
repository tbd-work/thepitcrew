using Raven.Client;
using Raven.Client.Document;
using StructureMap.Configuration.DSL;

namespace Pricing.Views.Handlers.DependencyInjection
{
    public class RavenRegistry : Registry
    {
        public RavenRegistry()
        {
            For<IDocumentStore>().Singleton().Use(() => new DocumentStore
                                                        {
                                                            ConnectionStringName = "Raven"
                                                        }.Initialize());
        }
    }
}