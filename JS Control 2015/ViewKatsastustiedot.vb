Imports MySql.Data.MySqlClient

Public Class ViewKatsastustiedot
    Private myYhteys As MySqlConnection = New MySqlConnection(serverString)

    Private Sub ViewKatsastustiedot_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LataaTunnuksetDataKenttaan()
    End Sub

    Public Sub LataaTunnuksetDataKenttaan()
        Try
            myYhteys.Close()
            myYhteys.Open()

            Dim tbAdapter As New MySqlDataAdapter()

            With tbAdapter
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
                .SelectCommand.CommandText = "SELECT Kalusto.AutoNro, Kalusto.RekNro, Kalusto.RekistPVM, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Vastuukuljettaja, KatsastuksienSeuranta.SeuraavaKatsastusPVM, KatsastuksienSeuranta.AjoaikaaAsti, KatsastuksienSeuranta.PVM As Katsastettu FROM Kalusto " & _
             "INNER JOIN KatsastuksienSeuranta ON KatsastuksienSeuranta.AutoNro=Kalusto.AutoNro " & _
             "INNER JOIN Henkilosto ON Kalusto.VastuuKuljettaja=Henkilosto.HloNro " & _
             "WHERE Kalusto.Raporteissa='1' " & _
             "ORDER BY Kalusto.RekNro ASC "


            End With
            Dim myDataSet As DataSet = New DataSet()

            tbAdapter.Fill(myDataSet, "Kalusto")

            Dim myDataView = New DataView(myDataSet.Tables("Kalusto"))
            DGW.AutoGenerateColumns = True
            DGW.DataSource = myDataSet
            DGW.DataMember = "Kalusto"
            myYhteys.Close()
            DGW.AutoResizeColumns()
        Catch ex As Exception
            Err.Clear()

        End Try


    End Sub


End Class