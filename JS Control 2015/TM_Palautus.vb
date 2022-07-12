Imports MySql.Data.MySqlClient



Public Class TM_Palautus
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)
    Private T As CheckBox()
    Private H As TextBox()
    Private JarrutE As CheckBox()
    Private JarrutT As CheckBox()
    Private TbConnection2 As MySqlConnection = New MySqlConnection(serverString)
    Public Sub HaeKuljettajatCBhen()
        Dim cmd As New MySqlCommand()
        cbTekija.Items.Clear()
        TbConnection.Close()

        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT SukuNimi, EtuNimi FROM Henkilosto WHERE KorjaamoTyo = 1"

        End With

        TbConnection.Open()

        Try
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then

                While rd.Read

                    cbTekija.Items.Add(rd.GetString(0) & " " & rd.GetString(1))

                End While


            End If
            rd.Close()
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TM_palautus", ErrorToString, 0, 0)
            Err.Clear()


            ' Throw ex
        End Try


        TbConnection.Close()

    End Sub

    Private Sub TMpalautus_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Panel1.Visible = True
        LataaAutot()
        T = New CheckBox() {t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18}
        H = New TextBox() {h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, h11, h12, h13, h14, h15, h16, h17, h18}
        '       Otsikko.SelectedIndex = 0
        JarrutE = New CheckBox() {VE_anturi, VE_levyt, VE_palat, oe_anturi, oe_levyt, oe_palat}
        JarrutT = New CheckBox() {vt_anturi, vt_levyt, vt_palat, ot_anturi, ot_levyt, ot_palat}
        HaeKuljettajatCBhen()

        cbTekija.Text = KayttajaNimi

        Otsikko.Text = ""
        Me.Cursor = Cursors.Default

    End Sub
    Public Sub lataaVikailmoituksetILMANTyoMaaraysta()
        TbConnection.Close()
        Viat.Items.Clear()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT riviID, IlmoitettuVika, IlmPVM FROM VikaIlmoitukset WHERE AutoNro = @AutoNro AND Korjattu = @Korjattu ORDER BY IlmPVM DESC"
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbRekNro.Text))
            .Parameters.AddWithValue("@Korjattu", False)

        End With
        TbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then
            While rd.Read
                Viat.Items.Add(FormatDateTime(rd.GetValue(2), DateFormat.ShortDate).ToString & vbTab & rd.GetString(1) & vbTab & vbTab & vbTab & rd.GetValue(0).ToString)
            End While
        End If
        TbConnection.Close()
        rd.Close()

    End Sub
    Private Sub TableLayoutPanel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub


    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Public Sub LataaAutot()
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
        TbConnection.Close()


    End Sub

    Private Sub etuKaikki_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles etuKaikki.CheckedChanged
        If etuKaikki.Checked = True Then
            VE_anturi.Checked = True
            VE_levyt.Checked = True
            VE_palat.Checked = True
            oe_anturi.Checked = True
            oe_levyt.Checked = True
            oe_palat.Checked = True
        End If
        If etuKaikki.Checked = False Then
            VE_anturi.Checked = False
            VE_levyt.Checked = False
            VE_palat.Checked = False
            oe_anturi.Checked = False
            oe_levyt.Checked = False
            oe_palat.Checked = False
        End If

    End Sub

    Private Sub takaKaikki_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles takaKaikki.CheckedChanged
        If takaKaikki.Checked = True Then
            vt_anturi.Checked = True
            vt_levyt.Checked = True
            vt_palat.Checked = True
            ot_anturi.Checked = True
            ot_levyt.Checked = True
            ot_palat.Checked = True
        End If
        If takaKaikki.Checked = False Then
            vt_anturi.Checked = False
            vt_levyt.Checked = False
            vt_palat.Checked = False
            ot_anturi.Checked = False
            ot_levyt.Checked = False
            ot_palat.Checked = False
        End If


    End Sub

    Private Sub TextBox1_Enter(sender As Object, e As System.EventArgs) Handles TextBox1.Enter
        '    SplitContainer1.SplitterDistance = 100

    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As System.EventArgs) Handles TextBox1.Leave
        '     SplitContainer1.SplitterDistance = 375

    End Sub

    Private Sub TextBox1_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox6_Enter(sender As Object, e As System.EventArgs) Handles TextBox6.Enter
        '   SplitContainer1.SplitterDistance = 100

    End Sub

    Private Sub TextBox6_Leave(sender As Object, e As System.EventArgs) Handles TextBox6.Leave
        '    SplitContainer1.SplitterDistance = 375

    End Sub



    Private Sub TextBox6_TextChanged(sender As System.Object, e As System.EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub m_ilmansuodatin_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles m_ilmansuodatin.CheckedChanged
        If m_ilmansuodatin.Checked = True Then M_ilmansuodatinHUOM.Focus()

    End Sub

    Private Sub m_oljynsuodatin_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles m_oljynsuodatin.CheckedChanged
        If m_oljynsuodatin.Checked = True Then m_olsynsuodatinHUOM.Focus()

    End Sub

    Private Sub M_oljyt_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles M_oljyt.CheckedChanged
        If M_oljyt.Checked = True Then M_oljyHUOM.Focus()

    End Sub

    Private Sub v_oljyt_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles v_oljyt.CheckedChanged
        If v_oljyt.Checked = True Then v_oljytHUOM.Focus()
    End Sub

    Private Sub p_oljyt_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles p_oljyt.CheckedChanged
        If p_oljyt.Checked = True Then p_oljytHUOM.Focus()

    End Sub

    Private Sub I_kylmaainettaLIS_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles I_kylmaainettaLIS.CheckedChanged
        If I_kylmaainettaLIS.Checked = True Then kylmaianeHUOM.Focus()


    End Sub
    Public Sub lataaHuoltoHistoria()
        Dim newRootNode As TreeNode
        Dim NewParentNode As TreeNode
        Dim NewChild As TreeNode
        ' hh poistettu TMpalautuksesta        HH.Nodes.Clear()
        If cbRekNro.Text = "" Then Exit Sub
        Dim rd As MySqlDataReader
        newRootNode = New TreeNode(cbRekNro.Text)
        ' hh poistettu TMpalautuksesta        HH.Nodes.Add(newRootNode)
        TbConnection.Close()
        Try

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT Tyomaaraykset.LuontiPVM, Tyomaaraykset.Otsikko, Tehtytyo.Tyo, TyohonKaytettyAika.HloNro, TyohonKaytettyAika.AikaMIN, " & _
                               "KMTiedot.KMlukema, Henkilosto.SukuNimi, Henkilosto.EtuNimi, VikaIlmoitukset.IlmoitettuVika " & _
                               "FROM Tyomaaraykset " & _
                               "INNER JOIN Tehtytyo ON Tyomaaraykset.TMnro = Tehtytyo.TMnro " & _
                             "INNER JOIN TyohonKaytettyAika ON Tyomaaraykset.TMnro = TyohonKaytettyAika.TMnro " & _
                             "INNER JOIN VikaIlmoitukset ON Tyomaaraykset.TMnro = VikaIlmoitukset.LiitettyTMnro " & _
                               "INNER JOIN Kalusto ON Tyomaaraykset.AutoNro = Kalusto.AutoNro " & _
                               "INNER JOIN Henkilosto ON TyohonKaytettyAika.HloNro = Henkilosto.HloNro " & _
                               "INNER JOIN KMTiedot ON Tyomaaraykset.TMnro = KMTiedot.TMnro " & _
                               "WHERE Tyomaaraykset.AutoNro = @AutoNro " & _
                               "ORDER BY Tyomaaraykset.LuontiPVM DESC"
                .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbRekNro.Text))

            End With

            '                        "INNER JOIN TMKaytetytOsat ON Tyomaaraykset.TMnro = TMkaytetytOsat.TMnro " & _
            'TMKaytetytOsat.Osa,
            TbConnection.Open()
            rd = cmd.ExecuteReader
            Dim US As hsiirto
            Dim vs As hsiirto = Nothing
            Dim asentajia(1) As String '() = Nothing
            Dim aikaakaytetty(1) As String '() = Nothing
            ''        Dim osia(1) As String ' = Nothing
            Dim toita(1) As String '= Nothing
            Dim vikoja(1) As String '() = Nothing
            If rd.HasRows = True Then


                While rd.Read
                    US.pvm = rd.GetValue(0)
                    US.otsikko = rd.GetString(1)
                    US.Tyo = rd.GetString(2)
                    ''  US.osat = rd.GetString(2)
                    US.hloNro = rd.GetValue(3)
                    US.MIN = rd.GetValue(4)
                    US.km = rd.GetValue(5)
                    US.asentaja = rd.GetString(6) & " " & rd.GetValue(7)
                    US.vika = rd.GetString(8)

                    '          MsgBox("PVM:" & vbTab & FormatDateTime(vanhaS.pvm, DateFormat.ShortDate).ToUpper & vbCrLf & _
                    '                    "Otsikko:" & vbTab & vanhaS.otsikko & vbCrLf & _
                    '                   "Tyo:" & vbTab & vanhaS.Tyo & vbCrLf & _
                    '                  "HloNro:" & vbTab & vanhaS.hloNro.ToString & vbCrLf & _
                    '                 "Asentaja" & vbTab & vanhaS.asentaja & vbCrLf & _
                    '                "MIN" & vbTab & vanhaS.MIN.ToString & vbCrLf & _
                    '               "Osat:" & vbTab & vanhaS.osat & vbCrLf & _
                    '              "KM: " & vbTab & vanhaS.km.ToString)
                    'luodaan pvm km ja otsikkorivi
                    If FormatDateTime(CType(US.pvm, Date), DateFormat.ShortDate) = FormatDateTime(CType(vs.pvm, Date), DateFormat.ShortDate) And US.otsikko = vs.otsikko Then
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


                            NewParentNode = New TreeNode(FormatDateTime(vs.pvm, DateFormat.ShortDate).ToString & " | " & vs.km.ToString & " km | " & vs.otsikko)
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
                                'End If
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





                    End If

                End While
            End If
            TbConnection.Close()
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE tm palautus xx", ErrorToString, 0, 0)
            Err.Clear()

        End Try


        ' hh poistettu TMpalautuksesta        Dim tn As TreeNode
        ' hh poistettu TMpalautuksesta         For Each tn In HH.Nodes
        ' hh poistettu TMpalautuksesta If tn.Text = cbRekNro.Text Then
        ' hh poistettu TMpalautuksesta HH.SelectedNode = tn
        ' hh poistettu TMpalautuksesta        HH.SelectedNode.Expand()

        ' hh poistettu TMpalautuksesta         Exit For
        ' hh poistettu TMpalautuksesta         End If
        ' hh poistettu TMpalautuksesta        Next

        '    HH.ExpandAll()

    End Sub

    Public Structure hsiirto
        Dim pvm As Date
        Dim Tyo As String
        '        Dim osat As String
        Dim hloNro As Integer
        Dim MIN As String
        Dim asentaja As String
        Dim otsikko As String
        Dim km As Integer
        Dim vika As String

    End Structure

    Private Sub cbRekNro_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbRekNro.SelectedIndexChanged
        If cbRekNro.Text = "" Then Exit Sub
        '     lataaHuoltoHistoria()
        If txtTMnro.Text = "" Then lataaVikailmoituksetILMANTyoMaaraysta()

        If txtTMnro.Text <> "" Then

            lataaTMLiitetytVikailmoitukset()


        End If

        lataakirjatutHuollot()
    End Sub
    Public Sub HaeTMtiedot()
        '     Try
        TbConnection.Close()
        Dim tm As String = txtTMnro.Text

        Dim cmd As New MySqlCommand
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT AutoNro, Valmis FROM Tyomaaraykset WHERE TMnro=@TMnro"
            .Parameters.AddWithValue("@TMnro", tm)

        End With

        TbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = False Then
            Main.TopMost = False : MsgBox("Tuntematon työmääräys")
            Main.TopMost = True : Me.Close()
            Exit Sub


        End If
        rd.Read()
        If rd.GetValue(1) = True Then
            Main.TopMost = False : MsgBox("Työmääräys on jo palautettu")
            Main.TopMost = True : Me.Close()
            Exit Sub

        End If
        Dim bilikkanro As Integer = rd.GetValue(0)

        Button8.Visible = False


        cbRekNro.Text = PalautaSQLRekkariNumerosta(bilikkanro)
        TbConnection.Close()

        Dim cmd2 As New MySqlCommand
        With cmd2
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT Otsikko FROM Tyomaaraykset WHERE TMnro=@TMnro"
            .Parameters.AddWithValue("@TMnro", tm)

        End With

        TbConnection.Open()
        TyonKuvaus.Text = cmd2.ExecuteScalar
    
        TbConnection.Close()

        Me.Cursor = Cursors.Default

        'Catch ex As Exception
        'Err.Clear()

        '        End Try
    End Sub
    Public Sub LataaTMLiitetytVikailmoitukset()
        TbConnection.Close()
        Viat.Items.Clear()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT riviID, IlmoitettuVika, IlmPVM FROM VikaIlmoitukset WHERE LiitettyTMnro = @TMNro ORDER BY IlmPVM DESC"
            .Parameters.AddWithValue("@TMNro", (txtTMnro.Text))
      
        End With
        TbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then
            While rd.Read
                Viat.Items.Add(FormatDateTime(rd.GetValue(2), DateFormat.ShortDate).ToString & vbTab & rd.GetString(1) & vbTab & vbTab & vbTab & rd.GetValue(0).ToString)
            End While
        End If
        TbConnection.Close()
        rd.Close()
    End Sub
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
    Private Sub HH_AfterSelect(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs)

    End Sub

    Private Sub HH_MouseEnter(sender As Object, e As System.EventArgs)
        '      SplitContainer1.SplitterDistance = 100

    End Sub

    Private Sub HH_MouseLeave(sender As Object, e As System.EventArgs)
        '     SplitContainer1.SplitterDistance = 518

    End Sub



    Private Sub MennytAikaa_Leave(sender As Object, e As System.EventArgs) Handles MennytAikaa.Leave
        MennytAikaa.Text = ErotteleAika(MennytAikaa.Text)

        If TyossaMukana.Items.Count = 1 Then
            Dim itemi As itemTekijat = TyossaMukana.Items.Item(0)



            '   TyossaMukana.Tag = UsedTime.Text
            '   Dim nimi As String = TyossaMukana.Text

            TyossaMukana.Items.Remove(itemi)
            itemi.Aika = MennytAikaa.Text
            itemi.NimijaAika = itemi.Nimi & " (" & itemi.Aika & ")"


            TyossaMukana.Items.Add(itemi)

        End If
        If TyossaMukana.Items.Count = 0 Then

            Dim itemi As New itemTekijat
            itemi.Nimi = KayttajaNimi
            itemi.Aika = MennytAikaa.Text
            itemi.NimijaAika = itemi.Nimi & " (" & itemi.Aika & ")"


            TyossaMukana.Items.Add(itemi)

        End If

    End Sub

    Private Sub MennytAikaa_TextChanged(sender As System.Object, e As System.EventArgs) Handles MennytAikaa.TextChanged

    End Sub
    Public Sub TCheckked(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles t1.CheckedChanged, t2.CheckedChanged, t3.CheckedChanged, t4.CheckedChanged, t5.CheckedChanged, t6.CheckedChanged, t7.CheckedChanged, t8.CheckedChanged, t9.CheckedChanged, t10.CheckedChanged, t11.CheckedChanged, t12.CheckedChanged, t13.CheckedChanged, t14.CheckedChanged, t15.CheckedChanged, t16.CheckedChanged, t17.CheckedChanged, t18.CheckedChanged
        Dim clickedLabel As CheckBox

        clickedLabel = CType(sender, CheckBox)

        If clickedLabel.Checked = False Then Exit Sub 'clickedLabel.ForeColor = Color.Red


        'MsgBox("Label #" & clickedLabel.Name)
        Dim stra As String = clickedLabel.Name
        stra = stra.Replace("t", "")

        Dim a As Integer = Val(stra)
        a = a - 1

        H(a).Focus()



    End Sub
    Private Sub t1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles t1.CheckedChanged

    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub Button5_Click_1(sender As System.Object, e As System.EventArgs) Handles Button5.Click

        If Viat.Items.Count = 0 And LisattavaOmaVika.Text = "" Then
            Main.TopMost = False : MsgBox("KORJATTAVAA VIKAA EI OLE MÄÄRITETTY")
            LisattavaOmaVika.BackColor = Color.Red
            LisattavaOmaVika.Focus()
            Viat.BackColor = Color.Red
            Main.TopMost = True
            Exit Sub

        End If
        Me.Cursor = Cursors.WaitCursor

        LisattavaOmaVika.BackColor = Color.White
        Viat.BackColor = Color.White
        Main.TopMost = False
        If kmLukema.Text = "" Then MsgBox("Kilometrilukema puuttuu") : kmLukema.BackColor = Color.Red : kmLukema.Focus() : Main.TopMost = True : Exit Sub
        kmLukema.BackColor = Color.White

        If kmLukema.Text = "?" Then kmLukema.Text = "0"
        If MennytAikaa.Text = "" Then MsgBox("Käytettyaika puuttuu") : MennytAikaa.BackColor = Color.Red : MennytAikaa.Focus() : Main.TopMost = True : Exit Sub
        MennytAikaa.BackColor = Color.White

        If cbRekNro.Text = "" Then MsgBox("Autoa ei ole valittu") : cbRekNro.Focus() : Main.TopMost = True : Exit Sub
        cbRekNro.BackColor = Color.White

        If Otsikko.Text = "" Then MsgBox("Otsikko puuttuu") : Otsikko.BackColor = Color.Red : Otsikko.Focus() : Main.TopMost = True : Exit Sub
        '  KayttajaHloNro = 314
        Otsikko.BackColor = Color.White
        Otsikko.Text = Otsikko.Text.ToUpper
        Main.TopMost = True
        Dim nroTM As Integer = 0

        TbConnection.Close()
        Dim cmd As New MySqlCommand()

        If txtTMnro.Text = "" Then
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Tyomaaraykset " & _
                               "(Tulostettu, LuontiPVM, Otsikko, LuontiHlo, Valmis, AutoNro, LisattyPVM, LisaajaHloNnro, AikaaMIN) " & _
                               "VALUES(@Tulostettu, @LuontiPVM, @Otsikko, @LuontiHlo, @Valmis, @AutoNro, @LisattyPVM, @LisaajaHloNnro, @AikaaMIN)"
                .Parameters.AddWithValue("@Tulostettu", False)
                .Parameters.AddWithValue("@LuontiPVM", CType(dtpPVM.Value, Date))
                .Parameters.AddWithValue("@Otsikko", Otsikko.Text.ToUpper)
                .Parameters.AddWithValue("@LuontiHlo", KayttajaHloNro)
                .Parameters.AddWithValue("@Valmis", True)
                .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbRekNro.Text))
                .Parameters.AddWithValue("@LisattyPVM", CType(Now, Date))
                .Parameters.AddWithValue("@LisaajaHloNnro", KayttajaHloNro)
                .Parameters.AddWithValue("@AikaaMIN", muutaTunnitMinsoiksi(MennytAikaa.Text))
            End With

            TbConnection.Open()
            cmd.ExecuteNonQuery()
            cmd.CommandText = "SELECT @@IDENTITY AS TEMPVALUE"
            nroTM = cmd.ExecuteScalar   'KÄYTETTÄVISSI* OLEVA TM NRO
            TbConnection.Close()



        Else
            ' TALLENNETAAN TEHDYN TYÖMÄÄRÄYKSEN PALAUTUS

            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "UPDATE Tyomaaraykset " & _
                               "SET Otsikko=@Otsikko, TyonKuvaus=@TyonKuvaus, Valmis=@Valmis, AikaaMIN=@AikaaMIN, LisattyPVM=@LisattyPVM, LisaajaHloNnro=@LisaajaHloNnro " & _
                               "WHERE TMnro=@TMNro"
                .Parameters.AddWithValue("@Otsikko", Otsikko.Text.ToUpper)
                .Parameters.AddWithValue("@TyonKuvaus", TyonKuvaus.Text)
                .Parameters.AddWithValue("@Valmis", True)
                .Parameters.AddWithValue("@LisattyPVM", CType(dtpPVM.Value, Date))
                .Parameters.AddWithValue("@LisaajaHloNnro", KayttajaHloNro)
                .Parameters.AddWithValue("@AikaaMIN", muutaTunnitMinsoiksi(MennytAikaa.Text))
                .Parameters.AddWithValue("@TMNro", txtTMnro.Text)
            End With
            TbConnection.Open()
            cmd.ExecuteNonQuery()
            TbConnection.Close()

            nroTM = Val(txtTMnro.Text)




            ' ******************************************************************************************************************************************************************

        End If
        'LISÄTÄÄN KÄYTETTYT OSAT
        '      If omatOsat.Text <> "" Then
        Dim TMosat As New MySqlCommand()
        With TMosat
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO TMkaytetytOsat " & _
                         "(TMnro, Osa, vanhaSiirto, LisasattyPVM, lisaajaHloNro) " & _
                         "VALUES(@TMnro, @Osa, @vanhaSiirto, @LisasattyPVM, @lisaajaHloNro)"
            .Parameters.AddWithValue("@TMnro", nroTM)
            .Parameters.AddWithValue("@Osa", omatOsat.Text.ToUpper)
            .Parameters.AddWithValue("@vanhaSiirto", False)
            .Parameters.AddWithValue("@LisasattyPVM", CType(Now, Date))
            .Parameters.AddWithValue("@lisaajaHloNro", KayttajaHloNro)

        End With

        Try
            TbConnection.Open()
            TMosat.ExecuteNonQuery()
            TbConnection.Close()

        Catch ex As Exception
            Err.Clear()

        End Try

        '     End If




        Dim itemi As itemTekijat
        For Each itemi In TyossaMukana.Items
            TbConnection.Close()
            'LISÄTÄÄN KAYTETTYAIKA TAULUKKOON
            Dim TMomatyoA As New MySqlCommand()
            With TMomatyoA
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO TyohonKaytettyAika " & _
                             "(HloNro, VanhaMerkinta, AikaMIN, TMnro, LisasettyPVM, LisaajaHloNro) " & _
                             "VALUES(@HloNro, @VanhaMerkinta, @AikaMIN, @TMnro, @LisasettyPVM, @LisaajaHloNro)"
                .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(itemi.Nimi))
                .Parameters.AddWithValue("@VanhaMerkinta", False)
                .Parameters.AddWithValue("@AikaMIN", muutaTunnitMinsoiksi(itemi.Aika))
                .Parameters.AddWithValue("@TMnro", nroTM)
                .Parameters.AddWithValue("@LisasettyPVM", CType(Now, Date))
                .Parameters.AddWithValue("@LisaajaHloNro", KayttajaHloNro)

            End With
            Try
                TbConnection.Open()
                TMomatyoA.ExecuteNonQuery()
                TbConnection.Close()

            Catch ex As Exception
                Err.Clear()

            End Try

        Next








        '      If omaTyo.Text <> "" Then'
        Dim TMomatyo As New MySqlCommand()
        With TMomatyo
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO Tehtytyo " & _
                         "(TMnro, Tyo, LisattyPVM, LisaajaHloNro) " & _
                         "VALUES(@TMnro, @Tyo, @LisattyPVM, @LisaajaHloNro)"
            .Parameters.AddWithValue("@TMnro", nroTM)
            .Parameters.AddWithValue("@Tyo", omaTyo.Text.ToUpper)
            .Parameters.AddWithValue("@LisattyPVM", CType(Now, Date))
            .Parameters.AddWithValue("@LisaajaHloNro", KayttajaHloNro)

        End With
        Try
            TbConnection.Open()
            TMomatyo.ExecuteNonQuery()
            TbConnection.Close()

        Catch ex As Exception
            Err.Clear()

        End Try

        'LISÄTÄÄN JARRU TYÖT
        Dim ETUjarrut As String = ""
        Dim TAKAjarrut As String = ""
        '    JarrutE = New CheckBox() {VE_anturi, VE_levyt, VE_palat, oe_anturi, oe_levyt, oe_palat}
        '   JarrutT = New CheckBox() {vt_anturi, vt_levyt, vt_palat, ot_anturi, ot_levyt, ot_palat}

        ' VAIHDETTU EDESTÄ PALAT (V/O) LEVYT (V/0) ANTURIT (V/O)
        ' VAIHDETTU TAKAA PALAT (V/O) LEVYT (V/0) ANTURIT (V/O)
        '      alku        palat (puolet) levyt (puolet) anturit (puolet)
        Dim etesta As Boolean = False

        For i As Integer = 0 To 5
            If JarrutE(i).Checked = True Then etesta = True
        Next

        If etesta = True Then
            ETUjarrut = "VAIHDETTU EDESTÄ "

            If VE_palat.Checked = True And oe_palat.Checked = True Then ETUjarrut &= "JARRUPALAT (V/O) "
            If VE_palat.Checked = True And oe_palat.Checked = False Then ETUjarrut &= "JARRUPALAT (VASEN) "
            If VE_palat.Checked = False And oe_palat.Checked = True Then ETUjarrut &= "JARRUPALAT (OIKEA) "

            If VE_levyt.Checked = True And oe_levyt.Checked = True Then ETUjarrut &= "JARRULEVYT (V/O) "
            If VE_levyt.Checked = True And oe_levyt.Checked = False Then ETUjarrut &= "JARRULEVY (VASEN) "
            If VE_levyt.Checked = False And oe_levyt.Checked = True Then ETUjarrut &= "JARRULEVY (OIKEA) "

            If VE_anturi.Checked = True And oe_anturi.Checked = True Then ETUjarrut &= "JARRUANTURIT (V/O) "
            If VE_anturi.Checked = True And oe_anturi.Checked = False Then ETUjarrut &= "JARRUANTURI (VASEN) "
            If VE_anturi.Checked = False And oe_anturi.Checked = True Then ETUjarrut &= "JARRUANTURI (OIKEA) "


        End If
        Dim takkaa As Boolean = False

        For i As Integer = 0 To 5
            If JarrutT(i).Checked = True Then takkaa = True
        Next

        If takkaa = True Then
            TAKAjarrut = "VAIHDETTU TAKAA "

            If vt_palat.Checked = True And ot_palat.Checked = True Then TAKAjarrut &= "JARRUPALAT (V/O) "
            If vt_palat.Checked = True And ot_palat.Checked = False Then TAKAjarrut &= "JARRUPALAT (VASEN) "
            If vt_palat.Checked = False And ot_palat.Checked = True Then TAKAjarrut &= "JARRUPALAT (OIKEA) "

            If vt_levyt.Checked = True And ot_levyt.Checked = True Then TAKAjarrut &= "JARRULEVYT (V/O) "
            If vt_levyt.Checked = True And ot_levyt.Checked = False Then TAKAjarrut &= "JARRULEVY (VASEN) "
            If vt_levyt.Checked = False And ot_levyt.Checked = True Then TAKAjarrut &= "JARRULEVY (OIKEA) "

            If vt_anturi.Checked = True And ot_anturi.Checked = True Then TAKAjarrut &= "JARRUANTURIT (V/O) "
            If vt_anturi.Checked = True And ot_anturi.Checked = False Then TAKAjarrut &= "JARRUANTURI (VASEN) "
            If vt_anturi.Checked = False And ot_anturi.Checked = True Then TAKAjarrut &= "JARRUANTURI (OIKEA) "


        End If

        '   If JarrutE(0).Checked=True or JarrutE(1).Checked=True or JarrutE(2).Checked=True and 

        If ETUjarrut <> "" Then
            TbConnection.Close()

            Dim TMomatyo2j As New MySqlCommand()
            With TMomatyo2j
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Tehtytyo " & _
                             "(TMnro, Tyo, LisattyPVM, LisaajaHloNro) " & _
                             "VALUES(@TMnro, @Tyo, @LisattyPVM, @LisaajaHloNro)"
                .Parameters.AddWithValue("@TMnro", nroTM)
                .Parameters.AddWithValue("@Tyo", ETUjarrut)
                .Parameters.AddWithValue("@LisattyPVM", CType(Now, Date))
                .Parameters.AddWithValue("@LisaajaHloNro", KayttajaHloNro)

            End With

            TbConnection.Open()
            TMomatyo2j.ExecuteNonQuery()
            TbConnection.Close()

        End If
        If TAKAjarrut <> "" Then
            TbConnection.Close()

            Dim TMomatyo2j2 As New MySqlCommand()
            With TMomatyo2j2
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Tehtytyo " & _
                             "(TMnro, Tyo, LisattyPVM, LisaajaHloNro) " & _
                             "VALUES(@TMnro, @Tyo, @LisattyPVM, @LisaajaHloNro)"
                .Parameters.AddWithValue("@TMnro", nroTM)
                .Parameters.AddWithValue("@Tyo", TAKAjarrut)
                .Parameters.AddWithValue("@LisattyPVM", CType(Now, Date))
                .Parameters.AddWithValue("@LisaajaHloNro", KayttajaHloNro)

            End With

            TbConnection.Open()
            TMomatyo2j2.ExecuteNonQuery()
            TbConnection.Close()

        End If

        '       End If
        ' LISÄTÄÄN TARKASTUKSET
        Dim lisa(1) As String
        TbConnection.Close()
        For i As Integer = 0 To 18
            Try
                If T(i).Checked = True Then
                    ReDim Preserve lisa(0 To UBound(lisa) + 1)
                    If H(i).Text = "" Then lisa(UBound(lisa)) = T(i).Text.ToUpper Else lisa(UBound(lisa)) = T(i).Text.ToUpper & " (" & H(i).Text.ToLower & ")" & vbCrLf
                End If

            Catch ex As Exception
                Err.Clear()

            End Try
        Next
        Dim lisays As String = Join(lisa, ", ")

        lisays = lisays.Replace(", ,", "")

        If Len(lisays) > 5 Then
            lisays = "TARKASTATTU: " & lisays
            Dim TMomatyo2 As New MySqlCommand()
            With TMomatyo2
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Tehtytyo " & _
                             "(TMnro, Tyo, LisattyPVM, LisaajaHloNro) " & _
                             "VALUES(@TMnro, @Tyo, @LisattyPVM, @LisaajaHloNro)"
                .Parameters.AddWithValue("@TMnro", nroTM)
                .Parameters.AddWithValue("@Tyo", lisays)
                .Parameters.AddWithValue("@LisattyPVM", CType(Now, Date))
                .Parameters.AddWithValue("@LisaajaHloNro", KayttajaHloNro)

            End With

            TbConnection.Open()
            TMomatyo2.ExecuteNonQuery()
            TbConnection.Close()

        End If
        'LISÄTÄÄN KMTIETO
        TbConnection.Close()

        Dim km As New MySqlCommand()
        With km
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO KMTiedot " & _
                         "(AutoNro, PVM, HloNro, Tankkauksesta, TMnro, KMlukema, LisattyPVM, LisaajaHloNro) " & _
                         "VALUES(@AutoNro, @PVM, @HloNro, @Tankkauksesta, @TMnro, @KMlukema, @LisattyPVM, @LisaajaHloNro)"
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbRekNro.Text))
            .Parameters.AddWithValue("@PVM", CType(dtpPVM.Value, Date))
            .Parameters.AddWithValue("@HloNro", KayttajaHloNro)
            .Parameters.AddWithValue("@Tankkauksesta", False)
            .Parameters.AddWithValue("@TMnro", nroTM)
            .Parameters.AddWithValue("@KMlukema", kmLukema.Text)
            .Parameters.AddWithValue("@LisattyPVM", CType(Now, Date))
            .Parameters.AddWithValue("@LisaajaHloNro", KayttajaHloNro)

        End With

        TbConnection.Open()
        km.ExecuteNonQuery()
        TbConnection.Close()



        'LISÄTÄÄN HUOLTOSEURANTA MERKINNÄT + TEHTYINÄ TÖINÄ
        If M_oljyt.Checked = True Or v_oljyt.Checked = True Or p_oljyt.Checked = True Or I_kylmaainettaLIS.Checked = True Or naftasuodatin.Checked = True Or raitisilmasuodatin.Checked = True Then

            TbConnection.Close()
            Dim huolto As New MySqlCommand()
            With huolto
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO HuoltojenSeuranta " & _
                    "(PVM, RekNro, KMlukema, HloNro, MoottoriOljyt, VaihteistoOljyt, PeraOljyt, IlmastointiHuolto, NaftaSuodatinVaihdettu, RaitisIlmasuodatin, lisattyPVM, LisaajaHloNro) " & _
                "VALUES(@PVM, @RekNro, @KMlukema, @HloNro, @MoottoriOljyt, @VaihteistoOljyt, @PeraOljyt, @IlmastointiHuolto, @NaftaSuodatinVaihdettu, @RaitisIlmasuodatin, @lisattyPVM, @LisaajaHloNro)"


                .Parameters.AddWithValue("@PVM", CType(dtpPVM.Value, Date))
                .Parameters.AddWithValue("@RekNro", PalautaSQLNumeroRekkarista(cbRekNro.Text))
                .Parameters.AddWithValue("@KMlukema", kmLukema.Text)
                .Parameters.AddWithValue("@HloNro", KayttajaHloNro)
                If M_oljyt.Checked = True Then .Parameters.AddWithValue("@MoottoriOljyt", True) Else .Parameters.AddWithValue("@MoottoriOljyt", False)
                If v_oljyt.Checked = True Then .Parameters.AddWithValue("@VaihteistoOljyt", True) Else .Parameters.AddWithValue("@VaihteistoOljyt", False)
                If p_oljyt.Checked = True Then .Parameters.AddWithValue("@PeraOljyt", True) Else .Parameters.AddWithValue("@PeraOljyt", False)
                If I_kylmaainettaLIS.Checked = True Then .Parameters.AddWithValue("@IlmastointiHuolto", True) Else .Parameters.AddWithValue("@IlmastointiHuolto", False)
                If naftasuodatin.Checked = True Then .Parameters.AddWithValue("@NaftaSuodatinVaihdettu", True) Else .Parameters.AddWithValue("@NaftaSuodatinVaihdettu", False)
                If raitisilmasuodatin.Checked = True Then .Parameters.AddWithValue("@RaitisIlmasuodatin", True) Else .Parameters.AddWithValue("@RaitisIlmasuodatin", False)
                .Parameters.AddWithValue("@lisattyPVM", CType(Now, Date))
                .Parameters.AddWithValue("@LisaajaHloNro", KayttajaHloNro)
            End With

            TbConnection.Open()
            huolto.ExecuteNonQuery()
            TbConnection.Close()




            'tallennetaantehdyt huollot tehtyihin töihin
            Dim huoltolisays(1) As String

            If M_oljyt.Checked = True Then
                ReDim Preserve huoltolisays(0 To UBound(huoltolisays) + 1)
                If M_oljyHUOM.Text = "" Then huoltolisays(UBound(huoltolisays)) = "VAIHDETTU MOOTTORIÖLJYT" Else huoltolisays(UBound(huoltolisays)) = "VAIHDETTU MOOTTORIÖLJYT" & " (" & M_oljyHUOM.Text.ToLower & ")"
            End If
            If m_ilmansuodatin.Checked = True Then
                ReDim Preserve huoltolisays(0 To UBound(huoltolisays) + 1)
                If M_ilmansuodatinHUOM.Text = "" Then huoltolisays(UBound(huoltolisays)) = "VAIHDETTU ILMANSUODATIN" Else huoltolisays(UBound(huoltolisays)) = "VAIHDETTU ILMANSUODATIN" & " (" & M_ilmansuodatinHUOM.Text.ToLower & ")"
            End If
            If m_oljynsuodatin.Checked = True Then
                ReDim Preserve huoltolisays(0 To UBound(huoltolisays) + 1)
                If m_olsynsuodatinHUOM.Text = "" Then huoltolisays(UBound(huoltolisays)) = "VAIHDETTU ÖLJYNSUODATIN" Else huoltolisays(UBound(huoltolisays)) = "VAIHDETTU ÖLJYNSUODATIN" & " (" & m_olsynsuodatinHUOM.Text.ToLower & ")"
            End If
            If v_oljyt.Checked = True Then
                ReDim Preserve huoltolisays(0 To UBound(huoltolisays) + 1)
                If v_oljytHUOM.Text = "" Then huoltolisays(UBound(huoltolisays)) = "VAIHDETTU VAIHTEISTOÖLJYT" Else huoltolisays(UBound(huoltolisays)) = "VAIHDETTU VAIHTEISTOÖLJYT" & " (" & v_oljytHUOM.Text.ToLower & ")"
            End If
            If p_oljyt.Checked = True Then
                ReDim Preserve huoltolisays(0 To UBound(huoltolisays) + 1)
                If p_oljytHUOM.Text = "" Then huoltolisays(UBound(huoltolisays)) = "VAIHDETTU PERÄÖLJYT" Else huoltolisays(UBound(huoltolisays)) = "VAIHDETTU PERÄÖLJYT" & " (" & p_oljytHUOM.Text.ToLower & ")"
            End If

            If I_kylmaainettaLIS.Checked = True Then
                ReDim Preserve huoltolisays(0 To UBound(huoltolisays) + 1)
                If kylmaianeHUOM.Text = "" Then huoltolisays(UBound(huoltolisays)) = "LISÄTTY KYLMÄAINETTA" Else huoltolisays(UBound(huoltolisays)) = "LISÄTTY KYLMÄAINETTA" & " (" & kylmaianeHUOM.Text.ToLower & ")"
            End If

            If naftasuodatin.Checked = True Then
                ReDim Preserve huoltolisays(0 To UBound(huoltolisays) + 1)
                If naftahuom.Text = "" Then huoltolisays(UBound(huoltolisays)) = "VAIHDETTU NAFTASUODATIN" Else huoltolisays(UBound(huoltolisays)) = "VAIHDETTU NAFTASUODATIN" & " (" & naftahuom.Text.ToLower & ")"
            End If

            If raitisilmasuodatin.Checked = True Then
                ReDim Preserve huoltolisays(0 To UBound(huoltolisays) + 1)
                If raitisilmahuom.Text = "" Then huoltolisays(UBound(huoltolisays)) = "VAIHDETTU RAITISILMASUODATIN" Else huoltolisays(UBound(huoltolisays)) = "VAIHDETTU RAITISILMASUODATIN" & " (" & raitisilmahuom.Text.ToLower & ")"
            End If
            If TextBox6.Text <> "" Then
                ReDim Preserve huoltolisays(0 To UBound(huoltolisays) + 1)
                huoltolisays(UBound(huoltolisays)) = TextBox6.Text

            End If

            For k As Integer = 0 To huoltolisays.Count - 1
                If huoltolisays(k) <> Nothing Then
                    TbConnection.Close()
                    Dim TMomatyo2 As New MySqlCommand()
                    With TMomatyo2
                        .Connection = TbConnection
                        .CommandType = CommandType.Text
                        .CommandText = "INSERT INTO Tehtytyo " & _
                                     "(TMnro, Tyo, LisattyPVM, LisaajaHloNro) " & _
                                     "VALUES(@TMnro, @Tyo, @LisattyPVM, @LisaajaHloNro)"
                        .Parameters.AddWithValue("@TMnro", nroTM)
                        .Parameters.AddWithValue("@Tyo", huoltolisays(k))
                        .Parameters.AddWithValue("@LisattyPVM", CType(Now, Date))
                        .Parameters.AddWithValue("@LisaajaHloNro", KayttajaHloNro)

                    End With

                    TbConnection.Open()
                    TMomatyo2.ExecuteNonQuery()
                    TbConnection.Close()

                End If

            Next k






        End If


        ' jos on ilmoitettuja vikoja merkitään ne ilmoitetuiksi
        ' JOS EI OLE TEHDÄään merkintä "MÄÄRITTÄMÄTÖN VIKA

        For i = 0 To Viat.Items.Count - 1
            TbConnection.Close()
            Try
                Dim kasittely As String = Viat.Items(i).ToString
                Dim VIKA As String() = kasittely.Split(vbTab)

                Dim haeVika As New MySqlCommand()
                With haeVika
                    .Connection = TbConnection
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE VikaIlmoitukset " & _
                        "SET LiitettyTMnro = @TMNro, Korjattu = @Korjattu " & _
                        "WHERE riviID = @riviID"
                    .Parameters.AddWithValue("@Korjattu", 1)
                    .Parameters.AddWithValue("@riviID", VIKA(4))
                    .Parameters.AddWithValue("@TMnro", nroTM)
                End With
                TbConnection.Open()
                haeVika.ExecuteNonQuery()

            Catch ex As Exception
                Err.Clear()

            End Try
            TbConnection.Close()



        Next

        If LisattavaOmaVika.Text = "7897" Then 'ESTETTY TOIMINTA 7897
            Try
                TbConnection.Close()
                Dim haeVika2 As New MySqlCommand()
                With haeVika2
                    .Connection = TbConnection
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO VikaIlmoitukset " & _
                        "(IlmoitettuVika, AutoNro, HloNro, IlmPVM, Korjattu, LiitettyTMnro) " & _
                        "VALUES(@IlmoitettuVika, @AutoNro, @HloNro, @IlmPVM, @Korjattu, @LiitettyTMnro)"
                    .Parameters.AddWithValue("@Korjattu", 1)
                    .Parameters.AddWithValue("@IlmoitettuVika", LisattavaOmaVika.Text)
                    .Parameters.AddWithValue("@LiitettyTMnro", nroTM)

                    .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbRekNro.Text))
                    .Parameters.AddWithValue("@HloNro", KayttajaHloNro)
                    .Parameters.AddWithValue("@IlmPVM", CType(dtpPVM.Value, Date))


                End With
                TbConnection.Open()
                haeVika2.ExecuteNonQuery()

            Catch ex As Exception
                Err.Clear()

            End Try
            TbConnection.Close()



        End If


        lataaHuoltoHistoria()
        lataakirjatutHuollot()
        Me.Cursor = Cursors.Default
        Main.TopMost = False
        MsgBox("TALLENNETTU")
        Main.TopMost = True : Me.Close()
        Dim itemtext As String = "Työmääräykset"
        For Each node As TreeNode In Main.tview.Nodes
            If node.Text = itemtext Then
                Main.tview.SelectedNode = node
                Exit For
            End If
        Next
    End Sub



    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        Dim ETUjarrut As String = ""
        Dim TAKAjarrut As String = ""
        '    JarrutE = New CheckBox() {VE_anturi, VE_levyt, VE_palat, oe_anturi, oe_levyt, oe_palat}
        '   JarrutT = New CheckBox() {vt_anturi, vt_levyt, vt_palat, ot_anturi, ot_levyt, ot_palat}

        ' VAIHDETTU EDESTÄ PALAT (V/O) LEVYT (V/0) ANTURIT (V/O)
        ' VAIHDETTU TAKAA PALAT (V/O) LEVYT (V/0) ANTURIT (V/O)
        '      alku        palat (puolet) levyt (puolet) anturit (puolet)
        Dim etesta As Boolean = False

        For i As Integer = 0 To 5
            If JarrutE(i).Checked = True Then etesta = True
        Next

        If etesta = True Then
            ETUjarrut = "VAIHDETTU EDESTÄ "

            If VE_palat.Checked = True And oe_palat.Checked = True Then ETUjarrut &= "PALAT (V/O) "
            If VE_palat.Checked = True And oe_palat.Checked = False Then ETUjarrut &= "PALAT (VASEN) "
            If VE_palat.Checked = False And oe_palat.Checked = True Then ETUjarrut &= "PALAT (OIKEA) "

            If VE_levyt.Checked = True And oe_levyt.Checked = True Then ETUjarrut &= "LEVYT (V/O) "
            If VE_levyt.Checked = True And oe_levyt.Checked = False Then ETUjarrut &= "LEVY (VASEN) "
            If VE_levyt.Checked = False And oe_levyt.Checked = True Then ETUjarrut &= "LEVY (OIKEA) "

            If VE_anturi.Checked = True And oe_anturi.Checked = True Then ETUjarrut &= "ANTURIT (V/O) "
            If VE_anturi.Checked = True And oe_anturi.Checked = False Then ETUjarrut &= "ANTURI (VASEN) "
            If VE_anturi.Checked = False And oe_anturi.Checked = True Then ETUjarrut &= "ANTURI (OIKEA) "


        End If
        Dim takkaa As Boolean = False

        For i As Integer = 0 To 5
            If JarrutT(i).Checked = True Then takkaa = True
        Next

        If takkaa = True Then
            TAKAjarrut = "VAIHDETTU TAKAA "

            If vt_palat.Checked = True And ot_palat.Checked = True Then TAKAjarrut &= "PALAT (V/O) "
            If vt_palat.Checked = True And ot_palat.Checked = False Then TAKAjarrut &= "PALAT (VASEN) "
            If vt_palat.Checked = False And ot_palat.Checked = True Then TAKAjarrut &= "PALAT (OIKEA) "

            If vt_levyt.Checked = True And ot_levyt.Checked = True Then TAKAjarrut &= "LEVYT (V/O) "
            If vt_levyt.Checked = True And ot_levyt.Checked = False Then TAKAjarrut &= "LEVY (VASEN) "
            If vt_levyt.Checked = False And ot_levyt.Checked = True Then TAKAjarrut &= "LEVY (OIKEA) "

            If vt_anturi.Checked = True And ot_anturi.Checked = True Then TAKAjarrut &= "ANTURIT (V/O) "
            If vt_anturi.Checked = True And ot_anturi.Checked = False Then TAKAjarrut &= "ANTURI (VASEN) "
            If vt_anturi.Checked = False And ot_anturi.Checked = True Then TAKAjarrut &= "ANTURI (OIKEA) "


        End If
        Main.TopMost = False
        MsgBox(ETUjarrut & vbCrLf & TAKAjarrut)
        Main.TopMost = True
    End Sub

    Private Sub Viat_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Viat.MouseDoubleClick
        If Viat.Text = "" Then Exit Sub
              TbConnection.Close()
            Try
                Dim kasittely As String = Viat.Text
                Dim VIKA As String() = kasittely.Split(vbTab)

                Dim haeVika As New MySqlCommand()
                With haeVika
                    .Connection = TbConnection
                    .CommandType = CommandType.Text
                    .CommandText = "UPDATE VikaIlmoitukset " & _
                        "SET LiitettyTMnro = @TMNro, Korjattu = @Korjattu " & _
                        "WHERE riviID = @riviID"
                .Parameters.AddWithValue("@Korjattu", 0)
                    .Parameters.AddWithValue("@riviID", VIKA(4))
                .Parameters.AddWithValue("@TMnro", 0)
                End With
                TbConnection.Open()
                haeVika.ExecuteNonQuery()

            Catch ex As Exception
                Err.Clear()

            End Try
            TbConnection.Close()

        Viat.Items.Remove(Viat.Text)


    End Sub

    Private Sub Viat_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Viat.SelectedIndexChanged

    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        lataaVikailmoituksetILMANTyoMaaraysta()
    End Sub






    Private Sub TyossaMukana_DrawItem(sender As Object, e As System.Windows.Forms.DrawItemEventArgs) Handles TyossaMukana.DrawItem

    End Sub

    Private Sub TyossaMukana_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles TyossaMukana.MouseDoubleClick
        If TyossaMukana.Text = "" Then Exit Sub
        Dim itemi As itemTekijat = TyossaMukana.SelectedItem

        TyossaMukana.Items.Remove(itemi)
        If TyossaMukana.Items.Count > 1 Then MennytAikaa.Enabled = False
        If TyossaMukana.Items.Count < 2 Then MennytAikaa.Enabled = True
        If TyossaMukana.Items.Count > 1 Then
            Dim aikaa As Integer = 0
            For i As Integer = 0 To TyossaMukana.Items.Count - 1
                itemi = TyossaMukana.Items.Item(i)
                aikaa += muutaTunnitMinsoiksi(itemi.Aika)


            Next
            MennytAikaa.Text = MuutaMinsatTunneiksi(aikaa)


        End If

        If TyossaMukana.Items.Count = 1 Then
            Dim aikaa As Integer = 0
            itemi = TyossaMukana.Items.Item(0)
            MennytAikaa.Text = itemi.Aika

        End If

        If TyossaMukana.Items.Count = 0 Then MennytAikaa.Text = ""

    End Sub

    Private Sub TyossaMukana_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles TyossaMukana.SelectedIndexChanged

    End Sub

    Private Sub cbTekija_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbTekija.SelectedIndexChanged

    End Sub
    Public Structure itemTekijat
        Public Property Nimi As String
        Public Property NimijaAika As String
        Public Property Aika As String

    End Structure
    Private Sub cbTekija_SelectedValueChanged(sender As Object, e As System.EventArgs) Handles cbTekija.SelectedValueChanged
        If cbTekija.Text = "" Then Exit Sub
        TyossaMukana.DisplayMember = "NimijaAika"

        Dim uusi As New itemTekijat

        uusi.Nimi = cbTekija.Text
        uusi.NimijaAika = cbTekija.Text


        Dim itemi As New itemTekijat

        For Each ITEMI In TyossaMukana.Items
            If itemi.Nimi = uusi.Nimi Then Exit Sub

        Next

        TyossaMukana.Items.Add(uusi)


        'TyossaMukana.Items.Add(cbTekija.Text)
        If TyossaMukana.Items.Count > 1 Then MennytAikaa.Enabled = False
        If TyossaMukana.Items.Count < 2 Then MennytAikaa.Enabled = True


    End Sub

    Private Sub UsedTime_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles UsedTime.KeyDown
        If e.KeyCode = Keys.Enter Then
            '    Dim x As Integer = TyossaMukana.SelectedIndex
            Dim itemi As itemTekijat = TyossaMukana.SelectedItem

            If TyossaMukana.Text = "" Then Exit Sub

            If UsedTime.Text = "" Then Exit Sub

            UsedTime.Text = ErotteleAika(UsedTime.Text)

            '   TyossaMukana.Tag = UsedTime.Text
            '   Dim nimi As String = TyossaMukana.Text

            TyossaMukana.Items.Remove(itemi)
            itemi.Aika = UsedTime.Text
            itemi.NimijaAika = itemi.Nimi & " (" & itemi.Aika & ")"


            TyossaMukana.Items.Add(itemi)
            ContextMenuStrip1.Close()


            If TyossaMukana.Items.Count > 1 Then
                Dim aikaa As Integer = 0
                For i As Integer = 0 To TyossaMukana.Items.Count - 1
                    itemi = TyossaMukana.Items.Item(i)
                    aikaa += muutaTunnitMinsoiksi(itemi.Aika)


                Next
                MennytAikaa.Text = MuutaMinsatTunneiksi(aikaa)


            End If

            If TyossaMukana.Items.Count = 1 Then
                Dim aikaa As Integer = 0
                itemi = TyossaMukana.Items.Item(0)
                MennytAikaa.Text = itemi.Aika

            End If

            If TyossaMukana.Items.Count = 0 Then MennytAikaa.Text = ""



        End If

    End Sub
    Private Sub UsedTime_Leave(sender As Object, e As System.EventArgs) Handles UsedTime.Leave
        If cbTekija.Text = "" Then Exit Sub
        If UsedTime.Text = "" Then Exit Sub

        UsedTime.Text = ErotteleAika(UsedTime.Text)

        TyossaMukana.Tag = UsedTime.Text
        Dim nimi As String = TyossaMukana.Text

        TyossaMukana.Items.Remove(TyossaMukana.SelectedItem)
        TyossaMukana.Items.Add(nimi & " = " & UsedTime.Text)



    End Sub

    Private Sub HH_toimi_Click(sender As System.Object, e As System.EventArgs)
        ' hh poistettu TMpalautuksesta        If HH_toimi.Text = "AVAA" Then
        ' hh poistettu TMpalautuksesta HH.ExpandAll()
        ' hh poistettu TMpalautuksesta        HH_toimi.Text = "SULJE"
        ' hh poistettu TMpalautuksesta        Exit Sub
        ' hh poistettu TMpalautuksesta        End If

        ' hh poistettu TMpalautuksesta        If HH_toimi.Text = "SULJE" Then
        ' hh poistettu TMpalautuksesta HH.CollapseAll()


        ' hh poistettu TMpalautuksesta        HH_toimi.Text = "AVAA"
        ' hh poistettu TMpalautuksesta        Exit Sub
        ' hh poistettu TMpalautuksesta       End If

    End Sub
    Public Function poistaTuplat(ByVal initialArray As String()) As String()
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim newArray(0) As String

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

        Return newArray

    End Function
    Private Sub LisattavaOmaVika_TextChanged(sender As System.Object, e As System.EventArgs) Handles LisattavaOmaVika.TextChanged

    End Sub

    Private Sub Otsikko_Click(sender As Object, e As System.EventArgs) Handles Otsikko.Click
        '    HaeTMtiedot()

    End Sub

    Private Sub Otsikko_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Otsikko.SelectedIndexChanged

    End Sub

    Private Sub txtTMnro_Click(sender As System.Object, e As System.EventArgs) Handles txtTMnro.Click

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If LisattavaOmaVika.Text = "" Then LisattavaOmaVika.Focus() : Exit Sub

        Try
            TbConnection.Close()
            Dim haeVika2 As New MySqlCommand()
            With haeVika2
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO VikaIlmoitukset " & _
                    "(IlmoitettuVika, AutoNro, HloNro, IlmPVM, Korjattu, LiitettyTMnro) " & _
                    "VALUES(@IlmoitettuVika, @AutoNro, @HloNro, @IlmPVM, @Korjattu, @LiitettyTMnro)"
                .Parameters.AddWithValue("@Korjattu", False)
                .Parameters.AddWithValue("@IlmoitettuVika", LisattavaOmaVika.Text)
                '       .Parameters.AddWithValue("@LiitettyTMnro", nroTM)

                .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbRekNro.Text))
                .Parameters.AddWithValue("@HloNro", KayttajaHloNro)
                .Parameters.AddWithValue("@IlmPVM", Now)
                .Parameters.AddWithValue("@LiitettyTMnro", Val(txtTMnro.Text))


            End With
            TbConnection.Open()
            haeVika2.ExecuteNonQuery()
            haeVika2.CommandText = "SELECT @@IDENTITY AS TEMPVALUE"
            Dim tmnrovap As Integer = haeVika2.ExecuteScalar
            TbConnection.Close()

            Viat.Items.Add(FormatDateTime(Now, DateFormat.ShortDate).ToString & vbTab & LisattavaOmaVika.Text.ToUpper & vbTab & vbTab & vbTab & tmnrovap.ToString)

        Catch ex As Exception
            Err.Clear()

        End Try


        LisattavaOmaVika.Text = ""


    End Sub

    Private Sub UsedTime_Click(sender As System.Object, e As System.EventArgs) Handles UsedTime.Click

    End Sub
End Class