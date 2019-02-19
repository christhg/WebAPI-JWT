using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;


namespace WebAPI_JWT_
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            // Web API 路由
            //0.0 OData 設定
            //config.AddODataQueryFilter();//加入ODataQueryFilter方法

            //0.1 Attribute routing.屬性路由
            config.MapHttpAttributeRoutes();//Api直接設定使用屬性路由[Route("")],調用時比較直觀匹配

            //0.2 Convention-based routing.合約路由
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //0.3 Action-based routing.動作路由
            //config.Routes.MapHttpRoute(
            //    name: "ActionApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional }
            //);

            //0.4 改成API預設傳回 JSON 格式.
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}
