using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ThinkBridge.Models;

namespace ThinkBridge
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
            //Odata Configuration
            //ODataModelBuilder is a class which is used to create the EDM or the Entity Data Model by using the default naming conventions
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            //configuring the entity for which we want to use.
            builder.EntitySet<Product>("Products");
            //select: which properties to include in a response.Expand: expand the related entities in line.Filter : filer result based on condition. Orderby: used to sort results
            config.Select().Expand().Filter().OrderBy().Count();
            //define map odata service route, which is an extension method which is used to tell web api, how to route the http request to the end point
            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel()
                );
        }
    }
}
