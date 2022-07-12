Imports Microsoft.VisualBasic.PowerPacks

Public Class L_LT02
    Private Sub PictureBox1_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDoubleClick
        Me.Focus()


        Dim pr As New Microsoft.VisualBasic.PowerPacks.Printing.PrintForm
        pr.PrinterSettings.DefaultPageSettings.Margins.Left = 0.25
        pr.PrinterSettings.DefaultPageSettings.Margins.Right = 0.25
        pr.PrinterSettings.DefaultPageSettings.Margins.Top = 0.25
        pr.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0.25
        pr.Form = Me

        pr.PrintAction = Drawing.Printing.PrintAction.PrintToPrinter
        pr.Print()

    End Sub
End Class