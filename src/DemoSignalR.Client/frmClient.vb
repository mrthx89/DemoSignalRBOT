Imports Microsoft.AspNet.SignalR.Client
Imports DemoSignalR.Model
Imports DemoSignalR.Client.Data.Constant

Public Class frmClient
    Private hubConnection As HubConnection
    Private hubProxy As IHubProxy

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        btnRetrive.PerformClick()
    End Sub

    '<summary>
    'Creates And connects the hub connection And hub proxy.
    '</summary>
    Private Async Sub ConnectAsync()
        Try
            Await hubConnection.Start
            txtClientID.Text = hubConnection.ConnectionId
            Button1.Enabled = True
        Catch ex As Exception
            MessageBox.Show("Unable to connect to server: Start server before connecting clients.")
        End Try
    End Sub

    'method untuk menampilkan data customer ke listview
    Private Sub FillToListView(msg As Messages, Optional ByVal Pesan As String = "")
        Try
            If txtLog.InvokeRequired Then
                txtLog.Invoke(Sub()
                                  FillToListView(msg, Pesan)
                              End Sub)
                Exit Sub
            End If

            txtLog.Text &= System.Environment.NewLine &
            ">> " & msg.PhoneNumber & " : " & msg.Message & " >> Status : " & Pesan
        Catch ex As Exception
            txtLog.Text &= System.Environment.NewLine &
            ">> ERR : Try To Send " & msg.PhoneNumber & " : " & msg.Message
        End Try
    End Sub

    Private Sub FrmClient_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            Repository.RepLog.SaveLog("Log_" & Now.ToString("yyyyMMdd") & ".txt", txtLog)

            If (Not IsNothing(hubConnection)) Then
                hubConnection.Stop()
                hubConnection.Dispose()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim messages As New Messages With {
        .Server = New DemoSignalR.Model.Server With {.ServerId = "", .NickName = ""},
        .Client = New DemoSignalR.Model.Client With {.ClientId = txtClientID.Text, .NickName = "BOT_WA"},
        .Message = txtMessage.Text,
        .NickName = "BOT_WA",
        .PhoneNumber = txtPhoneNumber.Text}
        hubProxy.Invoke("ClientChat", messages)
    End Sub

    Private Sub btnRetrive_Click(sender As Object, e As EventArgs) Handles btnRetrive.Click
        RetriveClient()
    End Sub

    Private Sub RetriveClient()
        'InisialisasiListView()

        hubConnection = New HubConnection(URI_SignalR)

        'membuat objek proxy dari class hub yang ada di server
        hubProxy = hubConnection.CreateHubProxy("ServerHub")

        'set mode listen untuk method RefreshData
        'method RefreshData sebelumnya harus didefinisikan di server
        hubProxy.On(Of Messages)("RefreshData", Sub(messages)
                                                    FillToListView(messages)
                                                End Sub)
        ConnectAsync()

        Repository.RepLog.ReadLog("Log_" & Now.ToString("yyyyMMdd") & ".txt", txtLog)
    End Sub
End Class