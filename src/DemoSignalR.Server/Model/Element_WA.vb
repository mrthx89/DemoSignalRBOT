Namespace Model
    Public Class Element_WA
        Public Property ELEMENT_PROFILE_2 As String
        Public Property ELEMENT_PROFILE_3 As String
        Public Property ELEMENT_PROFILE_4 As String
        Public Property ELEMENT_PROFILE_5 As String
        Public Property ELEMENT_PROFILE_6 As String
        Public Property ELEMENT_PROFILE_7 As String
        Public Property ELEMENT_PROFILE_8 As String
        Public Property ELEMENT_PROFILE_9 As String
        Public Property ELEMENT_PROFILE_10 As String
        Public Property ELEMENT_PROFILE_11 As String
        Public Property ELEMENT_PROFILE_12 As String
        Public Property ELEMENT_PROFILE_13 As String
    End Class

    Public Class [Result]
        Public Sub New(ByVal Result As Boolean,
                       ByVal Message As String,
                       ByVal Value As Object)
            Me.Result = Result
            Me.Message = Message
            Me.Value = Value
        End Sub
        Public Property [Result] As Boolean
        Public Property [Message] As String
        Public Property [Value] As Object
    End Class
End Namespace