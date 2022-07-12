Imports MySql.Data.MySqlClient

Public Class ViewTallennaKatsastus
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)

    Private Sub ViewTeeKatsastusIlmoitus_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LeimaPVM.Value = Now
        JalkiPVM.Value = Now.AddDays(30)
        LataaCBhenAutot()

        If OletusAuto <> "" Then cbAuto.Text = OletusAuto


    End Sub
    Public Structure AutoItem
        Public Property AutoNro As Integer
        Public Property RekNro As String
        Public Property RekistPVM As Date

    End Structure

    Public Sub LataaCBhenAutot()
        Try
            cbAuto.DisplayMember = "RekNro"
            cbAuto.Items.Clear()
            TbConnection.Close()
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT AutoNro, RekNro, RekistPVM FROM Kalusto WHERE Raporteissa=1"
            End With

            TbConnection.Open()
            Dim rd As MySqlDataReader = cmd.ExecuteReader

            If rd.HasRows = True Then

                While rd.Read
                    Dim uusi As New AutoItem

                    If rd.GetString(1) <> "xxxxxx" Then
                        uusi.AutoNro = rd.GetValue(0)
                        uusi.RekNro = rd.GetString(1)
                        uusi.RekistPVM = rd.GetValue(2)
                        cbAuto.Items.Add(uusi)

                    End If



                End While
            End If

            rd.Close()
            TbConnection.Close()
        Catch ex As Exception
            Err.Clear()

        End Try



    End Sub


    Private Sub cbAuto_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbAuto.SelectedIndexChanged
        Try
            txtRekPVM.Text = ""
            If cbAuto.Text = "" Then txtRekPVM.Text = "" : Exit Sub

            Dim autoi As AutoItem
            autoi = cbAuto.SelectedItem

            txtRekPVM.Text = FormatDateTime(autoi.RekistPVM, DateFormat.ShortDate).ToCharArray



            'HAETAAN VIIMEISIN KATSASTUSTIETO

            TbConnection.Close()

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT PVM, AjoaikaaAsti FROM KatsastuksienSeuranta WHERE AutoNro = @AutoNro"
                .Parameters.AddWithValue("@AutoNro", autoi.AutoNro)
            End With
            TbConnection.Open()
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                If rd.GetValue(1) Is DBNull.Value Then
                    viimeksiLeima.Text = FormatDateTime(CType(rd.GetValue(0), Date), DateFormat.ShortDate).ToString
                    hylky.Text = ""
                Else
                    viimeksiLeima.Text = FormatDateTime(CType(rd.GetValue(0), Date), DateFormat.ShortDate).ToString
                    hylky.Text = "AJOAIKAA " & FormatDateTime(CType(rd.GetValue(1), Date), DateFormat.ShortDate).ToString & " asti"
                End If
            Else
                viimeksiLeima.Text = "EI TIETOA"
            End If
            rd.Close()
            TbConnection.Close()

        Catch ex As Exception
            Err.Clear()

        End Try
        OletusAuto = cbAuto.Text

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Try
            Main.TopMost = False
            If cbAuto.Text = "" Then MsgBox("VALITSE KATSASTETTAVA AUTO") : Main.TopMost = True : Exit Sub
            Main.TopMost = True : Dim auti As AutoItem
            auti = cbAuto.SelectedItem
            Dim onnas As Boolean = False

            onnas = TallennaKatsastus(auti.AutoNro, True, JalkiPVM.Value, KayttajaHloNro, LeimaPVM.Value)

            If onnas = True Then
                Main.TopMost = False
                MsgBox("TALLENNETTU KATSASTUS")
                Main.TopMost = True
            Else
                Main.TopMost = False : MsgBox("VIRHE KATSASTUKSEN TALLENTAMISESSA. Ota yhteys Joakimiin. p. 050 521 6804")
                Main.TopMost = True
            End If

            Dim autoi As AutoItem
            autoi = cbAuto.SelectedItem

            txtRekPVM.Text = FormatDateTime(autoi.RekistPVM, DateFormat.ShortDate).ToCharArray



            'HAETAAN VIIMEISIN KATSASTUSTIETO

            TbConnection.Close()

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT PVM, AjoaikaaAsti FROM KatsastuksienSeuranta WHERE AutoNro = @AutoNro"
                .Parameters.AddWithValue("@AutoNro", autoi.AutoNro)
            End With
            TbConnection.Open()
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                If rd.GetValue(1) Is DBNull.Value Then
                    viimeksiLeima.Text = FormatDateTime(CType(rd.GetValue(0), Date), DateFormat.ShortDate).ToString
                    hylky.Text = ""
                Else
                    viimeksiLeima.Text = FormatDateTime(CType(rd.GetValue(0), Date), DateFormat.ShortDate).ToString
                    hylky.Text = "AJOAIKAA " & FormatDateTime(CType(rd.GetValue(1), Date), DateFormat.ShortDate).ToString & " asti"
                End If
            Else
                viimeksiLeima.Text = "EI TIETOA"
            End If
            rd.Close()
            TbConnection.Close()
        Catch ex As Exception
            Err.Clear()

        End Try

    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Try
            If cbAuto.Text = "" Then MsgBox("VALITSE KATSASTETTAVA AUTO") : Exit Sub
            Dim auti As AutoItem
            auti = cbAuto.SelectedItem
            Dim onnas As Boolean = False

            onnas = TallennaKatsastus(auti.AutoNro, False, JalkiPVM.Value, KayttajaHloNro, LeimaPVM.Value)
            Dim smsviesti As String = "Hylatty katsastus autosta " & cbAuto.Text & ", ajoaikaa " & FormatDateTime(JalkiPVM.Value, DateFormat.ShortDate).ToString & " asti. Merkinnan teki " & KayttajaNimi & "."

            LahetäSMSViestiOdottamaanLehettamista("0505216835", smsviesti, KayttajaHloNro)
            LahetäSMSViestiOdottamaanLehettamista("0505216834", smsviesti, KayttajaHloNro)

            If onnas = True Then
                MsgBox("TALLENNETTU HYLÄTTY KATSASTUS. Tekstiviesti on lähetetty korjaamopäällikölle. TEE VIKAILMOITUS KATSASTUSLAPPUUN MERKITYISTÄ VIOISTA!!!")

            Else
                MsgBox("VIRHE KATSASTUKSEN TALLENTAMISESSA. Ota yhteys Joakimiin. p. 050 521 6804")

            End If
            Dim autoi As AutoItem
            autoi = cbAuto.SelectedItem

            txtRekPVM.Text = FormatDateTime(autoi.RekistPVM, DateFormat.ShortDate).ToCharArray



            'HAETAAN VIIMEISIN KATSASTUSTIETO

            TbConnection.Close()

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "SELECT PVM, AjoaikaaAsti FROM KatsastuksienSeuranta WHERE AutoNro = @AutoNro"
                .Parameters.AddWithValue("@AutoNro", autoi.AutoNro)
            End With
            TbConnection.Open()
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                rd.Read()
                If rd.GetValue(1) Is DBNull.Value Then
                    viimeksiLeima.Text = FormatDateTime(CType(rd.GetValue(0), Date), DateFormat.ShortDate).ToString
                    hylky.Text = ""
                Else
                    viimeksiLeima.Text = FormatDateTime(CType(rd.GetValue(0), Date), DateFormat.ShortDate).ToString
                    hylky.Text = "AJOAIKAA " & FormatDateTime(CType(rd.GetValue(1), Date), DateFormat.ShortDate).ToString & " asti"
                End If
            Else
                viimeksiLeima.Text = "EI TIETOA"
            End If
            rd.Close()
            TbConnection.Close()

        Catch ex As Exception
            Err.Clear()

        End Try

    End Sub

    Private Sub Panel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
    Private Sub TableLayoutPanel4_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel4.Paint

    End Sub
    Private Sub Label6_Click(sender As System.Object, e As System.EventArgs) Handles Label6.Click

    End Sub
    Private Sub TableLayoutPanel3_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel3.Paint

    End Sub
    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles Label1.Click

    End Sub
    Private Sub TableLayoutPanel1_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub
    Private Sub hylky_Click(sender As System.Object, e As System.EventArgs) Handles hylky.Click

    End Sub
    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click

    End Sub
    Private Sub TableLayoutPanel2_Paint(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles TableLayoutPanel2.Paint

    End Sub
    Private Sub txtRekPVM_Click(sender As System.Object, e As System.EventArgs) Handles txtRekPVM.Click

    End Sub
    Private Sub Label5_Click(sender As System.Object, e As System.EventArgs) Handles Label5.Click

    End Sub
    Private Sub JalkiPVM_ValueChanged(sender As System.Object, e As System.EventArgs) Handles JalkiPVM.ValueChanged

    End Sub
    Private Sub LeimaPVM_ValueChanged(sender As System.Object, e As System.EventArgs) Handles LeimaPVM.ValueChanged

    End Sub
    Private Sub Label4_Click(sender As System.Object, e As System.EventArgs) Handles Label4.Click

    End Sub
    Private Sub Label3_Click(sender As System.Object, e As System.EventArgs) Handles Label3.Click

    End Sub
    Private Sub Label2_Click(sender As System.Object, e As System.EventArgs) Handles Label2.Click

    End Sub
    Private Sub viimeksiLeima_Click(sender As System.Object, e As System.EventArgs) Handles viimeksiLeima.Click

    End Sub
    Private Sub Label7_Click(sender As System.Object, e As System.EventArgs) Handles Label7.Click

    End Sub
End Class