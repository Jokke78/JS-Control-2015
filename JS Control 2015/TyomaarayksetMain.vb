Imports MySql.Data.MySqlClient

Public Class TyomaarayksetMain
    Private myYhteys As MySqlConnection = New MySqlConnection(serverString)

    Private Sub TyomaarayksetMain_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        LataaTunnuksetDataKenttaan()

    End Sub
    Private Sub DGW_RowHeaderMouseClick(sender As Object, e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGW.RowHeaderMouseClick
        txtKaytettyaika.Text = ""
        txtTehtytyo.Text = ""
        txtLiitetytViat.Text = ""

        myYhteys.Close()

        Dim i As Integer
        i = DGW.CurrentRow.Index

        Dim x As Integer = DGW.Item(0, i).Value
        Try
            myYhteys.Close()
            myYhteys.Open()

            Dim tbAdapter1 As New MySqlDataAdapter()

            With tbAdapter1
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
                .SelectCommand.CommandText = "SELECT Tyomaaraykset.TMnro, Tyomaaraykset.LuontiPVM, Tyomaaraykset.Otsikko, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Kuljettaja, Tyomaaraykset.Tulostettu, Tyomaaraykset.Valmis, Kalusto.RekNro, Tyomaaraykset.AikaaMIN, Tyomaaraykset.TyonKuvaus FROM Tyomaaraykset " & _
                    "INNER JOIN Henkilosto ON Tyomaaraykset.LuontiHlo=Henkilosto.HloNro " & _
                    "INNER JOIN Kalusto ON Tyomaaraykset.AutoNro=Kalusto.AutoNro " & _
                    "WHERE TMnro=@TMnro"
                .SelectCommand.Parameters.AddWithValue("@TMnro", x)

            End With
            Dim myDataSet1 As DataSet = New DataSet()

            tbAdapter1.Fill(myDataSet1, "Tyomaaraykset")

            Dim myDataView1 = New DataView(myDataSet1.Tables("Tyomaaraykset"))
            DGW1.AutoGenerateColumns = True
            DGW1.DataSource = myDataSet1
            DGW1.DataMember = "Tyomaaraykset"

        Catch ex As Exception
            Err.Clear()

        End Try

        '**************************
        Try
            myYhteys.Close()
            myYhteys.Open()

            Dim tbAdapter2 As New MySqlDataAdapter()

            With tbAdapter2
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
                .SelectCommand.CommandText = "SELECT VikaIlmoitukset.RiviID, VikaIlmoitukset.IlmoitettuVika, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Kuljettaja, VikaIlmoitukset.IlmPVM, VikaIlmoitukset.Korjattu  FROM VikaIlmoitukset " & _
                    "INNER JOIN Henkilosto ON VikaIlmoitukset.HloNro=Henkilosto.HloNro " & _
                    "WHERE LiitettyTMnro=@TMnro"
                .SelectCommand.Parameters.AddWithValue("@TMnro", x)

            End With
            Dim myDataSet2 As DataSet = New DataSet()

            tbAdapter2.Fill(myDataSet2, "VikaIlmoitukset")

            Dim myDataView2 = New DataView(myDataSet2.Tables("VikaIlmoitukset"))
            DGW2.AutoGenerateColumns = True
            DGW2.DataSource = myDataSet2
            DGW2.DataMember = "VikaIlmoitukset"


        Catch ex As Exception
            Err.Clear()

        End Try
        '**************************
        Try
            myYhteys.Close()
            myYhteys.Open()

            Dim tbAdapter3 As New MySqlDataAdapter()

            With tbAdapter3
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
                .SelectCommand.CommandText = "SELECT ID, TMnro, HloNro, AikaMIN, LisasettyPVM, LisaajaHloNro FROM TyohonKaytettyAika WHERE TMnro=@TMnro"
                .SelectCommand.Parameters.AddWithValue("@TMnro", x)

            End With
            Dim myDataSet3 As DataSet = New DataSet()

            tbAdapter3.Fill(myDataSet3, "TyohonKaytettyAika")

            Dim myDataView3 = New DataView(myDataSet3.Tables("TyohonKaytettyAika"))
            DGW3.AutoGenerateColumns = True
            DGW3.DataSource = myDataSet3
            DGW3.DataMember = "TyohonKaytettyAika"

        Catch ex As Exception
            Err.Clear()

        End Try

        '**************************
        Try
            myYhteys.Close()
            myYhteys.Open()

            Dim tbAdapter4 As New MySqlDataAdapter()

            With tbAdapter4
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
                .SelectCommand.CommandText = "SELECT * FROM TMkaytetytOsat WHERE TMnro=@TMnro"
                .SelectCommand.Parameters.AddWithValue("@TMnro", x)

            End With
            Dim myDataSet4 As DataSet = New DataSet()

            tbAdapter4.Fill(myDataSet4, "TMkaytetytOsat")

            Dim myDataView4 = New DataView(myDataSet4.Tables("TMkaytetytOsat"))
            DGW4.AutoGenerateColumns = True
            DGW4.DataSource = myDataSet4
            DGW4.DataMember = "TMkaytetytOsat"


        Catch ex As Exception
            Err.Clear()

        End Try
        '**************************
        Try
            myYhteys.Close()
            myYhteys.Open()

            Dim tbAdapter5 As New MySqlDataAdapter()

            With tbAdapter5
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
                .SelectCommand.CommandText = "SELECT * FROM Tehtytyo WHERE TMnro=@TMnro"
                .SelectCommand.Parameters.AddWithValue("@TMnro", x)

            End With
            Dim myDataSet5 As DataSet = New DataSet()

            tbAdapter5.Fill(myDataSet5, "Tehtytyo")

            Dim myDataView5 = New DataView(myDataSet5.Tables("Tehtytyo"))
            DGW5.AutoGenerateColumns = True
            DGW5.DataSource = myDataSet5
            DGW5.DataMember = "Tehtytyo"

        Catch ex As Exception
            Err.Clear()

        End Try


        '**************************
        Try
            myYhteys.Close()
            myYhteys.Open()

            Dim tbAdapter6 As New MySqlDataAdapter()

            With tbAdapter6
                .SelectCommand = New MySqlCommand
                .SelectCommand.Connection = myYhteys
                .SelectCommand.CommandText = "SELECT * FROM KMTiedot WHERE TMnro=@TMnro"
                .SelectCommand.Parameters.AddWithValue("@TMnro", x)

            End With
            Dim myDataSet6 As DataSet = New DataSet()

            tbAdapter6.Fill(myDataSet6, "KMTiedot")

            Dim myDataView6 = New DataView(myDataSet6.Tables("KMTiedot"))
            DGW6.AutoGenerateColumns = True
            DGW6.DataSource = myDataSet6
            DGW6.DataMember = "KMTiedot"
            myYhteys.Close()

        Catch ex As Exception
            Err.Clear()

        End Try



        'HAETAAN ERITTELY TIETO

        ' x on työmääräyksen numero

        Try
            myYhteys.Close()
            Dim haku As New MySqlCommand()
            With haku
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT Otsikko, AutoNro, AikaaMIN FROM Tyomaaraykset WHERE TMnro=@tmnro"
                .Parameters.AddWithValue("@tmnro", x)

            End With
            myYhteys.Open()

            Dim rdH As MySqlDataReader = haku.ExecuteReader
            If rdH.HasRows = True Then
                rdH.Read()
                lbotsikko.Text = rdH.GetString(0)
                lbRekNro.Text = PalautaSQLRekkariNumerosta(rdH.GetValue(1)) & " (" & rdH.GetValue(1) & ")"
                lbKaytettyaiak.Text = MuutaMinsatTunneiksi(rdH.GetValue(2))
                lbTMnumero.Text = x.ToString

            End If

        Catch ex As Exception
            Err.Clear()

        End Try
        myYhteys.Close()

        ' haetaan kilometri tieto
        Try
            myYhteys.Close()
            Dim haku2 As New MySqlCommand()
            With haku2
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT KMlukema FROM KMTiedot WHERE TMnro=@tmnro"
                .Parameters.AddWithValue("@tmnro", x)

            End With

            myYhteys.Open()
            Dim rd2 As MySqlDataReader = haku2.ExecuteReader
            If rd2.HasRows = True Then
                rd2.Read()
                lbKMlukema.Text = rd2.GetValue(0).ToString


            Else
                lbKMlukema.Text = "?"
            End If


        Catch ex As Exception
            Err.Clear()
            myYhteys.Close()

        End Try
        myYhteys.Close()

        'haetaan tehtytyö

        Try
            myYhteys.Close()
            Dim haku3 As New MySqlCommand()
            With haku3
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT Tyo FROM Tehtytyo WHERE TMnro=@tmnro"
                .Parameters.AddWithValue("@tmnro", x)
            End With

            myYhteys.Open()
            Dim rd3 As MySqlDataReader = haku3.ExecuteReader
            If rd3.HasRows = True Then
                While rd3.Read
                    txtTehtytyo.Text &= rd3.GetString(0) & vbCrLf

                End While

            Else

                txtTehtytyo.Text = ""

            End If
        Catch ex As Exception
            Err.Clear()

        End Try
        myYhteys.Close()

        'haetaan käyetttyaika
        Try
            myYhteys.Close()
            Dim haku4 As New MySqlCommand()
            With haku4
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) as Kuljettaja, TyohonKaytettyAika.AikaMIN FROM TyohonKaytettyAika " & _
                    "INNER JOIN Henkilosto ON TyohonKaytettyAika.HloNro = Henkilosto.HloNro " & _
                    "WHERE TyohonKaytettyAika.TMnro=@tmnro"
                .Parameters.AddWithValue("@tmnro", x)

            End With
            myYhteys.Open()
            Dim rd4 As MySqlDataReader = haku4.ExecuteReader
            If rd4.HasRows = True Then
                While rd4.Read
                    txtKaytettyaika.Text &= rd4.GetString(0) & vbTab & MuutaMinsatTunneiksi(rd4.GetValue(1)) & vbCrLf

                End While


            Else
                txtKaytettyaika.Text = ""

            End If
        Catch ex As Exception
            Err.Clear()

        End Try
        myYhteys.Close()

        'haetaan liitety viat

        Try
            myYhteys.Close()
            Dim haku5 As New MySqlCommand()
            With haku5
                .Connection = myYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT IlmoitettuVika, Korjattu FROM VikaIlmoitukset WHERE LiitettyTMnro=@tmnro"
                .Parameters.AddWithValue("@tmnro", x)


            End With


            myYhteys.Open()
            Dim rd5 As MySqlDataReader = haku5.ExecuteReader
            If rd5.HasRows = True Then
                While rd5.Read

                    txtLiitetytViat.Text &= rd5.GetString(0)
                    If rd5.GetValue(1) = 1 Then
                        txtLiitetytViat.Text &= " [korjattu]" & vbCrLf
                    Else
                        txtLiitetytViat.Text &= " [AVOINNA]" & vbCrLf

                    End If

                End While


            Else
                txtLiitetytViat.Text = ""

            End If




        Catch ex As Exception
            Err.Clear()

        End Try
        myYhteys.Close()

    End Sub


    Public Sub LataaTunnuksetDataKenttaan()


        myYhteys.Close()
        myYhteys.Open()

        Dim tbAdapter As New MySqlDataAdapter()

        With tbAdapter
            .SelectCommand = New MySqlCommand
            .SelectCommand.Connection = myYhteys
            .SelectCommand.CommandText = "SELECT Tyomaaraykset.TMnro, Tyomaaraykset.LuontiPVM, Kalusto.RekNro, CONCAT(Henkilosto.SukuNimi, ' ', Henkilosto.EtuNimi) AS Tehnyt, Tyomaaraykset.Otsikko, Tyomaaraykset.Valmis FROM Tyomaaraykset " & _
         "INNER JOIN Kalusto ON Tyomaaraykset.AutoNro=Kalusto.AutoNro " & _
         "INNER JOIN Henkilosto ON Tyomaaraykset.LuontiHlo=Henkilosto.HloNro " & _
         "ORDER BY Tyomaaraykset.TMnro DESC "


        End With
        Dim myDataSet As DataSet = New DataSet()

        tbAdapter.Fill(myDataSet, "Tyomaaraykset")

        Dim myDataView = New DataView(myDataSet.Tables("Tyomaaraykset"))
        DGW.AutoGenerateColumns = True
        DGW.DataSource = myDataSet
        DGW.DataMember = "Tyomaaraykset"
        myYhteys.Close()
        DGW.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        DGW.Columns(0).Width = 50
        DGW.Columns(1).Width = 100
        DGW.Columns(2).Width = 80
        '   DGW.Columns(3).HeaderText = "Osasto"
        DGW.Columns(3).Width = 300
        '   DGW.Columns(4).Width = 110
        '   DGW.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        '   DGW.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        '   DGW.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

        '        DGW.Columns(5).Width = 110
        '        DGW.Columns(6).Width = 110
        '        DGW.Columns(7).HeaderText = "Ei voimassa"
        '        DGW.Columns(7).Width = 150
    End Sub


    Private Sub DGW_CellContentClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGW.CellContentClick

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim i As Integer
        i = DGW.CurrentRow.Index

        Dim x As Integer = DGW.Item(0, i).Value
        Dim tmValmi As Boolean = DGW.Item(5, i).Value
        If tmValmi = True Then MsgBox("Palautettua työmääräystä ei voi muokata") : Exit Sub

        If x = 0 Then Exit Sub

        LokiTapahtumanTallennus(KayttajaHloNro, "TM muokkaus avattu TMnro " & x.ToString, "", 0, 0)
        Try
            TeeTyomaaraus.Close()

        Catch ex As Exception
            LokiTapahtumanTallennus(KayttajaHloNro, "VIRHE TM muokkauksen avaamisessa tmnro " & x.ToString, ErrorToString, 0, 0)
            Err.Clear()

        End Try

        TeeTyomaaraus.MdiParent = Main
        TeeTyomaaraus.Dock = DockStyle.Fill

        TeeTyomaaraus.Show()
        Main.SC.Panel2.Controls.Add(TeeTyomaaraus)

        TeeTyomaaraus.LataaTM(x)

        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        DGW.Visible = False
        MsgBox("POISTETTU KAIKKI TYÖMÄÄRÄYKSET JA HUOLTOHISTORIAT")
        MsgBox("VITSI :) --> Toiminto ei ole vielä käytössä")
        DGW.Visible = True

    End Sub
End Class