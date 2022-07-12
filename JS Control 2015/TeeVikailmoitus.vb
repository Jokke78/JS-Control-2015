Imports MySql.Data.MySqlClient

Public Class TeeVikailmoitus
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection2 As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection3 As MySqlConnection = New MySqlConnection(serverString)
    Dim DA As New MySqlDataAdapter()
    Dim DS As New DataSet()

    Private Sub TeeVikailmoitus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'JSControlDataSet.Kalusto' table. You can move, or remove it, as needed.
        'Me.KalustoTableAdapter.Fill(Me.JSControlDataSet.Kalusto)
        Try
            TbConnection.Close()

            DA.SelectCommand = New MySqlCommand
            DA.SelectCommand.Connection = TbConnection
            '        If KayttajanTaso = "J" Or KayttajanTaso = "P" Then
            'DA.SelectCommand.CommandText = "SELECT AutoNro, RekNro FROM Kalusto" ' WHERE Raporteissa = 1 ORDER BY RekNro ASC"
            '       Else
            DA.SelectCommand.CommandText = "SELECT AutoNro, RekNro FROM Kalusto WHERE Raporteissa = '1' ORDER BY RekNro ASC"

            '       End If
            '   DA.SelectCommand.Parameters.AddWithValue("@Raporteissa", True)

            DA.SelectCommand.CommandType = CommandType.Text
            TbConnection.Open()
            DA.Fill(DS, "Kalusto")
            TbConnection.Close()
            Dim BS As New BindingSource()
            BS.DataSource = DS
            BS.DataMember = "Kalusto"
            BS.Sort = "RekNro"

            txtIlmoittaja.Text = KayttajaNimi

            txtREK.DataSource = BS
            txtREK.DisplayMember = "RekNro"
            txtREK.ValueMember = "AutoNro"
            txtREK.Text = ""

            '  If KayttajanAuto <> 0 Then

            '  End If

            HaeValmisViatNakyviin()


            Me.Cursor = Cursors.Default


            txtREK.Focus()
            TbConnection3.Close()
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection3
                .CommandType = CommandType.Text
                .CommandText = "SELECT HloNro, AutoNro FROM AjetutVuorot WHERE (AlkuPVM = @AlkuPVM) AND (HloNro = @HloNro)"
                '   cmd.Parameters.AddWithValue("@ALkuPVM", FormatDateTime(NaytettavaPVM.Value, DateFormat.ShortDate))
                .Parameters.AddWithValue("@AlkuPVM", Today)
                .Parameters.AddWithValue("@HloNro", KayttajaHloNro)

            End With
            TbConnection3.Open()

            Dim rd As MySqlDataReader = cmd.ExecuteReader

            If rd.HasRows = True Then
                rd.Read()
                txtREK.Text = PalautaSQLRekkariNumerosta(rd.GetValue(1))

            End If

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 1", ErrorToString, 0, 0)
            Err.Clear()

        Finally

            TbConnection3.Close()

        End Try
        If OletusAuto <> "" Then txtREK.Text = OletusAuto

    End Sub
    Public Sub HaeIlmoitetutViat()
        If txtREK.Text = "" Then Exit Sub
        Try
            TbConnection3.Close()
            Dim DA As New MySqlDataAdapter()
            DA.SelectCommand = New MySqlCommand()
            DA.SelectCommand.Connection = TbConnection3
            DA.SelectCommand.CommandText = "SELECT IlmPVM, IlmoitettuVika FROM VikaIlmoitukset WHERE AutoNro = @AutoNro AND Korjattu = @Korjattu ORDER BY IlmPVM DESC"
            Dim autoNro As Integer = PalautaSQLNumeroRekkarista(txtREK.Text)
            DA.SelectCommand.Parameters.AddWithValue("@AutoNro", autoNro)
            DA.SelectCommand.Parameters.AddWithValue("@Korjattu", False)

            Dim ds As New DataSet
            TbConnection3.Open()

            DA.Fill(ds, "Kalusto")

            DGV.DataSource = ds
            DGV.DataMember = "Kalusto"

            DGV.Columns(0).HeaderText = "Ilmoitus pvm"
            DGV.Columns(1).HeaderText = "Ilmoitettu vika"
            DGV.Columns(0).Width = 150 'AutoSizeMode.AllCells = True
            DGV.Columns(1).Width = 600

            TbConnection3.Close()
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 2", ErrorToString, 0, 0)
            Err.Clear()

        End Try





    End Sub
    Public Sub _XHaeValmisViatNakyviinUUSI()
        Try
            TbConnection.Close()
            DA.SelectCommand = New MySqlCommand
            DA.SelectCommand.Connection = TbConnection
            DA.SelectCommand.CommandText = "SELECT * FROM ValmisViat" 'WHERE Raporteissa = @Raporteissa ORDER BY RekNro ASC"
            '   DA.SelectCommand.Parameters.AddWithValue("@Raporteissa", True)

            DA.SelectCommand.CommandType = CommandType.Text
            TbConnection.Open()
            DA.Fill(DS, "ValmisViat")
            TbConnection.Close()
            Dim BS As New BindingSource()
            BS.DataSource = DS
            BS.DataMember = "ValmisViat"
            BS.Sort = "Vika"


            lbViat.DataSource = BS
            lbViat.DisplayMember = "IlmoitettuVika"
            lbViat.ValueMember = "RiviID"

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 3", ErrorToString, 0, 0)
            Err.Clear()

        End Try


    End Sub
    Public Sub HaeValmisViatNakyviin()
        Try
            TbConnection.Close()

            Dim cmd As New MySqlCommand()
            cmd.CommandText = "SELECT Vika FROM ValmisViat" ' ORDER BY Vika ASC"
            cmd.Connection = TbConnection
            TbConnection.Open()

            Dim rdV As MySqlDataReader = cmd.ExecuteReader

            lbViat.Items.Clear()

            While rdV.Read
                lbViat.Items.Add(rdV.GetString(0))

            End While
            rdV.Close()
            TbConnection.Close()


        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 4", ErrorToString, 0, 0)
            Err.Clear()

        End Try


    End Sub



    Private Sub txtREK_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtREK.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If RekkariLoytyy(txtREK.Text) = True Then
                    lbViat.Enabled = True
                    If lbViat.Items.Count < 5 Then
                        HaeValmisViatNakyviin()

                    End If

                    HaeIlmoitetutViat()
                    lbViat.Focus()

                End If
            End If
        Catch ex As Exception
            Err.Clear()

        End Try



    End Sub

    Private Sub ftxtREK_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtREK.Leave
        Try
            If RekkariLoytyy(txtREK.Text) = True Then
                lbViat.Enabled = True

                lbViat.Focus()

            End If

        Catch ex As Exception
            Err.Clear()

        End Try

    End Sub
    Public Function RekkariLoytyy(ByVal rekkari As String) As Boolean
        Dim paluu As Boolean = False
        Try
            TbConnection2.Close()
            Dim cmd As New MySqlCommand()
            cmd.Connection = TbConnection2
            cmd.CommandText = "SELECT * FROM Kalusto"
            TbConnection2.Open()

            Dim rdK As MySqlDataReader = cmd.ExecuteReader
            While rdK.Read
                If rdK.GetString(1) = rekkari Then
                    paluu = True
                    Exit While

                End If
            End While
            TbConnection2.Close()
            rdK.Close()

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 5", ErrorToString, 0, 0)
            Err.Clear()

        End Try

        Return paluu


    End Function
    Public Sub LataaHuoltohistoria()
        HH.Text = ""
        Dim autoNro As Integer = PalautaSQLNumeroRekkarista(txtREK.Text)
        Dim tmNro As Long = 0
        Dim kmTieto As Long = 0
        Dim otsikko As String = ""
        Dim edellinenTM As Long = 0
        Dim KokonaisAika As Integer = 0

        Try
            TbConnection.Close()
            TbConnection2.Close()

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT Tyomaaraykset.TMnro, Tyomaaraykset.LuontiPVM, Tyomaaraykset.Otsikko, Tyomaaraykset.TyonKuvaus, KMTiedot.PVM, KMTiedot.KMlukema FROM Tyomaaraykset " & _
                    "RIGHT OUTER JOIN KMTiedot ON Tyomaaraykset.TMnro = KMTiedot.TMnro " & _
                    "WHERE Tyomaaraykset.AutoNro=@AutoNro AND Tyomaaraykset.Valmis='1' ORDER BY KMTiedot.PVM DESC"
                .Parameters.AddWithValue("@AutoNro", autoNro)

            End With
            TbConnection.Open()

            Using rd As MySqlDataReader = cmd.ExecuteReader

                HH.Text = "HUOLTOHISTORIA" & vbTab & txtREK.Text & " [#" & autoNro.ToString & " ]" & vbTab & vbTab & FormatDateTime(Today, DateFormat.ShortDate).ToString & vbCrLf & vbCrLf
                If rd.HasRows = True Then


                    ' TÄHÄN AUTON PERUSTIETOJA KUTE REKISTERÖINTI, KATSASTUKSIEN TIEDOT JEN
                    HH.Text &= vbTab & "HUOMIOITAVAA KATSASTUKSESTA : " & TarkistaKatsastukset(txtREK.Text) & vbCrLf & vbCrLf
                    HH.Text &= vbTab & "VIIMEISIN KM TIETO : " & TarkistaKM(txtREK.Text) & vbCrLf
                    HH.Text &= "-------------------------------------------------------------------------------" & vbCrLf
                    While rd.Read

                        tmNro = rd.GetValue(0)
                        If tmNro = edellinenTM Then GoTo seuraava
                        otsikko = rd.GetString(3)
                        HH.Text &= FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & " | " & PalautaKMtietoTMsta(tmNro).ToString & " km | " & otsikko & " [" & tmNro.ToString & "]" & vbCrLf

                        'haetaan ilmoitetut viat
                        Dim cmd_Vika As New MySqlCommand()
                        With cmd_Vika
                            .Connection = TbConnection2
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT VikaIlmoitukset.IlmoitettuVika, VikaIlmoitukset.riviID FROM VikaIlmoitukset " & _
                                "WHERE VikaIlmoitukset.LiitettyTMnro=@tm AND VikaIlmoitukset.Korjattu='1' " & _
                                "ORDER BY VikaIlmoitukset.IlmoitettuVika ASC"
                            .Parameters.AddWithValue("@tm", tmNro)
                        End With

                        TbConnection2.Open()
                        Using rdViat As MySqlDataReader = cmd_Vika.ExecuteReader
                            If rdViat.HasRows = True Then
                                HH.Text &= vbTab & "Korjatut vikailmoitukset:" & vbCrLf

                                While rdViat.Read
                                    HH.Text &= vbTab & rdViat.GetString(0) & " [#" & rdViat.GetValue(1).ToString & "]" & vbCrLf


                                End While

                            Else

                                HH.Text &= vbTab & "Ei liitettyjä vikailmoituksia" & vbCrLf & vbCrLf



                            End If
                            TbConnection2.Close()

                        End Using

                        HH.Text &= vbCrLf

                        ' haetaan tehdyt työt
                        Dim cmd_Tehtyto As New MySqlCommand()
                        With cmd_Tehtyto
                            .Connection = TbConnection2
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT Tyo FROM Tehtytyo WHERE TMnro=@tm ORDER BY ID ASC"
                            .Parameters.AddWithValue("@tm", tmNro)

                        End With

                        TbConnection2.Open()
                        Using rd_Tyo As MySqlDataReader = cmd_Tehtyto.ExecuteReader
                            If rd_Tyo.HasRows = True Then
                                HH.Text &= vbTab & "Tehtytyö ja osat:" & vbCrLf
                                While rd_Tyo.Read
                                    HH.Text &= vbTab & rd_Tyo.GetString(0) & vbCrLf

                                End While

                            Else
                                HH.Text &= vbTab & "Ei kirjattuja toimenpiteitä" & vbCrLf & vbCrLf


                            End If
                        End Using
                        TbConnection2.Close()

                        ' haetaan käytetyt osat
                        Dim cmd_osat As New MySqlCommand()
                        With cmd_osat
                            .Connection = TbConnection2
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT Osa FROM TMkaytetytOsat WHERE TMnro=@tm ORDER BY ID ASC"
                            .Parameters.AddWithValue("@tm", tmNro)

                        End With

                        TbConnection2.Open()
                        Using rd_osat As MySqlDataReader = cmd_osat.ExecuteReader
                            If rd_osat.HasRows = True Then
                                While rd_osat.Read
                                    HH.Text &= vbTab & rd_osat.GetString(0) & vbCrLf

                                End While

                            End If
                        End Using
                        TbConnection2.Close()

                        HH.Text &= vbCrLf


                        ' haetaan tehdyt työt
                        Dim cmd_Aika As New MySqlCommand()
                        With cmd_Aika
                            .Connection = TbConnection2
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Asentaja, AikaMIN FROM TyohonKaytettyAika " & _
                               "INNER JOIN Henkilosto ON TyohonKaytettyAika.HloNro=Henkilosto.HloNro " & _
                               "WHERE TyohonKaytettyAika.TMnro=@tm ORDER BY ID ASC"
                            .Parameters.AddWithValue("@tm", tmNro)

                        End With

                        TbConnection2.Open()
                        Using rd_aika As MySqlDataReader = cmd_Aika.ExecuteReader
                            If rd_aika.HasRows = True Then
                                HH.Text &= vbTab & "Tekijät(t):" & vbCrLf
                                While rd_aika.Read
                                    HH.Text &= vbTab & rd_aika.GetString(0) & " [" & MuutaMinsatTunneiksi(rd_aika.GetValue(1)) & "]" & vbCrLf
                                    KokonaisAika += rd_aika.GetValue(1)

                                End While

                            Else
                                HH.Text &= vbTab & "Ei kirjattuja tekijöitä" & vbCrLf & vbCrLf


                            End If

                            HH.Text &= vbTab & "KÄYTETTYAIKA YHTEENSÄ : " & MuutaMinsatTunneiksi(KokonaisAika).ToString & vbCrLf & _
                                "-------------------------------------------------------------------------------" & vbCrLf & vbCrLf


                        End Using
                        TbConnection2.Close()

                        KokonaisAika = 0



