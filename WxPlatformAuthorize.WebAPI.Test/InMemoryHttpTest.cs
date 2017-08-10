using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using WxPlatformAuthorize.Service;

namespace WxPlatformAuthorize.WebAPI.Test
{
    public class InMemoryHttpTest
    {
        private HttpMessageInvoker _messageInvoker;

        public object GlobalConfiguration { get; private set; }

        public InMemoryHttpTest()
        {
            HttpConfiguration config = new HttpConfiguration();
            ConfigureAutofac(config);
            WebApiConfig.Register(config);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            HttpServer server = new HttpServer(config);
            _messageInvoker = new HttpMessageInvoker(server);

        }

        private void ConfigureAutofac(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            SetupResolveRules(builder);
            builder.RegisterApiControllers(typeof(Controllers.DevController).Assembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private void SetupResolveRules(ContainerBuilder builder)
        {
            builder.RegisterType<WxSDK.ApiClient>().As<WxSDK.IApiClient>();
            builder.RegisterType<AuthorizeService>().SingleInstance();
        }

        protected HttpResponseMessage SendInMemoryHttpRequest(HttpRequestMessage request)
        {
            return _messageInvoker.SendAsync(request, new CancellationTokenSource().Token).Result;
        }
    }
}
