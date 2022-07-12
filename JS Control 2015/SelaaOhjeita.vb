Imports Microsoft.VisualBasic.PowerPacks
Imports MySql.Data.MySqlClient


Public Class SelaaOhjeita
    Private myYhteys As MySqlConnection = New MySqlConnection(serverString)
    Private Sub PictureBox1_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDoubleClick

    End Sub

  

    Private Sub TulostaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TulostaToolStripMenuItem.Click

        Try
            myYhteys.Close()

            If KayttajanTaso = "K" Then
                Dim dAl1 As Date = Format(Today, "\#yyyy\-MM\-dd\#")

                Dim cmd As New MySqlCommand()
                With cmd
                    .Connection = myYhteys
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT COUNT(*) As Lasku FROM Tulosteet WHERE DATE(Aika)=@Aika AND HloNro=@HloNro"
                    .Parameters.AddWithValue("@Aika", dAl1)
                    .Parameters.AddWithValue("@HloNro", KayttajaHloNro)
                End With
                myYhteys.Open()
                Dim maara As Integer = cmd.ExecuteScalar
                myYhteys.Close()
                If maara > 2 Then
                    Main.TopMost = False

                    MsgBox("Päivittäinen tulostusmäärä on täyttynyt. Ole tarvittaessa yhteydessä hallipäivystäjään tulostaaksesi vielä tänään")
                    LokiTapahtumanTallennus(KayttajaHloNro, "Tulostomäärä täyttynyt", "", KayttajaHloNro, 0)
                    Main.TopMost = True

                    Exit Sub
                Else
                    Dim cmd2 As New MySqlCommand()
                    With cmd2
                        .Connection = myYhteys
                        .CommandType = CommandType.Text
                        .CommandText = "INSERT INTO Tulosteet (HloNro, Aika, Kohde) VALUES(@HloNro, @Aika, @Kohde)"
                        .Parameters.AddWithValue("@HloNro", KayttajaHloNro)
                        .Parameters.AddWithValue("@Aika", Now)
                        .Parameters.AddWithValue("@Kohde", Main.tview.SelectedNode.Text)

                    End With
                    myYhteys.Open()
                    cmd2.ExecuteNonQuery()
                    myYhteys.Close()

                End If
           
            End If



        Catch ex As Exception
            Err.Clear()
            Exit Sub

        End Try

        LokiTapahtumanTallennus(KayttajaHloNro, "Tulostettu ohje " & Main.tview.SelectedNode.Text.ToString, "", 0, 0)
        Try
            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Tulosteet (HloNro, Aika, Kohde) VALUES(@HloNro, @Aika, @Kohde)"
                .Parameters.AddWithValue("@HloNro", KayttajaHloNro)
                .Parameters.AddWithValue("@Aika", Now)
                .Parameters.AddWithValue("@Kohde", Main.tview.SelectedNode.Text)

            End With
            myYhteys.Open()
            cmd2.ExecuteNonQuery()
            myYhteys.Close()

            Me.Focus()
            Dim pr As New Microsoft.VisualBasic.PowerPacks.Printing.PrintForm
            pr.PrinterSettings.DefaultPageSettings.Margins.Left = 0.25
            pr.PrinterSettings.DefaultPageSettings.Margins.Right = 0.25
            pr.PrinterSettings.DefaultPageSettings.Margins.Top = 0.25
            pr.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0.25
            pr.Form = Me

            pr.PrintAction = Drawing.Printing.PrintAction.PrintToPrinter
            pr.Print()

        Catch ex As Exception
            Err.Clear()

        End Try

    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening

    End Sub

    Private Sub SelaaOhjeita_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click

    End Sub
End Class