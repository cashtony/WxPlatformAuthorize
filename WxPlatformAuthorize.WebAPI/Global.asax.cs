using System.Configuration;
using System.IO;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using WxPlatformAuthorize.Service;
using WxPlatformAuthorize.WebAPI.Controllers;
using WxPlatformAuthorize.WebAPI.Filters;

namespace WxPlatformAuthorize.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.RegisterAutofac(GlobalConfiguration.Configuration, RegisterServices);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(Server.MapPath("log4net.config")));
            builder.RegisterInstance(log4net.LogManager.GetLogger("WxPlatformAuthorize")).As<log4net.ILog>().SingleInstance();
            builder.RegisterInstance(new WxSDK.WxApiClientConfig()
            {
                ComponentAppId = ConfigurationManager.AppSettings["WxSDK.ComponentAppId"],
                ComponentAppSecret = ConfigurationManager.AppSettings["WxSDK.ComponentAppSecret"],
                Token = ConfigurationManager.AppSettings["WxSDK.Token"],
                EncodingAESKey = ConfigurationManager.AppSettings["WxSDK.EncodingAESKey"]
            });
            builder.RegisterType<WxSDK.HttpClient>().As<WxSDK.IHttpClient>();
            builder.RegisterType<WxSDK.WxApiClient>();
            builder.RegisterType<AuthorizeService>().SingleInstance();
        }
    }
}
