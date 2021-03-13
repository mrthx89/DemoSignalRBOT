Imports Microsoft.Owin.Hosting
Imports DemoSignalR.Server.Data.Constant
Imports OpenQA.Selenium.Chrome

Public Class frmServer
    Private Property signalR As IDisposable
    Private BOTS As New List(Of Model.BOT)

    Private Async Sub btnStart_ClickAsync(sender As Object, e As EventArgs) Handles Button1.Click
        WriteToConsole("Starting SignalR server...")

        Button1.Enabled = False
        Await Task.Run(Sub() StartServer())
    End Sub
    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            For Each bot In BOTS
                bot.WA.ChromeClose()
                bot.BOT.Dispose()
                WriteToConsole("BOTS " & bot.ID & " has stopped")
            Next
            If Not IsNothing(signalR) Then
                signalR.Dispose()
                WriteToConsole("Server stopped at " & URI_SignalR)
                Button1.Enabled = True
                Button2.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    '<summary>
    'Starts the server And checks For Error thrown When another server Is already 
    'running. This method Is called asynchronously from Button_Start.
    '</summary>

    Private Sub StartServer()
        Dim chrome_process() As Process
        Try
            chrome_process = Process.GetProcessesByName("chromedriver")
            For Each proc In chrome_process
                proc.Kill()
            Next

            signalR = WebApp.Start(URI_SignalR)

            Button2.Invoke(Sub()
                               Button2.Enabled = True
                           End Sub)

            WriteToConsole("Server started at " & URI_SignalR)
        Catch ex As Exception
            WriteToConsole("Server failed to start. A server is already running on " &
                           URI_SignalR)
            'Re-enable button to let user try to start server again

            'Me.this.Invoke
            Button1.Invoke(Sub()
                               Button1.Enabled = True
                           End Sub)
            Exit Sub
        End Try
    End Sub

    '<summary>
    'This method adds a line to the RichTextBoxConsole control, using Invoke if used
    'from a SignalR hub thread rather than the UI thread.
    '</summary>
    '<param name="message"></param>
    Public Sub WriteToConsole(message As String)
        If lstConsole.InvokeRequired Then
            lstConsole.Invoke(Sub()
                                  WriteToConsole(message)
                              End Sub)
            Exit Sub
        End If

        If Not System.IO.Directory.Exists(Application.StartupPath & "\Log\") Then
            System.IO.Directory.CreateDirectory(Application.StartupPath & "\Log\")
        End If

        Dim FileName As String = Application.StartupPath & "\Log\Server_" & Now.ToString("yyyyMMdd") & ".txt"
        Dim Log As String = "[" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "] : " & message
        Using myStream As New System.IO.StreamWriter(FileName, True)
            myStream.AutoFlush = True
            myStream.WriteLine(Log)
            myStream.Flush()
        End Using
        lstConsole.Items.Add(Log)
    End Sub

    Private Sub frmServer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not Button1.Enabled Then
            e.Cancel = True
        Else
            If Not IsNothing(signalR) Then
                signalR.Dispose()
            End If
        End If
    End Sub

    Private Sub frmServers_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim FileName As String = ""
        'Buka Log sebelumnya
        If Not System.IO.Directory.Exists(Application.StartupPath & "\Log\") Then
            System.IO.Directory.CreateDirectory(Application.StartupPath & "\Log\")
        End If

        FileName = Application.StartupPath & "\Log\Server_" & Now.ToString("yyyyMMdd") & ".txt"
        If System.IO.File.Exists(FileName) Then
            Using myStream As New System.IO.StreamReader(FileName)
                Dim Str As String = myStream.ReadToEnd().TrimEnd
                Dim data() As String = Str.Split(vbCrLf)
                lstConsole.Items.AddRange(data)
            End Using
        End If
    End Sub

    Private IDBOT As Integer = 0
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button3.Enabled = False
        If Button1.Enabled = False AndAlso Not IsNothing(signalR) Then
            IDBOT += 1
            Dim BOT As New Model.BOT(IDBOT)
            Threading.Thread.Sleep(5000)

            Dim Hasil = BOT.WA.CheckWAOnReady()
            Try
                If Hasil.Result Then
                    If Hasil.Message.Equals("Whatsapp QRCode Ready") Then
                        Using frmQRCode As New frmQRCode(Hasil, BOT.WA)
                            Try
                                If frmQRCode.ShowDialog(Me) = DialogResult.OK Then
                                    BOTS.Add(BOT)

                                    BOT.BOT.Show()
                                    BOT.BOT.Focus()

                                    WriteToConsole("BOT " & IDBOT & " has Started")
                                Else
                                    BOT.WA.ChromeClose()
                                End If
                            Catch ex As Exception
                                BOT.WA.ChromeClose()
                                WriteToConsole("ERR : " & ex.Message)
                            End Try
                        End Using
                    Else
                        BOTS.Add(BOT)

                        BOT.BOT.Show()
                        BOT.BOT.Focus()

                        WriteToConsole("BOT " & IDBOT & " has Started")
                    End If
                Else
                    BOT.WA.ChromeClose()
                    WriteToConsole("BOT " & IDBOT & " cannot Started, " & Hasil.Message)
                End If
            Catch ex As Exception
                BOT.WA.ChromeClose()
                WriteToConsole("BOT " & IDBOT & " cannot Started, " & Hasil.Message)
            End Try
        Else
            MsgBox("Service SignalR harus jalan dahulu.", MsgBoxStyle.Information)
        End If
        Button3.Enabled = True
    End Sub
End Class