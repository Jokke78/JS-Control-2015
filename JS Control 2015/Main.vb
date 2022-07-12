Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports Microsoft.VisualBasic.PowerPacks
Imports System
Imports System.Threading
Imports System.IO.Ports
Imports System.ComponentModel

Public Class Main
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection2 As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnectionx As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection76 As MySqlConnection = New MySqlConnection(serverString)
    Private myTbConnection23 As MySqlConnection = New MySqlConnection(serverString)
    Private myYhteys As MySqlConnection = New MySqlConnection(serverString)
    '   Private Declare Function GetActiveWindow Lib "user32" Alias "GetActiveWindow" () As IntPtr
    Delegate Sub SetTextCallBack(ByVal [text] As String)
    Dim thread1 As Thread
    Dim thread2 As Thread
    Dim thread3 As Thread
    Dim thread4 As Thread

    Public luettava As String
   
    'Public Sub luedata()
    '    If SerialPort1.IsOpen = False Then
    '        Exit Sub
    '    End If
    '    If SerialPort1.IsOpen = True Then
    '        Dim x As String = SerialPort1.ReadExisting

    '        '      If Microsoft.VisualBasic.Right(x, 1) = "X" Then SerialPort1.Close()

    '        ' If x <> "X" Then
    '        'luettava &= x

    '        '  Else
    '        If x Is Nothing Then Exit Sub

    '        luettava = x.Replace("X", "")

    '        Dim pituus As Integer = luettava.Length

    '        If pituus = 9 Then luettava = "0" & luettava
    '        If pituus = 8 Then luettava = "00" & luettava
    '        If pituus = 7 Then luettava = "000" & luettava
    '        If pituus = 6 Then luettava = "0000" & luettava


    '        If Kirjautuminen.Visible = False Then
    '            SammutaNaytonAjastimet()

    '            Naytto2014.Hide()
    '            '    Me.Hide()

    '            '   Kirjautuminen.MdiParent = Me
    '            Kirjautuminen.FormBorderStyle = Windows.Forms.FormBorderStyle.None

    '            Kirjautuminen.Location = New Point(2000, 0)
    '            Kirjautuminen.Dock = DockStyle.Fill
    '            Kirjautuminen.MinimizeBox = False
    '            Kirjautuminen.MaximizeBox = False
    '            Me.TopMost = False
    '            Kirjautuminen.LuettuKoodi.Text = luettava

    '            Kirjautuminen.ShowDialog()

    '            '     Kirjautuminen.TopMost = True
    '            Kirjautuminen.WindowState = FormWindowState.Maximized


    '            Dim xx As String = Kirjautuminen.LuettuKoodi.Text

    '            Kirjautuminen.LuettuKoodi.Focus()

    '            '        KirjauduToolStripMenuItem.Enabled = False
    '            '       MainToolS.Visible = False
    '            '      MsgBox(xx)



    '        End If
    '        '  Kirjautuminen.LuettuKoodi.Text = luettava
    '        '  SerialPort1.Close()

    '        ' receivedText(luettava)
    '        '  siirrateksti(luettava)

    '        'thread4 = New Thread(AddressOf siirrateksti)
    '        'thread4.Name = luettava
    '        'thread4.Start()


    '        'End If





    '        '     Me.outputTextBox.Text = (SerialPort1.ReadExisting())

    '        'SerialPort1.Close()
    '        'initButton_Click(Nothing, Nothing)
    '    End If


    'End Sub
    Public Sub siirrateksti(ByVal teksti As String)
        Kirjautuminen.LuettuKoodi.Text = teksti

    End Sub
    
    'Private Sub receivedText(ByVal [text] As String)

    '    If Len([text]) < 10 Then Exit Sub
    '    If Kirjautuminen.LuettuKoodi.InvokeRequired Then
    '        Dim x As New SetTextCallBack(AddressOf receivedText)
    '        Kirjautuminen.LuettuKoodi.Invoke(x, New Object() {(text)})
    '    Else
    '        Kirjautuminen.LuettuKoodi.Text &= [text] & vbCrLf


    '    End If
    'End Sub
    'Private Sub pidaAktiivinenIkkunaMainina(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.LostFocus

    '    If GetActiveWindow <> Me.Handle Then

    '        '    My.Computer.Audio.Play(My.Resources.Alert, AudioPlayMode.WaitToComplete)
    '        '    FlashWindow(Me.Handle, 1)
    '        Me.Activate()
    '        Me.Cursor = New Cursor(Cursor.Current.Handle)
    '        Cursor.Position = New Point(2000, 700)
    '        Me.txtKeskitys.Focus()



    '    End If
    'End Sub
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Location = New Point(2000, 0)

        Me.WindowState = FormWindowState.Maximized

        Me.Show()
        SC.Visible = False

        '     Me.TopMost = True
        '    SP1.Panel1Collapsed = False
        '   SP1.Panel2Collapsed = True
        Try
            myYhteys.Close()
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM Raportit"
            End With
            myYhteys.Open()
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                While rd.Read

                    If rd.GetValue(0) = 1 Then
                        'lähtölistaraportin lähetyaika
                        AjastetutRaportit.klo1 = rd.GetString(1)
                    End If


                    If rd.GetValue(0) = 2 Then
                        'pesuilmoitukset lähetyaika
                        AjastetutRaportit.klo2 = rd.GetString(1)
                    End If
                    If rd.GetValue(0) = 3 Then
                        'katsastuksien seuranta lähetyaika
                        AjastetutRaportit.klo3 = rd.GetString(1)
                    End If
                    If rd.GetValue(0) = 4 Then
                        'vikailmotukset lähetyaika
                        AjastetutRaportit.klo4 = rd.GetString(1)
                    End If

                End While
            Else
                AjastetutRaportit.klo1 = "14:00:00"
                AjastetutRaportit.klo2 = "06:00:01"
                AjastetutRaportit.klo3 = "10:00:00"
                AjastetutRaportit.klo4 = "06:01:01"
            End If
            rd.Close()
            rd.Dispose()
            myYhteys.Close()


        Catch ex As Exception
            Err.Clear()
        Finally
            myYhteys.Close()

        End Try

        'Kirjautumis sivuladataa SP2.panel1
        '        Kirjautuminen.MdiParent = Me
        '       Kirjautuminen.Dock = DockStyle.Fill

        ' Kirjautuminen.Show()
        ' SP1.Panel2.Controls.Add(Kirjautuminen)
        ' SP1.Panel2Collapsed = True



        Naytto2014.MdiParent = Me
        Naytto2014.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Naytto2014.Dock = DockStyle.Fill

        Naytto2014.Show()

        'Lahtolista sivu 1 näkymä ladataan
        '     SP1.Panel2Collapsed = True
        '        _LahtolistaView1.MdiParent = Me
        '        _LahtolistaView1.Dock = DockStyle.Fill

        ' _LahtolistaView1.Show()
        ' _LahtolistaView1.WindowState = FormWindowState.Maximized
        '   SP1.Panel2Collapsed = True

        'SP1.Panel1.Controls.Add(_LahtolistaView1)

        '    SP1.Panel1Collapsed = False

        KoneenTunnisteMAC = GetMACAddress()

        LokiTapahtumanTallennus(9998, "KÄYNNISTETTY JSControl 2015", "", 0, 0)

        'AjastetutRaportit aikojen lataaminen


        If KayttajaHloNro = 314 Then
            tview.BackColor = Color.Pink

        Else
            tview.BackColor = Color.White

        End If

        myYhteys.Close()

       

        Dim cmd2 As New MySqlCommand()
        With cmd2
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT KeyPath FROM Macit WHERE ID=1"
        End With
        myYhteys.Open()
        KeyPath = cmd2.ExecuteScalar
        myYhteys.Close()
        cmd2.Dispose()

        myYhteys.Close()
        Dim pysakointicmd As New MySqlCommand()
        With pysakointicmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT Pysakointi FROM Macit WHERE ID=1"
        End With
        myYhteys.Open()
        PysakointiPalvelut = pysakointicmd.ExecuteScalar
        myYhteys.Close()
        pysakointicmd.Dispose()


        'AvaaSerialPort()
        CheckForIllegalCrossThreadCalls = False
        Try
            LahetaSpostia("Joakim Selander", "joakim.selander@kolumbus.fi", "JS CONTROL", "ONNISTUNUT uudelleen käynnistetty - StartUp")

        Catch ex As Exception
            Err.Clear()

        End Try


    End Sub
    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton6.Click
        KirjauduToolStripMenuItem_Click(Nothing, Nothing)

    End Sub

    Private Sub wLahtolista_Click(sender As Object, e As EventArgs) Handles wLahtolista.Click
        sivu = 1


        Naytto2014.SivuVaihtuu()
        Naytto2014.SivunVaihto.Interval = 20000

    End Sub

    Private Sub w2viikkoa_Click(sender As Object, e As EventArgs) Handles w2viikkoa.Click
        sivu = 4


        Naytto2014.SivuVaihtuu()
        Naytto2014.SivunVaihto.Interval = 20000

    End Sub

    Private Sub wAutoInfo_Click(sender As Object, e As EventArgs) Handles wAutoInfo.Click
        sivu = 5


        Naytto2014.SivuVaihtuu()
        Naytto2014.SivunVaihto.Interval = 20000

    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        If Naytto2014.SivunVaihto.Enabled = False Then Naytto2014.SivunVaihto.Enabled = True : ToolStripLabel2.Text = "paina F1 kirjautuaksesi ohjelmaan" : Exit Sub
        If Naytto2014.SivunVaihto.Enabled = True Then Naytto2014.SivunVaihto.Enabled = False : ToolStripLabel2.Text = "paina F1 kirjautuaksesi ohjelmaan - SIVU PYSÄYTETTY" : Exit Sub


    End Sub
    '  Delegate Sub lataaKirjautuminen()

    Public Sub lataaKirjautuminenEsille()
            SammutaNaytonAjastimet()

            Naytto2014.Hide()

            Kirjautuminen.MdiParent = Me
            Kirjautuminen.FormBorderStyle = Windows.Forms.FormBorderStyle.None

            Kirjautuminen.Location = New Point(0, 0)
            Kirjautuminen.Dock = DockStyle.Fill
            Kirjautuminen.MinimizeBox = False
            Kirjautuminen.MaximizeBox = False

            Kirjautuminen.Show()

            Kirjautuminen.TopMost = True
            Kirjautuminen.WindowState = FormWindowState.Maximized



            Kirjautuminen.LuettuKoodi.Focus()

            KirjauduToolStripMenuItem.Enabled = False
            MainToolS.Visible = False
        

    End Sub
    Private Sub KirjauduToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KirjauduToolStripMenuItem.Click

        lataaKirjautuminenEsille()


        'Dim x As lataaKirjautuminen
        'x = AddressOf lataaKirjautuminenEsille

        'x()


    End Sub

    Private Sub KellonAika_Tick(sender As Object, e As EventArgs) Handles KellonAika.Tick
        Dim klotext As String = FormatDateTime(Now, DateFormat.LongTime).ToString

        Try
            '  If Klo.Text = "23:18:00" Then MsgBox("Katsastus SMS aika")
            If klotext = "x0:04:00" Or Klo.Text = "x00:04:00" Then

                'x tämä pois käytöstä 16.2.2015 alkaen, korvattu backupperila
                Naytto2014.Close()
                Naytto2014.MdiParent = Me
                Naytto2014.FormBorderStyle = Windows.Forms.FormBorderStyle.None

                Naytto2014.Location = New Point(0, 0)
                Naytto2014.WindowState = FormWindowState.Maximized
                Naytto2014.Dock = DockStyle.Fill

                Naytto2014.Show()

                Naytto2014.TopMost = True
                sivu = 1
                Naytto2014.NaytettavaPVM.Value = Today()

            End If

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE MAIN klo 00:04 tapahtumassa", ErrorToString, 0, 0)

            Err.Clear()

        End Try
        Klo.Text = FormatDateTime(TimeOfDay, DateFormat.LongTime).ToString


        ' *********************************************************************
        ' poistettu tilapåäisesti

        ' Tarkistetaan katsastukset ja lähetetään SMS viestit
        If klotext = AjastetutRaportit.klo3 Then


            TbConnection.Close()

            Dim nytON As Date = FormatDateTime(Now, DateFormat.ShortDate)
            Dim rekkari As String = ""

            Dim erotus As TimeSpan = Nothing

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT * FROM KatsastuksienSeuranta"
            End With
            TbConnection.Open()
            Dim rd As MySqlDataReader = cmd.ExecuteReader

            Try
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
                        If KalustoReader.GetValue(14) = True And rd.GetValue(5) Is DBNull.Value Then

                            erotus = CType(rd.GetValue(4), Date) - nytON
                            If erotus.TotalDays = 30 Or erotus.TotalDays = 15 Or erotus.TotalDays = 7 Or erotus.TotalDays = 5 Or erotus.TotalDays = 1 Or erotus.TotalDays = 120 Then
                                ' LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ AIKAA KATSASTAA")
                                '   rekkari = TeeVikailmoitus.PalautaSQLNumeroRekkarista(rd.GetValue(1))

                                Dim teksti As String = ""
                                Select Case erotus.TotalDays
                                    Case 120
                                        teksti = KalustoReader.GetValue(1) & ":N VUOSIKATSASTUSAIKA ON ALKANUT. AIKAA 4 KUUKAUTTA KATSASTAA."
                                    Case 30
                                        teksti = KalustoReader.GetValue(1) & " 30 VRK AIKAA KATSASTAA"
                                    Case 15
                                        teksti = KalustoReader.GetValue(1) & " 15 VRK AIKAA KATSASTAA"
                                    Case 7
                                        teksti = KalustoReader.GetValue(1) & " 7 VRK AIKAA KATSASTAA"
                                    Case 5
                                        teksti = KalustoReader.GetValue(1) & " 5 VRK AIKAA KATSASTAA"
                                    Case 1
                                        teksti = KalustoReader.GetValue(1) & " 1 VRK AIKAA KATSASTAA"

                                End Select
                                Dim vastuukulinnro As String = ""

                                vastuukulinnro = PalautaNimestaPuhelinNumero(PalautaSQLNrostaNimi(KalustoReader.GetValue(19)), Now)

                                If vastuukulinnro = "?" Then vastuukulinnro = "0505216835"

