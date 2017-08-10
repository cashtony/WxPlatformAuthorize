using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using WxPlatformAuthorize.Service;

namespace WxPlatformAuthorize.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ConfigureAutofac();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private void ConfigureAutofac()
        {
            var builder = new ContainerBuilder();
            SetupResolveRules(builder);
            builder.RegisterApiControllers(typeof(Controllers.DevController).Assembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<WxSDK.ApiClient>().As<WxSDK.IApiClient>();
            builder.RegisterType<AuthorizeService>().SingleInstance();
        }
    }
}
