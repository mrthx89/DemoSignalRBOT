Imports System.Net
Imports System.Security.Claims
Imports System.Web.Http

Public Class ValuesController
    Inherits ApiController

    ' Post api/Values/SendWA
    <HttpPost, Route("SendWA")>
    Public Function SendWA(<FromBody()> ByVal value As DemoSignalR.Model.Messages) As Model.JSONResult
        Dim identity = CType(User.Identity, ClaimsIdentity)
        If Not (identity Is Nothing OrElse IsDBNull(identity)) AndAlso ModelState.IsValid Then
            Dim rep As New Repository.RepositoryWA
            Dim hasil = rep.SendWA(value)
            rep.CloseConnection()
            Return hasil
        ElseIf Not ModelState.IsValid Then
            Return New Model.JSONResult With {.JSONMessage = "Model Invalid!", .JSONRows = 0, .JSONResult = False, .JSONValue = Nothing}
        Else
            Return New Model.JSONResult With {.JSONMessage = "IDentity is null!", .JSONRows = 0, .JSONResult = False, .JSONValue = Nothing}
        End If
    End Function
End Class
