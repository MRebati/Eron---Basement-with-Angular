using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Eron.Core.AppEnums;
using Eron.Core.ManagementSettings;
using Newtonsoft.Json.Serialization;

namespace Eron.Presentation.WebApplication.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //check for two different ways for mapping
            if (ApplicationSettings.MapperType == MapperType.AutoMapper)
            {
                MappingConfig.RegisterAutoMapperMappings();
            }
            else
            {
                MappingConfig.RegisterTinyMapperMappings();
            }
        }
    }
}
