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

            DialogResult = DialogResult.Cancel
            Me.Close()
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
        If Not IsNothing(signalR) Then
            signalR.Dispose()
        End If
    End Sub

    Private Sub frmServers_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadElementWA()
    End Sub

    Private Sub LoadElementWA()
        Dim Response As String = ""
        Dim uri As Uri = Nothing
        Try
            'Buka Log sebelumnya
            If Not System.IO.Directory.Exists(Application.StartupPath & "\Log\") Then
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\Log\")
            End If

            Dim FileName As String = Application.StartupPath & "\Log\Server_" & Now.ToString("yyyyMMdd") & ".txt"
            If System.IO.File.Exists(FileName) Then
                Using myStream As New System.IO.StreamReader(FileName)
                    While Not myStream.Read
                        lstConsole.Items.Add(myStream.ReadLine)
                    End While
                End Using
            End If

            'Server CTrlSoft
            uri = New Uri("http://ctrlsoft.id/wa_automation/element_wa.json")

            'Server VPoint
            uri = New Uri("http://vpoint.id/MyVPoint/Element_WA.json")

            Response = Repository.Utils.SendRequest(uri, Nothing, "application/json", "GET")
            Console.WriteLine(Response)
            If Response IsNot Nothing AndAlso Response <> "" Then
                ElementWA = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Model.Element_WA)(Response.Replace(vbLf & vbTab, ""))
            Else
                ElementWA = New Model.Element_WA With {.ELEMENT_PROFILE_2 = "user-data-dir",
                                                       .ELEMENT_PROFILE_3 = "_35EW6",
                                                       .ELEMENT_PROFILE_4 = "copyable-text selectable-text",
                                                       .ELEMENT_PROFILE_5 = "//*[@id='main']/footer/div[1]/div[2]/div/div[2]",
                                                       .ELEMENT_PROFILE_6 = "span[data-icon='send-light']",
                                                       .ELEMENT_PROFILE_7 = "span[data-icon='send']",
                                                       .ELEMENT_PROFILE_8 = "span[data-icon='clip']",
                                                       .ELEMENT_PROFILE_9 = "input[type='file']",
                                                       .ELEMENT_PROFILE_10 = "_1yHR2",
                                                       .ELEMENT_PROFILE_11 = "_1yHR2 UlvkP",
                                                       .ELEMENT_PROFILE_12 = "data-ref",
                                                       .ELEMENT_PROFILE_13 = "//div[@class='_3ipVb']//div[@role='button']"}
            End If
        Catch ex As Exception
            ElementWA = New Model.Element_WA With {.ELEMENT_PROFILE_2 = "user-data-dir",
                                                   .ELEMENT_PROFILE_3 = "_35EW6",
                                                   .ELEMENT_PROFILE_4 = "copyable-text selectable-text",
                                                   .ELEMENT_PROFILE_5 = "//*[@id='main']/footer/div[1]/div[2]/div/div[2]",
                                                   .ELEMENT_PROFILE_6 = "span[data-icon='send-light']",
                                                   .ELEMENT_PROFILE_7 = "span[data-icon='send']",
                                                   .ELEMENT_PROFILE_8 = "span[data-icon='clip']",
                                                   .ELEMENT_PROFILE_9 = "input[type='file']",
                                                   .ELEMENT_PROFILE_10 = "_1yHR2",
                                                   .ELEMENT_PROFILE_11 = "_1yHR2 UlvkP",
                                                   .ELEMENT_PROFILE_12 = "data-ref",
                                                   .ELEMENT_PROFILE_13 = "//div[@class='_3ipVb']//div[@role='button']"}
        End Try
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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub
End Class