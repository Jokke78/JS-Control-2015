<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UudetTyoOhjeet
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
        Me.TulostaTyoOhje = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TulostaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PanelTyoOhjae = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.FLP1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GB_TehdytVikailmoitukset = New System.Windows.Forms.GroupBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DGW_Viat = New System.Windows.Forms.DataGridView()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DGW_TMliitetytviat = New System.Windows.Forms.DataGridView()
        Me.cbTyoVuoro = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pvm = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnTulosta = New System.Windows.Forms.Button()
        Me.TulostaTyoOhje.SuspendLayout()
        Me.PanelTyoOhjae.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GB_TehdytVikailmoitukset.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DGW_Viat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.DGW_TMliitetytviat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TulostaTyoOhje
        '
        Me.TulostaTyoOhje.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TulostaToolStripMenuItem})
        Me.TulostaTyoOhje.Name = "TulostaTyoOhje"
        Me.TulostaTyoOhje.Size = New System.Drawing.Size(162, 26)
        '
        'TulostaToolStripMenuItem
        '
        Me.TulostaToolStripMenuItem.Name = "TulostaToolStripMenuItem"
        Me.TulostaToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.TulostaToolStripMenuItem.Text = "Tulosta paperille"
        '
        'PanelTyoOhjae
        '
        Me.PanelTyoOhjae.Controls.Add(Me.SplitContainer1)
        Me.PanelTyoOhjae.Location = New System.Drawing.Point(446, 12)
        Me.PanelTyoOhjae.Name = "PanelTyoOhjae"
        Me.PanelTyoOhjae.Size = New System.Drawing.Size(763, 1022)
        Me.PanelTyoOhjae.TabIndex = 72
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.FLP1)
        Me.SplitContainer1.Panel1MinSize = 20
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.PictureBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GB_TehdytVikailmoitukset)
        Me.SplitContainer1.Size = New System.Drawing.Size(763, 1022)
        Me.SplitContainer1.SplitterDistance = 52
        Me.SplitContainer1.SplitterWidth = 1
        Me.SplitContainer1.TabIndex = 1
        '
        'FLP1
        '
        Me.FLP1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FLP1.Location = New System.Drawing.Point(0, 0)
        Me.FLP1.Margin = New System.Windows.Forms.Padding(0)
        Me.FLP1.Name = "FLP1"
        Me.FLP1.Size = New System.Drawing.Size(763, 52)
        Me.FLP1.TabIndex = 0
        '
        'PictureBox1
        '
        Me.PictureBox1.ContextMenuStrip = Me.TulostaTyoOhje
        Me.PictureBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(763, 969)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'GB_TehdytVikailmoitukset
        '
        Me.GB_TehdytVikailmoitukset.BackColor = System.Drawing.Color.Transparent
        Me.GB_TehdytVikailmoitukset.Controls.Add(Me.TabControl1)
        Me.GB_TehdytVikailmoitukset.Location = New System.Drawing.Point(97, 424)
        Me.GB_TehdytVikailmoitukset.Name = "GB_TehdytVikailmoitukset"
        Me.GB_TehdytVikailmoitukset.Size = New System.Drawing.Size(262, 81)
        Me.GB_TehdytVikailmoitukset.TabIndex = 36
        Me.GB_TehdytVikailmoitukset.TabStop = False
        Me.GB_TehdytVikailmoitukset.Text = "Auton viat"
        Me.GB_TehdytVikailmoitukset.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(6, 25)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(532, 187)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DGW_Viat)
        Me.TabPage1.Location = New System.Drawing.Point(4, 33)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(524, 150)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Tehdyt vikailmoitukset"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'DGW_Viat
        '
        Me.DGW_Viat.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DGW_Viat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGW_Viat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGW_Viat.Location = New System.Drawing.Point(3, 3)
        Me.DGW_Viat.Name = "DGW_Viat"
        Me.DGW_Viat.Size = New System.Drawing.Size(518, 144)
        Me.DGW_Viat.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DGW_TMliitetytviat)
        Me.TabPage2.Location = New System.Drawing.Point(4, 33)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(524, 150)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Työmääräyksiin liitetyt viat"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DGW_TMliitetytviat
        '
        Me.DGW_TMliitetytviat.BackgroundColor = System.Drawing.SystemColors.ActiveCaption
        Me.DGW_TMliitetytviat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGW_TMliitetytviat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGW_TMliitetytviat.Location = New System.Drawing.Point(3, 3)
        Me.DGW_TMliitetytviat.Name = "DGW_TMliitetytviat"
        Me.DGW_TMliitetytviat.Size = New System.Drawing.Size(518, 144)
        Me.DGW_TMliitetytviat.TabIndex = 1
        '
        'cbTyoVuoro
        '
        Me.cbTyoVuoro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTyoVuoro.Font = New System.Drawing.Font("Arial Narrow", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTyoVuoro.FormattingEnabled = True
        Me.cbTyoVuoro.IntegralHeight = False
        Me.cbTyoVuoro.Location = New System.Drawing.Point(235, 72)
        Me.cbTyoVuoro.MaxDropDownItems = 10
        Me.cbTyoVuoro.Name = "cbTyoVuoro"
        Me.cbTyoVuoro.Size = New System.Drawing.Size(157, 33)
        Me.cbTyoVuoro.TabIndex = 74
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 158)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(282, 19)
        Me.Label2.TabIndex = 75
        Me.Label2.Text = "TYÖVUORON VOIMASSAOLOPÄIVÄ"
        '
        'pvm
        '
        Me.pvm.Location = New System.Drawing.Point(16, 180)
        Me.pvm.Name = "pvm"
        Me.pvm.Size = New System.Drawing.Size(278, 32)
        Me.pvm.TabIndex = 76
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(217, 24)
        Me.Label1.TabIndex = 77
        Me.Label1.Text = "VALITSE TYÖVUORO"
        '
        'btnTulosta
        '
        Me.btnTulosta.Location = New System.Drawing.Point(113, 303)
        Me.btnTulosta.Name = "btnTulosta"
        Me.btnTulosta.Size = New System.Drawing.Size(205, 43)
        Me.btnTulosta.TabIndex = 78
        Me.btnTulosta.Text = "TULOSTA"
        Me.btnTulosta.UseVisualStyleBackColor = True
        Me.btnTulosta.Visible = False
        '
        'UudetTyoOhjeet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(13.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1680, 1046)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnTulosta)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pvm)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cbTyoVuoro)
        Me.Controls.Add(Me.PanelTyoOhjae)
        Me.Font = New System.Drawing.Font("Arial", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "UudetTyoOhjeet"
        Me.ShowInTaskbar = false
        Me.Text = "UudetTyoOhjeet"
        Me.TulostaTyoOhje.ResumeLayout(false)
        Me.PanelTyoOhjae.ResumeLayout(false)
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.ResumeLayout(false)
        CType(Me.PictureBox1,System.ComponentModel.ISupportInitialize).EndInit
        Me.GB_TehdytVikailmoitukset.ResumeLayout(false)
        Me.TabControl1.ResumeLayout(false)
        Me.TabPage1.ResumeLayout(false)
        CType(Me.DGW_Viat,System.ComponentModel.ISupportInitialize).EndInit
        Me.TabPage2.ResumeLayout(false)
        CType(Me.DGW_TMliitetytviat,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents TulostaTyoOhje As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TulostaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PanelTyoOhjae As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents FLP1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GB_TehdytVikailmoitukset As System.Windows.Forms.GroupBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents DGW_Viat As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DGW_TMliitetytviat As System.Windows.Forms.DataGridView
    Friend WithEvents cbTyoVuoro As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pvm As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnTulosta As System.Windows.Forms.Button
End Class
