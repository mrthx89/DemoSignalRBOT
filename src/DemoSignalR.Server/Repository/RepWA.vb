Imports System.Web
Imports DemoSignalR.Server.Data.Constant
Imports OpenQA.Selenium.Chrome
Imports OpenQA.Selenium
Imports System.Threading

Namespace Repository
    Public Class RepWA
        Private Shared Sub NavigateWebURL(ByVal URL As String, Optional browser As String = "default")
            If Not (browser = "default") Then
                Try
                    '// try set browser if there was an error (browser not installed)
                    Process.Start(browser, URL)
                Catch ex As Exception
                    '// use default browser
                    Process.Start(URL)
                End Try
            Else
                '// use default browser
                Process.Start(URL)
            End If
        End Sub

        <STAThread()>
        Public Shared Function SendWA_BOT_Bebas(ByVal Phone As String,
                                                ByVal Message As String,
                                                ByVal Gambar As Image,
                                                ByVal File As String) As String
            If chromestarted AndAlso driver IsNot Nothing Then
                Try
Label_03A0:
                    Dim Pesan As String = Message
                    Dim Nomor As String = Phone.Trim.Replace(" ", "").Replace("-", "").Replace("+", "")
                    Dim URI As String = "https://web.whatsapp.com/send?phone="
                    If Nomor <> "" Then
                        If Nomor.Substring(0, 1) = "0" Then
                            Nomor = "62" & Nomor.Substring(1, Nomor.Length - 1)
                        End If
                        URI &= "phone=" & Nomor

                        pagesource = driver.PageSource
                        If Not pagesource.Contains(ElementWA.ELEMENT_PROFILE_4) Then
                            GoTo Label_03A0
                        End If
                        driver.Navigate.GoToUrl(URI)
                        Thread.Sleep(3000)
Label_01FB:
                        Try
                            If Not driver.PageSource.Contains((ElementWA.ELEMENT_PROFILE_4.ToString & """")) Then
                                GoTo Label_01FB
                            End If

Label_0237:
                            Try
                                Thread.Sleep(3000)
                                Dim element2 As IWebElement = driver.FindElement(By.XPath(ElementWA.ELEMENT_PROFILE_5.ToString))

                                Clipboard.SetText(Pesan)
                                element2.SendKeys((Keys.Control & "v"))
                                Thread.Sleep(3000)

                                If (Gambar Is Nothing AndAlso File Is Nothing) Then
                                    Dim element4 As IWebElement = driver.FindElement(By.CssSelector(ElementWA.ELEMENT_PROFILE_7.ToString))
                                    element4.Click()
                                    element4 = Nothing
                                    Thread.Sleep(4000)
                                    element2 = Nothing
                                    Thread.Sleep(5000)
                                Else
                                    If File IsNot Nothing Then
                                        'To send attachments
                                        'click to add
                                        driver.FindElement(By.CssSelector(ElementWA.ELEMENT_PROFILE_8)).Click()
                                        'add file To send by file path
                                        driver.FindElement(By.CssSelector(ElementWA.ELEMENT_PROFILE_9)).SendKeys(File)
                                        Thread.Sleep(3000)
                                    End If

                                    If Gambar IsNot Nothing Then
                                        Clipboard.SetImage(Gambar)
                                        element2.SendKeys((Keys.Control & "v"))
                                        Thread.Sleep(3000)
                                    End If

                                    Dim element4 As IWebElement = driver.FindElement(By.CssSelector(ElementWA.ELEMENT_PROFILE_7.ToString))
                                    element4.Click()
                                    element4 = Nothing
                                    Thread.Sleep(4000)
                                    element2 = Nothing
                                    Thread.Sleep(5000)
                                End If
                            Catch exception4 As Exception
                                Console.WriteLine(exception4.Message)
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

        Public Shared driver As ChromeDriver
        Public Shared chromestarted As Boolean = False
        Public Shared pagesource As String
        Public Shared [options] As ChromeOptions = Nothing

        Public Shared Function CheckWAOnReady() As Model.Result
            Dim result As New Model.Result(False, "Tidak ditemukan", Nothing)
            Dim URI As String = "https://web.whatsapp.com"
            Dim UlangProses As Boolean = False
            If chromestarted AndAlso driver IsNot Nothing Then
                If chromestarted AndAlso driver IsNot Nothing Then
                    Try
Label_1:
                        pagesource = driver.PageSource
                        If Not pagesource.Contains(ElementWA.ELEMENT_PROFILE_4) Then
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
            End If

            Return result
        End Function

        Public Shared Function GetQRCode() As Model.Result
            Dim result As New Model.Result(False, "Tidak ditemukan", Nothing)
            Dim URI As String = "https://web.whatsapp.com"
            Dim UlangProses As Boolean = False, iUlang As Integer = 0
            If chromestarted AndAlso driver IsNot Nothing Then
                If chromestarted AndAlso driver IsNot Nothing Then
                    Try
Label_1:
                        pagesource = driver.PageSource
                        If iUlang <= 3 AndAlso pagesource.Contains(ElementWA.ELEMENT_PROFILE_11) Then
                            driver.Navigate.GoToUrl(URI)
                            Thread.Sleep(3000)
                            iUlang += 1
                            GoTo Label_1
                        End If

                        If pagesource.Contains(ElementWA.ELEMENT_PROFILE_10) Then
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
                                Thread.Sleep(3000)
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
            End If
            Return result
        End Function

        Public Shared Function ChromeClose() As Model.Result
            Dim Hasil As New Model.Result(False, "", Nothing)
            Dim i As Integer = 0
            Try
                If driver IsNot Nothing Then
                    driver.Close()
                    driver.Dispose()
                End If
                driver = Nothing
                Application.ExitThread()

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
            chromestarted = Not Hasil.Result
            Return Hasil
        End Function

        Public Shared Function ChromeConnect() As Model.Result
            Dim Hasil As New Model.Result(False, "", Nothing)

            Dim argumentsToAdd As String()
            Dim service As ChromeDriverService
            Try
                If driver IsNot Nothing Then
                    driver.Close()
                    driver.Dispose()
                End If
                driver = Nothing

                'System.setProperty("webdriver.chrome.driver", "ChromeDriverPath")
                service = ChromeDriverService.CreateDefaultService()
                service.HideCommandPromptWindow = True

                [options] = New ChromeOptions
                'Hide Windows
                'argumentsToAdd = New String() {
                '    "--window-position=-32000,-32000",
                '    ElementWA.ELEMENT_PROFILE_2.ToString & "=" & Application.StartupPath.Replace("\", "\\") & "\\dreamtech"
                '}
                argumentsToAdd = New String() {
                    ElementWA.ELEMENT_PROFILE_2.ToString & "=" & Application.StartupPath.Replace("\", "\\") & "\\dreamtech"
                }
                'Visible Windows
                'argumentsToAdd = New String() {
                '    ElementWA.ELEMENT_PROFILE_2.ToString & "=" & Application.StartupPath.Replace("\", "\\") & "\\dreamtech"
                '}
                [options].AddArguments(argumentsToAdd)
                driver = New ChromeDriver(service, options)
                driver.Navigate.GoToUrl("https://web.whatsapp.com")
                Thread.Sleep(2000)
                pagesource = driver.PageSource
                With Hasil
                    .Result = True
                    .Message = "Chrome is Started."
                    .Value = pagesource
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
            chromestarted = Hasil.Result
            Return Hasil
        End Function
    End Class
End Namespace
