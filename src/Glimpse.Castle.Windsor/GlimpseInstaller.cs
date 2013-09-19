using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Glimpse.Castle.Windsor
{
    public class GlimpseInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            WindsorInspector.Kernal = () => container.Kernel;
        }
    }
}