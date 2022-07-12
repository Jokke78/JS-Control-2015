Imports MySql.Data.MySqlClient
Public Class ViewHuoltohistoriaUUSI
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection2 As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection3 As MySqlConnection = New MySqlConnection(serverString)

    Private Sub ViewHuoltohistoriaUUSI_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LataaAutot()
        If OletusAuto <> "" Then cbRekNro.Text = OletusAuto

    End Sub
    Public Sub LataaAutot()

        Try
            TbConnection.Close()
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT RekNro FROM Kalusto WHERE Raporteissa = @Raporteissa ORDER BY RekNro ASC"
                .Parameters.AddWithValue("@Raporteissa", True)

            End With

            TbConnection.Open()
            Dim rd As MySqlDataReader = cmd.ExecuteReader

            If rd.HasRows = True Then
                While rd.Read
                    cbRekNro.Items.Add(rd.GetString(0))
                End While
            End If
        Catch ex As Exception
            Err.Clear()
        Finally
            TbConnection.Close()

        End Try




    End Sub
    Private Sub cbRekNro_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbRekNro.SelectedIndexChanged
        If cbRekNro.Text = "" Then Exit Sub
        Me.Cursor = Cursors.WaitCursor

        lataaHuoltoHistoria()
        lataakirjatutHuollot()
        Me.Cursor = Cursors.Default
        OletusAuto = cbRekNro.Text

    End Sub
    Public Function TarkistaKatsastukset(ByVal TMrekkari As String)
        '       Dim ViimeisinLeima As String = ""
        Dim AikaaLeimaan As String = ""
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
            Err.Clear()

        End Try
        TbConnection3.Close()


        Return ViimKM


    End Function
    Public Sub LataaHuoltohistoria()
        HH.Text = ""
        Dim autoNro As Integer = PalautaSQLNumeroRekkarista(cbRekNro.Text)
        Dim tmNro As Long = 0
        Dim kmTieto As Long = 0
        Dim otsikko As String = ""
        Dim edellinenTM As Long = 0
        Dim KokonaisAika As Integer = 0

        '     Try
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

            HH.Text = "HUOLTOHISTORIA" & vbTab & cbRekNro.Text & " [#" & autoNro.ToString & " ]" & vbTab & FormatDateTime(Today, DateFormat.ShortDate).ToString & vbCrLf & vbCrLf
            If rd.HasRows = True Then


                ' TÄHÄN AUTON PERUSTIETOJA KUTE REKISTERÖINTI, KATSASTUKSIEN TIEDOT JEN
                HH.Text &= vbTab & "HUOMIOITAVAA KATSASTUKSESTA : " & TarkistaKatsastukset(cbRekNro.Text) & vbCrLf & vbCrLf
                HH.Text &= vbTab & "VIIMEISIN KM TIETO : " & TarkistaKM(cbRekNro.Text) & vbCrLf
                HH.Text &= "-------------------------------------------------------------------------------" & vbCrLf
                While rd.Read

                    tmNro = rd.GetValue(0)
                    If tmNro = edellinenTM Then GoTo seuraava
                    otsikko = rd.GetString(2)
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








        '  Catch ex As Exception
        '      Err.Clear()

        '   End Try

        HH.Focus()


    End Sub
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
            Err.Clear()

        End Try
        TbConnection2.Close()
        Return kmtieto

    End Function
    Public Sub lataakirjatutHuollot()
        TbConnection.Close()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM HuoltojenSeuranta WHERE RekNro = @RekNro ORDER BY PVM DESC"
            .Parameters.AddWithValue("@RekNro", PalautaSQLNumeroRekkarista(cbRekNro.Text))
        End With
        KirjatutHuollot.Items.Clear()

        TbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then

            While rd.Read
                Dim huollonTyyppi As String = ""
                If rd.GetValue(5) = True Then KirjatutHuollot.Items.Add(FormatDateTime(rd.GetValue(1), DateFormat.ShortDate).ToString & " | " & rd.GetValue(3).ToString & " km | " & "MOOTTORIÖLJYT")
                If rd.GetValue(6) = True Then KirjatutHuollot.Items.Add(FormatDateTime(rd.GetValue(1), DateFormat.ShortDate).ToString & " | " & rd.GetValue(3).ToString & " km | " & "VAIHTEISTOÖLJYT")
                If rd.GetValue(7) = True Then KirjatutHuollot.Items.Add(FormatDateTime(rd.GetValue(1), DateFormat.ShortDate).ToString & " | " & rd.GetValue(3).ToString & " km | " & "PERÄÖLJYT")
                If rd.GetValue(8) = True Then KirjatutHuollot.Items.Add(FormatDateTime(rd.GetValue(1), DateFormat.ShortDate).ToString & " | " & rd.GetValue(3).ToString & " km | " & "ILMASTOINTIHUOLTO")
                If rd.GetValue(9) = True Then KirjatutHuollot.Items.Add(FormatDateTime(rd.GetValue(1), DateFormat.ShortDate).ToString & " | " & rd.GetValue(3).ToString & " km | " & "NAFTASUODATIN VAIHDETTU")
                If rd.GetValue(10) = True Then KirjatutHuollot.Items.Add(FormatDateTime(rd.GetValue(1), DateFormat.ShortDate).ToString & " | " & rd.GetValue(3).ToString & " km | " & "RAITISILMASUODATIN VAIHDETTU")



            End While
        End If
        rd.Close()
        TbConnection.Close()

    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub
End Class