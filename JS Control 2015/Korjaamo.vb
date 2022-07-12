Public Class Korjaamo
    Dim objectposition, cursorpoint As Point
    Private Sub renew()
        objectposition = Button1.Location
        cursorpoint = Cursor.Position

    End Sub
    Private Sub Korjaamo_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub



    Private Sub Button1_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseDown
        Timer1.Enabled = True
        Timer1.Start()
        renew()

    End Sub

    Private Sub Button1_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseUp
        Timer1.Stop()
        renew()

    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Button1.Location = objectposition - cursorpoint + Cursor.Position
        Label1.Text = Button1.Location.X.ToString
        Label2.Text = Button1.Location.Y.ToString
        Vari()
    End Sub
    Public Sub Vari()
        Dim x As Integer = Button1.Location.X.ToString
        Dim y As Integer = Button1.Location.Y.ToString

        If x >= 17 And x <= 106 And y >= 78 And y <= 172 Then
            Button1.BackColor = Color.Red
        Else
            Button1.BackColor = Color.Cyan


        End If

    End Sub
End Class