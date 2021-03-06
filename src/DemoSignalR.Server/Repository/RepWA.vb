Imports System.Web
Imports DemoSignalR.Server.Data.Constant
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium
Imports System.Threading

Namespace Repository
    Public Class RepWA
        Public driver As ChromeDriver
        Public ChromeStarted As Boolean = False
        Public PageSource As String
        Public [options] As ChromeOptions = Nothing

        <STAThread()>
        Public Function SendWA_BOT_Bebas(ByVal Phone As String,
                                                ByVal Message As String,
                                                ByVal Gambar As Image,
                                                ByVal File As String) As String
            Dim iTryURI As Integer = 0
            Dim iTryPageSource As Integer = 0
            If ChromeStarted AndAlso driver IsNot Nothing Then
                Try
Label_03A0:
                    Application.DoEvents()

                    Dim Pesan As String = Message
                    Dim Nomor As String = Phone.Trim.Replace(" ", "").Replace("-", "").Replace("+", "")
                    Dim URI As String = "https://web.whatsapp.com/send?"
                    If Nomor <> "" Then
                        If Nomor.Substring(0, 1) = "0" Then
                            Nomor = "62" & Nomor.Substring(1, Nomor.Length - 1)
                        End If
                        URI = "https://web.whatsapp.com/send?phone=" & Nomor

                        PageSource = driver.PageSource
                        If Not PageSource.Contains(ElementWA.ELEMENT_PROFILE_4) Then
                            If iTryURI <= 5 Then
                                Dim Hasil = CheckWAOnReady()
                                If Hasil.Result Then
                                    If Hasil.Message.Equals("Whatsapp QRCode Ready") Then
                                        Using frmQRCode As New frmQRCode(Hasil, Me)
                                            Try
                                                frmQRCode.ShowDialog()
                                            Catch ex As Exception

                                            End Try
                                        End Using
                                    End If
                                End If

                                driver.Navigate.GoToUrl(URI)
                                Thread.Sleep(3000)

                                iTryURI += 1
                                GoTo Label_03A0
                            Else
                                Return "Unable to Send Data : Phone " & Phone & ", Message " & Message & ", " & IIf(IsNothing(Gambar), "", "Dengan Gambar,") & " File " & File
                            End If
                        End If
                        driver.Navigate.GoToUrl(URI)
                        Thread.Sleep(3000)
