Public Class frmQRCode
    Private Hasil As Model.Result
    Private WA As Repository.RepWA

    Public Sub New(Hasil As Model.Result,
                   WA As Repository.RepWA)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Hasil = Hasil
        Me.WA = WA
    End Sub

    Private Sub frmQRCode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowBarcode(Hasil)
        tmrCheckQR.Interval = 3000
        tmrCheckQR.Enabled = True
        Me.Focus()
    End Sub

    Private Sub tmrCheckQR_Tick(sender As Object, e As EventArgs) Handles tmrCheckQR.Tick
        tmrCheckQR.Enabled = False
        Cursor = Cursors.WaitCursor
        Try
            Hasil = WA.GetQRCode()
            If Hasil.Result Then
                If Hasil.Message.Equals("Whatsapp QRCode Ready") Then
                    ShowBarcode(Hasil)
                Else
                    'Sudah Login
                    DialogResult = DialogResult.OK
                    Me.Close()
                End If
            Else
                'Sudah Login
                DialogResult = DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception

        End Try
        Cursor = Cursors.Default
        tmrCheckQR.Enabled = True
    End Sub

    Private Sub ShowBarcode(ByVal Hasil As Model.Result)
        Dim qrcode As New QRCoder.QRCodeGenerator
        Dim data As QRCoder.QRCodeData
        Try
            data = qrcode.CreateQrCode(Hasil.Value, QRCoder.QRCodeGenerator.ECCLevel.Q)
            PictureBox1.Image = New QRCoder.QRCode(data).GetGraphic(6)
        Catch ex As Exception

        Finally
            qrcode = Nothing
        End Try
    End Sub
End Class