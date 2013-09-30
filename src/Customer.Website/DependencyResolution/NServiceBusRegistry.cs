using NServiceBus;
using StructureMap.Configuration.DSL;
using NSBConfig = NServiceBus.Configure;

namespace Customer.Website.DependencyResolution
{
    public class NServiceBusRegistry : Registry
    {
        public NServiceBusRegistry()
        {
            var bus = NSBConfig.With()
                .DefiningCommandsAs(
                    cfg => cfg.Namespace != null && cfg.Namespace.StartsWith("Commands"))
                .StructureMapBuilder()
                .UseTransport<Msmq>()
                .UnicastBus()
                .SendOnly();
            
            SetLoggingLibrary.NLog();

            For<IBus>().Singleton().Use(bus);

            FillAllPropertiesOfType<IBus>();
        }
    }
}