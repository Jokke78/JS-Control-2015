Imports MySql.Data.MySqlClient
Imports System.Net.NetworkInformation
Imports System.Management


Module Structuret
    '   Public serverString As String = "server=164.215.32.141;user id=sg1949_joakim; password=uqrh6755; database=sg1949_JSControl"
    'alla käytössä 18.9.2016 asti oleva ip
    ' Public serverString As String = "server=164.215.32.141;user id=sg1949_joakim; password=uqrh6755; database=sg1949_JSControl; Convert Zero Datetime=True" '; AllowZeroDatetime=True"
    'alla 18.9. alkaen uusi ip
    Public serverString As String = "server=95.175.101.201;user id=sg1949_joakim; password=uqrh6755; database=sg1949_JSControl; Convert Zero Datetime=True" '; AllowZeroDatetime=True"


    Public OletusAuto As String = ""

    '   Public COMportti As String
    Public KeyPath As Boolean
    Public PysakointiPalvelut As Boolean

    Private myTbConnection55 As MySqlConnection = New MySqlConnection(serverString)
    Private myTbConnection199 As MySqlConnection = New MySqlConnection(serverString)
    Private myTbConnection99 As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection27 As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection127 As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection199 As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection1991 As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnectionSMS As MySqlConnection = New MySqlConnection(serverString)
    Private lokiConnection As MySqlConnection = New MySqlConnection(serverString)
    Public KoneenTunnisteMAC As String = ""
    Public AjastetutRaportit As Raporteja

    Public sivu As String
    Public KayttajanTaso As String = ""
    Public KayttajanAuto As Integer = 0
    Public KayttajaHloNro As Integer = 0
    Public KayttajanSalasana As String = ""
    Public KayttajaNimi As String = ""
    Public PestytAutotMaara As Integer
    Public KirjautumisID As Integer = 0
    Public TietokantaRiviMaara As Long
    Public tietokannanTapahtuma As Integer
    Public Kayttaja As KayttajanTiedot

    Public Structure KayttajanTiedot
        'JSCONtrol 2016 käyttäjä tunnisteet

        Dim ID As Integer
        Dim Sukunimi As String
        Dim Etunimi As String
        Dim eMail As String
        Dim AlkaenPVM As Date
        Dim AstiPVM As Date
        Dim Taso As Integer ' Controlista poiketen arvo on INT: 9=pääkäyttäjä 1=kuljettaja
        Dim HloNro As Integer


        Dim Lahtolista_Kirjautumisero As Boolean
        Dim Graafinen_tagimuokkaus As Boolean
        Dim Lahtolista_PaallekkaisetKuljettajat As Boolean
        Dim Lahtolista_Osastojako As Boolean
        Dim lahtolista_AutojenVikailmoituksientarkastaminen As Boolean
        Dim eMail_AutojenpysakointiAlueet As Boolean
        Dim lahtolista_TyoOhjeenNayttaminen As Boolean
        Dim Henkilosto_Lisatoimet As Boolean
        Dim SalliTyojaksonMuokkausJaluonti As Boolean
        Dim Lahtolista_TVhistorianSeuranta As Boolean
        Dim Lahtolista_OnkoKuljettajaaInformoitu As Boolean
        Dim PoistuminenLisatoimet As Boolean
        Dim HH_emailiin As Boolean

        '
        '
        '1
        '2
        '3 = Kuljettaja
        '4 = Asentaja
        '5 = Hallipäivystäjä
        '6 = Hallimestari (Laajennettu Hallipäivystäjä)
        '7 = Esimies
        '8 = Johtaja
        '9 = Pääkäyttäjä

    End Structure




    Public Function palautakuljettajanEsimies(ByVal HhoNro As Integer)
        Dim paluu As Integer
        TbConnection27.Close()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection27
            .CommandType = CommandType.Text
            .CommandText = "SELECT Esimies FROM Henkilosto WHERE HloNro=@Hlo"
            .Parameters.AddWithValue("@Hlo", HhoNro)

        End With
        TbConnection27.Open()
        paluu = cmd.ExecuteScalar
        TbConnection27.Close()




        Return paluu

    End Function







    Public Structure Raporteja
        Dim klo1 As String
        Dim klo2 As String
        Dim klo3 As String
        Dim klo4 As String

    End Structure
    Public Function TallennaKatsastuksenPERUUTUS(ByVal AutoNro As Integer, ByVal Hyvaksytty As Boolean, ByVal AjoaikaaAsti As Date, ByVal KatsastajaHloNro As Integer, ByVal KatsastusPVm As Date)
        Dim onnnistui As Boolean = False

        TbConnection199.Close()
        Dim leima As New MySqlCommand()
        With leima
            .Connection = TbConnection199
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM KatsastuksienSeuranta WHERE AutoNro = @AutoNro"
            .Parameters.AddWithValue("@AutoNro", AutoNro)
        End With
        TbConnection199.Open()

        Dim rdLeimat As MySqlDataReader = leima.ExecuteReader

        If rdLeimat.HasRows = True Then
            rdLeimat.Read()
            Dim vanhaPV As Date = rdLeimat.GetValue(4)
            If Hyvaksytty = True Then vanhaPV = vanhaPV.AddYears(-1)

            Dim UusiLeimaus As New MySqlCommand()
            With UusiLeimaus
                .Connection = TbConnection1991
                .CommandType = CommandType.Text
                .CommandText = "UPDATE KatsastuksienSeuranta " & _
                               "SET HloNro = @HloNro, PVM = @PVM, SeuraavaKatsastusPVM = @SeuraavaKatsastusPVM, AjoaikaaAsti = @AjoaikaaAsti " & _
                               "WHERE RiviID = @RiviID"
                .Parameters.AddWithValue("@RiviID", rdLeimat.GetValue(0))
                .Parameters.AddWithValue("@HloNro", KatsastajaHloNro)
                .Parameters.AddWithValue("@SeuraavaKatsastusPVM", CType(vanhaPV, Date))
                If Hyvaksytty = True Then .Parameters.AddWithValue("@AjoaikaaAsti", DBNull.Value) Else .Parameters.AddWithValue("@AjoaikaaAsti", CType(AjoaikaaAsti, Date))
                .Parameters.AddWithValue("@PVM", CType(KatsastusPVm, Date))

            End With

            TbConnection1991.Open()
            UusiLeimaus.ExecuteNonQuery()
            TbConnection1991.Close()
            onnnistui = True

        Else
            rdLeimat.Read()
            Dim REKPV As Date = PalautaAutonNrostaRekisterointiPVM(AutoNro)

            Dim repPP As Integer = REKPV.Day
            Dim repKK As Integer = REKPV.Month
            Dim vuosinyt As Integer = Now.Year
            vuosinyt -= 1

            Dim tehdaanPVM As String = repPP.ToString & "." & repKK.ToString & "." & vuosinyt.ToString



            Dim UusiLeimaus As New MySqlCommand()
            With UusiLeimaus
                .Connection = TbConnection1991
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO KatsastuksienSeuranta " & _
                               "(HloNro, PVM, SeuraavaKatsastusPVM, AjoaikaaAsti, AutoNro)  " & _
                               "VALUES(@HloNro, @PVM, @SeuraavaKatsastusPVM, @AjoaikaaAsti, @AutoNro)"
                .Parameters.AddWithValue("@AutoNro", AutoNro)
                .Parameters.AddWithValue("@HloNro", KatsastajaHloNro)
                .Parameters.AddWithValue("@SeuraavaKatsastusPVM", CType(tehdaanPVM, Date))
                If Hyvaksytty = True Then .Parameters.AddWithValue("@AjoaikaaAsti", DBNull.Value) Else .Parameters.AddWithValue("@AjoaikaaAsti", CType(AjoaikaaAsti, Date))
                .Parameters.AddWithValue("@PVM", CType(KatsastusPVm, Date))

            End With

            TbConnection1991.Open()
            UusiLeimaus.ExecuteNonQuery()
            TbConnection1991.Close()
            onnnistui = True

        End If


        Return onnnistui

    End Function
    Public Sub LahetäSMSViestiOdottamaanLehettamista(ByVal VastOttajanPuh As String, ByVal LahettevaViesti As String, ByVal LahettajanHloNro As String)
        Try
            TbConnectionSMS.Close()
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnectionSMS
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO SMStoiminta " & _
                    "(Kasitelty, LahettajanPuhNro, Viesti, OdottaaLahettamista, ViestinTekijaHloNro, SaapunutAika) " & _
                    "VALUES(@Kasitelty, @LahettajanPuhNro, @Viesti, @OdottaaLahettamista, @ViestinTekijaHloNro, @SaapunutAika)"
                .Parameters.AddWithValue("@Kasitelty", True)
                .Parameters.AddWithValue("@LahettajanPuhNro", VastOttajanPuh)
                .Parameters.AddWithValue("@Viesti", LahettevaViesti)
                .Parameters.AddWithValue("@OdottaaLahettamista", True)
                .Parameters.AddWithValue("@ViestinTekijaHloNro", LahettajanHloNro)
                .Parameters.AddWithValue("@SaapunutAika", CType(Now, DateTime))



            End With

            Try
                TbConnectionSMS.Open()
                cmd.ExecuteNonQuery()
                TbConnectionSMS.Close()

            Catch ex As Exception
                Err.Clear()

            End Try
        Catch ex As Exception
            Err.Clear()

        End Try

    End Sub

    Public Function TallennaKatsastus(ByVal AutoNro As Integer, ByVal Hyvaksytty As Boolean, ByVal AjoaikaaAsti As Date, ByVal KatsastajaHloNro As Integer, ByVal KatsastusPVm As Date)
        Dim onnnistui As Boolean = False
        Try
            TbConnection199.Close()
            Dim leima As New MySqlCommand()
            With leima
                .Connection = TbConnection199
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM KatsastuksienSeuranta WHERE AutoNro = @AutoNro"
                .Parameters.AddWithValue("@AutoNro", AutoNro)
            End With
            TbConnection199.Open()

            Dim rdLeimat As MySqlDataReader = leima.ExecuteReader

            If rdLeimat.HasRows = True Then
                rdLeimat.Read()
                Dim vanhaPV As Date = rdLeimat.GetValue(4)
                If Hyvaksytty = True Then vanhaPV = vanhaPV.AddYears(1)

                Dim UusiLeimaus As New MySqlCommand()
                With UusiLeimaus
                    .Connection = TbConnection1991
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE KatsastuksienSeuranta " & _
                                   "SET HloNro = @HloNro, PVM = @PVM, SeuraavaKatsastusPVM = @SeuraavaKatsastusPVM, AjoaikaaAsti = @AjoaikaaAsti " & _
                                   "WHERE RiviID = @RiviID"
                    .Parameters.AddWithValue("@RiviID", rdLeimat.GetValue(0))
                    .Parameters.AddWithValue("@HloNro", KatsastajaHloNro)
                    .Parameters.AddWithValue("@SeuraavaKatsastusPVM", CType(vanhaPV, Date))
                    If Hyvaksytty = True Then .Parameters.AddWithValue("@AjoaikaaAsti", DBNull.Value) Else .Parameters.AddWithValue("@AjoaikaaAsti", CType(AjoaikaaAsti, Date))
                    .Parameters.AddWithValue("@PVM", CType(KatsastusPVm, Date))

                End With

                TbConnection1991.Open()
                UusiLeimaus.ExecuteNonQuery()
                TbConnection1991.Close()
                onnnistui = True

            Else
                rdLeimat.Read()
                Dim REKPV As Date = PalautaAutonNrostaRekisterointiPVM(AutoNro)

                Dim repPP As Integer = REKPV.Day
                Dim repKK As Integer = REKPV.Month
                Dim vuosinyt As Integer = Now.Year
                vuosinyt += 1

                Dim tehdaanPVM As String = repPP.ToString & "." & repKK.ToString & "." & vuosinyt.ToString



                Dim UusiLeimaus As New MySqlCommand()
                With UusiLeimaus
                    .Connection = TbConnection1991
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO KatsastuksienSeuranta " & _
                                   "(HloNro, PVM, SeuraavaKatsastusPVM, AjoaikaaAsti, AutoNro)  " & _
                                   "VALUES(@HloNro, @PVM, @SeuraavaKatsastusPVM, @AjoaikaaAsti, @AutoNro)"
                    .Parameters.AddWithValue("@AutoNro", AutoNro)
                    .Parameters.AddWithValue("@HloNro", KatsastajaHloNro)
                    .Parameters.AddWithValue("@SeuraavaKatsastusPVM", CType(tehdaanPVM, Date))
                    If Hyvaksytty = True Then .Parameters.AddWithValue("@AjoaikaaAsti", DBNull.Value) Else .Parameters.AddWithValue("@AjoaikaaAsti", CType(AjoaikaaAsti, Date))
                    .Parameters.AddWithValue("@PVM", CType(KatsastusPVm, Date))

                End With

                TbConnection1991.Open()
                UusiLeimaus.ExecuteNonQuery()
                TbConnection1991.Close()
                onnnistui = True

            End If
            TallennaTietokantaToiminto(3)
        Catch ex As Exception
            Err.Clear()

        End Try


        Return onnnistui

    End Function
    Public Function ErotteleAika(ByRef kellonaika As String) As String
        Dim eroteltu As String = ""
        Dim alkuosa As String = ""
        Dim loppuosa As String = ""
        If kellonaika = "1/2" Then eroteltu = "00:30" : Return eroteltu : Exit Function
        If kellonaika = "1/4" Then eroteltu = "00:15" : Return eroteltu : Exit Function
        If kellonaika = "3/4" Then eroteltu = "00:45" : Return eroteltu : Exit Function
        If kellonaika = "1/3" Then eroteltu = "00:20" : Return eroteltu : Exit Function
        If kellonaika = "2/3" Then eroteltu = "00:40" : Return eroteltu : Exit Function
        If kellonaika = "1/6" Then eroteltu = "00:10" : Return eroteltu : Exit Function


        Dim pituus As Integer = Len(kellonaika)

        Dim kaksoispisteOn As Boolean = Nothing

        For i = 1 To pituus
            If Mid(kellonaika, i, 1) = ":" Or Mid(kellonaika, i, 1) = "." Or Mid(kellonaika, i, 1) = "," Or Mid(kellonaika, i, 1) = ";" Or Mid(kellonaika, i, 1) = "-" Then
                kaksoispisteOn = True
                Exit For
            Else
                kaksoispisteOn = False
            End If
        Next

        If Len(kellonaika) = 1 And kaksoispisteOn = False Then

            eroteltu = "0" & kellonaika & ":00"
            Return eroteltu
            Exit Function
        End If


        If Len(kellonaika) = 3 And kaksoispisteOn = False Then
            loppuosa = Microsoft.VisualBasic.Right(kellonaika, 2)
            alkuosa = Microsoft.VisualBasic.Left(kellonaika, 1)
            eroteltu = "0" & alkuosa & ":" & loppuosa
            Return eroteltu
            Exit Function
        End If

        If Len(kellonaika) = 2 And kaksoispisteOn = False Then

            eroteltu = kellonaika & ":00"
            Return eroteltu
            Exit Function
        End If

        If Len(kellonaika) = 4 And kaksoispisteOn = False Then
            loppuosa = Microsoft.VisualBasic.Right(kellonaika, 2)
            alkuosa = Microsoft.VisualBasic.Left(kellonaika, 2)
            eroteltu = alkuosa & ":" & loppuosa
            Return eroteltu
            Exit Function

        End If

        If Len(kellonaika) = 4 And kaksoispisteOn = True Then
            loppuosa = Microsoft.VisualBasic.Right(kellonaika, 2)
            alkuosa = Microsoft.VisualBasic.Left(kellonaika, 1)
            eroteltu = "0" & alkuosa & ":" & loppuosa
            Return eroteltu
            Exit Function

        End If

        If Len(kellonaika) = 5 And kaksoispisteOn = True Then
            loppuosa = Microsoft.VisualBasic.Right(kellonaika, 2)
            alkuosa = Microsoft.VisualBasic.Left(kellonaika, 2)
            eroteltu = alkuosa & ":" & loppuosa
            Return eroteltu
            Exit Function

        End If

        If Len(kellonaika) = 5 Then
            eroteltu = kellonaika
            Return eroteltu

            Exit Function

        End If


        Return eroteltu

    End Function
    Public Sub SammutaNaytonAjastimet()
        Naytto2014.SivunVaihto.Enabled = False
        Naytto2014.PesujenSeuranta.Enabled = False
        Naytto2014.SeuraaTietokantaa.Enabled = False

    End Sub
    Public Sub KaynnistaNaytonAjastimet()
        Naytto2014.SivunVaihto.Enabled = True
        Naytto2014.PesujenSeuranta.Enabled = True
        Naytto2014.SeuraaTietokantaa.Enabled = True

    End Sub
    Public Function muutaTunnitMinsoiksi(ByVal tunnit As String)
        Dim minsat As Integer = 0
        Dim Xtun As String = ""
        Dim Xmin As String = ""
        Dim X As String = ""
        Dim eka As Boolean = False

        For i = 1 To Len(tunnit)
            X = Microsoft.VisualBasic.Mid(tunnit, i, 1)

            If X <> ":" And eka = False Then Xtun &= X : GoTo ja
            If X = ":" Then eka = True : GoTo ja
            If X <> ":" And eka = True Then Xmin &= X : GoTo ja

