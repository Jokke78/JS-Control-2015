Imports MySql.Data.MySqlClient

Public Class NaytaKatsastuksienVuosiKoonti
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection2 As MySqlConnection = New MySqlConnection(serverString)
    Public clickedLabel As ListBox
    Public itemiX As New autoItemi

    Private leimattu As ListBox()
    Private katsastamatta As ListBox()


    Private Sub NaytaKatsastuksienVuosiKoonti_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)




    End Sub
    Public Sub Alkutoimet()
        leimattu = New ListBox() {b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12}
        katsastamatta = New ListBox() {a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12}
        TarkistaKatsastukset()


        leimattu(Today.Month - 1).BackColor = Color.LightBlue
        katsastamatta(Today.Month - 1).BackColor = Color.LightBlue

     
    End Sub


    Public Sub TarkistaKatsastukset()
        For i As Integer = 0 To 11
            leimattu(i).Items.Clear()
            katsastamatta(i).Items.Clear()

        Next
        TbConnection.Close()

        '      Dim TMautonro As String = TeeVikailmoitus.PalautaSQLNumeroRekkarista(TMrekkari)
        Dim laskuri As Integer = 0

        Dim nytON As Date = FormatDateTime(Now, DateFormat.ShortDate)
        Dim rekkari As String = ""

        Dim erotus As TimeSpan = Nothing

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM KatsastuksienSeuranta ORDER BY SeuraavaKatsastusPVM ASC" ' WHERE AutoNro=@AutoNro"
            '    .Parameters.AddWithValue("@AutoNro", TMAutonro)

        End With
        TbConnection.Open()
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
            Dim rekpvm As Date
            TbConnection2.Open()
            Dim KalustoReader As MySqlDataReader = cmKAT.ExecuteReader
            If KalustoReader.HasRows = True Then
                KalustoReader.Read()



                If KalustoReader.GetValue(14) = True Then
                    laskuri += 1

                    '              ViimeisinLeima.Text = FormatDateTime(CType(rd.GetSqlValue(4), Date).AddYears(-1), DateFormat.ShortDate).ToString

                    rekpvm = CType(KalustoReader.GetValue(2), Date)

                    erotus = CType(rd.GetValue(4), Date) - nytON
                    '                    If erotus.TotalDays < 365 And erotus.TotalDays > 0 Then
                    If CType(rd.GetValue(4), Date) > nytON And CType(rd.GetValue(4), Date).Year > nytON.Year Then 'erotus.TotalDays < 365 And erotus.TotalDays > 0 Then
                        ' leimattu enne kuluvaa päivää
                        ' LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ AIKAA KATSASTAA")
                        '   rekkari = TeeVikailmoitus.PalautaSQLNumeroRekkarista(rd.GetValue(1))
                        AikaaLeimaan(FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString, PalautaSQLRekkariNumerosta(rd.GetValue(1)), True, rd.GetValue(1), rekpvm) ' & " " & Int(erotus.TotalDays) & " " & "PÄIVÄÄ"


                    End If

                    If CType(rd.GetValue(4), Date) > nytON And CType(rd.GetValue(4), Date).Year = nytON.Year Then 'erotus.TotalDays < 365 And erotus.TotalDays > 0 Then
                        ' kuluvan vuoden katsastus tekemättä
                        ' LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ AIKAA KATSASTAA")
                        '   rekkari = TeeVikailmoitus.PalautaSQLNumeroRekkarista(rd.GetValue(1))
                        AikaaLeimaan(FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString, PalautaSQLRekkariNumerosta(rd.GetValue(1)), False, rd.GetValue(1), rekpvm) ' & " " & Int(erotus.TotalDays) & " " & "PÄIVÄÄ"


                    End If

                    If CType(rd.GetValue(4), Date) < nytON And CType(rd.GetValue(4), Date).Year <= nytON.Year Then 'erotus.TotalDays < 365 And erotus.TotalDays > 0 Then
                        ' kuluvan vuoden katsastus tekemättä
                        ' LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ AIKAA KATSASTAA")
                        '   rekkari = TeeVikailmoitus.PalautaSQLNumeroRekkarista(rd.GetValue(1))
                        AikaaLeimaan(FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString, PalautaSQLRekkariNumerosta(rd.GetValue(1)), False, rd.GetValue(1), rekpvm) ' & " " & Int(erotus.TotalDays) & " " & "PÄIVÄÄ"


                    End If

                    If erotus.TotalDays = 0 Then
                        ' katsastettava kuluvan päivän aikana = katsastamaton
                        '  LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " KATSASTETTAVA TÄNÄÄN")
                        AikaaLeimaan(FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString, PalautaSQLRekkariNumerosta(rd.GetValue(1)), False, rd.GetValue(1), rekpvm) ' & " " & Int(erotus.TotalDays) & " " & "KATSASTETTAVA TÄNÄÄN"

                    End If

                    '                  If erotus.TotalDays < 0 Then
                    'Dim luku = -erotus.TotalDays

                    '         LB.Items.Add(rd.GetValue(1).ToString & vbTab & vbTab & FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString & vbTab & Int(erotus.TotalDays).ToString & " PÄIVÄÄ KATSASTAMATTOMANA")
                    '                AikaaLeimaan(FormatDateTime(rd.GetValue(4), DateFormat.ShortDate).ToString, TeeVikailmoitus.PalautaSQLRekkariNumerosta(rd.GetValue(1)), False) ' & " " & Int(luku) & " " & "PV KATSASTAMATTOMANA" ', AJOKIELLOSSA")

                    '            End If

                End If
                KalustoReader.Close()
                TbConnection2.Close()

            End If


        End While
        TbConnection.Close()
        rd.Close()
        lbLaskuri.Text = "KATSASTUKSIEN SEURANNASSA " & laskuri & " AUTOA"

    End Sub
    Public Structure autoItemi
        Public Property RekNro As String
        Public Property Nro As Integer
        Public Property Teksti As String
        Public Property rekPvm As Date

    End Structure
    Public Sub AikaaLeimaan(ByVal pvm As String, ByVal rekkari As String, ByVal tyyppi As Boolean, ByVal AutoNro As Integer, ByVal rekPVM As Date)
        Dim uusi As New autoItemi
        Dim kk As Date = CType(pvm, Date)
        Dim kuu As Integer = kk.Month


        uusi.Nro = AutoNro
        uusi.RekNro = rekkari
        uusi.rekPvm = rekPVM
        uusi.Teksti = pvm & " " & rekkari & "(" & AutoNro & ")"

        If tyyppi = True Then
            leimattu(kuu - 1).Items.Add(uusi)
        Else
            katsastamatta(kuu - 1).Items.Add(uusi)




        End If
    End Sub


    Private Sub a2_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles a1.MouseClick, a2.MouseClick, a3.MouseClick, a4.MouseClick, a5.MouseClick, a6.MouseClick, a7.MouseClick, a8.MouseClick, a9.MouseClick, a10.MouseClick, a11.MouseClick, a12.MouseClick, b1.MouseClick, b2.MouseClick, b3.MouseClick, b4.MouseClick, b5.MouseClick, b6.MouseClick, b7.MouseClick, b8.MouseClick, b9.MouseClick, b10.MouseClick, b11.MouseClick, b12.MouseClick


        '    Dim itemi As New autoItemi


        clickedLabel = CType(sender, ListBox)
        If clickedLabel IsNot Nothing Then itemiX = clickedLabel.SelectedItem


        '   If clickedLabel.Text = "" Then Exit Sub 'clickedLabel.ForeColor = Color.Red


        'MsgBox("Label #" & clickedLabel.Name)
        Dim stra As String = clickedLabel.Name
        '     stra = stra.Replace("a", "")
        '    stra = stra.Replace("b", "")

        '    Dim a As Integer = Val(stra)

        '     a -= 1
        If clickedLabel.SelectedItem IsNot Nothing Then
            '      MsgBox(itemiX.Nro.ToString)
        End If

    End Sub



    Private Sub a2_MouseEnter(sender As Object, e As System.EventArgs)

    End Sub

    Private Sub a2_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub a1_MouseLeave(sender As Object, e As System.EventArgs)
        clickedLabel = Nothing

    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If clickedLabel Is Nothing Then ContextMenuStrip1.Close()

        menuRekNro.Text = itemiX.RekNro
        RekpvmToolStripMenuItem.Text = FormatDateTime(itemiX.rekPvm, DateFormat.ShortDate).ToString

    End Sub

    Private Sub MERKITSEKATSASTETUKSIToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MERKITSEKATSASTETUKSIToolStripMenuItem.Click
        Dim onnas As Boolean = False
        If menuRekNro.Text = "" Then Exit Sub

        onnas = TallennaKatsastus(itemiX.Nro, True, Today, KayttajaHloNro, Today)
        TarkistaKatsastukset()
        If onnas = True Then
            Main.TopMost = False

            MsgBox("TALLENNETTU KATSASTUS")
            Main.TopMost = True

        Else
            Main.TopMost = False

            MsgBox("VIRHE KATSASTUKSEN TALLENTAMISESSA. Ota yhteys Joakimiin. p. 050 521 6804")
            Main.TopMost = True

        End If

    End Sub

    Private Sub PERUKATSASTUSToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles PERUKATSASTUSToolStripMenuItem.Click
        Dim onnas As Boolean = False
        If menuRekNro.Text = "" Then Exit Sub

        onnas = TallennaKatsastuksenPERUUTUS(itemiX.Nro, True, Today, KayttajaHloNro, Today)
        TarkistaKatsastukset()
        If onnas = True Then
            Main.TopMost = False

            MsgBox("TALLENNETTU KATSASTUKSEN PERUUTUS")
            Main.TopMost = True

        Else
            Main.TopMost = False
            MsgBox("VIRHE KATSASTUKSEN PERUUTTAMISESSA. Ota yhteys Joakimiin. p. 050 521 6804")
            Main.TopMost = True

        End If

    End Sub
   
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        SplitContainer1.SplitterDistance = 718


    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        SplitContainer1.SplitterDistance = 0

    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        SplitContainer1.SplitterDistance = 445

    End Sub
End Class