seuraava:
                        edellinenTM = tmNro



                    End While


                End If



            End Using








        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 6", ErrorToString, 0, 0)
            Err.Clear()

        End Try

        HH.Focus()
    End Sub
    Public Function TarkistaKatsastukset(ByVal TMrekkari As String)
        '       Dim ViimeisinLeima As String = ""
        Dim AikaaLeimaan As String = ""
        Try
            If TMrekkari = "" Then
                Return AikaaLeimaan
                Exit Function

            End If
            TbConnection3.Close()


            Dim TMautonro As String = PalautaSQLNumeroRekkarista(TMrekkari)

            Dim nytON As Date = FormatDateTime(Now, DateFormat.ShortDate)
            Dim rekkari As String = ""

            Dim erotus As TimeSpan = Nothing

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection3
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM KatsastuksienSeuranta WHERE AutoNro=@AutoNro"
                .Parameters.AddWithValue("@AutoNro", TMautonro)

            End With
            TbConnection3.Open()
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

                        '          ViimeisinLeima = FormatDateTime(rd.GetValue(3), DateFormat.ShortDate).ToString


                        erotus = CType(rd.GetValue(4), Date) - nytON
                        If erotus.TotalDays < 365 And erotus.TotalDays > 0 Then
                            ' LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ AIKAA KATSASTAA")
                            '   rekkari = TeeVikailmoitus.PalautaSQLNumeroRekkarista(rd.GetValue(1))
                            AikaaLeimaan = FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & " " & Int(erotus.TotalDays) & " " & "PÄIVÄÄ"




                        End If

                        If erotus.TotalDays = 0 Then
                            '  LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " KATSASTETTAVA TÄNÄÄN")
                            AikaaLeimaan = FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & " " & Int(erotus.TotalDays) & " " & "KATSASTETTAVA TÄNÄÄN"

                        End If

                        If erotus.TotalDays < 0 Then
                            Dim luku = -erotus.TotalDays

                            '         LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ KATSASTAMATTOMANA")
                            AikaaLeimaan = FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & " " & Int(luku) & " " & "PV KATSASTAMATTOMANA" ', AJOKIELLOSSA")

                        End If

                    End If
                    KalustoReader.Close()
                    TbConnection2.Close()

                End If


            End While
            TbConnection3.Close()
            rd.Close()
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 7", ErrorToString, 0, 0)
            Err.Clear()

        End Try


        Return AikaaLeimaan

        '       LaitaAikaJarjestykseen()
    End Function
    Public Function TarkistaKM(ByVal rekkari As String)
        Dim ViimKM As String = ""
        Try
            TbConnection3.Close()

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection3
                .CommandType = CommandType.Text
                .CommandText = "SELECT AutoNro, PVM, KMlukema FROM KMTiedot WHERE AutoNro = @AutoNro ORDER BY PVM DESC"
                .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(rekkari))

            End With
            TbConnection3.Open()
            Using rd As MySqlDataReader = cmd.ExecuteReader
                If rd.HasRows = True Then

                    rd.Read()

                    ViimKM = rd.GetValue(2).ToString & " / " & FormatDateTime(CType(rd.GetValue(1), Date), DateFormat.ShortDate).ToString


                End If
            End Using

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 8", ErrorToString, 0, 0)
            Err.Clear()

        End Try
        TbConnection3.Close()


        Return ViimKM


    End Function
    Public Function PalautaKMtietoTMsta(ByVal tm_nto As Long)
        Dim kmtieto As Long
        Try
            TbConnection2.Close()
            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = TbConnection2
                .CommandType = CommandType.Text
                .CommandText = "SELECT KMlukema FROM KMTiedot WHERE TMnro=@tm"
                .Parameters.AddWithValue("@tm", tm_nto)
            End With
            TbConnection2.Open()
            kmtieto = cmd2.ExecuteScalar

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 8", ErrorToString, 0, 0)
            Err.Clear()

        End Try
        TbConnection2.Close()
        Return kmtieto

    End Function
    Public Sub lataaHuoltoHistoriaVanhaTreeview()
        Dim newRootNode As TreeNode
        Dim NewParentNode As TreeNode
        Dim NewChild As TreeNode
        '      HH.Nodes.Clear()
        If txtREK.Text = "" Then Exit Sub
        Dim rd As MySqlDataReader
        newRootNode = New TreeNode(txtREK.Text)
        '      HH.Nodes.Add(newRootNode)
        TbConnection.Close()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT Tyomaaraykset.LisattyPVM, Tyomaaraykset.Otsikko, Tehtytyo.Tyo, TyohonKaytettyAika.HloNro, TyohonKaytettyAika.AikaMIN, " & _
                           "KMTiedot.KMlukema, Henkilosto.SukuNimi, Henkilosto.EtuNimi, VikaIlmoitukset.IlmoitettuVika, Tyomaaraykset.LuontiPVM, Tyomaaraykset.TMnro " & _
                           "FROM Tyomaaraykset " & _
                           "RIGHT JOIN Tehtytyo ON Tyomaaraykset.TMnro = Tehtytyo.TMnro " & _
                         "INNER JOIN TyohonKaytettyAika ON Tyomaaraykset.TMnro = TyohonKaytettyAika.TMnro " & _
                         "LEFT JOIN VikaIlmoitukset ON Tyomaaraykset.TMnro = VikaIlmoitukset.LiitettyTMnro " & _
                           "INNER JOIN Kalusto ON Tyomaaraykset.AutoNro = Kalusto.AutoNro " & _
                           "INNER JOIN Henkilosto ON TyohonKaytettyAika.HloNro = Henkilosto.HloNro " & _
                           "LEFT JOIN KMTiedot ON Tyomaaraykset.TMnro = KMTiedot.TMnro " & _
                           "WHERE Tyomaaraykset.AutoNro = @AutoNro " & _
                           "ORDER BY Tyomaaraykset.LuontiPVM DESC"
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(txtREK.Text))

        End With
        Dim apupvm As Date = Nothing


        '                        "INNER JOIN TMKaytetytOsat ON Tyomaaraykset.TMnro = TMkaytetytOsat.TMnro " & _
        'TMKaytetytOsat.Osa,
        TbConnection.Open()
        Try
            rd = cmd.ExecuteReader
            apupvm = Nothing

            Dim US As hsiirto = Nothing

            Dim vs As hsiirto = Nothing
            Dim asentajia(1) As String '() = Nothing
            Dim aikaakaytetty(1) As String '() = Nothing
            ''        Dim osia(1) As String ' = Nothing
            Dim toita(1) As String '= Nothing
            Dim vikoja(1) As String '() = Nothing
            If rd.HasRows = True Then


                While rd.Read
                    apupvm = rd.GetValue(0)
                    US.otsikko = rd.GetString(1)
                    US.Tyo = rd.GetString(2)
                    ''  US.osat = rd.GetString(2)
                    US.hloNro = rd.GetValue(3)
                    US.MIN = rd.GetValue(4)
                    US.km = rd.GetValue(5)
                    US.asentaja = rd.GetString(6) & " " & rd.GetValue(7)
                    US.vika = rd.GetString(8)
                    US.pvm = rd.GetValue(9)
                    If US.pvm < #1/1/2010# Then
                        US.pvm = apupvm

                    End If
                    US.TMnro = rd.GetValue(10)

                    '          MsgBox("PVM:" & vbTab & FormatDateTime(vanhaS.pvm, DateFormat.ShortDate).ToUpper & vbCrLf & _
                    '                    "Otsikko:" & vbTab & vanhaS.otsikko & vbCrLf & _
                    '                   "Tyo:" & vbTab & vanhaS.Tyo & vbCrLf & _
                    '                  "HloNro:" & vbTab & vanhaS.hloNro.ToString & vbCrLf & _
                    '                 "Asentaja" & vbTab & vanhaS.asentaja & vbCrLf & _
                    '                "MIN" & vbTab & vanhaS.MIN.ToString & vbCrLf & _
                    '               "Osat:" & vbTab & vanhaS.osat & vbCrLf & _
                    '              "KM: " & vbTab & vanhaS.km.ToString)
                    'luodaan pvm km ja otsikkorivi
                    If US.pvm = vs.pvm And US.otsikko = vs.otsikko Then
                        If US.asentaja <> "" Then
                            ReDim Preserve asentajia(0 To UBound(asentajia) + 1)
                            asentajia(UBound(asentajia)) = US.asentaja
                        End If
                        If US.MIN <> 0 Then
                            If aikaakaytetty IsNot Nothing Then
                                ReDim Preserve aikaakaytetty(0 To UBound(aikaakaytetty) + 1)
                                aikaakaytetty(UBound(aikaakaytetty)) = US.MIN
                            Else
                                ReDim Preserve aikaakaytetty(1)
                                aikaakaytetty(UBound(aikaakaytetty)) = US.MIN

                            End If
                        End If
                        ''                   If US.osat <> "" Then
                        ''ReDim Preserve osia(0 To UBound(osia) + 1)
                        ''                  osia(UBound(osia)) = US.osat
                        ''              End If
                        If US.Tyo <> "" Then
                            If toita IsNot Nothing Then
                                ReDim Preserve toita(0 To UBound(toita) + 1)
                                toita(UBound(toita)) = US.Tyo
                            Else
                                ReDim Preserve toita(1)
                                toita(UBound(toita)) = US.Tyo

                            End If
                        End If
                        If US.vika <> "" Then
                            ReDim Preserve vikoja(0 To UBound(vikoja) + 1)
                            vikoja(UBound(vikoja)) = US.vika
                        End If

                    Else
                        'jos eri lisätään vanha tauluun
                        If vs.pvm <> Nothing Then


                            NewParentNode = New TreeNode(FormatDateTime(vs.pvm, DateFormat.ShortDate).ToString & " | " & vs.km.ToString & " km | " & vs.otsikko & "[" & vs.TMnro & "]")
                            newRootNode.Nodes.Add(NewParentNode)

                            NewChild = New TreeNode("")
                            NewParentNode.Nodes.Add(NewChild)

                            If vikoja IsNot Nothing Then
                                vikoja = poistaTuplat(vikoja)
                                For i As Integer = 0 To vikoja.Count - 1
                                    NewChild = New TreeNode(vikoja(i))
                                    NewParentNode.Nodes.Add(NewChild)
                                Next
                            End If

                            NewChild = New TreeNode("")
                            NewParentNode.Nodes.Add(NewChild)

                            If toita IsNot Nothing Then
                                toita = poistaTuplat(toita)
                                For i As Integer = 0 To toita.Count - 1
                                    NewChild = New TreeNode(toita(i))
                                    NewParentNode.Nodes.Add(NewChild)
                                Next
                            End If
                            NewChild = New TreeNode("")
                            NewParentNode.Nodes.Add(NewChild)

                            ''                   If osia IsNot Nothing Then
                            ''osia = poistaTuplat(osia)
                            ''                  For i As Integer = 0 To osia.Count - 1
                            ''NewChild = New TreeNode(osia(i))
                            ''                  NewParentNode.Nodes.Add(NewChild)
                            ''                  Next
                            ''           End If
                            ''        NewChild = New TreeNode("")
                            ''         NewParentNode.Nodes.Add(NewChild)


                            If asentajia IsNot Nothing Then
                                asentajia = poistaTuplat(asentajia)
                                For i As Integer = 0 To asentajia.Count - 1
                                    If asentajia(i) <> "TUNTEMATON ASENTAJA" Then
                                        NewChild = New TreeNode(asentajia(i))
                                        NewParentNode.Nodes.Add(NewChild)
                                    End If


                                Next
                            End If
                            NewChild = New TreeNode("")
                            NewParentNode.Nodes.Add(NewChild)

                            If aikaakaytetty IsNot Nothing Then
                                aikaakaytetty = poistaTuplat(aikaakaytetty)
                                For i As Integer = 0 To aikaakaytetty.Count - 1
                                    Dim tunteina As String = MuutaMinsatTunneiksi(Val(aikaakaytetty(i)))
                                    NewChild = New TreeNode(tunteina)
                                    NewParentNode.Nodes.Add(NewChild)
                                Next
                            End If
                        End If

                        Erase toita
                        ''             Erase osia
                        Erase asentajia
                        Erase aikaakaytetty
                        Erase vikoja

                        If US.asentaja <> "" Then
                            ReDim Preserve asentajia(1)
                            asentajia(UBound(asentajia)) = US.asentaja
                        End If
                        If US.MIN <> 0 Then
                            ReDim Preserve aikaakaytetty(1)
                            aikaakaytetty(UBound(aikaakaytetty)) = US.MIN
                        End If
                        ''            If US.osat <> "" Then
                        ''ReDim Preserve osia(1)
                        ''            osia(UBound(osia)) = US.osat
                        ''        End If
                        If US.Tyo <> "" Then
                            ReDim Preserve toita(1)
                            toita(UBound(toita)) = US.Tyo
                        End If
                        If US.vika <> "" Then
                            ReDim Preserve vikoja(1)
                            vikoja(UBound(vikoja)) = US.vika
                        End If


                    End If






                    vs = US

                End While
            End If
            TbConnection.Close()

            '    Dim tn As TreeNode
            '     For Each tn In HH.Nodes
            'If tn.Text = txtREK.Text Then
            'HH.SelectedNode = tn
            '        HH.SelectedNode.Expand()
            '
            '        Exit For
            '       End If
            '       Next
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 9", ErrorToString, 0, 0)
            Err.Clear()

        End Try

        '    HH.ExpandAll()

    End Sub
    Function poistaTuplat(ByVal initialArray As String()) As String()
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim newArray(0) As String
        Try
            For i = 0 To UBound(initialArray)
                For j = 0 To UBound(initialArray)
                    If Not initialArray(i) = "" Then
                        If Not j = i Then
                            If initialArray(i) = initialArray(j) Then
                                initialArray(j) = ""
                            End If
                        End If
                    End If
                Next
            Next

            j = 0
            For i = 0 To UBound(initialArray)
                If Not initialArray(i) = "" Then
                    ReDim Preserve newArray(j)
                    newArray(j) = initialArray(i)
                    j = j + 1
                End If
            Next
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 10", ErrorToString, 0, 0)
            Err.Clear()

        End Try


        Return newArray

    End Function
    Public Structure hsiirto
        Dim pvm As Date
        Dim Tyo As String
        Dim osat As String
        Dim hloNro As Integer
        Dim MIN As String
        Dim asentaja As String
        Dim otsikko As String
        Dim km As Integer
        Dim vika As String
        Dim TMnro As Long

    End Structure

    Private Sub txtREK_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtREK.SelectedIndexChanged
        Me.Cursor = Cursors.WaitCursor
        If txtREK.Text = "" Then Exit Sub

        Try
            If RekkariLoytyy(txtREK.Text) = True Then
                lbViat.Enabled = True
                '   If lbViat.Items.Count < 5 Then
                'HaeValmisViatNakyviin()

                'End If
                LataaHuoltohistoria()
                HaeIlmoitetutViat()
                lbViat.Focus()

            End If
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 11", ErrorToString, 0, 0)
            Err.Clear()

        End Try

        Me.Cursor = Cursors.Default
        OletusAuto = txtREK.Text

    End Sub

    Private Sub btVaihdaAuto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtREK.Enabled = True
        lbViat.Enabled = False
        Lisättävävika.Text = ""
        txtREK.Focus()
        DGV.DataSource = Nothing
        DGV.DataMember = Nothing

    End Sub

    Private Sub txtREK_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtREK.TextChanged
        '      If Len(txtREK.Text) = 7 Then
        'jos rekkari löytyy laita viat näkyviin

        'If RekkariLoytyy(txtREK.Text) = True Then
        'lbViat.Enabled = True
        'btVaihdaAuto.Visible = True
        'txtREK.Enabled = False
        'lbViat.Focus()

        'End If
        'End If
        If txtREK.Text = "" Then
            lbViat.Enabled = False
            txtREK.Enabled = True
            btTallenna.Visible = False
            DGV.DataSource = Nothing
            DGV.DataMember = Nothing

        End If
    End Sub

    Private Sub lbViat_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lbViat.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lisättävävika.Text = lbViat.Text
            btTallenna.Visible = True
            btTallenna.Focus()

        End If

    End Sub

    Private Sub lbViat_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lbViat.MouseDoubleClick
        Lisättävävika.Text = lbViat.Text
        btTallenna.Visible = True
        btTallenna.Focus()


    End Sub

    Private Sub lbViat_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbViat.SelectedIndexChanged

    End Sub

    Private Sub Lisättävävika_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Lisättävävika.TextChanged
        If Len(Lisättävävika.Text) >= 6 Then

            btTallenna.Visible = True
        Else
            btTallenna.Visible = False

        End If


    End Sub

    Private Sub btTallenna_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btTallenna.Click
        If Lisättävävika.Text = "" Then Exit Sub
        Try

            If DGV.Rows.Count > 1 Then
                For i As Integer = 0 To DGV.Rows.Count - 1
                    '       Dim dg As New DataGridViewRow

                    '  If DGV.Item(1, i).Value.ToString = Lisättävävika.Text Then
                    If DGV.Item(1, i).Value = Lisättävävika.Text Then
                        Main.TopMost = False

                        MsgBox("VIKA ON JO LISÄTTY")
                        Main.TopMost = True

                        Exit Sub

                    End If

                Next
            End If
            TbConnection.Close()
            Dim cmd As New MySqlCommand()
            cmd.Connection = TbConnection
            cmd.CommandText = "INSERT INTO VikaIlmoitukset " & _
                                "(IlmoitettuVika, AutoNro, HloNro, strAUTO, strHLO, IlmPVM, VikaIlmoituksenTilaID, strIlmoituksenTila, strKiirreellisyys, Korjattu, LiitettyTMnro)" & _
                                                   "VALUES(@IlmoitettuVika, @AutoNro, @HloNro, @strAUTO, @strHLO, @IlmPVM, @VikaIlmoituksenTilaID, @strIlmoituksenTila, @strKiirreellisyys, @Korjattu, @LiitettyTMnro)"

            cmd.Parameters.AddWithValue("@IlmoitettuVika", Lisättävävika.Text)
            cmd.Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(txtREK.Text))
            cmd.Parameters.AddWithValue("@HloNro", KayttajaHloNro)
            cmd.Parameters.AddWithValue("@strAUTO", txtREK.Text)
            cmd.Parameters.AddWithValue("@strHLO", KayttajaNimi)
            cmd.Parameters.AddWithValue("@IlmPVM", CType(Now, Date))
            cmd.Parameters.AddWithValue("@VikaIlmoituksenTilaID", 1)
            cmd.Parameters.AddWithValue("@strIlmoituksenTila", "")
            cmd.Parameters.AddWithValue("@strKiirreellisyys", "LUOKITTELEMATON")
            cmd.Parameters.AddWithValue("@Korjattu", False)
            cmd.Parameters.AddWithValue("@LiitettyTMnro", 0)
            LokiTapahtumanTallennus(KayttajaHloNro, "Lisätty vikailmoitus " & Lisättävävika.Text & " / " & "" & " / " & "", "", KayttajaHloNro, PalautaSQLNumeroRekkarista(txtREK.Text))


            TbConnection.Open()
            cmd.ExecuteNonQuery()
            TbConnection.Close()

            HaeIlmoitetutViat()

            Lisättävävika.Text = ""

            lbViat.Focus()
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 12", ErrorToString, 0, 0)
            Err.Clear()

        End Try

        ' LISÄTÄÄN UUSI VIKA




    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim postia As String = ""
        Try
            TbConnection.Close()

            postia = "ILMOITETUT VIAT " & vbTab & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString & vbTab & PalautaViikonPaiva(Today.AddDays(-1)) & vbCrLf & vbCrLf

            Dim rd As New MySqlCommand()
            Dim dAl As Date = Format(Today.AddDays(-1), "\#yyyy\-MM\-dd\#")

            With rd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT Vikailmoitukset.AutoNro, Vikailmoitukset.IlmoitettuVika, Henkilosto.SukuNimi, " & _
                "Henkilosto.EtuNimi, Kalusto.RekNro FROM Vikailmoitukset INNER JOIN Henkilosto ON Henkilosto.HloNro = Vikailmoitukset.HloNro " & _
                "INNER JOIN Kalusto ON Kalusto.AutoNro = Vikailmoitukset.AutoNro " & _
                "WHERE Vikailmoitukset.IlmPVM = @Aika ORDER BY Kalusto.RekNro ASC"
                .Parameters.AddWithValue("@Aika", dAl)

            End With
            TbConnection.Open()


            Dim luku As MySqlDataReader = rd.ExecuteReader

            If luku.HasRows = True Then

                While luku.Read

                    postia &= luku.GetString(4) & " (" & luku.GetValue(0) & ") " & vbTab & luku.GetString(1) & vbTab & "[" & luku.GetString(2) & " " & _
                        luku.GetString(3) & "]" & vbCrLf


                End While

            End If
            postiteksti.Text = postia

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TEE VIKAILMOITUS 13", ErrorToString, 0, 0)
            Err.Clear()

        End Try

        Try
            TbConnection.Close()
            LahetaSpostia("Joakim Selander", "joakim.selander@taksikuljetus.fi", "VIKAILMOITUKSET " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString, postia)
            LahetaSpostia("Hannu Virtanen", "hannu.virtanen@taksikuljetus.fi", "VIKAILMOITUKSET " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString, postia)
            LahetaSpostia("Jorma Kaukoranta", "jorma.kaukoranta@hotmail.com", "VIKAILMOITUKSET " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString, postia)
            LahetaSpostia("Lasse Tarvainen", "lasse.tarvainen@taksikuljetus.fi", "VIKAILMOITUKSET " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString, postia)

        Catch ex As Exception
            Err.Clear()

        End Try

    End Sub

    Private Sub TeeVikailmoitus_LostFocus(sender As Object, e As System.EventArgs) Handles Me.LostFocus
        '   Me.Close()

    End Sub
End Class