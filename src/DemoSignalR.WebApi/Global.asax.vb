Imports System.Web.Http
Imports System.Web.Optimization
Imports Swashbuckle.Application

Public Class WebApiApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        GlobalConfiguration.Configure(AddressOf WebApiConfig.Register)
        GlobalConfiguration.Configuration.
            EnableSwagger(Function(c)
                              c.SingleApiVersion("v1", "DemoSignalR.WebAPI")
                              Return c
                          End Function).
            EnableSwaggerUi(Function(c)
                                c.DocExpansion(DocExpansion.List)
                                Return c
                            End Function)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub
End Class
