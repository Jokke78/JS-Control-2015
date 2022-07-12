<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LomaJaVapaaIlmoitukset
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LomaJaVapaaIlmoitukset))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.DGW = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnTallenna = New System.Windows.Forms.Button()
        Me.txtPerustelu = New System.Windows.Forms.RichTextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.LoppuPVM = New System.Windows.Forms.DateTimePicker()
        Me.AlkuPVM = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.rbVapaa = New System.Windows.Forms.RadioButton()
        Me.rbTalvi = New System.Windows.Forms.RadioButton()
        Me.rbKesa = New System.Windows.Forms.RadioButton()
        Me.txtKuljettaja = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.DGW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.txtKuljettaja)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1680, 1046)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DGW)
        Me.GroupBox2.Location = New System.Drawing.Point(497, 226)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(798, 515)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Pidetyt/hyväksytyt vapaat ja lomat"
        '
        'DGW
        '
        Me.DGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGW.Location = New System.Drawing.Point(6, 28)
        Me.DGW.Name = "DGW"
        Me.DGW.Size = New System.Drawing.Size(862, 481)
        Me.DGW.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnTallenna)
        Me.GroupBox1.Controls.Add(Me.txtPerustelu)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.LoppuPVM)
        Me.GroupBox1.Controls.Add(Me.AlkuPVM)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.rbVapaa)
        Me.GroupBox1.Controls.Add(Me.rbTalvi)
        Me.GroupBox1.Controls.Add(Me.rbKesa)
        Me.GroupBox1.Location = New System.Drawing.Point(28, 226)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(463, 515)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Vapaa-/lomatoive"
        '
        'btnTallenna
        '
        Me.btnTallenna.BackColor = System.Drawing.Color.Lime
        Me.btnTallenna.Font = New System.Drawing.Font("Arial Black", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTallenna.Location = New System.Drawing.Point(283, 420)
        Me.btnTallenna.Name = "btnTallenna"
        Me.btnTallenna.Size = New System.Drawing.Size(174, 64)
        Me.btnTallenna.TabIndex = 9
        Me.btnTallenna.Text = "TALLENNA TOIVE"
        Me.btnTallenna.UseVisualStyleBackColor = False
        '
        'txtPerustelu
        '
        Me.txtPerustelu.Location = New System.Drawing.Point(10, 268)
        Me.txtPerustelu.MaxLength = 500
        Me.txtPerustelu.Name = "txtPerustelu"
        Me.txtPerustelu.Size = New System.Drawing.Size(447, 146)
        Me.txtPerustelu.TabIndex = 8
        Me.txtPerustelu.Text = ""
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 243)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(286, 22)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Perustelu ajankohdan tärkeydelle"
        '
        'LoppuPVM
        '
        Me.LoppuPVM.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LoppuPVM.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.LoppuPVM.Location = New System.Drawing.Point(159, 188)
        Me.LoppuPVM.Name = "LoppuPVM"
        Me.LoppuPVM.Size = New System.Drawing.Size(284, 44)
        Me.LoppuPVM.TabIndex = 6
        '
        'AlkuPVM
        '
        Me.AlkuPVM.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AlkuPVM.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.AlkuPVM.Location = New System.Drawing.Point(159, 136)
        Me.AlkuPVM.Name = "AlkuPVM"
        Me.AlkuPVM.Size = New System.Drawing.Size(284, 44)
        Me.AlkuPVM.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 205)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(136, 22)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Päättymispäivä"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 153)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 22)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Alkamispäivä"
        '
        'rbVapaa
        '
        Me.rbVapaa.AutoSize = True
        Me.rbVapaa.Checked = True
        Me.rbVapaa.Location = New System.Drawing.Point(6, 92)
        Me.rbVapaa.Name = "rbVapaa"
        Me.rbVapaa.Size = New System.Drawing.Size(208, 26)
        Me.rbVapaa.TabIndex = 2
        Me.rbVapaa.TabStop = True
        Me.rbVapaa.Text = "VAPAAPÄIVÄTOIVE"
        Me.ToolTip1.SetToolTip(Me.rbVapaa, "Esitän toiveen vapaapäivästä/päivistä. Pekkaspäivä jne.")
        Me.rbVapaa.UseVisualStyleBackColor = True
        '
        'rbTalvi
        '
        Me.rbTalvi.AutoSize = True
        Me.rbTalvi.Font = New System.Drawing.Font("Arial Black", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbTalvi.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.rbTalvi.Location = New System.Drawing.Point(6, 52)
        Me.rbTalvi.Name = "rbTalvi"
        Me.rbTalvi.Size = New System.Drawing.Size(240, 34)
        Me.rbTalvi.TabIndex = 1
        Me.rbTalvi.Text = "TALVILOMATOIVE"
        Me.ToolTip1.SetToolTip(Me.rbTalvi, "Esitän toiveen talviloma ajankohdasta")
        Me.rbTalvi.UseVisualStyleBackColor = True
        '
        'rbKesa
        '
        Me.rbKesa.AutoSize = True
        Me.rbKesa.Enabled = False
        Me.rbKesa.Location = New System.Drawing.Point(6, 28)
        Me.rbKesa.Name = "rbKesa"
        Me.rbKesa.Size = New System.Drawing.Size(192, 26)
        Me.rbKesa.TabIndex = 0
        Me.rbKesa.Text = "KESÄLOMATOIVE"
        Me.ToolTip1.SetToolTip(Me.rbKesa, "Esitän toiveen kesäloman ajankohdasta")
        Me.rbKesa.UseVisualStyleBackColor = True
        '
        'txtKuljettaja
        '
        Me.txtKuljettaja.Enabled = False
        Me.txtKuljettaja.Location = New System.Drawing.Point(143, 191)
        Me.txtKuljettaja.Name = "txtKuljettaja"
        Me.txtKuljettaja.Size = New System.Drawing.Size(272, 29)
        Me.txtKuljettaja.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 194)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 22)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Kuljettaja"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(24, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(989, 66)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(403, 36)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "LOMA- JA VAPAATOIVEET"
        '
        'LomaJaVapaaIlmoitukset
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
        Me.Name = "LomaJaVapaaIlmoitukset"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "LomaJaVapaaIlmoitukset"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.DGW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtKuljettaja As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents DGW As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnTallenna As System.Windows.Forms.Button
    Friend WithEvents txtPerustelu As System.Windows.Forms.RichTextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents LoppuPVM As System.Windows.Forms.DateTimePicker
    Friend WithEvents AlkuPVM As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rbVapaa As System.Windows.Forms.RadioButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents rbTalvi As System.Windows.Forms.RadioButton
    Friend WithEvents rbKesa As System.Windows.Forms.RadioButton
End Class
