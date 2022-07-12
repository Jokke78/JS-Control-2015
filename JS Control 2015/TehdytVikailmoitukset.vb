Imports MySql.Data.MySqlClient

Public Class TehdytVikailmoitukset
    Private TbConnection3 As MySqlConnection = New MySqlConnection(serverString)
    Private TbConnection As MySqlConnection = New MySqlConnection(serverString)

    Public Sub LataaViatNakyviin()
        Viat.Items.Clear()
        Me.Cursor = Cursors.WaitCursor

        TbConnection3.Close()
        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection3
            .CommandType = CommandType.Text
            .CommandText = "SELECT IlmoitettuVika, AutoNro, HloNro, IlmPVM, riviID " & _
                           "FROM VikaIlmoitukset " & _
                           "WHERE Korjattu = @Korjattu AND LiitettyTMnro='0' " & _
                           "ORDER BY IlmPVM DESC LIMIT @limit"
            .Parameters.AddWithValue("@Korjattu", False)
            .Parameters.AddWithValue("@limit", Val(cbShowLimit.Text))

        End With
        TbConnection3.Open()

        Try
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                While rd.Read
                    Dim erotus As TimeSpan = Now - CType(rd.GetValue(3), Date)

                    Viat.Items.Add(FormatDateTime(rd.GetValue(3), DateFormat.ShortDate).ToString & vbTab & "[" & Int(erotus.TotalDays).ToString & "]" & vbTab & PalautaSQLRekkariNumerosta(rd.GetValue(1)) & vbTab & rd.GetString(0) & vbTab & "[" & PalautaSQLNrostaNimi(rd.GetValue(2)) & "]" & vbTab & "|" & rd.GetValue(4).ToString & "|")
                    '   Viat.Refresh()

                    '      If PalautaSQLRekkariNumerosta(rd.GetValue(1)) = "" Then MsgBox(rd.GetValue(1).ToString)



                End While
            End If
        Catch ex As Exception
            Err.Clear()


        End Try

        Me.Cursor = Cursors.Default
        VikojaYHT.Text = Viat.Items.Count




    End Sub

    Private Sub IlmoitetutViat_Opening(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles IlmoitetutViat.Opening
        If Viat.Text = "" Then IlmoitetutViat.Close() : Exit Sub


        Dim rivit As String() = Viat.Text.Split(vbTab)

        Dim aikaa As String = rivit(1)
        aikaa = aikaa.Replace("[", "")
        aikaa = aikaa.Replace("]", "")

        IlmjaAikaa.Text = "Ilmoitettu " & rivit(0) & "(" & aikaa & " päivää sitten)"
        vikaToTxt.Text = rivit(3)
        Dim ilmoittaja As String = rivit(4).Replace("[", "")
        ilmoittaja = ilmoittaja.Replace("]", "")
        IlmNimi.Text = ilmoittaja
        IlmPuhNro.Text = PalautaNimestaPuhelinNumero(ilmoittaja, CType(rivit(0), Date))
        RekNrotoTXT.Text = rivit(2) & " (Auto nro & " & PalautaSQLNumeroRekkarista(rivit(2)) & ")"





    End Sub

    Private Sub btPoista_Click(sender As System.Object, e As System.EventArgs) Handles btPoista.Click
        If Viat.Text = "" Then Exit Sub
        Main.TopMost = False
   
        Dim x As Integer = MsgBox("Haluatko varmasti poistaa vian " & vikaToTxt.Text & " tietokannasta", vbYesNo)
        Main.TopMost = True
    
        If x = MsgBoxResult.No Then
            Exit Sub

        End If

        '      MsgBox("POISTETAAN")

        Dim rivit As String() = Viat.Text.Split(vbTab)


        Dim poistettavaRivi As String = rivit(5).Replace("|", "")

        TbConnection3.Close()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection3
            .CommandType = CommandType.Text
            .CommandText = "UPDATE VikaIlmoitukset " & _
                            "SET Korjattu = @Korjattu, PoistettuPVM = @PoistettuPVM, PoistajaHloNro = @PoistajaHloNro " & _
                            "WHERE riviID = @riviID"
            .Parameters.AddWithValue("@riviID", poistettavaRivi)
            .Parameters.AddWithValue("@PoistettuPVM", CType(Now, Date))
            .Parameters.AddWithValue("@PoistajaHloNro", KayttajaHloNro)
            .Parameters.AddWithValue("@Korjattu", True)

        End With
        Try
            TbConnection3.Open()
            cmd.ExecuteNonQuery()
            TbConnection3.Close()

        Catch ex As Exception
            Err.Clear()
            Main.TopMost = False

            MsgBox("EPÄONNISTUI")
            Main.TopMost = True
            TbConnection3.Close()
            Exit Sub
        Finally
            Main.TopMost = False
            MsgBox("POISTETTU")
            Main.TopMost = True

        End Try

        TbConnection3.Close()










        Viat.Items.Remove(Viat.Items(Viat.SelectedIndex))

    End Sub
    Public Sub LataaAutot()
        cbrekNro.Items.Clear()

        TbConnection.Close()
        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection
            .CommandType = CommandType.Text
            .CommandText = "SELECT RekNro FROM Kalusto WHERE Raporteissa = '1' ORDER BY RekNro ASC"
            '      .Parameters.AddWithValue("@Raporteissa", True)

        End With

        TbConnection.Open()
        Dim rd As MySqlDataReader = cmd.ExecuteReader

        If rd.HasRows = True Then
            While rd.Read
                cbrekNro.Items.Add(rd.GetString(0))
            End While
        End If
        TbConnection.Close()


    End Sub
    Private Sub Viat_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles Viat.SelectedIndexChanged
        VikojaYHT.Text = Viat.Items.Count

    End Sub



    Private Sub cbrekNro_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cbrekNro.SelectedIndexChanged
        Me.Cursor = Cursors.WaitCursor

        TbConnection3.Close()
        If cbrekNro.Text = "" Then LataaViatNakyviin() : VikojaYHT.Text = Viat.Items.Count : Exit Sub
        Viat.Items.Clear()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection3
            .CommandType = CommandType.Text
            .CommandText = "SELECT IlmoitettuVika, AutoNro, HloNro, IlmPVM, riviID " & _
                           "FROM VikaIlmoitukset " & _
                           "WHERE Korjattu = @Korjattu AND AutoNro = @AutoNro AND LiitettyTMnro='0'" & _
                           "ORDER BY IlmPVM DESC"
            .Parameters.AddWithValue("@Korjattu", False)
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbrekNro.Text))
        End With
        TbConnection3.Open()

        Try
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                While rd.Read
                    Dim erotus As TimeSpan = Now - CType(rd.GetValue(3), Date)

                    Viat.Items.Add(FormatDateTime(rd.GetValue(3), DateFormat.ShortDate).ToString & vbTab & "[" & Int(erotus.TotalDays).ToString & "]" & vbTab & PalautaSQLRekkariNumerosta(rd.GetValue(1)) & vbTab & rd.GetString(0) & vbTab & "[" & PalautaSQLNrostaNimi(rd.GetValue(2)) & "]" & vbTab & "|" & rd.GetValue(4).ToString & "|")
                    Viat.Refresh()

                    '      If PalautaSQLRekkariNumerosta(rd.GetValue(1)) = "" Then MsgBox(rd.GetValue(1).ToString)



                End While
            End If
        Catch ex As Exception
            Err.Clear()


        End Try

        VikojaYHT.Text = Viat.Items.Count

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub cbrekNro_Click(sender As System.Object, e As System.EventArgs) Handles cbrekNro.Click

    End Sub

    Private Sub cbrekNro_TextChanged(sender As Object, e As System.EventArgs) Handles cbrekNro.TextChanged
        TbConnection3.Close()
        If cbrekNro.Text = "" Then LataaViatNakyviin() : Exit Sub
        Viat.Items.Clear()

        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = TbConnection3
            .CommandType = CommandType.Text
            .CommandText = "SELECT IlmoitettuVika, AutoNro, HloNro, IlmPVM, riviID " & _
                           "FROM VikaIlmoitukset " & _
                           "WHERE Korjattu = @Korjattu AND AutoNro = @AutoNro AND LiitettyTMnro='0' " & _
                           "ORDER BY IlmPVM DESC"
            .Parameters.AddWithValue("@Korjattu", False)
            .Parameters.AddWithValue("@AutoNro", PalautaSQLNumeroRekkarista(cbrekNro.Text))
        End With
        TbConnection3.Open()

        Try
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                While rd.Read
                    Dim erotus As TimeSpan = Now - CType(rd.GetValue(3), Date)

                    Viat.Items.Add(FormatDateTime(rd.GetValue(3), DateFormat.ShortDate).ToString & vbTab & "[" & Int(erotus.TotalDays).ToString & "]" & vbTab & PalautaSQLRekkariNumerosta(rd.GetValue(1)) & vbTab & rd.GetString(0) & vbTab & "[" & PalautaSQLNrostaNimi(rd.GetValue(2)) & "]" & vbTab & "|" & rd.GetValue(4).ToString & "|")
                    Viat.Refresh()

                    '      If PalautaSQLRekkariNumerosta(rd.GetValue(1)) = "" Then MsgBox(rd.GetValue(1).ToString)



                End While
            End If
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE tehdytvikailmoitukset", ErrorToString, 0, 0)
            Err.Clear()

        End Try

        VikojaYHT.Text = Viat.Items.Count
    End Sub
    Public Sub alkutoimet()
        cbShowLimit.Text = "30"
        LataaAutot()
        VikojaYHT.Text = Viat.Items.Count

    End Sub
    Private Sub TehdytVikailmoitukset_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        alkutoimet()

    End Sub

    Private Sub cbShowLimit_Click(sender As System.Object, e As System.EventArgs) Handles cbShowLimit.Click

    End Sub

    Private Sub cbShowLimit_indexChanged(sender As Object, e As System.EventArgs) Handles cbShowLimit.SelectedIndexChanged

        LataaViatNakyviin()

    End Sub
End Class