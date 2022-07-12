Imports MySql.Data.MySqlClient
Public Class Etatulostus
    Private myYhteys As MySqlConnection = New MySqlConnection(serverString)
    Private myYhteys2 As MySqlConnection = New MySqlConnection(serverString)
    Private myYhteys3 As MySqlConnection = New MySqlConnection(serverString)
    Private myYhteys22 As MySqlConnection = New MySqlConnection(serverString)
    Private myYhteys122 As MySqlConnection = New MySqlConnection(serverString)

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub Etatulostus_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Main.Enabled = False

        myYhteys3.Close()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys3
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Etatulostus WHERE Tulostettu =0"
        End With
        myYhteys3.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then
            While rd.Read
                Dim riviID As Long = rd.GetValue(0)

                Select Case rd.GetValue(2)

                    Case 1

                        Dim tulosteennumero As Integer = rd.GetValue(3)
                     
                        TulostaTM(riviID, tulosteennumero)

                End Select


            End While
        End If

        rd.Dispose()

        myYhteys3.Close()
        Main.Enabled = True
   
        Me.Dispose()

    End Sub

    Public Sub TulostaTM(ByVal riviID As Long, ByVal tulosteennumero As Integer)
        Try

        myYhteys.Close()
        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT LuontiPVM, LuontiHlo, AutoNro, Otsikko, Osoitettu FROM Tyomaaraykset WHERE TMnro=@nro"
            .Parameters.AddWithValue("@nro", tulosteennumero)
        End With
        myYhteys.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        rd.Read()

        Dim luontipvm As Date = rd.GetValue(0)
        Dim luontiHloNro As Integer = rd.GetValue(1)
        Dim autoNro As Integer = rd.GetValue(2)
        Dim otsikko As String = rd.GetString(3)
        Dim osoitettu As Integer = rd.GetValue(4)
        Dim osoitettuNimi As String = ""
        If osoitettu <> 0 Then
            osoitettuNimi = PalautaSQLNrostaNimi(osoitettu)
        End If

        rd.Dispose()
        myYhteys.Close()

        TulostaTyoMaaraus.Location = New Point(0, 0)
        TulostaTyoMaaraus.Timer1.Enabled = False

        TulostaTyoMaaraus.Show()
        Dim autonrekkari As String = PalautaSQLRekkariNumerosta(autoNro)

        TulostaTyoMaaraus.TMnrojaPVM.Text = tulosteennumero
        TulostaTyoMaaraus.rekjaNro.Text = autonrekkari & " (" & autoNro & ")"
        TulostaTyoMaaraus.rekpvm.Text = PalautaAutonNrostaRekisterointiPVM(autoNro)
        TulostaTyoMaaraus.Label3.Text = TarkistaHuollot(autonrekkari)
        'TulostaTyoMaaraus.katsastettu.Text = ViimeisinLeima.Text
        'TulostaTyoMaaraus.aikaaleimaan.Text = AikaaLeimaan.Text
        TarkistaKatsastukset(autoNro)
        TulostaTyoMaaraus.kmlukemaJApvm.Text = TarkistaKM(autoNro)
        TulostaTyoMaaraus.varattuPVM.Text = FormatDateTime(Now, DateFormat.ShortDate).ToString
        TulostaTyoMaaraus.Laatija.Text = PalautaSQLNrostaNimi(luontiHloNro) 'Kayttaja.Sukunimi & " " & Kayttaja.Etunimi
        '   TulostaTyoMaaraus.asentaja.Text = Asentajalle.Text
        TulostaTyoMaaraus.huomioitavaa.Text = otsikko
        TulostaTyoMaaraus.asentaja.Text = osoitettuNimi

        myYhteys122.Close()

        Dim cmd2 As New MySqlCommand()
        With cmd2
            .Connection = myYhteys122
            .CommandType = CommandType.Text
            .CommandText = "SELECT IlmoitettuVika, IlmPVM FROM VikaIlmoitukset WHERE LiitettyTMnro=@nro"
            .Parameters.AddWithValue("@nro", tulosteennumero)
        End With
        myYhteys122.Open()
        Dim rd2 As MySqlDataReader = cmd2.ExecuteReader

        If rd2.HasRows = True Then
            Dim i As Integer = 0

            While rd2.Read

                Dim lisattavavika As String = rd2.GetString(0)

                Dim ilmpvm As String = rd2.GetValue(1)
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
                i += 1


            End While


        End If
        myYhteys122.Close()
        TulostaTyoMaaraus.Refresh()

        TulostaTyoMaaraus.TextBox1.Focus()
        TulostaTyoMaaraus.tulosta()


        '      btTulosta.Visible = False
        Dim cmd3 As New MySqlCommand()
        With cmd3
            .Connection = myYhteys122
            .CommandType = CommandType.Text
            .CommandText = "UPDATE Etatulostus SET Tulostettu=1 WHERE riviID=@rivi"
            .Parameters.AddWithValue("@rivi", riviID)
        End With
        myYhteys122.Open()
        cmd3.ExecuteNonQuery()
        Catch ex As Exception
            Err.Clear()
        Finally
            myYhteys122.Close()

        End Try


    End Sub
    Public Sub TarkistaKatsastukset(ByVal autoNro As Integer)
        myYhteys2.Close()


        Dim nytON As Date = FormatDateTime(Now, DateFormat.ShortDate)
        Dim rekkari As String = ""

        Dim erotus As TimeSpan = Nothing

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys2
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM KatsastuksienSeuranta WHERE AutoNro=@AutoNro"
            .Parameters.AddWithValue("@AutoNro", autoNro)

        End With
        myYhteys2.Open()
        Try
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then

                While rd.Read

                    'tarkistetaan onko katsastusvahti käytössä
                    Dim cmKAT As New MySqlCommand()
                    myYhteys22.Close()
                    With cmKAT
                        .Connection = myYhteys22
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT * FROM Kalusto WHERE AutoNro = @AutoNro"
                        .Parameters.AddWithValue("@AutoNro", rd.GetValue(1))

                    End With
                    myYhteys22.Open()
                    Dim KalustoReader As MySqlDataReader = cmKAT.ExecuteReader
                    If KalustoReader.HasRows = True Then
                        KalustoReader.Read()
                        If KalustoReader.GetValue(14) = True Then

                            TulostaTyoMaaraus.katsastettu.Text = FormatDateTime(rd.GetValue(3), DateFormat.ShortDate).ToString


                            erotus = CType(rd.GetValue(4), Date) - nytON
                            If erotus.TotalDays < 365 And erotus.TotalDays > 0 Then
                                ' LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ AIKAA KATSASTAA")
                                '   rekkari = TeeVikailmoitus.PalautaSQLNumeroRekkarista(rd.GetValue(1))
                                TulostaTyoMaaraus.aikaaleimaan.Text = FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & " " & Int(erotus.TotalDays) & " " & "PÄIVÄÄ"



                            End If

                            If erotus.TotalDays = 0 Then
                                '  LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " KATSASTETTAVA TÄNÄÄN")
                                TulostaTyoMaaraus.aikaaleimaan.Text = FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & " " & Int(erotus.TotalDays) & " " & "KATSASTETTAVA TÄNÄÄN"

                            End If

                            If erotus.TotalDays < 0 Then
                                Dim luku = -erotus.TotalDays

                                '         LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ KATSASTAMATTOMANA")
                                TulostaTyoMaaraus.aikaaleimaan.Text = FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & " " & Int(luku) & " " & "PV KATSASTAMATTOMANA" ', AJOKIELLOSSA")

                            End If

                        End If
                        KalustoReader.Close()
                        myYhteys22.Close()

                    End If


                End While
                myYhteys2.Close()
                rd.Dispose()
            End If

 Catch ex As Exception
            MsgBox(ex)

        End Try



        '       LaitaAikaJarjestykseen()
    End Sub
    Public Function TarkistaKM(ByVal autonro As Integer)
        myYhteys2.Close()
        Dim palautus As String = ""

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys2
            .CommandType = CommandType.Text
            .CommandText = "SELECT AutoNro, PVM, KMlukema FROM KMTiedot WHERE AutoNro = @AutoNro ORDER BY KMlukema DESC"
            .Parameters.AddWithValue("@AutoNro", autonro)

        End With
        myYhteys2.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then

            rd.Read()

            palautus = rd.GetValue(2).ToString & " / " & FormatDateTime(CType(rd.GetValue(1), Date), DateFormat.ShortDate).ToString


        End If
        myYhteys2.Close()
        rd.Dispose()

        Return palautus

    End Function
    Public Function TarkistaHuollot(ByVal rekkari As String)
        myYhteys2.Close()

        Dim palautus As String = ""

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys2
            .CommandType = CommandType.Text
            .CommandText = "SELECT RekNro, MoottoriOljyt, PVM, KMlukema FROM HuoltojenSeuranta WHERE RekNro = @AutoNro ORDER BY PVM DESC"
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(rekkari))

        End With
        myYhteys2.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then

            rd.Read()

            palautus = rd.GetValue(3).ToString & " / " & FormatDateTime(CType(rd.GetValue(2), Date), DateFormat.ShortDate).ToString


        End If
        myYhteys2.Close()
        rd.Dispose()
        Return palautus

    End Function
End Class