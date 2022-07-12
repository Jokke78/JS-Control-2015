Imports MySql.Data.MySqlClient

Public Class xLahtoLista
    Private MyYhteys As MySqlConnection = New MySqlConnection(serverString)
    Private MyYhteys2 As MySqlConnection = New MySqlConnection(serverString)

    Private Sub PVM_ValueChanged(sender As System.Object, e As System.EventArgs) Handles PVM.ValueChanged
        Viikonpaiva.Text = PalautaViikonPaiva(PVM.Value)
        LataaTunnuksetDataKenttaan()
    End Sub
    Public Sub LataaTunnuksetDataKenttaan()
        LokiTapahtumanTallennus(KayttajaHloNro, "Ladattu lähtölistan muokkaus", PVM.Value.ToString, 0, 0)


        MyYhteys.Close()
        MyYhteys.Open()

        Dim tbAdapter As New MySqlDataAdapter()
        Dim dAl As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")

        With tbAdapter
            .SelectCommand = New MySqlCommand
            .SelectCommand.Connection = MyYhteys
            .SelectCommand.CommandText = "SELECT AjetutVuorot.TVLyhenne, Kalusto.RekNro, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.Etunimi) AS Kuljettaja FROM AjetutVuorot " & _
                "INNER JOIN Henkilosto ON Henkilosto.HloNro=AjetutVuorot.HloNro " & _
                "INNER JOIN Kalusto ON Kalusto.AutoNro=AjetutVuorot.AutoNro " & _
                "WHERE AlkuPVM = @PVM ORDER BY AjetutVuorot.TVLyhenne ASC"
            .SelectCommand.Parameters.AddWithValue("@PVM", dAl)


        End With
        Dim myDataSet As DataSet = New DataSet()

        tbAdapter.Fill(myDataSet, "AjetutVuorot")

        Dim myDataView = New DataView(myDataSet.Tables("AjetutVuorot"))
        DGW.AutoGenerateColumns = True
        DGW.DataSource = myDataSet
        DGW.DataMember = "AjetutVuorot"
        MyYhteys.Close()
        DGW.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGW.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        DGW.Columns(0).Width = 100
        DGW.Columns(0).HeaderText = "Vuoro"
        DGW.Columns(1).Width = 100
        DGW.Columns(1).HeaderText = "Auto"
        DGW.Columns(2).Width = 250
        DGW.Columns(2).HeaderText = "Kuljettaja"

        '     DGW.Columns(3).Width = 180
        '    DGW.Columns(3).HeaderText = "Etunimi"




    End Sub
    Private Sub DGW2_RowHeaderMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGW.RowHeaderMouseClick

        MyYhteys.Close()

        Dim i As Integer
        i = DGW.CurrentRow.Index
        Dim dAl As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")
        ValittuRivi.Text = i.ToString

        Dim TVll As String = DGW.Item(0, i).Value.ToString
        Dim kuli As Integer = PalautaSQLNroNimiesta(DGW.Item(2, i).Value.ToString)

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = MyYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT AjetutVuorot.TVLyhenne, Henkilosto.SukuNimi, Henkilosto.EtuNimi, Kalusto.RekNro, TyoVuorot.TVAlkaa, TyoVuorot.TVLoppuu, AjetutVuorot.AlkuKLO, AjetutVuorot.loppuKLO, AjetutVuorot.RiviID FROM AjetutVuorot " & _
            "INNER JOIN Henkilosto ON AjetutVuorot.HloNro=Henkilosto.HloNro " & _
            "INNER JOIN Kalusto ON AjetutVuorot.AutoNro=Kalusto.AutoNro " & _
            "INNER JOIN TyoVuorot ON AjetutVuorot.TVLyhenne=TyoVuorot.Lyhenne AND (@nyt BETWEEN TyoVuorot.AlkaenPVM AND TyoVuorot.AstiPVM) " & _
            "WHERE AjetutVuorot.AlkuPVM=@Nyt AND AjetutVuorot.HloNro=@kk AND AjetutVuorot.TVLyhenne=@id" ' AND Henkilosto.tSuhdeVoimassa='1' "

            .Parameters.AddWithValue("@id", TVll)
            .Parameters.AddWithValue("@nyt", dAl)
            .Parameters.AddWithValue("@kk", kuli)

        End With
        MyYhteys.Open()
        '     Try
        Dim rd As MySqlDataReader = cmd.ExecuteReader

        If rd.HasRows = True Then
            rd.Read()
            TVnLyhenne.Text = TVll
            TVAlkuaika.Text = Microsoft.VisualBasic.Left(rd.GetValue(4).ToString, 5)
            TVloppuaika.Text = Microsoft.VisualBasic.Left(rd.GetValue(5).ToString, 5)
            cbKuljettaja.Text = DGW.Item(2, i).Value.ToString

            tvAloitettu.Text = Microsoft.VisualBasic.Left(rd.GetValue(6).ToString, 5)
            tvLopetettu.Text = Microsoft.VisualBasic.Left(rd.GetValue(7).ToString, 5)
            cbAuto.Text = rd.GetString(3)
            riviNumero.Text = rd.GetValue(8).ToString

        End If

        ' Catch ex As Exception
        'Err.Clear()
        ' End Try
        indexAuto.Text = cbAuto.SelectedIndex.ToString
        indexKuljettaja.Text = cbKuljettaja.SelectedIndex.ToString
        btTallenna.Visible = False
        MyYhteys.Close()
        '      SC.Visible = True
        '     DGW.Enabled = False


    End Sub
    Public Sub lataaAutoCBhen()
        myYhteys.Close()
        cbAuto.Items.Clear()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT RekNro FROM Kalusto WHERE Raporteissa='1' ORDER BY RekNro ASC"
            '           .Parameters.AddWithValue("@e", "E")
            '        .Parameters.AddWithValue("@j", "J")
            '

        End With
        '   Try
        myYhteys.Open()

        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then
            While rd.Read
                cbAuto.Items.Add(rd.GetValue(0))
            End While
        End If
        '  Catch ex As Exception
        'Err.Clear()

        '  End Try
        myYhteys.Close()
    End Sub
    Private Sub LahtoLista_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Viikonpaiva.Text = PalautaViikonPaiva(PVM.Value)
        LataaKuljettajatCBhen()
        lataaAutoCBhen()
        HaeViimeisimmätlomailmoiotukset()
        If KayttajanTaso = "H" Then
            esimiesToimet.Visible = False
        Else
            esimiesToimet.Visible = True

        End If
    End Sub

    Private Sub TVAlkuaika_TextChanged(sender As System.Object, e As System.EventArgs) Handles TVAlkuaika.TextChanged
        If TVAlkuaika.Text = "00:00" Then TVAlkuaika.Text = ""
    End Sub
    Private Sub TVloppuaika_TextChanged(sender As System.Object, e As System.EventArgs) Handles TVloppuaika.TextChanged
        If TVloppuaika.Text = "00:00" Then TVloppuaika.Text = ""
    End Sub

    Private Sub tvAloitettu_TextChanged(sender As System.Object, e As System.EventArgs) Handles tvAloitettu.TextChanged
        If tvAloitettu.Text = "00:00" Then tvAloitettu.Text = ""
    End Sub

    Private Sub tvLopetettu_TextChanged(sender As System.Object, e As System.EventArgs) Handles tvLopetettu.TextChanged
        If tvLopetettu.Text = "00:00" Then tvLopetettu.Text = ""
    End Sub
    Public Sub LataaKuljettajatCBhen()
        myYhteys.Close()
        cbKuljettaja.Items.Clear()


        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT SukuNimi, EtuNimi FROM Henkilosto WHERE tSuhdeVoimassa=0 ORDER BY SukuNimi ASC"
            '  .Parameters.AddWithValue("@id", False)

        End With
        Try
            MyYhteys.Open()

            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                While rd.Read
                    cbKuljettaja.Items.Add(rd.GetValue(0) & " " & rd.GetValue(1))
                    lomaKuljettaja.Items.Add(rd.GetValue(0) & " " & rd.GetValue(1))

                End While
            End If
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE lahtolista kuljettajien latuas cbhen", ErrorToString, 0, 0)

            Err.Clear()

        End Try
        MyYhteys.Close()

    End Sub

    Private Sub cbKuljettaja_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbKuljettaja.SelectedIndexChanged
        txtPunNro.Text = PalautaNimestaPuhelinNumero(cbKuljettaja.Text, Today)
        If TVnLyhenne.Text = "" Then btTallenna.Visible = False : Exit Sub

        If cbKuljettaja.SelectedIndex <> Val(indexKuljettaja.Text) Or cbAuto.SelectedIndex <> Val(indexAuto.Text) Then
            btTallenna.Visible = True
        Else
            btTallenna.Visible = False

        End If


    End Sub

    Private Sub cbAuto_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbAuto.SelectedIndexChanged
        AutonNumero.Text = PalautaSQLNumeroRekkarista(cbAuto.Text)
        If TVnLyhenne.Text = "" Then btTallenna.Visible = False : Exit Sub

        If cbKuljettaja.SelectedIndex <> Val(indexKuljettaja.Text) Or cbAuto.SelectedIndex <> Val(indexAuto.Text) Then
            btTallenna.Visible = True
        Else
            btTallenna.Visible = False

        End If
        OletusAuto = cbAuto.Text

    End Sub

    Private Sub btTallenna_Click(sender As System.Object, e As System.EventArgs) Handles btTallenna.Click
        MyYhteys.Close()


        Dim cmd As New MySqlCommand()
        LokiTapahtumanTallennus(KayttajaHloNro, "Tallennettu muutos työvuoroon " & TVnLyhenne.Text & " / " & cbKuljettaja.Text & " / " & cbAuto.Text, PVM.Value.ToString, PalautaSQLNroNimiesta(cbKuljettaja.Text), PalautaSQLNumeroRekkarista(cbAuto.Text))

        With cmd
            .Connection = MyYhteys
            .CommandType = CommandType.Text
            .CommandText = "UPDATE AjetutVuorot SET HloNro=@HloNro, AutoNro=@AutoNro, EdAuto=@EdAuto, EdHlo=@EdHlo WHERE RiviID=@id"
            .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(cbKuljettaja.Text))
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbAuto.Text))
            If Val(indexAuto.Text) <> cbAuto.SelectedIndex Then .Parameters.AddWithValue("@EdAuto", PalautaSQLNumeroRekkarista(cbAuto.Items.Item(indexAuto.Text).ToString())) Else .Parameters.AddWithValue("@EdAuto", "0")
            If Val(indexKuljettaja.Text) <> cbKuljettaja.SelectedIndex Then .Parameters.AddWithValue("@EdHlo", PalautaSQLNroNimiesta(cbKuljettaja.Items.Item(indexKuljettaja.Text).ToString())) Else .Parameters.AddWithValue("@EdHlo", "0")


            .Parameters.AddWithValue("id", Val(riviNumero.Text))
        End With
        MyYhteys.Open()
        '   Try
        cmd.ExecuteNonQuery()

        '  Catch ex As Exception
        '      Err.Clear()
        '      MsgBox("Tallentaminen ei onnistunut")
        '     Exit Sub

        '  End Try
        MyYhteys.Close()

        '****************Tarkistetaan kuljettaja onko merkitty toiseen työvuoroon
        If cbKuljettaja.Text <> "" Or PalautaSQLNroNimiesta(cbKuljettaja.Text) <> 0 Then
            Dim dAl As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")

            Dim cmd2 As New MySqlCommand()
            cmd2.Connection = MyYhteys
            cmd2.CommandType = CommandType.Text
            cmd2.CommandText = "SELECT RiviID, TVLyhenne FROM AjetutVuorot WHERE AlkuPVM=@AlkuPVM AND TVLyhenne<>@Lyhenne AND HloNro=@kuli"
            '     cmd2.Parameters.AddWithValue("@AlkuPVM", FormatDateTime(DAL2, DateFormat.ShortDate))
            cmd2.Parameters.AddWithValue("@Lyhenne", TVnLyhenne.Text)
            cmd2.Parameters.AddWithValue("@kuli", PalautaSQLNroNimiesta(cbKuljettaja.Text))
            cmd2.Parameters.AddWithValue("@AlkuPVM", dAl)

            MyYhteys.Open()

            Dim rd As MySqlDataReader = cmd2.ExecuteReader
            If PalautaSQLNroNimiesta(cbKuljettaja.Text) <> 0 And rd.HasRows = True Then
                rd.Read()
                Dim kysymys As MsgBoxResult = Nothing
                Main.TopMost = False

                kysymys = MsgBox("Kuljettaja on merkitty työvuoroon " & rd.GetString(1) & ", poistetaanko kuljettaja tästä työvuorosta", vbYesNo)
                Main.TopMost = True

                If kysymys = MsgBoxResult.Yes Then
                    LokiTapahtumanTallennus(KayttajaHloNro, "Lahtolista: Poistettu työvuorosta " & rd.GetString(1) & " kuljettaja " & cbKuljettaja.Text, "", PalautaSQLNroNimiesta(cbKuljettaja.Text), 0)

                    MyYhteys2.Close()
                    Dim cmdU As New MySqlCommand()
                    cmdU.Connection = MyYhteys2
                    cmdU.CommandType = CommandType.Text
                    cmdU.CommandText = "UPDATE AjetutVuorot SET HloNro = @HloNro WHERE RiviID = @TVid" ' AND HloNro = @VanhaKuski"
                    '  cmdU.Parameters.AddWithValue("@AlkuPVM", FormatDateTime(DAL2, DateFormat.ShortDate))

                    cmdU.Parameters.AddWithValue("@TVid", rd.GetValue(0))
                    '   cmdU.Parameters.AddWithValue("@VanhaKuski", PalautaSQLNroNimiesta(UUSIKULI.Text))
                    cmdU.Parameters.AddWithValue("@HloNro", 0)
                    MyYhteys2.Open()
                    cmdU.ExecuteNonQuery()
                    MyYhteys2.Close()




                End If


            End If

            MyYhteys2.Close()
        End If


        '***************
        'tarkistetaan aoko auto merkitty toiseen työvuoroon
        '****************Tarkistetaan kuljettaja onko merkitty toiseen työvuoroon
        If cbAuto.Text <> "" And Val(indexAuto.Text) <> cbAuto.SelectedIndex Then
            Dim dAl As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")
            MyYhteys.Close()

            Dim cmd2 As New MySqlCommand()
            cmd2.Connection = MyYhteys
            cmd2.CommandType = CommandType.Text
            cmd2.CommandText = "SELECT RiviID, TVLyhenne FROM AjetutVuorot WHERE AlkuPVM=@AlkuPVM AND TVLyhenne<>@Lyhenne AND AutoNro=@kuli"
            '     cmd2.Parameters.AddWithValue("@AlkuPVM", FormatDateTime(DAL2, DateFormat.ShortDate))
            cmd2.Parameters.AddWithValue("@Lyhenne", TVnLyhenne.Text)
            cmd2.Parameters.AddWithValue("@kuli", PalautaSQLNumeroRekkarista(cbAuto.Text))
            cmd2.Parameters.AddWithValue("@AlkuPVM", dAl)

            MyYhteys.Open()

            Dim rd As MySqlDataReader = cmd2.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                Dim kysymys As MsgBoxResult = Nothing
                Main.TopMost = False

                kysymys = MsgBox("Auto on merkitty työvuoroon " & rd.GetString(1) & ", poistetaanko auto tästä työvuorosta", vbYesNo)
                Main.TopMost = True
                If kysymys = MsgBoxResult.Yes Then
                    LokiTapahtumanTallennus(KayttajaHloNro, "Lahtolista: Poistettu työvuorosta " & rd.GetString(1) & " auto " & cbAuto.Text, "", 0, PalautaSQLNumeroRekkarista(cbAuto.Text))

                    MyYhteys2.Close()
                    Dim cmdU As New MySqlCommand()
                    cmdU.Connection = MyYhteys2
                    cmdU.CommandType = CommandType.Text
                    cmdU.CommandText = "UPDATE AjetutVuorot SET AutoNro = @AutoNro WHERE RiviID = @TVid" ' AND HloNro = @VanhaKuski"
                    '  cmdU.Parameters.AddWithValue("@AlkuPVM", FormatDateTime(DAL2, DateFormat.ShortDate))

                    cmdU.Parameters.AddWithValue("@TVid", rd.GetValue(0))
                    '   cmdU.Parameters.AddWithValue("@VanhaKuski", PalautaSQLNroNimiesta(UUSIKULI.Text))
                    cmdU.Parameters.AddWithValue("@AutoNro", 0)
                    MyYhteys2.Open()
                    cmdU.ExecuteNonQuery()
                    MyYhteys2.Close()




                End If


            End If

            MyYhteys2.Close()
        End If
        '*************




        LataaTunnuksetDataKenttaan()
        '       DGW.Enabled = True
        TVAlkuaika.Text = ""
        tvAloitettu.Text = ""
        tvLopetettu.Text = ""
        TVloppuaika.Text = ""
        cbAuto.Text = ""
        cbKuljettaja.Text = ""
        TVnLyhenne.Text = ""
        txtPunNro.Text = ""
        AutonNumero.Text = ""

        btTallenna.Visible = False
        DGW.Rows(Val(ValittuRivi.Text)).Selected = True
        TallennaTietokantaToiminto(1)


    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        PVM.Value = PVM.Value.AddDays(-1)

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        PVM.Value = PVM.Value.AddDays(1)

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        cbKuljettaja.Text = ""
        cbKuljettaja.SelectedIndex = 0

    End Sub


    Private Sub TVnLyhenne_TextChanged(sender As System.Object, e As System.EventArgs) Handles TVnLyhenne.TextChanged
        If TVnLyhenne.Text = "" Then
            cbKuljettaja.Visible = False
            cbAuto.Visible = False
            Button3.Visible = False
        Else
            cbKuljettaja.Visible = True
            cbAuto.Visible = True
            Button3.Visible = True

        End If
    End Sub

    Private Sub rbSaikku_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbSaikku.CheckedChanged
        If rbSaikku.Checked = True Or rbMuuvapaa.Checked = True Or rbKesaLoma.Checked = True Or rbTalviloma.Checked = True Then
            Button4.Visible = True

        Else
            Button4.Visible = False

        End If
        '    lomaLoppu.Value = lomaAlku.Value

    End Sub

    Private Sub rbKesaLoma_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbKesaLoma.CheckedChanged
        '    lomaLoppu.Value = lomaAlku.Value.AddDays(26)
        If rbSaikku.Checked = True Or rbMuuvapaa.Checked = True Or rbKesaLoma.Checked = True Or rbTalviloma.Checked = True Then
            Button4.Visible = True

        Else
            Button4.Visible = False

        End If

    End Sub

    Private Sub rbTalviloma_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbTalviloma.CheckedChanged
        '   lomaLoppu.Value = lomaAlku.Value.AddDays(5)
        If rbSaikku.Checked = True Or rbMuuvapaa.Checked = True Or rbKesaLoma.Checked = True Or rbTalviloma.Checked = True Then
            Button4.Visible = True

        Else
            Button4.Visible = False

        End If

    End Sub

    Private Sub rbMuuvapaa_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbMuuvapaa.CheckedChanged
        '     lomaLoppu.Value = lomaAlku.Value
        If rbSaikku.Checked = True Or rbMuuvapaa.Checked = True Or rbKesaLoma.Checked = True Or rbTalviloma.Checked = True Then
            Button4.Visible = True

        Else
            Button4.Visible = False

        End If

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Main.TopMost = False

        If lomaKuljettaja.Text = "" Then MsgBox("Valitse kuljettaja") : Exit Sub
        Main.TopMost = True

        Dim tyyppi As String = ""
        If rbKesaLoma.Checked = True Then tyyppi = " kesäloma "
        If rbSaikku.Checked = True Then tyyppi = " saikku "
        If rbMuuvapaa.Checked = True Then tyyppi = " vapaa "
        If rbTalviloma.Checked = True Then tyyppi = " talviloma "

        LokiTapahtumanTallennus(KayttajaHloNro, "Lähtölista: Lomatallennus" & tyyppi & lomaKuljettaja.Text, "ajalle " & lomaAlku.Value.ToString & "-" & lomaLoppu.Value.ToString, PalautaSQLNroNimiesta(lomaKuljettaja.Text), 0)

        Try
            MyYhteys.Close()
            Dim cmd As New MySqlCommand
            With cmd
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Lomat (HloNro, AlkaenPVM, AstiPVM, LomaTyyppi, Lisaaja, Lisatty) VALUES(@HloNro, @AlkaenPVM, @AstiPVM, @LomaTyyppi, @Lisaaja, @Lisatty) "
                .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(lomaKuljettaja.Text))
                .Parameters.AddWithValue("@AlkaenPVM", lomaAlku.Value)
                .Parameters.AddWithValue("@AstiPVM", lomaLoppu.Value)
                If rbKesaLoma.Checked = True Then .Parameters.AddWithValue("@LomaTyyppi", "3")
                If rbSaikku.Checked = True Then .Parameters.AddWithValue("@LomaTyyppi", "1")
                If rbMuuvapaa.Checked = True Then .Parameters.AddWithValue("@LomaTyyppi", "2")
                If rbTalviloma.Checked = True Then .Parameters.AddWithValue("@LomaTyyppi", "4")
                .Parameters.AddWithValue("@Lisaaja", KayttajaHloNro)
                .Parameters.AddWithValue("@Lisatty", Now)

            End With
            MyYhteys.Open()
            cmd.ExecuteNonQuery()
            MyYhteys.Close()


            ' SIIRRETÄÄN TYÖVUOOROT KULJETTAJALTA POIS (--> EdHlo:ksi)
            Dim dAl1 As Date = Format(lomaAlku.Value, "\#yyyy\-MM\-dd\#")
            Dim dAl2 As Date = Format(lomaLoppu.Value, "\#yyyy\-MM\-dd\#")

            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "UPDATE AjetutVuorot SET HloNro=@uusi, EdHlo=@EdHlo WHERE HloNro=@HloNro AND (AlkuPVM BETWEEN @alku AND @loppu)"
                .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(lomaKuljettaja.Text))
                .Parameters.AddWithValue("@uusi", "0")
                .Parameters.AddWithValue("@alku", dal1)
                .Parameters.AddWithValue("@loppu", dal2)
                .Parameters.AddWithValue("@EdHlo", PalautaSQLNroNimiesta(lomaKuljettaja.Text))
            End With
            MyYhteys.Open()
            '   Try
            cmd2.ExecuteNonQuery()

            '  Catch ex As Exception
            '      Err.Clear()
            '      MsgBox("Tallentaminen ei onnistunut")
            '     Exit Sub

            '  End Try
            MyYhteys.Close()












        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE lomatallennus", ErrorToString, 0, 0)
            Err.Clear()
            Main.TopMost = False

            MsgBox("Virhe tallentamisessa")
            Main.TopMost = True

            Exit Sub

        Finally
            Main.TopMost = False

            MsgBox("Tallennettu")
            lomaKuljettaja.Text = ""
            Main.TopMost = True

        End Try
        Button4.Visible = False
        HaeViimeisimmätlomailmoiotukset()

    End Sub

    Private Sub lomaKuljettaja_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lomaKuljettaja.SelectedIndexChanged
        '     If lomaKuljettaja.Text = "" Then Button4.Visible = False

    End Sub

    Private Sub lomaAlku_ValueChanged(sender As System.Object, e As System.EventArgs) Handles lomaAlku.ValueChanged
        If rbSaikku.Checked = True Or rbMuuvapaa.Checked = True Or rbKesaLoma.Checked = True Or rbTalviloma.Checked = True Then
            Button4.Visible = True

        Else
            Button4.Visible = False

        End If

    End Sub

    Private Sub lomaLoppu_ValueChanged(sender As System.Object, e As System.EventArgs) Handles lomaLoppu.ValueChanged
        If rbSaikku.Checked = True Or rbMuuvapaa.Checked = True Or rbKesaLoma.Checked = True Or rbTalviloma.Checked = True Then
            Button4.Visible = True

        Else
            Button4.Visible = False

        End If

    End Sub
    Public Sub HaeViimeisimmätlomailmoiotukset()

        Try
            MyYhteys.Close()
            MyYhteys.Open()

            Dim tbAdapter As New MySqlDataAdapter()

            With tbAdapter
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = MyYhteys
                .SelectCommand.CommandText = "SELECT Lomat.ID, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Kuljettaja, Lomat.AlkaenPVM, Lomat.AstiPVM, LomaTyypit.Nimike, Lomat.Lisatty FROM Lomat " & _
             "INNER JOIN Henkilosto ON Lomat.HloNro=Henkilosto.HloNro " & _
             "INNER JOIN LomaTyypit ON Lomat.LomaTyyppi=LomaTyypit.LomaTyyppi " & _
             "ORDER BY Lomat.Lisatty DESC LIMIT 10"


            End With
            Dim myDataSet As DataSet = New DataSet()

            tbAdapter.Fill(myDataSet, "Lomat")

            Dim myDataView = New DataView(myDataSet.Tables("Lomat"))
            DGW2.AutoGenerateColumns = True
            DGW2.DataSource = myDataSet
            DGW2.DataMember = "Lomat"
            MyYhteys.Close()

            DGW2.AutoResizeColumns()


        Catch ex As Exception
            Err.Clear()

        End Try


    End Sub
    Private Sub DGW_RowHeaderMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGW2.RowHeaderMouseClick

        MyYhteys.Close()

        Dim i As Integer
        i = DGW2.CurrentRow.Index

        Dim ID As String = DGW2.Item(0, i).Value.ToString

        Main.TopMost = False

        Dim a As MsgBoxResult = MsgBox("POISTETAANKO RIVI?", vbYesNo)
        Main.TopMost = True

        If a = MsgBoxResult.Yes Then
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "DELETE FROM Lomat WHERE ID=@id" ' AND Henkilosto.tSuhdeVoimassa='1' "
                .Parameters.AddWithValue("@id", ID)


            End With
            MyYhteys.Open()
            cmd.ExecuteNonQuery()
            MyYhteys.Close()


        End If

        HaeViimeisimmätlomailmoiotukset()


    End Sub

    Private Sub DGW_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGW.CellContentClick

    End Sub
End Class