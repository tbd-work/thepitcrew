


using StructureMap;

namespace Pricing.Views.Handlers
{
    using NServiceBus;

	/*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
	    public void Init()
	    {
	        ObjectFactory.Initialize(cfg => cfg.Scan(scan =>
	                                                 {
	                                                     scan.AssembliesFromApplicationBaseDirectory();
	                                                     scan.LookForRegistries();
	                                                     scan.WithDefaultConventions();
	                                                 }));

	        Configure.With()
	            .DefiningCommandsAs(cfg => cfg.Namespace != null && cfg.Namespace.StartsWith("Commands"))
	            .DefiningEventsAs(cfg => cfg.Namespace != null && cfg.Namespace.StartsWith("Events"))
	            .StructureMapBuilder(ObjectFactory.Container)
                .RavenSubscriptionStorage()
                .UseTransport<Msmq>();

	        Configure.Serialization.Xml();
	    }
    }
}