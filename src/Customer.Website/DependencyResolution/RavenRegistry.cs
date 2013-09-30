using Common.NServiceBus;
using Common.Persistence;
using Common.Publishers;
using NServiceBus;
using Raven.Client;
using Raven.Client.Document;
using StructureMap.Configuration.DSL;

namespace Customer.Website.DependencyResolution
{
    public class RavenRegistry : Registry
    {
        public RavenRegistry()
        {
            For<IDocumentStore>().Singleton().Use(() => new DocumentStore
                                                        {
                                                            ConnectionStringName = "Raven"
                                                        }.Initialize());

            FillAllPropertiesOfType<IDocumentStore>();

            For<IPublisher>().Singleton().Use<NServiceBusPublisher>();
            For<IRepository>().Singleton().Use<RavenRepository>();            
        }
    }
}