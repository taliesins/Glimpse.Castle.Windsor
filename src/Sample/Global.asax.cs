using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Glimpse.Castle.Windsor;
using Sample.App_Start;
using Sample.Components;

namespace Sample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            ConfigureWindsor();
        }

        private static void ConfigureWindsor()
        {
            var container = new WindsorContainer();


            // Register some dependencies
            container.Register(
                
                
                Classes.FromThisAssembly()
                            .BasedOn<IComponent>()
                            .WithServiceAllInterfaces()
                            .LifestyleTransient(),
                                      
                Component.For<IComponent2>()
                            .ImplementedBy<Component2>()
                            .LifestyleTransient()
                            .Interceptors<LoggingInterceptor>(),

                Component.For<LoggingInterceptor>(),

                                       
                Component.For<IComponent4>()
                            .ImplementedBy<Component4>()
                            .LifestylePerWebRequest(),

                Classes.FromThisAssembly()
                            .BasedOn<IController>()
                            .LifestyleTransient()
                                      
                );

            
            var controllerFactory = new WindsorControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            // Here we wire up the Castle Windsor Glimpse plugin by installing the glimpse installer
            container.Install(new GlimpseInstaller());
            
        }
    }

    
   
}