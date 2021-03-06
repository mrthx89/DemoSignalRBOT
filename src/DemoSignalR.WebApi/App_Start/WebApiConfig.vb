Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http
Imports Newtonsoft.Json

Public Module WebApiConfig
    Public Sub Register(ByVal config As HttpConfiguration)
        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = New Newtonsoft.Json.JsonSerializerSettings With {
            .DateFormatHandling = DateFormatHandling.IsoDateFormat,
            .DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
            .DateFormatString = "yyyy-MM-dd HH:mm:ss"}

        ' Web API configuration and services
        config.Formatters.Remove(config.Formatters.XmlFormatter)

        ' Web API routes
        config.MapHttpAttributeRoutes()

        config.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{action}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        )
    End Sub
End Module
