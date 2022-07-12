Imports MySql.Data.MySqlClient

Public Class PysakointiNappeina
    Public bil As Button()
    Private MyYhteys As MySqlConnection = New MySqlConnection(serverString)
    Private MyYhteys2 As MySqlConnection = New MySqlConnection(serverString)
    Private TBConnection As MySqlConnection = New MySqlConnection(serverString)

    Private Sub PysakointiNappeina_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        bil = New Button() {_1, _2, _3, _4, _5, _6, _7, _8, _9, _10, _11, _12, _13, _14, _15, _16, _17, _18, _19, _20, _21, _22, _23, _24, _25, _26, _27, _28, _29, _30, _31, _32, _33, _34, _35, _36, _37, _38, _39, _40, _41, _42, _43, _44, _45, _46, _47, _48, _49, _50, _51, _52, _53, _54, _55, _56, _57, _58, _59, _60, _61, _62, _63, _64, _65, _66, _67, _68, _69, _70, _71, _72, _73, _74, _75, _76, _77, _78, _79, _80, _81, _82, _83, _84, _85, _86, _87, _88, _89, _90, _91, _92, _93, _94, _95, _96}


        hae_autotilanne()
        LataaKuljettajatCBhen()
        lataaAutoCBhen()

    End Sub
    Public Sub LataaKuljettajatCBhen()
        myYhteys.Close()
        Kuli.Items.Clear()


        Dim cmd As New MySqlCommand()
        With cmd
            .Connection = myYhteys
            .CommandType = CommandType.Text
            .CommandText = "SELECT SukuNimi, EtuNimi FROM Henkilosto WHERE tSuhdeVoimassa=0 ORDER BY SukuNimi ASC"
            '  .Parameters.AddWithValue("@id", False)

        End With
        Try
            MyYhteys.Open()

            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                While rd.Read
                    Kuli.Items.Add(rd.GetValue(0) & " " & rd.GetValue(1))
                End While
            End If
        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE Pysakointihistorianäkymä lataa kuljettajatCB", ErrorToString, 0, 0)
            Err.Clear()

        End Try
        MyYhteys.Close()

    End Sub
    Public Sub lataaAutoCBhen()
        myYhteys.Close()

        '       Dim cmd As New MySqlCommand()
        Dim dAl As Date = Format(Today, "\#yyyy\-MM\-dd\#")

        'With cmd
        '.Connection = MyYhteys
        '.CommandType = CommandType.Text
        '.CommandText = "SELECT COUNT(*) FROM Tagit " & _
        '    "INNER JOIN Kalusto ON Kalusto.AutoNro=Tagit.AutoNro " & _
        '    "WHERE Tagit.Kulin='0' AND (@Alku BETWEEN AlkaenPVM AND AstiPVM) ORDER BY Kalusto.RekNro ASC"
        '.Parameters.AddWithValue("@Alku", dal)

        '           .Parameters.AddWithValue("@e", "E")
        '        .Parameters.AddWithValue("@j", "J")
        '

        '        End With
        Try
            '  MyYhteys.Open()

            '   Dim auto_maara As Integer = cmd.ExecuteScalar
            MyYhteys.Close()

            Dim cmd2 As New MySqlCommand()

            With cmd2
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT Kalusto.AutoNro FROM Tagit " & _
                    "INNER JOIN Kalusto ON Kalusto.AutoNro=Tagit.AutoNro " & _
                    "WHERE Tagit.Kulin='0' AND (@Alku BETWEEN AlkaenPVM AND AstiPVM) AND Kalusto.Raporteissa='1' ORDER BY Kalusto.RekNro ASC"
                .Parameters.AddWithValue("@Alku", dAl)

                '           .Parameters.AddWithValue("@e", "E")
                '        .Parameters.AddWithValue("@j", "J")
                '

            End With
            Try
                MyYhteys.Open()
                Dim laskuri As Integer = 0

                Dim rd As MySqlDataReader = cmd2.ExecuteReader
                If rd.HasRows = True Then
                    While rd.Read
                        MyYhteys2.Close()

                        Dim haku As New MySqlCommand()
                        With haku
                            .Connection = MyYhteys2
                            .CommandType = CommandType.Text
                            .CommandText = "SELECT * FROM PysakointiUUSI WHERE AutoNro=@AutoNro ORDER BY ID DESC LIMIT 1"
                            .Parameters.AddWithValue("@AutoNro", rd.GetValue(0))
                        End With
                        MyYhteys2.Open()

                        Dim tieto As MySqlDataReader = haku.ExecuteReader
                        Dim tooltippi As New ToolTip
                        tooltippi.AutoPopDelay = 2000
                        tooltippi.InitialDelay = 500
                        tooltippi.ReshowDelay = 500
                        tooltippi.ShowAlways = True
                        tooltippi.IsBalloon = True

                        If tieto.HasRows = True Then
                            tieto.Read()
                            bil(laskuri).Visible = True

                            bil(laskuri).Text = PalautaSQLRekkariNumerosta(tieto.GetValue(1))
                            Select Case tieto.GetValue(2)

                                Case 10
                                    bil(laskuri).BackColor = Color.Yellow
                                Case 11
                                    bil(laskuri).BackColor = Color.Aqua

                                Case 12
                                    bil(laskuri).BackColor = Color.Red

                                Case Else
                                    If tieto.GetValue(5) = 0 Then bil(laskuri).BackColor = Color.LightBlue : tooltippi.SetToolTip(Me.bil(laskuri), tieto.GetValue(2)) Else bil(laskuri).BackColor = Color.Lime : tooltippi.SetToolTip(Me.bil(laskuri), PalautaSQLNrostaNimi(tieto.GetValue(4)) & " / " & tieto.GetValue(3).ToString)


                            End Select

                        Else
                            bil(laskuri).Text = PalautaSQLRekkariNumerosta(tieto.GetValue(1))
                            bil(laskuri).BackColor = Color.Yellow
                            bil(laskuri).Visible = True

                        End If
                        MyYhteys2.Close()


                        laskuri += 1

                    End While
                End If
            Catch ex As Exception

                Err.Clear()

            End Try





        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE Pysakointikartta -  lataa autotCB", ErrorToString, 0, 0)

            Err.Clear()

        End Try
        MyYhteys.Close()
    End Sub

    Private Sub AutojenToimetMultipleEvents(ByVal sender As Object, ByVal e As System.EventArgs) Handles _1.MouseClick, _2.MouseClick, _3.MouseClick, _4.MouseClick, _5.MouseClick, _6.MouseClick, _7.MouseClick, _8.MouseClick, _9.MouseClick, _10.MouseClick, _11.MouseClick, _12.MouseClick, _13.MouseClick, _14.MouseClick, _15.MouseClick, _16.MouseClick, _17.MouseClick, _18.MouseClick, _19.MouseClick, _20.MouseClick, _21.MouseClick, _22.MouseClick, _23.MouseClick, _24.MouseClick, _25.MouseClick, _26.MouseClick, _27.MouseClick, _28.MouseClick, _29.MouseClick, _30.MouseClick, _31.MouseClick, _32.MouseClick, _33.MouseClick, _34.MouseClick, _35.MouseClick, _36.MouseClick, _37.MouseClick, _38.MouseClick, _39.MouseClick, _40.MouseClick, _41.MouseClick, _42.MouseClick, _43.MouseClick, _44.MouseClick, _45.MouseClick, _46.MouseClick, _47.MouseClick, _48.MouseClick, _49.MouseClick, _50.MouseClick, _51.MouseClick, _52.MouseClick, _53.MouseClick, _54.MouseClick, _55.MouseClick, _56.MouseClick, _57.MouseClick, _58.MouseClick, _59.MouseClick, _60.MouseClick, _61.MouseClick, _62.MouseClick, _63.MouseClick, _64.MouseClick, _65.MouseClick, _66.MouseClick, _67.MouseClick, _68.MouseClick, _69.MouseClick, _70.MouseClick, _71.MouseClick, _72.MouseClick, _73.MouseClick, _74.MouseClick, _75.MouseClick, _76.MouseClick, _77.MouseClick, _78.MouseClick, _79.MouseClick, _80.MouseClick, _81.MouseClick, _82.MouseClick, _83.MouseClick, _84.MouseClick, _85.MouseClick, _86.MouseClick, _87.MouseClick, _88.MouseClick, _89.MouseClick, _90.MouseClick, _91.MouseClick, _92.MouseClick, _93.MouseClick, _94.MouseClick, _95.MouseClick, _96.MouseClick

        Try
            Dim clickedButton As Button

            clickedButton = CType(sender, Button)

            If clickedButton.Text = "" Then Exit Sub 'clickedLabel.ForeColor = Color.Red

            Dim autoRek As String = clickedButton.Text

            Dim autoNro = PalautaSQLNumeroRekkarista(autoRek)
            Toimet.Text = autoRek & " [" & autoNro.ToString & "]"
            piiloAutoNro.Text = autoNro

            MyYhteys.Close()
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT PysakointiUUSI.Aika, PysakointiUUSI.Alue, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Kuljettaja, PysakointiUUSI.Otettu FROM PysakointiUUSI " & _
                    "INNER JOIN Henkilosto ON Henkilosto.HloNro=PysakointiUUSI.HloNro " & _
                    "WHERE PysakointiUUSI.AutoNro=@AutoNro ORDER BY PysakointiUUSI.ID DESC LIMIT 10"
                .Parameters.AddWithValue("@AutoNro", autoNro)

            End With

            MyYhteys.Open()
            Using rd As MySqlDataReader = cmd.ExecuteReader
                info.Text = "Pysäköintihistoria AUTO " & autoRek & vbCrLf & vbCrLf

                If rd.HasRows = True Then
                    While rd.Read
                        info.Text &= rd.GetValue(0) & vbTab & "alue " & rd.GetValue(1) & " " & rd.GetString(2) & vbTab
                        If rd.GetValue(3) = 0 Then info.Text &= "PYSÄKÖINYT" & vbCrLf Else info.Text &= "OTTANUT" & vbCrLf

                    End While
                End If
            End Using
            MyYhteys.Close()
        Catch ex As Exception
            Err.Clear()

        End Try



    End Sub

    Public Sub hae_autotilanne()








    End Sub


    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        If piiloAutoNro.Text = "" Then Exit Sub

        Try
            MyYhteys.Close()
            '  Dim muokkausrivi As Integer = 0



            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO PysakointiUUSI " & _
                "(AutoNro, Alue, Aika, HloNro, Otettu) " & _
                "VALUES(@AutoNro, @Alue, @Aika, @HloNro, @Otettu)"
                .Parameters.AddWithValue("@AutoNro", Val(piiloAutoNro.Text))
                .Parameters.AddWithValue("@Alue", Val(Alue.Text))
                .Parameters.AddWithValue("@Aika", Now)
                .Parameters.AddWithValue("@Otettu", False)
                .Parameters.AddWithValue("@HloNro", KayttajaHloNro)

                '   .Parameters.AddWithValue("@JattanytHlo", PalautaSQLNroNimiesta(PalauttajaNimi.Text))
            End With
            MyYhteys.Open()
            cmd2.ExecuteNonQuery()
            MyYhteys.Close()

            For i = 0 To 95
                If bil(i).Text = PalautaSQLRekkariNumerosta(piiloAutoNro.Text) Then
                    bil(i).BackColor = Color.LightBlue

                    Exit For
                End If
            Next


        Catch ex As Exception
            Err.Clear()

        End Try
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        If Toimet.Text = "" Then Exit Sub

        Try
            TbConnection.Close()
            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO PysakointiUUSI " & _
                "(AutoNro, HloNro, Aika, Alue, Otettu) " & _
                "VALUES(@AutoNro, @HloNro, @Aika, @Alue, @Otettu)"
                .Parameters.AddWithValue("@AutoNro", Val(piiloAutoNro.Text))
                .Parameters.AddWithValue("@HloNro", PalautaSQLNroNimiesta(Kuli.Text))
                .Parameters.AddWithValue("@Aika", Now)
                .Parameters.AddWithValue("@Alue", 0)
                .Parameters.AddWithValue("@Otettu", True)
            End With
            TbConnection.Open()
            cmd2.ExecuteNonQuery()
            TbConnection.Close()
            For i = 0 To 95
                If bil(i).Text = PalautaSQLRekkariNumerosta(piiloAutoNro.Text) Then
                    bil(i).BackColor = Color.Lime

                    Exit For
                End If
            Next

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE työvuoro kirjaus- auton ajoon merkitseminen", ErrorToString, 0, 0)

            Err.Clear()

        End Try
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click

        TallennaPysakointi(11, KayttajaHloNro)

    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        TallennaPysakointi(10, KayttajaHloNro)

    End Sub

    Private Sub Button10_Click(sender As System.Object, e As System.EventArgs) Handles Button10.Click
        TallennaPysakointi(12, KayttajaHloNro)

    End Sub
    Public Sub TallennaPysakointi(ByVal Alue As Integer, ByVal palauttajanNro As Integer)
        Try
            TbConnection.Close()
            '  Dim muokkausrivi As Integer = 0



            Dim cmd2 As New MySqlCommand()
            With cmd2
                .Connection = TbConnection
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO PysakointiUUSI " & _
                "(AutoNro, Alue, Aika, HloNro, Otettu) " & _
                "VALUES(@AutoNro, @Alue, @Aika, @HloNro, @Otettu)"
                .Parameters.AddWithValue("@AutoNro", Val(piiloAutoNro.Text))
                .Parameters.AddWithValue("@Alue", Alue)
                .Parameters.AddWithValue("@Aika", Now)
                .Parameters.AddWithValue("@Otettu", False)
                .Parameters.AddWithValue("@HloNro", palauttajanNro)

                '   .Parameters.AddWithValue("@JattanytHlo", PalautaSQLNroNimiesta(PalauttajaNimi.Text))
            End With
            TbConnection.Open()
            cmd2.ExecuteNonQuery()
            TbConnection.Close()
   
        Catch ex As Exception
            Err.Clear()

        End Try
        For i = 0 To 95
            If bil(i).Text = PalautaSQLRekkariNumerosta(piiloAutoNro.Text) Then
                Select Case Alue
                    Case 10
                        bil(i).BackColor = Color.Yellow
                        Exit For
                    Case 11
                        bil(i).BackColor = Color.Aqua
                        Exit For
                    Case 12
                        bil(i).BackColor = Color.Red
                        Exit For

                End Select
            End If
        Next
        Dim autoNro As Integer = Val(piiloAutoNro.Text)
        Try
            Toimet.Text = PalautaSQLRekkariNumerosta(Val(piiloAutoNro.Text)) & " [" & piiloAutoNro.Text & "]"
            '     piiloAutoNro.Text = autoNro

            MyYhteys.Close()
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT PysakointiUUSI.Aika, PysakointiUUSI.Alue, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Kuljettaja, PysakointiUUSI.Otettu FROM PysakointiUUSI " & _
                    "INNER JOIN Henkilosto ON Henkilosto.HloNro=PysakointiUUSI.HloNro " & _
                    "WHERE PysakointiUUSI.AutoNro=@AutoNro ORDER BY PysakointiUUSI.ID DESC LIMIT 10"
                .Parameters.AddWithValue("@AutoNro", autoNro)

            End With

            MyYhteys.Open()
            Using rd As MySqlDataReader = cmd.ExecuteReader
                info.Text = "Pysäköintihistoria AUTO " & PalautaSQLRekkariNumerosta(Val(piiloAutoNro.Text)) & vbCrLf & vbCrLf

                If rd.HasRows = True Then
                    While rd.Read
                        info.Text &= rd.GetValue(0) & vbTab & "alue " & rd.GetValue(1) & " " & rd.GetString(2) & vbTab
                        If rd.GetValue(3) = 0 Then info.Text &= "PYSÄKÖINYT" & vbCrLf Else info.Text &= "OTTANUT" & vbCrLf

                    End While
                End If
            End Using
            MyYhteys.Close()
        Catch ex As Exception
            Err.Clear()

        End Try
    End Sub

    Private Sub btnPaivita_Click(sender As System.Object, e As System.EventArgs) Handles btnPaivita.Click
        Me.Cursor = Cursors.WaitCursor

        lataaAutoCBhen()
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub info_TextChanged(sender As System.Object, e As System.EventArgs) Handles info.TextChanged

    End Sub
End Class