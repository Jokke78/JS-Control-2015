Imports MySql.Data.MySqlClient

'Imports System.Data.SqlClient
Imports System.Threading
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.ComponentModel

Public Class Naytto2014
    Public tvX As Label()
    Public bil As Label()
    Public kuli As Label()

    Private paiva As Label()
    Private pvm2 As Label()
    Private PV As ListBox()
    Private PVM As Label()
    Private rek As Label()
    Private nro As Label()
    Private teksti As Label()
    Private PesuRek As Label()
    Private PesuKul As Label()
    Private PesuAika As Label()
    Private PesustaRek As Label()
    Private PesustaAika As Label()
    Public myyYhteysx As MySqlConnection = New MySqlConnection(serverString)

    Private myTbConnection As MySqlConnection = New MySqlConnection(serverString)
    Private myTbConnection2 As MySqlConnection = New MySqlConnection(serverString)
    Private myTbConnectionPesuILM As MySqlConnection = New MySqlConnection(serverString)


    Private Sub Naytto2014_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tvX = New Label() {t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, t19, t20, t21, t22, t23, t24, t25, t26, t27, t28, t29, t30, t31, t32, t33, t34, t35, t36, t37, t38, t39, t40, t41, t42, t43, t44, t45, t46, t47, t48, t49, t50}
        bil = New Label() {a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17, a18, a19, a20, a21, a22, a23, a24, a25, a26, a27, a28, a29, a30, a31, a32, a33, a34, a35, a36, a37, a38, a39, a40, a41, a42, a43, a44, a45, a46, a47, a48, a49, a50}
        kuli = New Label() {k1, k2, k3, k4, k5, k6, k7, k8, k9, k10, k11, k12, k13, k14, k15, k16, k17, k18, k19, k20, k21, k22, k23, k24, k25, k26, k27, k28, k29, k30, k31, k32, k33, k34, k35, k36, k37, k38, k39, k40, k41, k42, k43, k44, k45, k46, k47, k48, k49, k50}

        PV = New ListBox() {pz1, pz2, pz3, pz4, pz5, pz6, pz7, pz8, pz9, pz10, pz11, pz12, pz13, pz14}

        paiva = New Label() {v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14}
        pvm2 = New Label() {d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, d11, d12, d13, d14}
        teksti = New Label() {q1, q2, q3, q4, q5, q6, q7, q8, q9, q10, q11, q12, q13, q14, q15, q16, q17, q18, q19, q20, q21, q22, q23, q24, q25, q26, q27, q28, q29, q30}
        rek = New Label() {r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17, r18, r19, r20, r21, r22, r23, r24, r25, r26, r27, r28, r29, r30}
        PVM = New Label() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30}
        nro = New Label() {n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15, n16, n17, n18, n19, n20, n21, n22, n23, n24, n25, n26, n27, n28, n29, n30}
        PesuRek = New Label() {ra1, ra2, ra3, ra4, ra5, ra6, ra7, ra8, ra9, ra10, ra11, ra12, ra13, ra14, ra15, ra16, ra17, ra18, ra19, ra20}
        PesuKul = New Label() {ku1, ku2, ku3, ku4, ku5, ku6, ku7, ku8, ku9, ku10, ku11, ku12, ku13, ku14, ku15, ku16, ku17, ku18, ku19, ku20}
        PesuAika = New Label() {kl1, kl2, kl3, kl4, kl5, kl6, kl7, kl8, kl9, kl10, kl11, kl12, kl13, kl14, kl15, kl16, kl17, kl18, kl19, kl20}

        PesustaRek = New Label() {Z1, z2, z3, z4, z5, z6, z7, z8, z9, z10, z11, z12, z13, z14, z15, z16, z17, z18, z19, z20, z21, z22, z23, z24, z25, z26, z27, z28, z29, z30, z31, z32, z33, z34, z35, z36, z37, z38, z39, z40}
        PesustaAika = New Label() {X1, x2, x3, x4, x5, x6, x7, x8, x9, x10, x11, x12, x13, x14, x15, x16, x17, x18, x19, x20, x21, x22, x23, x24, x25, x26, x27, x28, x29, x30, x31, x32, x33, x34, x35, x36, x37, x38, x39, x40}

        sivu = 1


        '     NaytettavaPVM.Value = Today.AddDays(-1)
        '   Try
        '    NaytettavaPVM.Value = Today
        '    NaytettavaPVM.MaxDate = Today.AddDays(14)
        '     NaytettavaPVM.MinDate = Today.AddDays(-14)

        '    Catch ex As Exception
        'Throw ex
        '    End Try
        Try
            Dim pvmm As Date = Today '.AddDays(3)
            NaytettavaPVM.Value = pvmm

            '    ViikonPaiva.Text = PalautaViikonPaiva(pvmm) ', DateFormat.ShortDate))
            '       NaytettavaPVM_ValueChanged(Nothing, Nothing)
            '     LaitaVuorotNakyville()
            '     Panel1.Visible = True

            ViikonPaiva.Focus()

            Dim tanaanOn As Date = Today
            For i As Integer = 0 To 13
                pvm2(i).Text = FormatDateTime(tanaanOn.AddDays(i), DateFormat.ShortDate).ToString

                paiva(i).Text = PalautaViikonPaiva(tanaanOn.AddDays(i))
                '          HaeIkkunaTietoIlmanAutoa(CType(pvm2(i).Text, Date).ToString, i)

            Next
        Catch ex As Exception
            Err.Clear()

        End Try

        Try
            Control.CheckForIllegalCrossThreadCalls = False
            InitializeBGW()

        Catch ex As Exception
            Err.Clear()

        End Try

   




        For i As Integer = 0 To 35
            _AutoListaus(i).rek = ""
            _AutoListaus(i).Maara = 0
            _AutoListaus(i).teksti = ""
            _AutoListaus(i).PunTausta = False
            _AutoListaus(i).pvm = ""
            _AutoListaus(i).punaisella = False

        Next
        For i As Integer = 0 To 19
            PesuRek(i).Text = ""
            PesuAika(i).Text = ""
            PesuKul(i).Text = ""

        Next
        For i As Integer = 0 To 39
            PesustaAika(i).Text = ""
            PesustaRek(i).Text = ""
            _Pesemattomat(i).rekkari = ""
            _Pesemattomat(i).Aika = ""

        Next
        For i As Integer = 0 To 2000
            _2VKlista(i).tv = ""
            _2VKlista(i).nimi = ""
            _2VKlista(i).boxi = Nothing

        Next

        '      TyhjennaLista()

        '     LaitaVuorotNakyville() ' lähtölista
        tyhjennaAutoInfoNaytto()

        LataaLahtolistaTiedot()

        LahtoListaEsille()

        hae2viikkoTiedot()
        '   Laita2viikkoa_Nakyville()
        HaeAjoKieltoAutot()
        TarkistaKatsastukset()

        laitaAutotEsille()
        LataaPesuilmoitukset()
        LataaPesemattomatAutot()
        LaitaPesaemattomatEsille()
        LaskePesuilmoitukset()
    End Sub


    Public Sub LaitaPesaemattomatEsille()
        Try
            For i As Integer = 0 To 39
                PesustaAika(i).Text = _Pesemattomat(i).Aika
                PesustaRek(i).Text = _Pesemattomat(i).rekkari


            Next
        Catch ex As Exception
            Err.Clear()

        End Try




    End Sub

    Public Sub LataaPesemattomatAutot()
        Try
            myTbConnection.Close()
            For i As Integer = 0 To 39
                PesustaAika(i).Text = ""
                PesustaRek(i).Text = ""
                _Pesemattomat(i).rekkari = ""
                _Pesemattomat(i).Aika = ""

            Next
            For i As Integer = 0 To 2000
                _2VKlista(i).tv = ""
                _2VKlista(i).nimi = ""
                _2VKlista(i).boxi = Nothing

            Next

        Catch ex As Exception
            Err.Clear()

        End Try

        Dim nytON As Date = FormatDateTime(Now, DateFormat.ShortDate)
        Dim rekkari As String = ""

        Dim erotus As TimeSpan = Nothing

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myTbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT AutoNro, RekNro FROM Kalusto WHERE KatsastusVahti = @KatsastusVahti AND AjoNeuvoLuokka='M2' OR AjoNeuvoLuokka='M3' AND Raporteissa='1' ORDER BY RekNro ASC"
            .Parameters.AddWithValue("@KatsastusVahti", True)
        End With
        myTbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then
            Try
                While rd.Read

                    'tarkistetaan onko katsastusvahti käytössä
                    ' tarkistetaan, että autoneuvoluokka on M2 tai m3 (bussi)

                    '    If rd.GetValue(6) IsNot DBNull.Value Then
                    '       If rd.GetString(6) = "M2" Or rd.GetString(6) = "M3" Then

                    Dim cmKAT As New MySqlCommand()
                    myTbConnection2.Close()
                    With cmKAT
                        .Connection = myTbConnection2
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT Kalusto.RekNro, Pesuilmoitukset.Pesuaika FROM Pesuilmoitukset " & _
                            "INNER JOIN Kalusto ON Pesuilmoitukset.AutoNro=Kalusto.AutoNro " & _
                            "WHERE Pesuilmoitukset.AutoNro = @AutoNro ORDER BY Pesuilmoitukset.Pesuaika DESC"
                        .Parameters.AddWithValue("@AutoNro", rd.GetValue(0))

                    End With

                    myTbConnection2.Open()
                    Dim PesuReader As MySqlDataReader = cmKAT.ExecuteReader
                    If PesuReader.HasRows = True Then
                        PesuReader.Read()
                        erotus = CType(PesuReader.GetValue(1), Date) - nytON

                        If erotus.TotalDays < -7 Then
                            LisaaPesemattomiin(rd.GetValue(1), FormatDateTime(CType(PesuReader.GetValue(1), Date), DateFormat.ShortDate).ToString)

                        End If

                    End If


                    PesuReader.Close()
                    myTbConnection2.Close()

                    '    End If




                    ' End If







                End While
            Catch ex As Exception
                Err.Clear()
            Finally

                myTbConnection.Close()
            End Try
        End If


        rd.Dispose()


    End Sub
    Public Sub LisaaPesemattomiin(ByVal rek As String, ByVal pvm As String)
        For w As Integer = 0 To 39
            If _Pesemattomat(w).rekkari = "" Or _Pesemattomat(w).rekkari = Nothing Then
                _Pesemattomat(w).rekkari = rek
                _Pesemattomat(w).Aika = pvm
                Exit Sub

            End If

        Next
    End Sub
    Public Sub LataaPesuIlmoitukset()
        Try
            For i As Integer = 0 To 19
                PesuRek(i).Text = ""
                PesuAika(i).Text = ""
                PesuKul(i).Text = ""

            Next
        Catch ex As Exception
            Err.Clear()
            Exit Sub

        End Try


        myTbConnection.Close()
        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myTbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT Henkilosto.SukuNimi, Henkilosto.EtuNimi, Kalusto.RekNro, Pesuilmoitukset.Pesuaika FROM Pesuilmoitukset " & _
                    "INNER JOIN Henkilosto ON Pesuilmoitukset.HloNro=Henkilosto.HloNro " & _
                    "INNER JOIN Kalusto ON Pesuilmoitukset.AutoNro=Kalusto.AutoNro " & _
                    "WHERE Pesuilmoitukset.OmaAuto='0' ORDER BY Pesuaika DESC"
        End With

        Try
            myTbConnection.Open()

        Catch ex As Exception
            Err.Clear()

            Exit Sub
        End Try
        Dim laskuri As Integer = 0

        Dim rd As MySqlDataReader = cmd.ExecuteReader
        Try
            If rd.HasRows = True Then
                While rd.Read
                    laskuri += 1
                    If laskuri = 20 Then
                        myTbConnection.Close()
                        Exit While
                    End If

                    _PesuSeuranta(laskuri - 1).Kuljettaja = rd.GetString(0) & " " & rd.GetString(1)
                    _PesuSeuranta(laskuri - 1).Rek = rd.GetString(2)
                    _PesuSeuranta(laskuri - 1).Aika = FormatDateTime(CType(rd.GetValue(3), Date), DateFormat.ShortDate).ToString & " klo " & FormatDateTime(CType(rd.GetValue(3), DateTime), DateFormat.ShortTime).ToString





                End While
            End If

        Catch ex As Exception
            Err.Clear()

        End Try
        myTbConnection.Close()
        Try
            For i = 0 To 19

                PesuAika(i).Text = _PesuSeuranta(i).Aika
                PesuKul(i).Text = _PesuSeuranta(i).Kuljettaja
                PesuRek(i).Text = _PesuSeuranta(i).Rek

            Next

        Catch ex As Exception
            Err.Clear()

        End Try




    End Sub
    Public Sub laitaAutotEsille()
        For i As Integer = 0 To 29
            Try
                If _AutoListaus(i).rek <> "" Then
                    PVM(i).Text = _AutoListaus(i).pvm
                    rek(i).Text = _AutoListaus(i).rek
                    nro(i).Text = _AutoListaus(i).Maara.ToString
                    teksti(i).Text = _AutoListaus(i).teksti
                    If _AutoListaus(i).punaisella = True Then
                        PVM(i).ForeColor = Color.Red
                        rek(i).ForeColor = Color.Red
                        nro(i).ForeColor = Color.Red
                        teksti(i).ForeColor = Color.Red

                    End If
                    If _AutoListaus(i).PunTausta = True Then
                        PVM(i).BackColor = Color.Red
                        rek(i).BackColor = Color.Red
                        nro(i).BackColor = Color.Red
                        teksti(i).BackColor = Color.Red

                    End If
                End If
            Catch ex As Exception
                Err.Clear()
            End Try

        Next

    End Sub
    Public Sub tyhjennaAutoInfoNaytto()
        For i As Integer = 0 To 29
            PVM(i).Text = ""
            rek(i).Text = ""
            nro(i).Text = ""
            teksti(i).Text = ""

            rek(i).ForeColor = Color.Black
            rek(i).BackColor = Color.WhiteSmoke
            teksti(i).ForeColor = Color.Black
            teksti(i).BackColor = Color.WhiteSmoke
            PVM(i).ForeColor = Color.Black
            PVM(i).BackColor = Color.WhiteSmoke
            nro(i).ForeColor = Color.Black
            nro(i).BackColor = Color.WhiteSmoke

        Next
    End Sub

    Public Sub TarkistaKatsastukset()
        myTbConnection.Close()

        Dim nytON As Date = Today
        Dim rekkari As String = ""
        Dim ajoaika As Date = Nothing

        Dim erotus As TimeSpan = Nothing

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myTbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM KatsastuksienSeuranta"
        End With
        myTbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader

        Try
            While rd.Read

                'tarkistetaan onko katsastusvahti käytössä
                Dim cmKAT As New MySqlCommand()
                myTbConnection2.Close()
                With cmKAT
                    .Connection = myTbConnection2
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT KatsastusVahti FROM Kalusto WHERE AutoNro = @AutoNro"
                    .Parameters.AddWithValue("@AutoNro", rd.GetValue(1))

                End With
                myTbConnection2.Open()
                Dim KalustoReader As MySqlDataReader = cmKAT.ExecuteReader
                If KalustoReader.HasRows = True Then
                    KalustoReader.Read()
                    If KalustoReader.GetValue(0) = True Then
                        Dim aika As Date = rd.GetValue(4)
                        erotus = aika - nytON
                        If erotus.TotalDays < 30 And erotus.TotalDays > 0 Then
                            ' LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ AIKAA KATSASTAA")
                            '   rekkari = TeeVikailmoitus.PalautaSQLNumeroRekkarista(rd.GetValue(1))
                            LisaaNaytolle(PalautaSQLRekkariNumerosta(rd.GetValue(1)), FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString, Int(erotus.TotalDays), "PÄIVÄÄ AIKAA KATSASTAA")
                        End If

                        If erotus.TotalDays = 0 Then
                            '  LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " KATSASTETTAVA TÄNÄÄN")
                            LisaaNaytolle(PalautaSQLRekkariNumerosta(rd.GetValue(1)), FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString, Int(erotus.TotalDays), "KATSASTETTAVA TÄNÄÄN")

                        End If

                        If erotus.TotalDays < 0 Then
                            Dim luku = -erotus.TotalDays

                            '         LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ KATSASTAMATTOMANA")
                            LisaaNaytolle(PalautaSQLRekkariNumerosta(rd.GetValue(1)), FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString, Int(luku), "PÄIVÄÄ KATSASTAMATTOMANA, AJOKIELLOSSA")

                        End If

                    End If
                    'JATKOAIKAA ON LAITETTU
                    Try

                        If KalustoReader.GetValue(0) = True Then

                            If rd.GetValue(5) IsNot DBNull.Value Then
                                ajoaika = rd.GetValue(5)

                                erotus = ajoaika - nytON
                                If erotus.TotalDays < 30 And erotus.TotalDays > 0 Then
                                    ' LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ AIKAA KATSASTAA")
                                    '   rekkari = TeeVikailmoitus.PalautaSQLNumeroRekkarista(rd.GetValue(1))
                                    LisaaNaytolle(PalautaSQLRekkariNumerosta(rd.GetValue(1)), FormatDateTime(rd.GetValue(5), DateFormat.ShortDate).ToString, Int(erotus.TotalDays), "PÄIVÄÄ UUSINTA KATSASTUKSEEN")
                                End If

                                If erotus.TotalDays = 0 Then
                                    '  LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " KATSASTETTAVA TÄNÄÄN")
                                    LisaaNaytolle(PalautaSQLRekkariNumerosta(rd.GetValue(1)), FormatDateTime(rd.GetValue(5), DateFormat.ShortDate).ToString, Int(erotus.TotalDays), "UUSINTA KATSASTU TÄNÄÄN")

                                End If

                                If erotus.TotalDays < 0 Then
                                    Dim luku = -erotus.TotalDays

                                    '         LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ KATSASTAMATTOMANA")
                                    LisaaNaytolle(PalautaSQLRekkariNumerosta(rd.GetValue(1)), FormatDateTime(rd.GetValue(5), DateFormat.ShortDate).ToString, Int(luku), "PÄIVÄÄ UUSINTA KATSASTUSAJAN PÄÄTTYMISESTÄ, AJOKIELLOSSA")

                                End If
                            End If

                        End If

                    Catch ex As Exception
                        Err.Clear()
                    End Try

                End If

                KalustoReader.Close()
                myTbConnection2.Close()



            End While
        Catch ex As Exception
            Err.Clear()

        End Try

        myTbConnection.Close()
        rd.Close()


        LaitaAikaJarjestykseen()
    End Sub
    Public Sub HaeAjoKieltoAutot()
        Dim PP As String = ""
        myTbConnection.Close()

        Dim xcom As New MySqlCommand()
        xcom.Connection = myTbConnection
        xcom.CommandText = "SELECT * FROM Kalusto WHERE Raporteissa='1'" ' WHERE AutoTyyppiID = @AutotyyppiID"
        xcom.Parameters.AddWithValue("@AutoTyyppiID", 2)

        myTbConnection.Open()
        Dim rdxxxx As MySqlDataReader = xcom.ExecuteReader

        Try
            While rdxxxx.Read
                PP = ""
                ' AUTOTYYPPI PITÄÄ OLLA LA = 2 (1=ha, 3=ka)
                '      If rdxxxx.GetValue(5) IsNot DBNull.Value Then
                If rdxxxx.GetValue(5) = 2 Then
                    PP = rdxxxx.GetValue(1)
                    LisaaBiileihin(PP)

                End If

                '    End If
            End While

        Catch ex As Exception
            Err.Clear()

        End Try
        myTbConnection.Close()



    End Sub
    Public Sub LisaaBiileihin(ByVal bilikka As String)
        ' TARKISTETAAN ONKO AUTO AutojenTilaseuranta tilasta tehty ilmoitusta
        myTbConnection2.Close()
        Dim strNytON As String = FormatDateTime(Now, DateFormat.ShortDate).ToString & " " & "06:00:00"
        Dim nytON As DateTime = CType(strNytON, DateTime)

        Dim commm As New MySqlCommand()
        With commm
            .CommandType = CommandType.Text
            .Connection = myTbConnection2
            .CommandText = "SELECT * FROM AutojenTilaSeuranta WHERE AutoNro = @AutoNro AND AlkaenPVM < @AlkaenPVM"
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(bilikka))
            .Parameters.AddWithValue("@AlkaenPVM", nytON)


        End With
        myTbConnection2.Open()
        Dim erotus As TimeSpan = Nothing

        Dim commRd As MySqlDataReader = commm.ExecuteReader
        If commRd.HasRows = True Then
            While commRd.Read
                '     If commRd.GetValue(4) IsNot DBNull.Value Then
                If nytON > commRd.GetValue(4) Then
                    'Toiminto.Items.Add(bilikka)
                    erotus = nytON - CType(commRd.GetValue(3), DateTime)

                    LisaaNaytolle(bilikka, FormatDateTime(commRd.GetValue(3), DateFormat.ShortDate).ToString, Int(erotus.TotalDays), "PÄIVÄÄ RIKKI, AJOKIELLOSSA")

                    Exit While
                End If

                '   End If
            End While


        End If
        commRd.Close()
        myTbConnection2.Close()
    End Sub

    Public Sub LisaaNaytolle(ByVal reknro As String, ByVal Xpvm As String, ByVal Xnro As Integer, ByVal telsti As String)

        For i As Integer = 0 To 33
            Try
                If _AutoListaus(i).rek = "" Or _AutoListaus(i).rek = Nothing Then
                    _AutoListaus(i).pvm = Xpvm
                    _AutoListaus(i).rek = reknro
                    _AutoListaus(i).Maara = Xnro.ToString
                    _AutoListaus(i).teksti = telsti
                    Exit For
                End If
            Catch ex As Exception
                Err.Clear()
            End Try

        Next



    End Sub
    Private Structure AutoInfoAakkostus
        Dim r As String
        Dim pvm As Date
        Dim n As Long
        Dim t As String

    End Structure
    Public Sub LaitaAikaJarjestykseen()
        Dim alkup(33) As AutoInfoAakkostus


        'siirretään tilapäis taulukkoon
        For i As Integer = 0 To 33
            If _AutoListaus(i).rek <> "" Or _AutoListaus(i).rek <> Nothing Then
                alkup(i).r = _AutoListaus(i).rek
                alkup(i).pvm = CType(_AutoListaus(i).pvm, Date)
                alkup(i).n = Val(_AutoListaus(i).Maara)
                alkup(i).t = _AutoListaus(i).teksti
            End If
        Next

        Dim apu As AutoInfoAakkostus = Nothing
        ' järjestetään aikajärjestykseen
        For i = 0 To 33
            For j = i + 1 To 33
                If alkup(j).n > alkup(i).n Then
                    apu = alkup(i)
                    alkup(i) = alkup(j)
                    alkup(j) = apu

                End If
            Next j
        Next i
        Dim L As Integer = -1

        For i = 0 To 33
            If alkup(i).r <> "" Then L += 1
        Next
        For i = 0 To 35
            _AutoListaus(i).Maara = 0
            _AutoListaus(i).teksti = ""
            _AutoListaus(i).pvm = ""
            _AutoListaus(i).rek = ""
            _AutoListaus(i).punaisella = False
            _AutoListaus(i).PunTausta = False

        Next
        For i As Integer = L To 0 Step -1

            If alkup(L - i).r <> "" Then
                _AutoListaus(L - i).rek = alkup(i).r
                _AutoListaus(L - i).pvm = FormatDateTime(alkup(i).pvm, DateFormat.ShortDate).ToString
                _AutoListaus(L - i).Maara = alkup(i).n.ToString
                _AutoListaus(L - i).teksti = alkup(i).t
            End If
        Next
        LaitaVarit()



    End Sub

    Public Sub LaitaVarit()
        For i As Integer = 0 To 33

            If _AutoListaus(i).teksti = "KATSASTETTAVA TÄNÄÄN" Then _AutoListaus(i).PunTausta = True
            If _AutoListaus(i).teksti = "PÄIVÄÄ AIKAA KATSASTAA" And Val(_AutoListaus(i).Maara) <= 5 Then _AutoListaus(i).punaisella = True

            If _AutoListaus(i).teksti = "PÄIVÄÄ KATSASTAMATTOMANA, AJOKIELLOSSA" Then
                _AutoListaus(i).punaisella = True

            End If

        Next
    End Sub


    Public Sub LahtoListaEsille()
        Dim x As Integer = 0
        TyhjennaLahtoListaNaytolta()
        Select Case sivu
            Case 1
                x = 0
            Case 2
                x = 50


            Case 3
                x = 99
        End Select

        For i = 0 To 49

            '         If x = 0 And i = 50 Then Exit For
            '        If x = 1 And i = 100 Then Exit For
            If LLISTA(i + x).TV <> Nothing Then
                Try
                    '  If LLISTA(i + x).TV = "812A" Then Beep()
                    kuli(i).Text = LLISTA(i + x).KULI
                    tvX(i).Text = LLISTA(i + x).TV
                    bil(i).Text = LLISTA(i + x).REK
                    If LLISTA(i + x).AutoOK = False Then bil(i).ForeColor = Color.Red Else bil(i).ForeColor = Color.Black
                    If LLISTA(i + x).ilm = True Then kuli(i).ForeColor = Color.DarkGreen Else kuli(i).ForeColor = Color.Black

                Catch ex As Exception
                    Err.Clear()

                End Try

            End If

        Next
        LaitaPUNAISTA()


    End Sub
    Private Sub BackgroundWorker1_Disposed(sender As Object, e As System.EventArgs) Handles BackgroundWorker1.Disposed
        Thread.Sleep(250)

    End Sub
    Public Structure tietoja
        Public Property Vuoro As String
        Public Property riviID As Long
        Public Property HloNro As Integer
        Public Property AutoNro As Integer

    End Structure
    Public Sub xLaitaVuorotEsille()

        myTbConnection.Close()

        myTbConnection.Open()

        Dim tbAdapter As New MySqlDataAdapter()
        Dim dAl As Date = Format(NaytettavaPVM.Value, "\#yyyy\-MM\-dd\#")

        With tbAdapter
            .SelectCommand = New MySqlCommand
            .SelectCommand.Connection = myTbConnection
            .SelectCommand.CommandText = "SELECT AjetutVuorot.TVLyhenne, Kalusto.RekNro, Henkilosto.SukuNimi, Henkilosto.Etunimi FROM AjetutVuorot " & _
                "INNER JOIN Henkilosto ON Henkilosto.HloNro=AjetutVuorot.HloNro " & _
                "INNER JOIN Kalusto ON Kalusto.AutoNro=AjetutVuorot.AutoNro " & _
                "WHERE AlkuPVM = @PVM ORDER BY AjetutVuorot.TVLyhenne ASC"
            .SelectCommand.Parameters.AddWithValue("@PVM", dAl)
        End With

        Dim myDataSet As DataSet = New DataSet()

        tbAdapter.Fill(myDataSet, "AjetutVuorot")




    End Sub

    Public Sub LataaLahtolistaTiedot()
        For i = 0 To 149
            LLISTA(i).TV = ""
            LLISTA(i).REK = ""
            LLISTA(i).AutoOK = False
            LLISTA(i).KULI = ""
            LLISTA(i).ilm = False

        Next
