Imports MySql.Data.MySqlClient

Public Class NaytaTalvilomaToiveet
    Public kpl As Button()
    Public vkNro As Label()
    Public AlkuPVM As Label()
    Public LoppuPVM As Label()

    Private MyYhteys As MySqlConnection = New MySqlConnection(serverString)

    Private Sub NaytaTalvilomaToiveet_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        kpl = New Button() {t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16, t17, t18, t19}
        vkNro = New Label() {vk1, vk2, vk3, vk4, vk5, vk6, vk7, vk8, vk9, vk10, vk11, vk12, vk13, vk14, vk15, vk16, vk17, vk18, vk19}
        AlkuPVM = New Label() {p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19}
        LoppuPVM = New Label() {l1, l2, l3, l4, l5, l6, l7, l8, l9, l10, l11, l12, l13, l14, l15, l16, l17, l18, l19}

        For i = 0 To 18
            kpl(i).Text = ""
            vkNro(i).Text = ""
            AlkuPVM(i).Text = ""
            LoppuPVM(i).Text = ""
        Next

        HaePaivamaarat()

        HaeToiveet()





    End Sub
    Public Sub HaePaivamaarat()
        Try
            MyYhteys.Close()

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT vkNro, AlkuPVM, LoppuPVM FROM Viikot WHERE Vuosi='2015' AND (vkNRO BETWEEN '1' AND '19') ORDER BY vkNRO ASC"
            End With
            MyYhteys.Open()
            Dim i As Integer = 0

            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                While rd.Read
                    '      For i = 0 To 18

                    vkNro(i).Text = rd.GetValue(0).ToString
                    AlkuPVM(i).Text = FormatDateTime(rd.GetValue(1), DateFormat.ShortDate).ToString
                    LoppuPVM(i).Text = FormatDateTime(rd.GetValue(2), DateFormat.ShortDate).ToString
                    'Next
                    i += 1

                End While
            End If
            MyYhteys.Close()

        Catch ex As Exception
            Err.Clear()

        End Try
    End Sub

    Public Sub HaeToiveet()

        Try
            MyYhteys.Close()
            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT Viikot.vkNRO, Lomat.AlkaenPVM, COUNT(Viikot.vkNro) FROM Lomat " & _
                "INNER JOIN Viikot ON Lomat.AlkaenPVM=Viikot.AlkuPVM " & _
                "WHERE Lomat.LomaTyyppi='14' OR Lomat.LomaTyyppi='24' GROUP BY Viikot.vkNRO ORDER BY Viikot.vkNRO ASC"
            End With
            MyYhteys.Open()
            Dim rd As MySqlDataReader = cmd.ExecuteReader
            If rd.HasRows = True Then
                While rd.Read
                    '   MsgBox(rd.GetValue(0).ToString & " | " & rd.GetValue(1).ToString & " | " & rd.GetValue(2).ToString)
                    For i = 0 To 18
                        If rd.GetValue(0) = Val(vkNro(i).Text) Then
                            kpl(i).Text = rd.GetValue(2).ToString
                            Exit For
                        End If

                    Next


                End While
            End If

            MyYhteys.Close()
        Catch ex As Exception
            Err.Clear()

        End Try



        For i = 1 To 19

            Chart1.Series("Lomatoiveet").Points.AddXY(i.ToString, Val(kpl(i - 1).Text))


        Next

    End Sub

    Private Sub t1_Click(sender As System.Object, e As System.EventArgs) Handles t1.Click
        HaeToiveet()
    End Sub
End Class