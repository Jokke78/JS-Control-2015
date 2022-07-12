
Imports MySql.Data.MySqlClient


Public Class NaytaTyovuorolista
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)
    Private MyYhteys As MySqlConnection = New MySqlConnection(serverString)

    Private PVM As Label()
    Private TV As Label()
    Private tAika As Label()
    '   Private Tauko As Label()
    Private bil As Label()
    Private info As RichTextBox()


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        LokiTapahtumanTallennus(KayttajaHloNro, "Tulostettu työvuorolista " & cbJakso.Text, cbKuljettaja.Text, 0, 0)


        'KIRJATAAN KULJETTAJA TIETOISIKSI TYÖVUOROISTA

        Try
            Dim dAl1 As Date = Format(CType(p1.Text, Date), "\#yyyy\-MM\-dd\#")
            Dim dAl2 As Date = Format(CType(p14.Text, Date), "\#yyyy\-MM\-dd\#")

            Dim tietoinenCMD As New MySqlCommand()
            With tietoinenCMD
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "UPDATE AjetutVuorot SET Tietaa=1 WHERE " & _
                    "(AlkuPVM BETWEEN @alku AND @loppu) AND HloNro=@HloNro"
                .Parameters.AddWithValue("@alku", dAl1)
                .Parameters.AddWithValue("@loppu", dAl2)
                .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(cbKuljettaja.Text))
            End With
            MyYhteys.Open()
            tietoinenCMD.ExecuteNonQuery()


        Catch ex As Exception
            Err.Clear()
        Finally
            MyYhteys.Close()

        End Try



        Try
            TulostaTyoVuoroLista.Location = New Point(0, 0)

            TulostaTyoVuoroLista.Show()

            Dim infoa(1) As String

            For i As Integer = 0 To 13
                If TV(i).Text <> "" Then
                    ReDim Preserve infoa(0 To UBound(infoa) + 1)
                    infoa(UBound(infoa)) = TV(i).Text
                End If

            Next

            infoa = poistaTuplat(infoa)

            '     MsgBox(infoa.Count.ToString)
            '   Exit Sub
            informaatio.Visible = True

            TulostaTyoVuoroLista.Location = New Point(0, 0)

            TulostaTyoVuoroLista.Show()


            TulostaTyoVuoroLista.tv1.Text = TV(0).Text
            TulostaTyoVuoroLista.tv2.Text = TV(1).Text
            TulostaTyoVuoroLista.tv3.Text = TV(2).Text
            TulostaTyoVuoroLista.tv4.Text = TV(3).Text
            TulostaTyoVuoroLista.tv5.Text = TV(4).Text
            TulostaTyoVuoroLista.tv6.Text = TV(5).Text
            TulostaTyoVuoroLista.tv7.Text = TV(6).Text
            TulostaTyoVuoroLista.tv8.Text = TV(7).Text
            TulostaTyoVuoroLista.tv9.Text = TV(8).Text
            TulostaTyoVuoroLista.tv10.Text = TV(9).Text
            TulostaTyoVuoroLista.tv11.Text = TV(10).Text
            TulostaTyoVuoroLista.tv12.Text = TV(11).Text
            TulostaTyoVuoroLista.tv13.Text = TV(12).Text
            TulostaTyoVuoroLista.tv14.Text = TV(13).Text

            TulostaTyoVuoroLista.p1.Text = PVM(0).Text
            TulostaTyoVuoroLista.p2.Text = PVM(1).Text
            TulostaTyoVuoroLista.p3.Text = PVM(2).Text
            TulostaTyoVuoroLista.p4.Text = PVM(3).Text
            TulostaTyoVuoroLista.p5.Text = PVM(4).Text
            TulostaTyoVuoroLista.p6.Text = PVM(5).Text
            TulostaTyoVuoroLista.p7.Text = PVM(6).Text
            TulostaTyoVuoroLista.p8.Text = PVM(7).Text
            TulostaTyoVuoroLista.p9.Text = PVM(8).Text
            TulostaTyoVuoroLista.p10.Text = PVM(9).Text
            TulostaTyoVuoroLista.p11.Text = PVM(10).Text
            TulostaTyoVuoroLista.p12.Text = PVM(11).Text
            TulostaTyoVuoroLista.p13.Text = PVM(12).Text
            TulostaTyoVuoroLista.p14.Text = PVM(13).Text

            TulostaTyoVuoroLista.c1.Text = bil(0).Text
            TulostaTyoVuoroLista.c2.Text = bil(1).Text
            TulostaTyoVuoroLista.c3.Text = bil(2).Text
            TulostaTyoVuoroLista.c4.Text = bil(3).Text
            TulostaTyoVuoroLista.c5.Text = bil(4).Text
            TulostaTyoVuoroLista.c6.Text = bil(5).Text
            TulostaTyoVuoroLista.c7.Text = bil(6).Text
            TulostaTyoVuoroLista.c8.Text = bil(7).Text
            TulostaTyoVuoroLista.c9.Text = bil(8).Text
            TulostaTyoVuoroLista.c10.Text = bil(9).Text
            TulostaTyoVuoroLista.c11.Text = bil(10).Text
            TulostaTyoVuoroLista.c12.Text = bil(11).Text
            TulostaTyoVuoroLista.c13.Text = bil(12).Text
            TulostaTyoVuoroLista.c14.Text = bil(13).Text

            TulostaTyoVuoroLista.a1.Text = tAika(0).Text
            TulostaTyoVuoroLista.a2.Text = tAika(1).Text
            TulostaTyoVuoroLista.a3.Text = tAika(2).Text
            TulostaTyoVuoroLista.a4.Text = tAika(3).Text
            TulostaTyoVuoroLista.a5.Text = tAika(4).Text
            TulostaTyoVuoroLista.a6.Text = tAika(5).Text
            TulostaTyoVuoroLista.a7.Text = tAika(6).Text
            TulostaTyoVuoroLista.a8.Text = tAika(7).Text
            TulostaTyoVuoroLista.a9.Text = tAika(8).Text
            TulostaTyoVuoroLista.a10.Text = tAika(9).Text
            TulostaTyoVuoroLista.a11.Text = tAika(10).Text
            TulostaTyoVuoroLista.a12.Text = tAika(11).Text
            TulostaTyoVuoroLista.a13.Text = tAika(12).Text
            TulostaTyoVuoroLista.a14.Text = tAika(13).Text

            TulostaTyoVuoroLista.Kuljettajannimijanumero.Text = cbKuljettaja.Text & " (" & PalautaSQLNroNimiesta(cbKuljettaja.Text) & ")"



            'HAETAANINFOT
            If infoa.Count > 0 Then

                For i As Integer = 0 To infoa.Count - 1

                    If i = 0 Then TulostaTyoVuoroLista.info1.Text = palautaTyoVuoronSelite(infoa(i))
                    If i = 1 Then TulostaTyoVuoroLista.info2.Text = palautaTyoVuoronSelite(infoa(i))
                    If i = 2 Then TulostaTyoVuoroLista.info3.Text = palautaTyoVuoronSelite(infoa(i))
                    If i = 3 Then TulostaTyoVuoroLista.info4.Text = palautaTyoVuoronSelite(infoa(i))
                    If i = 4 Then TulostaTyoVuoroLista.info5.Text = palautaTyoVuoronSelite(infoa(i))
                    If i = 5 Then TulostaTyoVuoroLista.info6.Text = palautaTyoVuoronSelite(infoa(i))

                Next

            End If





            Dim jakson As New cbJaksoTiedot
            jakson = cbJakso.SelectedItem

            TulostaTyoVuoroLista.jaksonpaivamaarat.Text = FormatDateTime(jakson.AlkuPVM, DateFormat.ShortDate).ToString & " - " & FormatDateTime(jakson.LoppuPVM, DateFormat.ShortDate).ToString
            TulostaTyoVuoroLista.tulostusPVM.Text = Now.ToString
            TulostaTyoVuoroLista.TextBox1.Focus()

            TulostaTyoVuoroLista.TextBox1.Visible = False





            'tulostetaan käynnistämälläajastin
            TulostaTyoVuoroLista.Timer1.Enabled = True
            informaatio.Visible = False

        Catch ex As Exception
            Err.Clear()
            TulostaTyoVuoroLista.Close()



        End Try

    End Sub
    Public Class cbJaksoTiedot
        Public Property JaksonNro As String
        Public Property AlkuPVM As Date
        Public Property LoppuPVM As Date
    End Class

    Public Function palautaTyoVuoronSelite(ByVal tv As String) As String
        Dim paluu As String = ""

        TbConnection.Close()
        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT Lyhenne, Selite FROM TyoVuorot"
        End With
        TbConnection.Open()

        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then
            While rd.Read
                If rd.GetString(0) = tv Then
                    If rd.GetValue(1) IsNot DBNull.Value Then
                        paluu = rd.GetString(1)
                        TbConnection.Close()
                        rd.Close()

                        Return paluu
                        Exit Function

                    End If
                End If
            End While
        End If





        Return paluu

    End Function
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
    Public Sub LataaCBjaksot()
        cbJakso.Items.Clear()

        TbConnection.Close()
        Dim dal As Date = CType("13.5.2013", Date)


        Dim cmd As New MySqlCommand()

        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT JaksoID, AlkaenPVM, AstiPVM FROM Jaksot " & _
                "WHERE (AlkaenPVM > @Aika) AND naytaKahvio=1 ORDER BY AlkaenPVM DESC"
            .Parameters.AddWithValue("@Aika", dal)

        End With
        TbConnection.Open()
        '0003376797

        Try
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            cbJakso.DisplayMember = "JaksonNro"


            If rd.HasRows = True Then

                While rd.Read
                    Dim uusiLB As New cbJaksoTiedot
                    If rd.GetValue(0) IsNot DBNull.Value Then uusiLB.JaksonNro = rd.GetString(0)
                    If rd.GetValue(1) IsNot DBNull.Value Then uusiLB.AlkuPVM = rd.GetValue(1)
                    If rd.GetValue(2) IsNot DBNull.Value Then uusiLB.LoppuPVM = rd.GetValue(2)

                    cbJakso.Items.Add(uusiLB)


                End While
                rd.Close()
            End If
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE Naytatyövuorolista", ErrorToString, 0, 0)

            Err.Clear()

        End Try
        Dim rivi As New cbJaksoTiedot

        TbConnection.Close()

        'HAETAAN JAKSO JOSSA OLEMMA
        Dim dAl_new As Date = Format(Today, "\#yyyy\-MM\-dd\#")
        Dim cmd2 As New MySqlCommand()

        With cmd2
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT JaksoID, AlkaenPVM, AstiPVM FROM Jaksot WHERE (@Alku BETWEEN AlkaenPVM AND AstiPVM) "
            .Parameters.AddWithValue("@Alku", dAl_new)
        End With
        Try
            Try
                TbConnection.Open()
                Dim rd As MySqlDataReader = cmd2.ExecuteReader
                If rd.HasRows = True Then
                    rd.Read()
                    Dim paluu As String = rd.GetString(0)
                    rivi.AlkuPVM = rd.GetValue(1)
                    rivi.LoppuPVM = rd.GetValue(2)
                    TbConnection.Close()
                    cbJakso.Text = paluu

                End If

            Catch ex As Exception
                Err.Clear()

            End Try


        Catch ex As Exception
            Err.Clear()

        End Try
        For i As Integer = 0 To 13
            PVM(i).Text = ""
            TV(i).Text = ""
            tAika(i).Text = ""
            '         Tauko(i).Text = ""
            bil(i).Text = ""

        Next
        '  rivi = cbJakso.SelectedItem
        aikavali.Text = rivi.AlkuPVM & " - " & rivi.LoppuPVM

        Dim alkuPVM As Date = rivi.AlkuPVM
        For i As Integer = 0 To 13
            PVM(i).Text = alkuPVM
            alkuPVM = alkuPVM.AddDays(1)

        Next

    End Sub
    Private Sub NaytaTyovuoroLista_Load(sender As Object, e As EventArgs) Handles Me.Load
        PVM = New Label() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14}
        TV = New Label() {tv1, tv2, tv3, tv4, tv5, tv6, tv7, tv8, tv9, tv10, tv11, tv12, tv13, tv14}
        tAika = New Label() {a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14}
        '     Tauko = New Label() {b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12, b13, b14}
        bil = New Label() {c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14}

        '    info = New RichTextBox() {info1, info2, info3, info4, info5, info6}
        LataaKuljettajatCBhen()
        LataaCBjaksot()
        '      For i As Integer = 0 To 13
        'PVM(i).Text = ""
        'TV(i).Text = ""
        'tAika(i).Text = ""
        '        Tauko(i).Text = ""
        'bil(i).Text = ""

        'Next
        ' Jos keyPath 1.00 laajennus ei ole käytössä piilotetaan TULOSTA näppäin
        If KeyPath = True Then
            Button2.Visible = True
        Else
            Button2.Visible = False

        End If
    End Sub
    Public Sub LataaKuljettajatCBhen()
        TbConnection.Close()
        cbKuljettaja.Items.Clear()


        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT SukuNimi, EtuNimi FROM Henkilosto WHERE tSuhdeVoimassa=0 ORDER BY SukuNimi ASC"
            '  .Parameters.AddWithValue("@id", False)

        End With
        '   Try
        TbConnection.Open()

        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then
            While rd.Read
                cbKuljettaja.Items.Add(rd.GetValue(0) & " " & rd.GetValue(1))
            End While
        End If
        '  Catch ex As Exception
        'Err.Clear()

        '  End Try
        TbConnection.Close()

    End Sub

    Private Sub cbKuljettaja_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbKuljettaja.SelectedIndexChanged
        HAeTyoytNakyviin()

    End Sub


    Public Sub HAeTyoytNakyviin()
        For i As Integer = 0 To 13
            '      PVM(i).Text = ""
            TV(i).Text = ""
            tAika(i).Text = ""
            '        Tauko(i).Text = ""
            bil(i).Text = ""

        Next

        Dim jaksoTAGi As cbJaksoTiedot
        jaksoTAGi = cbJakso.SelectedItem
        '  Dim kuliTAgi As cbKuljettajaTiedot
        '    kuliTAgi.Nimi = cbKuljettaja.Text
        '   kuliTAgi.HloNro = PalautaSQLNroNimiesta(cbKuljettaja.Text)



        If cbKuljettaja.Text = "" Or cbJakso.Text = "" Then Exit Sub


        Dim alkuaika As Date = jaksoTAGi.AlkuPVM
        Dim kuliNro As Integer = PalautaSQLNroNimiesta(cbKuljettaja.Text)



        For i As Integer = 0 To 13
            TbConnection.Close()

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT AjetutVuorot.TVLyhenne, AjetutVuorot.AutoNro, TyoVuorot.TVAlkaa, TyoVuorot.TVLoppuu FROM AjetutVuorot " & _
                    "INNER JOIN TyoVuorot ON AjetutVuorot.TVLyhenne=TyoVuorot.Lyhenne AND (@AlkuPVM BETWEEN TyoVuorot.AlkaenPVM AND TyoVuorot.AstiPVM) " & _
                    "WHERE AjetutVuorot.AlkuPVM = @AlkuPVM AND AjetutVuorot.HloNro = @HloNro"
                .Parameters.AddWithValue("@AlkuPVM", alkuaika.AddDays(i))
                .Parameters.AddWithValue("@HloNro", kuliNro)
            End With

            TbConnection.Open()
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                Try
                    If rd.GetValue(0) IsNot DBNull.Value Then TV(i).Text = rd.GetString(0)
                    If rd.GetValue(1) IsNot DBNull.Value Then bil(i).Text = PalautaSQLRekkariNumerosta(rd.GetValue(1))
                    If rd.GetValue(2) IsNot DBNull.Value Then tAika(i).Text = Microsoft.VisualBasic.Left(rd.GetValue(2).ToString, 5)
                    If rd.GetValue(3) IsNot DBNull.Value Then tAika(i).Text = tAika(i).Text & " - " & Microsoft.VisualBasic.Left(rd.GetValue(3).ToString, 5)

                Catch ex As Exception
                    Err.Clear()
                End Try


            End If


        Next






    End Sub
    Private Sub cbJakso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbJakso.SelectedIndexChanged
        aikavali.Text = ""

        '   tyhjennetään lista


        For i As Integer = 0 To 13
            PVM(i).Text = ""
            TV(i).Text = ""
            tAika(i).Text = ""
            '         Tauko(i).Text = ""
            bil(i).Text = ""

        Next
        Dim rivi As New cbJaksoTiedot
        rivi = cbJakso.SelectedItem
        aikavali.Text = rivi.AlkuPVM & " - " & rivi.LoppuPVM

        Dim alkuPVM As Date = rivi.AlkuPVM
        For i As Integer = 0 To 13
            PVM(i).Text = alkuPVM
            alkuPVM = alkuPVM.AddDays(1)

        Next
        HAeTyoytNakyviin()
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click


        Dim x As Integer = cbJakso.SelectedIndex

        Dim uusi As Integer = x - 1

        If uusi >= 0 Then cbJakso.SelectedIndex = uusi


    End Sub

    Private Sub TableLayoutPanel10_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel10.Paint

    End Sub
End Class