Public Class ViewUpdate
    Public Sub CheckForUpdates()
        '    If ProgressBar1.Value = 100 Then
        Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("https://www.dropbox.com/s/a8tcd0s31b5mmyl/JS%20Control%20versiotieto.txt?dl=0")
        Dim response As System.Net.HttpWebResponse = request.GetResponse()
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
        Dim newestversion As String = sr.ReadToEnd()
        Dim currentversion As String = Application.ProductVersion
        If newestversion.Contains(currentversion) Then
            Button1.Text = ("You are up todate!")
            Label2.Text = ("You may now close this dialog")
        Else
            Button1.Text = ("Downloading update!")
            WebBrowser1.Navigate("https://www.dropbox.com/s/inebxv25tfiw168/setup.exe?dl=0")

            Label2.Text = ("You may now close this dialog")
        End If
        '   End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        Button1.Enabled = False
        Button1.Text = "Checking for updates..."
        '      Timer1.Start()
        Label1.Text = ProgressBar1.Value
        CheckForUpdates()
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        ProgressBar1.Increment(5)
        Label1.Text = ProgressBar1.Value
        If ProgressBar1.Value = 100 Then
            Timer1.Stop()
            If ProgressBar1.Value = 100 Then
                Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("https://www.dropbox.com/s/a8tcd0s31b5mmyl/JS%20Control%20versiotieto.txt?dl=0")
                Dim response As System.Net.HttpWebResponse = request.GetResponse()
                Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
                Dim newestversion As String = sr.ReadToEnd()
                Dim currentversion As String = Application.ProductVersion
                If newestversion.Contains(currentversion) Then
                    Button1.Text = ("You are up todate!")
                    Label2.Text = ("You may now close this dialog")
                Else
                    Button1.Text = ("Downloading update!")
                    WebBrowser1.Navigate("http://dl.dropbox.com/u/46370133/Noter/Noter.exe")
                    Label2.Text = ("You may now close this dialog")
                End If
            End If
        End If
    End Sub
End Class