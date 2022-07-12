Imports MySql.Data.MySqlClient


Public Class LahtoListaUseatMuutokset
    Private MyYhteys As MySqlConnection = New MySqlConnection(serverString)


    Public Sub lataaAutoCBhen()
        myYhteys.Close()
        Auto1.Items.Clear()
        ValittuAuto.Items.Clear()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT RekNro FROM Kalusto WHERE Raporteissa='1' ORDER BY RekNro ASC"
            '           .Parameters.AddWithValue("@e", "E")
            '        .Parameters.AddWithValue("@j", "J")
            '

        End With
        '   Try
        myYhteys.Open()

        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then
            While rd.Read
                If rd.GetValue(0) <> "xxxxxx" Then Auto1.Items.Add(rd.GetValue(0))
                If rd.GetValue(0) <> "xxxxxx" Then ValittuAuto.Items.Add(rd.GetValue(0))

            End While
        End If
        '  Catch ex As Exception
        'Err.Clear()

        '  End Try
        myYhteys.Close()
    End Sub

    Public Sub LataaTyoVuorotCBhin()
        MyYhteys.Close()
        TVuorot1.Items.Clear()
        TVuorot2.Items.Clear()


        Dim dAl As Date = Format(Today, "\#yyyy\-MM\-dd\#")

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = MyYhteys
            .CommandType = CommandType.Text     '  
            .CommandText = "SELECT Lyhenne FROM TyoVuorot WHERE AlkaenPVM<@Nyt AND AstiPVM>@Nyt"
            .Parameters.AddWithValue("@nyt", dAl)
            '        .Parameters.AddWithValue("@j", "J")
            '

        End With
        '   Try
        MyYhteys.Open()

        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then
            While rd.Read

                If rd.GetString(0) <> "" Then TVuorot1.Items.Add(rd.GetString(0))
                If rd.GetString(0) <> "" Then TVuorot2.Items.Add(rd.GetString(0))


            End While
        End If
        '  Catch ex As Exception
        'Err.Clear()

        '  End Try
        MyYhteys.Close()









    End Sub
    Public Sub LataaKuljettajatCBhen()
        myYhteys.Close()
        Kuljettajat1.Items.Clear()
        Kuljettajat2a.Items.Clear()
        Kuljettajat2b.Items.Clear()
        Kuljettajat4.Items.Clear()
        ValittuKuljettaja.Items.Clear()



        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT SukuNimi, EtuNimi FROM Henkilosto WHERE tSuhdeVoimassa=0 ORDER BY SukuNimi ASC"
            '  .Parameters.AddWithValue("@id", False)

        End With
        '   Try
        myYhteys.Open()

        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then
            While rd.Read
                Kuljettajat1.Items.Add(rd.GetValue(0) & " " & rd.GetValue(1))
                Kuljettajat2a.Items.Add(rd.GetValue(0) & " " & rd.GetValue(1))
                Kuljettajat2b.Items.Add(rd.GetValue(0) & " " & rd.GetValue(1))
                Kuljettajat4.Items.Add(rd.GetValue(0) & " " & rd.GetValue(1))
                ValittuKuljettaja.Items.Add(rd.GetValue(0) & " " & rd.GetValue(1))

            End While
        End If
        '  Catch ex As Exception
        'Err.Clear()

        '  End Try
        MyYhteys.Close()

    End Sub
    Private Sub LahtoListaUseatMuutokset_Load(sender As Object, e As EventArgs) Handles Me.Load
        LataaTyoVuorotCBhin()
        LataaKuljettajatCBhen()
        lataaAutoCBhen()
    End Sub

    Private Sub Alkupvm1_ValueChanged(sender As Object, e As EventArgs) Handles Alkupvm1.ValueChanged

        Alkupvm2.Value = Alkupvm1.Value
        Alkupvm3.Value = Alkupvm1.Value
        Alkupvm4.Value = Alkupvm1.Value



    End Sub

    Private Sub Alkupvm2_ValueChanged(sender As Object, e As EventArgs) Handles Alkupvm2.ValueChanged
        Alkupvm1.Value = Alkupvm2.Value
        Alkupvm3.Value = Alkupvm2.Value
        Alkupvm4.Value = Alkupvm2.Value

    End Sub

    Private Sub Alkupvm3_ValueChanged(sender As Object, e As EventArgs) Handles Alkupvm3.ValueChanged
        Alkupvm2.Value = Alkupvm3.Value
        Alkupvm1.Value = Alkupvm3.Value
        Alkupvm4.Value = Alkupvm3.Value

    End Sub

    Private Sub Alkupvm4_ValueChanged(sender As Object, e As EventArgs) Handles Alkupvm4.ValueChanged
        Alkupvm2.Value = Alkupvm4.Value
        Alkupvm3.Value = Alkupvm4.Value
        Alkupvm1.Value = Alkupvm4.Value

    End Sub

    Private Sub loppupvm1_ValueChanged(sender As Object, e As EventArgs) Handles loppupvm1.ValueChanged
        loppupvm2.Value = loppupvm1.Value
        loppupvm3.Value = loppupvm1.Value
        loppupvm4.Value = loppupvm1.Value

    End Sub

    Private Sub loppupvm2_ValueChanged(sender As Object, e As EventArgs) Handles loppupvm2.ValueChanged
        loppupvm1.Value = loppupvm2.Value
        loppupvm3.Value = loppupvm2.Value
        loppupvm4.Value = loppupvm2.Value

    End Sub

    Private Sub loppupvm3_ValueChanged(sender As Object, e As EventArgs) Handles loppupvm3.ValueChanged
        loppupvm2.Value = loppupvm3.Value
        loppupvm1.Value = loppupvm3.Value
        loppupvm4.Value = loppupvm3.Value

    End Sub

    Private Sub loppupvm4_ValueChanged(sender As Object, e As EventArgs) Handles loppupvm4.ValueChanged
        loppupvm2.Value = loppupvm4.Value
        loppupvm3.Value = loppupvm4.Value
        loppupvm1.Value = loppupvm4.Value

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        MyYhteys.Close()
        MyYhteys.Open()

        Dim tbAdapter As New MySqlDataAdapter()
        Dim dAl1 As Date = Format(Alkupvm4.Value, "\#yyyy\-MM\-dd\#")
        Dim dAl2 As Date = Format(loppupvm4.Value, "\#yyyy\-MM\-dd\#")

        With tbAdapter
            .SelectCommand = New MySqlCommand
            .SelectCommand.Connection = MyYhteys
            .SelectCommand.CommandText = "SELECT AjetutVuorot.AlkuPVM, AjetutVuorot.TVLyhenne, Kalusto.RekNro, Henkilosto.SukuNimi, Henkilosto.Etunimi FROM AjetutVuorot " & _
                "INNER JOIN Henkilosto ON Henkilosto.HloNro=AjetutVuorot.HloNro " & _
                "INNER JOIN Kalusto ON Kalusto.AutoNro=AjetutVuorot.AutoNro " & _
                "WHERE (AjetutVuorot.AlkuPVM BETWEEN @AlkaenPVM AND @AstiPVM) AND (AjetutVuorot.HloNro=@Hlo) ORDER BY AjetutVuorot.AlkuPVM ASC"
            .SelectCommand.Parameters.AddWithValue("@AlkaenPVM", dAl1)
            .SelectCommand.Parameters.AddWithValue("@AstiPVM", dAl2)
            .SelectCommand.Parameters.AddWithValue("@Hlo", PalautaSQLNroNimiesta(Kuljettajat4.Text))


        End With
        Dim myDataSet As DataSet = New DataSet()

        tbAdapter.Fill(myDataSet, "AjetutVuorot")

        Dim myDataView = New DataView(myDataSet.Tables("AjetutVuorot"))
        DGW.AutoGenerateColumns = True
        DGW.DataSource = myDataSet
        DGW.DataMember = "AjetutVuorot"
        MyYhteys.Close()
        DGW.Columns(0).Width = 100
        DGW.Columns(0).HeaderText = "PVM"
        DGW.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DGW.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        DGW.Columns(1).Width = 100
        DGW.Columns(1).HeaderText = "Vuoro"
        DGW.Columns(2).Width = 100
        DGW.Columns(2).HeaderText = "Auto"
        DGW.Columns(3).Width = 180
        DGW.Columns(3).HeaderText = "Sukunimi"

        DGW.Columns(4).Width = 180
        DGW.Columns(4).HeaderText = "Etunimi"

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TVuorot2.Text = "" Then Exit Sub
        Try
            MyYhteys.Close()

        Catch ex As Exception
            Err.Clear()

        End Try

        Dim dAl1 As Date = Format(Alkupvm3.Value, "\#yyyy\-MM\-dd\#")
        Dim dAl2 As Date = Format(loppupvm3.Value, "\#yyyy\-MM\-dd\#")

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = MyYhteys
            .CommandType = CommandType.Text
            .CommandText = "UPDATE AjetutVuorot SET AutoNro=@AutoNro WHERE (AlkuPVM BETWEEN @AlkaenPVM AND @AstiPVM) AND (TVLyhenne=@TVLyhenne)"
            .Parameters.AddWithValue("@AlkaenPVM", dAl1)
            .Parameters.AddWithValue("@AstiPVM", dAl2)
            .Parameters.AddWithValue("@TVLyhenne", TVuorot2.Text)
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(Auto1.Text))

        End With

        Try
            MyYhteys.Open()
            cmd.ExecuteNonQuery()
            MyYhteys.Close()
            Main.TopMost = False

            MsgBox("Vaihdettu")
            Main.TopMost = True

        Catch ex As Exception
            Err.Clear()

        End Try









    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            MyYhteys.Close()

        Catch ex As Exception
            Err.Clear()

        End Try
        'postetaan uuden kuljettajat vanha työvuorot
        Dim dAl1 As Date = Format(Alkupvm2.Value, "\#yyyy\-MM\-dd\#")
        Dim dAl2 As Date = Format(loppupvm2.Value, "\#yyyy\-MM\-dd\#")

        Dim cmd2 As New MySqlCommand()
        With cmd2
            .Connection = MyYhteys
            .CommandType = CommandType.Text
            .CommandText = "UPDATE AjetutVuorot SET HloNro=@HloNroVanha WHERE (AlkuPVM BETWEEN @AlkaenPVM AND @AstiPVM) AND (HloNro=@HloNroUusi)"
            .Parameters.AddWithValue("@AlkaenPVM", dAl1)
            .Parameters.AddWithValue("@AstiPVM", dAl2)
            .Parameters.AddWithValue("@HloNroVanha", 0)
            .Parameters.AddWithValue("@HloNroUusi", PalautaSQLNroNimiesta(Kuljettajat2b.Text))

        End With

        Try
            MyYhteys.Open()
            cmd2.ExecuteNonQuery()
            MyYhteys.Close()


        Catch ex As Exception
            Err.Clear()

        End Try




        'lisätään uudet työvuorot


        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = MyYhteys
            .CommandType = CommandType.Text
            .CommandText = "UPDATE AjetutVuorot SET HloNro=@HloNroUusi WHERE (AlkuPVM BETWEEN @AlkaenPVM AND @AstiPVM) AND (HloNro=@HloNroVanha)"
            .Parameters.AddWithValue("@AlkaenPVM", dAl1)
            .Parameters.AddWithValue("@AstiPVM", dAl2)
            .Parameters.AddWithValue("@HloNroVanha", PalautaSQLNroNimiesta(Kuljettajat2a.Text))
            .Parameters.AddWithValue("@HloNroUusi", PalautaSQLNroNimiesta(Kuljettajat2b.Text))

        End With

        Try
            MyYhteys.Open()
            cmd.ExecuteNonQuery()
            MyYhteys.Close()
            Main.TopMost = False

            MsgBox("Vaihdettu")
            Main.TopMost = True

        Catch ex As Exception
            Err.Clear()

        End Try







    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            MyYhteys.Close()

        Catch ex As Exception
            Err.Clear()

        End Try

        Dim dAl1 As Date = Format(Alkupvm1.Value, "\#yyyy\-MM\-dd\#")
        Dim dAl2 As Date = Format(loppupvm1.Value, "\#yyyy\-MM\-dd\#")
        'postetaan ensiksi kaikki työvuorot kuljettajalta

        Dim cmd2 As New MySqlCommand()
        With cmd2
            .Connection = MyYhteys
            .CommandType = CommandType.Text
            .CommandText = "UPDATE AjetutVuorot SET HloNro=@HloNro WHERE (AlkuPVM BETWEEN @AlkaenPVM AND @AstiPVM) AND (TVLyhenne=@TVLyhenne)"
            .Parameters.AddWithValue("@AlkaenPVM", dAl1)
            .Parameters.AddWithValue("@AstiPVM", dAl2)
            .Parameters.AddWithValue("@TVLyhenne", TVuorot1.Text)
            .Parameters.AddWithValue("@HloNro", 0)

        End With

        Try
            MyYhteys.Open()
            cmd2.ExecuteNonQuery()
            MyYhteys.Close()


        Catch ex As Exception
            Err.Clear()

        End Try



        'lisätään uudet työvuorot

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = MyYhteys
            .CommandType = CommandType.Text
            .CommandText = "UPDATE AjetutVuorot SET HloNro=@HloNro WHERE (AlkuPVM BETWEEN @AlkaenPVM AND @AstiPVM) AND (TVLyhenne=@TVLyhenne)"
            .Parameters.AddWithValue("@AlkaenPVM", dAl1)
            .Parameters.AddWithValue("@AstiPVM", dAl2)
            .Parameters.AddWithValue("@TVLyhenne", TVuorot1.Text)
            .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(Kuljettajat1.Text))

        End With

        Try
            MyYhteys.Open()
            cmd.ExecuteNonQuery()
            MyYhteys.Close()
            Main.TopMost = False

            MsgBox("Vaihdettu")
            Main.TopMost = True

        Catch ex As Exception
            Err.Clear()

        End Try




    End Sub

    Private Sub DGW_RowHeaderMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGW.RowHeaderMouseClick

        myYhteys.Close()
        Panel6.Visible = True

        Dim i As Integer
        i = DGW.CurrentRow.Index
        Dim dAl As Date = Format(DGW.Item(0, i).Value, "\#yyyy\-MM\-dd\#")
        ValittuPVM.Text = FormatDateTime(dAl, DateFormat.ShortDate).ToString


        Dim autovali As String = DGW.Item(2, i).Value.ToString
        Dim kuli As String = DGW.Item(3, i).Value.ToString & " " & DGW.Item(4, i).Value.ToString
        ValittuAuto.Text = autovali
        ValittuKuljettaja.Text = kuli
        PiiloTV.Text = DGW.Item(1, i).Value.ToString

        indexauto.Text = ValittuAuto.SelectedIndex.ToString
        indexkuljettaja.Text = ValittuKuljettaja.SelectedIndex.ToString
        btTallennus.Visible = False

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btTallennus.Click
        Dim dAl As Date = Format(CType(ValittuPVM.Text, Date), "\#yyyy\-MM\-dd\#")

        Try
            MyYhteys.Close()

        Catch ex As Exception
            Err.Clear()
        End Try

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = MyYhteys
            .CommandType = CommandType.Text
            .CommandText = "UPDATE AjetutVuorot SET AutoNro=@AutoNro, HloNro=@HloNro WHERE AlkuPVM=@pvm AND TVLyhenne=@TV"
            .Parameters.AddWithValue("@pvm", dAl)
            .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(ValittuKuljettaja.Text))
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(ValittuAuto.Text))
            .Parameters.AddWithValue("@TV", PiiloTV.Text)

        End With

        Try
            MyYhteys.Open()
            cmd.ExecuteNonQuery()
            MyYhteys.Close()
            Main.TopMost = False

            MsgBox("Tallennettu")
            Main.TopMost = True

        Catch ex As Exception
            Err.Clear()

        End Try
        Panel6.Visible = False
        Button4_Click(Nothing, Nothing)


    End Sub

    Private Sub ValittuKuljettaja_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ValittuKuljettaja.SelectedIndexChanged
        If ValittuKuljettaja.SelectedIndex <> Val(indexkuljettaja.Text) Or ValittuAuto.SelectedIndex <> Val(indexauto.Text) Then
            btTallennus.Visible = True
        Else
            btTallennus.Visible = False

        End If

    End Sub

    Private Sub ValittuAuto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ValittuAuto.SelectedIndexChanged
        If ValittuKuljettaja.SelectedIndex <> Val(indexkuljettaja.Text) Or ValittuAuto.SelectedIndex <> Val(indexauto.Text) Then
            btTallennus.Visible = True
        Else
            btTallennus.Visible = False

        End If
    End Sub
End Class