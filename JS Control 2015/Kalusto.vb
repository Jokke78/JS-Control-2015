Imports MySql.Data.MySqlClient

Public Class Kalusto
    Private myYhteys As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection3 As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection2 As MySqlConnection = New MySqlConnection(serverString)
    Private estaAutomaattinenMallienhaku As Boolean
    Public AutoMaattinenTallennus As Boolean
    Public Sub LataaKuljettajatCBhen()
        myYhteys.Close()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT CONCAT(SukuNimi, ' ', EtuNimi) As _Kuljettaja FROM Henkilosto WHERE tSuhdeVoimassa=0 ORDER BY SukuNimi ASC"
            '  .Parameters.AddWithValue("@id", False)

        End With
        '   Try
        myYhteys.Open()

        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then
            While rd.Read
                vVastuuKuli.Items.Add(rd.GetValue(0))
            End While
        End If
        '  Catch ex As Exception
        'Err.Clear()

        '  End Try
        myYhteys.Close()

    End Sub
    Private Sub DGW_RowHeaderMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGW.RowHeaderMouseClick

        myYhteys.Close()

        Dim i As Integer
        i = DGW.CurrentRow.Index

        Dim x As Integer = DGW.Item(0, i).Value
        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT AutoNro, RekNro, RekistPVM, Vastuukuljettaja, KesaRenkaat, KatsastusVahti, Raporteissa FROM Kalusto WHERE AutoNro=@id"
            .Parameters.AddWithValue("@id", x)

        End With
        myYhteys.Open()
        Try
            Dim rd As MySqlDataReader = cmd.ExecuteReader

            If rd.HasRows = True Then
                rd.Read()

                vAutoNro.Text = rd.GetValue(0)
                vRekkari.Text = rd.GetString(1)
                vRekPVM.Value = rd.GetValue(2) : piilorekistPVM.Text = FormatDateTime(rd.GetValue(2), DateFormat.ShortDate).ToString
                vVastuuKuli.Text = PalautaSQLNrostaNimi(rd.GetValue(3))

                Select Case rd.GetValue(4)
                    Case 1
                        vTalvi.Checked = True
                    Case 0
                        vKesa.Checked = True
                    Case 2
                        vNastat.Checked = True

                End Select

                Select Case rd.GetValue(5)
                    Case False
                        vKatsVahtiEI.Checked = True
                    Case True
                        vKatsVahtiKYLLA.Checked = True
                End Select
                Select Case rd.GetValue(6)
                    Case False
                        raporteissaEI.Checked = True
                    Case True
                        raporteissakylla.Checked = True
                End Select

            End If

        Catch ex As Exception
            Err.Clear()
        End Try

        myYhteys.Close()
        '      DGW.Enabled = False



    End Sub
    Public Sub LataaTunnuksetDataKenttaan()


        myYhteys.Close()
        myYhteys.Open()

        Dim tbAdapter As New MySqlDataAdapter()

        With tbAdapter
            .SelectCommand = New MySqlCommand
            .SelectCommand.Connection = myYhteys
            If TSkaikki.Checked = True Then .SelectCommand.CommandText = "SELECT Kalusto.AutoNro, Kalusto.RekNro, RengasTyyppi.Rengas, Kalusto.RekistPVM, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Vastuukuljettaja, KatsastuksienSeuranta.SeuraavaKatsastusPVM, Kalusto.KatsastusVahti, Kalusto.Raporteissa FROM Kalusto " &
                "INNER JOIN Henkilosto ON Kalusto.VastuuKuljettaja = Henkilosto.HloNro " &
                "INNER JOIN RengasTyyppi ON Kalusto.KesaRenkaat=RengasTyyppi.ID " &
                "INNER JOIN KatsastuksienSeuranta ON Kalusto.AutoNro = KatsastuksienSeuranta.AutoNro " &
                "ORDER BY Kalusto.AutoNro ASC"

            If TSvoimassa.Checked = True Then .SelectCommand.CommandText = "SELECT Kalusto.AutoNro, Kalusto.RekNro, RengasTyyppi.Rengas, Kalusto.RekistPVM, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Vastuukuljettaja, KatsastuksienSeuranta.SeuraavaKatsastusPVM, Kalusto.KatsastusVahti, Kalusto.Raporteissa FROM Kalusto " &
                  "INNER JOIN Henkilosto ON Kalusto.VastuuKuljettaja = Henkilosto.HloNro " &
                "INNER JOIN RengasTyyppi ON Kalusto.KesaRenkaat=RengasTyyppi.ID " &
                  "INNER JOIN KatsastuksienSeuranta ON Kalusto.AutoNro = KatsastuksienSeuranta.AutoNro " &
                  "WHERE Kalusto.Raporteissa='1' ORDER BY Kalusto.AutoNro ASC"
            If TSEIKYLLÄ.Checked = True Then .SelectCommand.CommandText = "SELECT Kalusto.AutoNro, Kalusto.RekNro, RengasTyyppi.Rengas, Kalusto.RekistPVM, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Vastuukuljettaja, KatsastuksienSeuranta.SeuraavaKatsastusPVM, Kalusto.KatsastusVahti, Kalusto.Raporteissa FROM Kalusto " &
                    "INNER JOIN Henkilosto ON Kalusto.VastuuKuljettaja = Henkilosto.HloNro " &
                "INNER JOIN RengasTyyppi ON Kalusto.KesaRenkaat=RengasTyyppi.ID " &
                    "INNER JOIN KatsastuksienSeuranta ON Kalusto.AutoNro = KatsastuksienSeuranta.AutoNro " &
                    "WHERE Kalusto.Raporteissa='0' ORDER BY Kalusto.AutoNro ASC"

        End With
        Dim myDataSet As DataSet = New DataSet()

        tbAdapter.Fill(myDataSet, "Kalusto")

        Dim myDataView = New DataView(myDataSet.Tables("Kalusto"))
        DGW.AutoGenerateColumns = True
        DGW.DataSource = myDataSet
        DGW.DataMember = "Kalusto"
        myYhteys.Close()

        DGW.Columns(0).Width = 90
        DGW.Columns(1).Width = 80
        DGW.Columns(2).HeaderText = "KesäRenk.."
        DGW.Columns(2).Width = 80

        DGW.Columns(3).Width = 110
        '     DGW.Columns(4).HeaderText = "Vastuukuljett"
        DGW.Columns(4).Width = 200
        DGW.Columns(5).Width = 150
        DGW.Columns(6).HeaderText = "Katsastusvahti"
        DGW.Columns(6).Width = 120
        DGW.Columns(7).HeaderText = "Raporteissa"
        DGW.Columns(7).Width = 150


    End Sub

    Private Sub Kalusto_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        LataaTunnuksetDataKenttaan()
        LataaKuljettajatCBhen()

        UusiRekistPVM.Value = Today

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs)
        DGW.Enabled = True

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        myYhteys.Close()


        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "UPDATE Kalusto SET VastuuKuljettaja=@SopimusID, KatsastusVahti=@Nimi, RekistPVM=@Osoite, KesaRenkaat=@Lyhenne, Raporteissa=@Raporteissa WHERE AutoNro=@id"
            .Parameters.AddWithValue("@SopimusID", PalautaSQLNroNimiesta(vVastuuKuli.Text))
            If vKatsVahtiKYLLA.Checked = True Then .Parameters.AddWithValue("@Nimi", True)
            If vKatsVahtiEI.Checked = True Then .Parameters.AddWithValue("@Nimi", False)

            .Parameters.AddWithValue("@Osoite", CType(vRekPVM.Value, Date))
            If vKesa.Checked = True Then .Parameters.AddWithValue("@Lyhenne", 0)
            If vTalvi.Checked = True Then .Parameters.AddWithValue("@Lyhenne", 1)
            If vNastat.Checked = True Then .Parameters.AddWithValue("@Lyhenne", 2)

            If raporteissakylla.Checked = False Then .Parameters.AddWithValue("@Raporteissa", False)
            If raporteissakylla.Checked = True Then .Parameters.AddWithValue("@Raporteissa", True)

            .Parameters.AddWithValue("id", Val(vAutoNro.Text))
        End With
        myYhteys.Open()
        '   Try
        cmd.ExecuteNonQuery()

        '  Catch ex As Exception
        '      Err.Clear()
        '      MsgBox("Tallentaminen ei onnistunut")
        '     Exit Sub

        '  End Try
        myYhteys.Close()
        If piilorekistPVM.Text <> FormatDateTime(vRekPVM.Value, DateFormat.ShortDate).ToString Then
            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "UPDATE KatsastuksienSeuranta SET PVM=@pvm, SeuraavaKatsastusPVM=@seuraava WHERE AutoNro=@AutoNro"
                .Parameters.AddWithValue("@pvm", vRekPVM.Value)
                .Parameters.AddWithValue("@seuraava", vRekPVM.Value.AddYears(1))
                .Parameters.AddWithValue("@AutoNro", Val(vAutoNro.Text))

            End With
            myYhteys.Open()
            Try
                cmd2.ExecuteNonQuery()

            Catch ex As Exception
                Err.Clear()

            End Try
            myYhteys.Close()

        End If

        LataaTunnuksetDataKenttaan()
        DGW.Enabled = True
        'tallennetaan muuttunut katsastuspvm myös katsastuksien seurantaan
        myYhteys.Close()

        MsgBox("Muutokset tallennettu")


    End Sub


    Private Sub DGW_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGW.CellContentClick

    End Sub




    Private Sub TSkaikki_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles TSkaikki.CheckedChanged, TSvoimassa.CheckedChanged, TSEIKYLLÄ.CheckedChanged
        '      LataaTunnuksetDataKenttaan()
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        LataaTunnuksetDataKenttaan()
        DGW.Enabled = True

    End Sub

    Private Sub UuusiAutoNro_TextChanged(sender As Object, e As EventArgs) Handles UuusiAutoNro.TextChanged

    End Sub

    Private Sub UuusiAutoNro_Leave(sender As Object, e As EventArgs) Handles UuusiAutoNro.Leave
        If UuusiAutoNro.Text = "" Then Exit Sub

        myYhteys.Close()

        Dim cmd As New MySqlCommand
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Kalusto WHERE AutoNro=@Auto"
            .Parameters.AddWithValue("@Auto", UuusiAutoNro.Text)
        End With
        myYhteys.Open()

        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then

            MsgBox("Autonumero on jo käytössä")
            UuusiAutoNro.Text = ""
            UuusiAutoNro.Focus()
            myYhteys.Close()
            rd.Dispose()

            Exit Sub


        End If
        rd.Dispose()
        myYhteys.Close()

        If UusiRekNro.Text <> "" Then
            btnLisaaUusiAuto.Visible = True
        Else
            btnLisaaUusiAuto.Visible = False

        End If


    End Sub

    Private Sub UusiRekNro_TextChanged(sender As Object, e As EventArgs) Handles UusiRekNro.TextChanged

    End Sub

    Private Sub UusiRekNro_Leave(sender As Object, e As EventArgs) Handles UusiRekNro.Leave
        If UuusiAutoNro.Text <> "" And UusiRekistPVM.Text <> "" Then
            btnLisaaUusiAuto.Visible = True

        Else
            btnLisaaUusiAuto.Visible = False


        End If
    End Sub

    Private Sub btnLisaaUusiAuto_Click(sender As Object, e As EventArgs) Handles btnLisaaUusiAuto.Click
        myYhteys.Close()

        Dim cmd_kalusto As New MySqlCommand
        With cmd_kalusto
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO Kalusto (AutoNro, RekNro, RekistPVM) VALUES(@AutoNro, @RekNro, @RekistPVM)"
            .Parameters.AddWithValue("@AutoNro", Val(UuusiAutoNro.Text))
            .Parameters.AddWithValue("@RekNro", UusiRekNro.Text)
            .Parameters.AddWithValue("@RekistPVM", UusiRekistPVM.Value)
        End With
        myYhteys.Open()
        cmd_kalusto.ExecuteNonQuery()
        myYhteys.Close()

        Dim cmd_lisaT As New MySqlCommand()
        With cmd_lisaT
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO KalustoLisaTiedot (AutoNro) VALUES(@AutoNro)"
            .Parameters.AddWithValue("@AutoNro", Val(UuusiAutoNro.Text))
        End With
        myYhteys.Open()
        cmd_lisaT.ExecuteNonQuery()
        myYhteys.Close()

        Dim cmd_katsastus As New MySqlCommand()
        With cmd_katsastus
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO KatsastuksienSeuranta (AutoNro, HloNro, PVM, SeuraavaKatsastusPVM) VALUES(@AutoNro, @HloNro, @PVM, @SeuraavaKatsastusPVM)"
            .Parameters.AddWithValue("@AutoNro", Val(UuusiAutoNro.Text))
            .Parameters.AddWithValue("@HloNro", Kayttaja.HloNro)
            .Parameters.AddWithValue("@PVM", Today)
            .Parameters.AddWithValue("@SeuraavaKatsastusPVM", UusiRekistPVM.Value.AddYears(1))

        End With

        myYhteys.Open()
        cmd_katsastus.ExecuteNonQuery()
        myYhteys.Close()
        LataaTunnuksetDataKenttaan()


        MsgBox("LISÄTTY UUSI AUTO " & UuusiAutoNro.Text & " (" & UusiRekNro.Text & ")")


        UuusiAutoNro.Text = ""
        UusiRekNro.Text = ""






    End Sub
End Class