Imports MySql.Data.MySqlClient


Public Class KirjautumisenSplah
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)
    Dim ohita As Boolean = False

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If ohita = False Then
            Try
                TbConnection.Close()
                Dim cmd2 As New MySqlCommand()
                With cmd2
                    .Connection = TbConnection
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO PysakointiUUSI " & _
                    "(AutoNro, HloNro, Aika, Alue, Otettu) " & _
                    "VALUES(@AutoNro, @HloNro, @Aika, @Alue, @Otettu)"
                    .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(AUTOOOO.Text))
                    .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(KayttajaNimi))
                    .Parameters.AddWithValue("@Aika", Now)
                    .Parameters.AddWithValue("@Alue", 0)
                    .Parameters.AddWithValue("@Otettu", True)
                End With
                TbConnection.Open()
                cmd2.ExecuteNonQuery()
                TbConnection.Close()

            Catch ex As Exception
                LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE työvuoro kirjaus- auton ajoon merkitseminen", ErrorToString, 0, 0)

                Err.Clear()

            End Try
        End If


        Me.Cursor = Cursors.WaitCursor

        'Me.TopMost = False
        Me.Close()
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

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Try
            TbConnection.Close()
            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO PysakointiUUSI " & _
                "(AutoNro, HloNro, Aika, Alue, Otettu) " & _
                "VALUES(@AutoNro, @HloNro, @Aika, @Alue, @Otettu)"
                .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(AUTOOOO.Text))
                .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(KayttajaNimi))
                .Parameters.AddWithValue("@Aika", Now)
                .Parameters.AddWithValue("@Alue", 0)
                .Parameters.AddWithValue("@Otettu", True)
            End With
            TbConnection.Open()
            cmd2.ExecuteNonQuery()
            TbConnection.Close()

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE työvuoro kirjaus- auton ajoon merkitseminen", ErrorToString, 0, 0)

            Err.Clear()

        End Try

        Me.Cursor = Cursors.WaitCursor

        'Me.TopMost = False
        Me.Close()
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

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)
        pb1.Visible = False
        pb2.Visible = True

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs)
        pb2.Visible = False
        pb1.Visible = True

    End Sub

    Private Sub KirjautumisenSplah_Load(sender As Object, e As System.EventArgs) Handles Me.Load


    End Sub

    Public Sub laitaPysakoinninKuvaEsille(ByVal AutoN As Integer)
        If PysakointiPalvelut = True Then
            Try
                TbConnection.Close()
                Dim cmd As New MySqlCommand()
                With cmd
                    .Connection = TbConnection
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT Alue FROM PysakointiUUSI WHERE AutoNro = @AutoNro ORDER BY ID DESC"
                    .Parameters.AddWithValue("@AutoNro", AutoN)
                End With

                TbConnection.Open()
                Dim rd As MySqlDataReader = cmd.ExecuteReader
                If rd.HasRows = True Then
                    rd.Read()
                    Select Case rd.GetValue(0)
                        Case 1 : pb1.Visible = True
                        Case 2 : pb2.Visible = True
                        Case 3 : pb3.Visible = True
                        Case 4 : pb4.Visible = True
                        Case 5 : pb5.Visible = True
                        Case 6 : pb6.Visible = True
                        Case 7 : pb7.Visible = True
                        Case 8 : pb8.Visible = True
                        Case 9 : pb9.Visible = True
                        Case 10 : korjaamolla.Visible = True

                        Case 11 : korjaamolla.Visible = True
                        Case 12 : ajokielto.Visible = True


                        Case Else
                            eipystietoa.Visible = True

                    End Select
                End If
                rd.Close()
                TbConnection.Close()
            Catch ex As Exception
                LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE työvuoro kirjaus kuvan esille laittaminen", ErrorToString, 0, 0)
                Err.Clear()

            End Try
        Else

            'PYSAKÖINTIPALCELUT EIVÄT KÄYTÖSSÄ
            eipystietoa.Visible = True


        End If



    End Sub
    Public Function HaeTagistaKayttajaTiedot(ByVal koodi As Integer)
        Dim hloNro As Integer = 0 'cmd.ExecuteScalar

        Dim cmd As New MySqlCommand()
        Try
            TbConnection.Close()

            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT HloNro, Kulin, AutoNro FROM Tagit " & _
                      "WHERE (@PVM " & _
                    "BETWEEN AlkaenPVM AND AstiPVM) AND " & _
                    "(Nro = @ID) "
                .Parameters.AddWithValue("@PVM", Today)
                .Parameters.AddWithValue("@ID", koodi)

            End With

            TbConnection.Open()
            Dim rd As MySqlDataReader = cmd.ExecuteReader

            If rd.HasRows = True Then
                While rd.Read
                    If rd.GetValue(1) = 0 And rd.GetValue(2) <> 0 Then rd.Close() : TbConnection.Close() : Return -2 : Exit Function
                    If rd.GetValue(1) = 0 Then
                        TbConnection.Close() : rd.Close() : TbConnection.Close() : Return -1 : Exit Function
                    Else
                        hloNro = rd.GetValue(0)
                    End If
                End While
            End If
            rd.Close()

            TbConnection.Close()

            If hloNro <> 0 Then

                Dim hloHaku As New MySqlCommand()
                With hloHaku
                    .Connection = TbConnection
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT * FROM Henkilosto WHERE HloNro = @HloNro"
                    .Parameters.AddWithValue("@HloNro", hloNro)
                End With
                TbConnection.Open()
                Dim rdhlo As MySqlDataReader = hloHaku.ExecuteReader

                If rdhlo.HasRows = True Then
                    rdhlo.Read()
                    KayttajaNimi = rdhlo.GetString(1) & " " & rdhlo.GetString(2)
                    KayttajanAuto = rdhlo.GetValue(10)
                    KayttajanTaso = rdhlo.GetString(5)
                    KayttajanSalasana = rdhlo.GetString(14)
                    KayttajaHloNro = rdhlo.GetValue(0)


                    TbConnection.Close()
                    rdhlo.Close()

                Else
                    hloNro = 0
                    TbConnection.Close()
                    rdhlo.Close()
                End If
            End If

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE työvuoro kirjaus haetagista tiedot", ErrorToString, 0, 0)
            Err.Clear()

        End Try
      

        Return hloNro

    End Function
    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged
        Try
            Dim KayttajaOK As Boolean = False
            Dim tagiNro As Integer = 0

            ' TAGIN TUNNISTUS
            If Len(TextBox1.Text) = 10 And Val(TextBox1.Text) <> 0 Then
                '    Label3.Text = "TAGI NRO " & LuettuViivaKoodi.Text
                ' --> Tarkista tagi historiasta onko kuljettaja vai autotagi
                ' Jos kuljettaja tagi kirjaudu jos auto tagi tarkista auto sijanti
                ' Jos kortin taso on A tai H kirjaa työpäivä jne....
                '
                tagiNro = Val(TextBox1.Text)
                Dim koodi As Integer = Val(TextBox1.Text)
                Dim paluu As Integer = HaeTagistaKayttajaTiedot(TextBox1.Text)
                If paluu = -1 Then
                    TextBox1.Text = ""
                    TextBox1.Focus()
                    Exit Sub

                End If
                If paluu = 0 Then
                    TextBox1.Text = ""
                    TextBox1.Focus()
                    Exit Sub
                End If

                If paluu = -2 Then
                    TextBox1.Text = ""
                    TextBox1.Focus()
                    Exit Sub


                End If


                'TALLENNETAAN AUTO KÄYTTÄJÄLLE
                TbConnection.Close()
                Dim cmd2 As New MySqlCommand()
                With cmd2
                    .Connection = TbConnection
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO PysakointiUUSI " & _
                   "(AutoNro, Alue, Aika, HloNro, Otettu) " & _
                   "VALUES(@AutoNro, @Alue, @Aika, @HloNro, @Otettu)"
                    .Parameters.AddWithValue("@AutoNro", Val(AUTOOOO.Text))
                    .Parameters.AddWithValue("@Alue", 0)
                    .Parameters.AddWithValue("@Aika", Now)
                    .Parameters.AddWithValue("@Otettu", True)
                    .Parameters.AddWithValue("@HloNro", paluu)
                End With
                TbConnection.Open()
                cmd2.ExecuteNonQuery()
                TbConnection.Close()
                ohita = True

                Button1_Click(Nothing, Nothing)

            End If

        Catch ex As Exception

            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE työvuoro kirjaus auton ajoon laittaminen", ErrorToString, 0, 0)
            Err.Clear()

        End Try

    







    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
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
             Me.Close()

            Main.SC.Visible = False

            '    OhjelmanValintaValikko.Close()

            ViewPalautaAuto.MdiParent = Main
            ViewPalautaAuto.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            ViewPalautaAuto.Dock = DockStyle.Fill
            ViewPalautaAuto.WindowState = FormWindowState.Maximized
            ViewPalautaAuto.PiiloAuto.Text = AUTOOOO.Text

            ViewPalautaAuto.Show()

            ViewPalautaAuto.TopMost = True

            ViewPalautaAuto.otsikko.Text = "AUTON " & AUTOOOO.Text & " PALAUTTAMINEN"
            ViewPalautaAuto.PalauttajaNimi.Text = PalautaSQLNrostaNimi(rd.GetValue(0))
            '       dffd()
            ViewPalautaAuto.Bilikka.Text = AUTOOOO.Text
            ViewPalautaAuto.TextBox1.Focus()

            rd.Close()
            TbConnection.Close()
        Else
            Main.TopMost = False
            MsgBox("TAGIA EI TUNNISTETTU")
            Main.TopMost = True

            TbConnection.Close()
            TextBox1.Focus()

        End If
    End Sub

    Private Sub Button2_Click_1(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Main.TopMost = False

        Dim aa As String = InputBox("NÄYTÄ HENKILÖKOHTAISTA TAGIASI LUKIJAAN")
        If aa = "" Or Val(aa) = 0 Then MsgBox("Tuntematon tagi") : Main.TopMost = True : Exit Sub
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
       
            Dim kulinro As Integer = rd.GetValue(0)
            rd.Close()
            TbConnection.Close()
            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT TyyppiID FROM Henkilosto WHERE HloNro=@HloNro"
                .Parameters.AddWithValue("@HloNro", kulinro)
            End With
            TbConnection.Open()
            Dim xx As String = cmd2.ExecuteScalar
            TbConnection.Close()

            If xx = "A" Or xx = "P" Or xx = "H" Or xx = "J" Or xx = "E" Then
                TallennaPysakointi(10, kulinro)


            Else
                Main.TopMost = False
                MsgBox("OTA YHTEYS KORJAAMOHENKILÖKUNTAAN TAI HALLIMESTARIIN AUTON SIIRTÄMISESTÄ KORJAAMOLLE")
                Main.TopMost = True
                TextBox1.Focus()

            End If


        Else
            Main.TopMost = False
            MsgBox("TAGIA EI TUNNISTETTU")
            Main.TopMost = True

            TbConnection.Close()
            TextBox1.Focus()

        End If
    End Sub
    Public Sub TallennaPysakointi(ByVal Alue As Integer, ByVal palauttajanNro As Integer)
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
                .Parameters.AddWithValue("@AutoNro", Val(AUTOOOO.Text))
                .Parameters.AddWithValue("@Alue", Alue)
                .Parameters.AddWithValue("@Aika", Now)
                .Parameters.AddWithValue("@Otettu", False)
                .Parameters.AddWithValue("@HloNro", palauttajanNro)

                '   .Parameters.AddWithValue("@JattanytHlo", PalautaSQLNroNimiesta(PalauttajaNimi.Text))
            End With
            TbConnection.Open()
            cmd2.ExecuteNonQuery()
            TbConnection.Close()
            ohita = True

            Button1_Click(Nothing, Nothing)
        Catch ex As Exception
            Err.Clear()

        End Try





    End Sub

    Private Sub Button3_Click_1(sender As System.Object, e As System.EventArgs) Handles Button3.Click
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

            Dim kulinro As Integer = rd.GetValue(0)
            rd.Close()
            TbConnection.Close()
            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT TyyppiID FROM Henkilosto WHERE HloNro=@HloNro"
                .Parameters.AddWithValue("@HloNro", kulinro)
            End With
            TbConnection.Open()
            Dim xx As String = cmd2.ExecuteScalar
            TbConnection.Close()

            If xx = "A" Or xx = "P" Or xx = "H" Or xx = "J" Or xx = "E" Then
                TallennaPysakointi(11, kulinro)


            Else
                Main.TopMost = False
                MsgBox("OTA YHTEYS KORJAAMOHENKILÖKUNTAAN TAI HALLIMESTARIIN AUTON SIIRTÄMISESTÄ ULKOPUOLISELLE KORJAAMOLLE")
                TextBox1.Focus()
                Main.TopMost = True

            End If


        Else
            Main.TopMost = False

            MsgBox("TAGIA EI TUNNISTETTU")
            Main.TopMost = True

            TbConnection.Close()
            TextBox1.Focus()

        End If
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
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

            Dim kulinro As Integer = rd.GetValue(0)
            rd.Close()
            TbConnection.Close()
            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT TyyppiID FROM Henkilosto WHERE HloNro=@HloNro"
                .Parameters.AddWithValue("@HloNro", kulinro)
            End With
            TbConnection.Open()
            Dim xx As String = cmd2.ExecuteScalar
            TbConnection.Close()

            If xx = "A" Or xx = "P" Or xx = "H" Or xx = "J" Or xx = "E" Then
                TallennaPysakointi(12, kulinro)


            Else
                Main.TopMost = False
                MsgBox("OTA YHTEYS KORJAAMOHENKILÖKUNTAAN TAI HALLIMESTARIIN AUTON SIIRTÄMISESTÄ AJOKIELTOON")
                Main.TopMost = True
                TextBox1.Focus()

            End If


        Else
            Main.TopMost = False
            MsgBox("TAGIA EI TUNNISTETTU")
            Main.TopMost = True

            TbConnection.Close()
            TextBox1.Focus()

        End If
    End Sub

   
End Class