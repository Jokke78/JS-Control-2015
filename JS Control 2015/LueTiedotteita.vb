Imports MySql.Data.MySqlClient

Public Class LueTiedotteita
    Private MyYhteys As MySqlConnection = New MySqlConnection(serverString)

    Private Sub LueTiedotteita_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        LataaTiedotteet()
    End Sub
    Public Sub LataaTiedotteet()

        lbTiedotteet.Items.Clear()


        Try
            Dim dAl As Date = Format(Today, "\#yyyy\-MM\-dd\#")

            MyYhteys.Close()
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT Tiedotteet.TiedoteNro, DATE(Tiedotteet.LisaysPVM), Tiedotteet.Otsikko, LuettavatTiedotteet.Luettu FROM Tiedotteet " & _
                    "INNER JOIN LuettavatTiedotteet ON Tiedotteet.TiedoteNro=LuettavatTiedotteet.TiedoteNro " & _
                    "WHERE LuettavatTiedotteet.HloNro=@Hlo AND (@alku BETWEEN Tiedotteet.VoimassaAlkaenPVM AND Tiedotteet.VoimassaAstiPVM)"
                .Parameters.AddWithValue("@Hlo", KayttajaHloNro)
                .Parameters.AddWithValue("@alku", dal)

            End With
            MyYhteys.Open()
            Using rd As MySqlDataReader = cmd.ExecuteReader

                If rd.HasRows = True Then
                    While rd.Read

                        If rd.GetValue(3) = False Then
                            lbTiedotteet.Items.Add(rd.GetValue(0).ToString & " | [UUSI]" & rd.GetValue(1) & vbTab & rd.GetString(2))
                        Else
                            lbTiedotteet.Items.Add(rd.GetValue(0).ToString & " | " & rd.GetValue(1) & vbTab & rd.GetString(2))

                        End If


                    End While

                End If
            End Using
            MyYhteys.Close()


        Catch ex As Exception
            Err.Clear()

        End Try

    End Sub

    Private Sub lbTiedotteet_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles lbTiedotteet.MouseClick

    End Sub

    Private Sub lbTiedotteet_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lbTiedotteet.SelectedIndexChanged
        If lbTiedotteet.Text = "" Then Exit Sub
        LokiTapahtumanTallennus(KayttajaHloNro, "Luettu tiedote ", lbTiedotteet.Text, 0, 0)
        Try
            Dim a() As String

            ' a = valitun rivin tiedotteen numero
            a = lbTiedotteet.Text.Split("|")


            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT Kuva FROM Tiedotteet WHERE TiedoteNro=@nro"
                .Parameters.AddWithValue("@nro", a(0))

            End With

            Using adapter As New MySqlDataAdapter
                adapter.SelectCommand = cmd
                Dim Data As DataTable

                Data = New DataTable

                '        adapter(cmd, connection)


                Dim commandbuild As MySqlCommandBuilder = New MySqlCommandBuilder(adapter)
                adapter.Fill(Data)

                Dim lb() As Byte = Data.Rows(0).Item("Kuva")
                Dim lstr As New System.IO.MemoryStream(lb)
                PictureBox1.Image = Image.FromStream(lstr)
                PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                lstr.Close()
            End Using
            MyYhteys.Close()


            Dim cmd3 As New MySqlCommand()
            With cmd3
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT Luettu FROM LuettavatTiedotteet WHERE HloNro=@Hlo AND TiedoteNro=@Nro"
                .Parameters.AddWithValue("@Nro", a(0))
                .Parameters.AddWithValue("@Hlo", KayttajaHloNro)

            End With
            MyYhteys.Open()

            Dim onLuettu As Boolean = cmd3.ExecuteScalar

            MyYhteys.Close()

            If onLuettu = False Then
                Dim cmd2 As New MySqlCommand()
                With cmd2
                    .Connection = MyYhteys
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE LuettavatTiedotteet SET Luettu='1', LuettuPVM=@Luettu WHERE TiedoteNro=@Nro AND HloNro=@Hlo"
                    .Parameters.AddWithValue("@Nro", a(0))
                    .Parameters.AddWithValue("@Hlo", KayttajaHloNro)
                    .Parameters.AddWithValue("@Luettu", Now)

                End With
                MyYhteys.Open()
                cmd2.ExecuteNonQuery()
                MyYhteys.Close()

            End If




            MyYhteys.Close()
        Catch ex As Exception
            Err.Clear()

        End Try


    End Sub
End Class