Imports MySql.Data.MySqlClient

Public Class ViewTallennaPesuilmoitus
    Private TbConnection3 As MySqlConnection = New MySqlConnection(serverString)

    Private Sub ViewTallennaPesuilmoitus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.Default
        HaeIlmoitetutPesut()
        LataaAutotCBhen()



        'TARKISTETAAN KULJETTAJAN KO PÄIVÄN TYÖVUORO JA VALITAAN AUTOKSI KO AUTO

        TbConnection3.Close()
        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection3
            .CommandType = CommandType.Text
            .CommandText = "SELECT HloNro, AutoNro FROM AjetutVuorot WHERE (AlkuPVM = @AlkuPVM) AND (HloNro = @HloNro)"
            '   cmd.Parameters.AddWithValue("@ALkuPVM", FormatDateTime(NaytettavaPVM.Value, DateFormat.ShortDate))
            .Parameters.AddWithValue("@AlkuPVM", Today)
            .Parameters.AddWithValue("@HloNro", KayttajaHloNro)

        End With
        TbConnection3.Open()
        Try
            Dim rd As MySqlDataReader = cmd.ExecuteReader

            If rd.HasRows = True Then
                rd.Read()
                cbAuto.Text = PalautaSQLRekkariNumerosta(rd.GetValue(1))

            End If

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE view tallenna pesuilmoitu", ErrorToString, 0, 0)
            Err.Clear()

        Finally

            TbConnection3.Close()

        End Try

    End Sub
    Public Sub LataaAutotCBhen()
        Try
            TbConnection3.Close()

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection3
                .CommandType = CommandType.Text
                .CommandText = "SELECT RekNro FROM Kalusto WHERE Raporteissa='1' ORDER BY RekNro ASC"

            End With
            TbConnection3.Open()

            Dim re As MySqlDataReader = cmd.ExecuteReader
            Try
                While re.Read
                    If re.GetString(0) <> "xxxxxx" Then cbAuto.Items.Add(re.GetString(0))
                End While

            Catch ex As Exception
                Err.Clear()

            End Try

            TbConnection3.Close()
            re.Close()
        Catch ex As Exception
            Err.Clear()

        End Try


    End Sub
    Public Sub HaeIlmoitetutPesut()
        Try
            TbConnection3.Close()
            Dim DA As New MySqlDataAdapter()
            DA.SelectCommand = New MySqlCommand()
            DA.SelectCommand.Connection = TbConnection3
            DA.SelectCommand.CommandText = "SELECT Kalusto.RekNro, Pesuilmoitukset.Pesuaika, Pesuilmoitukset.OmaAuto FROM Pesuilmoitukset " & _
                                    "INNER JOIN Kalusto ON Pesuilmoitukset.AutoNro = Kalusto.AutoNro " & _
                                "WHERE Pesuilmoitukset.HloNro = @HloNro ORDER BY Pesuaika DESC"

            DA.SelectCommand.Parameters.AddWithValue("@HloNro", KayttajaHloNro)
            '      DA.SelectCommand.Parameters.AddWithValue("@Korjattu", False)

            Dim ds As New DataSet
            TbConnection3.Open()

            DA.Fill(ds, "Pesuilmoitukset")

            DGV.DataSource = ds
            DGV.DataMember = "Pesuilmoitukset"

            DGV.Columns(0).HeaderText = "Rek-Nro"
            DGV.Columns(1).HeaderText = "Pesuaika"
            DGV.Columns(2).HeaderText = "Oma auto"


            DGV.Columns(0).Width = 130 'AutoSizeMode.AllCells = True
            DGV.Columns(1).Width = 350
            DGV.Columns(2).Width = 80

            TbConnection3.Close()

            '      PesujaYht.Text = DGV.RowCount - 1.ToString


        Catch ex As Exception
            Err.Clear()

        End Try


    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If cbAuto.Text = "" Then MsgBox("Valitse auto") : Exit Sub


        TallennaPesuilmoitus(PalautaSQLNumeroRekkarista(cbAuto.Text), KayttajaHloNro, Now, "", True)

        MsgBox("Tallennettu pesuilmoitus")

        HaeIlmoitetutPesut()

    End Sub
End Class