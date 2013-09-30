using Common.NServiceBus;
using Common.Persistence;
using Common.Publishers;
using Raven.Client;
using Raven.Client.Document;
using StructureMap.Configuration.DSL;

namespace Pricing.Handlers.DependencyInjection
{
    public class RavenRegistry : Registry
    {
        public RavenRegistry()
        {
            For<IDocumentStore>().Singleton().Use(() => new DocumentStore
                                            {
                                                ConnectionStringName = "Raven"
                                            }.Initialize());

            For<IPublisher>().Use<NServiceBusPublisher>();
            For<IRepository>().Use<RavenRepository>();
        }
    }
}