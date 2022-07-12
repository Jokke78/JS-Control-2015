
Imports MySql.Data.MySqlClient



Public Class ViewArrak
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection2 As MySqlConnection = New MySqlConnection(serverString)

    Private Sub ViewArrak_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.Default

        LataaKuljettajat()

        Kuli1.DisplayMember = "Nimi"
        Kuli2.DisplayMember = "Nimi"
        kuli3.DisplayMember = "Nimi"



    End Sub
    Public Structure listrivi
        Public Property Nimi As String
        Public Property PuhNro As String
    End Structure

    Public Sub LataaKuljettajat()
        '(@Alku BETWEEN AlkaenPVM AND AstiPVM)
        TbConnection.Close()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT Henkilosto.SukuNimi, Henkilosto.Etunimi, PuhelinNrot.Numero FROM Henkilosto " & _
           "INNER JOIN PuhelinNrot ON Henkilosto.HloNro = PuhelinNrot.HloNro " & _
           "WHERE (@PVM BETWEEN PuhelinNrot.AlkaenPVM AND PuhelinNrot.AstiPVM) AND LahetetaanSMS = 1 AND Henkilosto.tSuhdeVoimassa='0'"
            .Parameters.AddWithValue("@PVM", CType(Today, Date))
        End With

        TbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader

        If rd.HasRows = True Then

            While rd.Read
                Dim uusiLB As New listrivi
                uusiLB.Nimi = rd.GetString(0) & " " & rd.GetString(1)
                uusiLB.PuhNro = rd.GetString(2)

                Kuli1.Items.Add(uusiLB)
                Kuli2.Items.Add(uusiLB)
                kuli3.Items.Add(uusiLB)

            End While

        End If

        TbConnection.Close()
        rd.Close()


    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Try
            WebBrowser1.Document.GetElementById("name").SetAttribute("value", "hannu.virtanen")
            WebBrowser1.Document.GetElementById("password").SetAttribute("value", "no6yuc")
            WebBrowser1.Document.Forms(0).InvokeMember("submit")
            Timer1.Enabled = False
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Err.Clear()

        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If WebBrowser1.Url.ToString = "https://dtime.arrak.fi/dtime/dtime.dll/index.zml" Then
            Main.TopMost = False : MsgBox("Kirjaudu ohjelmasta ensin ulos")
            Main.TopMost = True : Exit Sub
            '
        End If

        Main.tview.Enabled = True
        Me.Close()


    End Sub
    Private Sub WebBrowser1_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        ' Prevent opening a new windows
        e.Cancel = True
    End Sub

    Private Sub TableLayoutPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim sis As listrivi = Kuli1.SelectedItem


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
            If btViikossa.Checked = True Then .Parameters.AddWithValue("@Viesti", "Digipiirturikorttisi tyhjennys tulisi suorittaa 7 vuorokauden kuluessa, kiitos.")
            If btHeti.Checked = True Then .Parameters.AddWithValue("@Viesti", "Digipiirturikorttisi tyhjennysaika on vanhentunut. Tyhjennys mahdollisimman pian, kiitos.")

            .Parameters.AddWithValue("@OdottaaLahettamista", True)
            .Parameters.AddWithValue("@ViestinTekijaHloNro", KayttajaHloNro)
            .Parameters.AddWithValue("@SaapunutAika", CType(Now, DateTime))



        End With
        TbConnection.Open()
        cmd.ExecuteNonQuery()
        TbConnection.Close()



        Main.TopMost = False
        MsgBox("Lähetetty SMS")
        Main.TopMost = True
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        Dim sis As listrivi = kuli3.SelectedItem


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
            .Parameters.AddWithValue("@Viesti", "Digipiirturikorttisi voimassaoloaika on loppumassa. Ota yhteys esimieheesi.")

            .Parameters.AddWithValue("@OdottaaLahettamista", True)
            .Parameters.AddWithValue("@ViestinTekijaHloNro", KayttajaHloNro)
            .Parameters.AddWithValue("@SaapunutAika", CType(Now, DateTime))



        End With
        TbConnection.Open()
        cmd.ExecuteNonQuery()
        TbConnection.Close()



        Main.TopMost = False
        MsgBox("Lähetetty SMS")
        Main.TopMost = True
    End Sub


    Private Sub Kuli1_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Kuli1.SelectedIndexChanged
        Dim sis As listrivi = Kuli1.SelectedItem

        seNumero.Text = sis.PuhNro
    End Sub
End Class