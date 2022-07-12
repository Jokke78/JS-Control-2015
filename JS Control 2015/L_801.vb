Imports Microsoft.VisualBasic.PowerPacks

Public Class L_801

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub PictureBox1_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDoubleClick

        PictureBox1.Image = Image.FromHbitmap(My.Resources.LT02.GetHbitmap())
        Exit Sub

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