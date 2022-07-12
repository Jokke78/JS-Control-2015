Imports MySql.Data.MySqlClient

Public Class RekisteroiHloTagi
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)

    Private Sub RekisteroiHloTagi_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        HaeKuljettajatCBhen()
        LuettuViivakoodi2.Focus()

    End Sub
    Public Sub HaeKuljettajatCBhen()
        Dim cmd As New MySqlCommand()
        RekisteroiHenkilolle.Items.Clear()
        TbConnection.Close()

        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT SukuNimi, EtuNimi FROM Henkilosto"

        End With

        TbConnection.Open()

        Try
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then

                While rd.Read


                    RekisteroiHenkilolle.Items.Add(rd.GetString(0) & " " & rd.GetString(1))
                End While


            End If
            rd.Close()
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "Virhe rekisteöitagi osa 2", ErrorToString, 0, 0)
            Err.Clear()

        Finally
            TbConnection.Close()

        End Try



        LuettuViivakoodi2.Focus()

    End Sub

   

    Private Sub btTallenna_Click(sender As System.Object, e As System.EventArgs) Handles btTallenna.Click
        TbConnection.Close()

        Dim koodi As String = TagiNro.Text

        'TARKISTETAAN, ETTEI TAGI OLE KÄYTÖSSÄ

        Dim cmd As New MySqlCommand()

        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Tagit WHERE Nro = @Nro"
            .Parameters.AddWithValue("@Nro", TagiNro.Text)
        End With
        TbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        Main.TopMost = False

        If rd.HasRows = True Then MsgBox("TAGI on jo käytössä. Ota yhteys Joakimiin") : btTallenna.Visible = False : Main.TopMost = True : Exit Sub
        Main.TopMost = True

        rd.Close()

        TbConnection.Close()

        Dim uusiTagi As New MySqlCommand()
        With uusiTagi
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO Tagit " & _
                "(Nro, Kulin, HloNro, AlkaenPVM, AstiPVM, TilaID) " & _
                "VALUES(@Nro, @Kulin, @HloNro, @AlkaenPVM, @AstiPVM, @TilaID)"
            .Parameters.AddWithValue("@Nro", TagiNro.Text)
            .Parameters.AddWithValue("@Kulin", True)
            .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(Nimi.Text))
            .Parameters.AddWithValue("@AlkaenPVM", CType(Today, Date))
            .Parameters.AddWithValue("@AstiPVM", CType(Today.AddYears(9), Date))
            .Parameters.AddWithValue("@TilaID", "1")

        End With
        TbConnection.Open()
        Try
            uusiTagi.ExecuteNonQuery()

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "Virhe rekisteöitagi osa 1", ErrorToString, 0, 0)
            Err.Clear()

        Finally
            TbConnection.Close()

        End Try
   
        Me.Close()

        Kirjautuminen.MdiParent = Main
        Kirjautuminen.FormBorderStyle = Windows.Forms.FormBorderStyle.None

        Kirjautuminen.Location = New Point(0, 0)
        Kirjautuminen.WindowState = FormWindowState.Maximized
        Kirjautuminen.Dock = DockStyle.Fill

        Kirjautuminen.Show()

        Kirjautuminen.TopMost = True

        Do Until Len(koodi) = 10
            koodi = "0" & koodi
        Loop
        Kirjautuminen.LuettuKoodi.Text = koodi


    End Sub

    Private Sub LuettuViivakoodi2_TextChanged(sender As System.Object, e As System.EventArgs) Handles LuettuViivakoodi2.TextChanged
        Dim KayttajanTaso2 As String = ""
        If Len(LuettuViivakoodi2.Text) = 10 And Val(LuettuViivakoodi2.Text) <> 0 Then
            '
            Dim koodi As Integer = Val(LuettuViivakoodi2.Text)
            Dim paluu As Integer = Kirjautuminen.HaeTagistaKayttajaTiedot(LuettuViivakoodi2.Text)
            If paluu = 0 Then
                LuettuViivakoodi2.Text = ""
                LuettuViivakoodi2.Focus()

                Exit Sub
            End If

            If KayttajanTaso = "E" Or KayttajanTaso = "P" Or KayttajanTaso = "J" Or KayttajanTaso = "H" Then

                '     KayttajaOK = True

                LuettuViivakoodi2.Visible = False
                Label7.Visible = True
                RekisteroiHenkilolle.Visible = True
                btTallenna2.Visible = True
                RekisteroiHenkilolle.Text = KayttajaNimi
                Me.Cursor = Cursors.Default
                RekisteroiHenkilolle.Focus()

                Exit Sub
            Else
                LuettuViivakoodi2.Text = ""
                LuettuViivakoodi2.Focus()
            End If


        End If


  






    End Sub

    Private Sub btTallenna2_Click(sender As System.Object, e As System.EventArgs) Handles btTallenna2.Click
        TbConnection.Close()

        Dim koodi As String = TagiNro.Text

        'TARKISTETAAN, ETTEI TAGI OLE KÄYTÖSSÄ

        Dim cmd As New MySqlCommand()

        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT * FROM Tagit WHERE Nro = @Nro"
            .Parameters.AddWithValue("@Nro", TagiNro.Text)
        End With
        TbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader
        Main.TopMost = False

        If rd.HasRows = True Then MsgBox("TAGI on jo käytössä. Ota yhteys Joakimiin") : btTallenna2.Visible = False : Main.TopMost = True : Exit Sub
        rd.Close()
        Main.TopMost = True

        TbConnection.Close()

        Dim uusiTagi As New MySqlCommand()
        With uusiTagi
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "INSERT INTO Tagit " & _
                "(Nro, Kulin, HloNro, AutoNro, AlkaenPVM, AstiPVM, TilaID) " & _
                "VALUES(@Nro, @Kulin, @HloNro, @AutoNro, @AlkaenPVM, @AstiPVM, @TilaID)"
            .Parameters.AddWithValue("@Nro", TagiNro.Text)
            .Parameters.AddWithValue("@Kulin", True)
            .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(RekisteroiHenkilolle.Text))
            .Parameters.AddWithValue("@AlkaenPVM", CType(Today, Date))
            .Parameters.AddWithValue("@AstiPVM", CType(Today.AddYears(10), Date))
            .Parameters.AddWithValue("@TilaID", "1")
            .Parameters.AddWithValue("@AutoNro", "0")
        End With
        TbConnection.Open()
        Try
            uusiTagi.ExecuteNonQuery()
            LokiTapahtumanTallennus(KayttajaHloNro, "Rekisteröity TAGI", TagiNro.Text, PalautaSQLNroNimiesta(RekisteroiHenkilolle.Text), 0)

        Catch ex As Exception
            Err.Clear()
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE rekisteröi tagi", "", 0, 0)

        Finally
            TbConnection.Close()

        End Try
 
        Me.Close()

        Kirjautuminen.MdiParent = Main
        Kirjautuminen.FormBorderStyle = Windows.Forms.FormBorderStyle.None

        Kirjautuminen.Location = New Point(0, 0)
        Kirjautuminen.WindowState = FormWindowState.Maximized
        Kirjautuminen.Dock = DockStyle.Fill

        Kirjautuminen.Show()

        Kirjautuminen.TopMost = True
        KayttajanTaso = ""
        KayttajaHloNro = 0
        KayttajaNimi = ""
        KayttajanSalaSana = ""
        KayttajanAuto = 0


        Do Until Len(koodi) = 10
            koodi = "0" & koodi
        Loop
        Kirjautuminen.LuettuKoodi.Text = koodi


    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Me.Close()

        Kirjautuminen.MdiParent = Main
        Kirjautuminen.FormBorderStyle = Windows.Forms.FormBorderStyle.None

        Kirjautuminen.Location = New Point(0, 0)
        Kirjautuminen.WindowState = FormWindowState.Maximized
        Kirjautuminen.Dock = DockStyle.Fill

        Kirjautuminen.Show()

        Kirjautuminen.TopMost = True
        Kirjautuminen.LuettuKoodi.Focus()

    End Sub
End Class