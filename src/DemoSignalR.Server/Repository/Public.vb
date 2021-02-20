Imports System.Globalization
Imports System.Threading

Public Module [Public]
    Public MainForm As New frmServer
    'Public BOT1Form, BOT2Form, BOT3Form As frmClient

    <STAThread()>
    Public Sub Main()
        Dim localformat As CultureInfo = Nothing
        Try
            'Repository.RepUtils.BuildKoneksi()

            'Change app font
            'Dim fontSize = 11.0F
            'DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = New Font("Segoe UI", fontSize)
            'DevExpress.XtraEditors.WindowsFormsSettings.DefaultMenuFont = New Font("Segoe UI", fontSize)

            'WindowsFormsSettings.LoadApplicationSettings()
            Application.EnableVisualStyles()
            'Application.SetCompatibleTextRenderingDefault(False)
            'WindowsFormsSettings.DefaultFont = My.Settings.FontMain
            'WindowsFormsSettings.DefaultMenuFont = My.Settings.FontMain
            'WindowsFormsSettings.DefaultPrintFont = My.Settings.FontMain

            ' Change current culture
            Dim culture As CultureInfo
            culture = CultureInfo.CreateSpecificCulture("en-US")
            Thread.CurrentThread.CurrentCulture = culture
            Thread.CurrentThread.CurrentUICulture = culture

            'SkinManager.EnableFormSkins()
            'UserLookAndFeel.Default.SetSkinStyle(My.Settings.Skins)

            ServerHub.iListMessage = New List(Of DemoSignalR.Model.Messages)
            Application.Run(MainForm)
        Catch ex As Exception
            MsgBox("Error " & ex.Message, MsgBoxStyle.ApplicationModal)
            'MsgError("mdlPublic.Main", mdlPublic.UserAktif, ex)
        End Try
    End Sub
End Module
