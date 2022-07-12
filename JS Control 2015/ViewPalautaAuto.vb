Imports MySql.Data.MySqlClient

Public Class ViewPalautaAuto
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)
    Public ohita As Boolean = False

    Private Sub Nro1_Click(sender As System.Object, e As System.EventArgs) Handles Nro1.Click
        TallennaPysakointi(1)

    End Sub

    Public Sub TallennaPysakointi(ByVal Alue As Integer)
        Try
            TbConnection.Close()
            '  Dim muokkausrivi As Integer = 0



            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO PysakointiUUSI " & _
                "(AutoNro, Alue, Aika, HloNro, Otettu) " & _
                "VALUES(@AutoNro, @Alue, @Aika, @HloNro, @Otettu)"
                .Parameters.AddWithValue("@AutoNro", Val(Bilikka.Text))
                .Parameters.AddWithValue("@Alue", Alue)
                .Parameters.AddWithValue("@Aika", Now)
                .Parameters.AddWithValue("@Otettu", False)
                .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(PalauttajaNimi.Text))

                '   .Parameters.AddWithValue("@JattanytHlo", PalautaSQLNroNimiesta(PalauttajaNimi.Text))
            End With
            TbConnection.Open()
            cmd2.ExecuteNonQuery()
            TbConnection.Close()

            ' TALLENNA TYÖVUORON LOPETUSAIKA
            Try
                Dim dAl As Date = Format(Today, "\#yyyy\-MM\-dd\#")

                Dim cmdXX As New MySqlCommand()
                With cmdXX
                    .Connection = TbConnection
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE AjetutVuorot SET loppuKLO=@loppuaika WHERE DATE(AlkuPVM)=@pvm AND HloNro=@HloNro"
                    .Parameters.AddWithValue("@pvm", dAl)
                    .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(PalauttajaNimi.Text))
                    .Parameters.AddWithValue("@loppuaika", FormatDateTime(Now, DateFormat.ShortTime))

                End With
                TbConnection.Open()
                cmdXX.ExecuteNonQuery()


            Catch ex As Exception
                Err.Clear()

            End Try


        Catch ex As Exception
            Err.Clear()

        Finally
            TbConnection.Close()
        End Try

        TallennaKtila(Val(Bilikka.Text))

      

        ohita = True


        Button1_Click(Nothing, Nothing)

       


    End Sub

    Public Sub TallennaKtila(ByVal autoZ As Integer)
        Try
            TbConnection.Close()
            Dim cmd As New MySqlCommand()

            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "UPDATE Kalusto SET KTila=@tila WHERE AutoNro=@autoNro"
                .Parameters.AddWithValue("@autoNro", autoZ)

                .Parameters.AddWithValue("@tila", 0)


                TbConnection.Open()
                cmd.ExecuteNonQuery()

            End With
        Catch ex As Exception
            Err.Clear()
        Finally
            TbConnection.Close()

        End Try
    End Sub

    Private Sub Paikka1ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles Paikka1ToolStripMenuItem.Click
        TallennaPysakointi(1)

    End Sub

    Private Sub Paikka2ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles Paikka2ToolStripMenuItem.Click
        TallennaPysakointi(2)
    End Sub

    Private Sub Paikka3ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles Paikka3ToolStripMenuItem.Click
        TallennaPysakointi(3)
    End Sub

    Private Sub Paikka4ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles Paikka4ToolStripMenuItem.Click
        TallennaPysakointi(4)
    End Sub

    Private Sub Paikka5ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles Paikka5ToolStripMenuItem.Click
        TallennaPysakointi(5)
    End Sub

    Private Sub Paikka6ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles Paikka6ToolStripMenuItem.Click
        TallennaPysakointi(6)
    End Sub

    Private Sub Paikka7ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles Paikka7ToolStripMenuItem.Click
        TallennaPysakointi(7)
    End Sub

    Private Sub Paikka8ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles Paikka8ToolStripMenuItem.Click
        TallennaPysakointi(8)
    End Sub

    Private Sub Paikka9ToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles Paikka9ToolStripMenuItem.Click
        TallennaPysakointi(9)
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        '

        If ohita = False Then TallennaPysakointi(0)

        Me.Cursor = Cursors.WaitCursor


        'Me.TopMost = False
        Me.Dispose()

        Naytto2014.MdiParent = Main
        Naytto2014.FormBorderStyle = Windows.Forms.FormBorderStyle.None

        Naytto2014.Location = New Point(0, 0)
        Naytto2014.WindowState = FormWindowState.Maximized
        Naytto2014.Dock = DockStyle.Fill

        Naytto2014.Show()

        Naytto2014.TopMost = True

        sivu = 1
        Main.KirjauduToolStripMenuItem.Enabled = True
        Main.Cursor = Cursors.Default
        KaynnistaNaytonAjastimet()

        Main.MainToolS.Visible = True

    End Sub

    Private Sub ViewPalautaAuto_Load(sender As Object, e As System.EventArgs) Handles Me.Load


        '        Dim ab As MsgBoxResult = MsgBox("OLETKO " & PalauttajaNimi.Text & " ?", MsgBoxStyle.YesNo, "TARKISTUS")
        '       If ab = MsgBoxResult.Yes Then
        'Exit Sub
        'Else

        'Dim aa As String = InputBox("NÄYTÄ HENKILÖKOHTAISTA TAGIASI LUKIJAAN")

        'If aa = "" Or Val(aa) = 0 Then MsgBox("Tuntematon tagi") : Exit Sub
        'Dim a As Long = Val(aa)

        'TbConnection.Close()

        'Dim cmd As New MySqlCommand()
        'With cmd
        '       .Connection = TbConnection
        '      .CommandType = CommandType.Text
        '     .CommandText = "SELECT HloNro, Kulin, AutoNro FROM Tagit " & _
        '               "WHERE (@PVM " & _
        '            "BETWEEN AlkaenPVM AND AstiPVM) AND " & _
        '           "(Nro = @ID) AND Kulin='1'"
        ' .Parameters.AddWithValue("@PVM", Today)
        ' .Parameters.AddWithValue("@ID", a)
        ' End With
        ' TbConnection.Open()
        ' Dim rd As MySqlDataReader = cmd.ExecuteReader
        ' If rd.HasRows = True Then
        ' rd.Read()

        '        PalauttajaNimi.Text = PalautaSQLNrostaNimi(rd.GetValue(0))
        '       TbConnection.Close()
        '      Else
        '     PalauttajaNimi.Text = ""
        '    TbConnection.Close()

        'End If


        'end If












        TextBox1.Focus()

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Main.TopMost = False
        MsgBox("Valitse näppäimistön F-näppäimistä")
        Main.TopMost = True

    End Sub

    Private Sub PalauttajaNimi_Click(sender As Object, e As EventArgs) Handles PalauttajaNimi.Click

    End Sub

    Private Sub PalauttajaNimi_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles PalauttajaNimi.MouseDoubleClick
        Main.TopMost = False


        Dim aa As String = InputBox("NÄYTÄ HENKILÖKOHTAISTA TAGIASI LUKIJAAN")
        If aa = "" Or Val(aa) = 0 Then
            MsgBox("Tuntematon tagi")
            Main.TopMost = True
            Exit Sub

        End If
        Main.TopMost = True

        Dim a As Long = Val(aa)

        TbConnection.Close()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT HloNro, Kulin, AutoNro FROM Tagit " & _
                       "WHERE (@PVM " & _
                     "BETWEEN AlkaenPVM AND AstiPVM) AND " & _
                     "(Nro = @ID) AND Kulin='1'"
            .Parameters.AddWithValue("@PVM", Today)
            .Parameters.AddWithValue("@ID", a)
        End With
        TbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then
            rd.Read()

            PalauttajaNimi.Text = PalautaSQLNrostaNimi(rd.GetValue(0))
            TbConnection.Close()
        Else
            PalauttajaNimi.Text = ""
            TbConnection.Close()

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If PiiloAuto.Text = "0" Or PiiloAuto.Text = "" Then Exit Sub

        TallennaPesuilmoitus(PiiloAuto.Text, PalautaSQLNroNimiesta(PalauttajaNimi.Text), Now, "", False)
        Button2.Visible = False

        'pysäköinti ei käytössä, tallennetaan vuoron päättyminen ja KTila muutos
        TallennaPysakointi(0)


        'Button1_Click(Nothing, Nothing)

    End Sub

    Private Sub PiiloAuto_Click(sender As Object, e As EventArgs) Handles PiiloAuto.Click
        If PiiloAuto.Text = "" Or PiiloAuto.Text = "0" Then Button2.Visible = False Else Button2.Visible = True

    End Sub
End Class