ja:
        Next

        minsat = Val(Xtun) * 60 + Val(Xmin)


        Return minsat

    End Function
    Public Structure LahtoListalle
        Dim TV As String
        Dim L1Rivi As Integer
        Dim L2Rivi As Integer
        Dim listalle As Boolean

    End Structure
    Public Function TallennaPesuilmoitus(ByVal AutoNro As Integer, ByVal HloNro As Integer, ByVal aika As DateTime, ByVal numerosta As String, ByVal Koneelta As Boolean)
        Dim onnistuiko As Boolean = False

        Try
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection27
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Pesuilmoitukset " & _
                               "(AutoNro, HloNro, Pesuaika, Numerosta, Tietokoneelta, OmaAuto) " & _
                               "VALUES(@AutoNro, @HloNro, @Pesuaika, @Numerosta, @Tietokoneelta, @OmaAuto)"
                .Parameters.AddWithValue("@AutoNro", AutoNro)
                .Parameters.AddWithValue("@HloNro", HloNro)
                .Parameters.AddWithValue("@Pesuaika", CType(aika, DateTime))
                .Parameters.AddWithValue("@Numerosta", numerosta)
                .Parameters.AddWithValue("@Tietokoneelta", Koneelta)


            End With
            If AutoNro = 5555 Then cmd.Parameters.AddWithValue("@OmaAuto", True) Else cmd.Parameters.AddWithValue("@OmaAuto", False)

            TbConnection27.Open()
            cmd.ExecuteNonQuery()
            TbConnection27.Close()
            onnistuiko = False

        Catch ex As Exception
            Err.Clear()
            onnistuiko = False
        End Try
        TbConnection27.Close()
        TallennaTietokantaToiminto(2)




        Return onnistuiko
    End Function
    Public Sub TallennaTietokantaToiminto(ByVal Toimi As Integer)
        Dim xx As New MySqlCommand()
        With xx
            .Connection = TbConnection127
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO Lataa (Komento) VALUES(@Komento)"
            .Parameters.AddWithValue("@Komento", Toimi)  ' koodi lähtölistan ja 2viikkoa tietojen päivittämiselle JSControliin

        End With
        Try
            TbConnection127.Open()
            xx.ExecuteNonQuery()
            TbConnection127.Close()

        Catch ex As Exception
            Err.Clear()

        End Try
    End Sub
    Public Function TarvitseekoTyoVuoroKirjautumisen(ByVal TV As String)
        Dim paluu As Boolean = False
        Try

            myTbConnection199.Close()
            Dim rekpv As New MySqlCommand()
            With rekpv
                .Connection = myTbConnection199
                .CommandType = CommandType.Text
                .CommandText = "SELECT Lyhenne, OltavaKuljettaja FROM TyoVuorot WHERE Lyhenne = @Lyhenne "
                .Parameters.AddWithValue("@Lyhenne", TV)

            End With
            myTbConnection199.Open()
            Dim rdRek As MySqlDataReader = rekpv.ExecuteReader

            If rdRek.HasRows = True Then
                rdRek.Read()

                paluu = rdRek.GetValue(1)

            Else
                paluu = False

            End If

            rdRek.Close()
            myTbConnection199.Close()


        Catch ex As Exception
            Err.Clear()

        End Try

        Return paluu

    End Function

    Public Function OnkoAutoAjoKelvoton(ByVal Autonro As Integer, ByVal pvm As String, ByVal klo As String) As Boolean
        Dim paluu As Boolean = False
        Try
            If klo = "" Then klo = "06:00:00"

            ' TARKISTETAAN ONKO AUTO AutojenTilaseuranta tilasta tehty ilmoitusta
            myTbConnection199.Close()
            Dim strNytON As String = pvm & " " & klo
            Dim nytON As DateTime = CType(strNytON, DateTime)

            Dim commm As New MySqlCommand()
            With commm
                .CommandType = CommandType.Text
                .Connection = myTbConnection199
                .CommandText = "SELECT * FROM AutojenTilaSeuranta WHERE AutoNro = @AutoNro AND AlkaenPVM < @AlkaenPVM"
                .Parameters.AddWithValue("@AutoNro", Autonro)
                .Parameters.AddWithValue("@AlkaenPVM", nytON)


            End With
            myTbConnection199.Open()

            Dim commRd As MySqlDataReader = commm.ExecuteReader
            If commRd.HasRows = True Then
                While commRd.Read
                    '      If commRd.GetValue(4) IsNot DBNull.Value Then
                    If nytON > commRd.GetValue(4) Then
                        paluu = True
                        Exit While

                    Else
                        paluu = False
                        Exit While

                    End If

                    '   End If
                End While

            Else

                paluu = True

            End If
            commRd.Close()
            myTbConnection199.Close()
        Catch ex As Exception
            Err.Clear()

        End Try

        Return paluu

    End Function
    Public Function PalautaAutonNrostaRekisterointiPVM(ByVal AutoNro As Integer)

        Dim paluurekPV As Date
        Try
            myTbConnection99.Close()
            Dim rekpv As New MySqlCommand()
            With rekpv
                .Connection = myTbConnection99
                .CommandType = CommandType.Text
                .CommandText = "SELECT AutoNro, RekistPVM FROM Kalusto WHERE AutoNro = @AutoNro "
                .Parameters.AddWithValue("@AutoNro", AutoNro)

            End With
            myTbConnection99.Open()
            Dim rdRek As MySqlDataReader = rekpv.ExecuteReader

            If rdRek.HasRows = True Then
                rdRek.Read()
                paluurekPV = rdRek.GetValue(1)

            End If
            rdRek.Close()
            myTbConnection99.Close()

        Catch ex As Exception
            Err.Clear()

        End Try

        Return paluurekPV

    End Function
    Public Function PalautaTiedotLahtolistaaVarten(ByVal TV As String) As LahtoListalle
        Dim paluu As LahtoListalle = Nothing
        Try
            myTbConnection55.Close()

            Dim cmd As New MySqlCommand()
            cmd.Connection = myTbConnection55
            cmd.CommandText = "SELECT lRivi1, lRivi2, Lahtolistalle FROM TyoVuorot WHERE Lyhenne=@Lyhenne"
            cmd.Parameters.AddWithValue("@Lyhenne", TV)
            myTbConnection55.Open()

            Dim rdx As MySqlDataReader = cmd.ExecuteReader

            If rdx.HasRows = True Then
                rdx.Read()
                paluu.L1Rivi = rdx.GetValue(0)
                paluu.L2Rivi = rdx.GetValue(1)
                paluu.listalle = rdx.GetValue(2)

            End If



            rdx.Close()

            myTbConnection55.Close()
        Catch ex As Exception
            Err.Clear()

        End Try


        Return paluu


    End Function
    Public Function GetMACAddress() As String
        Dim MACAddress As String = [String].Empty
        Try
            Dim mc As New ManagementClass("Win32_NetworkAdapterConfiguration")
            Dim moc As ManagementObjectCollection = mc.GetInstances()
            For Each mo As ManagementObject In moc
                If MACAddress = [String].Empty Then
                    ' only return MAC Address from first card
                    If CBool(mo("IPEnabled")) = True Then
                        MACAddress = mo("MacAddress").ToString()
                    End If
                End If
                mo.Dispose()
            Next

            MACAddress = MACAddress.Replace(":", "")

        Catch ex As Exception
            Err.Clear()
            MACAddress = "VIKA"

        End Try
        Return MACAddress
    End Function
    Public Sub LokiTapahtumanTallennus(ByVal LokiKayttaja As Integer, ByVal Selite1 As String, ByVal Selite2 As String, ByVal Hlo As Integer, ByVal Bilikka As Integer)
        Try
            lokiConnection.Close()

            Using cmd As New MySqlCommand()
                With cmd
                    .Connection = lokiConnection
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO Loki (HloNro, AutoNro, Selite1, Selite2, TekijaNro, Aika, Kone) VALUES(@HloNro, @AutoNro, @Selite1, @Selite2, @TekijaNro, @Aika, @Kone)"

                    .Parameters.AddWithValue("@HloNro", Hlo)
                    .Parameters.AddWithValue("@AutoNro", Bilikka)
                    .Parameters.AddWithValue("@Selite1", Selite1)
                    .Parameters.AddWithValue("@Selite2", Selite2)
                    .Parameters.AddWithValue("@TekijaNro", LokiKayttaja)
                    .Parameters.AddWithValue("@Aika", Now)
                    .Parameters.AddWithValue("@Kone", KoneenTunnisteMAC)


                End With
                lokiConnection.Open()
                cmd.ExecuteNonQuery()

            End Using



        Catch ex As Exception
            Err.Clear()
        Finally
            lokiConnection.Close()

        End Try


    End Sub
    Public Function MuutaMinsatTunneiksi(ByVal min As Integer)
        Dim nega As Boolean = False

        If min < 0 Then min = -min ' nega = True

        Dim palautus As String = ""
        Dim tunnit As Long = 0
        Dim minsat As Long = 0
        If min <= 0 Then palautus = "?" : GoTo loppu
        Dim mintoString As String = ""

        If min >= 60 Then tunnit = Int(min / 60)
        If tunnit <> 0 Then minsat = min - (tunnit * 60) Else palautus = "0:" & min.ToString : GoTo loppu
        mintoString = minsat.ToString
        If Len(mintoString) = 1 Then mintoString = "0" & mintoString
        If Len(mintoString) = 0 Then mintoString = "00"
        palautus = tunnit.ToString & ":" & mintoString   '
