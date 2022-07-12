Imports MySql.Data.MySqlClient

'Imports System.Data.SqlClient
Imports System.Threading
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.ComponentModel

Public Class _LahtolistaView1
    Public tvX As Label()
    Public bil As Label()
    Public kuli As Label()
    Public myyYhteysx As MySqlConnection = New MySqlConnection(serverString)

    Private myTbConnection As MySqlConnection = New MySqlConnection(serverString)
    Private myTbConnection2 As MySqlConnection = New MySqlConnection(serverString)
    Private myTbConnectionPesuILM As MySqlConnection = New MySqlConnection(serverString)

    Private Sub _LahtolistaView_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        tvX = New Label() {t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, t19, t20, t21, t22, t23, t24, t25, t26, t27, t28, t29, t30, t31, t32, t33, t34, t35, t36, t37, t38, t39, t40, t41, t42, t43, t44, t45, t46, t47, t48, t49, t50}
        bil = New Label() {a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17, a18, a19, a20, a21, a22, a23, a24, a25, a26, a27, a28, a29, a30, a31, a32, a33, a34, a35, a36, a37, a38, a39, a40, a41, a42, a43, a44, a45, a46, a47, a48, a49, a50}
        kuli = New Label() {k1, k2, k3, k4, k5, k6, k7, k8, k9, k10, k11, k12, k13, k14, k15, k16, k17, k18, k19, k20, k21, k22, k23, k24, k25, k26, k27, k28, k29, k30, k31, k32, k33, k34, k35, k36, k37, k38, k39, k40, k41, k42, k43, k44, k45, k46, k47, k48, k49, k50}
        Try
            Dim pvmm As Date = Today '.AddDays(3)
            NaytettavaPVM.Value = pvmm

            '    ViikonPaiva.Text = PalautaViikonPaiva(pvmm) ', DateFormat.ShortDate))
            '       NaytettavaPVM_ValueChanged(Nothing, Nothing)
            '     LaitaVuorotNakyville()
            '     Panel1.Visible = True

            ViikonPaiva.Focus()

        Catch ex As Exception
            Err.Clear()

        End Try



        Control.CheckForIllegalCrossThreadCalls = False
        InitializeBGW()





    
        LataaLahtolistaTiedot()

        LahtoListaEsille()



    End Sub
    Public Sub LahtoListaEsille()
        Dim x As Integer = 0
        TyhjennaLahtoListaNaytolta()
        Select Case sivu
            Case 1
                x = 0
            Case 2
                x = 50


            Case 3
                x = 99
        End Select

        For i = 0 To 49

            '         If x = 0 And i = 50 Then Exit For
            '        If x = 1 And i = 100 Then Exit For
            If LLISTA(i + x).TV <> Nothing Then
                Try
                    '  If LLISTA(i + x).TV = "812A" Then Beep()
                    kuli(i).Text = LLISTA(i + x).KULI
                    tvX(i).Text = LLISTA(i + x).TV
                    bil(i).Text = LLISTA(i + x).REK
                    If LLISTA(i + x).AutoOK = False Then bil(i).ForeColor = Color.Red Else bil(i).ForeColor = Color.Black
                    If LLISTA(i + x).ilm = True Then kuli(i).ForeColor = Color.DarkGreen Else kuli(i).ForeColor = Color.Black

                Catch ex As Exception
                    Err.Clear()

                End Try

            End If

        Next
        LaitaPUNAISTA()


    End Sub
    Public Sub TyhjennaLahtoListaNaytolta()
        For i As Integer = 0 To 49

            Try
                '     If tvX IsNot Nothing Then tvX(i).Text = "" : tvX(i).ForeColor = Color.Black
                '      If bil IsNot Nothing Then bil(i).Text = "" : bil(i).ForeColor = Color.Black
                '     If kuli IsNot Nothing Then kuli(i).Text = "" : kuli(i).ForeColor = Color.Black
                '     If tvX IsNot Nothing Then tvX(i).BackColor = Color.Transparent
                '     If bil IsNot Nothing Then bil(i).BackColor = Color.Transparent
                '      If kuli IsNot Nothing Then kuli(i).BackColor = Color.Transparent
                tvX(i).Text = "" : tvX(i).ForeColor = Color.Black
                bil(i).Text = "" : bil(i).ForeColor = Color.Black
                kuli(i).Text = "" : kuli(i).ForeColor = Color.Black
                tvX(i).BackColor = Color.Transparent
                bil(i).BackColor = Color.Transparent
                kuli(i).BackColor = Color.Transparent

            Catch ex As Exception
                Err.Clear()

            End Try

        Next

        '       For i As Integer = 0 To 49
        'tvX(i).Text = ""
        '      bil(i).Text = ""
        '      kuli(i).Text = ""

        '      Next



    End Sub
    Public Sub LaitaPUNAISTA()
        If bil IsNot Nothing And tvX IsNot Nothing Then
            For i = 0 To 49
                If tvX(i).Text <> "" And kuli(i).Text = "" Then
                    tvX(i).BackColor = Color.Red

                End If
                Try
                    If tvX(i).Text <> "" And bil(i).Text = "" Then
                        bil(i).BackColor = Color.Blue : bil(i).ForeColor = Color.Black

                        bil(i).Text = "       "

                    End If
                Catch ex As Exception
                    Err.Clear()
                End Try


            Next
        End If

    End Sub
    Private Sub BackgroundWorker1_DoWork(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork


        Select Case tietokannanTapahtuma
            Case 1
                LataaLahtolistaTiedot()
                LahtoListaEsille()
                tietokannanTapahtuma = 0

            Case 99
                Main.LuoLahtolista()
                tietokannanTapahtuma = 0

        End Select

    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        System.Threading.Thread.Sleep(500)

        BackgroundWorker1.Dispose()

    End Sub
    Private Sub BackgroundWorker1_Disposed(sender As Object, e As System.EventArgs) Handles BackgroundWorker1.Disposed
        Thread.Sleep(250)

    End Sub

    Public Sub InitializeBGW() 'LataaSMSpuhelimesta
        Me.BackgroundWorker1 = Nothing
        Me.BackgroundWorker1 = New BackgroundWorker
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        AddHandler Me.BackgroundWorker1.DoWork, New DoWorkEventHandler(AddressOf Me.BackgroundWorker1_DoWork)
        '    AddHandler Me.KasitteleSMS.ProgressChanged, New ProgressChangedEventHandler(AddressOf Me.worker_ProgressChanged)
        AddHandler Me.BackgroundWorker1.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf Me.BackgroundWorker1_RunWorkerCompleted)
        AddHandler Me.BackgroundWorker1.Disposed, New EventHandler(AddressOf Me.BackgroundWorker1_Disposed)

        '  Me.LataaSMSpuhelimesta = Nothing
        '   Me.LataaSMSpuhelimesta = New BackgroundWorker
        '    Me.LataaSMSpuhelimesta.WorkerReportsProgress = True
        '     Me.LataaSMSpuhelimesta.WorkerSupportsCancellation = True
        '      AddHandler Me.LataaSMSpuhelimesta.DoWork, New DoWorkEventHandler(AddressOf Me.LataaSMSpuhelimesta_DoWork)
        '    AddHandler Me.KasitteleSMS.ProgressChanged, New ProgressChangedEventHandler(AddressOf Me.worker_ProgressChanged)
        ' AddHandler Me.LataaSMSpuhelimesta.RunWorkerCompleted, New RunWorkerCompletedEventHandler(AddressOf Me.LataaSMSpuhelimesta_RunWorkerCompleted)
        'AddHandler Me.LataaSMSpuhelimesta.Disposed, New EventHandler(AddressOf Me.LataaSMSpuhelimesta_Disposed)

    End Sub
    Public Structure tietoja
        Public Property Vuoro As String
        Public Property riviID As Long
        Public Property HloNro As Integer
        Public Property AutoNro As Integer

    End Structure

    Public Sub LataaLahtolistaTiedot()
        For i = 0 To 49
            LLISTA(i).TV = ""
            LLISTA(i).REK = ""
            LLISTA(i).AutoOK = False
            LLISTA(i).KULI = ""
            LLISTA(i).ilm = False

        Next
