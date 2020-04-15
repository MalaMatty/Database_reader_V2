using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Database_reader_V2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Report", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Pdf",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Report", action = "Pdf_view", id = UrlParameter.Optional }
       );
        }
    }
}
