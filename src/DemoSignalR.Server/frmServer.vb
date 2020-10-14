Imports Microsoft.Owin.Hosting
Imports DemoSignalR.Server.Data.Constant
Imports OpenQA.Selenium.Chrome
Imports DemoSignalR.Server.Repository.RepWA

Public Class frmServer
    Private Property signalR As IDisposable

    Private Async Sub btnStart_ClickAsync(sender As Object, e As EventArgs) Handles Button1.Click
        If Not chromestarted Then WriteToConsole(False, "Unconnected API WhatsApp, Lakukan Login WA Web terlebih Dahulu.") : Exit Sub
        WriteToConsole(False, "Starting server...")

        Button1.Enabled = False
        Await Task.Run(Sub() StartServer())
    End Sub
    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            [Public].ClientForm.Close()
            [Public].ClientForm.Dispose()
        Catch ex As Exception

        End Try
        Repository.RepWA.ChromeClose()
        DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
    '<summary>
    'Starts the server And checks For Error thrown When another server Is already 
    'running. This method Is called asynchronously from Button_Start.
    '</summary>
    Private Sub StartServer()
        Try
            signalR = WebApp.Start(URI_SignalR)
        Catch ex As Exception
            WriteToConsole(False, "Server failed to start. A server is already running on " &
                           URI_SignalR)
            'Re-enable button to let user try to start server again

            'Me.this.Invoke
            Button1.Invoke(Sub()
                               Button1.Enabled = True
                           End Sub)
            Exit Sub
        End Try

        Button2.Invoke(Sub()
                           Button2.Enabled = True
                       End Sub)

        WriteToConsole(True, "Server started at " &
                       URI_SignalR)

    End Sub

    '<summary>
    'This method adds a line to the RichTextBoxConsole control, using Invoke if used
    'from a SignalR hub thread rather than the UI thread.
    '</summary>
    '<param name="message"></param>
    Public Sub WriteToConsole(ByVal OnLoad As Boolean, message As String)
        If lstConsole.InvokeRequired Then
            lstConsole.Invoke(Sub()
                                  WriteToConsole(OnLoad, message)
                              End Sub)
            Exit Sub
        End If

        If OnLoad AndAlso Not IsNothing(signalR) Then
            Try
                [Public].ClientForm.Close()
                [Public].ClientForm.Dispose()
            Catch exx As Exception

            End Try
            [Public].ClientForm = New frmClient
            [Public].ClientForm.Show()
            [Public].ClientForm.Focus()
        End If

        lstConsole.Items.Add(message)
    End Sub
    Private Sub frmServer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If Not IsNothing(signalR) Then
            signalR.Dispose()
        End If
        Repository.RepWA.ChromeClose()
    End Sub

    Private Sub frmServers_Load(sender As Object, e As EventArgs) Handles Me.Load
        LoadElementWA()
        Repository.RepWA.ChromeConnect()
    End Sub

    Private Sub LoadElementWA()
        Try
            Dim uri As New Uri("http://vpoint.id/MyVPoint/Element_WA.json")
            Dim Response As String = Repository.Utils.SendRequest(uri, Nothing, "application/json", "GET")
            Console.WriteLine(Response)
            If Response IsNot Nothing AndAlso Response <> "" Then
                ElementWA = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Model.Element_WA)(Response)
            Else
                ElementWA = New Model.Element_WA With {.ELEMENT_PROFILE_2 = "user-data-dir",
                                                       .ELEMENT_PROFILE_3 = "_35EW6",
                                                       .ELEMENT_PROFILE_4 = "copyable-text selectable-text",
                                                       .ELEMENT_PROFILE_5 = "//*[@id='main']/footer/div[1]/div[2]/div/div[2]",
                                                       .ELEMENT_PROFILE_6 = "span[data-icon='send-light']",
                                                       .ELEMENT_PROFILE_7 = "span[data-icon='send']"}
            End If
        Catch ex As Exception
            'lbCredit.Text = "Credit Thanks To :" & vbCrLf &
            '                "Load Error " & ex.Message
        Finally
            'lbCredit.AllowHtmlString = True
            'lbCredit.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap
            'lbCredit.Appearance.Options.UseTextOptions = True
            'lbCredit.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical
        End Try
    End Sub
End Class