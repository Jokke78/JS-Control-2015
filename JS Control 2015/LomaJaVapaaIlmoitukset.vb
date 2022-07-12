Imports MySql.Data.MySqlClient

Public Class LomaJaVapaaIlmoitukset
    Private myYhteys As MySqlConnection = New MySqlConnection(serverString)

    Private Sub LomaJaVapaaIlmoitukset_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        AlkuPVM.MinDate = Today
        LoppuPVM.MinDate = Today
        txtKuljettaja.Text = KayttajaNimi
        LataaTunnuksetDataKenttaan()
        rbVapaa.Checked = True

    End Sub
    Public Sub LataaTunnuksetDataKenttaan()

        Try
            myYhteys.Close()
            myYhteys.Open()

            Dim tbAdapter As New MySqlDataAdapter()

            With tbAdapter
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys '
                .SelectCommand.CommandText = "SELECT Lomat.AlkaenPVM, Lomat.AstiPVM, LomaTyypit.Nimike FROM Lomat " & _
     "LEFT JOIN LomaTyypit ON Lomat.LomaTyyppi=LomaTyypit.LomaTyyppi " & _
     "WHERE HloNro=@HloNro AND (Lomat.LomaTyyppi BETWEEN '2' AND '24') " & _
"ORDER BY Lomat.AlkaenPVM DESC" ' WHERE Henkilosto.TsuhdeVoimassa='0' ORDER BY Henkilosto.SukuNimi ASC 

                '                .SelectCommand.CommandText = "SELECT Lomat.AlkaenPVM, Lomat.AstiPVM, LomaTyypit.Nimike, Lomat.Tarkennus, Lomat.Perustelu FROM Lomat " & _
                '                  "LEFT JOIN LomaTyypit ON Lomat.LomaTyyppi=LomaTyypit.LomaTyyppi " & _
                '                  "WHERE HloNro=@HloNro AND (Lomat.LomaTyyppi BETWEEN '2' AND '24') " & _
                '            "ORDER BY Lomat.AlkaenPVM DESC" ' WHERE Henkilosto.TsuhdeVoimassa='0' ORDER BY Henkilosto.SukuNimi ASC 
                .SelectCommand.Parameters.AddWithValue("@HloNro", KayttajaHloNro)

            End With
            Dim myDataSet As DataSet = New DataSet()

            tbAdapter.Fill(myDataSet, "Lomat")

            Dim myDataView = New DataView(myDataSet.Tables("Lomat"))
            DGW.AutoGenerateColumns = True
            DGW.DataSource = myDataSet
            DGW.DataMember = "Lomat"
            myYhteys.Close()
            '    DGW.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

            DGW.Columns(0).Width = 140
            DGW.Columns(1).Width = 140
            DGW.Columns(2).Width = 220
            '     DGW.Columns(3).Width = 200
            '    DGW.Columns(4).Width = 300

            '    DGW.Columns(3).HeaderText = "Osasto"
            '   DGW.Columns(3).Width = 160
            '  DGW.Columns(3).Width = 180
            ' DGW.Columns(4).Width = 50


            '       DGW.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            '      DGW.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        Catch ex As Exception
            Err.Clear()

        End Try


    End Sub

    Private Sub rbKesa_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rbKesa.CheckedChanged, rbTalvi.CheckedChanged, rbVapaa.CheckedChanged
        If rbKesa.Checked = False Then Exit Sub

        Me.Cursor = Cursors.WaitCursor

        Main.SC.Panel2.Controls.Clear()

        Try
            KesaLomatoiveidenIlmoittaminen.Close()

        Catch ex As Exception
            Err.Clear()

        End Try
        KesaLomatoiveidenIlmoittaminen.MdiParent = Main
        KesaLomatoiveidenIlmoittaminen.Dock = DockStyle.Fill

        KesaLomatoiveidenIlmoittaminen.Show()
        Main.SC.Panel2.Controls.Add(KesaLomatoiveidenIlmoittaminen)


        Me.Cursor = Cursors.Default
        'ViewKatsastustiedot.LataaTunnuksetDataKenttaan()

        Me.Dispose()




        '   If rbKesa.Checked = False And rbTalvi.Checked = False And rbVapaa.Checked = False Then
        'btnTallenna.Visible = False
        'Else
        'btnTallenna.Visible = True
        'If rbKesa.Checked = True Then LoppuPVM.Value = AlkuPVM.Value.AddDays(26)
        'If rbTalvi.Checked = True Then LoppuPVM.Value = AlkuPVM.Value.AddDays(6)
        'If rbVapaa.Checked = True Then LoppuPVM.Value = AlkuPVM.Value

        'End If
    End Sub

    Private Sub btnTallenna_Click(sender As System.Object, e As System.EventArgs) Handles btnTallenna.Click
        Main.TopMost = False

        If LoppuPVM.Value < AlkuPVM.Value Then MsgBox("Tarkista päivämäärien järjestys") : Main.TopMost = True : Exit Sub
        Main.TopMost = True

        Try
            myYhteys.Close()
            Dim cmd As New MySqlCommand
            With cmd
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO Lomat (HloNro, AlkaenPVM, AstiPVM, LomaTyyppi, Perustelu, Lisatty) VALUES(@HloNro, @AlkaenPVM, @AstiPVM, @LomaTyyppi, @Perustelu, @Lisatty)"
                .Parameters.AddWithValue("@HloNro", KayttajaHloNro)
                .Parameters.AddWithValue("@AlkaenPVM", AlkuPVM.Value)
                .Parameters.AddWithValue("@AstiPVM", LoppuPVM.Value)
                If rbVapaa.Checked = True Then .Parameters.AddWithValue("@LomaTyyppi", "12")
                If rbKesa.Checked = True Then .Parameters.AddWithValue("@LomaTyyppi", "13")
                If rbTalvi.Checked = True Then .Parameters.AddWithValue("@LomaTyyppi", "14")
                .Parameters.AddWithValue("@Perustelu", txtPerustelu.Text)
                .Parameters.AddWithValue("@Lisatty", Now)


            End With
            myYhteys.Open()
            cmd.ExecuteNonQuery()
            myYhteys.Close()
            Main.TopMost = False

            '********************* LISÄTÄÄN myös tieto informaatioo omalle esimiehelle ***************************


            Dim kulinEsimies As Integer = palautakuljettajanEsimies(PalautaSQLNroNimiesta(txtKuljettaja.Text))

            If kulinEsimies <> 0 Then
                Dim cmdX As New MySqlCommand()

                Dim valittupvmtxt As String = ""
                If FormatDateTime(AlkuPVM.Value, DateFormat.ShortDate) = FormatDateTime(LoppuPVM.Value, DateFormat.ShortDate) Then

                    valittupvmtxt = FormatDateTime(AlkuPVM.Value, DateFormat.ShortDate).ToString
                Else

                    valittupvmtxt = FormatDateTime(AlkuPVM.Value, DateFormat.ShortDate).ToString & "-" & FormatDateTime(LoppuPVM.Value, DateFormat.ShortDate).ToString
                End If

                With cmdX
                    .Connection = myYhteys
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO Informaatio (Teksti, OsoitettuHloNro, LisaajaHloNro, LisattyPVMklo) VALUES(@Teksti, @OsoitettuHloNro, @LisaajaHloNro, @LisattyPVMklo)"
                    .Parameters.AddWithValue("@Teksti", "VAPAATOIVE " & txtKuljettaja.Text & " " & valittupvmtxt)
                    .Parameters.AddWithValue("@OsoitettuHloNro", kulinEsimies)
                    .Parameters.AddWithValue("@LisaajaHloNro", KayttajaHloNro)
                    .Parameters.AddWithValue("@LisattyPVMklo", Now)

                End With
                myYhteys.Open()
                cmdX.ExecuteNonQuery()
                myYhteys.Close()

            End If




            '*****************************************************************************************************




            MsgBox("Toive on lisätty")
            Main.TopMost = True
            LataaTunnuksetDataKenttaan()
            NaytaTalvilomaToiveet.Close()

        Catch ex As Exception
            Err.Clear()

        End Try
    End Sub

    Private Sub AlkuPVM_ValueChanged(sender As System.Object, e As System.EventArgs) Handles AlkuPVM.ValueChanged
        If rbKesa.Checked = True Then LoppuPVM.Value = AlkuPVM.Value.AddDays(26)
        If rbTalvi.Checked = True Then LoppuPVM.Value = AlkuPVM.Value.AddDays(5)
        If rbVapaa.Checked = True Then LoppuPVM.Value = AlkuPVM.Value
        If LoppuPVM.Value < AlkuPVM.Value Then LoppuPVM.Value = AlkuPVM.Value

    End Sub
End Class