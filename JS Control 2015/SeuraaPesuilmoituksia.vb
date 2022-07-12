Imports MySql.Data.MySqlClient

Public Class SeuraaPesuilmoituksia
    Private MyYhteys As MySqlConnection = New MySqlConnection(serverString)
    Private MyYhteys2 As MySqlConnection = New MySqlConnection(serverString)

    Private Sub SeuraaPesuilmoituksia_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LataaTunnuksetDataKenttaan()
    End Sub
    Public Sub LataaTunnuksetDataKenttaan()

        Try
            LokiTapahtumanTallennus(KayttajaHloNro, "Avattu pesuilmoituksien seuranta", pvm.Value.ToString, 0, 0)

            MyYhteys.Close()
            MyYhteys.Open()

            Dim tbAdapter As New MySqlDataAdapter()
            Dim dAl As Date = Format(pvm.Value, "\#yyyy\-MM\-dd\#")

            With tbAdapter
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = MyYhteys
                .SelectCommand.CommandText = "SELECT Pesuilmoitukset.Pesuaika, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Kuljettaja, Kalusto.RekNro, Pesuilmoitukset.Tietokoneelta, Pesuilmoitukset.OmaAuto FROM Pesuilmoitukset " & _
                    "INNER JOIN Henkilosto ON Henkilosto.HloNro=Pesuilmoitukset.HloNro " & _
                         "INNER JOIN Kalusto ON Kalusto.AutoNro=Pesuilmoitukset.AutoNro " & _
                    "WHERE DATE(Pesuilmoitukset.Pesuaika)=@PVM " & _
                "ORDER BY Pesuilmoitukset.Pesuaika DESC"
                .SelectCommand.Parameters.AddWithValue("@PVM", dAl)


            End With
            Dim myDataSet As DataSet = New DataSet()

            tbAdapter.Fill(myDataSet, "Pesuilmoitukset")

            Dim myDataView = New DataView(myDataSet.Tables("Pesuilmoitukset"))
            DGW.AutoGenerateColumns = True
            DGW.DataSource = myDataSet
            DGW.DataMember = "Pesuilmoitukset"
            MyYhteys.Close()
            '   DGW.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            '  DGW.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

            DGW.Columns(0).Width = 140
            DGW.Columns(0).HeaderText = "Pesuaika"
            DGW.Columns(1).Width = 200
            DGW.Columns(1).HeaderText = "Kuljettaja"
            '   DGW.Columns(2).Width = 140
            '  DGW.Columns(2).HeaderText = "Etunimi"

            DGW.Columns(2).Width = 90
            DGW.Columns(2).HeaderText = "REK-NRO"

            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT COUNT(*) As Lasku FROM Pesuilmoitukset WHERE DATE(Pesuaika)=@PVM"
                .Parameters.AddWithValue("@PVM", dAl)


            End With
            MyYhteys.Open()
            Label1.Text = "KIRJATUT PESUILMOITUKSET " & cmd2.ExecuteScalar
            MyYhteys.Close()

        Catch ex As Exception
            Err.Clear()

        End Try
     


    End Sub

    Private Sub pvm_ValueChanged(sender As System.Object, e As System.EventArgs) Handles pvm.ValueChanged
        LataaTunnuksetDataKenttaan()


    End Sub
End Class