alku:
        myTbConnection.Close()
        '  Dim strPVM As Date = CDate(NaytettavaPVM.Value)
        '     Dim dAl As Date = Format(NaytettavaPVM.Value, "\#yyyy\-MM\-dd\#")
        '   tyhjennaLista()
        '     Try
        Dim cmd As New MySqlCommand()
        Dim dAl As Date = Format(NaytettavaPVM.Value, "\#yyyy\-MM\-dd\#")

        cmd.Connection = myTbConnection
        cmd.CommandText = "SELECT AjetutVuorot.RiviID, AjetutVuorot.TVLyhenne, Henkilosto.SukuNimi, Henkilosto.Etunimi, Kalusto.RekNro, AjetutVuorot.AutoNro, AjetutVuorot.Ilmoittauduttu, TyoVuorot.lRivi1, TyoVuorot.lRivi2, TyoVuorot.Lahtolistalle FROM AjetutVuorot " & _
                            "INNER JOIN Henkilosto ON AjetutVuorot.HloNro=Henkilosto.HloNro " & _
                            "INNER JOIN Kalusto ON AjetutVuorot.AutoNro=Kalusto.AutoNro " & _
                            "INNER JOIN TyoVuorot ON AjetutVuorot.TVLyhenne=TyoVuorot.Lyhenne AND (@AlkuPVM BETWEEN TyoVuorot.AlkaenPVM AND TyoVuorot.AstiPVM) " & _
                             "WHERE AlkuPVM = @AlkuPVM"
        '   cmd.Parameters.AddWithValue("@ALkuPVM", FormatDateTime(NaytettavaPVM.Value, DateFormat.ShortDate))
        cmd.Parameters.AddWithValue("@AlkuPVM", dAl)
        '    .Parameters.AddWithValue("@UusiTV", uusiTV.Text)
        '   .Parameters.AddWithValue("@VanhaTV", VanhaTV.Text)

        Dim str As String = FormatDateTime(NaytettavaPVM.Value, DateFormat.ShortDate).ToCharArray
        Dim kysely As LahtoListalle = Nothing
        Try
            myTbConnection.Open()

        Catch ex As Exception
            Err.Clear()
            Exit Sub

        End Try
        Dim TVlyhenne As String = ""
        Dim nroAuton As Integer = 0
        '     Dim NroHlon As Integer = 0
        Dim autonRek As String = ""
        Dim hloNimi As String = ""
        Dim IDriviNro As Integer
        Dim rdu As MySqlDataReader = cmd.ExecuteReader
        If rdu.HasRows = True Then
            Try
                While rdu.Read
                    kysely = Nothing
                    TVlyhenne = rdu.GetString(1)
                    nroAuton = rdu.GetValue(5)
                    '     NroHlon = rdu.GetValue(2)

                    autonRek = rdu.GetString(4)
                    hloNimi = rdu.GetString(2) & " " & rdu.GetString(3)
                    IDriviNro = rdu.GetValue(0)
                    kysely.L1Rivi = rdu.GetValue(7) ' = PalautaTiedotLahtolistaaVarten(TVlyhenne)
                    kysely.L2Rivi = rdu.GetValue(8)
                    kysely.listalle = rdu.GetValue(9)
                    kysely.TV = TVlyhenne

                    '  If sivu = 1 Then
                    If kysely.L1Rivi <> 0 Or kysely.L1Rivi <> Nothing Then
                        Dim uusiTagi As New tietoja

                        NaytaSIvu.Text = "1"
                        If tvX IsNot Nothing Then LLISTA(kysely.L1Rivi - 1).TV = TVlyhenne
                        uusiTagi.Vuoro = TVlyhenne
                        '    Dim auto As String = PalautaRekkariNumerosta(rd.GetValue(4))
                        '      If OnkoAutoAjoKelvoton(nroAuton, FormatDateTime(NaytettavaPVM.Value, DateFormat.ShortDate).ToString, "") = True Then If bil IsNot Nothing Then LLISTA(kysely.L1Rivi - 1).REK = autonRek 'uusiTagi.AutoNro = nroAuton

                        If TarkistaOnkoIlmoitettujaVikoja(nroAuton) = True Then
                            If bil IsNot Nothing Then LLISTA(kysely.L1Rivi - 1).AutoOK = False
                        Else
                            If bil IsNot Nothing Then LLISTA(kysely.L1Rivi - 1).AutoOK = True

                        End If
                        ' tarkista onko autosta tehtyhjä vikailmoituksia
                        LLISTA(kysely.L1Rivi - 1).KULI = hloNimi
                        '          LaitaPUNAISTA()
                        '    uusiTagi.HloNro = NroHlon
                        uusiTagi.riviID = IDriviNro
                        LLISTA(kysely.L1Rivi - 1).KULI = hloNimi
                        LLISTA(kysely.L1Rivi - 1).REK = autonRek
                        LLISTA(kysely.L1Rivi - 1).TV = uusiTagi.Vuoro
                        '    If uusiTagi.Vuoro = "812A" Then Beep()

                        If rdu.GetValue(6) = True Then LLISTA(kysely.L1Rivi - 1).ilm = True Else LLISTA(kysely.L1Rivi - 1).ilm = False

                    End If
                    '    End If
                    '  If sivu = 2 Then
                    If kysely.L2Rivi <> 0 Or kysely.L2Rivi <> Nothing Then
                        NaytaSIvu.Text = "2"
                        Dim uusiTagi As New tietoja

                        LLISTA(kysely.L2Rivi - 1).TV = TVlyhenne
                        uusiTagi.Vuoro = TVlyhenne

                        '     Dim auto As String = PalautaRekkariNumerosta(rd.GetValue(4))

                        '          If OnkoAutoAjoKelvoton(nroAuton, FormatDateTime(NaytettavaPVM.Value, DateFormat.ShortDate).ToString, "") = True Then LLISTA(kysely.L2Rivi - 1).REK = autonRek ': uusiTagi.AutoNro = nroAuton

                        If TarkistaOnkoIlmoitettujaVikoja(nroAuton) = True Then
                            LLISTA(kysely.L2Rivi - 1).AutoOK = False
                        Else
                            LLISTA(kysely.L2Rivi - 1).AutoOK = True

                        End If
                        ' tarkista onko autosta tehtyhjä vikailmoituksia
                        LLISTA(kysely.L2Rivi - 1).KULI = hloNimi

                        '  uusiTagi.HloNro = NroHlon
                        uusiTagi.riviID = IDriviNro

                        LLISTA(kysely.L2Rivi - 1).KULI = hloNimi
                        LLISTA(kysely.L2Rivi - 1).REK = autonRek
                        LLISTA(kysely.L2Rivi - 1).TV = uusiTagi.Vuoro
                        If rdu.GetValue(6) = True Then LLISTA(kysely.L2Rivi - 1).ilm = True Else LLISTA(kysely.L2Rivi - 1).ilm = False


                        '   End If


                    End If






                End While

            Catch ex As Exception
                Err.Clear()


            End Try
        End If

        '     Catch ex As Exception
        '          Throw ex

        '      End Try

        Dim laskuri As Boolean = False
        '           '                       '                          '                 LaitaPUNAISTA()
        If tvX IsNot Nothing Then
            For a As Integer = 0 To 49
                If tvX(a).Text <> "" Then laskuri = True

            Next
        End If

        If laskuri = False Then
            sivu = 3
            SivuVaihtuu()


        End If


        '      rd.Close()

        myTbConnection.Close()

    End Sub
    Public Function TarkistaOnkoIlmoitettujaVikoja(ByVal rek As Integer) As Boolean
        Dim paluu As Boolean = False

        myTbConnection2.Close()
        Dim cmd As New MySqlCommand()
        cmd.Connection = myTbConnection2
        cmd.CommandText = "SELECT AutoNro, Korjattu FROM VikaIlmoitukset WHERE AutoNro = @AutoNro AND Korjattu = @Korjattu"
        cmd.Parameters.AddWithValue("@AutoNro", rek)
        cmd.Parameters.AddWithValue("Korjattu", False)

        myTbConnection2.Open()

        Dim rdx As MySqlDataReader = cmd.ExecuteReader

        If rdx.HasRows = True Then
            paluu = True


        Else
            paluu = False

        End If
        myTbConnection2.Close()
        rdx.Close()


        Return paluu

    End Function
    Private Sub BackgroundWorker1_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork


        Select Case tietokannanTapahtuma
            Case 1
                LataaLahtolistaTiedot()
                LahtoListaEsille()
                hae2viikkoTiedot()
                '        Laita2viikkoa_Nakyville()
                tietokannanTapahtuma = 0
            Case 2
                '         TyhjennaLista()
                '        LahtoListaEsille()
                '    LataaPesuIlmoitukset()
                '     LataaPesemattomatAutot()
                '    LaitaPesaemattomatEsille()
                tietokannanTapahtuma = 0

            Case 3
                For i As Integer = 0 To 35
                    _AutoListaus(i).rek = ""
                    _AutoListaus(i).Maara = 0
                    _AutoListaus(i).teksti = ""
                    _AutoListaus(i).PunTausta = False
                    _AutoListaus(i).pvm = ""
                    _AutoListaus(i).punaisella = False

                Next

                tyhjennaAutoInfoNaytto()
                TarkistaKatsastukset()
                HaeAjoKieltoAutot()
                laitaAutotEsille()
                tietokannanTapahtuma = 0

                'EDIT ETÄTULOSTUS POIS PÄÄLÄ 
            Case 4

                Etatulostus.Show()



                tietokannanTapahtuma = 0

            Case 99
                Main.LuoLahtolista()
                tietokannanTapahtuma = 0

        End Select

    End Sub
    Public Sub LaskePesuilmoitukset()


        myTbConnectionPesuILM.Close()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myTbConnectionPesuILM
            .CommandType = CommandType.Text
            .CommandText = "SELECT COUNT(*) FROM Pesuilmoitukset"
        End With
        Try
            myTbConnectionPesuILM.Open()
            Dim maara As Integer = cmd.ExecuteScalar
            myTbConnectionPesuILM.Close()

            If PestytAutotMaara = 0 Or PestytAutotMaara = Nothing Then PestytAutotMaara = maara : Exit Sub

            If maara > PestytAutotMaara Then
                '     If BackgroundWorker1.IsBusy = False Then BackgroundWorker1.RunWorkerAsync() : PestytAutotMaara = maara
                tietokannanTapahtuma = 2
                PestytAutotMaara = maara

            End If

        Catch ex As Exception
            Err.Clear()

        End Try




    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        System.Threading.Thread.Sleep(500)

        BackgroundWorker1.Dispose()

    End Sub

    Public Sub InitializeBGW() 'LataaSMSpuhelimesta
        Me.BackgroundWorker1 = Nothing
        Me.BackgroundWorker1 = New BackgroundWorker
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        AddHandler Me.BackgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.BackgroundWorker1_DoWork)
        '    AddHandler Me.KasitteleSMS.ProgressChanged, New ProgressChangedEventHandler(AddressOf Me.worker_ProgressChanged)
        AddHandler Me.BackgroundWorker1.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf Me.BackgroundWorker1_RunWorkerCompleted)
        AddHandler Me.BackgroundWorker1.Disposed, New EventHandler(AddressOf Me.BackgroundWorker1_Disposed)

        '  Me.LataaSMSpuhelimesta = Nothing
        '   Me.LataaSMSpuhelimesta = New BackgroundWorker
        '    Me.LataaSMSpuhelimesta.WorkerReportsProgress = True
        '     Me.LataaSMSpuhelimesta.WorkerSupportsCancellation = True
        '      AddHandler Me.LataaSMSpuhelimesta.DoWork, New DoWorkEventHandler(AddressOf Me.LataaSMSpuhelimesta_DoWork)
        '    AddHandler Me.KasitteleSMS.ProgressChanged, New ProgressChangedEventHandler(AddressOf Me.worker_ProgressChanged)
        ' AddHandler Me.LataaSMSpuhelimesta.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf Me.LataaSMSpuhelimesta_RunWorkerCompleted)
        'AddHandler Me.LataaSMSpuhelimesta.Disposed, New EventHandler(AddressOf Me.LataaSMSpuhelimesta_Disposed)

    End Sub

    Public Sub SivuVaihtuu()

        Dim tyhja As Integer = 0

