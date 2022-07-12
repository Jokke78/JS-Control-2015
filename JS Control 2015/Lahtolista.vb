Imports MySql.Data.MySqlClient

Public Class Lahtolista
    Private myYhteys As MySqlConnection = New MySqlConnection(serverString)
    Private myYhteys2 As MySqlConnection = New MySqlConnection(serverString)
    Private connection As MySqlConnection = New MySqlConnection(serverString)
    Private myYhteys3 As MySqlConnection = New MySqlConnection(serverString)

    Private Sub Lahtolista_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LataaTunnuksetDataKenttaan()
        varjaaOsastonPomo(4)
        LataaKuljettajatCBhen()
        lataaAutoCBhen()

        Viikonpaiva.Text = PalautaViikonPaiva(PVM.Value)

        If Kayttaja.Lahtolista_Osastojako = False Then
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            Button17.Enabled = False

        Else
            Button4.Enabled = True
            Button5.Enabled = True
            Button6.Enabled = True
            Button17.Enabled = True

        End If
        SiirraPaneelitSivuun()
        PanelLahtolista.Size = New Size(421, 611)
        PanelLahtolista.Location = New Point(42, 86)
        PanelLahtolista.Visible = True
        ' Infot.Visible = True


        If Kayttaja.HloNro = 154 Then
            cbKuljettaja.BackColor = Color.Pink
            cbAuto.BackColor = Color.Pink
        Else
            cbKuljettaja.BackColor = Color.White
            cbAuto.BackColor = Color.White


        End If
    End Sub
    Public Sub lataaAutoCBhen()
        myYhteys.Close()
        cbAuto.Items.Clear()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT RekNro FROM Kalusto WHERE Raporteissa='1' AND KTila=0 ORDER BY RekNro ASC"
            '           .Parameters.AddWithValue("@e", "E")
            '        .Parameters.AddWithValue("@j", "J")
            '

        End With
        Try
            myYhteys.Open()

            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                While rd.Read
                    cbAuto.Items.Add(rd.GetValue(0))
                End While
            End If
        Catch ex As Exception
            Err.Clear()

        Finally
            myYhteys.Close()
        End Try

    End Sub
    Private Sub PVM_ValueChanged(sender As System.Object, e As System.EventArgs) Handles PVM.ValueChanged
        Viikonpaiva.Text = PalautaViikonPaiva(PVM.Value)

        If Button4.BackColor = Color.Lime Then
            varjaaOsastonPomo(1)
            LataaTunnuksetDataKenttaanPOMO(120)

        End If
        If Button5.BackColor = Color.Lime Then
            varjaaOsastonPomo(2)
            LataaTunnuksetDataKenttaanPOMO(150)

        End If
        If Button6.BackColor = Color.Lime Then
            varjaaOsastonPomo(3)
            LataaTunnuksetDataKenttaanPOMO(314)

        End If
        If Button7.BackColor = Color.Lime Then
            varjaaOsastonPomo(4)
            LataaTunnuksetDataKenttaan()
        End If

        If Button17.BackColor = Color.Lime Then
            varjaaOsastonPomo(5)
            LataaTunnuksetDataKenttaanPOMO(121)
        End If


        If FormatDateTime(PVM.Value, DateFormat.ShortDate).ToString <> FormatDateTime(Today, DateFormat.ShortDate).ToString Then
            Button3.BackgroundImage = Nothing

            Button3.BackColor = Color.Red
        Else
            Button3.BackColor = Color.Transparent

        End If
        Panel_TVhistoria.Visible = False

        If PanelLahtolista.Visible = True Then Exit Sub
        SiirraPaneelitSivuun()
        PanelLahtolista.Size = New Size(421, 611)
        PanelLahtolista.Location = New Point(42, 86)
        PanelLahtolista.Visible = True
        varjaaPaneeli(1)

        Me.Cursor = Cursors.Default


    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Cursor = Cursors.WaitCursor

        PVM.Value = PVM.Value.AddDays(-1)

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Me.Cursor = Cursors.WaitCursor

        PVM.Value = PVM.Value.AddDays(+1)

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Me.Cursor = Cursors.WaitCursor

        PVM.Value = Today


    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        LataaTunnuksetDataKenttaan()
        varjaaOsastonPomo(4)
        SiirraPaneelitSivuun()
        PanelLahtolista.Size = New Size(421, 611)
        PanelLahtolista.Location = New Point(42, 86)
        PanelLahtolista.Visible = True

    End Sub





    Public Sub LataaTunnuksetDataKenttaan()
        '      LokiTapahtumanTallennus(Kayttaja.HloNro, "Ladattu lähtölistan muokkaus", PVM.Value.ToString, 0, 0)
        PanelTyoVuoronTiedot.Visible = False
        Me.Cursor = Cursors.WaitCursor

        TyhjenneTVTiedot()
        myYhteys.Close()
        myYhteys.Open()
        Try
            Dim tbAdapter As New MySqlDataAdapter()
            Dim dAl As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")

            With tbAdapter
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
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
            DGW.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGW.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            DGW.Columns(0).Width = 70
            DGW.Columns(0).HeaderText = "Vuoro"
            DGW.Columns(1).Width = 80
            DGW.Columns(1).HeaderText = "Auto"
            DGW.Columns(2).Width = 241
            DGW.Columns(2).HeaderText = "Kuljettaja"

        Catch ex As Exception
            Err.Clear()

        Finally
            myYhteys.Close()

        End Try

        '     DGW.Columns(3).Width = 180
        '    DGW.Columns(3).HeaderText = "Etunimi"


        LataaKuljettajatCBhen()
        lataaAutoCBhen()

        Me.Cursor = Cursors.Default

    End Sub
    Public Sub LataaTunnuksetDataKenttaanPOMO(ByVal pomo As Integer)
        '      LokiTapahtumanTallennus(Kayttaja.HloNro, "Ladattu lähtölistan muokkaus", PVM.Value.ToString, 0, 0)
        TyhjenneTVTiedot()
        PanelTyoVuoronTiedot.Visible = False
        Me.Cursor = Cursors.WaitCursor
        Try
            myYhteys.Close()
            myYhteys.Open()

            Dim tbAdapter As New MySqlDataAdapter()
            Dim dAl As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")

            With tbAdapter
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
                .SelectCommand.CommandText = "SELECT DISTINCT AjetutVuorot.TVLyhenne, Kalusto.RekNro, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.Etunimi) AS Kuljettaja FROM AjetutVuorot " & _
                    "INNER JOIN Henkilosto ON Henkilosto.HloNro=AjetutVuorot.HloNro " & _
                    "INNER JOIN Kalusto ON Kalusto.AutoNro=AjetutVuorot.AutoNro " & _
                    "INNER JOIN TyoVuorot ON AjetutVuorot.TyoVuoroID=TyoVuorot.TyoVuoroID " & _
                    "WHERE AlkuPVM = @PVM AND TyoVuorot.Esimies=@boss " & _
                    "ORDER BY AjetutVuorot.TVLyhenne ASC"
                .SelectCommand.Parameters.AddWithValue("@PVM", dAl)
                .SelectCommand.Parameters.AddWithValue("@boss", pomo)


            End With
            Dim myDataSet As DataSet = New DataSet()

            tbAdapter.Fill(myDataSet, "AjetutVuorot")

            Dim myDataView = New DataView(myDataSet.Tables("AjetutVuorot"))
            DGW.AutoGenerateColumns = True
            DGW.DataSource = myDataSet
            DGW.DataMember = "AjetutVuorot"
            DGW.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGW.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            DGW.Columns(0).Width = 70
            DGW.Columns(0).HeaderText = "Vuoro"
            DGW.Columns(1).Width = 80
            DGW.Columns(1).HeaderText = "Auto"
            DGW.Columns(2).Width = 241
            DGW.Columns(2).HeaderText = "Kuljettaja"

        Catch ex As Exception
            Err.Clear()
        Finally
            myYhteys.Close()

        End Try

        '     DGW.Columns(3).Width = 180
        '    DGW.Columns(3).HeaderText = "Etunimi"

        LataaKuljettajatCBhen()
        Me.Cursor = Cursors.Default


    End Sub




    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        LataaTunnuksetDataKenttaanPOMO(150)
        varjaaOsastonPomo(2)
        SiirraPaneelitSivuun()
        PanelLahtolista.Size = New Size(421, 611)
        PanelLahtolista.Location = New Point(42, 86)
        PanelLahtolista.Visible = True

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        LataaTunnuksetDataKenttaanPOMO(120)
        varjaaOsastonPomo(1)
        SiirraPaneelitSivuun()
        PanelLahtolista.Size = New Size(421, 611)
        PanelLahtolista.Location = New Point(42, 86)
        PanelLahtolista.Visible = True

    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        LataaTunnuksetDataKenttaanPOMO(314)
        varjaaOsastonPomo(3)
        SiirraPaneelitSivuun()
        PanelLahtolista.Size = New Size(421, 611)
        PanelLahtolista.Location = New Point(42, 86)
        PanelLahtolista.Visible = True

    End Sub

    Public Sub varjaaOsastonPomo(ByVal nro As Integer)
        Button4.BackColor = Color.Pink
        Button5.BackColor = Color.Pink
        Button6.BackColor = Color.Pink
        Button7.BackColor = Color.Pink
        Button17.BackColor = Color.Pink

        Select Case nro
            Case 1
                Button4.BackColor = Color.Lime
            Case 2
                Button5.BackColor = Color.Lime
            Case 3
                Button6.BackColor = Color.Lime
            Case 4
                Button7.BackColor = Color.Lime
            Case 5
                Button17.BackColor = Color.Lime

        End Select

    End Sub
    Public Sub varjaaPaneeli(ByVal nro As Integer)
        Button11.BackColor = Color.Pink
        Button12.BackColor = Color.Pink
        Button13.BackColor = Color.Pink

        Select Case nro
            Case 1
                Button11.BackColor = Color.Lime
            Case 2
                Button12.BackColor = Color.Lime
            Case 3
                Button13.BackColor = Color.Lime

        End Select

    End Sub
    Public Sub TarkistaKirjautumisenErotus()
        If TVAlkuaika.Text <> "" And TValoitettu.Text <> "" And Kayttaja.Lahtolista_Kirjautumisero = True Then
            Dim alkaa As Date = CType(TVAlkuaika.Text, Date)
            Dim aloitettu As Date = CType(TValoitettu.Text, Date)
            Dim laskuri As TimeSpan = aloitettu - alkaa

            If laskuri.TotalMinutes < 0 Then
                ErotusAamu.Text = -1 * laskuri.TotalMinutes.ToString & " MIN ENNEN"
                ErotusAamu.ForeColor = Color.Lime

            Else

                ErotusAamu.Text = laskuri.TotalMinutes.ToString & " MIN MYÖHÄSSÄ"
                ErotusAamu.ForeColor = Color.IndianRed

            End If




        Else

            If Kayttaja.Lahtolista_Kirjautumisero = True Then
                ErotusAamu.Text = "PUUTTUU"
                ErotusAamu.ForeColor = Color.IndianRed
            Else
                ErotusAamu.Text = ""
                ErotusAamu.ForeColor = Color.IndianRed
            End If



        End If


        If TVLoppuaika.Text <> "" And TVlopetettu.Text <> "" And Kayttaja.Lahtolista_Kirjautumisero = True Then
            Dim alkaa As Date = CType(TVLoppuaika.Text, Date)
            Dim aloitettu As Date = CType(TVlopetettu.Text, Date)
            Dim laskuri As TimeSpan = aloitettu - alkaa

            If laskuri.TotalMinutes < 0 Then
                ErotusIlta.Text = -1 * laskuri.TotalMinutes.ToString & " MIN ENNEN"
                ErotusIlta.ForeColor = Color.IndianRed

            Else

                ErotusIlta.Text = laskuri.TotalMinutes.ToString & " MIN YLI"
                ErotusIlta.ForeColor = Color.Lime

            End If




        Else
            If Kayttaja.Lahtolista_Kirjautumisero = True Then
                ErotusIlta.Text = "PUUTTUU"
                ErotusIlta.ForeColor = Color.IndianRed
            Else
                ErotusIlta.Text = ""
                ErotusIlta.ForeColor = Color.IndianRed

            End If


        End If



    End Sub
    Public Sub TyhjenneTVTiedot()
        TVAlkuaika.Text = ""
        TVlopetettu.Text = ""
        TValoitettu.Text = ""
        TVLoppuaika.Text = ""
        TVnLyhenne.Text = ""
        ErotusAamu.Text = ""
        ErotusIlta.Text = ""
        cbAuto.Text = ""
        TVKuljettaja.Text = ""

    End Sub

    Private Sub DGW_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGW.CellContentClick

    End Sub

    Private Sub DGW_RowHeaderMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGW.RowHeaderMouseClick

        If Kayttaja.Lahtolista_OnkoKuljettajaaInformoitu = False Then
            btnTietaa.Visible = False
            TVKuljettaja.Location = New Point(135, 4)
        Else
            btnTietaa.Visible = True

        End If


        myYhteys.Close()
        PanelTyoVuoronTiedot.Visible = True
        GB_TehdytVikailmoitukset.Visible = False

        Dim i As Integer
        i = DGW.CurrentRow.Index
        Dim dAl As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")
        ValittuRivi.Text = i.ToString

        Dim TVll As String = DGW.Item(0, i).Value.ToString
        Dim kuli As Integer = PalautaSQLNroNimiesta(DGW.Item(2, i).Value.ToString)

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT AjetutVuorot.TVLyhenne, Henkilosto.SukuNimi, Henkilosto.EtuNimi, Kalusto.RekNro, TyoVuorot.TVAlkaa, TyoVuorot.TVLoppuu, AjetutVuorot.AlkuKLO, AjetutVuorot.loppuKLO, AjetutVuorot.RiviID, AjetutVuorot.Tietaa FROM AjetutVuorot " &
            "INNER JOIN Henkilosto ON AjetutVuorot.HloNro=Henkilosto.HloNro " &
            "INNER JOIN Kalusto ON AjetutVuorot.AutoNro=Kalusto.AutoNro " &
            "INNER JOIN TyoVuorot ON AjetutVuorot.TVLyhenne=TyoVuorot.Lyhenne AND (@nyt BETWEEN TyoVuorot.AlkaenPVM AND TyoVuorot.AstiPVM) " &
            "WHERE AjetutVuorot.AlkuPVM=@Nyt AND AjetutVuorot.HloNro=@kk AND AjetutVuorot.TVLyhenne=@id" ' AND Henkilosto.tSuhdeVoimassa='1' "

            .Parameters.AddWithValue("@id", TVll)
            .Parameters.AddWithValue("@nyt", dAl)
            .Parameters.AddWithValue("@kk", kuli)
            '          .Parameters.AddWithValue("@AikaON", dAl)

        End With
        myYhteys.Open()
        Try
            Dim rd As MySqlDataReader = cmd.ExecuteReader

            If rd.HasRows = True Then
                rd.Read()
                TVnLyhenne.Text = TVll
                TVAlkuaika.Text = Microsoft.VisualBasic.Left(rd.GetValue(4).ToString, 5)
                TVLoppuaika.Text = Microsoft.VisualBasic.Left(rd.GetValue(5).ToString, 5)
                TVKuljettaja.Text = DGW.Item(2, i).Value.ToString

                TValoitettu.Text = Microsoft.VisualBasic.Left(rd.GetValue(6).ToString, 5)
                TVlopetettu.Text = Microsoft.VisualBasic.Left(rd.GetValue(7).ToString, 5)
                TB1.Value = 0

                AjastaTB1()

                TarkistaKirjautumisenErotus()

                TVAUTO.Text = rd.GetString(3)
                rivinumero.Text = rd.GetValue(8).ToString
                TVAUTO.ForeColor = Color.Black
                Select Case rd.GetValue(9)
                    Case True
                        btnTietaa.Image = Image.FromHbitmap(My.Resources.accept.GetHbitmap())
                        ToolTip1.SetToolTip(Me.btnTietaa, "Kuljettaja tietää työvuoron")
                    Case False
                        btnTietaa.Image = Image.FromHbitmap(My.Resources._error.GetHbitmap())
                        ToolTip1.SetToolTip(Me.btnTietaa, "Kuljettaja EI ole tietoinen työvuorosta")

                End Select
                rd.Dispose()
                cmd.Dispose()
                If Kayttaja.lahtolista_AutojenVikailmoituksientarkastaminen = True Then
                    Dim cmd2 As New MySqlCommand()
                    With cmd2
                        .Connection = myYhteys
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT COUNT(VikaIlmoitukset.IlmoitettuVika) FROM VikaIlmoitukset " &
                            "JOIN Kalusto ON VikaIlmoitukset.AutoNro=Kalusto.AutoNro " &
                            "WHERE Kalusto.RekNro=@autorek AND VikaIlmoitukset.Korjattu='0'"
                        .Parameters.AddWithValue("@autorek", TVAUTO.Text)
                    End With

                    Dim maara As Integer = cmd2.ExecuteScalar

                    If maara > 0 Then
                        TVAUTO.ForeColor = Color.IndianRed
                    End If

                    cmd2.Dispose()

                End If



            End If

        Catch ex As Exception
            Err.Clear()
        Finally
            myYhteys.Close()

        End Try
        indexAuto.Text = cbAuto.SelectedIndex.ToString
        indexKuljettaja.Text = cbKuljettaja.SelectedIndex.ToString
        '      btTallenna.Visible = False
        myYhteys.Close()
        TallenneKuljettaja.Visible = False

        '      SC.Visible = True
        '     DGW.Enabled = False


        'LAITETAAN TV historia esille

        PanelTyoVuoronTiedot.Size = New Size(540, 228)
        If Kayttaja.Lahtolista_TVhistorianSeuranta = True Then
            Panel_TVhistoria.Location = New Point(470, 273)
            Panel_TVhistoria.Size = New Size(540, 212)
            Panel_TVhistoria.Visible = True

            Hae_TV_historia_esille()
        End If


    End Sub
    Public Sub Hae_TV_historia_esille()
        Try
            myYhteys.Close()
            myYhteys.Open()

            Dim tbAdapter2 As New MySqlDataAdapter()


            With tbAdapter2
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
                .SelectCommand.CommandText = "SELECT CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.Etunimi) AS Kuljettaja, Kalusto.RekNro, AjetutTapahtumat.PVMklo, AjetutSyyt.Selitys, AjetutTapahtumat.Ilmoittaja, AjetutTapahtumat.Tarkennus FROM AjetutTapahtumat " &
                    "INNER JOIN Henkilosto ON Henkilosto.HloNro=AjetutTapahtumat.HloNro " &
                    "INNER JOIN Kalusto ON Kalusto.AutoNro=AjetutTapahtumat.AutoNro " &
                    "INNER JOIN AjetutSyyt ON AjetutSyyt.ID=AjetutTapahtumat.SyyID " &
                    "WHERE AjetutTapahtumat.RiviID=@id ORDER BY AjetutTapahtumat.PVMklo DESC"
                .SelectCommand.Parameters.AddWithValue("@id", rivinumero.Text)


            End With
            Dim myDataSet2 As DataSet = New DataSet()

            tbAdapter2.Fill(myDataSet2, "AjetutTapahtumat")

            Dim myDataView = New DataView(myDataSet2.Tables("AjetutTapahtumat"))
            DGV_TVhistoria.AutoGenerateColumns = True
            DGV_TVhistoria.DataSource = myDataSet2
            DGV_TVhistoria.DataMember = "AjetutTapahtumat"
            'DGV_TVhistoria.AutoSizeColumnsMode = True


            '     DGW.Columns(3).Width = 180
            '    DGW.Columns(3).HeaderText = "Etunimi"



        Catch ex As Exception
            Err.Clear()
        Finally
            myYhteys.Close()

        End Try





    End Sub

    Private Sub TValoitettu_TextChanged(sender As System.Object, e As System.EventArgs) Handles TValoitettu.TextChanged
        If TValoitettu.Text = "00:00" Then TValoitettu.Text = ""

    End Sub

    Private Sub TVlopetettu_TextChanged(sender As System.Object, e As System.EventArgs) Handles TVlopetettu.TextChanged
        If TVlopetettu.Text = "00:00" Then TVlopetettu.Text = ""

    End Sub

    Public Sub LataaKuljettajatCBhen()
        Try
            myYhteys.Close()
            cbKuljettaja.Items.Clear()


            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT DISTINCT(CONCAT(H.SukuNimi, ' ', H.EtuNimi)) AS Nimi FROM Henkilosto H WHERE tSuhdeVoimassa=0 AND Ajokelpoinen=1 ORDER BY Nimi ASC"
                '            .CommandText = "SELECT CONCAT(SukuNimi, ' ', EtuNimi) FROM Henkilosto H WHERE tSuhdeVoimassa=0 ORDER BY SukuNimi ASC"

                '  .Parameters.AddWithValue("@id", False)

            End With
            '      Try
            myYhteys.Open()

            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                While rd.Read
                    cbKuljettaja.Items.Add(rd.GetString(0))
                    '                  lomaKuljettaja.Items.Add(rd.GetValue(0) & " " & rd.GetValue(1))

                End While
            End If
            '      Catch ex As Exception
            '           LokiTapahtumanTallennus(Kayttaja.HloNro, "VIRHE lahtolista kuljettajien latuas cbhen", ErrorToString, 0, 0)

            'Err.Clear()

            '    End Try
        Catch ex As Exception
            Err.Clear()
        Finally
            myYhteys.Close()

        End Try

        LataaLOMAKuljettajatCBhen()
        If KeyPath = True Then PoistaYlimaaraisetKuljettajat()







    End Sub
    Public Sub LataaLOMAKuljettajatCBhen()
        Try
            myYhteys.Close()
            lomaKuljettaja.Items.Clear()


            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT DISTINCT(CONCAT(H.SukuNimi, ' ', H.EtuNimi)) AS Nimi FROM Henkilosto H WHERE tSuhdeVoimassa=0 ORDER BY Nimi ASC"
                '            .CommandText = "SELECT CONCAT(SukuNimi, ' ', EtuNimi) FROM Henkilosto H WHERE tSuhdeVoimassa=0 ORDER BY SukuNimi ASC"

                '  .Parameters.AddWithValue("@id", False)

            End With
            '      Try
            myYhteys.Open()

            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                While rd.Read
                    '      cbKuljettaja.Items.Add(rd.GetString(0))
                    lomaKuljettaja.Items.Add(rd.GetString(0))

                End While
            End If
            '      Catch ex As Exception
            '           LokiTapahtumanTallennus(Kayttaja.HloNro, "VIRHE lahtolista kuljettajien latuas cbhen", ErrorToString, 0, 0)

            'Err.Clear()

            '    End Try
        Catch ex As Exception
            Err.Clear()
        Finally
            myYhteys.Close()

        End Try








    End Sub

    Public Sub PoistaYlimaaraisetKuljettajat()
        Try
            If Kayttaja.Lahtolista_PaallekkaisetKuljettajat = True Then
                myYhteys.Close()

                Dim dAl As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")

                Dim cmd As New MySqlCommand()
                With cmd
                    .Connection = myYhteys
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Nimi FROM AjetutVuorot " & _
                    "INNER JOIN Henkilosto ON AjetutVuorot.HloNro=Henkilosto.HloNro " & _
                    "WHERE AjetutVuorot.AlkuPVM=@Nyt " ' AND Henkilosto.tSuhdeVoimassa='1' "

                    .Parameters.AddWithValue("@nyt", dAl)

                End With
                myYhteys.Open()
                '     Try
                Dim rd As MySqlDataReader = cmd.ExecuteReader

                If rd.HasRows = True Then
                    While rd.Read
                        cbKuljettaja.Items.Remove(rd.GetString(0))
                    End While
                End If

                myYhteys.Close()


                poistalomalaiset()
            End If
        Catch ex As Exception
            Err.Clear()
        Finally
            myYhteys.Close()

        End Try


    End Sub
    Public Sub poistalomalaiset()
        Try
            myYhteys.Close()

            Dim dAl As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Nimi FROM Lomat " & _
                "INNER JOIN Henkilosto ON Henkilosto.HloNro=Lomat.HloNro " & _
                "WHERE (@Nyt BETWEEN AlkaenPVM AND AstiPVM) AND (LomaTyyppi BETWEEN 1 AND 4) " ' AND Henkilosto.tSuhdeVoimassa='1' "

                .Parameters.AddWithValue("@nyt", dAl)

            End With
            myYhteys.Open()
            '     Try
            Dim rd As MySqlDataReader = cmd.ExecuteReader

            If rd.HasRows = True Then
                While rd.Read
                    cbKuljettaja.Items.Remove(rd.GetString(0))
                End While
            End If
        Catch ex As Exception
            Err.Clear()
        Finally
            myYhteys.Close()

        End Try



    End Sub



    Private Sub TVAUTO_Click(sender As System.Object, e As System.EventArgs) Handles TVAUTO.Click
        If Kayttaja.lahtolista_AutojenVikailmoituksientarkastaminen = True Then
            If TVAUTO.ForeColor = Color.Red Then
                GB_TehdytVikailmoitukset.Visible = False
                myYhteys.Close()
                GB_TehdytVikailmoitukset.Visible = True

                'haetaan tehdyt vikailmoitukset

                Dim tbAdapter As New MySqlDataAdapter()
                With tbAdapter
                    .SelectCommand = New MySqlCommand
                    .SelectCommand.Connection = myYhteys
                    .SelectCommand.CommandType = CommandType.Text
                    .SelectCommand.CommandText = "SELECT VikaIlmoitukset.IlmoitettuVika As 'Ilmoitettu vika', VikaIlmoitukset.IlmPVM AS 'pvm', CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) As Ilmoittaja FROM VikaIlmoitukset " & _
                            "JOIN Kalusto ON VikaIlmoitukset.AutoNro=Kalusto.AutoNro " & _
                            "JOIN Henkilosto ON VikaIlmoitukset.HloNro=Henkilosto.HloNro " & _
                            "WHERE Kalusto.RekNro=@autorek AND VikaIlmoitukset.Korjattu='0' " & _
                            "ORDER BY VikaIlmoitukset.IlmPVM DESC"
                    .SelectCommand.Parameters.AddWithValue("@autorek", TVAUTO.Text)
                End With
                myYhteys.Open()

                Dim myDataSet As DataSet = New DataSet()

                tbAdapter.Fill(myDataSet, "VikaIlmoitukset")

                Dim myDataView = New DataView(myDataSet.Tables("VikaIlmoitukset"))
                DGW_Viat.AutoGenerateColumns = True
                DGW_Viat.DataSource = myDataSet
                DGW_Viat.DataMember = "VikaIlmoitukset"
                myYhteys.Close()

                DGW_Viat.Columns(0).Width = 400
                DGW_Viat.Columns(1).Width = 100
                DGW_Viat.Columns(2).Width = 250

                HaeTMliitetytViat()




            End If
        End If
        myYhteys.Close()

    End Sub
    Public Sub HaeTMliitetytViat()
        myYhteys.Close()
        'haetaan tehdyt vikailmoitukset

        Dim tbAdapter As New MySqlDataAdapter()
        With tbAdapter
            .SelectCommand = New MySqlCommand
            .SelectCommand.Connection = myYhteys
            .SelectCommand.CommandType = CommandType.Text
            .SelectCommand.CommandText = "SELECT VikaIlmoitukset.IlmoitettuVika As 'Ilmoitettu vika', Tyomaaraykset.LuontiPVM AS 'pvm', Tyomaaraykset.TMnro FROM VikaIlmoitukset " & _
                    "JOIN Kalusto ON VikaIlmoitukset.AutoNro=Kalusto.AutoNro " & _
                    "JOIN Tyomaaraykset ON VikaIlmoitukset.LiitettyTMnro=Tyomaaraykset.TMnro " & _
                    "WHERE Kalusto.RekNro=@autorek AND VikaIlmoitukset.Korjattu='0' AND Tyomaaraykset.Valmis='0'" & _
                    "ORDER BY pvm DESC"
            .SelectCommand.Parameters.AddWithValue("@autorek", TVAUTO.Text)
        End With
        myYhteys.Open()

        Dim myDataSet As DataSet = New DataSet()

        tbAdapter.Fill(myDataSet, "VikaIlmoitukset")

        Dim myDataView = New DataView(myDataSet.Tables("VikaIlmoitukset"))
        DGW_TMliitetytviat.AutoGenerateColumns = True
        DGW_TMliitetytviat.DataSource = myDataSet
        DGW_TMliitetytviat.DataMember = "VikaIlmoitukset"
        myYhteys.Close()

        DGW_TMliitetytviat.Columns(0).Width = 400
        DGW_TMliitetytviat.Columns(1).Width = 100
        DGW_TMliitetytviat.Columns(2).Width = 100

        myYhteys.Close()


    End Sub


    Private Sub Button11_Click(sender As System.Object, e As System.EventArgs) Handles Button11.Click
        If PanelLahtolista.Visible = True Then Exit Sub
        SiirraPaneelitSivuun()
        PanelLahtolista.Size = New Size(421, 611)
        PanelLahtolista.Location = New Point(42, 86)
        PanelLahtolista.Visible = True
        varjaaPaneeli(1)


    End Sub

    Public Sub SiirraPaneelitSivuun()

        PanelLahtolista.Visible = False
        PanelLahtolista.Size = New Size(23, 38)
        PanelLahtolista.Location = New Point(12, 553)

        PanelTyoOhjae.Visible = False
        PanelTyoOhjae.Size = New Size(23, 38)
        PanelTyoOhjae.Location = New Point(12, 486)


    End Sub
    Private Sub ButtonXXX_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim clickedButton As Button
        If TVnLyhenne.Text = "" Then Exit Sub

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
                .Connection = connection
                .CommandType = CommandType.Text
                .CommandText = "SELECT Kuva, Sivu FROM TyoOhjeet WHERE TVnimi=@nimi AND (@pvm BETWEEN AlkaenPVM AND AstiPVM) AND Sivu=@Sivu"
                .Parameters.AddWithValue("@nimi", TVnLyhenne.Text)
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


        Catch ex As Exception
            Err.Clear()

        End Try


        myYhteys.Close()





    End Sub
    Private Sub Button12_Click(sender As System.Object, e As System.EventArgs) Handles Button12.Click
        If PanelTyoOhjae.Visible = True Then Exit Sub
        PictureBox1.Image = Nothing

        If TVnLyhenne.Text = "" Then Exit Sub
        SiirraPaneelitSivuun()
        PanelTyoOhjae.Size = New Size(421, 611)
        PanelTyoOhjae.Location = New Point(42, 86)
        PanelTyoOhjae.Visible = True
        varjaaPaneeli(2)


        If Kayttaja.lahtolista_TyoOhjeenNayttaminen = False Then Exit Sub

        myYhteys.Open()

        Dim dAl1 As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")



        Dim cmdmaara As New MySqlCommand()
        With cmdmaara
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT COUNT(*) FROM TyoOhjeet WHERE TVnimi=@nimi AND (@pvm BETWEEN AlkaenPVM AND AstiPVM)"
            .Parameters.AddWithValue("@nimi", TVnLyhenne.Text)
            .Parameters.AddWithValue("@pvm", dAl1)
        End With

        Dim maara As Integer = cmdmaara.ExecuteScalar

        If maara > 1 Then
            FLP1.Controls.Clear()
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
                .Connection = connection
                .CommandType = CommandType.Text
                .CommandText = "SELECT Kuva, Sivu FROM TyoOhjeet WHERE TVnimi=@nimi AND (@pvm BETWEEN AlkaenPVM AND AstiPVM) ORDER BY ID ASC"
                .Parameters.AddWithValue("@nimi", TVnLyhenne.Text)
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

        End Try


        myYhteys.Close()

    End Sub

    Public Sub LataaTyoOhjeEsille(ByVal Ohje As String)
        If TVnLyhenne.Text = "" Then Exit Sub


        Try
            Dim dAl As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = connection
                .CommandType = CommandType.Text
                .CommandText = "SELECT Kuva FROM TyoOhjeet WHERE TVnimi=@nro AND (@pvm BETWEEN AlkaenPVM and AstiPVM)"
                .Parameters.AddWithValue("@nro", TVnLyhenne.Text)
                .Parameters.AddWithValue("@pvm", dal)

            End With

            Dim adapter As New MySqlDataAdapter
            adapter.SelectCommand = cmd
            Dim Data As DataTable

            Data = New DataTable

            '        adapter(cmd, connection)


            Dim commandbuild As MySqlCommandBuilder = New MySqlCommandBuilder(adapter)
            adapter.Fill(Data)

            Dim lb() As Byte = Data.Rows(0).Item("Kuva")
            Using lstr As New System.IO.MemoryStream(lb)
                PictureBox1.Image = Image.FromStream(lstr)
                PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
                lstr.Close()
            End Using


        Catch ex As Exception
            Err.Clear()

        End Try




    End Sub

    Private Sub Button13_Click(sender As System.Object, e As System.EventArgs) Handles Button13.Click
        varjaaPaneeli(3)

    End Sub

   


    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs) Handles PictureBox1.Click

    End Sub

   


    Private Sub btTallenna_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub cbKuljettaja_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbKuljettaja.SelectedIndexChanged
        '     Label2.Text = PalautaNimestaPuhelinNumero(cbKuljettaja.Text, Today)
        If TVnLyhenne.Text = "" Then TallenneKuljettaja.Visible = False : Exit Sub

        If cbKuljettaja.SelectedIndex <> Val(indexKuljettaja.Text) Or cbAuto.SelectedIndex <> Val(indexAuto.Text) Then
            TallenneKuljettaja.Visible = True
        Else
            TallenneKuljettaja.Visible = False

        End If

    End Sub

    Private Sub Button15_Click(sender As System.Object, e As System.EventArgs) Handles Button15.Click
        TB1.Value = TB1.Value + 1

    End Sub


    Public Sub AjastaTB1()
        If TVAlkuaika.Text = "0:00" And TVLoppuaika.Text = "0:00" Then Exit Sub
        If TVAlkuaika.Text = "" And TVLoppuaika.Text = "" Then Exit Sub

        Dim alku As Date = CType(TVAlkuaika.Text, Date)
        Dim loppu As Date = CType(TVLoppuaika.Text, Date)
        If loppu < alku Then loppu = loppu.AddHours(24)

        Dim erotus As TimeSpan = loppu - alku
        Dim YHTminsat As Integer = erotus.TotalMinutes

        TB1.Maximum = YHTminsat

        A1.Text = TVAlkuaika.Text
        B2.Text = TVLoppuaika.Text





    End Sub

    Private Sub TB1_Scroll(sender As System.Object, e As System.EventArgs) Handles TB1.Scroll
        Dim alku As Date = CType(TVAlkuaika.Text, Date)
        Dim loppu As Date = CType(TVLoppuaika.Text, Date)
        If loppu < alku Then loppu = loppu.AddHours(24)


        Dim erotus As TimeSpan = loppu - alku
        Dim YHTminsat As Integer = erotus.TotalMinutes
        If TB1.Value = 0 Or TB1.Value = TB1.Maximum Then
            B1.Visible = False
            B2.Visible = False
            A2.Text = TVLoppuaika.Text
            Exit Sub

        End If
        B1.Visible = True
        B2.Visible = True

        A1.Text = TVAlkuaika.Text
        B2.Text = TVLoppuaika.Text

        Dim Analku As Date = alku.AddMinutes(TB1.Value)
        A2.Text = FormatDateTime(Analku, DateFormat.ShortTime).ToString
        B1.Text = A2.Text

    End Sub

    Private Sub TB1_ValueChanged(sender As Object, e As System.EventArgs) Handles TB1.ValueChanged
        If TB1.Value = 0 Then leftTB.Visible = False Else leftTB.Visible = True

        If TB1.Value = TB1.Maximum Then rightTB.Visible = False Else rightTB.Visible = True
        TB1_Scroll(Nothing, Nothing)


    End Sub

    Private Sub rightTB_Click(sender As System.Object, e As System.EventArgs) Handles rightTB.Click
        If (TB1.Value + 5) <= TB1.Maximum Then TB1.Value = TB1.Value + 5


    End Sub

    Private Sub leftTB_Click(sender As System.Object, e As System.EventArgs) Handles leftTB.Click
        If TB1.Value >= 5 Then TB1.Value = TB1.Value - 5

    End Sub

    Private Sub Button16_Click(sender As System.Object, e As System.EventArgs) Handles Button16.Click

    End Sub

    Private Sub B2_TextChanged(sender As System.Object, e As System.EventArgs) Handles B2.TextChanged

    End Sub

    Private Sub A2_TextChanged(sender As System.Object, e As System.EventArgs) Handles A2.TextChanged

    End Sub

    Private Sub A1_TextChanged(sender As System.Object, e As System.EventArgs) Handles A1.TextChanged

    End Sub

    Private Sub B1_TextChanged(sender As System.Object, e As System.EventArgs) Handles B1.TextChanged

    End Sub

    Private Sub Button22_Click(sender As System.Object, e As System.EventArgs) Handles Button22.Click
        If PanelTyoVuoronTiedot.Size.Height = 228 Then
            PanelTyoVuoronTiedot.Size = New Size(540, 489)
            If Kayttaja.Lahtolista_TVhistorianSeuranta = True Then

                Panel_TVhistoria.Visible = False
                Panel_TVhistoria.Location = New Point(125, 594)
                Panel_TVhistoria.Size = New Size(23, 33)
            End If

            Exit Sub
        End If
        If PanelTyoVuoronTiedot.Size.Height = 489 Then
            PanelTyoVuoronTiedot.Size = New Size(540, 228)
            'laitetaab tv historia esille
            If Kayttaja.Lahtolista_TVhistorianSeuranta = True Then

                Panel_TVhistoria.Location = New Point(470, 273)
                Panel_TVhistoria.Size = New Size(540, 212)
                Panel_TVhistoria.Visible = True
            End If

            Exit Sub
        End If

    End Sub

    Private Sub PictureBox1_DoubleClick(sender As Object, e As EventArgs) Handles PictureBox1.DoubleClick
        'Dim fm As New Form

        'With fm
        '    .Name = "fr"
        '    .Size = New Size(720, 1000)
        '    .Text = "TYÖOHJE " & TVnLyhenne.Text & " " & FormatDateTime(Now, DateFormat.LongDate).ToString
        '    .MinimizeBox = False
        '    .MaximizeBox = False
        '    .FormBorderStyle = FormBorderStyle.Fixed3D


        'End With

        'Dim pb As New PictureBox
        'pb.Image = PictureBox1.Image
        'pb.Location = New Point(0, 0)
        'pb.Dock = DockStyle.Fill


        'fm.Controls.Add(pb)

        'fm.Show()


    End Sub

    Private Sub TulostaTiedostoonToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim openfiledialog1 As New SaveFileDialog

        With openfiledialog1
            .CheckFileExists = True
            .Filter = "*.jgp|*.jg"
            .FilterIndex = 2
            If .ShowDialog = DialogResult.OK Then
                ' Load the specified file into a PictureBox control.
                '            PicCopy = Image.FromFile(.FileName)
                '           PictureBox1.Image = PicCopy
            End If
            .Dispose()
        End With
        If Me.PictureBox1.Image IsNot Nothing Then
            Me.PictureBox1.Image.Save(IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyPictures, "TestFile.jpg"))
        End If
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles PanelLomat.Paint

    End Sub

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        '  If PanelTyoVuoronTiedot.Visible = True Then Exit Sub
        '    SiirraPaneelitSivuun()
        '    PanelLahtolista.Size = New Size(421, 611)
        '    PanelLahtolista.Location = New Point(42, 86)
        '    PanelLahtolista.Visible = True
        '    varjaaPaneeli(1)
        '    panel_poissaolot.Visible = False
        '  Infot.Visible = True
        panel_poissaolot.Visible = False
        Button18.BackColor = Color.Lime
        Button21.BackColor = Color.Silver

    End Sub
    Public Sub SiirraPaneelitSivuunYla()

        PanelLomat.Visible = False
        PanelLomat.Size = New Size(1008, 509)
        PanelLomat.Location = New Point(25, 56)

        PanelTyoVuoronTiedot.Visible = False
        PanelTyoVuoronTiedot.Size = New Size(23, 38)
        PanelTyoVuoronTiedot.Location = New Point(1008, 120)


    End Sub

    Private Sub cbAuto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbAuto.SelectedIndexChanged
        '       Label9.Text = PalautaSQLNumeroRekkarista(cbAuto.Text)
        If TVnLyhenne.Text = "" Then TallenneKuljettaja.Visible = False : Exit Sub

        If cbKuljettaja.SelectedIndex <> Val(indexKuljettaja.Text) Or cbAuto.SelectedIndex <> Val(indexAuto.Text) Then
            TallenneKuljettaja.Visible = True
        Else
            TallenneKuljettaja.Visible = False

        End If

    End Sub

    Private Sub TVAUTO_TextChanged(sender As Object, e As EventArgs) Handles TVAUTO.TextChanged
        Label9.Text = PalautaSQLNumeroRekkarista(TVAUTO.Text)

    End Sub

    Private Sub TVKuljettaja_Click(sender As Object, e As EventArgs) Handles TVKuljettaja.Click

    End Sub

    Private Sub TVKuljettaja_TextChanged(sender As Object, e As EventArgs) Handles TVKuljettaja.TextChanged
        Label2.Text = PalautaNimestaPuhelinNumero(TVKuljettaja.Text, Today)

    End Sub

    Private Sub TallenneKuljettaja_Click(sender As Object, e As EventArgs) Handles TallenneKuljettaja.Click
        myYhteys.Close()
        '      My.Computer.Audio.Play("c:\temp\x.mp3")

        ' Tarkistetaan kuljettajan esimies ja jos kuuluu toiseen osastoon niin listään esimiehelle informaatio
        Dim kulinEsimies As Integer = palautakuljettajanEsimies(PalautaSQLNroNimiesta(cbKuljettaja.Text))
        If kulinEsimies <> Kayttaja.HloNro Then

            Dim cmdX As New MySqlCommand()
            With cmdX
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Informaatio (Teksti, OsoitettuHloNro, LisaajaHloNro, LisattyPVMklo) VALUES(@Teksti, @OsoitettuHloNro, @LisaajaHloNro, @LisattyPVMklo)"
                .Parameters.AddWithValue("@Teksti", cbKuljettaja.Text & " SIIRRETTY TYÖVUOROON " & TVnLyhenne.Text & " " & FormatDateTime(PVM.Value, DateFormat.ShortDate).ToString)
                .Parameters.AddWithValue("@OsoitettuHloNro", kulinEsimies)
                .Parameters.AddWithValue("@LisaajaHloNro", Kayttaja.HloNro)
                .Parameters.AddWithValue("@LisattyPVMklo", Now)

            End With
            myYhteys.Open()
            cmdX.ExecuteNonQuery()
            myYhteys.Close()


        End If


        Dim cmd As New MySqlCommand()
        '      LokiTapahtumanTallennus(Kayttaja.HloNro, "Tallennettu muutos työvuoroon " & TVnLyhenne.Text & " / " & cbKuljettaja.Text & " / " & cbAuto.Text, PVM.Value.ToString, PalautaSQLNroNimiesta(cbKuljettaja.Text), PalautaSQLNumeroRekkarista(cbAuto.Text))

        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "UPDATE AjetutVuorot SET HloNro=@HloNro, AutoNro=@AutoNro, EdAuto=@EdAuto, EdHlo=@EdHlo, Tietaa=@Tietaa WHERE RiviID=@id"
            '       .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(cbKuljettaja.Text))
            '     .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbAuto.Text))

            If cbAuto.Text = "" Then
                .Parameters.AddWithValue("@EdAuto", "0")
                .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(TVAUTO.Text))
                '  .Parameters.AddWithValue("@Tietaa", "1")

            Else
                .Parameters.AddWithValue("@EdAuto", PalautaSQLNumeroRekkarista(TVAUTO.Text))
                .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbAuto.Text))
                '      .Parameters.AddWithValue("@Tietaa", 1)

            End If


            If cbKuljettaja.Text = "" Then
                .Parameters.AddWithValue("@EdHlo", "0")
                .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(TVKuljettaja.Text))
                .Parameters.AddWithValue("@Tietaa", 1)

            Else
                .Parameters.AddWithValue("@EdHlo", PalautaSQLNroNimiesta(TVKuljettaja.Text))
                .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(cbKuljettaja.Text))
                .Parameters.AddWithValue("@Tietaa", 0)

            End If


            .Parameters.AddWithValue("id", Val(rivinumero.Text))
        End With
        myYhteys.Open()
        '   Try
        cmd.ExecuteNonQuery()

        ' Tehdään tallennus AjetutTapahtumat tauluun AUTO
        If cbAuto.Text <> "" Then
            Dim cmdTVtapahtuma4 As New MySqlCommand()
            With cmdTVtapahtuma4
                .Connection = myYhteys2
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO AjetutTapahtumat (RiviID, AutoNro, Ilmoittaja, PVMklo, SyyID, Tarkennus) " &
                            "VALUES(@RiviID, @AutoNro, @Ilmoittaja, @PVMklo, @SyyID, @Tarkennus)"
                .Parameters.AddWithValue("@RiviID", Val(rivinumero.Text))
                .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(TVAUTO.Text))
                .Parameters.AddWithValue("@Ilmoittaja", Kayttaja.Sukunimi & " " & Kayttaja.Etunimi)
                .Parameters.AddWithValue("@PVMklo", Now)
                .Parameters.AddWithValue("@SyyID", 9)
                .Parameters.AddWithValue("@Tarkennus", "Siirretty työvuorosta " & TVnLyhenne.Text)

            End With
            myYhteys2.Open()
            cmdTVtapahtuma4.ExecuteNonQuery()
            myYhteys2.Close()

        End If

        ' Tehdään tallennus AjetutTapahtumat tauluun KULJETTAJA
        If cbKuljettaja.Text <> "" Then
            Dim cmdTVtapahtuma2 As New MySqlCommand()
            With cmdTVtapahtuma2
                .Connection = myYhteys2
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO AjetutTapahtumat (RiviID, HloNro, Ilmoittaja, PVMklo, SyyID, Tarkennus) " &
                            "VALUES(@RiviID, @HloNro, @Ilmoittaja, @PVMklo, @SyyID, @Tarkennus)"
                .Parameters.AddWithValue("@RiviID", Val(rivinumero.Text))
                .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(TVKuljettaja.Text))
                .Parameters.AddWithValue("@Ilmoittaja", Kayttaja.Sukunimi & " " & Kayttaja.Etunimi)
                .Parameters.AddWithValue("@PVMklo", Now)
                .Parameters.AddWithValue("@SyyID", 9)
                .Parameters.AddWithValue("@Tarkennus", "Poistettu työvuorosta " & TVnLyhenne.Text)

            End With
            myYhteys2.Open()
            cmdTVtapahtuma2.ExecuteNonQuery()
            myYhteys2.Close()

        End If

        '  Catch ex As Exception
        '      Err.Clear()
        '      MsgBox("Tallentaminen ei onnistunut")
        '     Exit Sub

        '  End Try
        myYhteys.Close()

        '****************Tarkistetaan kuljettaja onko merkitty toiseen työvuoroon
        If cbKuljettaja.Text <> "" Or PalautaSQLNroNimiesta(cbKuljettaja.Text) <> 0 Then
            Dim dAl As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")

            Dim cmd2 As New MySqlCommand()
            cmd2.Connection = myYhteys
            cmd2.CommandType = CommandType.Text
            cmd2.CommandText = "SELECT RiviID, TVLyhenne FROM AjetutVuorot WHERE AlkuPVM=@AlkuPVM AND TVLyhenne<>@Lyhenne AND HloNro=@kuli"
            '     cmd2.Parameters.AddWithValue("@AlkuPVM", FormatDateTime(DAL2, DateFormat.ShortDate))
            cmd2.Parameters.AddWithValue("@Lyhenne", TVnLyhenne.Text)
            cmd2.Parameters.AddWithValue("@kuli", PalautaSQLNroNimiesta(cbKuljettaja.Text))
            cmd2.Parameters.AddWithValue("@AlkuPVM", dAl)

            myYhteys.Open()

            Dim rd As MySqlDataReader = cmd2.ExecuteReader
            If PalautaSQLNroNimiesta(cbKuljettaja.Text) <> 0 And rd.HasRows = True Then
                rd.Read()
                Dim kysymys As MsgBoxResult = Nothing
                Dim kuljettajanTV As String = rd.GetString(1)
                Dim tvkulirivi As Long = rd.GetValue(0)
                kysymys = MsgBox("Kuljettaja on merkitty työvuoroon " & kuljettajanTV & ", poistetaanko kuljettaja tästä työvuorosta", vbYesNo)
                If kysymys = MsgBoxResult.Yes Then
                    '         LokiTapahtumanTallennus(Kayttaja.HloNro, "Lahtolista: Poistettu työvuorosta " & rd.GetString(1) & " kuljettaja " & cbKuljettaja.Text, "", PalautaSQLNroNimiesta(cbKuljettaja.Text), 0)

                    myYhteys2.Close()
                    Dim cmdU As New MySqlCommand()
                    cmdU.Connection = myYhteys2
                    cmdU.CommandType = CommandType.Text
                    cmdU.CommandText = "UPDATE AjetutVuorot SET HloNro = @HloNro WHERE RiviID = @TVid" ' AND HloNro = @VanhaKuski"
                    '  cmdU.Parameters.AddWithValue("@AlkuPVM", FormatDateTime(DAL2, DateFormat.ShortDate))

                    cmdU.Parameters.AddWithValue("@TVid", tvkulirivi)
                    '   cmdU.Parameters.AddWithValue("@VanhaKuski", PalautaSQLNroNimiesta(UUSIKULI.Text))
                    cmdU.Parameters.AddWithValue("@HloNro", 0)
                    myYhteys2.Open()
                    cmdU.ExecuteNonQuery()
                    myYhteys2.Close()

                    Dim cmdTVtapahtuma As New MySqlCommand()
                    With cmdTVtapahtuma
                        .Connection = myYhteys2
                        .CommandType = CommandType.Text
                        .CommandText = "INSERT INTO AjetutTapahtumat (RiviID, HloNro, Ilmoittaja, PVMklo, SyyID, Tarkennus) " &
                            "VALUES(@RiviID, @HloNro, @Ilmoittaja, @PVMklo, @SyyID, @Tarkennus)"
                        .Parameters.AddWithValue("@RiviID", tvkulirivi)
                        .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(cbKuljettaja.Text))
                        .Parameters.AddWithValue("@Ilmoittaja", Kayttaja.Sukunimi & " " & Kayttaja.Etunimi)
                        .Parameters.AddWithValue("@PVMklo", Now)
                        .Parameters.AddWithValue("@SyyID", 9)
                        .Parameters.AddWithValue("@Tarkennus", "Siirretty työvuorosta " & kuljettajanTV)

                    End With
                    myYhteys2.Open()
                    cmdTVtapahtuma.ExecuteNonQuery()
                    myYhteys2.Close()



                End If


            End If

            myYhteys2.Close()
        End If


        '***************
        'tarkistetaan aoko auto merkitty toiseen työvuoroon
        '****************Tarkistetaan kuljettaja onko merkitty toiseen työvuoroon
        If cbAuto.Text <> "" And Val(indexAuto.Text) <> cbAuto.SelectedIndex Then
            Dim dAl As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")
            myYhteys.Close()

            Dim cmd2 As New MySqlCommand()
            cmd2.Connection = myYhteys
            cmd2.CommandType = CommandType.Text
            cmd2.CommandText = "SELECT RiviID, TVLyhenne FROM AjetutVuorot WHERE AlkuPVM=@AlkuPVM AND TVLyhenne<>@Lyhenne AND AutoNro=@kuli"
            '     cmd2.Parameters.AddWithValue("@AlkuPVM", FormatDateTime(DAL2, DateFormat.ShortDate))
            cmd2.Parameters.AddWithValue("@Lyhenne", TVnLyhenne.Text)
            cmd2.Parameters.AddWithValue("@kuli", PalautaSQLNumeroRekkarista(cbAuto.Text))
            cmd2.Parameters.AddWithValue("@AlkuPVM", dAl)

            myYhteys.Open()

            Dim rd As MySqlDataReader = cmd2.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                Dim kysymys As MsgBoxResult = Nothing
                Dim tyovuorosta As String = rd.GetString(1)
                kysymys = MsgBox("Auto on merkitty työvuoroon " & tyovuorosta & ", poistetaanko auto tästä työvuorosta", vbYesNo)
                If kysymys = MsgBoxResult.Yes Then
                    '     LokiTapahtumanTallennus(Kayttaja.HloNro, "Lahtolista: Poistettu työvuorosta " & rd.GetString(1) & " auto " & cbAuto.Text, "", 0, PalautaSQLNumeroRekkarista(cbAuto.Text))

                    myYhteys2.Close()
                    Dim cmdU As New MySqlCommand()
                    Dim TVn_rivi As Long = rd.GetValue(0)
                    cmdU.Connection = myYhteys2
                    cmdU.CommandType = CommandType.Text
                    cmdU.CommandText = "UPDATE AjetutVuorot SET AutoNro = @AutoNro WHERE RiviID = @TVid" ' AND HloNro = @VanhaKuski"
                    '  cmdU.Parameters.AddWithValue("@AlkuPVM", FormatDateTime(DAL2, DateFormat.ShortDate))

                    cmdU.Parameters.AddWithValue("@TVid", TVn_rivi)
                    '   cmdU.Parameters.AddWithValue("@VanhaKuski", PalautaSQLNroNimiesta(UUSIKULI.Text))
                    cmdU.Parameters.AddWithValue("@AutoNro", 0)
                    myYhteys2.Open()
                    cmdU.ExecuteNonQuery()
                    myYhteys2.Close()
                    '
                    Dim cmdTVtapahtuma As New MySqlCommand()
                    With cmdTVtapahtuma
                        .Connection = myYhteys2
                        .CommandType = CommandType.Text
                        .CommandText = "INSERT INTO AjetutTapahtumat (RiviID, AutoNro, Ilmoittaja, PVMklo, SyyID, Tarkennus) " &
                            "VALUES(@RiviID, @AutoNro, @Ilmoittaja, @PVMklo, @SyyID, @Tarkennus)"
                        .Parameters.AddWithValue("@RiviID", TVn_rivi)
                        .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbAuto.Text))
                        .Parameters.AddWithValue("@Ilmoittaja", Kayttaja.Sukunimi & " " & Kayttaja.Etunimi)
                        .Parameters.AddWithValue("@PVMklo", Now)
                        .Parameters.AddWithValue("@SyyID", 9)
                        .Parameters.AddWithValue("@Tarkennus", "Siirretty työvuorosta " & tyovuorosta)

                    End With
                    myYhteys2.Open()
                    cmdTVtapahtuma.ExecuteNonQuery()
                    myYhteys2.Close()



                End If


            End If

            myYhteys2.Close()
        End If
        '*************




        LataaTunnuksetDataKenttaan()


        If Button4.BackColor = Color.Lime Then
            varjaaOsastonPomo(1)
            LataaTunnuksetDataKenttaanPOMO(120)

        End If
        If Button5.BackColor = Color.Lime Then
            varjaaOsastonPomo(2)
            LataaTunnuksetDataKenttaanPOMO(150)

        End If
        If Button6.BackColor = Color.Lime Then
            varjaaOsastonPomo(3)
            LataaTunnuksetDataKenttaanPOMO(314)

        End If
        If Button7.BackColor = Color.Lime Then
            varjaaOsastonPomo(4)
            LataaTunnuksetDataKenttaan()
        End If

        If Button17.BackColor = Color.Lime Then
            varjaaOsastonPomo(5)
            LataaTunnuksetDataKenttaanPOMO(121)
        End If




        '       DGW.Enabled = True
        TVAlkuaika.Text = ""
        TValoitettu.Text = ""
        TVlopetettu.Text = ""
        TVLoppuaika.Text = ""
        cbAuto.Text = ""
        cbKuljettaja.Text = ""
        TVnLyhenne.Text = ""
        Label2.Text = ""
        Label9.Text = ""

        TallenneKuljettaja.Visible = False
        DGW.Rows(Val(valitturivi.Text)).Selected = True
        TallennaTietokantaToiminto(1)
        If Kayttaja.Lahtolista_TVhistorianSeuranta = True Then
            Panel_TVhistoria.Visible = False
            Panel_TVhistoria.Location = New Point(125, 594)
            Panel_TVhistoria.Size = New Size(23, 33)

        End If

        '     Main.ToolStripButton1_Click(Nothing, Nothing)

    End Sub

    Private Sub DGW_AutoSizeChanged(sender As Object, e As EventArgs) Handles DGW.AutoSizeChanged

    End Sub

    Private Sub btnTietaa_Click(sender As Object, e As EventArgs) Handles btnTietaa.Click
        myYhteys.Close()
        If TVKuljettaja.Text = "" Then Exit Sub

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT Tietaa FROM AjetutVuorot WHERE RiviID=@id"
            .Parameters.AddWithValue("@id", rivinumero.Text)

        End With
        myYhteys.Open()
        Dim tietaaHaku As Boolean = cmd.ExecuteScalar
        myYhteys.Close()
        cmd.Dispose()

        Dim cmd2 As New MySqlCommand()
        With cmd2
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO AjetutInformoitu (RiviID, HloNro, PVMklo, Ilmoittaja, LisattyTV) VALUES(@RiviID, @HloNro, @PVMklo, @Ilmoittaja, @LisattyTV)"
            .Parameters.AddWithValue("@RiviID", rivinumero.Text)
            .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(TVKuljettaja.Text))
            .Parameters.AddWithValue("@PVMklo", Now)
            .Parameters.AddWithValue("@Ilmoittaja", Kayttaja.Sukunimi & " " & Kayttaja.Etunimi)
            If tietaaHaku = False Then
                '    MsgBox("ERROR")
                .Parameters.AddWithValue("@LisattyTV", 1)
            Else
                '   MsgBox("ACCEPT")
                .Parameters.AddWithValue("@LisattyTV", 0)

            End If
            myYhteys.Open()
            cmd2.ExecuteNonQuery()



            Dim cmdAjet As New MySqlCommand()
            With cmdAjet
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "UPDATE AjetutVuorot SET Tietaa=@Tietaa WHERE RiviID=@id"
                .Parameters.AddWithValue("@id", rivinumero.Text)
                If tietaaHaku = False Then
                    '    MsgBox("ERROR")
                    .Parameters.AddWithValue("@Tietaa", 1)
                    btnTietaa.Image = Image.FromHbitmap(My.Resources.accept.GetHbitmap())
                    ToolTip1.SetToolTip(Me.btnTietaa, "Kuljettaja tietää työvuoron")

                Else
                    '   MsgBox("ACCEPT")
                    .Parameters.AddWithValue("@Tietaa", 0)
                    btnTietaa.Image = Image.FromHbitmap(My.Resources._error.GetHbitmap())
                    ToolTip1.SetToolTip(Me.btnTietaa, "Kuljettaja EI ole tietoinen työvuorosta")

                End If
                cmdAjet.ExecuteNonQuery()

            End With
            cmd2.Dispose()
            cmdAjet.Dispose()
            myYhteys.Close()

        End With


    End Sub

    Private Sub PVM_Click(sender As Object, e As EventArgs) Handles PVM.Click

    End Sub

    Private Sub Infot_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    'Private Sub Infot_VisibleChanged(sender As Object, e As EventArgs)
    '    If Infot.Visible = True Then
    '        Infot.Location = New Point(470, 489)
    '        Infot.Size = New Size(540, 208)
    '        HaeInformaatioEsille()

    '    End If
    '    If Infot.Visible = False Then
    '        Infot.Location = New Point(125, 637)
    '        Infot.Size = New Size(40, 70)
    '    End If
    'End Sub
    'Public Sub HaeInformaatioEsille()
    '    '     If Kayttaja.
    '    '   Infot.Controls.Clear()

    '    myYhteys.Close()

    '    Dim cmd As New MySqlCommand()
    '    With cmd
    '        .Connection = myYhteys
    '        .CommandType = CommandType.Text
    '        .CommandText = "SELECT Informaatio.ID, Informaatio.Teksti, Informaatio.LisattyPVMklo, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Kuljettaja FROM Informaatio " &
    '        "INNER JOIN Henkilosto ON Informaatio.LisaajaHloNro=Henkilosto.HloNro " &
    '        "WHERE Informaatio.Luettu=0 AND Informaatio.OsoitettuHloNro=@Hlolle " &
    '        "ORDER BY Informaatio.LisattyPVMklo DESC"
    '        .Parameters.AddWithValue("@Hlolle", Kayttaja.HloNro)
    '    End With

    '    myYhteys.Open()

    '    Dim rd As MySqlDataReader = cmd.ExecuteReader
    '    If rd.HasRows = True Then
    '        While rd.Read

    '            Dim btn As New Button
    '            btn.Tag = rd.GetValue(0)
    '            btn.Text = rd.GetString(1)
    '            Dim pvmkasittely As Date = rd.GetValue(2)
    '            ToolTip1.SetToolTip(btn, "Lisatty: " & FormatDateTime(pvmkasittely, DateFormat.ShortDate).ToString & " " & FormatDateTime(pvmkasittely, DateFormat.ShortTime).ToString & vbCrLf & "Lisääjä: " & rd.GetString(3))
    '            btn.AutoSize = True
    '            btn.TextAlign = ContentAlignment.MiddleLeft

    '            '   Infot.Controls.Add(btn)
    '            AddHandler btn.Click, AddressOf infon_Kasittely



    '        End While
    '    End If
    '    myYhteys.Close()

    'End Sub
    'Private Sub infon_Kasittely(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim clickedButton As Button

    '    clickedButton = CType(sender, Button)

    '    If clickedButton.Text = "" Then Exit Sub 'clickedLabel.ForeColor = Color.Red

    '    Dim info_rivinro As Long = clickedButton.Tag

    '    myYhteys.Close()
    '    Dim cmd As New MySqlCommand()
    '    With cmd
    '        .Connection = myYhteys
    '        .CommandType = CommandType.Text
    '        .CommandText = "UPDATE Informaatio SET Luettu=1, LuettuPVMklo=@pvm WHERE ID=@id"
    '        .Parameters.AddWithValue("@id", info_rivinro)
    '        .Parameters.AddWithValue("@pvm", Now)

    '    End With
    '    myYhteys.Open()
    '    cmd.ExecuteNonQuery()
    '    myYhteys.Close()


    '    '   MsgBox(CType(clickedButton.Tag, String))
    '    '      FLP.Visible = False
    '    '  Infot.Controls.Remove(clickedButton)
    '    '     FLP.Visible = True

    'End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        '  Infot.Visible = False
        panel_poissaolot.Visible = True
        Button18.BackColor = Color.Silver
        Button21.BackColor = Color.Lime





    End Sub

    Private Sub lomaAlku_ValueChanged(sender As Object, e As EventArgs) Handles lomaAlku.ValueChanged
        If rbSaikku.Checked = True Or rbMuuvapaa.Checked = True Or rbKesaLoma.Checked = True Or rbTalviloma.Checked = True Then
            Button8.Visible = True
            If lomaLoppu.Value < lomaAlku.Value Then lomaLoppu.Value = lomaAlku.Value

        Else
            Button8.Visible = False
            If lomaLoppu.Value < lomaAlku.Value Then lomaLoppu.Value = lomaAlku.Value


        End If

    End Sub

    Private Sub lomaLoppu_ValueChanged(sender As Object, e As EventArgs) Handles lomaLoppu.ValueChanged
        If rbSaikku.Checked = True Or rbMuuvapaa.Checked = True Or rbKesaLoma.Checked = True Or rbTalviloma.Checked = True Then
            Button8.Visible = True

        Else
            Button8.Visible = False

        End If

    End Sub

    Private Sub rbSaikku_CheckedChanged(sender As Object, e As EventArgs) Handles rbSaikku.CheckedChanged
        If rbSaikku.Checked = True Or rbMuuvapaa.Checked = True Or rbKesaLoma.Checked = True Or rbTalviloma.Checked = True Then
            Button8.Visible = True

        Else
            Button8.Visible = False

        End If
        '    lomaLoppu.Value = lomaAlku.Value

    End Sub

    Private Sub rbMuuvapaa_CheckedChanged(sender As Object, e As EventArgs) Handles rbMuuvapaa.CheckedChanged
        If rbSaikku.Checked = True Or rbMuuvapaa.Checked = True Or rbKesaLoma.Checked = True Or rbTalviloma.Checked = True Then
            Button8.Visible = True

        Else
            Button8.Visible = False

        End If
    End Sub

    Private Sub rbKesaLoma_CheckedChanged(sender As Object, e As EventArgs) Handles rbKesaLoma.CheckedChanged
        '    lomaLoppu.Value = lomaAlku.Value.AddDays(26)
        If rbSaikku.Checked = True Or rbMuuvapaa.Checked = True Or rbKesaLoma.Checked = True Or rbTalviloma.Checked = True Then
            Button8.Visible = True

        Else
            Button8.Visible = False

        End If

    End Sub

    Private Sub rbTalviloma_CheckedChanged(sender As Object, e As EventArgs) Handles rbTalviloma.CheckedChanged
        If rbSaikku.Checked = True Or rbMuuvapaa.Checked = True Or rbKesaLoma.Checked = True Or rbTalviloma.Checked = True Then
            Button8.Visible = True

        Else
            Button8.Visible = False

        End If

    End Sub

    Private Sub Button8_Click_1(sender As Object, e As EventArgs) Handles Button8.Click
        If lomaKuljettaja.Text = "" Then MsgBox("Valitse kuljettaja") : Exit Sub
        Dim tyyppi As String = ""
        Dim lomasyy As Integer = 0
        If rbKesaLoma.Checked = True Then tyyppi = " kesäloma " : lomasyy = 3
        If rbSaikku.Checked = True Then tyyppi = " saikku " : lomasyy = 1
        If rbMuuvapaa.Checked = True Then tyyppi = " vapaa " : lomasyy = 2
        If rbTalviloma.Checked = True Then tyyppi = " talviloma " : lomasyy = 4

        '  LokiTapahtumanTallennus(Kayttaja.HloNro, "Lähtölista: Lomatallennus" & tyyppi & lomaKuljettaja.Text, "ajalle " & lomaAlku.Value.ToString & "-" & lomaLoppu.Value.ToString, PalautaSQLNroNimiesta(lomaKuljettaja.Text), 0)

        '  Try
        myYhteys.Close()
        Dim cmd As New MySqlCommand
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO Lomat (HloNro, AlkaenPVM, AstiPVM, LomaTyyppi, Lisaaja, Lisatty, Perustelu) VALUES(@HloNro, @AlkaenPVM, @AstiPVM, @LomaTyyppi, @Lisaaja, @Lisatty, @Perustelu) "
            .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(lomaKuljettaja.Text))
            .Parameters.AddWithValue("@AlkaenPVM", lomaAlku.Value)
            .Parameters.AddWithValue("@AstiPVM", lomaLoppu.Value)
            If rbKesaLoma.Checked = True Then .Parameters.AddWithValue("@LomaTyyppi", "3")
            If rbSaikku.Checked = True Then .Parameters.AddWithValue("@LomaTyyppi", "1")
            If rbMuuvapaa.Checked = True Then .Parameters.AddWithValue("@LomaTyyppi", "2")
            If rbTalviloma.Checked = True Then .Parameters.AddWithValue("@LomaTyyppi", "4")
            .Parameters.AddWithValue("@Lisaaja", Kayttaja.HloNro)
            .Parameters.AddWithValue("@Lisatty", Now)
            .Parameters.AddWithValue("@Perustelu", txtTarkennus.Text)
        End With
        myYhteys.Open()
        cmd.ExecuteNonQuery()
        myYhteys.Close()
        ' lähetetään muutoksen tekijälle sähköposti avoimeksi tulleista vuoroista

        Dim dAl1a As Date = Format(lomaAlku.Value, "\#yyyy\-MM\-dd\#")
        Dim dAl2a As Date = Format(lomaLoppu.Value, "\#yyyy\-MM\-dd\#")
        Dim theviesti As String = ""

        Dim cmd2a As New MySqlCommand()
        With cmd2a
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT AlkuPVM, TVLyhenne FROM AjetutVuorot WHERE HloNro=@HloNro AND (AlkuPVM BETWEEN @alku AND @loppu) ORDER BY AlkuPVM ASC"
            .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(lomaKuljettaja.Text))
            .Parameters.AddWithValue("@alku", dAl1a)
            .Parameters.AddWithValue("@loppu", dAl2a)
        End With
        myYhteys.Open()
        '   Try
        Dim rdA As MySqlDataReader = cmd2a.ExecuteReader
        If rdA.HasRows = True Then


            While rdA.Read
                Dim pvmtovkpl As Date = rdA.GetValue(0)
                theviesti &= PalautaViikonPaiva(pvmtovkpl) & " " & FormatDateTime(pvmtovkpl, DateFormat.ShortDate).ToString & vbTab & rdA.GetString(1) & vbCrLf

            End While
            If Kayttaja.eMail = "" Then Kayttaja.eMail = "joakim.selander@taksikuljetus.fi"
            If theviesti <> "" Then
                LahetaSpostia(Kayttaja.Etunimi & " " & Kayttaja.Sukunimi, Kayttaja.eMail, "AVOIMET TYÖVUOROT " & lomaKuljettaja.Text & " " & tyyppi, theviesti)
            End If

        End If
        '  Catch ex As Exception
        '      Err.Clear()
        '      MsgBox("Tallentaminen ei onnistunut")
        '     Exit Sub

        '  End Try
        myYhteys.Close()


        ' SIIRRETÄÄN TYÖVUOOROT KULJETTAJALTA POIS (--> EdHlo:ksi)
        Dim dAl1 As Date = Format(lomaAlku.Value, "\#yyyy\-MM\-dd\#")
        Dim dAl2 As Date = Format(lomaLoppu.Value, "\#yyyy\-MM\-dd\#")

        'Kirjataan tapahtuma AjetutTapahtumat tauluun

        Dim AjetutCMD As New MySqlCommand()
        With AjetutCMD
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT RiviID, TVLyhenne FROM AjetutVuorot WHERE HloNro=@HloNro AND (AlkuPVM BETWEEN @alku AND @loppu)"
            .Parameters.AddWithValue("@alku", dAl1)
            .Parameters.AddWithValue("@loppu", dAl2)
            .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(lomaKuljettaja.Text))

        End With
        myYhteys2.Open()
        myYhteys.Open()
        Dim rdTapahtuma As MySqlDataReader = AjetutCMD.ExecuteReader
        If rdTapahtuma.HasRows = True Then
            While rdTapahtuma.Read
                Dim cmdsyotto As New MySqlCommand()
                With cmdsyotto
                    .Connection = myYhteys2
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO AjetutTapahtumat (RiviID, HloNro, Ilmoittaja, PVMklo, SyyID, Tarkennus) VALUES(@RiviID, @HloNro, @Ilmoittaja, @PVMklo, @SyyID, @Tarkennus)"
                    .Parameters.AddWithValue("@RiviID", rdTapahtuma.GetValue(0))
                    .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(lomaKuljettaja.Text))
                    .Parameters.AddWithValue("@Ilmoittaja", Kayttaja.Sukunimi & " " & Kayttaja.Etunimi)
                    .Parameters.AddWithValue("@PVMklo", Now)
                    .Parameters.AddWithValue("@SyyID", lomasyy)
                    .Parameters.AddWithValue("@Tarkennus", "POISSAOLOKIRJAUS POISTETTU TYÖVUOROSTA " & rdTapahtuma.GetString(1))
                End With

                cmdsyotto.ExecuteNonQuery()


            End While



        End If



        myYhteys2.Close()

        'siirretään poistyövuorot
        Dim cmd2 As New MySqlCommand()
        With cmd2
            .Connection = myYhteys2
            .CommandType = CommandType.Text
            .CommandText = "UPDATE AjetutVuorot SET HloNro=@uusi, EdHlo=@EdHlo WHERE HloNro=@HloNro AND (AlkuPVM BETWEEN @alku AND @loppu)"
            .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(lomaKuljettaja.Text))
            .Parameters.AddWithValue("@uusi", "0")
            .Parameters.AddWithValue("@alku", dAl1)
            .Parameters.AddWithValue("@loppu", dAl2)
            .Parameters.AddWithValue("@EdHlo", PalautaSQLNroNimiesta(lomaKuljettaja.Text))
        End With
        myYhteys2.Open()
        '   Try
        cmd2.ExecuteNonQuery()

        '  Catch ex As Exception
        '      Err.Clear()
        '      MsgBox("Tallentaminen ei onnistunut")
        '     Exit Sub

        '  End Try
        myYhteys2.Close()

        myYhteys.Close()

        ' TEHDÄÄN MERKINN*T Informaatio tauluun

        Dim cmdInfo As New MySqlCommand()
        With cmdInfo
            .Connection = myYhteys2
            .CommandType = CommandType.Text
            .CommandText = "SELECT HloNro FROM Kayttajat WHERE Taso>6"
        End With

        myYhteys2.Open()
        Dim pomohaku As MySqlDataReader = cmdInfo.ExecuteReader
        Dim pvmvali As String = ""

        If lomaAlku.Value = lomaLoppu.Value Then
            pvmvali = FormatDateTime(lomaAlku.Value, DateFormat.ShortDate).ToString
        Else
            pvmvali = FormatDateTime(lomaAlku.Value, DateFormat.ShortDate).ToString & " - " & FormatDateTime(lomaLoppu.Value, DateFormat.ShortDate).ToString
        End If


        If pomohaku.HasRows = True Then
            myYhteys3.Open()

            While pomohaku.Read

                Dim infolisays As New MySqlCommand()
                With infolisays
                    .Connection = myYhteys3
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO Informaatio (Teksti, OsoitettuHloNro, LisaajaHloNro, LisattyPVMklo) VALUES(@Teksti, @OsoitettuHloNro, @LisaajaHloNro, @LisattyPVMklo)"
                    .Parameters.AddWithValue("@Teksti", LTrim(tyyppi) & lomaKuljettaja.Text & " " & pvmvali)
                    .Parameters.AddWithValue("@OsoitettuHloNro", pomohaku.GetValue(0))
                    .Parameters.AddWithValue("@LisaajaHloNro", Kayttaja.HloNro)
                    .Parameters.AddWithValue("@LisattyPVMklo", Now)

                End With
                infolisays.ExecuteNonQuery()


            End While

            pomohaku.Dispose()

        End If

        myYhteys.Close()
        myYhteys2.Close()
        myYhteys3.Close()







        ' Catch ex As Exception
        '        LokiTapahtumanTallennus(Kayttaja.HloNro, "VIRHE lomatallennus", ErrorToString, 0, 0)
        ' Err.Clear()

        '    MsgBox("Virhe tallentamisessa")
        '  Exit Sub

        ' Finally
        MsgBox("Tallennettu")
        lomaKuljettaja.Text = ""

        ' End Try
        Button8.Visible = False
        '    HaeViimeisimmätlomailmoiotukset()

    End Sub

    Private Sub panel_poissaolot_Paint(sender As Object, e As PaintEventArgs) Handles panel_poissaolot.Paint

    End Sub

    Private Sub panel_poissaolot_VisibleChanged(sender As Object, e As EventArgs) Handles panel_poissaolot.VisibleChanged
        If panel_poissaolot.Visible = True Then
            panel_poissaolot.Location = New Point(470, 489)
            panel_poissaolot.Size = New Size(540, 208)
            lomaKuljettaja.Text = ""


        End If
        If panel_poissaolot.Visible = False Then
            panel_poissaolot.Location = New Point(302, 626)
            panel_poissaolot.Size = New Size(40, 70)
        End If

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim a As MsgBoxResult = MsgBox("Poistetaanko kuljettaja " & TVKuljettaja.Text & " työvuorosta " & TVnLyhenne.Text & "?", vbYesNo)

        If a = MsgBoxResult.Yes Then
            myYhteys.Close()

            Dim cmd As New MySqlCommand()
            '      LokiTapahtumanTallennus(Kayttaja.HloNro, "Tallennettu muutos työvuoroon " & TVnLyhenne.Text & " / " & cbKuljettaja.Text & " / " & cbAuto.Text, PVM.Value.ToString, PalautaSQLNroNimiesta(cbKuljettaja.Text), PalautaSQLNumeroRekkarista(cbAuto.Text))

            With cmd
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "UPDATE AjetutVuorot SET HloNro=@HloNro, EdHlo=@EdHlo, Tietaa=@Tietaa WHERE RiviID=@id"

                .Parameters.AddWithValue("@EdHlo", PalautaSQLNroNimiesta(TVKuljettaja.Text))
                .Parameters.AddWithValue("@HloNro", 0)
                .Parameters.AddWithValue("@Tietaa", 0)



                .Parameters.AddWithValue("id", Val(rivinumero.Text))
            End With
            myYhteys.Open()
            '   Try
            cmd.ExecuteNonQuery()


            ' Tehdään tallennus AjetutTapahtumat tauluun KULJETTAJA
            Dim cmdTVtapahtuma2 As New MySqlCommand()
            With cmdTVtapahtuma2
                .Connection = myYhteys2
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO AjetutTapahtumat (RiviID, HloNro, Ilmoittaja, PVMklo, SyyID, Tarkennus) " &
                            "VALUES(@RiviID, @HloNro, @Ilmoittaja, @PVMklo, @SyyID, @Tarkennus)"
                .Parameters.AddWithValue("@RiviID", Val(rivinumero.Text))
                .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(TVKuljettaja.Text))
                .Parameters.AddWithValue("@Ilmoittaja", Kayttaja.Sukunimi & " " & Kayttaja.Etunimi)
                .Parameters.AddWithValue("@PVMklo", Now)
                .Parameters.AddWithValue("@SyyID", 9)
                .Parameters.AddWithValue("@Tarkennus", "Poistettu työvuorosta " & TVnLyhenne.Text)

            End With
            myYhteys2.Open()
            cmdTVtapahtuma2.ExecuteNonQuery()
            myYhteys2.Close()


            '  Catch ex As Exception
            '      Err.Clear()
            '      MsgBox("Tallentaminen ei onnistunut")
            '     Exit Sub

            '  End Try
            myYhteys.Close()



            LataaTunnuksetDataKenttaan()
            '       DGW.Enabled = True
            TVAlkuaika.Text = ""
            TValoitettu.Text = ""
            TVlopetettu.Text = ""
            TVLoppuaika.Text = ""
            cbAuto.Text = ""
            cbKuljettaja.Text = ""
            TVnLyhenne.Text = ""
            Label2.Text = ""
            Label9.Text = ""

            TallenneKuljettaja.Visible = False
            DGW.Rows(Val(valitturivi.Text)).Selected = True
            TallennaTietokantaToiminto(1)
            If Kayttaja.Lahtolista_TVhistorianSeuranta = True Then
                Panel_TVhistoria.Visible = False
                Panel_TVhistoria.Location = New Point(125, 594)
                Panel_TVhistoria.Size = New Size(23, 33)

            End If




        End If


    End Sub

    Private Sub Button14_Click_1(sender As Object, e As EventArgs) Handles Button14.Click
        Dim a As MsgBoxResult = MsgBox("Poistetaanko auto " & TVAUTO.Text & " työvuorosta " & TVnLyhenne.Text & "?", vbYesNo)

        If a = MsgBoxResult.Yes Then
            myYhteys.Close()
            '      My.Computer.Audio.Play("c:\temp\x.mp3")

            Dim cmd As New MySqlCommand()
            '      LokiTapahtumanTallennus(Kayttaja.HloNro, "Tallennettu muutos työvuoroon " & TVnLyhenne.Text & " / " & cbKuljettaja.Text & " / " & cbAuto.Text, PVM.Value.ToString, PalautaSQLNroNimiesta(cbKuljettaja.Text), PalautaSQLNumeroRekkarista(cbAuto.Text))

            With cmd
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "UPDATE AjetutVuorot SET HloNro=@HloNro, AutoNro=@AutoNro, EdAuto=@EdAuto, EdHlo=@EdHlo, Tietaa=@Tietaa WHERE RiviID=@id"
                '       .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(cbKuljettaja.Text))
                '     .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbAuto.Text))


                .Parameters.AddWithValue("@EdAuto", PalautaSQLNumeroRekkarista(TVAUTO.Text))
                .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbAuto.Text))
                '      .Parameters.AddWithValue("@Tietaa", 1)



                .Parameters.AddWithValue("id", Val(rivinumero.Text))
            End With
            myYhteys.Open()
            '   Try
            cmd.ExecuteNonQuery()

            ' Tehdään tallennus AjetutTapahtumat tauluun AUTO
            If cbAuto.Text <> "" Then
                Dim cmdTVtapahtuma4 As New MySqlCommand()
                With cmdTVtapahtuma4
                    .Connection = myYhteys2
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO AjetutTapahtumat (RiviID, AutoNro, Ilmoittaja, PVMklo, SyyID, Tarkennus) " &
                            "VALUES(@RiviID, @AutoNro, @Ilmoittaja, @PVMklo, @SyyID, @Tarkennus)"
                    .Parameters.AddWithValue("@RiviID", Val(rivinumero.Text))
                    .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(TVAUTO.Text))
                    .Parameters.AddWithValue("@Ilmoittaja", Kayttaja.Sukunimi & " " & Kayttaja.Etunimi)
                    .Parameters.AddWithValue("@PVMklo", Now)
                    .Parameters.AddWithValue("@SyyID", 9)
                    .Parameters.AddWithValue("@Tarkennus", "Poistettu työvuorosta " & TVnLyhenne.Text)

                End With
                myYhteys2.Open()
                cmdTVtapahtuma4.ExecuteNonQuery()
                myYhteys2.Close()

            End If


            '  Catch ex As Exception
            '      Err.Clear()
            '      MsgBox("Tallentaminen ei onnistunut")
            '     Exit Sub

            '  End Try
            myYhteys.Close()

            '*************




            LataaTunnuksetDataKenttaan()
            '       DGW.Enabled = True
            TVAlkuaika.Text = ""
            TValoitettu.Text = ""
            TVlopetettu.Text = ""
            TVLoppuaika.Text = ""
            cbAuto.Text = ""
            cbKuljettaja.Text = ""
            TVnLyhenne.Text = ""
            Label2.Text = ""
            Label9.Text = ""

            TallenneKuljettaja.Visible = False
            DGW.Rows(Val(valitturivi.Text)).Selected = True
            TallennaTietokantaToiminto(1)
            If Kayttaja.Lahtolista_TVhistorianSeuranta = True Then
                Panel_TVhistoria.Visible = False
                Panel_TVhistoria.Location = New Point(125, 594)
                Panel_TVhistoria.Size = New Size(23, 33)

            End If

        End If

    End Sub

    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        LataaTunnuksetDataKenttaanPOMO(121)
        varjaaOsastonPomo(5)
        SiirraPaneelitSivuun()
        PanelLahtolista.Size = New Size(421, 611)
        PanelLahtolista.Location = New Point(42, 86)
        PanelLahtolista.Visible = True

    End Sub

    Private Sub Panel2_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub TulostaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles TulostaToolStripMenuItem.Click
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
            Dim dAl As Date = Format(PVM.Value, "\#yyyy\-MM\-dd\#")

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT Kuva FROM TyoOhjeet WHERE TVnimi=@nro AND (@pvm BETWEEN AlkaenPVM and AstiPVM) AND Sivu=@sivu"
                .Parameters.AddWithValue("@nro", TVnLyhenne.Text)
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

    Private Sub TulostaTyoOhje_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles TulostaTyoOhje.Opening

    End Sub
End Class