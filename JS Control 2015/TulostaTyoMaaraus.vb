Imports Microsoft.VisualBasic.PowerPacks

Public Class TulostaTyoMaaraus

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        tulosta()

    End Sub

    Public Sub tulosta()
        Dim pr As New Microsoft.VisualBasic.PowerPacks.Printing.PrintForm
        pr.PrinterSettings.DefaultPageSettings.Margins.Left = 0.25
        pr.PrinterSettings.DefaultPageSettings.Margins.Right = 0.25
        pr.PrinterSettings.DefaultPageSettings.Margins.Top = 0.25
        pr.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0.25
        pr.Form = Me
        pr.PrintAction = Drawing.Printing.PrintAction.PrintToPrinter

        Me.Focus() : pr.Print()
        Timer1.Enabled = False

        If Naytto2014.Visible = True Then Main.txtKeskitys.Focus()


        Me.Dispose()
        TeeTyomaaraus.Close()

    End Sub

End Class