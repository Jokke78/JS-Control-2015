Imports MySql.Data.MySqlClient

Public Class RyhmaSMSviestienLahettaminen
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)
    Private Myyhteys As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection99 As MySqlConnection = New MySqlConnection(serverString)

    Private Sub RyhmaSMSviestienLahettaminen_Leave(sender As Object, e As System.EventArgs) Handles Me.Leave
        '    Me.Close()

    End Sub

    Private Sub RyhmaSMSviestienLahettaminen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        nimet.DisplayMember = "Nimi"
        VastaanOttajat.DisplayMember = "Nimi"
        lataaOsastot()
    End Sub
    Public Sub lataaOsastot()
        myYhteys.Close()
        cbOsasto.Items.Clear()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT Nimike FROM Osastot ORDER BY Nimike ASC"
            '           .Parameters.AddWithValue("@e", "E")
            '        .Parameters.AddWithValue("@j", "J")
            '

        End With
        '   Try
        myYhteys.Open()

        Dim rd As MySqlDataReader = cmd.ExecuteReader
        If rd.HasRows = True Then
            While rd.Read
                cbOsasto.Items.Add(rd.GetValue(0))
            End While
        End If
        '  Catch ex As Exception
        'Err.Clear()

        '  End Try
        myYhteys.Close()

    End Sub
    Public Structure listrivi
        Public Property Nimi As String
        Public Property PuhNro As String
    End Structure

    Private Sub osasto_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbosasto.SelectedIndexChanged
        nimet.Items.Clear()

        Dim dAl As Date = Format(Today, "\#yyyy\-MM\-dd\#")

        TbConnection.Close()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT Henkilosto.SukuNimi, Henkilosto.Etunimi, PuhelinNrot.Numero FROM Henkilosto " & _
         "INNER JOIN PuhelinNrot ON Henkilosto.HloNro = PuhelinNrot.HloNro " & _
         "WHERE (@PVM BETWEEN PuhelinNrot.AlkaenPVM AND PuhelinNrot.AstiPVM) AND LahetetaanSMS = 1 AND Henkilosto.tSuhdeVoimassa='0' AND Henkilosto.OsastoID=@Osasto"
            .Parameters.AddWithValue("@PVM", dal)
            .Parameters.AddWithValue("@Osasto", PalautaOsastoNimestaID(cbosasto.Text))
        End With

        TbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader

        If rd.HasRows = True Then

            While rd.Read
                Dim uusiLB As New listrivi
                uusiLB.Nimi = rd.GetString(0) & " " & rd.GetString(1)
                uusiLB.PuhNro = rd.GetString(2)

                nimet.Items.Add(uusiLB)

            End While

        End If

        TbConnection.Close()
        rd.Close()





    End Sub
    Public Function PalautaOsastoNimestaID(ByVal nro As String) As String
        Dim nimi As Integer = 0

        Try
            TbConnection99.Close()
            Dim cmd As New MySqlCommand()
            cmd.Connection = TbConnection99
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "SELECT OsastoID FROM Osastot WHERE Nimike=@Nro"
            cmd.Parameters.AddWithValue("@Nro", nro)
            TbConnection99.Open()
            Try
                Dim rdA As MySqlDataReader = cmd.ExecuteReader
                If rdA.HasRows = True Then
                    rdA.Read()
                    nimi = rdA.GetValue(0)

                End If
            Catch ex As Exception
                Err.Clear()
                nimi = 0
            End Try

            TbConnection99.Close()

        Catch ex As Exception
            Err.Clear()
            TbConnection99.Close()

        End Try



loppu:

        Return nimi

    End Function
    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click

        For Each item In nimet.SelectedItems
            VastaanOttajat.Items.Add(item)

            '     nimet.Items.Remove(item)

        Next
        VastaanOttajat.Refresh()

        '     DeDupeList(VastaanOttajat)


    End Sub
    Public Sub DeDupeList(ByVal List As ListBox)
        On Error Resume Next
        Dim lstCollection As New Collection
        Dim i As Long
        For i = 0 To List.Items.Count - 1
            Dim haku As listrivi
            haku = List.Items(i)
            lstCollection.Add(haku.Nimi, haku.PuhNro)
        Next i
        List.Items.Clear()
        For i = 1 To lstCollection.Count
            List.Items.Add(lstCollection.Item(i))
        Next i
    End Sub

    Private Sub VastaanOttajat_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles VastaanOttajat.MouseDoubleClick
        If VastaanOttajat.SelectedItem Is Nothing Then Exit Sub

        VastaanOttajat.Items.Remove(VastaanOttajat.SelectedItem)

    End Sub

    Private Sub VastaanOttajat_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles VastaanOttajat.SelectedIndexChanged

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        For Each item In VastaanOttajat.Items
            Dim sis As listrivi = item

            '  MsgBox("Viesti: " & ViestiToGo.Text & " [" & sis.PuhNro & "] HloNro " & PalautaSQLNroNimiesta(sis.Nimi))
            TbConnection.Close()
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO SMStoiminta " & _
                    "(Kasitelty, LahettajanPuhNro, Viesti, OdottaaLahettamista, ViestinTekijaHloNro, SaapunutAika) " & _
                    "VALUES(@Kasitelty, @LahettajanPuhNro, @Viesti, @OdottaaLahettamista, @ViestinTekijaHloNro, @SaapunutAika)"
                .Parameters.AddWithValue("@Kasitelty", True)
                .Parameters.AddWithValue("@LahettajanPuhNro", sis.PuhNro)
                .Parameters.AddWithValue("@Viesti", ViestiToGo.Text)
                .Parameters.AddWithValue("@OdottaaLahettamista", True)
                .Parameters.AddWithValue("@ViestinTekijaHloNro", KayttajaHloNro)
                .Parameters.AddWithValue("@SaapunutAika", CType(Now, DateTime))



            End With
            TbConnection.Open()
            cmd.ExecuteNonQuery()
            TbConnection.Close()

        Next

        LokiTapahtumanTallennus(KayttajaHloNro, "Lähetetty " & VastaanOttajat.Items.Count.ToString & " henkilölle ryhmäviesti", ViestiToGo.Text, 0, 0)

        MsgBox("Lähetetty " & VastaanOttajat.Items.Count.ToString & " viestiä")
        ViestiToGo.Text = ""
        VastaanOttajat.Items.Clear()

    End Sub


End Class