Imports MySql.Data.MySqlClient

Public Class ViewHuoltohistoria
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)
    Private Sub ViewHuoltohistoria_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LataaAutot()
        If OletusAuto <> "" Then cbRekNro.Text = OletusAuto
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
        Dim AutoNro As Integer
        Dim TMnro As Long

    End Structure
    Public Sub lataaHuoltoHistoria()
        Dim newRootNode As TreeNode
        Dim NewParentNode As TreeNode
        Dim NewChild As TreeNode
        HH.Nodes.Clear()
        If cbRekNro.Text = "" Then Exit Sub
        Dim rd As MySqlDataReader
        newRootNode = New TreeNode(cbRekNro.Text)
        HH.Nodes.Add(newRootNode)
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
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbRekNro.Text))

        End With
        Dim apupvm As Date = Nothing


        '                        "INNER JOIN TMKaytetytOsat ON Tyomaaraykset.TMnro = TMkaytetytOsat.TMnro " & _
        'TMKaytetytOsat.Osa,
        TbConnection.Open()
        '7.12.2015     Try
        rd = cmd.ExecuteReader
        apupvm = Nothing

        Dim US As hsiirto
        Dim vs As hsiirto = Nothing
        Dim asentajia(1) As String '() = Nothing
        Dim aikaakaytetty(1) As String '() = Nothing
        ''        Dim osia(1) As String ' = Nothing
        Dim toita(1) As String '= Nothing
        Dim vikoja(1) As String '() = Nothing
        If rd.HasRows = True Then


            While rd.Read
                apupvm = CType(rd.GetValue(0), Date)
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

        Dim tn As TreeNode
        For Each tn In HH.Nodes
            If tn.Text = cbRekNro.Text Then
                HH.SelectedNode = tn
                HH.SelectedNode.Expand()

                Exit For
            End If
        Next
        '7.12.2015    Catch ex As Exception
        '7.12.2015    LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE viewhuoltohistoria", ErrorToString, 0, 0)
        '7.12.2015       Err.Clear()

        '7.12.2015       End Try


        '    HH.ExpandAll()

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



    Private Sub TableLayoutPanel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel1.Paint

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
    Private Sub cbRekNro_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbRekNro.SelectedIndexChanged
        If cbRekNro.Text = "" Then Exit Sub
        Me.Cursor = Cursors.WaitCursor

        lataaHuoltoHistoria()
        lataakirjatutHuollot()
        Me.Cursor = Cursors.Default
        OletusAuto = cbRekNro.Text

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

    Private Sub KirjatutHuollot_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles KirjatutHuollot.SelectedIndexChanged

    End Sub

    Private Sub HH_AfterSelect(sender As System.Object, e As System.Windows.Forms.TreeViewEventArgs) Handles HH.AfterSelect

    End Sub

    Private Sub SplitContainer1_SplitterMoved(sender As System.Object, e As System.Windows.Forms.SplitterEventArgs) Handles SplitContainer1.SplitterMoved

    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub TableLayoutPanel3_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel3.Paint

    End Sub

    Private Sub TableLayoutPanel2_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel2.Paint

    End Sub

    Private Sub Panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Label2_Click(sender As System.Object, e As System.EventArgs) Handles Label2.Click

    End Sub
End Class