JorelleSMSkans:

                                TbConnectionx.Close()
                                Dim Xcmd As New MySqlCommand()
                                With Xcmd
                                    .Connection = TbConnectionx
                                    .CommandType = CommandType.Text
                                    .CommandText = "INSERT INTO SMStoiminta " & _
                                        "(Kasitelty, LahettajanPuhNro, Viesti, OdottaaLahettamista, ViestinTekijaHloNro, SaapunutAika) " & _
                                        "VALUES(@Kasitelty, @LahettajanPuhNro, @Viesti, @OdottaaLahettamista, @ViestinTekijaHloNro, @SaapunutAika)"
                                    .Parameters.AddWithValue("@Kasitelty", True)
                                    .Parameters.AddWithValue("@LahettajanPuhNro", vastuukulinnro)
                                    .Parameters.AddWithValue("@Viesti", teksti)

                                    .Parameters.AddWithValue("@OdottaaLahettamista", True)
                                    .Parameters.AddWithValue("@ViestinTekijaHloNro", 314)
                                    .Parameters.AddWithValue("@SaapunutAika", CType(Now, DateTime))



                                End With
                                TbConnectionx.Open()
                                Xcmd.ExecuteNonQuery()
                                TbConnectionx.Close()

                                '          christian                      '
                                If vastuukulinnro = "0505216835" Then vastuukulinnro = "0505216834" : GoTo JorelleSMSkans
                                ' Seuraava rivi lähettä SMS:n Christianille
                                ' If vastuukulinnro = "0505216834" Then vastuukulinnro = "0505216807" : GoTo JorelleSMSkans



                            End If



                        End If



                    End If

                    KalustoReader.Close()
                    TbConnection2.Close()



                End While
            Catch ex As Exception
                LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE MAIN katsastusilmoituksien luonnit klo 10:00", ErrorToString, 0, 0)

                Err.Clear()
            Finally
                TbConnection.Close()
                rd.Dispose()


            End Try




        End If
        '        '  If Klo.Text = "22:31:00" Or Klo.Text = "22:31:00" Then


        If klotext = Microsoft.VisualBasic.Right(AjastetutRaportit.klo4, 7) Or klotext = AjastetutRaportit.klo4 Then

            If KeyPath = True Then
                'If klotext = "21:19:00" Then
                Try
                    SpostiPohja.Text = ""

                    TbConnection76.Close()
                    Dim postia As String = ""

                    '      postia = "ILMOITETUT VIAT " & vbTab & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString & vbTab & PalautaViikonPaiva(Today.AddDays(-1)) & vbCrLf & vbCrLf
                    SpostiPohja.Text = "<html><body><p><b>ILMOITETUT VIAT " & " " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString & " " & PalautaViikonPaiva(Today.AddDays(-1)) & "</b><br><br>"

                    Dim rd As New MySqlCommand()
                    Dim dAl As Date = Format(Today.AddDays(-1), "\#yyyy\-MM\-dd\#")

                    With rd
                        .Connection = TbConnection76
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT VikaIlmoitukset.AutoNro, VikaIlmoitukset.IlmoitettuVika, Henkilosto.SukuNimi, " & _
                        "Henkilosto.EtuNimi, Kalusto.RekNro, VikaIlmoitukset.LiitettyTMnro FROM VikaIlmoitukset INNER JOIN Henkilosto ON Henkilosto.HloNro = VikaIlmoitukset.HloNro " & _
                        "INNER JOIN Kalusto ON Kalusto.AutoNro = VikaIlmoitukset.AutoNro " & _
                        "WHERE DATE(VikaIlmoitukset.IlmPVM) = @Aika ORDER BY Kalusto.RekNro ASC"
                        .Parameters.AddWithValue("@Aika", dAl)

                    End With
                    TbConnection76.Open()

                    Try
                        Dim luku As MySqlDataReader = rd.ExecuteReader
                        Dim autoxx As String = ""
                        Dim vikaxx As String = ""
                        Dim ilmoittajaxx As String = ""
                        Dim Tmnroxx As Integer = 0

                        If luku.HasRows = True Then

                            While luku.Read

                                autoxx = luku.GetString(4) & " (" & luku.GetValue(0) & ") "
                                vikaxx = luku.GetString(1)
                                ilmoittajaxx = "[<i>" & luku.GetString(2) & " " & luku.GetString(3) & "</i>]" & vbCrLf
                                ' tyhjennetään ilmoittaja muuttuja 13.11.2020 alkaen ei vian ilmoittajaa näytetä
                                ilmoittajaxx = "" & vbCrLf

                                If luku.GetValue(5) IsNot DBNull.Value Then Tmnroxx = luku.GetValue(5) Else Tmnroxx = 0
                                If Tmnroxx = 0 Then
                                    SpostiPohja.Text &= autoxx & " " & vikaxx & " " & ilmoittajaxx & "<br><br>"
                                Else

                                    SpostiPohja.Text &= autoxx & " " & vikaxx & " " & ilmoittajaxx & "<b><font color='red'> TM(" & Tmnroxx.ToString & ")</font></b><br><br>"




                                End If




                            End While
                            SpostiPohja.Text &= "</body></html>"

                            Try
                                LahetaSpostia("Joakim Selander", "joakim.selander@taksikuljetus.fi", "VIKAILMOITUKSET " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString, SpostiPohja.Text)
                                LahetaSpostia("Hannu Virtanen", "hannu.virtanen@taksikuljetus.fi", "VIKAILMOITUKSET " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString, SpostiPohja.Text)
                                LahetaSpostia("Hallimestari", "hallimestari@taksikuljetus.fi", "VIKAILMOITUKSET " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString, SpostiPohja.Text)
                                LahetaSpostia("Lasse Tarvainen", "lasse.tarvainen@taksikuljetus.fi", "VIKAILMOITUKSET " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString, SpostiPohja.Text)
                                LahetaSpostia("Pertti Korri", "pertti.korri@gmail.com", "VIKAILMOITUKSET " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString, SpostiPohja.Text)
                                LahetaSpostia("Sami Oksa", "sami.oksa@taksikuljetus.fi", "VIKAILMOITUKSET " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString, SpostiPohja.Text)
                                '  LahetaSpostia("Christian Lagerstedt", "christian.lagerstedt@celena.fi", "VIKAILMOITUKSET " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString, SpostiPohja.Text)

                            Catch ex As Exception
                                LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE MAIN Vikailmoituslistan spostilähettämisessä", ErrorToString, 0, 0)

                                Err.Clear()

                            End Try
                        End If
                    Catch ex As Exception
                        LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE MAIN sposti vikailmoitukset kohta 2", ErrorToString, 0, 0)

                        Err.Clear()

                    End Try


                    TbConnection.Close()
                Catch ex As Exception
                    LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE MAIN vikailmoitukset kohta 3", ErrorToString, 0, 0)

                    Err.Clear()

                End Try
            End If


        End If
        ' LÄHETETÄÄN PESUILMOITUKSET
        ' If Klo.Text = "22:35:00" Then
        If klotext = Microsoft.VisualBasic.Right(AjastetutRaportit.klo2, 7) Or klotext = AjastetutRaportit.klo2 Then
            If KeyPath = True Then
                Try
                    TbConnection76.Close()
                    Dim postia As String = ""

                    postia = "ILMOITETUT PESUT " & vbTab & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString & vbTab & PalautaViikonPaiva(Today.AddDays(-1)) & vbCrLf & vbCrLf

                    Dim rd As New MySqlCommand()
                    Dim dAl As Date = Format(Today.AddDays(-1), "\#yyyy\-MM\-dd\#")

                    With rd
                        .Connection = TbConnection76
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT Pesuilmoitukset.AutoNro, Pesuilmoitukset.Pesuaika, Pesuilmoitukset.Numerosta, Henkilosto.SukuNimi, " & _
                        "Henkilosto.EtuNimi, Kalusto.RekNro FROM Pesuilmoitukset INNER JOIN Henkilosto ON Henkilosto.HloNro = Pesuilmoitukset.HloNro " & _
                        "INNER JOIN Kalusto ON Kalusto.AutoNro = Pesuilmoitukset.AutoNro " & _
                                  "WHERE DATE(Pesuilmoitukset.Pesuaika) = @Aika ORDER BY Kalusto.RekNro ASC"

                        '          "WHERE CAST(Pesuilmoitukset.Pesuaika as date) = @Aika ORDER BY Kalusto.RekNro ASC"
                        .Parameters.AddWithValue("@Aika", dAl)

                    End With
                    TbConnection76.Open()

                    Try
                        Dim luku As MySqlDataReader = rd.ExecuteReader

                        If luku.HasRows = True Then

                            While luku.Read

                                postia &= luku.GetString(5) & " (" & luku.GetValue(0) & ") " & vbTab & luku.GetString(3) & " " & luku.GetString(4) & vbTab & _
                                    luku.GetString(2) & vbTab & luku.GetValue(1) & vbCrLf


                            End While
                            Try
                                LahetaSpostia("Joakim Selander", "joakim.selander@taksikuljetus.fi", "PESUILMOITUKSET " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString, postia)


                                LahetaSpostia("Hannu Virtanen", "hannu.virtanen@taksikuljetus.fi", "PESUILMOITUKSET " & FormatDateTime(Today.AddDays(-1), DateFormat.ShortDate).ToString, postia)

                            Catch ex As Exception
                                Err.Clear()

                            End Try
                        End If
                    Catch ex As Exception
                        LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE MAIN Pesuilmoituksien lähetys", ErrorToString, 0, 0)

                        Err.Clear()

                    End Try

                    TbConnection76.Close()
                Catch ex As Exception
                    LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE MAIN Pesuilmoitukset osa 2", ErrorToString, 0, 0)

                    Err.Clear()

                End Try




            End If
        End If
        'lähetetään lähtölistat sähköpostilla
        ' If Klo.Text = "22:17:00" Then

        If klotext = AjastetutRaportit.klo1 And KeyPath = True Then

            TbConnection76.Close()
            '  Dim strPVM As Date = CDate(NaytettavaPVM.Value)
            Dim dAl As Date = Format(Today, "\#yyyy\-MM\-dd\#")


            Dim cmd As New MySqlCommand()
            cmd.Connection = TbConnection76
            cmd.CommandText = "SELECT PaivaTyyppi FROM AjetutVuorot WHERE AlkuPVM = @AlkuPVM"
            cmd.Parameters.AddWithValue("@ALkuPVM", dAl)
            Dim kysely As LahtoListalle = Nothing
            Try
                TbConnection76.Open()

            Catch ex As Exception
                LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE MAIN lähtölista yhteyden avaus", ErrorToString, 0, 0)

                Err.Clear()
                Exit Sub

            End Try

            Dim rd As MySqlDataReader = cmd.ExecuteReader
            Try
                If rd.HasRows = True Then
                    rd.Read()
                    '    If rd.GetValue(0) IsNot DBNull.Value Then
                    If rd.GetString(0) = "LA" Or rd.GetString(0) = "L2" Or rd.GetString(0) = "SU" Or rd.GetString(0) = "S2" Then

                        rd.Close()
                        TbConnection76.Close()
                    Else
                        tietokannanTapahtuma = 99
                        If Naytto2014.BackgroundWorker1.IsBusy = False Then Naytto2014.BackgroundWorker1.RunWorkerAsync()

                    End If
                    '  tietokannanTapahtuma = 99
                End If

                rd.Close()
                TbConnection76.Close()
            Catch ex As Exception
                LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE MAIN lähtölistan luonti", ErrorToString, 0, 0)
                Err.Clear()

            End Try


        End If
    End Sub
    Public Sub LuoLahtolista()
        '      rtb.Text = ""
        myTbConnection23.Close()
        Dim tosi As Boolean = False
        Dim paiva As Date = Today '.AddDays(-1)
        Dim listalle As LahtoListalle

        Dim lahtevaviesti As String = ""
        Dim lAika As String = ""
        Dim rekkari As String = ""
        Dim AUTOnROO As Integer = 0

        Dim kulixx As String = ""
