using System.Web.Http;
using Autofac;
using WxPlatformAuthorize.Service;

namespace WxPlatformAuthorize.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.RegisterAutofac(GlobalConfiguration.Configuration, RegisterTypes);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterInstance(new WxSDK.WxApiClientConfig()
            {
                ComponentAppId = "_TestComponentAppId_",
                ComponentAppSecret = "_TestComponentAppSecret_"
            });
            builder.RegisterType<WxSDK.HttpClient>().As<WxSDK.IHttpClient>();
            builder.RegisterType<WxSDK.WxApiClient>();
            builder.RegisterType<AuthorizeService>().SingleInstance();
        }
    }
}
