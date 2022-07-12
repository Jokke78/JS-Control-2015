<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TeeVikailmoitus
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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.postiteksti = New System.Windows.Forms.RichTextBox()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.HH = New System.Windows.Forms.RichTextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DGV = New System.Windows.Forms.DataGridView()
        Me.btTallenna = New System.Windows.Forms.Button()
        Me.Lisättävävika = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbViat = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtREK = New System.Windows.Forms.ComboBox()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txtIlmoittaja = New System.Windows.Forms.TextBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DGV, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(3, 206)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(6, 44)
        Me.Button1.TabIndex = 38
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.postiteksti, 0, 4)
        Me.TableLayoutPanel3.Controls.Add(Me.Button1, 0, 1)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(3, 24)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 5
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(12, 1019)
        Me.TableLayoutPanel3.TabIndex = 38
        '
        'postiteksti
        '
        Me.postiteksti.Location = New System.Drawing.Point(3, 815)
        Me.postiteksti.Name = "postiteksti"
        Me.postiteksti.Size = New System.Drawing.Size(6, 191)
        Me.postiteksti.TabIndex = 39
        Me.postiteksti.Text = ""
        Me.postiteksti.Visible = False
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.40462!))
        Me.TableLayoutPanel2.Controls.Add(Me.HH, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.609271!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.39073!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(1489, 922)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'HH
        '
        Me.HH.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HH.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HH.Location = New System.Drawing.Point(3, 82)
        Me.HH.Name = "HH"
        Me.HH.Size = New System.Drawing.Size(1483, 837)
        Me.HH.TabIndex = 0
        Me.HH.Text = ""
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TableLayoutPanel2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 41)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1495, 928)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "HUOLTOHISTORIAT"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DGV
        '
        Me.DGV.AllowUserToAddRows = False
        Me.DGV.AllowUserToDeleteRows = False
        Me.DGV.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV.Location = New System.Drawing.Point(10, 697)
        Me.DGV.Name = "DGV"
        Me.DGV.Size = New System.Drawing.Size(1479, 268)
        Me.DGV.TabIndex = 36
        '
        'btTallenna
        '
        Me.btTallenna.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.btTallenna.Font = New System.Drawing.Font("Courier New", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btTallenna.ForeColor = System.Drawing.Color.Black
        Me.btTallenna.Location = New System.Drawing.Point(12, 622)
        Me.btTallenna.Name = "btTallenna"
        Me.btTallenna.Size = New System.Drawing.Size(175, 60)
        Me.btTallenna.TabIndex = 35
        Me.btTallenna.Text = "TALLENNA"
        Me.btTallenna.UseVisualStyleBackColor = False
        Me.btTallenna.Visible = False
        '
        'Lisättävävika
        '
        Me.Lisättävävika.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.Lisättävävika.Location = New System.Drawing.Point(10, 564)
        Me.Lisättävävika.Name = "Lisättävävika"
        Me.Lisättävävika.Size = New System.Drawing.Size(1013, 39)
        Me.Lisättävävika.TabIndex = 34
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(6, 529)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(210, 32)
        Me.Label3.TabIndex = 33
        Me.Label3.Text = "Ilmoitettava vika"
        '
        'lbViat
        '
        Me.lbViat.DisplayMember = "VIKA"
        Me.lbViat.Enabled = False
        Me.lbViat.Font = New System.Drawing.Font("Courier New", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbViat.FormattingEnabled = True
        Me.lbViat.ItemHeight = 30
        Me.lbViat.Location = New System.Drawing.Point(6, 63)
        Me.lbViat.Name = "lbViat"
        Me.lbViat.ScrollAlwaysVisible = True
        Me.lbViat.Size = New System.Drawing.Size(1483, 424)
        Me.lbViat.Sorted = True
        Me.lbViat.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(417, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(124, 32)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Ilmoittaja"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 32)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "REK-NRO"
        '
        'txtREK
        '
        Me.txtREK.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtREK.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.txtREK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.txtREK.FormattingEnabled = True
        Me.txtREK.Location = New System.Drawing.Point(152, 23)
        Me.txtREK.MaxLength = 7
        Me.txtREK.Name = "txtREK"
        Me.txtREK.Size = New System.Drawing.Size(170, 40)
        Me.txtREK.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DGV)
        Me.TabPage1.Controls.Add(Me.btTallenna)
        Me.TabPage1.Controls.Add(Me.Lisättävävika)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.lbViat)
        Me.TabPage1.Controls.Add(Me.txtIlmoittaja)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.txtREK)
        Me.TabPage1.Location = New System.Drawing.Point(4, 41)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1648, 974)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "TEE VIKAILMOITUS"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txtIlmoittaja
        '
        Me.txtIlmoittaja.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtIlmoittaja.Enabled = False
        Me.txtIlmoittaja.Location = New System.Drawing.Point(560, 23)
        Me.txtIlmoittaja.Name = "txtIlmoittaja"
        Me.txtIlmoittaja.Size = New System.Drawing.Size(259, 39)
        Me.txtIlmoittaja.TabIndex = 5
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Arial", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(21, 24)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1656, 1019)
        Me.TabControl1.TabIndex = 37
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 18.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TabControl1, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1680, 1046)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1680, 1046)
        Me.Panel1.TabIndex = 1
        '
        'TeeVikailmoitus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1680, 1046)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TeeVikailmoitus"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "TeeVikailmoitus"
        Me.TableLayoutPanel3.ResumeLayout(false)
        Me.TableLayoutPanel2.ResumeLayout(false)
        Me.TabPage2.ResumeLayout(false)
        CType(Me.DGV,System.ComponentModel.ISupportInitialize).EndInit
        Me.TabPage1.ResumeLayout(false)
        Me.TabPage1.PerformLayout
        Me.TabControl1.ResumeLayout(false)
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.Panel1.ResumeLayout(false)
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents postiteksti As System.Windows.Forms.RichTextBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DGV As System.Windows.Forms.DataGridView
    Friend WithEvents btTallenna As System.Windows.Forms.Button
    Friend WithEvents Lisättävävika As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbViat As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtREK As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents txtIlmoittaja As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents HH As System.Windows.Forms.RichTextBox
End Class
