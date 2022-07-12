Imports Microsoft.VisualBasic.PowerPacks

Public Class TulostaTyoVuoroLista

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim pr As New Microsoft.VisualBasic.PowerPacks.Printing.PrintForm
        pr.PrinterSettings.DefaultPageSettings.Margins.Left = 0.25
        pr.PrinterSettings.DefaultPageSettings.Margins.Right = 0.25
        pr.PrinterSettings.DefaultPageSettings.Margins.Top = 0.25
        pr.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0.25
        pr.Form = Me
        pr.PrintAction = Drawing.Printing.PrintAction.PrintToPrinter
        pr.Print()
        Timer1.Enabled = False

        Me.Close()

    End Sub
End Class