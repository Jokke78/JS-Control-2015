Imports MySql.Data.MySqlClient

Public Class TeeTyomaaraus
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)

    Private TbConnection2 As MySqlConnection = New MySqlConnection(serverString)

    Private Sub TeeTM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '     Viikonpaiva.Text = PalautaViikonPaiva(Pvm.Value)
        LataaAutotCBhen()
        haeAsentajat()
        Me.Cursor = Cursors.Default
        AutoRek.Enabled = True

    End Sub
    Private Sub Pvm_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '      Viikonpaiva.Text = PalautaViikonPaiva(Pvm.Value)

    End Sub
    Public Sub haeAsentajat()

        Dim cmd As New MySqlCommand()
        Asentajalle.Items.Clear()
        TbConnection.Close()
        Asentajalle.Items.Add("")

        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT SukuNimi, EtuNimi FROM Henkilosto WHERE KorjaamoTyo = @Tosi"
            .Parameters.AddWithValue("@Tosi", True)
        End With

        TbConnection.Open()

        Try
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then

                While rd.Read


                    Asentajalle.Items.Add(rd.GetString(0) & " " & rd.GetString(1))
                End While


            End If
            rd.Close()
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE Teetyömääräys", ErrorToString, 0, 0)
            Err.Clear()

            '  Throw ex
        End Try


        TbConnection.Close()


    End Sub
    Public Sub LataaAutotCBhen()
        AutoRek.Items.Clear()
        TbConnection.Close()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT RekNro FROM Kalusto WHERE Raporteissa='1'"

        End With
        TbConnection.Open()

        Dim re As MySqlDataReader = cmd.ExecuteReader

        While re.Read
            If re.GetString(0) <> "xxxxxx" Then AutoRek.Items.Add(re.GetString(0))
        End While

        TbConnection.Close()
        re.Close()



    End Sub
    Public Sub LataaTM(ByVal numero As Integer)
        If numero = 0 Then Exit Sub

        Try
            TbConnection.Close()

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT Tyomaaraykset.Otsikko, Kalusto.RekNro FROM Tyomaaraykset " & _
                    "INNER JOIN Kalusto ON Tyomaaraykset.AutoNro=Kalusto.AutoNro " & _
                    "WHERE Tyomaaraykset.TMnro=@TMnro"
                .Parameters.AddWithValue("@TMnro", numero)
            End With
            TbConnection.Open()
            Using rd As MySqlDataReader = cmd.ExecuteReader
                If rd.HasRows = True Then
                    rd.Read()
                    Huomioitavaa.Text = rd.GetString(0)
                    TMnumero.Text = numero.ToString
                    AutoRek.Text = rd.GetString(1)

                End If


            End Using
            If AutoRek.Text <> "" Then
                TarkistaKatsastukset(AutoRek.Text)

                TarkistaHuollot(AutoRek.Text)

                TarkistaKM(AutoRek.Text)

            End If

            TbConnection.Close()

            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM VikaIlmoitukset WHERE LiitettyTMnro=@TMnro AND Korjattu='0'"
                .Parameters.AddWithValue("@TMnro", numero)

            End With
            TbConnection.Open()

            Using rd As MySqlDataReader = cmd2.ExecuteReader
                If rd.HasRows = True Then
                    While rd.Read
                        IlmViat.Items.Add(rd.GetString(1) & vbTab & "(" & FormatDateTime(CType(rd.GetValue(6), Date), DateFormat.ShortDate).ToString & ")" & vbTab & rd.GetValue(0).ToString)

                    End While


                End If
            End Using
            TbConnection.Close()


        Catch ex As Exception
            Err.Clear()

        End Try
        btTallennaTM.Visible = True
        AutoRek.Enabled = False
        btnLisaa.Visible = True

        Me.Cursor = Cursors.Default




    End Sub
    Private Sub AutoRek_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutoRek.SelectedIndexChanged
        If AutoRek.Text <> "" And TMnumero.Text = "" Then
            btnLisaa.Visible = True
            TarkistaKatsastukset(AutoRek.Text)
            HaeIlmoitetutViat()
            TarkistaHuollot(AutoRek.Text)
            TarkistaKM(AutoRek.Text)
        Else
            btnLisaa.Visible = False

        End If
    End Sub
    Public Sub HaeIlmoitetutViat()
        IlmViat.Items.Clear()

        Dim cmd As New MySqlCommand()
        TbConnection.Close()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM VikaIlmoitukset WHERE AutoNro = @AutoNro AND Korjattu = @Tosi" ' AND LiitettyTMnro = @nro"
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(AutoRek.Text))
            .Parameters.AddWithValue("@Tosi", False)
            '        .Parameters.AddWithValue("@nro", 0)

        End With

        TbConnection.Open()
        Try
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            While rd.Read
                If rd.GetValue(11) IsNot DBNull.Value Then
                    If rd.GetBoolean(11) = False Then
                        IlmViat.Items.Add(rd.GetString(1) & vbTab & "(" & FormatDateTime(CType(rd.GetValue(6), Date), DateFormat.ShortDate).ToString & ")" & vbTab & rd.GetValue(0).ToString)
                    End If
                End If


            End While
            rd.Close()

        Catch ex As Exception
            Err.Clear()

        End Try


        TbConnection.Close()

    End Sub
    Public Sub TarkistaHuollot(ByVal rekkari As String)
        ViimHuolto.Text = ""
        TbConnection.Close()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT RekNro, MoottoriOljyt, PVM, KMlukema FROM HuoltojenSeuranta WHERE RekNro = @AutoNro ORDER BY PVM DESC"
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(AutoRek.Text))

        End With
        TbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then

            rd.Read()

            ViimHuolto.Text = rd.GetValue(3).ToString & " / " & FormatDateTime(CType(rd.GetValue(2), Date), DateFormat.ShortDate).ToString


        End If
        TbConnection.Close()
        rd.Close()


    End Sub
    Public Sub TarkistaKM(ByVal rekkari As String)
        ViimKM.Text = ""
        TbConnection.Close()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT AutoNro, PVM, KMlukema FROM KMTiedot WHERE AutoNro = @AutoNro ORDER BY KMlukema DESC"
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(AutoRek.Text))

        End With
        TbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then

            rd.Read()

            ViimKM.Text = rd.GetValue(2).ToString & " / " & FormatDateTime(CType(rd.GetValue(1), Date), DateFormat.ShortDate).ToString


        End If
        TbConnection.Close()
        rd.Close()


    End Sub

    Public Sub TarkistaKatsastukset(ByVal TMrekkari As String)
        ViimeisinLeima.Text = ""
        AikaaLeimaan.Text = ""
        If TMrekkari = "" Then
            Exit Sub

        End If
        TbConnection.Close()
        AikaaLeimaan.ForeColor = Color.Black


        Dim TMautonro As String = PalautaSQLNumeroRekkarista(TMrekkari)

        Dim nytON As Date = FormatDateTime(Now, DateFormat.ShortDate)
        Dim rekkari As String = ""

        Dim erotus As TimeSpan = Nothing

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM KatsastuksienSeuranta WHERE AutoNro=@AutoNro"
            .Parameters.AddWithValue("@AutoNro", TMautonro)

        End With
        TbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader


        While rd.Read

            'tarkistetaan onko katsastusvahti käytössä
            Dim cmKAT As New MySqlCommand()
            TbConnection2.Close()
            With cmKAT
                .Connection = TbConnection2
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Kalusto WHERE AutoNro = @AutoNro"
                .Parameters.AddWithValue("@AutoNro", rd.GetValue(1))

            End With
            TbConnection2.Open()
            Dim KalustoReader As MySqlDataReader = cmKAT.ExecuteReader
            If KalustoReader.HasRows = True Then
                KalustoReader.Read()
                If KalustoReader.GetValue(14) = True Then

                    ViimeisinLeima.Text = FormatDateTime(rd.GetValue(3), DateFormat.ShortDate).ToString


                    erotus = CType(rd.GetValue(4), Date) - nytON
                    If erotus.TotalDays < 365 And erotus.TotalDays > 0 Then
                        ' LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ AIKAA KATSASTAA")
                        '   rekkari = TeeVikailmoitus.PalautaSQLNumeroRekkarista(rd.GetValue(1))
                        AikaaLeimaan.Text = FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & " " & Int(erotus.TotalDays) & " " & "PÄIVÄÄ"
                        If erotus.TotalDays < 30 Then
                            AikaaLeimaan.ForeColor = Color.Violet
                        Else
                            AikaaLeimaan.ForeColor = Color.Black

                        End If



                    End If

                    If erotus.TotalDays = 0 Then
                        '  LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " KATSASTETTAVA TÄNÄÄN")
                        AikaaLeimaan.Text = FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & " " & Int(erotus.TotalDays) & " " & "KATSASTETTAVA TÄNÄÄN"

                    End If

                    If erotus.TotalDays < 0 Then
                        Dim luku = -erotus.TotalDays

                        '         LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ KATSASTAMATTOMANA")
                        AikaaLeimaan.Text = FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & " " & Int(luku) & " " & "PV KATSASTAMATTOMANA" ', AJOKIELLOSSA")
                        AikaaLeimaan.ForeColor = Color.Red

                    End If

                End If
                KalustoReader.Close()
                TbConnection2.Close()

            End If


        End While
        TbConnection.Close()
        rd.Close()


        '       LaitaAikaJarjestykseen()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub btTallennaTM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btTallennaTM.Click

        If TMnumero.Text = "" Then
            Main.TopMost = False
            If AutoRek.Text = "" Then MsgBox("Auto puuttuu") : Main.TopMost = True : Exit Sub
            ' If Asentajalle.Text = "" Then MsgBox("Asentaja puuttuu") : Exit Sub
            Main.TopMost = True
            TbConnection.Close()
            Main.TopMost = False
            If IlmViat.Items.Count = 0 And Huomioitavaa.Text = "" Then MsgBox("Työmääräyksessä ei ole lisättyjä töitä") : Main.TopMost = True : Exit Sub
            If IlmViat.Items.Count >= 11 Then MsgBox("Työmääräykseen voidaan liittää korkeintaa 10 vikaa") : Main.TopMost = True : Exit Sub
            Main.TopMost = True
            Dim tbCommand As MySqlCommand = New MySqlCommand()


            tbCommand.Connection = TbConnection

            tbCommand.CommandText = "INSERT INTO Tyomaaraykset " & _
              "(LuontiPVM, AutoNro, Otsikko, LuontiHlo) " & _
                "VALUES(@LuontiPVM, @AutoNro, @Otsikko, @LuontiHlo);"
            '     Dim alku As String = FormatDateTime(Pvm.Value, DateFormat.ShortDate).ToString & " " & FormatDateTime(klo.Value, DateFormat.ShortTime).ToString
            Dim loppu As String = ""

            tbCommand.Parameters.AddWithValue("@LuontiPVM", Now)
            tbCommand.Parameters.AddWithValue("@LuontiHlo", KayttajaHloNro)
            '     tbCommand.Parameters.AddWithValue("@Kiireellisyys", Kiireellisyys.SelectedIndex)
            tbCommand.Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(AutoRek.Text))
            tbCommand.Parameters.AddWithValue("@Otsikko", Huomioitavaa.Text)


            TbConnection.Open() ' avataan yhteys
            '   Try
            tbCommand.ExecuteNonQuery() ' tallennetaan
            tbCommand.CommandText = "SELECT @@IDENTITY AS TEMPVALUE"
            Dim tmnrovap As Integer = tbCommand.ExecuteScalar
            TbConnection.Close()

            TMnumero.Text = tmnrovap.ToString
            btTulosta.Visible = True

            TbConnection.Close() ' suljetaan yhteys

            If IlmViat.Items.Count > 0 Then
                Dim vikaN As Integer = 0
                For i As Integer = 0 To IlmViat.Items.Count - 1
                    IlmViat.SelectedItem = i
                    Dim koe As String = IlmViat.Items.Item(i).ToString

                    Dim rivit As String() = koe.Split(vbTab)              'Split(vbTab)

                    Dim poistettavarivi As Integer = rivit(2).ToString
                    rivit = Nothing

                    Dim cmd As New MySqlCommand()
                    With cmd
                        .Connection = TbConnection
                        .CommandType = CommandType.Text
                        .CommandText = "UPDATE VikaIlmoitukset " & _
                                          "SET LiitettyTMnro = @LiitettyTMnro " & _
                                          "WHERE riviID = @riviID"
                        .Parameters.AddWithValue("@riviID", poistettavarivi)
                        .Parameters.AddWithValue("@LiitettyTMnro", tmnrovap)
                    End With
                    TbConnection.Open()
                    Try
                        cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        Err.Clear()
                        Main.TopMost = False
                        MsgBox("Vikaa...")
                        Main.TopMost = True
                    End Try
                    TbConnection.Close()
                    cmd = Nothing



                Next


            End If


            AutoRek.Enabled = False
            IlmViat.Enabled = False
            btTallennaTM.Visible = False
        Else ' VANHAN TM MUOKKAUS, Tallentaminen


            Try
                TbConnection.Close()
                Dim cmd2 As New MySqlCommand()
                With cmd2
                    .Connection = TbConnection
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE Tyomaaraykset SET Otsikko=@Otsikko WHERE TMnro=@TMnro"
                    .Parameters.AddWithValue("@Otsikko", Huomioitavaa.Text)
                    .Parameters.AddWithValue("@TMnro", TMnumero.Text)

                End With
                TbConnection.Open()
                cmd2.ExecuteNonQuery()



                btTulosta.Visible = True

                TbConnection.Close() ' suljetaan yhteys

                If IlmViat.Items.Count > 0 Then
                    Dim vikaN As Integer = 0
                    For i As Integer = 0 To IlmViat.Items.Count - 1
                        IlmViat.SelectedItem = i
                        Dim koe As String = IlmViat.Items.Item(i).ToString

                        Dim rivit As String() = koe.Split(vbTab)              'Split(vbTab)

                        Dim poistettavarivi As Integer = rivit(2).ToString
                        rivit = Nothing

                        Dim cmd As New MySqlCommand()
                        With cmd
                            .Connection = TbConnection
                            .CommandType = CommandType.Text
                            .CommandText = "UPDATE VikaIlmoitukset " & _
                                              "SET LiitettyTMnro = @LiitettyTMnro " & _
                                              "WHERE riviID = @riviID"
                            .Parameters.AddWithValue("@riviID", poistettavarivi)
                            .Parameters.AddWithValue("@LiitettyTMnro", Val(TMnumero.Text))
                        End With
                        TbConnection.Open()
                        Try
                            cmd.ExecuteNonQuery()

                        Catch ex As Exception
                            Err.Clear()
                            Main.TopMost = False
                            MsgBox("Vikaa...")
                            Main.TopMost = True
                        End Try
                        TbConnection.Close()
                        cmd = Nothing



                    Next


                End If


                AutoRek.Enabled = False
                IlmViat.Enabled = False
                btTallennaTM.Visible = False


            Catch ex As Exception
                Err.Clear()

            End Try




        End If



        ' HUOM VIKAILMOITUKSIIN LISÄTTÄVÄ TMNUMERO !!!!!!!



    End Sub

    Private Sub TMnumero_Click(sender As System.Object, e As System.EventArgs) Handles TMnumero.Click

    End Sub

    Private Sub IlmViat_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles IlmViat.MouseDoubleClick
        If IlmViat.Text = "" Then Exit Sub

        If TMnumero.Text = "" Then
            IlmViat.Items.Remove(IlmViat.Text)
        Else ' poistetaan tmnro linkitys viasta

            Try
                Dim rivit As String() = IlmViat.Text.Split(vbTab)              'Split(vbTab)

                Dim poistettavarivi As Integer = rivit(2).ToString

                TbConnection.Close()
                Dim cmd As New MySqlCommand()
                With cmd
                    .Connection = TbConnection
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE VikaIlmoitukset " & _
                                  "SET LiitettyTMnro = @LiitettyTMnro " & _
                                  "WHERE riviID = @riviID"
                    .Parameters.AddWithValue("@riviID", poistettavarivi)
                    .Parameters.AddWithValue("@LiitettyTMnro", "0")

                End With
                TbConnection.Open()
                cmd.ExecuteNonQuery()
                TbConnection.Close()
                IlmViat.Items.Remove(IlmViat.Text)

            Catch ex As Exception
                Err.Clear()

            End Try



        End If

    End Sub

    Private Sub IlmViat_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles IlmViat.SelectedIndexChanged

    End Sub

    Private Sub btnLisaa_Click(sender As System.Object, e As System.EventArgs) Handles btnLisaa.Click
        Main.TopMost = False
        If LisattavaVika.Text = "" Then MsgBox("Tekstikenttä on tyhjä") : Main.TopMost = True : Exit Sub
        If AutoRek.Text = "" Then MsgBox("Autoa ei ole valittu") : Main.TopMost = True : Exit Sub
        Main.TopMost = True

        TbConnection.Close()
        Dim haeVika2 As New MySqlCommand()
        With haeVika2
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO VikaIlmoitukset " & _
                "(IlmoitettuVika, AutoNro, HloNro, IlmPVM, Korjattu) " & _
                "VALUES(@IlmoitettuVika, @AutoNro, @HloNro, @IlmPVM, @Korjattu)"
            .Parameters.AddWithValue("@Korjattu", False)
            .Parameters.AddWithValue("@IlmoitettuVika", LisattavaVika.Text)
            '       .Parameters.AddWithValue("@LiitettyTMnro", nroTM)

            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(AutoRek.Text))
            .Parameters.AddWithValue("@HloNro", KayttajaHloNro)
            .Parameters.AddWithValue("@IlmPVM", Now)


        End With
        TbConnection.Open()
        haeVika2.ExecuteNonQuery()
        haeVika2.CommandText = "SELECT @@IDENTITY AS TEMPVALUE"
        Dim tmnrovap As Integer = haeVika2.ExecuteScalar
        TbConnection.Close()

        IlmViat.Items.Add(LisattavaVika.Text & vbTab & "(" & FormatDateTime(Now, DateFormat.ShortDate).ToString & ")" & vbTab & tmnrovap.ToString)

        LisattavaVika.Text = ""
        LisattavaVika.Focus()


    End Sub

    Private Sub btTulosta_Click(sender As System.Object, e As System.EventArgs) Handles btTulosta.Click
        TulostaTyoMaaraus.Location = New Point(0, 0)

        TulostaTyoMaaraus.Show()
        Dim autonNumero As Integer = PalautaSQLNumeroRekkarista(AutoRek.Text)

        TulostaTyoMaaraus.TMnrojaPVM.Text = TMnumero.Text
        TulostaTyoMaaraus.rekjaNro.Text = AutoRek.Text & " (" & autonNumero & ")"
        TulostaTyoMaaraus.rekpvm.Text = PalautaAutonNrostaRekisterointiPVM(autonNumero)
        TulostaTyoMaaraus.Label3.Text = ViimHuolto.Text
        TulostaTyoMaaraus.katsastettu.Text = ViimeisinLeima.Text
        TulostaTyoMaaraus.aikaaleimaan.Text = AikaaLeimaan.Text
        TulostaTyoMaaraus.kmlukemaJApvm.Text = ViimKM.Text
        TulostaTyoMaaraus.varattuPVM.Text = FormatDateTime(Now, DateFormat.ShortDate).ToString
        TulostaTyoMaaraus.Laatija.Text = PalautaSQLNrostaNimi(KayttajaHloNro)
        TulostaTyoMaaraus.asentaja.Text = Asentajalle.Text
        TulostaTyoMaaraus.huomioitavaa.Text = Huomioitavaa.Text

        If IlmViat.Items.Count > 0 Then
            Dim vikaN As Integer = 0
            For i As Integer = 0 To IlmViat.Items.Count - 1
                IlmViat.SelectedItem = i
                Dim koe As String = IlmViat.Items.Item(i).ToString

                Dim rivit As String() = koe.Split(vbTab)              'Split(vbTab)

                Dim lisattavavika As String = rivit(0).ToString
                Dim ilmpvm As String = rivit(1)
                Select Case i
                    Case 0
                        TulostaTyoMaaraus.tyo1.Text = lisattavavika : TulostaTyoMaaraus.ilm1.Text = ilmpvm
                    Case 1
                        TulostaTyoMaaraus.tyo2.Text = lisattavavika : TulostaTyoMaaraus.ilm2.Text = ilmpvm
                    Case 2
                        TulostaTyoMaaraus.tyo3.Text = lisattavavika : TulostaTyoMaaraus.ilm3.Text = ilmpvm
                    Case 3
                        TulostaTyoMaaraus.tyo4.Text = lisattavavika : TulostaTyoMaaraus.ilm4.Text = ilmpvm
                    Case 4
                        TulostaTyoMaaraus.tyo5.Text = lisattavavika : TulostaTyoMaaraus.ilm5.Text = ilmpvm
                    Case 5
                        TulostaTyoMaaraus.tyo6.Text = lisattavavika : TulostaTyoMaaraus.ilm6.Text = ilmpvm
                    Case 6
                        TulostaTyoMaaraus.tyo7.Text = lisattavavika : TulostaTyoMaaraus.ilm7.Text = ilmpvm
                    Case 7
                        TulostaTyoMaaraus.tyo8.Text = lisattavavika : TulostaTyoMaaraus.ilm8.Text = ilmpvm
                    Case 8
                        TulostaTyoMaaraus.tyo9.Text = lisattavavika : TulostaTyoMaaraus.ilm9.Text = ilmpvm
                    Case 9
                        TulostaTyoMaaraus.tyo10.Text = lisattavavika : TulostaTyoMaaraus.ilm10.Text = ilmpvm

                End Select
            Next i



        End If
        '  TulostaTyoMaaraus.osa1.Text = osa1.Text : TulostaTyoMaaraus.maara1.Text = maara1.Text
        ' TulostaTyoMaaraus.osa2.Text = osa2.Text : TulostaTyoMaaraus.maara2.Text = maara2.Text
        'TulostaTyoMaaraus.osa3.Text = osa3.Text : TulostaTyoMaaraus.maara3.Text = maara3.Text
        'TulostaTyoMaaraus.osa4.Text = osa4.Text : TulostaTyoMaaraus.maara4.Text = maara4.Text

        TulostaTyoMaaraus.TextBox1.Focus()
        '      btTulosta.Visible = False
        Me.Close()

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

   
    Private Sub HaeKaikkiKorjaamattomatViatToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles HaeKaikkiKorjaamattomatViatToolStripMenuItem.Click
        HaeIlmoitetutViat()

    End Sub

    Private Sub Button1_Click_1(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        TulostaTyoMaaraus.Location = New Point(0, 0)

        TulostaTyoMaaraus.Show()
 
        TulostaTyoMaaraus.TMnrojaPVM.Text = ""
        TulostaTyoMaaraus.rekjaNro.Text = ""
        TulostaTyoMaaraus.rekpvm.Text = ""
        TulostaTyoMaaraus.Label3.Text = ""
        TulostaTyoMaaraus.katsastettu.Text = ""
        TulostaTyoMaaraus.aikaaleimaan.Text = ""
        TulostaTyoMaaraus.kmlukemaJApvm.Text = ""
        TulostaTyoMaaraus.varattuPVM.Text = ""
        TulostaTyoMaaraus.Laatija.Text = ""
        TulostaTyoMaaraus.asentaja.Text = ""
        TulostaTyoMaaraus.huomioitavaa.Text = ""

      
        '  TulostaTyoMaaraus.osa1.Text = osa1.Text : TulostaTyoMaaraus.maara1.Text = maara1.Text
        ' TulostaTyoMaaraus.osa2.Text = osa2.Text : TulostaTyoMaaraus.maara2.Text = maara2.Text
        'TulostaTyoMaaraus.osa3.Text = osa3.Text : TulostaTyoMaaraus.maara3.Text = maara3.Text
        'TulostaTyoMaaraus.osa4.Text = osa4.Text : TulostaTyoMaaraus.maara4.Text = maara4.Text

        TulostaTyoMaaraus.TextBox1.Focus()
        '      btTulosta.Visible = False
        Me.Close()
    End Sub
End Class