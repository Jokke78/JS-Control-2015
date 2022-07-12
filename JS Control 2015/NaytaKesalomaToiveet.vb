Imports MySql.Data.MySqlClient
Public Class NaytaKesalomaToiveet
    Public kpl As Button()
    Public vkNro As Label()
    Public AlkuPVM As Label()
    Public LoppuPVM As Label()
    Private MyYhteys As MySqlConnection = New MySqlConnection(serverString)

    Private Sub NaytaKesalomaToiveet_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        '    kpl = New Button() {t1, t2, t3, t4, t5}
        '     vkNro = New Label() {vk1, vk2, vk3, vk4, vk5}
        '   AlkuPVM = New Label() {p1, p2, p3, p4, p5}
        '  LoppuPVM = New Label() {l1, l2, l3, l4, l5}

        For i = 0 To 18
            kpl(i).Text = ""
            vkNro(i).Text = ""
            AlkuPVM(i).Text = ""
            LoppuPVM(i).Text = ""
        Next

        HaePaivamaarat()

        '     HaeToiveet()

    End Sub
    Public Sub HaePaivamaarat()
        Try
            MyYhteys.Close()

            Dim cmd As New MySqlCommand()
            With cmd
                .Connection = MyYhteys
                .CommandType = CommandType.Text
                .CommandText = "SELECT vkNro, AlkuPVM, LoppuPVM FROM Viikot WHERE Vuosi='2015' AND (vkNRO BETWEEN '19' AND '38') ORDER BY vkNRO ASC"
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
End Class