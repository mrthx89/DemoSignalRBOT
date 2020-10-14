Imports Microsoft.VisualBasic
Imports System.ComponentModel

Namespace Model
    Public Class JSONResult
        Private JSONMessage_ As String
        Private JSONResult_ As Boolean
        Private JSONRows_ As Long
        Private JSONValue_ As Object

        <DefaultValue("Data tidak ditemukan.")> _
        Public Property JSONMessage() As String
            Get
                Return JSONMessage_
            End Get
            Set(ByVal value As String)
                JSONMessage_ = value
            End Set
        End Property

        <DefaultValue(False)> _
        Public Property JSONResult() As Boolean
            Get
                Return JSONResult_
            End Get
            Set(ByVal value As Boolean)
                JSONResult_ = value
            End Set
        End Property

        <DefaultValue(0)> _
        Public Property JSONRows() As Long
            Get
                Return JSONRows_
            End Get
            Set(ByVal value As Long)
                JSONRows_ = value
            End Set
        End Property

        Public Property JSONValue() As Object
            Get
                Return JSONValue_
            End Get
            Set(ByVal value As Object)
                JSONValue_ = value
            End Set
        End Property
    End Class
End Namespace