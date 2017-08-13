using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using WxPlatformAuthorize.WebAPI.Controllers;
using WxPlatformAuthorize.WebAPI.Filters;

namespace WxPlatformAuthorize.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static void RegisterAutofac(HttpConfiguration config, Action<ContainerBuilder> registerServices)
        {
            var builder = new ContainerBuilder();

            //Register services
            registerServices(builder);

            //Register filters
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterType<WebApiExceptionFilterAttribute>().AsWebApiExceptionFilterFor<AuthorizeController>();
            builder.RegisterType<WebApiExceptionFilterAttribute>().AsWebApiExceptionFilterFor<NotifyController>();
            builder.RegisterType<WebApiExceptionFilterAttribute>().AsWebApiExceptionFilterFor<DebugController>();

            //Register controllers
            builder.RegisterApiControllers(typeof(Controllers.DebugController).Assembly);

            //Set resolver
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