loppu:
        '  If nega = True Then palautus = "-" & palautus
        If Len(palautus) = 3 Then palautus = "0:0" & Microsoft.VisualBasic.Right(palautus, 1)
        If Len(palautus) > 7 Then palautus = "?"
        Return palautus

    End Function

    Public Function PalautaViikonPaiva(ByVal fag As Date)
        Dim vPaiva As String = ""


        Select Case Weekday(fag)
            Case 1
                vPaiva = "SUNNUNTAI"
            Case 2
                vPaiva = "MAANANTAI"
            Case 3
                vPaiva = "TIISTAI"
            Case 4
                vPaiva = "KESKIVIIKKO"
            Case 5
                vPaiva = "TORSTAI"
            Case 6
                vPaiva = "PERJANTAI"
            Case 7
                vPaiva = "LAUANTAI"

        End Select


        Return vPaiva

    End Function

    Public LLISTA(150) As _LLahtolista
    Public _2VKlista(2000) As _2vk
    Public _AutoListaus(35) As _lAutot
    Public _PesuSeuranta(20) As AutoPesuSeurantaVirtuaali
    Public _Pesemattomat(39) As pesemattomatStructure

    Public Structure pesemattomatStructure
        Dim rekkari As String
        Dim Aika As String

    End Structure

    Public Structure AutoPesuSeurantaVirtuaali
        Dim Rek As String
        Dim Kuljettaja As String
        Dim Aika As String

    End Structure
    Public Structure _LLahtolista
        Dim TV As String
        Dim REK As String
        Dim KULI As String
        Dim ilm As Boolean
        Dim AutoOK As Boolean

    End Structure
    Public Structure _lAutot
        Dim rek As String
        Dim pvm As String
        Dim Maara As Integer
        Dim teksti As String
        Dim punaisella As Boolean
        Dim PunTausta As Boolean

    End Structure
    Public Structure _2vk
        Dim tv As String
        Dim nimi As String
        Dim boxi As Integer
    End Structure

    Public Function PalautaSQLNrostaNimi(ByVal nro As Integer) As String
        Dim nimi As String = ""

        Try
            myTbConnection99.Close()
            Dim cmd As New MySqlCommand()
            cmd.Connection = myTbConnection99
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SELECT SukuNimi, EtuNimi FROM Henkilosto WHERE HloNro=@Nro"
            cmd.Parameters.AddWithValue("@Nro", nro)
            myTbConnection99.Open()
            Try
                Dim rdA As MySqlDataReader = cmd.ExecuteReader
                If rdA.HasRows = True Then
                    rdA.Read()
                    nimi = rdA.GetString(0) & " " & rdA.GetString(1)

                End If
            Catch ex As Exception
                Err.Clear()
                nimi = ""
            End Try

            myTbConnection99.Close()


        Catch ex As Exception
            Err.Clear()

        End Try