alku:
        myTbConnection.Close()
        '  Dim strPVM As Date = CDate(NaytettavaPVM.Value)
        '     Dim dAl As Date = Format(NaytettavaPVM.Value, "\#yyyy\-MM\-dd\#")
        '   tyhjennaLista()
        '     Try
        Dim cmd As New MySqlCommand()
        Dim dAl As Date = Format(NaytettavaPVM.Value, "\#yyyy\-MM\-dd\#")

        cmd.Connection = myTbConnection
        cmd.CommandText = "SELECT AjetutVuorot.RiviID, AjetutVuorot.TVLyhenne, Henkilosto.SukuNimi, Henkilosto.Etunimi, Kalusto.RekNro, AjetutVuorot.AutoNro, AjetutVuorot.Ilmoittauduttu, TyoVuorot.lRivi1, TyoVuorot.lRivi2, TyoVuorot.Lahtolistalle FROM AjetutVuorot " & _
                            "INNER JOIN Henkilosto ON AjetutVuorot.HloNro=Henkilosto.HloNro " & _
                            "INNER JOIN Kalusto ON AjetutVuorot.AutoNro=Kalusto.AutoNro " & _
                            "INNER JOIN TyoVuorot ON AjetutVuorot.TVLyhenne=TyoVuorot.Lyhenne " & _
                             "WHERE AlkuPVM = @AlkuPVM"
        '   cmd.Parameters.AddWithValue("@ALkuPVM", FormatDateTime(NaytettavaPVM.Value, DateFormat.ShortDate))
        cmd.Parameters.AddWithValue("@AlkuPVM", dAl)
        '    .Parameters.AddWithValue("@UusiTV", uusiTV.Text)
        '   .Parameters.AddWithValue("@VanhaTV", VanhaTV.Text)

        Dim str As String = FormatDateTime(NaytettavaPVM.Value, DateFormat.ShortDate).ToCharArray
        Dim kysely As LahtoListalle = Nothing
        Try
            myTbConnection.Open()

        Catch ex As Exception
            Err.Clear()
            Exit Sub

        End Try
        Dim TVlyhenne As String = ""
        Dim nroAuton As Integer = 0
        '     Dim NroHlon As Integer = 0
        Dim autonRek As String = ""
        Dim hloNimi As String = ""
        Dim IDriviNro As Integer
        Dim rdu As MySqlDataReader = cmd.ExecuteReader
        If rdu.HasRows = True Then
            Try
                While rdu.Read
                    kysely = Nothing
                    TVlyhenne = rdu.GetString(1)
                    nroAuton = rdu.GetValue(5)
                    '     NroHlon = rdu.GetValue(2)

                    autonRek = rdu.GetString(4)
                    hloNimi = rdu.GetString(2) & " " & rdu.GetString(3)
                    IDriviNro = rdu.GetValue(0)
                    kysely.L1Rivi = rdu.GetValue(7) ' = PalautaTiedotLahtolistaaVarten(TVlyhenne)
                    kysely.L2Rivi = rdu.GetValue(8)
                    kysely.listalle = rdu.GetValue(9)
                    kysely.TV = TVlyhenne

                    '  If sivu = 1 Then
                    If kysely.L1Rivi <> 0 Or kysely.L1Rivi <> Nothing Then
                        Dim uusiTagi As New tietoja

                        NaytaSIvu.Text = "1"
                        If tvX IsNot Nothing Then LLISTA(kysely.L1Rivi - 1).TV = TVlyhenne
                        uusiTagi.Vuoro = TVlyhenne
                        '    Dim auto As String = PalautaRekkariNumerosta(rd.GetValue(4))
                        '      If OnkoAutoAjoKelvoton(nroAuton, FormatDateTime(NaytettavaPVM.Value, DateFormat.ShortDate).ToString, "") = True Then If bil IsNot Nothing Then LLISTA(kysely.L1Rivi - 1).REK = autonRek 'uusiTagi.AutoNro = nroAuton

                        If TarkistaOnkoIlmoitettujaVikoja(nroAuton) = True Then
                            If bil IsNot Nothing Then LLISTA(kysely.L1Rivi - 1).AutoOK = False
                        Else
                            If bil IsNot Nothing Then LLISTA(kysely.L1Rivi - 1).AutoOK = True

                        End If
                        ' tarkista onko autosta tehtyhjä vikailmoituksia
                        LLISTA(kysely.L1Rivi - 1).KULI = hloNimi
                        '          LaitaPUNAISTA()
                        '    uusiTagi.HloNro = NroHlon
                        uusiTagi.riviID = IDriviNro
                        LLISTA(kysely.L1Rivi - 1).KULI = hloNimi
                        LLISTA(kysely.L1Rivi - 1).REK = autonRek
                        LLISTA(kysely.L1Rivi - 1).TV = uusiTagi.Vuoro
                        '    If uusiTagi.Vuoro = "812A" Then Beep()

                        If rdu.GetValue(6) = True Then LLISTA(kysely.L1Rivi - 1).ilm = True Else LLISTA(kysely.L1Rivi - 1).ilm = False

                    End If
                    '    End If
                    '  If sivu = 2 Then
                    If kysely.L2Rivi <> 0 Or kysely.L2Rivi <> Nothing Then
                        NaytaSIvu.Text = "2"
                        Dim uusiTagi As New tietoja

                        LLISTA(kysely.L2Rivi - 1).TV = TVlyhenne
                        uusiTagi.Vuoro = TVlyhenne

                        '     Dim auto As String = PalautaRekkariNumerosta(rd.GetValue(4))

                        '          If OnkoAutoAjoKelvoton(nroAuton, FormatDateTime(NaytettavaPVM.Value, DateFormat.ShortDate).ToString, "") = True Then LLISTA(kysely.L2Rivi - 1).REK = autonRek ': uusiTagi.AutoNro = nroAuton

                        If TarkistaOnkoIlmoitettujaVikoja(nroAuton) = True Then
                            LLISTA(kysely.L2Rivi - 1).AutoOK = False
                        Else
                            LLISTA(kysely.L2Rivi - 1).AutoOK = True

                        End If
                        ' tarkista onko autosta tehtyhjä vikailmoituksia
                        LLISTA(kysely.L2Rivi - 1).KULI = hloNimi

                        '  uusiTagi.HloNro = NroHlon
                        uusiTagi.riviID = IDriviNro

                        LLISTA(kysely.L2Rivi - 1).KULI = hloNimi
                        LLISTA(kysely.L2Rivi - 1).REK = autonRek
                        LLISTA(kysely.L2Rivi - 1).TV = uusiTagi.Vuoro
                        If rdu.GetValue(6) = True Then LLISTA(kysely.L2Rivi - 1).ilm = True Else LLISTA(kysely.L2Rivi - 1).ilm = False


                        '   End If


                    End If






                End While

            Catch ex As Exception
                Err.Clear()


            End Try
        End If

        '     Catch ex As Exception
        '          Throw ex

        '      End Try

        Dim laskuri As Boolean = False
        '           '                       '                          '                 LaitaPUNAISTA()
        If tvX IsNot Nothing Then
            For a As Integer = 0 To 49
                If tvX(a).Text <> "" Then laskuri = True

            Next
        End If

        If laskuri = False Then
            sivu = 3
            '     SivuVaihtuu()


        End If


        '      rd.Close()

        myTbConnection.Close()

    End Sub
    Public Function TarkistaOnkoIlmoitettujaVikoja(ByVal rek As Integer) As Boolean
        Dim paluu As Boolean = False

        myTbConnection2.Close()
        Dim cmd As New MySqlCommand()
        cmd.Connection = myTbConnection2
        cmd.CommandText = "SELECT AutoNro, Korjattu FROM VikaIlmoitukset WHERE AutoNro = @AutoNro AND Korjattu = @Korjattu"
        cmd.Parameters.AddWithValue("@AutoNro", rek)
        cmd.Parameters.AddWithValue("Korjattu", False)

        myTbConnection2.Open()

        Dim rdx As MySqlDataReader = cmd.ExecuteReader

        If rdx.HasRows = True Then
            paluu = True


        Else
            paluu = False

        End If
        myTbConnection2.Close()
        rdx.Close()


        Return paluu

    End Function
End Class