Imports MySql.Data.MySqlClient
Public Class KesaLomatoiveidenIlmoittaminen
    Public viikko As Button()
    Private myYhteys As MySqlConnection = New MySqlConnection(serverString)

    Private Sub KesaLomatoiveidenIlmoittaminen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        viikko = New Button() {vk1, vk2, vk3, vk4, vk5}
        tyhjennaviikot()
        Button1.Visible = True


    End Sub


    Public Sub tyhjennaviikot()
        For i = 0 To 4
            viikko(i).BackColor = Color.LightGray

        Next
    End Sub

    Private Sub vk1_Click(sender As System.Object, e As System.EventArgs) Handles vk1.Click
        tyhjennaviikot()
        vk1.BackColor = Color.Lime
        valittuaika.Text = "4.5. - 30.5.2015"
        alkuPVM.Text = "4.5.2015"
        loppupvm.Text = "30.5.2015"
        LokiTapahtumanTallennus(KayttajaHloNro, "Klikattu lomatoivetta " & valittuaika.Text & " / " & "" & " / " & "", "", KayttajaHloNro, 0)

    End Sub

    Private Sub vk2_Click(sender As System.Object, e As System.EventArgs) Handles vk2.Click
        tyhjennaviikot()
        vk2.BackColor = Color.Lime
        valittuaika.Text = "1.6. - 27.6.2015"
        alkuPVM.Text = "1.6.2015"
        loppupvm.Text = "27.6.2015"
        LokiTapahtumanTallennus(KayttajaHloNro, "Klikattu lomatoivetta " & valittuaika.Text & " / " & "" & " / " & "", "", KayttajaHloNro, 0)

    End Sub

    Private Sub vk3_Click(sender As System.Object, e As System.EventArgs) Handles vk3.Click
        tyhjennaviikot()
        vk3.BackColor = Color.Lime
        valittuaika.Text = "29.6. - 25.7.2015"
        alkuPVM.Text = "29.6.2015"
        loppupvm.Text = "25.7.2015"
        LokiTapahtumanTallennus(KayttajaHloNro, "Klikattu lomatoivetta " & valittuaika.Text & " / " & "" & " / " & "", "", KayttajaHloNro, 0)

    End Sub

    Private Sub vk4_Click(sender As System.Object, e As System.EventArgs) Handles vk4.Click
        tyhjennaviikot()
        vk4.BackColor = Color.Lime
        valittuaika.Text = "27.7. - 22.8.2015"
        alkuPVM.Text = "27.7.2015"
        loppupvm.Text = "22.8.2015"
        LokiTapahtumanTallennus(KayttajaHloNro, "Klikattu lomatoivetta " & valittuaika.Text & " / " & "" & " / " & "", "", KayttajaHloNro, 0)

    End Sub

    Private Sub vk5_Click(sender As System.Object, e As System.EventArgs) Handles vk5.Click
        tyhjennaviikot()
        vk5.BackColor = Color.Lime
        valittuaika.Text = "24.8. - 19.9.2015"
        alkuPVM.Text = "24.8.2015"
        loppupvm.Text = "19.9.2015"
        LokiTapahtumanTallennus(KayttajaHloNro, "Klikattu lomatoivetta " & valittuaika.Text & " / " & "" & " / " & "", "", KayttajaHloNro, 0)

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If viikko(0).BackColor = Color.LightGray And _
            viikko(1).BackColor = Color.LightGray And _
            viikko(2).BackColor = Color.LightGray And _
            viikko(3).BackColor = Color.LightGray And _
            viikko(4).BackColor = Color.LightGray Then
            Main.TopMost = False

            MsgBox("LOMATOIVEJAKSOA EI OLE VALITTU")
            Main.TopMost = True
            Exit Sub

        End If
        Try
            myYhteys.Close()
            Dim cmd As New MySqlCommand
            With cmd
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Lomat (HloNro, AlkaenPVM, AstiPVM, LomaTyyppi, Perustelu, Lisatty) VALUES(@HloNro, @AlkaenPVM, @AstiPVM, @LomaTyyppi, @Perustelu, @Lisatty)"
                .Parameters.AddWithValue("@HloNro", KayttajaHloNro)
                .Parameters.AddWithValue("@AlkaenPVM", CType(alkuPVM.Text, Date))
                .Parameters.AddWithValue("@AstiPVM", CType(loppupvm.Text, Date))

                .Parameters.AddWithValue("@LomaTyyppi", "13")

                .Parameters.AddWithValue("@Perustelu", txtPerustelu.Text)
                .Parameters.AddWithValue("@Lisatty", Now)


            End With
            myYhteys.Open()
            cmd.ExecuteNonQuery()
            myYhteys.Close()
            Main.TopMost = False
            LokiTapahtumanTallennus(KayttajaHloNro, "Lisätty lomatoive " & valittuaika.Text & " / " & "" & " / " & "", "", KayttajaHloNro, 0)

            MsgBox("Toive on lisätty")
            Main.TopMost = True

            Me.Close()
            Try

                Main.SC.Panel2.Controls.Clear()
          
                LomaJaVapaaIlmoitukset.Close()

            Catch ex As Exception
                Err.Clear()

            End Try

            LomaJaVapaaIlmoitukset.MdiParent = Main
            LomaJaVapaaIlmoitukset.Dock = DockStyle.Fill

            LomaJaVapaaIlmoitukset.Show()
            Main.SC.Panel2.Controls.Add(LomaJaVapaaIlmoitukset)
            Button1.Visible = False


        Catch ex As Exception
            Err.Clear()

        End Try
    End Sub
End Class