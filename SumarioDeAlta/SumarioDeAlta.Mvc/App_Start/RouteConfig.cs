using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SumarioDeAlta.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Funciona grid",
            //    url:"DadosGerais/",
            //    defaults:new {controller ="Paciente",action ="DadosGerais"}
            //    );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Paciente", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}