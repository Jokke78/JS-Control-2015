Imports MySql.Data.MySqlClient
Public Class UudetTyoOhjeet
    Private myYhteys As MySqlConnection = New MySqlConnection(serverString)

    Private Sub UudetTyoOhjeet_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        pvm.MinDate = Today
        lataaTyovuorot()
    End Sub
    Public Sub lataaTyovuorot()
        cbTyoVuoro.Items.Clear()
        PictureBox1.Image = Nothing

        Try
            myYhteys.Close()
            Dim dAl As Date = Format(pvm.Value, "\#yyyy\-MM\-dd\#")


            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT DISTINCT Lyhenne FROM TyoVuorot WHERE (@pvm BETWEEN AlkaenPVM AND AstiPVM) ORDER BY Lyhenne ASC"
                .Parameters.AddWithValue("@pvm", dAl)
            End With

            myYhteys.Open()
            Using rd As MySqlDataReader = cmd.ExecuteReader

                If rd.HasRows = True Then
                    While rd.Read
                        cbTyoVuoro.Items.Add(rd.GetString(0))

                    End While
                End If

            End Using

        Catch ex As Exception
            Err.Clear()
        Finally
            myYhteys.Close()

        End Try



    End Sub
    Private Sub TulostaTyoOhje_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs)

    End Sub
    Public Sub haeTyoohjeesille()
        myYhteys.Open()
        PictureBox1.Image = Nothing

        Dim dAl1 As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")



        Dim cmdmaara As New MySqlCommand()
        With cmdmaara
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT COUNT(*) FROM TyoOhjeet WHERE TVnimi=@nimi AND (@pvm BETWEEN AlkaenPVM AND AstiPVM)"
            .Parameters.AddWithValue("@nimi", cbTyoVuoro.Text)
            .Parameters.AddWithValue("@pvm", dAl1)
        End With

        Dim maara As Integer = cmdmaara.ExecuteScalar
        FLP1.Controls.Clear()

        If maara > 1 Then
            SplitContainer1.Panel1Collapsed = False
            For i = 1 To maara

                Dim xbutton As New Button
                xbutton.Size = New Size(60, 25)
                xbutton.Text = i.ToString
                xbutton.Name = "btOhje" & i
                AddHandler xbutton.Click, AddressOf ButtonXXX_Click
                FLP1.Controls.Add(xbutton)
            Next

            For Each ButtonXXX In FLP1.Controls
                If CType(ButtonXXX.Name, String) = "btOhje1" Then
                    ButtonXXX.backcolor = Color.Lime

                Else
                    ButtonXXX.backcolor = Color.IndianRed

                End If


            Next
        Else


            SplitContainer1.Panel1Collapsed = True


        End If


        Try
            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT Kuva, Sivu FROM TyoOhjeet WHERE TVnimi=@nimi AND (@pvm BETWEEN AlkaenPVM AND AstiPVM) ORDER BY ID ASC"
                .Parameters.AddWithValue("@nimi", cbTyoVuoro.Text)
                .Parameters.AddWithValue("@pvm", dAl1)

            End With

            Dim adapter As New MySqlDataAdapter
            adapter.SelectCommand = cmd2
            Dim Data As DataTable

            Data = New DataTable

            Dim commandbuild As MySqlCommandBuilder = New MySqlCommandBuilder(adapter)
            adapter.Fill(Data)

            Dim lb() As Byte = Data.Rows(0).Item("Kuva")
            Using lstr As New System.IO.MemoryStream(lb)
                PictureBox1.Image = Image.FromStream(lstr)
                PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                lstr.Close()
            End Using
            Dim sivut As Integer = Data.Rows(0).Item("Sivu")
            '      If sivut = 0 Then
            '    SplitContainer1.Panel1Collapsed = True
            '     Else
            '    SplitContainer1.Panel1Collapsed = False


            '     End If


        Catch ex As Exception
            Err.Clear()
        Finally
            myYhteys.Close()

        End Try


        If PictureBox1.Image IsNot Nothing Then
            btnTulosta.Visible = True
        Else
            btnTulosta.Visible = False

        End If


    End Sub
    Private Sub ButtonXXX_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim clickedButton As Button
        If cbTyoVuoro.Text = "" Then Exit Sub

        clickedButton = CType(sender, Button)

        If clickedButton.Text = "" Then Exit Sub 'clickedLabel.ForeColor = Color.Red

        Dim dAl1 As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")

        Dim sivu As Integer = Val(clickedButton.Text)
        For Each ButtonXXX In FLP1.Controls
            If CType(ButtonXXX.Name, String) = "btOhje" & clickedButton.Text Then
                ButtonXXX.backcolor = Color.Lime

            Else
                ButtonXXX.backcolor = Color.IndianRed

            End If


        Next
        Try
            myYhteys.Open()

            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT Kuva, Sivu FROM TyoOhjeet WHERE TVnimi=@nimi AND (@pvm BETWEEN AlkaenPVM AND AstiPVM) AND Sivu=@Sivu"
                .Parameters.AddWithValue("@nimi", cbTyoVuoro.Text)
                .Parameters.AddWithValue("@pvm", dAl1)
                .Parameters.AddWithValue("@Sivu", sivu)

            End With

            Dim adapter As New MySqlDataAdapter
            adapter.SelectCommand = cmd2
            Dim Data As DataTable

            Data = New DataTable

            Dim commandbuild As MySqlCommandBuilder = New MySqlCommandBuilder(adapter)
            adapter.Fill(Data)

            Dim lb() As Byte = Data.Rows(0).Item("Kuva")
            Using lstr As New System.IO.MemoryStream(lb)
                PictureBox1.Image = Image.FromStream(lstr)
                PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                lstr.Close()
            End Using
            Dim sivut As Integer = Data.Rows(0).Item("Sivu")
            '      If sivut = 0 Then
            '    SplitContainer1.Panel1Collapsed = True
            '     Else
            '    SplitContainer1.Panel1Collapsed = False


            '     End If

            btnTulosta.Visible = True
        Catch ex As Exception
            Err.Clear()

        End Try


        myYhteys.Close()





    End Sub
    Private Sub cbTyoVuoro_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbTyoVuoro.SelectedIndexChanged
        haeTyoohjeesille()

    End Sub

    Private Sub pvm_ValueChanged(sender As System.Object, e As System.EventArgs) Handles pvm.ValueChanged
        lataaTyovuorot()
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
                If maara > 3 Then
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
                        .Parameters.AddWithValue("@Kohde", cbTyoVuoro.Text)

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

        LokiTapahtumanTallennus(KayttajaHloNro, "Tulostettu ohje " & cbTyoVuoro.Text, "", 0, 0)
        Try
            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Tulosteet (HloNro, Aika, Kohde) VALUES(@HloNro, @Aika, @Kohde)"
                .Parameters.AddWithValue("@HloNro", KayttajaHloNro)
                .Parameters.AddWithValue("@Aika", Now)
                .Parameters.AddWithValue("@Kohde", cbTyoVuoro.Text)

            End With
            myYhteys.Open()
            cmd2.ExecuteNonQuery()
            myYhteys.Close()

        Catch ex As Exception
            Err.Clear()

        End Try

        XTulostaTyoOhje.Location = New Point(0, 0)
        myYhteys.Close()

        Dim tulostSivu As Integer
        Dim btn As New Button

        Dim x As Integer = FLP1.Controls.Count
        If x = 0 Then
            tulostSivu = 0
        Else
            For Each btn In FLP1.Controls
                If btn.BackColor = Color.Lime Then
                    tulostSivu = Val(btn.Text)
                End If
            Next
        End If

        Try
            Dim dAl As Date = Format(pvm.Value, "\#yyyy\-MM\-dd\#")

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT Kuva FROM TyoOhjeet WHERE TVnimi=@nro AND (@pvm BETWEEN AlkaenPVM and AstiPVM) AND Sivu=@sivu"
                .Parameters.AddWithValue("@nro", cbTyoVuoro.Text)
                .Parameters.AddWithValue("@pvm", dAl)
                .Parameters.AddWithValue("@sivu", tulostSivu)
            End With
            myYhteys.Open()

            Dim adapter As New MySqlDataAdapter
            adapter.SelectCommand = cmd
            Dim Data As DataTable

            Data = New DataTable

            '        adapter(cmd, connection)


            Dim commandbuild As MySqlCommandBuilder = New MySqlCommandBuilder(adapter)
            adapter.Fill(Data)

            Dim lb() As Byte = Data.Rows(0).Item("Kuva")
            Using lstr As New System.IO.MemoryStream(lb)
                XTulostaTyoOhje.PictureBox1.Image = Image.FromStream(lstr)
                XTulostaTyoOhje.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                lstr.Close()
            End Using


        Catch ex As Exception
            Err.Clear()

        End Try


        '      Me.Enabled = False

        XTulostaTyoOhje.Location = New Point(0, 0)

        XTulostaTyoOhje.Show()
        myYhteys.Close()


    End Sub

    Private Sub btnTulosta_Click(sender As System.Object, e As System.EventArgs) Handles btnTulosta.Click
        TulostaToolStripMenuItem_Click(Nothing, Nothing)
        btnTulosta.Visible = False

    End Sub

    Private Sub Label2_Click(sender As System.Object, e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub
End Class