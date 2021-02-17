Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Strings
Imports System.IO
Imports System.Xml.Serialization
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Net.Cache

Namespace Repository
    Public Class Utils
        Public Shared Function DecryptText(ByVal strText As String, ByVal strPwd As String) As String
            Dim i As Integer, c As Integer
            Dim strBuff As String = Nothing

#If Not CASE_SENSITIVE_PASSWORD Then

            'Convert password to upper case
            'if not case-sensitive
            strPwd = UCase$(strPwd)

#End If

            'Decrypt string
            If CBool(Len(strPwd)) Then
                For i = 1 To Len(strText)
                    c = Asc(Mid$(strText, i, 1))
                    c = c - Asc(Mid$(strPwd, (i Mod Len(strPwd)) + 1, 1))
                    strBuff = strBuff & Chr(c And &HFF)
                Next i
            Else
                strBuff = strText
            End If
            Return strBuff
        End Function
        Public Shared Function EncryptText(ByVal strText As String, ByVal strPwd As String) As String
            Dim i As Integer, c As Integer
            Dim strBuff As String = Nothing

#If Not CASE_SENSITIVE_PASSWORD Then

            'Convert password to upper case
            'if not case-sensitive
            strPwd = UCase$(strPwd)

#End If

            'Encrypt string
            If CBool(Len(strPwd)) Then
                For i = 1 To Len(strText)
                    c = Asc(Mid$(strText, i, 1))
                    c = c + Asc(Mid$(strPwd, (i Mod Len(strPwd)) + 1, 1))
                    strBuff = strBuff & Chr(c And &HFF)
                Next i
            Else
                strBuff = strText
            End If
            Return strBuff
        End Function
        Public Shared Function FixApostropi(ByVal obj As Object) As String
            Dim x As String = ""
            Try
                x = obj.ToString.Replace("'", "''")
            Catch ex As Exception
                x = ""
            End Try
            Return x
        End Function
        Public Shared Function FixKoma(ByVal obj As Object) As String
            Dim x As String = ""
            Try
                x = obj.ToString.Replace(",", ".")
            Catch ex As Exception
                x = ""
            End Try
            Return x
        End Function
        'Public Shared Function GetModulSerial() As Boolean
        '    Try
        '        Return NullToBool(System.Configuration.ConfigurationManager.AppSettings("ModulSerial").ToString)
        '    Catch ex As Exception
        '        Return False
        '    End Try
        'End Function
        'Public Shared Function GetStrKon() As String
        '    Dim sConn As String = ""
        '    sConn = System.Configuration.ConfigurationManager.ConnectionStrings("My_ConnectionString").ConnectionString
        '    Return sConn
        'End Function
        Public Shared Function NullToBool(ByVal Value As Object) As Boolean
            If IsDBNull(Value) Then
                Return False
            ElseIf Value Is Nothing Then
                Return False
            ElseIf Value.ToString = "" Then
                Return False
            Else
                Return CBool(Value)
            End If
        End Function
        Public Shared Function NullTostr(ByVal X As Object) As String
            If TypeOf X Is DBNull Then
                Return ""
            Else
                Return CStr(X)
            End If
        End Function
        Public Shared Function NullToLong(ByVal X As Object) As Long
            If TypeOf X Is DBNull Or Not IsNumeric(X) Then
                Return 0
            Else
                Return CLng(X)
            End If
        End Function
        Public Shared Function NullToDbl(ByVal X As Object) As Double
            If TypeOf X Is DBNull Or Not IsNumeric(X) Then
                Return 0
            Else
                Return CDbl(X)
            End If
        End Function
        Public Shared Function NullToDate(ByVal X As Object) As Date
            If TypeOf X Is Date Then
                Return X
            Else
                Return CDate("1/1/1900")
            End If
        End Function
        Public Shared Function Bulatkan(ByVal x As Double, ByVal Koma As Integer) As Double
            If Koma >= 0 Then
                Bulatkan = System.Math.Round(x, CInt(Koma))
                If System.Math.Round(x - Bulatkan, CInt(Koma + 5)) >= 0.5 / (10 ^ Koma) Then Bulatkan = Bulatkan + 1 / (10 ^ Koma)
            Else
                Bulatkan = x
            End If
        End Function
        Public Shared Function NullToDblA(ByVal Value As Object) As Double
            If IsDBNull(Value) Then
                Return 0.0
            Else
                If IsNumeric(Value) Then
                    Return Value
                Else
                    Return 0.0
                End If

            End If
        End Function
        Public Shared Function fnRomawi(ByVal input As Integer) As String
            Dim numeral As String = String.Empty

            If input < 1 OrElse input > 4000 Then
                'Throw an exception if the input is out of range
                Throw New ArgumentOutOfRangeException("input", input.ToString, "Value must be greater than 0 and less than 4,001.")
            Else
                'Load a dictionary that will store the actual value with the roman equivalent
                Dim numeralDic As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)
                With numeralDic
                    .Add(1, "I")
                    .Add(4, "IV")
                    .Add(5, "V")
                    .Add(9, "IX")
                    .Add(10, "X")
                    .Add(40, "XL")
                    .Add(50, "L")
                    .Add(90, "XC")
                    .Add(100, "C")
                    .Add(400, "CD")
                    .Add(500, "D")
                    .Add(900, "CM")
                End With

                'Loop through each input
                For x As Integer = 0 To input.ToString.Length - 1
                    Dim currentValue As Integer = CInt(input.ToString.Substring(x, 1))

                    If x = input.ToString.Length - 1 Then
                        'It can only be 1 - 9
                        If numeralDic.ContainsKey(currentValue) Then
                            'Add the key to the roman numeral value
                            numeral &= numeralDic(currentValue)
                        ElseIf currentValue < 4 Then
                            'Loop from 1 to the current value adding I to the roman numeral
                            'IE: if the current value is 3 then we'd add: I(1), I(2), I(3)
                            For y As Integer = 1 To currentValue
                                numeral &= "I"
                            Next
                        ElseIf currentValue > 5 Then
                            'Add the V to the roman numeral
                            numeral &= "V"

                            'Loop from 6 to the current value adding I to the roman numeral
                            'IE: if the current value is 8 then we'd add: I(6), I(7), I(8)
                            For y As Integer = 6 To currentValue
                                numeral &= "I"
                            Next
                        End If
                    ElseIf x = input.ToString.Length - 2 Then
                        'It can only be 10 - 90
                        'Mulitply the current value by 10 because we are past 1 - 9
                        'The same principal's apply from 1 - 9, only we're in the ten's slot
                        currentValue = currentValue * 10
                        If numeralDic.ContainsKey(currentValue) Then
                            numeral &= numeralDic(currentValue)
                        ElseIf currentValue < 4 Then
                            For y As Integer = 1 To currentValue
                                numeral &= "X"
                            Next
                        ElseIf currentValue > 5 Then
                            numeral &= "L"
                            For y As Integer = 6 To currentValue
                                numeral &= "X"
                            Next
                        End If
                    ElseIf x = input.ToString.Length - 3 Then
                        'It can only be 100 - 900
                        'Mulitply the current value by 100 because we are past 10 - 90
                        'The same principal's apply from 1 - 9, only we're in the hundreds's slot
                        currentValue = currentValue * 100
                        If numeralDic.ContainsKey(currentValue) Then
                            numeral &= numeralDic(currentValue)
                        ElseIf currentValue < 4 Then
                            For y As Integer = 1 To currentValue
                                numeral &= "C"
                            Next
                        ElseIf currentValue > 5 Then
                            numeral &= "D"
                            For y As Integer = 6 To currentValue
                                numeral &= "C"
                            Next
                        End If
                    Else
                        'It can only be 1,000 - 4,000
                        For y As Integer = 1 To currentValue
                            numeral &= "M"
                        Next
                    End If
                Next
            End If
            Return numeral
        End Function
        Public Shared Function DeserializeXMLFileToObject(Of T)(ByVal XmlFilename As Stream) As T
            Dim returnObject As T = Nothing
            If XmlFilename Is Nothing Then Return Nothing

            Try
                Dim xmlStream As StreamReader = New StreamReader(XmlFilename)
                Dim serializer As XmlSerializer = New XmlSerializer(GetType(T))
                returnObject = CType(serializer.Deserialize(xmlStream), T)
            Catch ex As Exception
                'ExceptionLogger.WriteExceptionToConsole(ex, DateTime.Now)
            End Try

            Return returnObject
        End Function
        Public Shared Function ConvertIntToString(ByVal intParameter As Integer) As String
            Return intParameter.ToString()
        End Function
        Public Shared Function SendRequest(ByVal uri As Uri, ByVal jsonDataBytes As Byte(), ByVal contentType As String, ByVal method As String) As String
            Dim response As String = ""
            Dim request As WebRequest
            Try
                request = DirectCast(WebRequest.Create(uri), WebRequest)
                If jsonDataBytes IsNot Nothing Then
                    request.ContentLength = jsonDataBytes.Length
                End If
                ' Define a cache policy for this request only.
                request.CachePolicy = New HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore)
                request.ContentType = contentType
                request.Method = method
                'request.Timeout = 60000

                Select Case request.Method.ToUpper
                    Case "GET"
                        Using responseStream = request.GetResponse.GetResponseStream
                            Using reader As New StreamReader(responseStream)
                                response = reader.ReadToEnd()
                            End Using
                        End Using
                    Case "POST"
                        Using requestStream = request.GetRequestStream
                            requestStream.Write(jsonDataBytes, 0, jsonDataBytes.Length)
                            requestStream.Close()
                            Using responseStream = request.GetResponse.GetResponseStream
                                Using reader As New StreamReader(responseStream)
                                    response = reader.ReadToEnd()
                                End Using
                            End Using
                        End Using
                    Case Else
                        response = ""
                End Select
            Catch ex As WebException
                Dim Message As String = New StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
                Console.WriteLine(Message)
                'DevExpress.XtraEditors.XtraMessageBox.Show(Message)

                response = ""
            Catch ex As Exception
                'DevExpress.XtraEditors.XtraMessageBox.Show("Info Kesalahan : " & ex.Message)
                Console.WriteLine(ex.Message)
                response = ""
            End Try

            Return response
        End Function
        Public Shared Function Base64ToImage(ByVal base64String As String) As Image
            ' Convert Base64 String to byte[]
            Dim imageBytes As Byte() = Convert.FromBase64String(base64String)
            Dim ms As New MemoryStream(imageBytes, 0, imageBytes.Length)

            ' Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length)
            Dim ConvertedBase64Image As Image = Image.FromStream(ms, True)
            Return ConvertedBase64Image
        End Function
        Public Shared Function ImageToBase64(ByVal image As Image, ByVal format As System.Drawing.Imaging.ImageFormat) As String
            Using ms As New MemoryStream()
                ' Convert Image to byte[]
                image.Save(ms, format)
                Dim imageBytes As Byte() = ms.ToArray() ' Convert byte[] to Base64 String
                Dim base64String As String = Convert.ToBase64String(imageBytes)
                Return base64String
            End Using
        End Function
        Public Shared Function getMacAddress()
            Dim nics() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
            Return nics(1).GetPhysicalAddress.ToString
        End Function
    End Class
End Namespace