loppu:

        Return nimi

    End Function
    Public Function PalautaSQLNroNimiesta(ByVal dNimi As String) As Integer
        Dim nro As Integer = 0
        Try
            If dNimi = "" Then nro = 0 : Return nro : Exit Function
            Dim namesX() As String = Nothing
            namesX = dNimi.Split


            myTbConnection99.Close()
            Dim cmd As New MySqlCommand()
            cmd.Connection = myTbConnection99
            cmd.CommandText = "SELECT HloNro FROM Henkilosto WHERE SukuNimi=@sukunimi AND EtuNimi=@etunimi"
            cmd.Parameters.AddWithValue("@sukunimi", namesX(0))
            cmd.Parameters.AddWithValue("@etunimi", namesX(1))
            myTbConnection99.Open()
            Try
                Dim rdA As MySqlDataReader = cmd.ExecuteReader

                If rdA.HasRows = True Then
                    rdA.Read()
                    nro = rdA.GetValue(0)

                Else
                    nro = 7777
                End If
            Catch ex As Exception
                Err.Clear()
                nro = 7777

            End Try


    
        Catch ex As Exception
            Err.Clear()
        Finally
            myTbConnection99.Close()

        End Try



        Return nro

    End Function
    Public Function PalautaSQLRekkariNumerosta(ByVal AutoNro As Integer) As String
        Dim rek As String = ""
        Try
            myTbConnection99.Close()
            Dim cmd As New MySqlCommand()
            cmd.Connection = myTbConnection99
            cmd.CommandText = "SELECT RekNro FROM Kalusto WHERE AutoNro=@id"
            cmd.Parameters.AddWithValue("@id", AutoNro)
            myTbConnection99.Open()
            Try
                Dim rdA As MySqlDataReader = cmd.ExecuteReader
                ' Using rdA
                If rdA.HasRows = True Then
                    rdA.Read()
                    rek = rdA.GetString(0)

                End If


                '   End Using
                rdA.Close()
                myTbConnection99.Close()
            Catch ex As Exception
                Err.Clear()
                rek = ""
            End Try
        Catch ex As Exception
            Err.Clear()

        End Try





        Return rek

    End Function
    Public Sub LahetaSpostia(ByVal VastottajaNimi As String, ByVal emailOsoite As String, ByVal otsikko As String, ByVal viesti As String)
        Dim success As Boolean
        Dim mailman2 As New Chilkat.MailMan()
        Try
            success = False
            success = mailman2.UnlockComponent("KOLUMBMAILQ_E6Ty4DSQnW3b")
            If (success <> True) Then
                '  MsgBox(mailman2.LastErrorText)
                Exit Sub
            End If


            ' Set the SMTP server host.
            ' mailman2.SmtpHost = "mail.jsop.fi"
            mailman2.SmtpHost = "smtp.kolumbus.fi"
            ' mailman2.SmtpUsername = "taksikuljetus@jsop.fi"

            '  mailman2.SmtpPassword = "cawugument8"

            '  mailman2.SmtpPort = 465
            '   mailman2.StartTLS = True
            '  mailman2.SmtpSsl = False



            ' Create a simple email with a file attachment.
            Dim email2 As New Chilkat.Email()

            ' This email will have both a plain-text body and an HTML body.

            email2.Body = viesti
            email2.Subject = otsikko
            email2.AddTo(VastottajaNimi, emailOsoite)
            email2.From = "JSOP <jsop@kolumbus.fi>" 'postp 6.4.2018        

            '   email2.From = "JSOP <taksikuljetus@jsop.fi>"


            ' If you're curious, show the MIME text of the email to be sent..
            '     MessageBox.Show(email.GetMime())

            ' Send email.
            Try
                success = mailman2.SendEmail(email2)

            Catch ex As Exception
                Err.Clear()

            End Try
        Catch ex As Exception
            Err.Clear()

        End Try

        '     If success = False Then MessageBox.Show(mailman2.LastErrorText)



    End Sub
    Public Sub LahetaSpostiaHTML(ByVal VastottajaNimi As String, ByVal emailOsoite As String, ByVal otsikko As String, ByVal viesti As String)

 


  

        Dim success As Boolean
        Dim mailman2 As New Chilkat.MailMan()
        Try
            success = False
            success = mailman2.UnlockComponent("KOLUMBMAILQ_E6Ty4DSQnW3b")
            If (success <> True) Then
                '  MsgBox(mailman2.LastErrorText)
                Exit Sub
            End If


            ' Set the SMTP server host.
            mailman2.SmtpHost = "smtp.kolumbus.fi"

            ' Create a simple email with a file attachment.
            Dim email2 As New Chilkat.Email()

            ' This email will have both a plain-text body and an HTML body.
            email2.AddHtmlAlternativeBody(viesti)
            email2.Subject =
            email2.AddTo(VastottajaNimi, emailOsoite)
            email2.From = "JSOP <jsop@kolumbus.fi>"


            ' If you're curious, show the MIME text of the email to be sent..
            '     MessageBox.Show(email.GetMime())

            ' Send email.
            Try
                success = mailman2.SendEmail(email2)

            Catch ex As Exception
                Err.Clear()

            End Try
        Catch ex As Exception
            Err.Clear()

        End Try

        '     If success = False Then MessageBox.Show(mailman2.LastErrorText)



    End Sub
    Public Function PalautaNimestaPuhelinNumero(ByVal nimi As String, ByVal pvm As Date)
        Dim paluu As String = ""
        Try
            TbConnection27.Close()
            Dim hlonoro As Integer = PalautaSQLNroNimiesta(nimi)

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection27
                .CommandType = CommandType.Text
                .CommandText = "SELECT Numero FROM PuhelinNrot " & _
                    "WHERE (@PVM " & _
                    "BETWEEN AlkaenPVM AND AstiPVM) AND " & _
                    "(HloNro = @HloNro)"
                .Parameters.AddWithValue("@Pvm", pvm)
                .Parameters.AddWithValue("@HloNro", hlonoro)

            End With
            TbConnection27.Open()
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                paluu = rd.GetString(0)

            Else
                paluu = "?"

            End If

            TbConnection27.Close()
            rd.Close()
        Catch ex As Exception
            Err.Clear()

        End Try



        Return paluu

    End Function
    Public Function PalautaSQLNumeroRekkarista(ByVal Rekkari As String) As Integer
        Dim nro As Integer = 0
        Try
            myTbConnection99.Close()
            Dim cmd As New MySqlCommand()
            cmd.Connection = myTbConnection99
            cmd.CommandText = "SELECT AutoNro FROM Kalusto WHERE RekNro=@id"
            cmd.Parameters.AddWithValue("@id", Rekkari)


            myTbConnection99.Open()
            Dim rdA As MySqlDataReader = cmd.ExecuteReader
            If rdA.HasRows = True Then
                rdA.Read()
                nro = rdA.GetValue(0)
                rdA.Close()

            End If
        Catch ex As Exception
            Err.Clear()
            nro = 0
        End Try

        myTbConnection99.Close()



        Return nro

    End Function


End Module

