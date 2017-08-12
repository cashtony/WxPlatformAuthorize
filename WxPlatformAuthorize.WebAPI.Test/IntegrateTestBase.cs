using System;
using System.Configuration;
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
            WebApiConfig.RegisterAutofac(config, RegisterTypes);
            WebApiConfig.Register(config);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            HttpServer server = new HttpServer(config);
            _messageInvoker = new HttpMessageInvoker(server);

        }

        private void RegisterTypes(ContainerBuilder builder)
        {
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
