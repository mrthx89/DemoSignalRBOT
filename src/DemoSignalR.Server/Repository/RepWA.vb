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

        Public Shared Function ChromeClose() As Boolean
            Dim flag As Boolean = False, i As Integer = 0
            Try
                'If System.IO.Directory.Exists(Application.StartupPath & "\Cookies\") Then
                '    System.IO.Directory.CreateDirectory(Application.StartupPath & "\Cookies\")
                'End If
                'For Each file In System.IO.Directory.GetFiles(Application.StartupPath & "\Cookies\")
                '    System.IO.File.Delete(file)
                'Next

                'For Each cookies In driver.Manage.Cookies.AllCookies
                '    i += 1
                '    System.IO.File.Create(Application.StartupPath & "\Cookies\" & i & ".txt")
                '    System.IO.File.AppendAllText(Application.StartupPath & "\Cookies\" & i & ".txt", cookies.ToString)
                'Next
                driver.Close()
                driver.Dispose()
                driver = Nothing
                Application.ExitThread()

                flag = True
            Catch ex As Exception
                flag = False
            End Try
            chromestarted = Not flag
            Return flag
        End Function

        Public Shared Function ChromeConnect() As Boolean
            Dim flag As Boolean = False
            Dim argumentsToAdd As String()
            Try
                'System.setProperty("webdriver.chrome.driver", "ChromeDriverPath")
                Dim service As ChromeDriverService = ChromeDriverService.CreateDefaultService
                service.HideCommandPromptWindow = True
                options = New ChromeOptions
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
                options.AddArguments(argumentsToAdd)
                driver = New ChromeDriver(service, options)
                driver.Navigate.GoToUrl("https://web.whatsapp.com")
                pagesource = driver.PageSource
                flag = True
            Catch obj1 As Exception
                flag = False
            End Try
            chromestarted = flag
            Return flag
        End Function
    End Class
End Namespace
