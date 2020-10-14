Imports Microsoft.AspNet.SignalR
Imports Microsoft.Owin.Cors
Imports Owin

Public Class Startup
    Public Sub Configuration(app As IAppBuilder)
        Dim hubConfiguration = New HubConfiguration()
        hubConfiguration.EnableDetailedErrors = True
        app.UseCors(CorsOptions.AllowAll)
        app.MapSignalR(hubConfiguration)
    End Sub
End Class
