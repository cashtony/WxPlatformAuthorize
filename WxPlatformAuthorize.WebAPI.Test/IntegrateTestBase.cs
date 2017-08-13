using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Autofac;
using WxPlatformAuthorize.Service;

namespace WxPlatformAuthorize.WebAPI.Test
{
    public class IntegrateTestBase : WxSDK.IHttpClient
    {
        private HttpMessageInvoker _messageInvoker;

        protected Func<string, string, string> OnMockWxServerRequest;

        public object GlobalConfiguration { get; private set; }

        public IntegrateTestBase()
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.RegisterAutofac(config, RegisterServices);
            WebApiConfig.Register(config);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            HttpServer server = new HttpServer(config);
            _messageInvoker = new HttpMessageInvoker(server);

        }

        private void RegisterServices(ContainerBuilder builder)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo("..\\..\\..\\WxPlatformAuthorize.WebAPI\\log4net.config"));
            builder.RegisterInstance(log4net.LogManager.GetLogger("WxPlatformAuthorize")).As<log4net.ILog>().SingleInstance();
            builder.RegisterInstance(new WxSDK.WxApiClientConfig()
            {
                ComponentAppId = ConfigurationManager.AppSettings["WxSDK.ComponentAppId"],
                ComponentAppSecret = ConfigurationManager.AppSettings["WxSDK.ComponentAppSecret"]
            });
            builder.RegisterInstance(this).As<WxSDK.IHttpClient>();
            builder.RegisterType<WxSDK.WxApiClient>();
            builder.RegisterType<AuthorizeService>().SingleInstance();
        }

        protected HttpResponseMessage SendInMemoryHttpRequest(HttpRequestMessage request)
        {
            return _messageInvoker.SendAsync(request, new CancellationTokenSource().Token).Result;
        }

        public string Post(string url, string body)
        {
            if(OnMockWxServerRequest == null)
            {
                throw new NotImplementedException($"{nameof(OnMockWxServerRequest)} is not set");
            }
            return OnMockWxServerRequest(url, body);
        }
    }
}
