<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PysakointiHistoria
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pvm = New System.Windows.Forms.DateTimePicker()
        Me.rbPaiva = New System.Windows.Forms.RadioButton()
        Me.cbAuto = New System.Windows.Forms.ComboBox()
        Me.rbAuto = New System.Windows.Forms.RadioButton()
        Me.cbKuljettaja = New System.Windows.Forms.ComboBox()
        Me.rbKuljettaja = New System.Windows.Forms.RadioButton()
        Me.rbKaikki = New System.Windows.Forms.RadioButton()
        Me.DGW = New System.Windows.Forms.DataGridView()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DGW, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.DGW)
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1058, 964)
        Me.Panel1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.pvm)
        Me.GroupBox1.Controls.Add(Me.rbPaiva)
        Me.GroupBox1.Controls.Add(Me.cbAuto)
        Me.GroupBox1.Controls.Add(Me.rbAuto)
        Me.GroupBox1.Controls.Add(Me.cbKuljettaja)
        Me.GroupBox1.Controls.Add(Me.rbKuljettaja)
        Me.GroupBox1.Controls.Add(Me.rbKaikki)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(309, 413)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Rajaa hakua"
        '
        'pvm
        '
        Me.pvm.Location = New System.Drawing.Point(25, 275)
        Me.pvm.Name = "pvm"
        Me.pvm.Size = New System.Drawing.Size(259, 26)
        Me.pvm.TabIndex = 6
        Me.pvm.Visible = False
        '
        'rbPaiva
        '
        Me.rbPaiva.AutoSize = True
        Me.rbPaiva.Location = New System.Drawing.Point(6, 247)
        Me.rbPaiva.Name = "rbPaiva"
        Me.rbPaiva.Size = New System.Drawing.Size(74, 22)
        Me.rbPaiva.TabIndex = 5
        Me.rbPaiva.TabStop = True
        Me.rbPaiva.Text = "Päivän"
        Me.rbPaiva.UseVisualStyleBackColor = True
        '
        'cbAuto
        '
        Me.cbAuto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAuto.Font = New System.Drawing.Font("Arial Black", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAuto.FormattingEnabled = True
        Me.cbAuto.Location = New System.Drawing.Point(25, 195)
        Me.cbAuto.Name = "cbAuto"
        Me.cbAuto.Size = New System.Drawing.Size(259, 41)
        Me.cbAuto.TabIndex = 4
        Me.cbAuto.Visible = False
        '
        'rbAuto
        '
        Me.rbAuto.AutoSize = True
        Me.rbAuto.Location = New System.Drawing.Point(6, 167)
        Me.rbAuto.Name = "rbAuto"
        Me.rbAuto.Size = New System.Drawing.Size(66, 22)
        Me.rbAuto.TabIndex = 3
        Me.rbAuto.TabStop = True
        Me.rbAuto.Text = "Auton"
        Me.rbAuto.UseVisualStyleBackColor = True
        '
        'cbKuljettaja
        '
        Me.cbKuljettaja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbKuljettaja.Font = New System.Drawing.Font("Arial Black", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbKuljettaja.FormattingEnabled = True
        Me.cbKuljettaja.Location = New System.Drawing.Point(25, 110)
        Me.cbKuljettaja.Name = "cbKuljettaja"
        Me.cbKuljettaja.Size = New System.Drawing.Size(259, 41)
        Me.cbKuljettaja.TabIndex = 2
        Me.cbKuljettaja.Visible = False
        '
        'rbKuljettaja
        '
        Me.rbKuljettaja.AutoSize = True
        Me.rbKuljettaja.Location = New System.Drawing.Point(6, 82)
        Me.rbKuljettaja.Name = "rbKuljettaja"
        Me.rbKuljettaja.Size = New System.Drawing.Size(97, 22)
        Me.rbKuljettaja.TabIndex = 1
        Me.rbKuljettaja.TabStop = True
        Me.rbKuljettaja.Text = "Kuljettajan"
        Me.rbKuljettaja.UseVisualStyleBackColor = True
        '
        'rbKaikki
        '
        Me.rbKaikki.AutoSize = True
        Me.rbKaikki.Location = New System.Drawing.Point(6, 25)
        Me.rbKaikki.Name = "rbKaikki"
        Me.rbKaikki.Size = New System.Drawing.Size(111, 22)
        Me.rbKaikki.TabIndex = 0
        Me.rbKaikki.TabStop = True
        Me.rbKaikki.Text = "Näytä kaikki"
        Me.rbKaikki.UseVisualStyleBackColor = True
        '
        'DGW
        '
        Me.DGW.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGW.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.DGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGW.Location = New System.Drawing.Point(12, 431)
        Me.DGW.Name = "DGW"
        Me.DGW.Size = New System.Drawing.Size(997, 482)
        Me.DGW.TabIndex = 1
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.JS_Control_2015.My.Resources.Resources.pysäköinnin_kartta_uusi
        Me.PictureBox1.Location = New System.Drawing.Point(327, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(682, 422)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'PysakointiHistoria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1058, 964)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PysakointiHistoria"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "PysakointiHistoria"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DGW, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents DGW As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents pvm As System.Windows.Forms.DateTimePicker
    Friend WithEvents rbPaiva As System.Windows.Forms.RadioButton
    Friend WithEvents cbAuto As System.Windows.Forms.ComboBox
    Friend WithEvents rbAuto As System.Windows.Forms.RadioButton
    Friend WithEvents cbKuljettaja As System.Windows.Forms.ComboBox
    Friend WithEvents rbKuljettaja As System.Windows.Forms.RadioButton
    Friend WithEvents rbKaikki As System.Windows.Forms.RadioButton
End Class