Label_01FB:
                        Application.DoEvents()
                        Try
                            If Not driver.PageSource.Contains(ElementWA.ELEMENT_PROFILE_4.ToString & """") Then
                                If iTryPageSource <= 10 Then
                                    iTryPageSource += 1
                                    GoTo Label_01FB
                                ElseIf iTryURI <= 5 Then
                                    iTryURI += 1
                                    GoTo Label_03A0
                                Else
                                    Return "Unable to Send Data : Phone " & Phone & ", Message " & Message & ", " & IIf(IsNothing(Gambar), "", "Dengan Gambar,") & " File " & File
                                End If
                            End If
Label_0237:
                            Application.DoEvents()
                            Try
                                Thread.Sleep(3000)
                                Dim element2 As IWebElement = driver.FindElementByXPath(ElementWA.ELEMENT_PROFILE_5)

                                Clipboard.SetText(Pesan)
                                element2.SendKeys(Keys.Control & "v")
                                Thread.Sleep(3000)

                                If (Gambar Is Nothing AndAlso File Is Nothing) Then
                                    element2.SendKeys(Keys.Enter)
                                    'Dim element4 As IWebElement = driver.FindElement(By.CssSelector(ElementWA.ELEMENT_PROFILE_7.ToString))
                                    'element4.Click()
                                    'element4 = Nothing
                                    'Thread.Sleep(4000)
                                    element2 = Nothing
                                    'Thread.Sleep(5000)
                                Else
                                    If Gambar IsNot Nothing Then
                                        Clipboard.SetImage(Gambar)
                                        element2.SendKeys(Keys.Control & "v")
                                        Thread.Sleep(3000)
                                    End If
                                    Dim element4 = driver.FindElementsByCssSelector(ElementWA.ELEMENT_PROFILE_7)
                                    If element4.Count >= 1 Then
                                        element4.Item(0).Click()
                                    Else
                                        element4 = driver.FindElementsByXPath(ElementWA.ELEMENT_PROFILE_13)
                                        If element4.Count >= 1 Then
                                            element4.Item(0).Click()
                                        Else
                                            element2.Click()
                                        End If
                                    End If
                                    Thread.Sleep(4000)

                                    If File IsNot Nothing Then
                                        'To send attachments
                                        'click to add
                                        driver.FindElementByCssSelector(ElementWA.ELEMENT_PROFILE_8).Click()
                                        'add file To send by file path
                                        driver.FindElementByCssSelector(ElementWA.ELEMENT_PROFILE_9).SendKeys(File)
                                        Thread.Sleep(3000)
                                    End If
                                    element4 = driver.FindElementsByCssSelector(ElementWA.ELEMENT_PROFILE_7)
                                    If element4.Count >= 1 Then
                                        element4.Item(0).Click()
                                    Else
                                        element4 = driver.FindElementsByXPath(ElementWA.ELEMENT_PROFILE_13)
                                        If element4.Count >= 1 Then
                                            element4.Item(0).Click()
                                        Else
                                            element2.Click()
                                        End If
                                    End If
                                    Thread.Sleep(4000)
                                    element4 = Nothing
                                End If
                            Catch exception4 As Exception
                                Console.WriteLine(exception4.Message)
                                Return exception4.Message
                            End Try
                        Catch ex As Exception
                            Console.WriteLine(ex.Message)
                            GoTo Label_0237
                        End Try
                    Else
                        Return "Nomor HP tidak Valid"
                    End If
                Catch ex As Exception
                    Return "Error : " & ex.Message
                End Try
            Else
                Return "Chrome Client Offline"
            End If

            Return "OK"
        End Function

        Public Function CheckWAOnReady() As Model.Result
            Dim result As New Model.Result(False, "Tidak ditemukan", Nothing)
            Dim URI As String = "https://web.whatsapp.com"
            Dim UlangProses As Boolean = False
            If ChromeStarted AndAlso driver IsNot Nothing Then
                Try
Label_1:
                    PageSource = driver.PageSource
                    If PageSource.Contains("_1dwBj") Then
                        Dim buttons = driver.FindElementsByClassName("_1dwBj")
                        For Each btn In buttons
                            If btn.Text.Replace(" ", "") = "GUNAKAN DI SINI".Replace(" ", "") OrElse btn.Text.Replace(" ", "") = "COBASEKARANG".Replace(" ", "") Then
                                btn.Click()
                                Thread.Sleep(3000)
                                UlangProses = True
                                GoTo Label_1
                            End If
                        Next
                    ElseIf Not PageSource.Contains(ElementWA.ELEMENT_PROFILE_4) Then
                        If UlangProses Then
                            Return GetQRCode()
                        Else
                            driver.Navigate.GoToUrl(URI)
                            Thread.Sleep(3000)
                            UlangProses = True
                            GoTo Label_1
                        End If
                    Else
                        With result
                            .Result = True
                            .Message = "Whatsapp On Ready"
                            .Value = Nothing
                        End With
                    End If
                Catch ex As Exception
                    With result
                        .Result = False
                        .Message = "Whatsapp not Ready : " & ex.Message
                        .Value = Nothing
                    End With
                End Try
            Else
                With result
                    .Result = False
                    .Message = "Service is not Running."
                    .Value = Nothing
                End With
            End If

            Return result
        End Function

        Public Function GetQRCode() As Model.Result
            Dim result As New Model.Result(False, "Tidak ditemukan", Nothing)
            Dim URI As String = "https://web.whatsapp.com"
            Dim UlangProses As Boolean = False, iUlang As Integer = 0
            If ChromeStarted AndAlso driver IsNot Nothing Then
                Try
Label_1:
                    PageSource = driver.PageSource
                    If iUlang <= 3 AndAlso PageSource.Contains(ElementWA.ELEMENT_PROFILE_11) Then
                        driver.Navigate.GoToUrl(URI)
                        Thread.Sleep(2000)
                        iUlang += 1
                        GoTo Label_1
                    End If

                    If PageSource.Contains(ElementWA.ELEMENT_PROFILE_10) Then
                        Dim Data As String = driver.FindElementByClassName(ElementWA.ELEMENT_PROFILE_10).GetAttribute(ElementWA.ELEMENT_PROFILE_12)
                        With result
                            .Result = True
                            .Message = "Whatsapp QRCode Ready"
                            .Value = Data
                        End With
                    Else
                        If UlangProses Then
                            With result
                                .Result = False
                                .Message = "Whatsapp Not Ready"
                                .Value = Nothing
                            End With
                        Else
                            driver.Navigate.GoToUrl(URI)
                            Thread.Sleep(2000)
                            UlangProses = True
                            GoTo Label_1
                        End If
                    End If
                Catch ex As Exception
                    With result
                        .Result = False
                        .Message = "Whatsapp not Ready : " & ex.Message
                        .Value = Nothing
                    End With
                End Try
            Else
                With result
                    .Result = False
                    .Message = "Service is not Running."
                    .Value = Nothing
                End With
            End If
            Return result
        End Function

        Public Function ChromeClose() As Model.Result
            Dim Hasil As New Model.Result(False, "", Nothing)
            Dim i As Integer = 0
            Try
                If driver IsNot Nothing Then
                    driver.Quit()
                    driver.Close()
                    driver.Dispose()
                End If
                driver = Nothing

                With Hasil
                    .Result = True
                    .Message = "Chrome is Stopped."
                    .Value = ""
                End With
            Catch ex As Exception
                With Hasil
                    .Result = False
                    .Message = "Err : " & ex.Message
                    .Value = Nothing
                End With
            End Try
            ChromeStarted = Not Hasil.Result
            Return Hasil
        End Function

        Public Property ModeSenyap As Boolean

        Public Function ChromeConnect(ByVal IDBOT As Integer) As Model.Result
            Dim Hasil As New Model.Result(False, "", Nothing)

            Dim service As ChromeDriverService
            Dim DEFAULT_USER_AGENT As String = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_3) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.122 Safari/537.36"
            Try
                If driver IsNot Nothing Then
                    driver.Close()
                    driver.Dispose()
                End If
                driver = Nothing

                [options] = New ChromeOptions
                'Hide Windows
                'argumentsToAdd = New String() {
                '    "--window-position=-32000,-32000",
                '    ElementWA.ELEMENT_PROFILE_2.ToString & "=" & Application.StartupPath.Replace("\", "\\") & "\\dreamtech_" & IDBOT
                '}

                options.AddArguments("no-sandbox")
                If MsgBox("Ingin menampilkan Windows Browser atau dalam Mode Senyap?" & vbCrLf & "YES : Mode Senyap, NO : POP UP WINDOW", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                    Me.ModeSenyap = True
                    options.AddArguments("--headless") '//hide browser
                    options.AddArguments("--start-maximized")
                Else
                    Me.ModeSenyap = False
                    options.AddArguments("--window-position=-32000,-32000")
                    options.AddArguments("--start-minimize")
                End If
                'options.AddArguments(ElementWA.ELEMENT_PROFILE_2 & "=" & Application.StartupPath.Replace("\", "\\") & "\\dreamtech_" & IDBOT)
                options.AddArguments("--disable-extensions")
                options.AddArguments("--test-type")
                options.AddArguments("--ignore-certificate-errors")
                options.AddArguments("--disable-dev-shm-usage")
                options.AddArguments("--silent")
                options.AddArguments("--disable-infobars")
                options.AddArguments("--user-agent=" + DEFAULT_USER_AGENT)

                'options.BinaryLocation = "C:\Program Files\Google\Chrome\Application\chrome.exe"

                'If System.IO.File.Exists("C:\Program Files\Google\Chrome\Application\chrome.exe") Then
                '    options.BinaryLocation = "C:\Program Files\Google\Chrome\Application\chrome.exe"
                'Else
                '    If System.IO.File.Exists("C:\Program Files (x86)\Google\Chrome\Application\chrome.exe") Then
                '        options.BinaryLocation = "C:\Program Files (x86)\Google\Chrome\Application\chrome.exe"
                '    Else
                '        Using file As New OpenFileDialog
                '            Try
                '                file.Title = "Open Chrome Application"
                '                file.Filter = "Chrome Application|chrome.exe"
                '                If file.ShowDialog() Then
                '                    RepLog.SaveLog("ChromePath.txt", file.FileName, False)
                '                    options.BinaryLocation = file.FileName
                '                End If
                '            Catch ex As Exception
                '                With Hasil
                '                    .Result = False
                '                    .Message = "Error : " & ex.Message
                '                    .Value = Nothing
                '                End With
                '            End Try
                '        End Using
                '    End If
                'End If

                'Visible Windows
                'argumentsToAdd = New String() {
                '    ElementWA.ELEMENT_PROFILE_2.ToString & "=" & Application.StartupPath.Replace("\", "\\") & "\\dreamtech_" & IDBOT
                '}

                service = ChromeDriverService.CreateDefaultService()
                service.HideCommandPromptWindow = True

                driver = New ChromeDriver(service, options)
                driver.Navigate.GoToUrl("https://web.whatsapp.com")
                Thread.Sleep(2000)
                PageSource = driver.PageSource
                With Hasil
                    .Result = True
                    .Message = "Chrome is Started."
                    .Value = PageSource
                End With
            Catch ex As Exception
                If driver IsNot Nothing Then
                    driver.Close()
                    driver.Dispose()
                End If
                driver = Nothing
                With Hasil
                    .Result = False
                    .Message = "ERR Starting Chrome : " & ex.Message
                    .Value = Nothing
                End With
            End Try
            ChromeStarted = Hasil.Result
            Return Hasil
        End Function
    End Class
End Namespace
