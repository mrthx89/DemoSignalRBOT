Imports System.Globalization
Imports System.Threading

Public Module [Public]
    Public MainForm As New frmServer
    <STAThread()>
    Public Sub Main()
        Dim localformat As CultureInfo = Nothing
        Try
            Application.EnableVisualStyles()
            ' Change current culture
            Dim culture As CultureInfo
            culture = CultureInfo.CreateSpecificCulture("en-US")
            Thread.CurrentThread.CurrentCulture = culture
            Thread.CurrentThread.CurrentUICulture = culture

            ServerHub.iListMessage = New List(Of DemoSignalR.Model.Messages)
            Application.Run(MainForm)
        Catch ex As Exception
            MsgBox("Error " & ex.Message, MsgBoxStyle.ApplicationModal)
        End Try
    End Sub
End Module
