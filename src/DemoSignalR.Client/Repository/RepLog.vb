Namespace Repository
    Public Class RepLog
        Public Shared Function SaveLog(ByVal fileName As String, ByVal MemoEdit As TextBox) As Boolean
            Try
                If Not System.IO.Directory.Exists(Application.StartupPath & "\Log\") Then
                    System.IO.Directory.CreateDirectory(Application.StartupPath & "\Log\")
                End If

                If Not System.IO.File.Exists(Application.StartupPath & "\Log\" & fileName) Then
                    System.IO.File.Create(Application.StartupPath & "\Log\" & fileName)
                End If

                Using myStream As New System.IO.StreamWriter(Application.StartupPath & "\Log\" & fileName)
                    myStream.AutoFlush = True
                    myStream.WriteLine(MemoEdit.Text)
                    myStream.Flush()
                End Using

                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function ReadLog(ByVal fileName As String, ByVal MemoEdit As TextBox) As Boolean
            Try
                If Not System.IO.Directory.Exists(Application.StartupPath & "\Log\") Then
                    System.IO.Directory.CreateDirectory(Application.StartupPath & "\Log\")
                End If
                If System.IO.File.Exists(Application.StartupPath & "\Log\" & fileName) Then
                    Using myStream As New System.IO.StreamReader(Application.StartupPath & "\Log\" & fileName)
                        MemoEdit.Text = myStream.ReadToEnd()
                    End Using
                Else
                    MemoEdit.Text = ""
                End If

                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
    End Class
End Namespace
