Imports MySql.Data.MySqlClient

Public Class PysakointiHistoria
    Private myYhteys As MySqlConnection = New MySqlConnection(serverString)

    Private Sub PysakointiHistoria_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        rbKaikki.Checked = True

        LataaKuljettajatCBhen()
        lataaAutoCBhen()
    End Sub
    Public Sub LataaTunnuksetDataKenttaan()
        Try
            myYhteys.Close()
            myYhteys.Open()

            Dim tbAdapter As New MySqlDataAdapter()

            With tbAdapter
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
                .SelectCommand.CommandText = "SELECT PysakointiUUSI.ID, Kalusto.RekNro, PysakointiUUSI.Alue, PysakointiUUSI.Aika, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Kuljettaja, PysakointiUUSI.Otettu FROM PysakointiUUSI " & _
                 "INNER JOIN Kalusto ON Kalusto.AutoNro=PysakointiUUSI.AutoNro " & _
                  "INNER JOIN Henkilosto ON Henkilosto.HloNro=PysakointiUUSI.HloNro " & _
            "ORDER BY PysakointiUUSI.ID DESC LIMIT 50"
                '     "AND Henkilosto.HloNro=Pysakointi.JattanytHlo " & _


            End With
            Dim myDataSet As DataSet = New DataSet()

            tbAdapter.Fill(myDataSet, "PysakointiUUSI")

            Dim myDataView = New DataView(myDataSet.Tables("PysakointiUUSI"))
            DGW.AutoGenerateColumns = True
            DGW.DataSource = myDataSet
            DGW.DataMember = "PysakointiUUSI"
            myYhteys.Close()
            '  DGW.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGW.AutoResizeColumns()

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE Pysakointihistoria Lataa tunnukset", ErrorToString, 0, 0)
            Err.Clear()

        End Try


        'DGW.Columns(0).Width = 90
        'DGW.Columns(1).Width = 220
        'DGW.Columns(2).Width = 180
        '    DGW.Columns(3).HeaderText = "Osasto"
        '   DGW.Columns(3).Width = 160
        'DGW.Columns(3).Width = 110
        'DGW.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DGW.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        'DGW.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        'DGW.Columns(4).Width = 110
        'DGW.Columns(5).Width = 110
        '   DGW.Columns(7).HeaderText = "Ei voimassa"
        '  DGW.Columns(7).Width = 150
    End Sub
    Public Sub lataaAutoCBhen()
        myYhteys.Close()
        cbAuto.Items.Clear()

        Dim cmd As New MySqlCommand()
        Dim dAl As Date = Format(Today, "\#yyyy\-MM\-dd\#")

        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT Kalusto.RekNro FROM Tagit " & _
                "INNER JOIN Kalusto ON Kalusto.AutoNro=Tagit.AutoNro " & _
                "WHERE Tagit.Kulin='0' AND (@Alku BETWEEN AlkaenPVM AND AstiPVM) AND Kalusto.Raporteissa='1' ORDER BY Kalusto.RekNro ASC"
            .Parameters.AddWithValue("@Alku", dal)

            '           .Parameters.AddWithValue("@e", "E")
            '        .Parameters.AddWithValue("@j", "J")
            '

        End With
        Try
            myYhteys.Open()

            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                While rd.Read
                    cbAuto.Items.Add(rd.GetValue(0))
                End While
            End If
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE Pysakointi historia lataa autotCB", ErrorToString, 0, 0)

            Err.Clear()

        End Try
        myYhteys.Close()
    End Sub
    Public Sub LataaKuljettajatCBhen()
        myYhteys.Close()
        cbKuljettaja.Items.Clear()


        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT SukuNimi, EtuNimi FROM Henkilosto WHERE tSuhdeVoimassa=0 ORDER BY SukuNimi ASC"
            '  .Parameters.AddWithValue("@id", False)

        End With
        Try
            MyYhteys.Open()

            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                While rd.Read
                    cbKuljettaja.Items.Add(rd.GetValue(0) & " " & rd.GetValue(1))
                End While
            End If
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE Pysakointihistoria lataa kuljettajatCB", ErrorToString, 0, 0)
            Err.Clear()

        End Try
        MyYhteys.Close()

    End Sub

    Private Sub rbKaikki_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbKaikki.CheckedChanged
        If rbKaikki.Checked = True Then
            LataaTunnuksetDataKenttaan()

        End If
    End Sub

    Private Sub rbKuljettaja_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbKuljettaja.CheckedChanged
        If rbKuljettaja.Checked = True Then
            cbKuljettaja.Visible = True
            cbAuto.Visible = False
            pvm.Visible = False

        End If
    End Sub

    Private Sub cbKuljettaja_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbKuljettaja.SelectedIndexChanged
        If cbKuljettaja.Text = "" Then Exit Sub

        Try
            myYhteys.Close()
            myYhteys.Open()

            Dim tbAdapter As New MySqlDataAdapter()

            With tbAdapter
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
                .SelectCommand.CommandText = "SELECT PysakointiUUSI.ID, Kalusto.RekNro, PysakointiUUSI.Alue, PysakointiUUSI.Aika, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Kuljettaja, PysakointiUUSI.Otettu FROM PysakointiUUSI " & _
                 "INNER JOIN Kalusto ON Kalusto.AutoNro=PysakointiUUSI.AutoNro " & _
                  "INNER JOIN Henkilosto ON Henkilosto.HloNro=PysakointiUUSI.HloNro " & _
                  "WHERE PysakointiUUSI.HloNro=@HloNro " & _
            "ORDER BY PysakointiUUSI.ID DESC LIMIT 100"
                '     "AND Henkilosto.HloNro=Pysakointi.JattanytHlo " & _
                .SelectCommand.Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(cbKuljettaja.Text))

            End With
            Dim myDataSet As DataSet = New DataSet()

            tbAdapter.Fill(myDataSet, "PysakointiUUSI")

            Dim myDataView = New DataView(myDataSet.Tables("PysakointiUUSI"))
            DGW.AutoGenerateColumns = True
            DGW.DataSource = myDataSet
            DGW.DataMember = "PysakointiUUSI"
            myYhteys.Close()
            '  DGW.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE Pysäköintihistoria lajittele kuljettajan mukaan", ErrorToString, 0, 0)
            Err.Clear()

        End Try

    End Sub

    Private Sub cbAuto_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbAuto.SelectedIndexChanged
        If cbAuto.Text = "" Then Exit Sub

        Try
            myYhteys.Close()
            myYhteys.Open()

            Dim tbAdapter As New MySqlDataAdapter()

            With tbAdapter
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
                .SelectCommand.CommandText = "SELECT PysakointiUUSI.ID, Kalusto.RekNro, PysakointiUUSI.Alue, PysakointiUUSI.Aika, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Kuljettaja, PysakointiUUSI.Otettu FROM PysakointiUUSI " & _
                 "INNER JOIN Kalusto ON Kalusto.AutoNro=PysakointiUUSI.AutoNro " & _
                  "INNER JOIN Henkilosto ON Henkilosto.HloNro=PysakointiUUSI.HloNro " & _
                  "WHERE PysakointiUUSI.AutoNro=@AutoNro " & _
            "ORDER BY PysakointiUUSI.ID DESC LIMIT 100 "
                '     "AND Henkilosto.HloNro=Pysakointi.JattanytHlo " & _
                .SelectCommand.Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbAuto.Text))

            End With
            Dim myDataSet As DataSet = New DataSet()

            tbAdapter.Fill(myDataSet, "PysakointiUUSI")

            Dim myDataView = New DataView(myDataSet.Tables("PysakointiUUSI"))
            DGW.AutoGenerateColumns = True
            DGW.DataSource = myDataSet
            DGW.DataMember = "PysakointiUUSI"
            myYhteys.Close()
            '  DGW.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE pysäköntihistoria lajittele auton mukaan", ErrorToString, 0, 0)
            Err.Clear()

        End Try
    End Sub

    Private Sub rbAuto_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbAuto.CheckedChanged
        If rbAuto.Checked = True Then
            cbAuto.Visible = True
            cbKuljettaja.Visible = False
            pvm.Visible = False

        End If
    End Sub

    Private Sub rbPaiva_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbPaiva.CheckedChanged
        If rbPaiva.Checked = True Then
            cbAuto.Visible = False
            cbKuljettaja.Visible = False
            pvm.Visible = True

        End If
    End Sub

    Private Sub pvm_ValueChanged(sender As System.Object, e As System.EventArgs) Handles pvm.ValueChanged
      
        Try
            myYhteys.Close()
            myYhteys.Open()
            Dim dAl As Date = Format(pvm.Value, "\#yyyy\-MM\-dd\#")

            Dim tbAdapter As New MySqlDataAdapter()

            With tbAdapter
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
                .SelectCommand.CommandText = "SELECT PysakointiUUSI.ID, Kalusto.RekNro, PysakointiUUSI.Alue, PysakointiUUSI.Aika, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Kuljettaja, PysakointiUUSI.Otettu FROM PysakointiUUSI " & _
                 "INNER JOIN Kalusto ON Kalusto.AutoNro=PysakointiUUSI.AutoNro " & _
                  "INNER JOIN Henkilosto ON Henkilosto.HloNro=PysakointiUUSI.HloNro " & _
                  "WHERE DATE(PysakointiUUSI.Aika)=@Aika " & _
            "ORDER BY PysakointiUUSI.ID DESC LIMIT 100"
                '     "AND Henkilosto.HloNro=Pysakointi.JattanytHlo " & _
                .SelectCommand.Parameters.AddWithValue("@Aika", dAl)

            End With
            Dim myDataSet As DataSet = New DataSet()

            tbAdapter.Fill(myDataSet, "PysakointiUUSI")

            Dim myDataView = New DataView(myDataSet.Tables("PysakointiUUSI"))
            DGW.AutoGenerateColumns = True
            DGW.DataSource = myDataSet
            DGW.DataMember = "PysakointiUUSI"
            myYhteys.Close()
            '  DGW.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE Pysäköintihistoria - PVM vaihtunut", ErrorToString, 0, 0)
            Err.Clear()

        End Try
    End Sub
End Class