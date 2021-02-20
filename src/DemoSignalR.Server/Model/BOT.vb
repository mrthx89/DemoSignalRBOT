Namespace Model
    Public Class BOT
        Public Property ID As Integer
        Public Property BOT As frmClient
        Public Property WA As Repository.RepWA

        Public Sub New(ByVal IDBOT As Integer)
            Me.ID = IDBOT
            Me.WA = New Repository.RepWA
            Me.WA.ChromeConnect(Me.ID)
            Me.BOT = New frmClient(ID, WA)
        End Sub

        Public Sub Dispose()
            Try
                WA.ChromeClose()
            Catch ex As Exception

            End Try
        End Sub
    End Class
End Namespace
