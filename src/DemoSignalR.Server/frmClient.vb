Imports Microsoft.AspNet.SignalR.Client
Imports DemoSignalR.Model
Imports DemoSignalR.Server.Data.Constant
Imports System.Drawing.Imaging

Public Class frmClient
    Private hubConnection As HubConnection
    Private hubProxy As IHubProxy
    Private WA As Repository.RepWA
    Private ID As Integer
    Private IsBusy As Boolean = False

    Public Sub New(ID As Integer,
                   WA As Repository.RepWA)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.ID = ID
        Me.WA = WA
        Me.Text = "BOT WhatsApp [Client " & ID & "]"
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
    Private Sub FillToListView(msg As Messages,
                               Optional ByVal Pesan As String = "")
        Dim Gambar As Image = Nothing
        Me.IsBusy = True
        Try
            If txtLog.InvokeRequired Then
                txtLog.Invoke(Sub()
                                  FillToListView(msg, Pesan)
                              End Sub)
                Exit Sub
            End If

            If Not (msg.Image Is Nothing OrElse msg.Image = "") Then
                Gambar = Repository.Utils.Base64ToImage(msg.Image)
            End If

            Pesan = WA.SendWA_BOT_Bebas(msg.PhoneNumber, msg.Message,
                                        Gambar,
                                        IIf(msg.File Is Nothing OrElse msg.File = "", Nothing, msg.File))
            txtLog.Text &= System.Environment.NewLine &
            ">> " & msg.PhoneNumber & " : " & msg.Message & " >> Status : " & Pesan
        Catch ex As Exception
            txtLog.Text &= System.Environment.NewLine &
            ">> ERR : Try To Send " & msg.PhoneNumber & " : " & msg.Message
        End Try
        Me.IsBusy = False
    End Sub

    Private Sub FrmClient_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Try
            Repository.RepLog.SaveLog("Log_" & Now.ToString("yyyyMMdd") & ".txt", txtLog)

            If (Not IsNothing(hubConnection)) Then
                hubConnection.Stop()
                hubConnection.Dispose()
            End If
        Catch ex As Exception
            txtLog.Text &= System.Environment.NewLine & ">> ERR : " & ex.Message
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Enabled = False
        Button3.Enabled = False
        Dim Gambar As Image = Nothing
        Dim Messages As Messages

        Try
            If (IsDBNull(img.Image) OrElse IsNothing(img.ImageLocation)) Then
                Messages = New Messages With {.Server = New DemoSignalR.Model.Server With {.ServerId = "", .NickName = ""},
                                              .Client = New DemoSignalR.Model.Client With {.ClientId = txtClientID.Text, .NickName = "BOT_WA"},
                                              .Message = txtMessage.Text,
                                              .NickName = "BOT_WA",
                                              .PhoneNumber = txtPhoneNumber.Text,
                                              .Image = "",
                                              .File = TextBox2.Text}
            Else
                Messages = New Messages With {.Server = New DemoSignalR.Model.Server With {.ServerId = "", .NickName = ""},
                                              .Client = New DemoSignalR.Model.Client With {.ClientId = txtClientID.Text, .NickName = "BOT_WA"},
                                              .Message = txtMessage.Text,
                                              .NickName = "BOT_WA",
                                              .PhoneNumber = txtPhoneNumber.Text,
                                              .Image = Repository.Utils.ImageToBase64(img.Image, ImageFormat.Jpeg),
                                              .File = TextBox2.Text}
            End If

            If Not (Messages.Image Is Nothing OrElse Messages.Image = "") Then
                Gambar = Repository.Utils.Base64ToImage(Messages.Image)
            End If

            Dim Pesan = WA.SendWA_BOT_Bebas(Messages.PhoneNumber,
                                Messages.Message,
                                Gambar,
                                IIf(Messages.File Is Nothing OrElse Messages.File = "", Nothing, Messages.File))
            txtLog.Text &= System.Environment.NewLine &
            ">> " & Messages.PhoneNumber & " : " & Messages.Message & " >> Status : " & Pesan
        Catch ex As Exception
            txtLog.Text &= System.Environment.NewLine & ">> ERR : " & ex.Message
        End Try
        Button1.Enabled = True
        Button3.Enabled = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Button1.Enabled = False
        Button3.Enabled = False
        Using file As New OpenFileDialog
            Try
                file.Title = "Open file Document"
                file.Filter = "PDF Files|*.pdf|Word Files|*.doc"
                file.Multiselect = False
                If file.ShowDialog(Me) = DialogResult.OK Then
                    TextBox2.Text = file.FileName
                End If
            Catch ex As Exception
                txtLog.Text &= System.Environment.NewLine & ">> ERR : " & ex.Message
            End Try
        End Using
        Button1.Enabled = True
        Button3.Enabled = True
    End Sub

    Private Sub img_Click(sender As Object, e As EventArgs) Handles img.Click
        Using file As New OpenFileDialog
            Try
                file.Title = "Open image Document"
                file.Filter = "JPG Files|*.jpg"
                file.Multiselect = False
                If file.ShowDialog(Me) = DialogResult.OK Then
                    img.Load(file.FileName)
                    img.Tag = file.FileName
                End If
            Catch ex As Exception
                txtLog.Text &= System.Environment.NewLine & ">> ERR : " & ex.Message
            End Try
        End Using
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button1.Enabled = False
        Button3.Enabled = False
        Dim messages As Messages
        Try
            If (IsDBNull(img.Image) OrElse IsNothing(img.ImageLocation)) Then
                messages = New Messages With {.Server = New DemoSignalR.Model.Server With {.ServerId = "", .NickName = ""},
                                              .Client = New DemoSignalR.Model.Client With {.ClientId = txtClientID.Text, .NickName = "BOT_WA"},
                                              .Message = txtMessage.Text,
                                              .NickName = "BOT_WA",
                                              .PhoneNumber = txtPhoneNumber.Text,
                                              .Image = "",
                                              .File = TextBox2.Text}
            Else
                messages = New Messages With {.Server = New DemoSignalR.Model.Server With {.ServerId = "", .NickName = ""},
                                              .Client = New DemoSignalR.Model.Client With {.ClientId = txtClientID.Text, .NickName = "BOT_WA"},
                                              .Message = txtMessage.Text,
                                              .NickName = "BOT_WA",
                                              .PhoneNumber = txtPhoneNumber.Text,
                                              .Image = Repository.Utils.ImageToBase64(img.Image, ImageFormat.Jpeg),
                                              .File = TextBox2.Text}
            End If

            hubProxy.Invoke("ClientChat", messages)
        Catch ex As Exception
            txtLog.Text &= System.Environment.NewLine & ">> ERR : " & ex.Message
        End Try
        Button1.Enabled = True
        Button3.Enabled = True
    End Sub

    Private Sub frmClient_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            hubConnection = New HubConnection(URI_SignalR)

            'membuat objek proxy dari class hub yang ada di server
            hubProxy = hubConnection.CreateHubProxy("ServerHub")

            'set mode listen untuk method RefreshData
            'method RefreshData sebelumnya harus didefinisikan di server
            hubProxy.On(Of Messages)("RefreshData", Sub(messages)
                                                        FillToListView(messages)
                                                    End Sub)
            hubProxy.On(Of Messages)("IsBusy", Function(messages)
                                                   Return IsBusy
                                               End Function)
            ConnectAsync()

            Repository.RepLog.ReadLog("Log_" & Now.ToString("yyyyMMdd") & ".txt", txtLog)
        Catch ex As Exception
            txtLog.Text &= System.Environment.NewLine & ">> ERR : " & ex.Message
        End Try
    End Sub
End Class