alku:
        tosi = False

        paiva = paiva.AddDays(1)
        lahtevaviesti &= "<html><body><p><b>" & FormatDateTime(paiva, DateFormat.ShortDate).ToString & vbTab & PalautaViikonPaiva(paiva) & "</b><br><br>"
        '   Dim dAl As DateTime = Convert.ToDateTime(paiva)

        Dim cmd As New MySqlCommand()

        Using cmd
            With cmd
                .Connection = myTbConnection23
                .CommandType = CommandType.Text
                .CommandText = "SELECT AjetutVuorot.TVLyhenne, AjetutVuorot.PaivaTyyppi, AjetutVuorot.HloNro, AjetutVuorot.AutoNro, TyoVuorot.TVAlkaa, " & _
                   "AjetutVuorot.EdHlo, AjetutVuorot.EdAuto FROM AjetutVuorot " & _
                   "INNER JOIN TyoVuorot ON AjetutVuorot.TVLyhenne=TyoVuorot.Lyhenne AND (@AlkuPVM BETWEEN TyoVuorot.AlkaenPVM AND TyoVuorot.AstiPVM) " & _
                   "WHERE AjetutVuorot.AlkuPVM = @AlkuPVM"
                .Parameters.AddWithValue("@AlkuPVM", CType(paiva, Date))
            End With
            '.CommandText = "SELECT * " & _
            '  "FROM AjetutVuorot " & _
            '   "WHERE AlkuPVM = @AlkuPVM"

            Try
                myTbConnection23.Open()

            Catch ex As Exception
                Err.Clear()
            End Try

            Try
                Dim rd As MySqlDataReader = cmd.ExecuteReader
                Dim lista(1) As String
                Dim puuttulistalle(1) As String
                Dim eiAjokunnossa(1) As String


                If rd.HasRows = True Then
                    While rd.Read
                        ReDim Preserve lista(0 To UBound(lista) + 1)
                        ReDim Preserve puuttulistalle(0 To UBound(puuttulistalle) + 1)
                        ReDim Preserve eiAjokunnossa(0 To UBound(eiAjokunnossa) + 1)

                        'tarkistetaan näkyykö työvuoro lähtölistalla
                        listalle = PalautaTiedotLahtolistaaVarten(rd.GetString(0))
                        rekkari = PalautaSQLRekkariNumerosta(rd.GetValue(3))
                        AUTOnROO = rd.GetValue(3)

                        If OnkoAutoAjoKelvoton(rd.GetValue(3), FormatDateTime(paiva, DateFormat.ShortDate).ToString, "") = False Then
                            rekkari = ""
                            eiAjokunnossa(UBound(eiAjokunnossa)) = "<font color='red'>" & PalautaSQLRekkariNumerosta(rd.GetValue(3)) & "</font> " & rd.GetString(0)

                        End If


                        'jos näkyy lisätään listalle
                        If listalle.listalle = True Then
                            If rd.GetValue(4) IsNot DBNull.Value Then
                                Try
                                    If rd.GetValue(4).ToString <> "" Then lAika = Microsoft.VisualBasic.Left(rd.GetValue(4).ToString, 5)

                                Catch ex As Exception
                                    Err.Clear()
                                End Try
                            Else
                                lAika = "00:00"
                            End If
                            If rekkari <> "" Then

                                If rd.GetValue(5) <> 0 Then
                                    kulixx = "<font color='red'>" & PalautaSQLNrostaNimi(rd.GetValue(2)) & "</font>"
                                Else
                                    kulixx = PalautaSQLNrostaNimi(rd.GetValue(2))

                                End If
                                If rd.GetValue(6) <> 0 Then
                                    rekkari = "<font color='red'>" & rekkari & "</font>"

                                End If

                                lista(UBound(lista)) = "<b>" & rd.GetString(0) & "</b> " & lAika & " " & rekkari & " [<b>" & AUTOnROO & "</b>]" & " " & kulixx & "<br>"
                            Else
                                '   If rd.GetString(0) = "KOR A1" Or rd.GetString(0) = "KOR A2" Or rd.GetString(0) = "KOR I" Or rd.GetString(0) = "HP A" Or rd.GetString(0) = "HP I" Then
                                'ei lisätä
                                '   Else
                                If rd.GetValue(5) <> 0 Then
                                    kulixx = "<font color='red'>" & PalautaSQLNrostaNimi(rd.GetValue(2)) & "</font>"
                                Else
                                    kulixx = PalautaSQLNrostaNimi(rd.GetValue(2))

                                End If
                                If rd.GetValue(6) <> 0 Then
                                    rekkari = "<font color='red'>" & rekkari & "</font>"

                                End If

                                puuttulistalle(UBound(puuttulistalle)) = "<b>" & rd.GetString(0) & "</b> " & lAika & " " & rekkari & " [<b>" & AUTOnROO & "</b>]" & " " & kulixx & "<br>"

                                '    End If

                            End If
                            If rd.GetValue(1) IsNot DBNull.Value Then
                                If rd.GetString(1) = "LA" Or rd.GetString(1) = "L2" Or rd.GetString(1) = "SU" Or rd.GetString(1) = "S2" Then tosi = True
                            End If

                        End If
                    End While

                    Array.Sort(lista)
                    Array.Sort(puuttulistalle)


                    For i As Integer = 0 To puuttulistalle.Count - 1
                        If puuttulistalle(i) IsNot Nothing Then
                            If Microsoft.VisualBasic.Left(puuttulistalle(i), 5) = "<b>KO" Or Microsoft.VisualBasic.Left(puuttulistalle(i), 5) = "<b>HP" Or Microsoft.VisualBasic.Left(puuttulistalle(i), 5) = "<b>HM" Then
                                lahtevaviesti &= puuttulistalle(i) & "<br>"
                            Else
                                lahtevaviesti &= "AUTOA EI OLE MÄÄRITETTY" & vbTab & puuttulistalle(i) & "<br>"

                            End If
                            '   rtb.Refresh()

                        End If

                    Next
                    For i As Integer = 0 To eiAjokunnossa.Count - 1
                        If eiAjokunnossa(i) IsNot Nothing Then
                            lahtevaviesti &= "EI AJOKUNNOSSA" & " " & eiAjokunnossa(i) & "<br>"
                            '   rtb.Refresh()

                        End If

                    Next
                    lahtevaviesti &= vbCrLf


                    For i As Integer = 0 To lista.Count - 1
                        If lista(i) IsNot Nothing Then
                            lahtevaviesti &= lista(i) & "<br>"
                            '   rtb.Refresh()

                        End If

                    Next





                    lahtevaviesti &= "<br><br><br></body></html>"
                    rd.Close()
                    myTbConnection23.Close()
                    Erase lista
                    Erase puuttulistalle

                End If
            Catch ex As Exception
                Err.Clear()

            End Try




        End Using
        If tosi = True Then GoTo alku

        Try
            LahetaSpostia("Joakim Selander", "joakim.selander@taksikuljetus.fi", "LÄHTÖLISTA", lahtevaviesti)
            LahetaSpostia("Hannu Virtanen", "hannu.virtanen@taksikuljetus.fi", "LÄHTÖLISTA", lahtevaviesti)
            LahetaSpostia("Lasse Tarvainen", "lasse.tarvainen@taksikuljetus.fi", "LÄHTÖLISTA", lahtevaviesti)
            LahetaSpostia("Tanja Lavikainen-Virtanen", "tanja.lavikainen-virtanen@taksikuljetus.fi", "LÄHTÖLISTA", lahtevaviesti)
            LahetaSpostia("Minna Kaukoranta", "minna.kaukoranta@taksikuljetus.fi", "LÄHTÖLISTA", lahtevaviesti)
            LahetaSpostia("Jorma Kaukoranta", "jorma.kaukoranta@hotmail.com", "LÄHTÖLISTA", lahtevaviesti)
            ' LahetaSpostia("Christian Lagerstedt", "christian.lagerstedt@celena.fi", "LÄHTÖLISTA", lahtevaviesti)

            tietokannanTapahtuma = 0

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE MAIN lähtölistan lähettämisessä", ErrorToString, 0, 0)

            Err.Clear()

        End Try

    End Sub
  
    Private Sub tview_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tview.AfterSelect
        Me.SC.Panel2.Controls.Clear()
        Me.Cursor = Cursors.WaitCursor
        Try
            Select Case tview.SelectedNode.Text
                Case "Poistu"
                    Try
                        SC.Visible = False

                        '    OhjelmanValintaValikko.Close()

                        Naytto2014.MdiParent = Me
                        Naytto2014.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                        Naytto2014.Dock = DockStyle.Fill

                        Naytto2014.Show()
                        KirjauduToolStripMenuItem.Enabled = True
                        MainToolS.Visible = True
                        TbConnection.Close()
                        Dim cmd As New MySqlCommand()
                        With cmd
                            .Connection = TbConnection
                            .CommandType = CommandType.Text
                            .CommandText = "UPDATE Kirjautumiset " & _
                                "SET Ulos = @Ulos " & _
                                "WHERE ID = @ID"
                            .Parameters.AddWithValue("@ID", KirjautumisID)
                            .Parameters.AddWithValue("@Ulos", CType(Now, Date))

                        End With

                        TbConnection.Open()
                        cmd.ExecuteNonQuery()
                        TbConnection.Close()

                        KayttajaHloNro = 0
                        KayttajanAuto = 0
                        KayttajaNimi = ""
                        KayttajanSalasana = ""
                        KayttajanTaso = ""
                        KirjautumisID = 0
                        KaynnistaNaytonAjastimet()
                        Kayttaja.HloNro = 0
                        Kayttaja.Taso = 0

                    Catch ex As Exception
                        Err.Clear()

                    End Try
             

                    Try
                        'SULJETAAN FORMEJA
                        TeeTyomaaraus.Dispose()
                        TeeVikailmoitus.Dispose()
                        '    Lahtolista.Dispose()
                        TM_Palautus.Dispose()
                        ViewTallennaPesuilmoitus.Dispose()
                        LahtoListaUseatMuutokset.Dispose()
                        Kirjautuminen.Dispose()
                        NaytaTyovuorolista.Dispose()
                        ViewTallennaKatsastus.Dispose()
                        ViewHuoltohistoria.Dispose()
                        LomaJaVapaaIlmoitukset.Dispose()
                        Muuli.Dispose()
                        PysakointiHistoria.Dispose()
                        NaytaKesalomaToiveet.Dispose()
                        NaytaKesalomaToiveet.Dispose()
                        KesaLomatoiveidenIlmoittaminen.Dispose()
                        KayttoestettyLOMAKE.Dispose()

                        SelaaOhjeita.Dispose()
                        SeuraaPesuilmoituksia.Dispose()
                        TulostaTyoVuoroLista.Dispose()
                        TyomaarayksetMain.Dispose()
                        TeeTyomaaraus.Dispose()
                        TulostaTyoMaaraus.Dispose()
                        LueTiedotteita.Dispose()
                        NaytaKatsastuksienVuosiKoonti.Dispose()
                        NaytaTalvilomaToiveet.Dispose()
                        RekisteroiHloTagi.Dispose()
                        UudetTyoOhjeet.Dispose()

                        SC.Panel2.Controls.Clear()

                    Catch ex As Exception
                        Err.Clear()

                    End Try

                           Case "SAMMUTA"
                    LokiTapahtumanTallennus(KayttajaHloNro, "PAKOTETTU SAMMUTUS " & " / " & "" & " / " & "", "", KayttajaHloNro, 0)

                    End

                Case "TYÖOHJEET"
                    Me.Cursor = Cursors.WaitCursor


                    LokiTapahtumanTallennus(KayttajaHloNro, "Tulosta työohje avattu", "", 0, 0)
                    Try
                        PysakointiNappeina.Close()

                    Catch ex As Exception
                        LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE työohjeiden tulostamisen avaamisessa", ErrorToString, 0, 0)
                        Err.Clear()

                    End Try


                    UudetTyoOhjeet.MdiParent = Me
                    UudetTyoOhjeet.Dock = DockStyle.Fill

                    UudetTyoOhjeet.Show()
                    SC.Panel2.Controls.Add(UudetTyoOhjeet)
                    '   TBkortit.LataaTunnuksetDataKenttaan()
                    Me.Cursor = Cursors.Default

                Case "Pysäköintinäkymä"
                    Me.Cursor = Cursors.WaitCursor


                    LokiTapahtumanTallennus(KayttajaHloNro, "Pysäköintinäkymä avattu", "", 0, 0)
                    Try
                        PysakointiNappeina.Close()

                    Catch ex As Exception
                        LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE pysäköintinäkymän avaamisessa", ErrorToString, 0, 0)
                        Err.Clear()

                    End Try


                    PysakointiNappeina.MdiParent = Me
                    PysakointiNappeina.Dock = DockStyle.Fill

                    PysakointiNappeina.Show()
                    SC.Panel2.Controls.Add(PysakointiNappeina)
                    '   TBkortit.LataaTunnuksetDataKenttaan()
                    Me.Cursor = Cursors.Default

                Case "LT01"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT01 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT01.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "LT02"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT02 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT02.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "LT03"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT03 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT03.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "LT04"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT04 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT04.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "LT05"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT05 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT05.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "LT06"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT06 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT06.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "LT07"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT07 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT07.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "LT08"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT08 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT08.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "LT09"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT09 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT09.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "LT10"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT10 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT10.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "LT11"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT11 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT11.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "LT20"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT20 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT20.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "LT21"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT21 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT21.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "LT22"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LT22 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.LT22.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "LÄNSI"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje LÄNSI avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.Länsi_Etusivu.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                    'IDÄN OHJEET

                Case "801"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 801 avattu", "", 0, 0)

                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._801.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "802a"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 802a avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._802a.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "805"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 805 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._805.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "811"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 811 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._811.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "812a"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 812a avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._812a.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "812i"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 812i avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._812i.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "813a"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 813a avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._813a.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "813i"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 813i avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._813i.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "815a"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 815a avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._815a.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "815i"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 815i avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._815i.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "816a"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 816a avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._816a.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "816i"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 816i avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._816i.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "817a"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 817a avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._817a.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "817i"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 817i avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._817i.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "818"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 818 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._818.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "819"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 819 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._819.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "8LAR"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 8LAR avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._8LAR.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "812L"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 812L avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._812L.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "817L"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 817L avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._817L.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "V1 aamu"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje V1 aamu avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.Vantaa1_aamu.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "V1 ilta"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje V1 ilta avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.Vantaa1_ilta.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "V1 LA"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje V1 LA avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.Vantaa1_LA.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "V1 SU"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje V1 SU avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.Vantaa1_SU.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "V2K"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje V2K avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.Vantaa2.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "V3 Työvuorot"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje V3 Työvuorot avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.Vantaa3_työvuorot.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "V3 Ajokaavio arki"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje V3 Ajokaavio arki avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.Vantaa3_ajokaavio.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "V3 Ajokaavio LA"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje V3 Ajokaavio LA avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.Vantaa3_LA_ajokaavio.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "V16"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje V16 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.Vantaa3_LA_ajokaavio.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "V17"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje V17 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.Vantaa2.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "Tanjan os"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Puhelin nrot Tanjan os avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.puhTanja_1_2015.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "Minnan os"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Puhelin nrot Minnan os avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.puhMinna_1_2015.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "Raijan os"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Puhelin nrot Raijan os avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.puhRaija_1_2015.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "Joakimin os"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Puhelin nrot Joakim os avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.puhJoakim_1_2015.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "POHJOINEN"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje Phjoinen avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.pohjoinen_info.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "6041"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 6041 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._6041.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "6042 MA-TO"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 6042 MA_TO avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._6042mato.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "6042 PE"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 6042 PE avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._6042pe.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "7011 MA-TO"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 7011 MA-TO avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._7011mato.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "7011 PE"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 7011 PE avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._7011pe.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "7021 MA-TO"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 7021 MA-TO avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._7021mato.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "7021 PE"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 7021 PE avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._7021pe.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "7031"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 7031 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._7031.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "7051"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työohje 7051 avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources._7051.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)
                Case "Ruokatunnit pohj."
                    LokiTapahtumanTallennus(KayttajaHloNro, "Ruokatunnit pohj. avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.pohjoinen_ruokikset.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "Vaihtoautojen liikkuminen"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Länsi vaihtoautojen liikkuminen avattu", "", 0, 0)
                    SelaaOhjeita.MdiParent = Me : SelaaOhjeita.Dock = DockStyle.Left
                    SelaaOhjeita.PictureBox1.Image = Image.FromHbitmap(My.Resources.VaihtoautojenLiikkuminen.GetHbitmap())
                    SelaaOhjeita.Show() : SC.Panel2.Controls.Add(SelaaOhjeita)

                Case "Katsastuksien vuosikoonti"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Katsastuksien vuosikoonti avattu", "", 0, 0)
                    Try
                        NaytaKatsastuksienVuosiKoonti.Close()
                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    NaytaKatsastuksienVuosiKoonti.MdiParent = Me
                    NaytaKatsastuksienVuosiKoonti.Dock = DockStyle.Fill

                    NaytaKatsastuksienVuosiKoonti.Show()
                    SC.Panel2.Controls.Add(NaytaKatsastuksienVuosiKoonti)
                    NaytaKatsastuksienVuosiKoonti.Alkutoimet()

                Case "Katsastustiedot"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu Katsastustiedot", "", 0, 0)

                    Try
                        ViewKatsastustiedot.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    ViewKatsastustiedot.MdiParent = Me
                    ViewKatsastustiedot.Dock = DockStyle.Fill

                    ViewKatsastustiedot.Show()
                    SC.Panel2.Controls.Add(ViewKatsastustiedot)
                    Me.Cursor = Cursors.WaitCursor


                    Me.Cursor = Cursors.Default
                    ViewKatsastustiedot.LataaTunnuksetDataKenttaan()
                Case "xTarkista päivitykset"
                    Try
                        ViewUpdate.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    ViewUpdate.MdiParent = Me
                    ViewUpdate.Dock = DockStyle.Fill

                    ViewUpdate.Show()
                    SC.Panel2.Controls.Add(ViewUpdate)
                    Me.Cursor = Cursors.WaitCursor


                    Me.Cursor = Cursors.Default
                    'ViewKatsastustiedot.LataaTunnuksetDataKenttaan()

                Case "ESITETYT TALVILOMATOIVEET"
                    Try
                        NaytaTalvilomaToiveet.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    NaytaTalvilomaToiveet.MdiParent = Me
                    NaytaTalvilomaToiveet.Dock = DockStyle.Fill

                    NaytaTalvilomaToiveet.Show()
                    SC.Panel2.Controls.Add(NaytaTalvilomaToiveet)
                    Me.Cursor = Cursors.WaitCursor


                    Me.Cursor = Cursors.Default


                Case "Pesuilmoitukset"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Pesuilmoitukset avattu", "", 0, 0)

                    Try
                        SeuraaPesuilmoituksia.Close()

                    Catch ex As Exception
                        LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE pesuilmoituksien avaamisessa", ErrorToString, 0, 0)
                        Err.Clear()

                    End Try

                    SeuraaPesuilmoituksia.MdiParent = Me
                    SeuraaPesuilmoituksia.Dock = DockStyle.Fill

                    SeuraaPesuilmoituksia.Show()
                    SC.Panel2.Controls.Add(SeuraaPesuilmoituksia)
                    SeuraaPesuilmoituksia.LataaTunnuksetDataKenttaan()


                Case "Tehdyt vikailmoitukset"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu tehdyt pesuilmoitukset", "", 0, 0)

                    Try
                        TehdytVikailmoitukset.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    TehdytVikailmoitukset.MdiParent = Me
                    TehdytVikailmoitukset.Dock = DockStyle.Fill

                    TehdytVikailmoitukset.Show()
                    SC.Panel2.Controls.Add(TehdytVikailmoitukset)
                    Me.Cursor = Cursors.WaitCursor

                    TehdytVikailmoitukset.alkutoimet()
                    Me.Cursor = Cursors.Default


                Case "Työmääräykset"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Työmääräykset avattu", "", 0, 0)
                    Try
                        TyomaarayksetMain.Close()
                        TM_Palautus.Close()
                        TeeTyomaaraus.Close()

                    Catch ex As Exception
                        LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE työmääräyksien avaamisessa", ErrorToString, 0, 0)
                        Err.Clear()

                    End Try

                    TyomaarayksetMain.MdiParent = Me
                    TyomaarayksetMain.Dock = DockStyle.Fill

                    TyomaarayksetMain.Show()
                    SC.Panel2.Controls.Add(TyomaarayksetMain)
                    TyomaarayksetMain.LataaTunnuksetDataKenttaan()

                Case "Digikortit"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu digikortit", "", 0, 0)

                    Try
                        ViewArrak.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    Me.Cursor = Cursors.WaitCursor
                    ViewArrak.MdiParent = Me
                    ViewArrak.Dock = DockStyle.Fill

                    ViewArrak.Show()
                    SC.Panel2.Controls.Add(ViewArrak)
                    tview.Enabled = False


                    Me.Cursor = Cursors.Default


                Case "Pysäköinti"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Pysäköinti avattu", "", 0, 0)
                    Try
                        PysakointiHistoria.Close()

                    Catch ex As Exception
                        LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE pysäköintien avaamisessa", ErrorToString, 0, 0)
                        Err.Clear()

                    End Try

                    PysakointiHistoria.MdiParent = Me
                    PysakointiHistoria.Dock = DockStyle.Fill

                    PysakointiHistoria.Show()
                    SC.Panel2.Controls.Add(PysakointiHistoria)
                    '   PuhelinNumerot.LataaTunnuksetDataKenttaan()







                Case "Usean päivän muutokset"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu lähtölistan useat muutokset", "", 0, 0)

                    Try
                        LahtoListaUseatMuutokset.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    LahtoListaUseatMuutokset.MdiParent = Me
                    LahtoListaUseatMuutokset.Dock = DockStyle.Fill

                    LahtoListaUseatMuutokset.Show()
                    SC.Panel2.Controls.Add(LahtoListaUseatMuutokset)

                Case "Peru HSL lähtö"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu Peru HSL lähtö", "", 0, 0)

                    Try
                        Muuli.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    Muuli.MdiParent = Me
                    Muuli.Dock = DockStyle.Fill

                    Muuli.Show()
                    SC.Panel2.Controls.Add(Muuli)
                Case "Työmääräyksen palautus"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu työmääräyksien palautus", "", 0, 0)

                    Me.TopMost = False

                    Dim a As String = InputBox("ANNA PALAUTETTAVAN TYÖMÄÄRÄYKSEN NUMERO. ILMAN NUMEROA KÄYNNISTYY TYÖMÄÄRÄYKSEN PALAUTUS ILMAN TYÖMÄÄRÄYSTÄ")
                    Me.TopMost = True

                    If a = "" Then
                        Try
                            TM_Palautus.Close()

                        Catch ex As Exception
                            Err.Clear()
                        End Try
                        TM_Palautus.MdiParent = Me
                        TM_Palautus.Dock = DockStyle.Fill
                        TM_Palautus.cbRekNro.Enabled = True

                        TM_Palautus.Show()
                        SC.Panel2.Controls.Add(TM_Palautus)
                        Me.Cursor = Cursors.Default

                        Exit Sub

                    End If
                    If Val(a) = 0 Then Exit Sub
                    If a <> 0 Then
                        Try
                            TM_Palautus.Close()

                        Catch ex As Exception
                            Err.Clear()

                        End Try
                        TM_Palautus.MdiParent = Me
                        TM_Palautus.Dock = DockStyle.Fill
                        TM_Palautus.cbRekNro.Enabled = False
                        TM_Palautus.Show()
                        SC.Panel2.Controls.Add(TM_Palautus)
                        TM_Palautus.txtTMnro.Text = a
                        TM_Palautus.HaeTMtiedot()
                        Me.Cursor = Cursors.Default

                    End If

                Case "Lomat ja vapaat"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Lomat ja vapaat avattu", "", 0, 0)
                    Try

                        SC.Panel2.Controls.Clear()
                        KesaLomatoiveidenIlmoittaminen.Close()
                        KesaLomatoiveidenIlmoittaminen.Close()

                        LomaJaVapaaIlmoitukset.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try

                    LomaJaVapaaIlmoitukset.MdiParent = Me
                    LomaJaVapaaIlmoitukset.Dock = DockStyle.Fill

                    LomaJaVapaaIlmoitukset.Show()
                    SC.Panel2.Controls.Add(LomaJaVapaaIlmoitukset)
                Case "TIEDOTTEET"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Tiedotteet avattu", "", 0, 0)
                    Try
                        LueTiedotteita.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try

                    LueTiedotteita.MdiParent = Me
                    LueTiedotteita.Dock = DockStyle.Fill

                    LueTiedotteita.Show()
                    SC.Panel2.Controls.Add(LueTiedotteita)

                Case "Omat tiedot"
                    '         OmatTiedot.MdiParent = Me
                    '        OmatTiedot.Dock = DockStyle.Fill

                    'OmatTiedot.Show()
                    'SC.Panel2.Controls.Add(OmatTiedot)
                Case "Huoltohistoriat"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu huoltohistoriat", "", 0, 0)

                    Try
                        ViewHuoltohistoriaUUSI.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    ViewHuoltohistoriaUUSI.MdiParent = Me
                    ViewHuoltohistoriaUUSI.Dock = DockStyle.Fill

                    ViewHuoltohistoriaUUSI.Show()
                    SC.Panel2.Controls.Add(ViewHuoltohistoriaUUSI)

                Case "Kalusto"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu Kalusto", "", 0, 0)

                    Try
                        Kalusto.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    Kalusto.MdiParent = Me
                    Kalusto.Dock = DockStyle.Fill

                    Kalusto.Show()
                    SC.Panel2.Controls.Add(Kalusto)

                Case "Palauta ilman määräystä"
                Case "Tee TYÖMÄÄRÄYS"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu Tee työmääräys", "", 0, 0)

                    Try
                        TeeTyomaaraus.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    TeeTyomaaraus.MdiParent = Me
                    TeeTyomaaraus.Dock = DockStyle.Fill

                    TeeTyomaaraus.Show()
                    SC.Panel2.Controls.Add(TeeTyomaaraus)


                Case "Tulosta työvuorolista"


                    Try
                        Dim cmd As New MySqlCommand()
                        With cmd
                            .Connection = myYhteys
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT TVLesto FROM Asetukset WHERE ID=1"
                        End With
                        myYhteys.Open()

                        Dim rd As MySqlDataReader = cmd.ExecuteReader
                        If rd.HasRows = True Then
                            rd.Read()
                            If rd.GetValue(0) = True Then
                                rd.Close()
                                myYhteys.Close()

                                Try
                                    KayttoestettyLOMAKE.Close()

                                Catch ex As Exception
                                    Err.Clear()

                                End Try
                                LokiTapahtumanTallennus(KayttajaHloNro, "Tulosta työvuorolista KAYTTOESTETTY", "", 0, 0)
                                KayttoestettyLOMAKE.MdiParent = Me
                                KayttoestettyLOMAKE.Dock = DockStyle.Fill

                                KayttoestettyLOMAKE.Show()
                                SC.Panel2.Controls.Add(KayttoestettyLOMAKE)

                            Else
                                myYhteys.Close()

                                LokiTapahtumanTallennus(KayttajaHloNro, "Avattu Tulosta työvuorolista", "", 0, 0)

                                Try
                                    NaytaTyovuorolista.Close()

                                Catch ex As Exception
                                    Err.Clear()

                                End Try
                                LokiTapahtumanTallennus(KayttajaHloNro, "Tulosta työvuorolista avattu", "", 0, 0)
                                NaytaTyovuorolista.MdiParent = Me
                                NaytaTyovuorolista.Dock = DockStyle.Fill

                                NaytaTyovuorolista.Show()
                                SC.Panel2.Controls.Add(NaytaTyovuorolista)
                                NaytaTyovuorolista.cbKuljettaja.Text = KayttajaNimi
                                If KayttajanTaso = "K" Or KayttajanTaso = "A" Then NaytaTyovuorolista.cbKuljettaja.Enabled = False
                                If KayttajanTaso = "E" Or KayttajanTaso = "H" Or KayttajanTaso = "J" Or KayttajanTaso = "P" Then NaytaTyovuorolista.cbKuljettaja.Enabled = True

                            End If

                        End If
                    Catch ex As Exception
                        Err.Clear()
                        myYhteys.Close()
                    End Try


                Case "Ryhmäviestit (SMS)"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu lähetä ryhmätekstiviestit", "", 0, 0)

                    Try
                        RyhmaSMSviestienLahettaminen.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    RyhmaSMSviestienLahettaminen.MdiParent = Me
                    RyhmaSMSviestienLahettaminen.Dock = DockStyle.Fill

                    RyhmaSMSviestienLahettaminen.Show()
                    SC.Panel2.Controls.Add(RyhmaSMSviestienLahettaminen)


                Case ("Tallenna PESUILMOITUS")
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu Tee pesuilmoitus", "", 0, 0)

                    Try
                        ViewTallennaPesuilmoitus.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    ViewTallennaPesuilmoitus.MdiParent = Me
                    ViewTallennaPesuilmoitus.Dock = DockStyle.Fill

                    ViewTallennaPesuilmoitus.Show()
                    SC.Panel2.Controls.Add(ViewTallennaPesuilmoitus)

                Case "Tallenna KATSASTUS"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu Tallenna Katsastus", "", 0, 0)

                    Try
                        ViewTallennaKatsastus.Close()

                    Catch ex As Exception
                        Err.Clear()


                    End Try
                    ViewTallennaKatsastus.MdiParent = Me
                    ViewTallennaKatsastus.Dock = DockStyle.Fill

                    ViewTallennaKatsastus.Show()
                    SC.Panel2.Controls.Add(ViewTallennaKatsastus)


                Case "Tallenna VIKAILMOITUS"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu Tallenna vikailmoitus", "", 0, 0)

                    Try
                        TeeVikailmoitus.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    TeeVikailmoitus.MdiParent = Me
                    TeeVikailmoitus.Dock = DockStyle.Fill

                    TeeVikailmoitus.Show()
                    SC.Panel2.Controls.Add(TeeVikailmoitus)
                Case "Lähtölista"
                    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu Lähtölista", "", 0, 0)

                    Try
                        Lahtolista.Close()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    Lahtolista.MdiParent = Me
                    Lahtolista.Dock = DockStyle.Fill

                    Lahtolista.Show()
                    SC.Panel2.Controls.Add(Lahtolista)
                    Lahtolista.LataaTunnuksetDataKenttaan()

                Case "Ohjekirja"
                    '     TyonjohdonOhjekirja.MdiParent = Me
                    '     TyonjohdonOhjekirja.Dock = DockStyle.Fill

                    'TyonjohdonOhjekirja.Show()
                    'SC.Panel2.Controls.Add(TyonjohdonOhjekirja)
                    'Me.Cursor = Cursors.WaitCursor




            End Select
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE MAIN Tview valinta", ErrorToString, 0, 0)
            Err.Clear()

        End Try

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub TulostuskoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TulostuskoToolStripMenuItem.Click
        ' ohjesivuntulostuskokeilu.Location = New Point(0, 0)

        '  ohjesivuntulostuskokeilu.Show()



    End Sub

    
    Public Overridable Sub Clear()

    End Sub
    Private Sub SuljeFormejaToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles SuljeFormejaToolStripMenuItem.Click




    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As System.IO.Ports.SerialDataReceivedEventArgs)

        'thread2 = New Thread(AddressOf luedata)
        'thread2.Start()



    End Sub
End Class
