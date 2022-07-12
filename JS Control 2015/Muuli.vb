Public Class Muuli

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        WebBrowser1.Document.GetElementById("user").SetAttribute("value", "TKl")
        WebBrowser1.Document.GetElementById("passwd").SetAttribute("value", "jpa472")
        WebBrowser1.Document.Forms(0).InvokeMember("submit")
        Me.Cursor = Cursors.Default
        Button1.Visible = True
        Button2.Visible = False
        Button3.Visible = False
        Main.tview.Enabled = False


    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        WebBrowser1.Document.GetElementById("user").SetAttribute("value", "KL")
        WebBrowser1.Document.GetElementById("passwd").SetAttribute("value", "Helm1kuu")
        WebBrowser1.Document.Forms(0).InvokeMember("submit")
        Me.Cursor = Cursors.Default
        Button1.Visible = True
        Button2.Visible = False
        Button3.Visible = False
        Main.tview.Enabled = False

    End Sub

    Private Sub Muuli_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ' WebBrowser1.Navigate("http://hsl.seasam.com/ajali/ajali?command=login&com=logout")
        WebBrowser1.Navigate("http://hsl.trapeze.fi/ajali/ajali")

    End Sub
    Private Sub WebBrowser1_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles WebBrowser1.NewWindow
        ' Prevent opening a new windows
        e.Cancel = True
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If WebBrowser1.Url.ToString <> "http://hsl.trapeze.fi/ajali/ajali?command=login&com=logout" Then
            Main.TopMost = False

            MsgBox("Kirjaudu ohjelmasta ensin ulos")
            Main.TopMost = True

            Exit Sub

        End If

        'If WebBrowser1.Url.ToString <> "http://hsl.seasam.com/ajali/ajali?command=login&com=logout" Then
        '    Main.TopMost = False

        '    MsgBox("Kirjaudu ohjelmasta ensin ulos")
        '    Main.TopMost = True

        '    Exit Sub

        'End If

        Button1.Visible = False
        Button2.Visible = True
        Button3.Visible = True
        Main.tview.Enabled = True

    End Sub
End Class