alku:
        If sivu = 2 Or sivu = 3 Then
            For i As Integer = 50 To 149
                If LLISTA(i).TV = "" Then tyhja += 1

            Next
            If tyhja = 100 Then sivu = 4
        End If

        If sivu = 1 Then
            Panel2viikkoa.Visible = False
            PanelAutoInfo.Visible = False
            panelLahtolista.Visible = True
            PanelAutoPesut.Visible = False

            NaytaSIvu.Text = "1"
            '           LaitaVuorotNakyville()
            LahtoListaEsille()

        End If

        If sivu = 2 Then

            Panel2viikkoa.Visible = False
            PanelAutoInfo.Visible = False
            panelLahtolista.Visible = True
            PanelAutoPesut.Visible = False
            NaytaSIvu.Text = "2"
            '           LaitaVuorotNakyville()
            LahtoListaEsille()

        End If

        tyhja = 0

        If sivu = 3 Then
            PanelAutoPesut.Visible = False

            Panel2viikkoa.Visible = False
            PanelAutoInfo.Visible = False
            panelLahtolista.Visible = True
            NaytaSIvu.Text = "3"
            '           LaitaVuorotNakyville()
            LahtoListaEsille()


        End If

        If sivu = 4 Then
            PanelAutoPesut.Visible = False
            PanelAutoInfo.Visible = False
            panelLahtolista.Visible = False

            Panel2viikkoa.Visible = True

        End If

        If sivu = 5 Then
            Panel2viikkoa.Visible = False
            panelLahtolista.Visible = False
            PanelAutoPesut.Visible = False
            PanelAutoInfo.Visible = True

        End If
        If sivu = 6 Then
            Panel2viikkoa.Visible = False
            PanelAutoInfo.Visible = False
            panelLahtolista.Visible = False
            PanelAutoPesut.Visible = True
            If BackgroundWorker1.IsBusy = False Then BackgroundWorker1.RunWorkerAsync()

        End If

    End Sub
    Public Sub hae2viikkoTiedot()
        Dim tanaanOn As Date = FormatDateTime(Now, DateFormat.ShortDate)
        For i As Integer = 0 To 13
            PV(i).Items.Clear()

        Next

        For i As Integer = 0 To 13
            pvm2(i).Text = FormatDateTime(tanaanOn.AddDays(i), DateFormat.ShortDate).ToString

            paiva(i).Text = PalautaViikonPaiva(tanaanOn.AddDays(i))
            HaeIkkunaTietoIlmanAutoa(CType(pvm2(i).Text, Date).ToString, i)

        Next

    End Sub
    Public Sub LaitaPUNAISTA()
        If bil IsNot Nothing And tvX IsNot Nothing Then
            For i = 0 To 49
                If tvX(i).Text <> "" And kuli(i).Text = "" Then
                    tvX(i).BackColor = Color.Red

                End If
                Try
                    If tvX(i).Text <> "" And bil(i).Text = "" Then
                        bil(i).BackColor = Color.Blue : bil(i).ForeColor = Color.Black

                        bil(i).Text = "       "

                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try


            Next
        End If

    End Sub
    Public Sub HaeIkkunaTietoIlmanAutoa(ByVal pvm As Date, ByVal ikkuna As Integer) ' As LisattavaTietoIkkunaan
        '    Dim paluu As LisattavaTietoIkkunaan = Nothing
        myTbConnection.Close()
        Dim strPVM As Date = CDate(pvm)
        Dim dAl As Date = Format(pvm, "\#yyyy\-MM\-dd\#")

        '      PV(ikkuna).Items.Clear()

        Dim cmd As New MySqlCommand()
        cmd.Connection = myTbConnection
        '    cmd.CommandText = "SELECT PVM, Vuoro, Auto, Sukunimi, Etunimi FROM AjetutTyoVuorot1 WHERE PVM = @PVM"
        cmd.CommandType = CommandType.Text

        cmd.CommandText = "SELECT DISTINCT AjetutVuorot.TVLyhenne, Henkilosto.SukuNimi, Henkilosto.EtuNimi, TyoVuorot.Lahtolistalle FROM AjetutVuorot " & _
            "INNER JOIN Henkilosto ON AjetutVuorot.HloNro=Henkilosto.HloNro " & _
           "INNER JOIN TyoVuorot ON AjetutVuorot.TVLyhenne=TyoVuorot.Lyhenne " & _
          "WHERE AlkuPVM=@AlkuPVM" ' ORDER BY Vuoro ASC"
    
        cmd.Parameters.AddWithValue("@AlkuPVM", dAl)

        Try
            myTbConnection.Open()

            Dim rdx As MySqlDataReader = cmd.ExecuteReader
            Dim nimis As String = ""
            Try
                While rdx.Read
                    nimis = ""

                    If rdx.GetString(3) = True Then

                        nimis = rdx.GetString(1) & " " & rdx.GetString(2)

                        Try

                            '   ,PV(ikkuna).Items.Add(rdx.GetString(2).ToString & vbTab & nimis)
                            Etsi2VKpaikka(nimis, rdx.GetString(0), ikkuna)

                        Catch ex As Exception
                            Err.Clear()
                        End Try

                    End If


                End While

            Catch ex As Exception
                LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE Naytto 2014", ErrorToString, 0, 0)
                Err.Clear()

                'Throw ex

            End Try

            myTbConnection.Close()


        Catch ex As Exception
            Err.Clear()

        End Try

        '      Return paluu

    End Sub
    Public Sub Etsi2VKpaikka(ByVal kuski As String, ByVal tv As String, ByVal ikkuna As Integer)
        If tv = "" Then Exit Sub
        PV(ikkuna).Items.Add(tv & vbTab & kuski)


    End Sub


    Private Sub SivunVaihto_Tick(sender As System.Object, e As System.EventArgs)
        sivu += 1
        If sivu = 7 Then sivu = 1
        SivuVaihtuu()

    End Sub

    Public Sub TyhjennaLahtoListaNaytolta()
        For i As Integer = 0 To 49

            Try
                '     If tvX IsNot Nothing Then tvX(i).Text = "" : tvX(i).ForeColor = Color.Black
                '      If bil IsNot Nothing Then bil(i).Text = "" : bil(i).ForeColor = Color.Black
                '     If kuli IsNot Nothing Then kuli(i).Text = "" : kuli(i).ForeColor = Color.Black
                '     If tvX IsNot Nothing Then tvX(i).BackColor = Color.Transparent
                '     If bil IsNot Nothing Then bil(i).BackColor = Color.Transparent
                '      If kuli IsNot Nothing Then kuli(i).BackColor = Color.Transparent
                tvX(i).Text = "" : tvX(i).ForeColor = Color.Black
                bil(i).Text = "" : bil(i).ForeColor = Color.Black
                kuli(i).Text = "" : kuli(i).ForeColor = Color.Black
                tvX(i).BackColor = Color.Transparent
                bil(i).BackColor = Color.Transparent
                kuli(i).BackColor = Color.Transparent

            Catch ex As Exception
                Err.Clear()

            End Try

        Next

        '       For i As Integer = 0 To 49
        'tvX(i).Text = ""
        '      bil(i).Text = ""
        '      kuli(i).Text = ""

        '      Next



    End Sub
    Private Sub NaytettavaPVM_ValueChanged(sender As System.Object, e As System.EventArgs) Handles NaytettavaPVM.ValueChanged
        Dim pvmm As Date = NaytettavaPVM.Value
        '     LaitaVuorotNakyville()
        ViikonPaiva.Text = PalautaViikonPaiva(pvmm)
        If FormatDateTime(pvmm, DateFormat.ShortDate) <> FormatDateTime(Now, DateFormat.ShortDate) Then

            For i As Integer = 0 To 49
                tvX(i).ForeColor = Color.Red

            Next
            ViikonPaiva.ForeColor = Color.Red
        Else
            For i As Integer = 0 To 49
                Try
                    tvX(i).ForeColor = Color.Black

                Catch ex As Exception
                    Err.Clear()
                End Try

            Next
            ViikonPaiva.ForeColor = Color.Black


        End If
        LahtoListaEsille()

    End Sub

    Private Sub NaytaSIvu_Click(sender As System.Object, e As System.EventArgs) Handles NaytaSIvu.Click
        Dim tyhja As Integer = 0

        If NaytaSIvu.Text = "1" Then
            For i As Integer = 50 To 149
                If LLISTA(i).TV = "" Then tyhja += 1

            Next
            If tyhja = 100 Then sivu = 4 : SivuVaihtuu() ': GoTo alku
        End If

        If NaytaSIvu.Text = "2" Then
            sivu = 3
            SivuVaihtuu()
            Exit Sub
        End If
        If NaytaSIvu.Text = "1" Then
            sivu = 2
            SivuVaihtuu()
            Exit Sub
        End If
        If NaytaSIvu.Text = "3" Then
            sivu = 1
            SivuVaihtuu()
            Exit Sub
        End If

    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles PesujenSeuranta.Tick
        '   LaskePesuilmoitukset()
    End Sub

    Private Sub AlaPalkki_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles AlaPalkki.ItemClicked

    End Sub

    Private Sub SivunVaihto_Tick_1(sender As Object, e As EventArgs) Handles SivunVaihto.Tick
        sivu += 1
        If sivu = 6 Then sivu = 1
        SivuVaihtuu()
        ' tietosuoja-asetuksen vuoksi pesijöiden nimetpoistettu
    End Sub

    Private Sub SeuraaTietokantaa_Tick(sender As Object, e As EventArgs) Handles SeuraaTietokantaa.Tick
        Dim cmd As New MySqlCommand()

        myTbConnectionPesuILM.Close()
        With cmd
            .Connection = myTbConnectionPesuILM
            .CommandType = CommandType.Text
            .CommandText = "SELECT COUNT(*) FROM Lataa"
        End With
        Try
            myTbConnectionPesuILM.Open()
            Dim maara As Integer = cmd.ExecuteScalar
            myTbConnectionPesuILM.Close()

            If PestytAutotMaara = 0 Or PestytAutotMaara = Nothing Then TietokantaRiviMaara = maara : Exit Sub

            If maara > TietokantaRiviMaara Then

                Dim ccc As New MySqlCommand()
                With ccc
                    .Connection = myTbConnectionPesuILM
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT Komento FROM Lataa ORDER BY ID DESC"
                End With
                Try
                    myTbConnectionPesuILM.Open()
                    Dim kom As Integer = ccc.ExecuteScalar
                    myTbConnectionPesuILM.Close()
                    Select Case kom
                        Case 1 ' päivitetään lähtölista
                            tietokannanTapahtuma = 1
                            TietokantaRiviMaara = maara

                        Case 2 ' päivitetään lähtölista ja 2 viikkoa
                            tietokannanTapahtuma = 2
                            TietokantaRiviMaara = maara

                        Case 3
                            tietokannanTapahtuma = 3
                            TietokantaRiviMaara = maara
                        Case 4

                            tietokannanTapahtuma = 4
                            TietokantaRiviMaara = maara


                    End Select
                    If BackgroundWorker1.IsBusy = False Then BackgroundWorker1.RunWorkerAsync()

                Catch ex As Exception
                    Err.Clear()

                End Try



            End If

        Catch ex As Exception
            Err.Clear()

        End Try


    End Sub
End Class