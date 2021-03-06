Imports System.Configuration

Namespace Data
    Module Constant
        Public NamaApplikasi As String = Application.ProductName.ToString
        'Public UserLogin As New Model.User With {.NoID = -1, .Kode = "", .Nama = "", .Pwd = "", .Supervisor = False}
        Public ElementWA As New Model.Element_WA

        Public Property URI_SignalR As String
            Get
                Return My.Settings.URI_SignalR
            End Get
            Set(value As String)

            End Set
        End Property

        Public Property NamaPerusahaan As String
            Get
                Return My.Settings.NamaPerusahaan
            End Get
            Set(value As String)

            End Set
        End Property
    End Module
End Namespace
