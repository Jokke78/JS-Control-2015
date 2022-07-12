Imports Microsoft.VisualBasic.PowerPacks

Public Class XTulostaTyoOhje

    Private Sub TulostaTyoOhje_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(0, 0)

    End Sub


    Public Sub Tulosta()

    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Try
            Me.Focus()


            Dim pr As New Microsoft.VisualBasic.PowerPacks.Printing.PrintForm
            pr.PrinterSettings.DefaultPageSettings.Margins.Left = 0.25
            pr.PrinterSettings.DefaultPageSettings.Margins.Right = 0.25
            pr.PrinterSettings.DefaultPageSettings.Margins.Top = 0.25
            pr.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0.25
            pr.Form = Me

            pr.PrintAction = Drawing.Printing.PrintAction.PrintToPrinter
            Me.Focus()
            pr.Print()


            '    Main.Enabled = False

            Me.Dispose()
        Catch ex As Exception
            Err.Clear()

        End Try



    End Sub
End Class