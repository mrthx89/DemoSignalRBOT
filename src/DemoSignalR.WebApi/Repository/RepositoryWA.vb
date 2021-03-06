Imports System.IO
Imports DemoSignalR.Model
Imports Microsoft.AspNet.SignalR.Client

Namespace Repository
    Public Class RepositoryWA
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
        Public Sub CloseConnection()
            Try
                If (Not IsNothing(hubConnection)) Then
                    hubConnection.Stop()
                    hubConnection.Dispose()
                End If
            Catch ex As Exception

            End Try
        End Sub
#End Region

        Public Function SendWA(pesan As DemoSignalR.Model.Messages) As Model.JSONResult
            Dim json As New Model.JSONResult
            Dim FileFoto As String, FileFile As String
            Try
                If pesan.Image IsNot Nothing AndAlso pesan.Image.Length >= 1 Then
                    'Base64
                    Dim gambar = Repository.Utils.Base64ToImage(pesan.Image)
                    If Not gambar Is Nothing Then
                        'Create Thumnails
                        'ImgThumb = ImgFoto.GetThumbnailImage(512, 512, Nothing, New IntPtr())
                        FileFoto = "Foto_" & Now.ToString("yyMMddHHmmss") & ".jpg"
                        Repository.Utils.SaveImage(gambar, HttpContext.Current.Server.MapPath("~/Assets/Foto/"), FileFoto)
                        pesan.Image = HttpContext.Current.Server.MapPath("~/Assets/Foto/") & FileFoto
                    End If
                Else
                    pesan.Image = ""
                End If

                If pesan.File IsNot Nothing AndAlso pesan.File.Length >= 1 Then
                    Dim Data = Convert.FromBase64String(pesan.File)
                    FileFile = "File_" & Now.ToString("yyMMddHHmmss") & ".pdf"
                    Dim fInfo = New System.IO.FileInfo(HttpContext.Current.Server.MapPath("~/Assets/File/" & FileFile))
                    If Not fInfo.Directory.Exists Then
                        fInfo.Directory.Create()
                    End If
                    pesan.File = fInfo.FullName
                    Using fStream As New FileStream(pesan.File, FileMode.Create, FileAccess.Write)
                        fStream.Write(Data, 0, Data.Length) 'File has Create
                    End Using
                Else
                    pesan.File = ""
                End If

                Dim messages As New Messages With {.Server = New DemoSignalR.Model.Server With {.ServerId = "", .NickName = ""},
                                                   .Client = New DemoSignalR.Model.Client With {.ClientId = myid, .NickName = myname},
                                                   .Message = pesan.Message,
                                                   .NickName = myname,
                                                   .Phone = pesan.Phone,
                                                   .Image = pesan.Image,
                                                   .File = pesan.File}
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
            End Try
            Return json
        End Function
    End Class
End Namespace
