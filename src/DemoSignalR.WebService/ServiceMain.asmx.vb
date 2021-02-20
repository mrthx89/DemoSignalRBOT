Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Microsoft.AspNet.SignalR.Client
Imports DemoSignalR.Model
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class ServiceMain
    Inherits System.Web.Services.WebService

    Private hubConnection As HubConnection
    Private hubProxy As IHubProxy
    Public Shared myid, myname As String

#Region "SignalR"
    Public Sub New()
        Try
            hubConnection = New HubConnection(My.Settings.URI_SignalR)
            hubProxy = hubConnection.CreateHubProxy("ServerHub")
            'hubProxy.On(Of Messages)("RefreshData", Sub(messages)
            '                                            FillToListView(messages)
            '                                        End Sub)
            ConnectAsync()
            hubConnection.Start().Wait()
        Catch ex As Exception

        End Try
    End Sub

    'method untuk menampilkan data customer ke listview
    Private Sub FillToListView(msg As Messages)
        Try
            'Repository.RepWA.SendWA_BOT_Bebas(msg.PhoneNumber, msg.Message, Nothing)

            'txtLog.Text &= System.Environment.NewLine &
            '">> " & msg.PhoneNumber & " : " & msg.Message
        Catch ex As Exception
            'txtLog.Text &= System.Environment.NewLine &
            '">> ERR : Try To Send " & msg.PhoneNumber & " : " & msg.Message
        End Try
    End Sub

    '<summary>
    'Creates And connects the hub connection And hub proxy.
    '</summary>
    Private Sub ConnectAsync()
        Try
            hubConnection.Start.Wait()
            myid = hubConnection.ConnectionId
        Catch ex As Exception
            'MessageBox.Show("Unable to connect to server: Start server before connecting clients.")
            myid = ""
        End Try
    End Sub
    Private Sub CustomerService_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        Try
            If (Not IsNothing(hubConnection)) Then
                hubConnection.Stop()
                hubConnection.Dispose()
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

    <WebMethod(Description:="JSON")>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json, UseHttpGet:=False)>
    Public Sub SendWAAPI(ByVal Phone As String, ByVal Message As String)
        Dim json As New Model.JSONResult With {.JSONMessage = "Data Tidak ditemukan.", .JSONResult = False, .JSONRows = 0, .JSONValue = Nothing}
        Try
            Dim messages As New Messages With {
                                    .Server = New DemoSignalR.Model.Server With {.ServerId = "", .NickName = ""},
                                    .Client = New DemoSignalR.Model.Client With {.ClientId = myid, .NickName = myname},
                                    .Message = Message,
                                    .NickName = myname,
                                    .PhoneNumber = Phone,
                                    .Image = "",
                                    .File = ""}
            hubProxy.Invoke("ClientChat", messages)

            With json
                .JSONMessage = "Sukses"
                .JSONValue = Nothing
                .JSONResult = True
                .JSONRows = 0
            End With
        Catch ex As Exception
            With json
                .JSONMessage = "Error : " & ex.Message
                .JSONValue = Nothing
                .JSONResult = False
                .JSONRows = 0
            End With
        Finally
            Dim Hasil = Newtonsoft.Json.JsonConvert.SerializeObject(json)

            Context.Response.Clear()
            Context.Response.ContentType = "application/json"
            Context.Response.AddHeader("content-length", Hasil.Length.ToString())
            Context.Response.Flush()
            Context.Response.Write(Hasil)
        End Try
    End Sub
End Class