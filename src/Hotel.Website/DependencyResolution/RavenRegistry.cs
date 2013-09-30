using Raven.Client;
using Raven.Client.Document;
using StructureMap.Configuration.DSL;

namespace Hotel.Website.DependencyResolution
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
        }
    }
}