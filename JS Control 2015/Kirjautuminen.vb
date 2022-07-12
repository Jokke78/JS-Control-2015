Imports MySql.Data.MySqlClient
Imports System.Runtime.InteropServices

Public Class Kirjautuminen
    Private myYhteys As MySqlConnection = New MySqlConnection(serverString)
    Private TiedoteYhteys As MySqlConnection = New MySqlConnection(serverString)
    Private MyConnection As MySqlConnection = New MySqlConnection(serverString)


    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection2 As MySqlConnection = New MySqlConnection(serverString)
    <DllImport("user32")> _
    Private Shared Function HideCaret(ByVal hWnd As IntPtr) As Integer
    End Function

    Private Sub TextBox1_GotFocus(sender As Object, e As System.EventArgs) Handles LuettuKoodi.GotFocus
        HideCaret(LuettuKoodi.Handle)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Cursor = Cursors.WaitCursor

        'Me.TopMost = False
        Naytto2014.MdiParent = Main
        Naytto2014.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None

        Naytto2014.Location = New Point(0, 0)
        Naytto2014.WindowState = FormWindowState.Maximized
        Naytto2014.Dock = DockStyle.Fill

        Naytto2014.Show()

        Naytto2014.TopMost = True
        KaynnistaNaytonAjastimet()

        sivu = 1
        Main.KirjauduToolStripMenuItem.Enabled = True
        Main.Cursor = Cursors.Default
        '   Main.AvaaSerialPort()

        Main.MainToolS.Visible = True
        Me.Dispose()

    End Sub

    Private Sub LuettuKoodi_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles LuettuKoodi.MouseDoubleClick
        LuettuKoodi.Text = ""

    End Sub

    Private Sub LuettuKoodi_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles LuettuKoodi.MouseMove
        HideCaret(LuettuKoodi.Handle)

    End Sub
    Public Function HaeTagistaKayttajaTiedot(ByVal koodi As Integer)
        TbConnection.Close()
        Dim hloNro As Integer = 0 'cmd.ExecuteScalar
        Dim dAl As Date = Format(Today, "\#yyyy\-MM\-dd\#")
        Try
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT HloNro, Kulin, AutoNro FROM Tagit " & _
                      "WHERE (@PVM " & _
                    "BETWEEN AlkaenPVM AND AstiPVM) AND " & _
                    "(Nro = @ID) "
                .Parameters.AddWithValue("@PVM", dAl)
                .Parameters.AddWithValue("@ID", koodi)

            End With

            TbConnection.Open()
            Dim rd As MySqlDataReader = cmd.ExecuteReader()

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


                     rdhlo.Close()

                Else
                    hloNro = 0
                    rdhlo.Close()
                End If
            End If

        Catch ex As Exception
            Err.Clear()

        Finally
            TbConnection.Close()

        End Try

    


        Return hloNro

    End Function
    Private Function UusiaTiedotteita()
        Dim paluu As Boolean = False
        Try
            TbConnection.Close()
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT COUNT(*) As Lasku FROM LuettavatTiedotteet WHERE HloNro=@HloNro AND Luettu='0' "
                .Parameters.AddWithValue("@HloNro", KayttajaHloNro)

            End With
            TbConnection.Open()

            Dim maara As Integer = cmd.ExecuteScalar
            TbConnection.Close()
            If maara <= 0 Then
                paluu = False
            Else
                paluu = True
        
            End If
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE Kirjautuminen - Avoimien lomien laskuri", ErrorToString, 0, 0)

            Err.Clear()

        End Try


        Return paluu

    End Function


    Private Sub LuettuKoodi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LuettuKoodi.TextChanged
        Dim KayttajaOK As Boolean = False
        Dim tagiNro As Long = 0

        ' TAGIN TUNNISTUS
        Try
            If Len(LuettuKoodi.Text) = 10 And Val(LuettuKoodi.Text) <> 0 Then
                '    Label3.Text = "TAGI NRO " & LuettuKoodi.Text
                ' --> Tarkista tagi historiasta onko kuljettaja vai autotagi
                ' Jos kuljettaja tagi kirjaudu jos auto tagi tarkista auto sijanti
                ' Jos kortin taso on A tai H kirjaa työpäivä jne....
                '

                tagiNro = Val(LuettuKoodi.Text)
                Dim koodi As Long = Val(LuettuKoodi.Text)
                Dim paluu As Integer = HaeTagistaKayttajaTiedot(LuettuKoodi.Text)
                If paluu = -1 Then
                    LuettuKoodi.Text = ""
                    LuettuKoodi.Focus()
                    Exit Sub

                End If
                If paluu = 0 Then
                    Label2.Text = "Tuntematon TAGI - Lue TAGI"
                    LuettuKoodi.Text = "" : LuettuKoodi.Focus() : Exit Sub
                    '    'AVATAAN HloTagiRekisteröinti
                    '    Me.Close()
                    '    LokiTapahtumanTallennus(KayttajaHloNro, "Avattu Rekisteröi TAGI", "", 0, 0)

                    '    RekisteroiHloTagi.MdiParent = Main
                    '    RekisteroiHloTagi.FormBorderStyle = Windows.Forms.FormBorderStyle.None

                    '    RekisteroiHloTagi.Location = New Point(0, 0)
                    '    RekisteroiHloTagi.WindowState = FormWindowState.Maximized
                    '    RekisteroiHloTagi.Dock = DockStyle.Fill

                    '    RekisteroiHloTagi.Show()

                    '    RekisteroiHloTagi.TagiNro.Text = koodi
                    '    RekisteroiHloTagi.LuettuViivakoodi2.Focus()

                    'Exit Sub
                End If

                If paluu = -2 Then
                    Me.Close()

                    Dim AutoNro As Integer = 0
                    AutoNro = palautaTagistaAutoNro(tagiNro)
                    '         MsgBox(PalautaSQLRekkariNumerosta(AutoNro))

                    'Exit Sub
                    'TARKISTETAAN PYSÄKÖINTI TILANNE
                    '    TbConnection.Close()
                    '    Dim cmd As New MySqlCommand()
                    '    With cmd
                    '        .Connection = TbConnection
                    '        .CommandType = CommandType.Text
                    '        .CommandText = "SELECT Alue, Otettu FROM PysakointiUUSI WHERE AutoNro = @AutoNro ORDER BY Aika DESC"
                    '        .Parameters.AddWithValue("@AutoNro", AutoNro)

                    '    End With
                    '    TbConnection.Open()
                    '    Dim rd As MySqlDataReader = cmd.ExecuteReader
                    '    If rd.HasRows = True Then
                    '        rd.Read()

                    '        '       If rd.GetValue(1) = True Then
                    '        rd.Close()
                    '        Me.Close()

                    '        Main.SC.Visible = False

                    '        '    OhjelmanValintaValikko.Close()

                    '        ViewPalautaAuto.MdiParent = Main
                    '        ViewPalautaAuto.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                    '        ViewPalautaAuto.Dock = DockStyle.Fill
                    '        ViewPalautaAuto.WindowState = FormWindowState.Maximized
                    '        ViewPalautaAuto.PiiloAuto.Text = AutoNro

                    '        ViewPalautaAuto.Show()



                    '        ViewPalautaAuto.TopMost = True

                    '        LokiTapahtumanTallennus(KayttajaHloNro, "Avattu Auton palautus", "", 0, 0)

                    '        ViewPalautaAuto.otsikko.Text = "AUTON " & PalautaSQLRekkariNumerosta(AutoNro) & " PALAUTTAMINEN"
                    '        ViewPalautaAuto.PalauttajaNimi.Text = PalautaNimiTyovuoronAutonPalauttajelle(AutoNro)
                    '        '       dffd()
                    '        ViewPalautaAuto.Bilikka.Text = AutoNro
                    '        ViewPalautaAuto.TextBox1.Focus()

                    '        Exit Sub
                    '    Else
                    '        'OTETAAN AUTO AJOON

                    '        Main.SC.Visible = False

                    '        '    OhjelmanValintaValikko.Close()

                    '        KirjautumisenSplah.MdiParent = Main
                    '        KirjautumisenSplah.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                    '        KirjautumisenSplah.Dock = DockStyle.Fill
                    '        KirjautumisenSplah.WindowState = FormWindowState.Maximized

                    '        KirjautumisenSplah.Show()



                    '        KirjautumisenSplah.TopMost = True



                    '        KirjautumisenSplah.Label2.Visible = False

                    '        KirjautumisenSplah.TVtekstilla.Text = "LUE HENKILÖTAGI KIRJATAKSESI AUTON " & PalautaSQLRekkariNumerosta(AutoNro) & " AJOON"
                    '        KirjautumisenSplah.Timer1.Enabled = False
                    '        KirjautumisenSplah.AUTOOOO.Text = AutoNro

                    '        KirjautumisenSplah.laitaPysakoinninKuvaEsille(AutoNro)
                    '        KirjautumisenSplah.TextBox1.Focus()


                    '        Exit Sub

                    '    End If

                    'Else
                    ' ei pysäköinti tietoa
                    ViewPalautaAuto.MdiParent = Main
                    ViewPalautaAuto.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None

                    ViewPalautaAuto.Location = New Point(0, 0)
                    ViewPalautaAuto.WindowState = FormWindowState.Maximized
                    ViewPalautaAuto.Dock = DockStyle.Fill

                    ViewPalautaAuto.Show()

                    'HAETAAN OLETEETU AUTON PALAUTTAJAN NIMI PesuTyovuorossa -taulusta

                    ViewPalautaAuto.PalauttajaNimi.Text = PalautaNimiTyovuoronAutonPalauttajelle(AutoNro)
                    ViewPalautaAuto.PiiloAuto.Text = AutoNro


                    ViewPalautaAuto.otsikko.Text = "AUTON " & PalautaSQLRekkariNumerosta(AutoNro) & " PALAUTTAMINEN"
                    ViewPalautaAuto.Bilikka.Text = AutoNro
                    ViewPalautaAuto.TextBox1.Focus()

                    Exit Sub

                    '     End If



                    '   RekisteroiHloTagi.TagiNro.Text = koodi
                    '    Exit Sub

                End If


                ' TARKISTETAAN ONKO TYÖVUOROA, JOHON KIRJATAAN

                For i As Integer = 0 To 149

                    If LLISTA(i).KULI = KayttajaNimi Then
                        If TarvitseekoTyoVuoroKirjautumisen(LLISTA(i).TV) = True And LLISTA(i).ilm = False Then

                            LLISTA(i).ilm = True
                            tallennaTyoVuoroonIlmoittautuminen(LLISTA(i).TV)
                            EsilleSpalash(LLISTA(i).TV, LLISTA(i).KULI, palautaTyovuronAUto(LLISTA(i).TV))

                            Exit Sub
                        Else
                            KayttajaOK = True

                        End If
                    End If

                Next



                If KayttajanTaso = "E" Or KayttajanTaso = "P" Or KayttajanTaso = "J" Or KayttajanTaso = "H" Then
                    Label1.Text = "ANNA SALASANA"
                    Label2.Text = KayttajanTaso & "-tason avaaminen edellyttää salasanaa"

                    LuettuKoodi.Text = ""
                    LuettuKoodi.PasswordChar = "*"
                    LuettuKoodi.Focus()
                    Me.Cursor = Cursors.Default

                    Exit Sub
                Else
                    KayttajaOK = True

                End If

                If KayttajaOK = True Then


                    Me.Close()
                    Main.SC.Visible = True

                    SammutaNaytonAjastimet()
                    TallennaKirjautumisAika()
                    '
                    Select Case KayttajanTaso
                        Case "K"
                            Main.SC.Visible = True
                            Main.tview.Nodes.Clear()
                            '  Dim NewParentNode As TreeNode

                            Dim NewRootNode As TreeNode
                            '   Dim newChildNode As TreeNode

                            SammutaNaytonAjastimet()
                            NewRootNode = New TreeNode(KayttajaNimi)
                            NewRootNode.ForeColor = Color.Red
                            Main.tview.Nodes.Add(NewRootNode)

                            '   NewRootNode = New TreeNode("ESITETYT TALVILOMATOIVEET")
                            '   Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Tulosta työvuorolista")
                            Main.tview.Nodes.Add(NewRootNode)

                            '     NewRootNode = New TreeNode("Omat tiedot")
                            '    Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Lomat ja vapaat")
                            Main.tview.Nodes.Add(NewRootNode)

                            '  NewRootNode = New TreeNode("TIEDOTTEET")
                            '   If UusiaTiedotteita() = True Then NewRootNode.BackColor = Color.Green

                            '   Main.tview.Nodes.Add(NewRootNode)
                            '*******************************
                            ' PuhelinNumeroListat()
                            '*******************************
                            NewRootNode = New TreeNode("Tallenna VIKAILMOITUS")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Tallenna KATSASTUS")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Tallenna PESUILMOITUS")
                            Main.tview.Nodes.Add(NewRootNode)

                            '****************************
                            LisaaTyohjeet()
                            '*****************************

                            NewRootNode = New TreeNode("Poistu")
                            Main.tview.Nodes.Add(NewRootNode)

                            Dim aa As Date = Today
                            Dim erapv As Date = CType("2.3.2015", Date)
                            If aa < erapv Then
                                KesaLomatoiveidenIlmoittaminen.MdiParent = Main
                                KesaLomatoiveidenIlmoittaminen.Dock = DockStyle.Fill

                                KesaLomatoiveidenIlmoittaminen.Show()
                                Main.SC.Panel2.Controls.Add(KesaLomatoiveidenIlmoittaminen)

                            End If



                            Me.Cursor = Cursors.Default

                            Me.Dispose()

                        Case "A"
                            Main.SC.Visible = True
                            Main.tview.Nodes.Clear()

                            Dim NewRootNode As TreeNode
                            Dim NewParentNode As TreeNode
                            '   Dim newChildNode As TreeNode

                            SammutaNaytonAjastimet()
                            NewRootNode = New TreeNode(KayttajaNimi)
                            NewRootNode.ForeColor = Color.Red
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("KORJAAMO")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Tehdyt vikailmoitukset")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Huoltohistoriat")
                            Main.tview.Nodes.Add(NewRootNode)
                            '     NewParentNode = New TreeNode("TYÖMÄÄRÄYS")
                            '    NewRootNode.Nodes.Add(NewParentNode)
                            NewParentNode = New TreeNode("Työmääräyksen palautus")
                            NewRootNode.Nodes.Add(NewParentNode)


                            '     NewRootNode = New TreeNode("Omat tiedot")
                            '    Main.tview.Nodes.Add(NewRootNode)
                            '  NewRootNode = New TreeNode("TIEDOTTEET")
                            '  If UusiaTiedotteita() = True Then NewRootNode.BackColor = Color.Green

                            '  Main.tview.Nodes.Add(NewRootNode)

                            '*******************************
                            '   PuhelinNumeroListat()
                            '*******************************


                            NewRootNode = New TreeNode("Tallenna VIKAILMOITUS")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Tallenna KATSASTUS")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Tallenna PESUILMOITUS")
                            Main.tview.Nodes.Add(NewRootNode)
                            '****************************
                            LisaaTyohjeet()
                            '*****************************

                            NewRootNode = New TreeNode("Poistu")
                            Main.tview.Nodes.Add(NewRootNode)

                            Me.Close()


                        Case "H"


                    End Select
                    Me.Dispose()

                    Exit Sub
                End If

            End If
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE tagin luennassa osa 1", ErrorToString, 0, 0)

            Err.Clear()

        End Try


        'MsgBox("Koodi on " & LuettuKoodi.Text) Else MsgBox("EI OLE TAGI")

        If Label1.Text = "ANNA SALASANA" And Len(LuettuKoodi.Text) = 4 Then
            If Label1.Text = "ANNA SALASANA" Then
                If LuettuKoodi.Text.ToUpper = KayttajanSalaSana Then
                    Main.SC.Visible = True
                    Main.tview.Nodes.Clear()
                    OletusAuto = ""
                    Dim NewRootNode As TreeNode
                    Dim NewParentNode As TreeNode
                    '   Dim newChildNode As TreeNode


                    ' haetaan käyttöoikeudet

                    MyConnection.Close()
                    Dim cmd As New MySqlCommand()
                    With cmd
                        .Connection = MyConnection
                        .CommandType = CommandType.Text
                        .CommandText = "SELECT ID, Sukunimi, Etunimi, Tunnus, Salasana, eMail, AlkaenPVM, AstiPVM, Taso FROM Kayttajat WHERE HloNro=@Hlo"
                        .Parameters.AddWithValue("@Hlo", KayttajaHloNro)

                    End With
                    Try
                        MyConnection.Open()

                    Catch ex As Exception
                        Err.Clear()

                    End Try
                    '    Try
                    Dim rd As MySqlDataReader = cmd.ExecuteReader

                    If rd.HasRows = True Then
                        rd.Read()
                        Kayttaja.ID = rd.GetValue(0)
                        Kayttaja.Etunimi = rd.GetString(2)
                        Kayttaja.Sukunimi = rd.GetString(1)
                        Kayttaja.eMail = rd.GetString(5)
                        Kayttaja.AlkaenPVM = rd.GetValue(6)
                        Kayttaja.AstiPVM = rd.GetValue(7)
                        Kayttaja.Taso = rd.GetValue(8)
                        Kayttaja.HloNro = PalautaSQLNroNimiesta(Kayttaja.Sukunimi & " " & Kayttaja.Etunimi)
                        rd.Dispose()
                        cmd.Dispose()

                        Dim cmd2 As New MySqlCommand()
                        With cmd2
                            .Connection = MyConnection
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT * FROM Kaytto_Oikeudet WHERE HloNro=@hlo"
                            .Parameters.AddWithValue("@hlo", Kayttaja.HloNro)

                        End With
                        Dim rd2 As MySqlDataReader = cmd2.ExecuteReader
                        If rd2.HasRows = True Then
                            rd2.Read()
                            '************************************************************************************************
                            ' KÄYTTÖOIKEUKSIEN LUKEMINEN
                            Kayttaja.Lahtolista_Kirjautumisero = rd2.GetValue(2)
                            Kayttaja.Graafinen_tagimuokkaus = rd2.GetValue(3)
                            Kayttaja.Lahtolista_PaallekkaisetKuljettajat = rd2.GetValue(4)
                            Kayttaja.Lahtolista_Osastojako = rd2.GetValue(5)
                            Kayttaja.lahtolista_AutojenVikailmoituksientarkastaminen = rd2.GetValue(6)
                            Kayttaja.eMail_AutojenpysakointiAlueet = rd2.GetValue(7)
                            Kayttaja.lahtolista_TyoOhjeenNayttaminen = rd2.GetValue(8)
                            Kayttaja.Henkilosto_Lisatoimet = rd2.GetValue(9)
                            Kayttaja.SalliTyojaksonMuokkausJaluonti = rd2.GetValue(10)
                            Kayttaja.Lahtolista_TVhistorianSeuranta = rd2.GetValue(11)
                            Kayttaja.Lahtolista_OnkoKuljettajaaInformoitu = rd2.GetValue(12)
                            Kayttaja.PoistuminenLisatoimet = rd2.GetValue(13)
                            Kayttaja.HH_emailiin = rd2.GetValue(14)

                            '************************************************************************************************

                        End If
                        MyConnection.Close()

                        If Today < Kayttaja.AstiPVM Then

                            Main.SC.Visible = True
                            TallennaKirjautumisAika()

                            Me.Dispose()




                        End If
                    End If


                    '************************


                    SammutaNaytonAjastimet()
                    Select Case KayttajanTaso
                        Case "E"
                            NewRootNode = New TreeNode(KayttajaNimi)
                            NewRootNode.ForeColor = Color.Red
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("KORJAAMO")
                            Main.tview.Nodes.Add(NewRootNode)
                            '   NewRootNode = New TreeNode("ESITETYT TALVILOMATOIVEET")
                            '   Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Lähtölista")
                            NewRootNode.BackColor = Color.Red

                            Main.tview.Nodes.Add(NewRootNode)
                            NewParentNode = New TreeNode("Usean päivän muutokset")
                            NewRootNode.Nodes.Add(NewParentNode)
                            ' NewParentNode = New TreeNode("Lähetä sähköpostiin")
                            ' NewRootNode.Nodes.Add(NewParentNode)
                            NewParentNode = New TreeNode("Tulosta työvuorolista")
                            NewRootNode.Nodes.Add(NewParentNode)
                            NewRootNode = New TreeNode("Tehdyt vikailmoitukset")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Työmääräykset")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewParentNode = New TreeNode("Tee TYÖMÄÄRÄYS")
                            NewRootNode.Nodes.Add(NewParentNode)
                            NewParentNode = New TreeNode("Työmääräyksen palautus")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewRootNode = New TreeNode("Huoltohistoriat")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Peru HSL lähtö")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Omat tiedot")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Lomat ja vapaat")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Ryhmäviestit (SMS)")
                            Main.tview.Nodes.Add(NewRootNode)

                            '****************************
                            LisaaTyohjeet()
                            '*****************************
                            '    NewRootNode = New TreeNode("TIEDOTTEET")
                            '    If UusiaTiedotteita() = True Then NewRootNode.BackColor = Color.Green

                            'Main.tview.Nodes.Add(NewRootNode)
                            '*******************************
                            '   PuhelinNumeroListat()
                            '*******************************

                            NewRootNode = New TreeNode("Kalusto")
                            Main.tview.Nodes.Add(NewRootNode)
                            '       NewParentNode = New TreeNode("Lisää uusi auto")
                            '       NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Katsastustiedot")
                            NewRootNode.Nodes.Add(NewParentNode)
                            NewParentNode = New TreeNode("Katsastuksien vuosikoonti")
                            NewRootNode.Nodes.Add(NewParentNode)
                            ' NewParentNode = New TreeNode("Pysäköinti")
                            ' NewRootNode.Nodes.Add(NewParentNode)
                            '  NewParentNode = New TreeNode("Pysäköintinäkymä")
                            '  NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Pesuilmoitukset")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Huoltohistoriat")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Vikailmoitukset")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewRootNode = New TreeNode("Tallenna VIKAILMOITUS")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Tallenna KATSASTUS")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Tallenna PESUILMOITUS")
                            Main.tview.Nodes.Add(NewRootNode)

                            '          NewRootNode = New TreeNode("Tarkista päivitykset")
                            '         Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("SAMMUTA")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Poistu")
                            Main.tview.Nodes.Add(NewRootNode)

                            TallennaKirjautumisAika()
                            '  Main.tview.Nodes.Add(NewRootNode)
                            '   NaytaTalvilomaToiveet.MdiParent = Main
                            '    NaytaTalvilomaToiveet.Dock = DockStyle.Fill

                            '   NaytaTalvilomaToiveet.Show()
                            '    Main.SC.Panel2.Controls.Add(NaytaTalvilomaToiveet)



                            Me.Cursor = Cursors.Default

                            Me.Dispose()
                        Case "J"
                            NewRootNode = New TreeNode(KayttajaNimi)
                            NewRootNode.ForeColor = Color.Red
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("KORJAAMO")
                            Main.tview.Nodes.Add(NewRootNode)
                            '  NewRootNode = New TreeNode("ESITETYT TALVILOMATOIVEET")
                            '   Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Lähtölista")
                            NewRootNode.BackColor = Color.Red

                            Main.tview.Nodes.Add(NewRootNode)
                            NewParentNode = New TreeNode("Usean päivän muutokset")
                            NewRootNode.Nodes.Add(NewParentNode)
                            NewParentNode = New TreeNode("Lähetä sähköpostiin")
                            NewRootNode.Nodes.Add(NewParentNode)
                            NewParentNode = New TreeNode("Tulosta työvuorolista")
                            NewRootNode.Nodes.Add(NewParentNode)
                            NewRootNode = New TreeNode("Tehdyt vikailmoitukset")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Työmääräykset")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewParentNode = New TreeNode("Tee TYÖMÄÄRÄYS")
                            NewRootNode.Nodes.Add(NewParentNode)
                            NewParentNode = New TreeNode("Työmääräyksen palautus")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewRootNode = New TreeNode("Huoltohistoriat")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Digikortit")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Peru HSL lähtö")
                            Main.tview.Nodes.Add(NewRootNode)
                            '  NewRootNode = New TreeNode("Omat tiedot")
                            '  Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Lomat ja vapaat")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Kalusto")
                            Main.tview.Nodes.Add(NewRootNode)
                            '       NewParentNode = New TreeNode("Lisää uusi auto")
                            '       NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Katsastustiedot")
                            NewRootNode.Nodes.Add(NewParentNode)
                            NewParentNode = New TreeNode("Katsastuksien vuosikoonti")
                            NewRootNode.Nodes.Add(NewParentNode)
                            '   NewParentNode = New TreeNode("Pysäköinti")
                            '   NewRootNode.Nodes.Add(NewParentNode)
                            '  NewParentNode = New TreeNode("Pysäköintinäkymä")
                            '  NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Pesuilmoitukset")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Huoltohistoriat")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Vikailmoitukset")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewRootNode = New TreeNode("Tallenna VIKAILMOITUS")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Tallenna KATSASTUS")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Tallenna PESUILMOITUS")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Ryhmäviestit (SMS)")
                            Main.tview.Nodes.Add(NewRootNode)

                            '****************************
                            LisaaTyohjeet()
                            '*****************************
                            '  NewRootNode = New TreeNode("TIEDOTTEET")
                            '  If UusiaTiedotteita() = True Then NewRootNode.BackColor = Color.Green

                            '  Main.tview.Nodes.Add(NewRootNode)
                            '*******************************
                            PuhelinNumeroListat()
                            '*******************************
                            '   NewRootNode = New TreeNode("Tarkista päivitykset")
                            '   Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("SAMMUTA")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Poistu")
                            Main.tview.Nodes.Add(NewRootNode)

                            TallennaKirjautumisAika()
                            '   Main.tview.Nodes.Add(NewRootNode)
                            '    NaytaTalvilomaToiveet.MdiParent = Main
                            '    NaytaTalvilomaToiveet.Dock = DockStyle.Fill

                            '    NaytaTalvilomaToiveet.Show()
                            '    Main.SC.Panel2.Controls.Add(NaytaTalvilomaToiveet)



                            Me.Cursor = Cursors.Default

                            Me.Dispose()
                        Case "P"
                            NewRootNode = New TreeNode(KayttajaNimi)
                            NewRootNode.ForeColor = Color.Red
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("KORJAAMO")
                            Main.tview.Nodes.Add(NewRootNode)

                            '     NewRootNode = New TreeNode("ESITETYT TALVILOMATOIVEET")
                            '    Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Lähtölista")
                            NewRootNode.BackColor = Color.Red

                            Main.tview.Nodes.Add(NewRootNode)
                            NewParentNode = New TreeNode("Usean päivän muutokset")
                            NewRootNode.Nodes.Add(NewParentNode)
                            NewParentNode = New TreeNode("Lähetä sähköpostiin")
                            NewRootNode.Nodes.Add(NewParentNode)
                            NewParentNode = New TreeNode("Tulosta työvuorolista")
                            NewRootNode.Nodes.Add(NewParentNode)
                            NewRootNode = New TreeNode("Tehdyt vikailmoitukset")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Työmääräykset")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewParentNode = New TreeNode("Tee TYÖMÄÄRÄYS")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Työmääräyksen palautus")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewRootNode = New TreeNode("Huoltohistoriat")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Digikortit")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Peru HSL lähtö")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Omat tiedot")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Lomat ja vapaat")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Kalusto")
                            Main.tview.Nodes.Add(NewRootNode)
                            '       NewParentNode = New TreeNode("Lisää uusi auto")
                            '       NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Katsastustiedot")
                            NewRootNode.Nodes.Add(NewParentNode)
                            NewParentNode = New TreeNode("Katsastuksien vuosikoonti")
                            NewRootNode.Nodes.Add(NewParentNode)
                            '   NewParentNode = New TreeNode("Pysäköinti")
                            '   NewRootNode.Nodes.Add(NewParentNode)
                            '   NewParentNode = New TreeNode("Pysäköintinäkymä")
                            '   NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Pesuilmoitukset")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Huoltohistoriat")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Vikailmoitukset")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewRootNode = New TreeNode("Tallenna VIKAILMOITUS")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Tallenna KATSASTUS")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Tallenna PESUILMOITUS")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Ryhmäviestit (SMS)")
                            Main.tview.Nodes.Add(NewRootNode)

                            '****************************
                            LisaaTyohjeet()
                            '*****************************
                            '  NewRootNode = New TreeNode("Testi kartta")
                            '  Main.tview.Nodes.Add(NewRootNode)
                            '  NewRootNode = New TreeNode("TIEDOTTEET")
                            '  If UusiaTiedotteita() = True Then NewRootNode.BackColor = Color.Green

                            '   Main.tview.Nodes.Add(NewRootNode)
                            '*******************************
                            '    PuhelinNumeroListat()
                            '*******************************
                            '    NewRootNode = New TreeNode("Tarkista päivitykset")
                            '   Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("SAMMUTA")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Poistu")
                            Main.tview.Nodes.Add(NewRootNode)

                            TallennaKirjautumisAika()
                            '  Main.tview.Nodes.Add(NewRootNode)
                            '     NaytaTalvilomaToiveet.MdiParent = Main
                            '     NaytaTalvilomaToiveet.Dock = DockStyle.Fill

                            '     NaytaTalvilomaToiveet.Show()
                            '     Main.SC.Panel2.Controls.Add(NaytaTalvilomaToiveet)



                            Me.Cursor = Cursors.Default

                            Me.Dispose()
                        Case "H"
                            Main.SC.Visible = True
                            Main.tview.Nodes.Clear()

                            NewRootNode = New TreeNode(KayttajaNimi)
                            Main.tview.Nodes.Add(NewRootNode)

                            ' NewRootNode = New TreeNode("ESITETYT TALVILOMATOIVEET")
                            '  Main.tview.Nodes.Add(NewRootNode)

                            SammutaNaytonAjastimet()
                            NewRootNode = New TreeNode("Lähtölista")
                            NewRootNode.BackColor = Color.Red

                            Main.tview.Nodes.Add(NewRootNode)
                            NewParentNode = New TreeNode("Usean päivän muutokset")
                            NewRootNode.Nodes.Add(NewParentNode)
                            '            NewParentNode = New TreeNode("Lähetä sähköpostiin")
                            '           NewRootNode.Nodes.Add(NewParentNode)
                            NewParentNode = New TreeNode("Tulosta työvuorolista")
                            NewRootNode.Nodes.Add(NewParentNode)
                            '     NewRootNode = New TreeNode("Tehdyt vikailmoitukset")
                            '     Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Kalusto")
                            Main.tview.Nodes.Add(NewRootNode)
                            '       NewParentNode = New TreeNode("Lisää uusi auto")
                            '       NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Katsastustiedot")
                            NewRootNode.Nodes.Add(NewParentNode)
                            NewParentNode = New TreeNode("Katsastuksien vuosikoonti")
                            NewRootNode.Nodes.Add(NewParentNode)
                            '   NewParentNode = New TreeNode("Pysäköinti")
                            '   NewRootNode.Nodes.Add(NewParentNode)
                            '  NewParentNode = New TreeNode("Pysäköintinäkymä")
                            '  NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Pesuilmoitukset")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Huoltohistoriat")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Tehdyt vikailmoitukset")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewRootNode = New TreeNode("Työmääräykset")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewParentNode = New TreeNode("Tee TYÖMÄÄRÄYS")
                            NewRootNode.Nodes.Add(NewParentNode)

                            NewParentNode = New TreeNode("Työmääräyksen palautus")
                            NewRootNode.Nodes.Add(NewParentNode)

                            '   NewRootNode = New TreeNode("TIEDOTTEET")
                            '   If UusiaTiedotteita() = True Then NewRootNode.BackColor = Color.Green
                            '   Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Ryhmäviestit (SMS)")
                            Main.tview.Nodes.Add(NewRootNode)

                            '*******************************
                            '     PuhelinNumeroListat()
                            '*******************************

                            '     NewRootNode = New TreeNode("Huoltohistoriat")
                            '    Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Digikortit")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Peru HSL lähtö")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Omat tiedot")
                            Main.tview.Nodes.Add(NewRootNode)
                            NewRootNode = New TreeNode("Lomat ja vapaat")
                            Main.tview.Nodes.Add(NewRootNode)


                            NewRootNode = New TreeNode("Tallenna VIKAILMOITUS")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Tallenna KATSASTUS")
                            Main.tview.Nodes.Add(NewRootNode)


                            NewRootNode = New TreeNode("Tallenna PESUILMOITUS")
                            Main.tview.Nodes.Add(NewRootNode)
                            '****************************************************
                            LisaaTyohjeet()
                            '****************************************************
                            '  NewRootNode = New TreeNode("Tarkista päivitykset")
                            '  Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("SAMMUTA")
                            Main.tview.Nodes.Add(NewRootNode)

                            NewRootNode = New TreeNode("Poistu")
                            Main.tview.Nodes.Add(NewRootNode)

                            TallennaKirjautumisAika()
                            ' Main.tview.Nodes.Add(NewRootNode)
                            '    NaytaTalvilomaToiveet.MdiParent = Main
                            '    NaytaTalvilomaToiveet.Dock = DockStyle.Fill

                            '   NaytaTalvilomaToiveet.Show()
                            '   Main.SC.Panel2.Controls.Add(NaytaTalvilomaToiveet)



                            Me.Cursor = Cursors.Default

                            Me.Dispose()
                    End Select



                End If

            End If

        End If
        '     Dim conn As New OleDbConnection









    End Sub




    ' ALKUPERÄINEN KIRJAUTUMINEN
    'Private Sub LuettuKoodi_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LuettuKoodi.TextChanged
    '    Dim KayttajaOK As Boolean = False
    '    Dim tagiNro As Long = 0

    '    ' TAGIN TUNNISTUS
    '    Try
    '        If Len(LuettuKoodi.Text) = 10 And Val(LuettuKoodi.Text) <> 0 Then
    '            '    Label3.Text = "TAGI NRO " & LuettuKoodi.Text
    '            ' --> Tarkista tagi historiasta onko kuljettaja vai autotagi
    '            ' Jos kuljettaja tagi kirjaudu jos auto tagi tarkista auto sijanti
    '            ' Jos kortin taso on A tai H kirjaa työpäivä jne....
    '            '
    '            tagiNro = Val(LuettuKoodi.Text)
    '            Dim koodi As Long = Val(LuettuKoodi.Text)
    '            Dim paluu As Integer = HaeTagistaKayttajaTiedot(LuettuKoodi.Text)
    '            If paluu = -1 Then
    '                LuettuKoodi.Text = ""
    '                LuettuKoodi.Focus()
    '                Exit Sub

    '            End If
    '            If paluu = 0 Then
    '                '  LuettuKoodi.Text = "" : LuettuKoodi.Focus() : Exit Sub
    '                'AVATAAN HloTagiRekisteröinti
    '                Me.Close()
    '                LokiTapahtumanTallennus(KayttajaHloNro, "Avattu Rekisteröi TAGI", "", 0, 0)

    '                RekisteroiHloTagi.MdiParent = Main
    '                RekisteroiHloTagi.FormBorderStyle = Windows.Forms.FormBorderStyle.None

    '                RekisteroiHloTagi.Location = New Point(0, 0)
    '                RekisteroiHloTagi.WindowState = FormWindowState.Maximized
    '                RekisteroiHloTagi.Dock = DockStyle.Fill

    '                RekisteroiHloTagi.Show()

    '                RekisteroiHloTagi.TagiNro.Text = koodi
    '                RekisteroiHloTagi.LuettuViivakoodi2.Focus()

    '                Exit Sub
    '            End If

    '            If paluu = -2 Then
    '                Me.Close()

    '                Dim AutoNro As Integer = 0
    '                AutoNro = palautaTagistaAutoNro(tagiNro)
    '                '         MsgBox(PalautaSQLRekkariNumerosta(AutoNro))

    '                'Exit Sub
    '                'TARKISTETAAN PYSÄKÖINTI TILANNE
    '                TbConnection.Close()
    '                Dim cmd As New MySqlCommand()
    '                With cmd
    '                    .Connection = TbConnection
    '                    .CommandType = CommandType.Text
    '                    .CommandText = "SELECT Alue, Otettu FROM PysakointiUUSI WHERE AutoNro = @AutoNro ORDER BY Aika DESC"
    '                    .Parameters.AddWithValue("@AutoNro", AutoNro)

    '                End With
    '                TbConnection.Open()
    '                Dim rd As MySqlDataReader = cmd.ExecuteReader
    '                If rd.HasRows = True Then
    '                    rd.Read()

    '                    If rd.GetValue(1) = True Then
    '                        rd.Close()
    '                        Me.Close()

    '                        Main.SC.Visible = False

    '                        '    OhjelmanValintaValikko.Close()

    '                        ViewPalautaAuto.MdiParent = Main
    '                        ViewPalautaAuto.FormBorderStyle = Windows.Forms.FormBorderStyle.None
    '                        ViewPalautaAuto.Dock = DockStyle.Fill
    '                        ViewPalautaAuto.WindowState = FormWindowState.Maximized
    '                        ViewPalautaAuto.PiiloAuto.Text = AutoNro

    '                        ViewPalautaAuto.Show()



    '                        ViewPalautaAuto.TopMost = True

    '                              LokiTapahtumanTallennus(KayttajaHloNro, "Avattu Auton palautus", "", 0, 0)

    '                        ViewPalautaAuto.otsikko.Text = "AUTON " & PalautaSQLRekkariNumerosta(AutoNro) & " PALAUTTAMINEN"
    '                        ViewPalautaAuto.PalauttajaNimi.Text = PalautaNimiTyovuoronAutonPalauttajelle(AutoNro)
    '                        '       dffd()
    '                        ViewPalautaAuto.Bilikka.Text = AutoNro
    '                        ViewPalautaAuto.TextBox1.Focus()

    '                        Exit Sub
    '                    Else
    '                        'OTETAAN AUTO AJOON

    '                        Main.SC.Visible = False

    '                        '    OhjelmanValintaValikko.Close()

    '                        KirjautumisenSplah.MdiParent = Main
    '                        KirjautumisenSplah.FormBorderStyle = Windows.Forms.FormBorderStyle.None
    '                        KirjautumisenSplah.Dock = DockStyle.Fill
    '                        KirjautumisenSplah.WindowState = FormWindowState.Maximized

    '                        KirjautumisenSplah.Show()



    '                        KirjautumisenSplah.TopMost = True



    '                        KirjautumisenSplah.Label2.Visible = False

    '                        KirjautumisenSplah.TVtekstilla.Text = "LUE HENKILÖTAGI KIRJATAKSESI AUTON " & PalautaSQLRekkariNumerosta(AutoNro) & " AJOON"
    '                        KirjautumisenSplah.Timer1.Enabled = False
    '                        KirjautumisenSplah.AUTOOOO.Text = AutoNro

    '                        KirjautumisenSplah.laitaPysakoinninKuvaEsille(AutoNro)
    '                        KirjautumisenSplah.TextBox1.Focus()


    '                        Exit Sub

    '                    End If

    '                Else
    '                    ' ei pysäköinti tietoa
    '                    ViewPalautaAuto.MdiParent = Main
    '                    ViewPalautaAuto.FormBorderStyle = Windows.Forms.FormBorderStyle.None

    '                    ViewPalautaAuto.Location = New Point(0, 0)
    '                    ViewPalautaAuto.WindowState = FormWindowState.Maximized
    '                    ViewPalautaAuto.Dock = DockStyle.Fill

    '                    ViewPalautaAuto.Show()

    '                    'HAETAAN OLETEETU AUTON PALAUTTAJAN NIMI PesuTyovuorossa -taulusta

    '                    ViewPalautaAuto.PalauttajaNimi.Text = PalautaNimiTyovuoronAutonPalauttajelle(AutoNro)
    '                    ViewPalautaAuto.PiiloAuto.Text = AutoNro


    '                    ViewPalautaAuto.otsikko.Text = "AUTON " & PalautaSQLRekkariNumerosta(AutoNro) & " PALAUTTAMINEN"
    '                    ViewPalautaAuto.Bilikka.Text = AutoNro
    '                    ViewPalautaAuto.TextBox1.Focus()

    '                    Exit Sub

    '                End If



    '                RekisteroiHloTagi.TagiNro.Text = koodi
    '                Exit Sub

    '            End If


    '            ' TARKISTETAAN ONKO TYÖVUOROA, JOHON KIRJATAAN

    '            For i As Integer = 0 To 149

    '                If LLISTA(i).KULI = KayttajaNimi Then
    '                    If TarvitseekoTyoVuoroKirjautumisen(LLISTA(i).TV) = True And LLISTA(i).ilm = False Then

    '                        LLISTA(i).ilm = True
    '                        tallennaTyoVuoroonIlmoittautuminen(LLISTA(i).TV)
    '                        EsilleSpalash(LLISTA(i).TV, LLISTA(i).KULI, LLISTA(i).REK)

    '                        Exit Sub
    '                    Else
    '                        KayttajaOK = True

    '                    End If
    '                End If

    '            Next



    '            If KayttajanTaso = "E" Or KayttajanTaso = "P" Or KayttajanTaso = "J" Or KayttajanTaso = "H" Then
    '                Label1.Text = "ANNA SALASANA"
    '                Label2.Text = KayttajanTaso & "-tason avaaminen edellyttää salasanaa"

    '                LuettuKoodi.Text = ""
    '                LuettuKoodi.PasswordChar = "*"
    '                LuettuKoodi.Focus()
    '                Me.Cursor = Cursors.Default

    '                Exit Sub
    '            Else
    '                KayttajaOK = True

    '            End If

    '            If KayttajaOK = True Then


    '                Me.Close()
    '                Main.SC.Visible = True

    '                SammutaNaytonAjastimet()
    '                TallennaKirjautumisAika()
    '                '
    '                Select Case KayttajanTaso
    '                    Case "K"
    '                        Main.SC.Visible = True
    '                        Main.tview.Nodes.Clear()
    '                        '  Dim NewParentNode As TreeNode

    '                        Dim NewRootNode As TreeNode
    '                        '   Dim newChildNode As TreeNode

    '                        SammutaNaytonAjastimet()
    '                        NewRootNode = New TreeNode(KayttajaNimi)
    '                        NewRootNode.ForeColor = Color.Red
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        '   NewRootNode = New TreeNode("ESITETYT TALVILOMATOIVEET")
    '                        '   Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Tulosta työvuorolista")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        '     NewRootNode = New TreeNode("Omat tiedot")
    '                        '    Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Lomat ja vapaat")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        '  NewRootNode = New TreeNode("TIEDOTTEET")
    '                        '   If UusiaTiedotteita() = True Then NewRootNode.BackColor = Color.Green

    '                        '   Main.tview.Nodes.Add(NewRootNode)
    '                        '*******************************
    '                        ' PuhelinNumeroListat()
    '                        '*******************************
    '                        NewRootNode = New TreeNode("Tallenna VIKAILMOITUS")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Tallenna KATSASTUS")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Tallenna PESUILMOITUS")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        '****************************
    '                        LisaaTyohjeet()
    '                        '*****************************

    '                        NewRootNode = New TreeNode("Poistu")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        Dim aa As Date = Today
    '                        Dim erapv As Date = CType("2.3.2015", Date)
    '                        If aa < erapv Then
    '                            KesaLomatoiveidenIlmoittaminen.MdiParent = Main
    '                            KesaLomatoiveidenIlmoittaminen.Dock = DockStyle.Fill

    '                            KesaLomatoiveidenIlmoittaminen.Show()
    '                            Main.SC.Panel2.Controls.Add(KesaLomatoiveidenIlmoittaminen)

    '                        End If



    '                        Me.Cursor = Cursors.Default

    '                        Me.Dispose()

    '                    Case "A"
    '                        Main.SC.Visible = True
    '                        Main.tview.Nodes.Clear()

    '                        Dim NewRootNode As TreeNode
    '                        Dim NewParentNode As TreeNode
    '                        '   Dim newChildNode As TreeNode

    '                        SammutaNaytonAjastimet()
    '                        NewRootNode = New TreeNode(KayttajaNimi)
    '                        NewRootNode.ForeColor = Color.Red
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("KORJAAMO")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Tehdyt vikailmoitukset")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Huoltohistoriat")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        '     NewParentNode = New TreeNode("TYÖMÄÄRÄYS")
    '                        '    NewRootNode.Nodes.Add(NewParentNode)
    '                        NewParentNode = New TreeNode("Työmääräyksen palautus")
    '                        NewRootNode.Nodes.Add(NewParentNode)


    '                        '     NewRootNode = New TreeNode("Omat tiedot")
    '                        '    Main.tview.Nodes.Add(NewRootNode)
    '                        '  NewRootNode = New TreeNode("TIEDOTTEET")
    '                        '  If UusiaTiedotteita() = True Then NewRootNode.BackColor = Color.Green

    '                        '  Main.tview.Nodes.Add(NewRootNode)

    '                        '*******************************
    '                        '   PuhelinNumeroListat()
    '                        '*******************************


    '                        NewRootNode = New TreeNode("Tallenna VIKAILMOITUS")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Tallenna KATSASTUS")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Tallenna PESUILMOITUS")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        '****************************
    '                        LisaaTyohjeet()
    '                        '*****************************

    '                        NewRootNode = New TreeNode("Poistu")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        Me.Close()


    '                    Case "H"


    '                End Select
    '                Me.Dispose()

    '                Exit Sub
    '            End If

    '        End If
    '    Catch ex As Exception
    '        LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE tagin luennassa osa 1", ErrorToString, 0, 0)

    '        Err.Clear()

    '    End Try


    '    'MsgBox("Koodi on " & LuettuKoodi.Text) Else MsgBox("EI OLE TAGI")

    '    If Label1.Text = "ANNA SALASANA" And Len(LuettuKoodi.Text) = 4 Then
    '        If Label1.Text = "ANNA SALASANA" Then
    '            If LuettuKoodi.Text.ToUpper = KayttajanSalaSana Then
    '                Main.SC.Visible = True
    '                Main.tview.Nodes.Clear()
    '                OletusAuto = ""
    '                Dim NewRootNode As TreeNode
    '                Dim NewParentNode As TreeNode
    '                '   Dim newChildNode As TreeNode


    '                ' haetaan käyttöoikeudet

    '                MyConnection.Close()
    '                Dim cmd As New MySqlCommand()
    '                With cmd
    '                    .Connection = MyConnection
    '                    .CommandType = CommandType.Text
    '                    .CommandText = "SELECT ID, Sukunimi, Etunimi, Tunnus, Salasana, eMail, AlkaenPVM, AstiPVM, Taso FROM Kayttajat WHERE HloNro=@Hlo"
    '                    .Parameters.AddWithValue("@Hlo", KayttajaHloNro)

    '                End With
    '                Try
    '                    MyConnection.Open()

    '                Catch ex As Exception
    '                    Err.Clear()

    '                End Try
    '                '    Try
    '                Dim rd As MySqlDataReader = cmd.ExecuteReader

    '                If rd.HasRows = True Then
    '                    rd.Read()
    '                    Kayttaja.ID = rd.GetValue(0)
    '                    Kayttaja.Etunimi = rd.GetString(2)
    '                    Kayttaja.Sukunimi = rd.GetString(1)
    '                    Kayttaja.eMail = rd.GetString(5)
    '                    Kayttaja.AlkaenPVM = rd.GetValue(6)
    '                    Kayttaja.AstiPVM = rd.GetValue(7)
    '                    Kayttaja.Taso = rd.GetValue(8)
    '                    Kayttaja.HloNro = PalautaSQLNroNimiesta(Kayttaja.Sukunimi & " " & Kayttaja.Etunimi)
    '                    rd.Dispose()
    '                    cmd.Dispose()

    '                    Dim cmd2 As New MySqlCommand()
    '                    With cmd2
    '                        .Connection = MyConnection
    '                        .CommandType = CommandType.Text
    '                        .CommandText = "SELECT * FROM Kaytto_Oikeudet WHERE HloNro=@hlo"
    '                        .Parameters.AddWithValue("@hlo", Kayttaja.HloNro)

    '                    End With
    '                    Dim rd2 As MySqlDataReader = cmd2.ExecuteReader
    '                    If rd2.HasRows = True Then
    '                        rd2.Read()
    '                        '************************************************************************************************
    '                        ' KÄYTTÖOIKEUKSIEN LUKEMINEN
    '                        Kayttaja.Lahtolista_Kirjautumisero = rd2.GetValue(2)
    '                        Kayttaja.Graafinen_tagimuokkaus = rd2.GetValue(3)
    '                        Kayttaja.Lahtolista_PaallekkaisetKuljettajat = rd2.GetValue(4)
    '                        Kayttaja.Lahtolista_Osastojako = rd2.GetValue(5)
    '                        Kayttaja.lahtolista_AutojenVikailmoituksientarkastaminen = rd2.GetValue(6)
    '                        Kayttaja.eMail_AutojenpysakointiAlueet = rd2.GetValue(7)
    '                        Kayttaja.lahtolista_TyoOhjeenNayttaminen = rd2.GetValue(8)
    '                        Kayttaja.Henkilosto_Lisatoimet = rd2.GetValue(9)
    '                        Kayttaja.SalliTyojaksonMuokkausJaluonti = rd2.GetValue(10)
    '                        Kayttaja.Lahtolista_TVhistorianSeuranta = rd2.GetValue(11)
    '                        Kayttaja.Lahtolista_OnkoKuljettajaaInformoitu = rd2.GetValue(12)
    '                        Kayttaja.PoistuminenLisatoimet = rd2.GetValue(13)
    '                        Kayttaja.HH_emailiin = rd2.GetValue(14)

    '                        '************************************************************************************************

    '                    End If
    '                    MyConnection.Close()

    '                    If Today < Kayttaja.AstiPVM Then

    '                        Main.SC.Visible = True
    '                        TallennaKirjautumisAika()

    '                        Me.Dispose()




    '                    End If
    '                End If


    '                '************************


    '                SammutaNaytonAjastimet()
    '                Select Case KayttajanTaso
    '                    Case "E"
    '                        NewRootNode = New TreeNode(KayttajaNimi)
    '                        NewRootNode.ForeColor = Color.Red
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("KORJAAMO")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        '   NewRootNode = New TreeNode("ESITETYT TALVILOMATOIVEET")
    '                        '   Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Lähtölista")
    '                        NewRootNode.BackColor = Color.Red

    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewParentNode = New TreeNode("Usean päivän muutokset")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        ' NewParentNode = New TreeNode("Lähetä sähköpostiin")
    '                        ' NewRootNode.Nodes.Add(NewParentNode)
    '                        NewParentNode = New TreeNode("Tulosta työvuorolista")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        NewRootNode = New TreeNode("Tehdyt vikailmoitukset")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Työmääräykset")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewParentNode = New TreeNode("Tee TYÖMÄÄRÄYS")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        NewParentNode = New TreeNode("Työmääräyksen palautus")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewRootNode = New TreeNode("Huoltohistoriat")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Peru HSL lähtö")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Omat tiedot")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Lomat ja vapaat")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Ryhmäviestit (SMS)")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        '****************************
    '                        LisaaTyohjeet()
    '                        '*****************************
    '                        '    NewRootNode = New TreeNode("TIEDOTTEET")
    '                        '    If UusiaTiedotteita() = True Then NewRootNode.BackColor = Color.Green

    '                        'Main.tview.Nodes.Add(NewRootNode)
    '                        '*******************************
    '                        '   PuhelinNumeroListat()
    '                        '*******************************

    '                        NewRootNode = New TreeNode("Kalusto")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        '       NewParentNode = New TreeNode("Lisää uusi auto")
    '                        '       NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Katsastustiedot")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        NewParentNode = New TreeNode("Katsastuksien vuosikoonti")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        ' NewParentNode = New TreeNode("Pysäköinti")
    '                        ' NewRootNode.Nodes.Add(NewParentNode)
    '                        '  NewParentNode = New TreeNode("Pysäköintinäkymä")
    '                        '  NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Pesuilmoitukset")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Huoltohistoriat")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Vikailmoitukset")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewRootNode = New TreeNode("Tallenna VIKAILMOITUS")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Tallenna KATSASTUS")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Tallenna PESUILMOITUS")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        '          NewRootNode = New TreeNode("Tarkista päivitykset")
    '                        '         Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("SAMMUTA")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Poistu")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        TallennaKirjautumisAika()
    '                        '  Main.tview.Nodes.Add(NewRootNode)
    '                        '   NaytaTalvilomaToiveet.MdiParent = Main
    '                        '    NaytaTalvilomaToiveet.Dock = DockStyle.Fill

    '                        '   NaytaTalvilomaToiveet.Show()
    '                        '    Main.SC.Panel2.Controls.Add(NaytaTalvilomaToiveet)



    '                        Me.Cursor = Cursors.Default

    '                        Me.Dispose()
    '                    Case "J"
    '                        NewRootNode = New TreeNode(KayttajaNimi)
    '                        NewRootNode.ForeColor = Color.Red
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("KORJAAMO")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        '  NewRootNode = New TreeNode("ESITETYT TALVILOMATOIVEET")
    '                        '   Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Lähtölista")
    '                        NewRootNode.BackColor = Color.Red

    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewParentNode = New TreeNode("Usean päivän muutokset")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        NewParentNode = New TreeNode("Lähetä sähköpostiin")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        NewParentNode = New TreeNode("Tulosta työvuorolista")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        NewRootNode = New TreeNode("Tehdyt vikailmoitukset")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Työmääräykset")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewParentNode = New TreeNode("Tee TYÖMÄÄRÄYS")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        NewParentNode = New TreeNode("Työmääräyksen palautus")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewRootNode = New TreeNode("Huoltohistoriat")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Digikortit")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Peru HSL lähtö")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        '  NewRootNode = New TreeNode("Omat tiedot")
    '                        '  Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Lomat ja vapaat")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Kalusto")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        '       NewParentNode = New TreeNode("Lisää uusi auto")
    '                        '       NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Katsastustiedot")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        NewParentNode = New TreeNode("Katsastuksien vuosikoonti")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        '   NewParentNode = New TreeNode("Pysäköinti")
    '                        '   NewRootNode.Nodes.Add(NewParentNode)
    '                        '  NewParentNode = New TreeNode("Pysäköintinäkymä")
    '                        '  NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Pesuilmoitukset")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Huoltohistoriat")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Vikailmoitukset")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewRootNode = New TreeNode("Tallenna VIKAILMOITUS")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Tallenna KATSASTUS")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Tallenna PESUILMOITUS")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Ryhmäviestit (SMS)")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        '****************************
    '                        LisaaTyohjeet()
    '                        '*****************************
    '                        '  NewRootNode = New TreeNode("TIEDOTTEET")
    '                        '  If UusiaTiedotteita() = True Then NewRootNode.BackColor = Color.Green

    '                        '  Main.tview.Nodes.Add(NewRootNode)
    '                        '*******************************
    '                        PuhelinNumeroListat()
    '                        '*******************************
    '                        '   NewRootNode = New TreeNode("Tarkista päivitykset")
    '                        '   Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("SAMMUTA")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Poistu")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        TallennaKirjautumisAika()
    '                        '   Main.tview.Nodes.Add(NewRootNode)
    '                        '    NaytaTalvilomaToiveet.MdiParent = Main
    '                        '    NaytaTalvilomaToiveet.Dock = DockStyle.Fill

    '                        '    NaytaTalvilomaToiveet.Show()
    '                        '    Main.SC.Panel2.Controls.Add(NaytaTalvilomaToiveet)



    '                        Me.Cursor = Cursors.Default

    '                        Me.Dispose()
    '                    Case "P"
    '                        NewRootNode = New TreeNode(KayttajaNimi)
    '                        NewRootNode.ForeColor = Color.Red
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("KORJAAMO")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        '     NewRootNode = New TreeNode("ESITETYT TALVILOMATOIVEET")
    '                        '    Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Lähtölista")
    '                        NewRootNode.BackColor = Color.Red

    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewParentNode = New TreeNode("Usean päivän muutokset")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        NewParentNode = New TreeNode("Lähetä sähköpostiin")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        NewParentNode = New TreeNode("Tulosta työvuorolista")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        NewRootNode = New TreeNode("Tehdyt vikailmoitukset")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Työmääräykset")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewParentNode = New TreeNode("Tee TYÖMÄÄRÄYS")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Työmääräyksen palautus")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewRootNode = New TreeNode("Huoltohistoriat")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Digikortit")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Peru HSL lähtö")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Omat tiedot")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Lomat ja vapaat")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Kalusto")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        '       NewParentNode = New TreeNode("Lisää uusi auto")
    '                        '       NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Katsastustiedot")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        NewParentNode = New TreeNode("Katsastuksien vuosikoonti")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        '   NewParentNode = New TreeNode("Pysäköinti")
    '                        '   NewRootNode.Nodes.Add(NewParentNode)
    '                        '   NewParentNode = New TreeNode("Pysäköintinäkymä")
    '                        '   NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Pesuilmoitukset")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Huoltohistoriat")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Vikailmoitukset")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewRootNode = New TreeNode("Tallenna VIKAILMOITUS")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Tallenna KATSASTUS")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Tallenna PESUILMOITUS")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Ryhmäviestit (SMS)")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        '****************************
    '                        LisaaTyohjeet()
    '                        '*****************************
    '                        '  NewRootNode = New TreeNode("Testi kartta")
    '                        '  Main.tview.Nodes.Add(NewRootNode)
    '                        '  NewRootNode = New TreeNode("TIEDOTTEET")
    '                        '  If UusiaTiedotteita() = True Then NewRootNode.BackColor = Color.Green

    '                        '   Main.tview.Nodes.Add(NewRootNode)
    '                        '*******************************
    '                        PuhelinNumeroListat()
    '                        '*******************************
    '                        '    NewRootNode = New TreeNode("Tarkista päivitykset")
    '                        '   Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("SAMMUTA")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Poistu")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        TallennaKirjautumisAika()
    '                        '  Main.tview.Nodes.Add(NewRootNode)
    '                        '     NaytaTalvilomaToiveet.MdiParent = Main
    '                        '     NaytaTalvilomaToiveet.Dock = DockStyle.Fill

    '                        '     NaytaTalvilomaToiveet.Show()
    '                        '     Main.SC.Panel2.Controls.Add(NaytaTalvilomaToiveet)



    '                        Me.Cursor = Cursors.Default

    '                        Me.Dispose()
    '                    Case "H"
    '                        Main.SC.Visible = True
    '                        Main.tview.Nodes.Clear()

    '                        NewRootNode = New TreeNode(KayttajaNimi)
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        ' NewRootNode = New TreeNode("ESITETYT TALVILOMATOIVEET")
    '                        '  Main.tview.Nodes.Add(NewRootNode)

    '                        SammutaNaytonAjastimet()
    '                        NewRootNode = New TreeNode("Lähtölista")
    '                        NewRootNode.BackColor = Color.Red

    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewParentNode = New TreeNode("Usean päivän muutokset")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        '            NewParentNode = New TreeNode("Lähetä sähköpostiin")
    '                        '           NewRootNode.Nodes.Add(NewParentNode)
    '                        NewParentNode = New TreeNode("Tulosta työvuorolista")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        '     NewRootNode = New TreeNode("Tehdyt vikailmoitukset")
    '                        '     Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Kalusto")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        '       NewParentNode = New TreeNode("Lisää uusi auto")
    '                        '       NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Katsastustiedot")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        NewParentNode = New TreeNode("Katsastuksien vuosikoonti")
    '                        NewRootNode.Nodes.Add(NewParentNode)
    '                        '   NewParentNode = New TreeNode("Pysäköinti")
    '                        '   NewRootNode.Nodes.Add(NewParentNode)
    '                        '  NewParentNode = New TreeNode("Pysäköintinäkymä")
    '                        '  NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Pesuilmoitukset")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Huoltohistoriat")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Tehdyt vikailmoitukset")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewRootNode = New TreeNode("Työmääräykset")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewParentNode = New TreeNode("Tee TYÖMÄÄRÄYS")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        NewParentNode = New TreeNode("Työmääräyksen palautus")
    '                        NewRootNode.Nodes.Add(NewParentNode)

    '                        '   NewRootNode = New TreeNode("TIEDOTTEET")
    '                        '   If UusiaTiedotteita() = True Then NewRootNode.BackColor = Color.Green
    '                        '   Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Ryhmäviestit (SMS)")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        '*******************************
    '                        '     PuhelinNumeroListat()
    '                        '*******************************

    '                        '     NewRootNode = New TreeNode("Huoltohistoriat")
    '                        '    Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Digikortit")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Peru HSL lähtö")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Omat tiedot")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        NewRootNode = New TreeNode("Lomat ja vapaat")
    '                        Main.tview.Nodes.Add(NewRootNode)


    '                        NewRootNode = New TreeNode("Tallenna VIKAILMOITUS")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Tallenna KATSASTUS")
    '                        Main.tview.Nodes.Add(NewRootNode)


    '                        NewRootNode = New TreeNode("Tallenna PESUILMOITUS")
    '                        Main.tview.Nodes.Add(NewRootNode)
    '                        '****************************************************
    '                        LisaaTyohjeet()
    '                        '****************************************************
    '                        '  NewRootNode = New TreeNode("Tarkista päivitykset")
    '                        '  Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("SAMMUTA")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        NewRootNode = New TreeNode("Poistu")
    '                        Main.tview.Nodes.Add(NewRootNode)

    '                        TallennaKirjautumisAika()
    '                        ' Main.tview.Nodes.Add(NewRootNode)
    '                        '    NaytaTalvilomaToiveet.MdiParent = Main
    '                        '    NaytaTalvilomaToiveet.Dock = DockStyle.Fill

    '                        '   NaytaTalvilomaToiveet.Show()
    '                        '   Main.SC.Panel2.Controls.Add(NaytaTalvilomaToiveet)



    '                        Me.Cursor = Cursors.Default

    '                        Me.Dispose()
    '                End Select



    '            End If

    '        End If

    '    End If
    '    '     Dim conn As New OleDbConnection









    'End Sub
    Public Sub PuhelinNumeroListat()
        Dim NewRootNode As TreeNode
        Dim NewParentNode As TreeNode

        NewRootNode = New TreeNode("Puhelinumerolista")
        Main.tview.Nodes.Add(NewRootNode)
        NewParentNode = New TreeNode("Tanjan os")
        NewRootNode.Nodes.Add(NewParentNode)
        NewParentNode = New TreeNode("Minnan os")
        NewRootNode.Nodes.Add(NewParentNode)
        NewParentNode = New TreeNode("Raijan os")
        NewRootNode.Nodes.Add(NewParentNode)
        NewParentNode = New TreeNode("Joakimin os")
        NewRootNode.Nodes.Add(NewParentNode)

    End Sub

    Public Function palautaTyovuronAUto(ByVal tvlyhenne As String)
        Dim rekkaaari As String = ""
        Dim dAl As Date = Format(Today, "\#yyyy\-MM\-dd\#")
        Dim rekkariksi As String = ""
        Try
            myYhteys.Close()
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT AutoNro FROM AjetutVuorot WHERE AlkuPVM=@dal AND TVLyhenne=@tv"
                .Parameters.AddWithValue("@dal", dAl)
                .Parameters.AddWithValue("@tv", tvlyhenne)

            End With
            myYhteys.Open()
            rekkaaari = cmd.ExecuteScalar
            myYhteys.Close()

            rekkariksi = PalautaSQLRekkariNumerosta(rekkaaari)

        Catch ex As Exception
            Err.Clear()
        Finally
            myYhteys.Close()
        End Try


        Return rekkariksi
    End Function
    Public Sub LisaaTyohjeet()
        '    Dim someDateAndTime As Date = #8/13/2015 12:14:00 PM#
        If KeyPath = True Then
            ' ladataan uusi työohjeden katsomis ja tulostamisohjelma

            Dim NewRootNode As TreeNode
   

            NewRootNode = New TreeNode("TYÖOHJEET")
            Main.tview.Nodes.Add(NewRootNode)

            Exit Sub
        End If

        Try
            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT TOTesto FROM Asetukset WHERE ID=1"
            End With
            myYhteys.Open()

            Dim rd As MySqlDataReader = cmd2.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                If rd.GetValue(0) = True Then
                    rd.Close()
                    myYhteys.Close()


                Else


                    Dim NewRootNode As TreeNode
                    Dim NewParentNode As TreeNode
                    Dim NewChildNode As TreeNode

                    NewRootNode = New TreeNode("TYÖOHJEET")
                    Main.tview.Nodes.Add(NewRootNode)

                    NewParentNode = New TreeNode("LÄNSI")
                    NewRootNode.Nodes.Add(NewParentNode)
                    '   Main.TV.SelectedNode.Add(NewParentNode)

                    NewChildNode = New TreeNode("LT01")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("LT02")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("LT03")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("LT04")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("LT05")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("LT06")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("LT07")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("LT08")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("LT09")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("LT10")
                    NewParentNode.Nodes.Add(NewChildNode)
                    If Today < #7/21/2015 11:00:00 AM# Then

                        NewChildNode = New TreeNode("LT11")
                        NewParentNode.Nodes.Add(NewChildNode)
                    End If

                    NewChildNode = New TreeNode("LT20")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("LT21")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("LT22")
                    NewParentNode.Nodes.Add(NewChildNode)

                    '*****************************
                    NewParentNode = New TreeNode("ITÄ")
                    NewRootNode.Nodes.Add(NewParentNode)
                    NewChildNode = New TreeNode("801")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("802a")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("805")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("811")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("812a")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("812i")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("813a")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("813i")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("815a")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("815i")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("816a")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("816i")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("817a")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("817i")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("818")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("819")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("8LAR")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("812L")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("817L")
                    NewParentNode.Nodes.Add(NewChildNode)
                    '****************************************************
                    NewParentNode = New TreeNode("VANTAA")
                    NewRootNode.Nodes.Add(NewParentNode)
                    NewChildNode = New TreeNode("V1 aamu")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("V1 ilta")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("V1 LA")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("V1 SU")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("V2K")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("V3 Työvuorot")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("V3 Ajokaavio arki")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("V3 Ajokaavio LA")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("V16")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("V17")
                    NewParentNode.Nodes.Add(NewChildNode)
                    '****************************************************
                    NewParentNode = New TreeNode("ESPOO")
                    NewRootNode.Nodes.Add(NewParentNode)
                    NewChildNode = New TreeNode("H15A")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("H15i")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("H152")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("E81")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("P40")
                    NewParentNode.Nodes.Add(NewChildNode)
                    '****************************************************
                    NewParentNode = New TreeNode("POHJOINEN")
                    NewRootNode.Nodes.Add(NewParentNode)
                    NewChildNode = New TreeNode("6041")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("6042 MA-TO")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("6042 PE")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("7011 MA-TO")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("7011 PE")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("7021 MA-TO")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("7021 PE")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("7031")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("7051")
                    NewParentNode.Nodes.Add(NewChildNode)
                    NewChildNode = New TreeNode("Ruokatunnit pohj.")
                    NewParentNode.Nodes.Add(NewChildNode)

                End If

            End If
        Catch ex As Exception
            Err.Clear()
            myYhteys.Close()
        End Try


    End Sub

    Public Function PalautaNimiTyovuoronAutonPalauttajelle(ByVal autoNro As Integer)
        Dim nimi As String = ""
        Dim HloNro As Integer = 0
        Dim TVlyh As String = ""
        Dim paluuTV As String = ""
        TbConnection2.Close()
        Dim dAl As Date = Format(Today, "\#yyyy\-MM\-dd\#")

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection2
            .CommandType = CommandType.Text
            .CommandText = "SELECT HloNro, TVLyhenne FROM AjetutVuorot " & _
                           "WHERE DATE(AlkuPVM) = @AlkuPVM AND AutoNro=@AutoNro"
            .Parameters.AddWithValue("@AlkuPVM", dAl)
            .Parameters.AddWithValue("@AutoNro", autoNro)

        End With
        TbConnection2.Open()
        Dim daZ As MySqlDataReader = cmd.ExecuteReader
        If daZ.HasRows = True Then ' päivästä löytyy työvuoro johon auto on kirjattu
            daZ.Read()
            HloNro = daZ.GetValue(0)
            TVlyh = daZ.GetString(1)
            daZ.Close()
            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = TbConnection2
                .CommandType = CommandType.Text
                .CommandText = "SELECT TVpalauttaa FROM PesuTyovuorossa WHERE TVnAuton=@TV"
                .Parameters.AddWithValue("@TV", TVlyh)
            End With
            Dim dax As MySqlDataReader = cmd2.ExecuteReader
            If dax.HasRows = True Then
                dax.Read()
                paluuTV = dax.GetString(0)

                dax.Close()
                Dim daq As New MySqlCommand()
                With daq
                    .Connection = TbConnection2
                    .CommandType = CommandType.Text
                    .CommandText = "SELECT AjetutVuorot.HloNro, Henkilosto.SukuNimi, Henkilosto.EtuNimi FROM AjetutVuorot " & _
                                   "INNER JOIN Henkilosto ON AjetutVuorot.HloNro=Henkilosto.HloNro " & _
                                   "WHERE AjetutVuorot.AlkuPVM = @AlkuPVM AND AjetutVuorot.TVLyhenne=@TV"
                    .Parameters.AddWithValue("@AlkuPVM", dAl)
                    .Parameters.AddWithValue("@TV", paluuTV)
                End With
                Dim daw As MySqlDataReader = daq.ExecuteReader
                If daw.HasRows = True Then
                    daw.Read()
                    nimi = daw.GetString(1) & " " & daw.GetString(2)
                    TbConnection2.Close()
                    Return nimi
                    Exit Function

                Else
                    nimi = ""
                    TbConnection2.Close()
                    Return nimi
                    Exit Function

                End If

            Else ' oletetaan, että sama työvuoro joka on aloittanut myös palauttaa auton
                nimi = PalautaSQLNrostaNimi(HloNro)
                TbConnection2.Close()

                Return nimi
                Exit Function

            End If
        Else ' päivästä ei löydy työvuoroa johon auto olisi kirjattu
            nimi = ""
            TbConnection2.Close()
            Return nimi
            Exit Function

        End If

        Return nimi


    End Function
    Public Function palautaTagistaAutoNro(ByVal tagi As Integer)
        Dim paluu As Integer = 0

        TbConnection2.Close()
        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection2
            .CommandType = CommandType.Text
            .CommandText = "SELECT Nro, AutoNro FROM Tagit WHERE Nro = @Nro"
            .Parameters.AddWithValue("@Nro", tagi)
        End With
        TbConnection2.Open()

        Dim rd As MySqlDataReader = cmd.ExecuteReader
        rd.Read()

        If rd.HasRows = True Then
            paluu = rd.GetValue(1)
            TbConnection2.Close()
            rd.Close()
            Return paluu
        Else
            TbConnection2.Close()
            rd.Close()
            Return -1

        End If



    End Function
    Public Sub EsilleSpalash(ByVal tv As String, ByVal kuli As String, ByVal rek As String)
        Me.Close()

        Main.SC.Visible = False

        '    OhjelmanValintaValikko.Close()

        KirjautumisenSplah.MdiParent = Main
        KirjautumisenSplah.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        KirjautumisenSplah.Dock = DockStyle.Fill

        KirjautumisenSplah.Show()

        '     KirjautumisenSplah.MdiParent = Main




        'KirjautumisenSplah.Show()
        'Main.LayoutMdi(MdiLayout.Cascade)
        'KirjautumisenSplah.StartPosition = FormStartPosition.CenterParent
        'KirjautumisenSplah.WindowState = FormWindowState.Maximized


        KirjautumisenSplah.TopMost = True
        Me.Enabled = False

        If rek <> "" Then
            KirjautumisenSplah.Label1.Text = "TYÖVUORON " & tv & " AUTO ON "
            KirjautumisenSplah.TVtekstilla.Text = rek
            KirjautumisenSplah.AUTOOOO.Text = rek
            KirjautumisenSplah.laitaPysakoinninKuvaEsille(PalautaSQLNumeroRekkarista(rek))
            KirjautumisenSplah.Timer1.Enabled = True

        End If
        If rek = "" Then KirjautumisenSplah.TVtekstilla.Text = "TYÖVUOROON " & tv & " EI OLE MÄÄRITETTY AUTOA" & vbCrLf & "OTA YHTEYS HALLIPÄIVYSTÄJÄÄN p. 050 521 6826"

        '***************************************
        '**** TARKISTETAAN TIEDOTTEET

        Dim KuliNro As Integer = PalautaSQLNroNimiesta(kuli)

        Try
            TiedoteYhteys.Close()
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TiedoteYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT COUNT(*) As Lasku FROM LuettavatTiedotteet WHERE HloNro=@Hlo AND Luettu='0'"
                .Parameters.AddWithValue("@Hlo", KuliNro)
            End With

            TiedoteYhteys.Open()
            Dim maara As Integer = cmd.ExecuteScalar
            TiedoteYhteys.Close()
            If maara > 0 Then
                KirjautumisenSplah.tiedotteitaKPL.Text = maara.ToString
                KirjautumisenSplah.tiedotteitaKPL.BackColor = Color.Red
                KirjautumisenSplah.txtInfoTiedote.BackColor = Color.Red
                KirjautumisenSplah.tiedotteitaKPL.Visible = True
                KirjautumisenSplah.txtInfoTiedote.Visible = True

            Else
                KirjautumisenSplah.tiedotteitaKPL.Visible = False
                KirjautumisenSplah.txtInfoTiedote.Visible = False


            End If

        Catch ex As Exception
            Err.Clear()
        End Try







        KirjautumisenSplah.TextBox1.Focus()


        Exit Sub


        '        Button1_Click(Nothing, Nothing)

    End Sub

    Public Sub tallennaTyoVuoroonIlmoittautuminen(ByVal TV As String)
        TbConnection2.Close()
        Dim dAl As Date = Format(Today, "\#yyyy\-MM\-dd\#")

        Dim cmd As New MySqlCommand()
        cmd.Connection = TbConnection2
        cmd.CommandType = CommandType.Text
        cmd.CommandText = "UPDATE AjetutVuorot SET AlkuKLO = @AlkuKLO, Ilmoittauduttu = @Ilmoittauduttu WHERE AlkuPVM = @AlkuPVM AND TVLyhenne = @TVid AND HloNro = @HloNro" ' AND HloNro = @VanhaKuski"
        cmd.Parameters.AddWithValue("@AlkuPVM", dal)

        '    cmd.Parameters.AddWithValue("@AlkuPVM", FormatDateTime(DAL2, DateFormat.ShortDate))

        cmd.Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(KayttajaNimi))
        cmd.Parameters.AddWithValue("@TVid", TV)
        cmd.Parameters.AddWithValue("@AlkuKLO", Now)
        cmd.Parameters.AddWithValue("@Ilmoittauduttu", True)

        '       cmd.Parameters.AddWithValue("@VanhaKuski", PalautaSQLNroNimiesta(VanhaKuski.Text))
        Try
            TbConnection2.Open()
            cmd.ExecuteNonQuery()
            TbConnection2.Close()

        Catch ex As Exception
            Err.Clear()

        End Try
        Dim bilikannro As String = ""

        For i As Integer = 0 To 149

            If LLISTA(i).KULI = KayttajaNimi Then
                If TarvitseekoTyoVuoroKirjautumisen(LLISTA(i).TV) = True Then
                    bilikannro = LLISTA(i).REK
                    LLISTA(i).ilm = True

                    Exit For
                End If
            End If

        Next


    End Sub

    Public Sub TallennaKirjautumisAika()
        Me.Cursor = Cursors.Default

        Try
            '     tbConnection.Close()

            TbConnection.Close()
            Dim cmdkir As New MySqlCommand()
            With cmdkir
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Kirjautumiset " & _
                    "(HloNro, Sisaan, Kone) " & _
                    "VALUES(@HloNro, @Sisaan, @Kone)"
                .Parameters.AddWithValue("@HloNro", KayttajaHloNro)
                .Parameters.AddWithValue("@Sisaan", CType(Now, Date))
                .Parameters.AddWithValue("@Kone", KoneenTunnisteMAC)

            End With

            TbConnection.Open()
            cmdkir.ExecuteNonQuery()
            cmdkir.CommandText = "SELECT @@IDENTITY AS TEMPVALUE"
            KirjautumisID = cmdkir.ExecuteScalar   'KÄYTETTÄVISSI* OLEVA kirjautumisID
            TbConnection.Close()

            TbConnection.Close()

        Catch ex As Exception
            Err.Clear()

        End Try



    End Sub

    Private Sub Kirjautuminen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LuettuKoodi.Cursor = Nothing


    End Sub

    Private Sub TableLayoutPanel3_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel3.Paint

    End Sub
End Class
