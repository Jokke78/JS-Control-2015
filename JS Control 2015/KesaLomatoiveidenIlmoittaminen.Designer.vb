<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KesaLomatoiveidenIlmoittaminen
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(KesaLomatoiveidenIlmoittaminen))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.loppupvm = New System.Windows.Forms.Label()
        Me.alkuPVM = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.valittuaika = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.vk1 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.vk2 = New System.Windows.Forms.Button()
        Me.txtperustelu = New System.Windows.Forms.RichTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.vk5 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.vk3 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.vk4 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.loppupvm)
        Me.Panel1.Controls.Add(Me.alkuPVM)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1058, 964)
        Me.Panel1.TabIndex = 0
        '
        'loppupvm
        '
        Me.loppupvm.AutoSize = True
        Me.loppupvm.Location = New System.Drawing.Point(190, 454)
        Me.loppupvm.Name = "loppupvm"
        Me.loppupvm.Size = New System.Drawing.Size(0, 24)
        Me.loppupvm.TabIndex = 17
        Me.loppupvm.Visible = False
        '
        'alkuPVM
        '
        Me.alkuPVM.AutoSize = True
        Me.alkuPVM.Location = New System.Drawing.Point(45, 454)
        Me.alkuPVM.Name = "alkuPVM"
        Me.alkuPVM.Size = New System.Drawing.Size(0, 24)
        Me.alkuPVM.TabIndex = 16
        Me.alkuPVM.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.JS_Control_2015.My.Resources.Resources.lomakierto
        Me.PictureBox1.Location = New System.Drawing.Point(566, 303)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(443, 165)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.valittuaika)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.vk1)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.vk2)
        Me.GroupBox1.Controls.Add(Me.txtperustelu)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.vk5)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.vk3)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.vk4)
        Me.GroupBox1.Location = New System.Drawing.Point(21, 507)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(953, 425)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "VALITSE LOMATOIVE VIIDESTÄ VAIHTOEHDOSTA"
        '
        'valittuaika
        '
        Me.valittuaika.AutoSize = True
        Me.valittuaika.Font = New System.Drawing.Font("Arial Black", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.valittuaika.Location = New System.Drawing.Point(17, 371)
        Me.valittuaika.Name = "valittuaika"
        Me.valittuaika.Size = New System.Drawing.Size(0, 38)
        Me.valittuaika.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(17, 339)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(90, 24)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "VALITTU"
        '
        'vk1
        '
        Me.vk1.Font = New System.Drawing.Font("Arial Black", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vk1.Location = New System.Drawing.Point(18, 31)
        Me.vk1.Name = "vk1"
        Me.vk1.Size = New System.Drawing.Size(114, 64)
        Me.vk1.TabIndex = 3
        Me.vk1.Text = "19"
        Me.vk1.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button1.Font = New System.Drawing.Font("Arial Black", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(521, 338)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(426, 72)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "TALLENNA LOMATOIVE"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(14, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(149, 66)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "LOMA VK 19-22" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "4.5. - 30.5.2015" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & """TOUKOKUU"""
        '
        'vk2
        '
        Me.vk2.Font = New System.Drawing.Font("Arial Black", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vk2.Location = New System.Drawing.Point(190, 31)
        Me.vk2.Name = "vk2"
        Me.vk2.Size = New System.Drawing.Size(114, 64)
        Me.vk2.TabIndex = 4
        Me.vk2.Text = "23"
        Me.vk2.UseVisualStyleBackColor = True
        '
        'txtperustelu
        '
        Me.txtperustelu.BackColor = System.Drawing.Color.LightGray
        Me.txtperustelu.Location = New System.Drawing.Point(17, 206)
        Me.txtperustelu.Name = "txtperustelu"
        Me.txtperustelu.Size = New System.Drawing.Size(930, 126)
        Me.txtperustelu.TabIndex = 14
        Me.txtperustelu.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(731, 98)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(161, 66)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "LOMA VK 35-38" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "24.8.  - 19.9.2015" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & """SYYSKUU"""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial Black", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(13, 173)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(489, 30)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Perusteluni loma-ajankohdan tärkeydelle:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(186, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(149, 66)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "LOMA VK 23-36" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "1.6. - 27.6.2015" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & """KESÄKUU"""
        '
        'vk5
        '
        Me.vk5.Font = New System.Drawing.Font("Arial Black", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vk5.Location = New System.Drawing.Point(735, 31)
        Me.vk5.Name = "vk5"
        Me.vk5.Size = New System.Drawing.Size(114, 64)
        Me.vk5.TabIndex = 7
        Me.vk5.Text = "35"
        Me.vk5.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(543, 98)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(156, 66)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "LOMA VK 31-34" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "27.7. - 22.8.2015" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & """ELOKUU"""
        '
        'vk3
        '
        Me.vk3.Font = New System.Drawing.Font("Arial Black", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vk3.Location = New System.Drawing.Point(366, 31)
        Me.vk3.Name = "vk3"
        Me.vk3.Size = New System.Drawing.Size(114, 64)
        Me.vk3.TabIndex = 5
        Me.vk3.Text = "27"
        Me.vk3.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(362, 98)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(156, 66)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "LOMA VK 27-30" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "29.6. - 25.7.2015" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & """HEINÄKUU"""
        '
        'vk4
        '
        Me.vk4.Font = New System.Drawing.Font("Arial Black", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vk4.Location = New System.Drawing.Point(547, 31)
        Me.vk4.Name = "vk4"
        Me.vk4.Size = New System.Drawing.Size(114, 64)
        Me.vk4.TabIndex = 6
        Me.vk4.Text = "31"
        Me.vk4.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(35, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(751, 336)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial Black", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(850, 50)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "KESÄLOMATOIVEEN ILMOITTAMINEN 2015"
        '
        'KesaLomatoiveidenIlmoittaminen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(12.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1058, 964)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(6)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "KesaLomatoiveidenIlmoittaminen"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "KesaLomatoiveidenIlmoittaminen"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtperustelu As System.Windows.Forms.RichTextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents vk5 As System.Windows.Forms.Button
    Friend WithEvents vk4 As System.Windows.Forms.Button
    Friend WithEvents vk3 As System.Windows.Forms.Button
    Friend WithEvents vk2 As System.Windows.Forms.Button
    Friend WithEvents vk1 As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents valittuaika As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents loppupvm As System.Windows.Forms.Label
    Friend WithEvents alkuPVM As System.Windows.Forms.Label
End Class
