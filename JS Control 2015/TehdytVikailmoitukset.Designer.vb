<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TehdytVikailmoitukset
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TehdytVikailmoitukset))
        Me.Viat = New System.Windows.Forms.ListBox()
        Me.IlmoitetutViat = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.vikaToTxt = New System.Windows.Forms.ToolStripMenuItem()
        Me.RekNrotoTXT = New System.Windows.Forms.ToolStripMenuItem()
        Me.IlmNimi = New System.Windows.Forms.ToolStripMenuItem()
        Me.IlmPuhNro = New System.Windows.Forms.ToolStripMenuItem()
        Me.IlmjaAikaa = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btPoista = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.VikojaYHT = New System.Windows.Forms.ToolStripLabel()
        Me.cbrekNro = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.cbShowLimit = New System.Windows.Forms.ToolStripComboBox()
        Me.IlmoitetutViat.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Viat
        '
        Me.Viat.ContextMenuStrip = Me.IlmoitetutViat
        Me.Viat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Viat.Font = New System.Drawing.Font("Courier New", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Viat.FormattingEnabled = True
        Me.Viat.ItemHeight = 27
        Me.Viat.Location = New System.Drawing.Point(3, 79)
        Me.Viat.Name = "Viat"
        Me.Viat.Size = New System.Drawing.Size(1271, 918)
        Me.Viat.TabIndex = 3
        '
        'IlmoitetutViat
        '
        Me.IlmoitetutViat.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.vikaToTxt, Me.RekNrotoTXT, Me.IlmNimi, Me.IlmPuhNro, Me.IlmjaAikaa, Me.ToolStripMenuItem1, Me.btPoista})
        Me.IlmoitetutViat.Name = "IlmoitetutViat"
        Me.IlmoitetutViat.Size = New System.Drawing.Size(254, 190)
        '
        'vikaToTxt
        '
        Me.vikaToTxt.Font = New System.Drawing.Font("Arial Narrow", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vikaToTxt.Image = CType(resources.GetObject("vikaToTxt.Image"), System.Drawing.Image)
        Me.vikaToTxt.Name = "vikaToTxt"
        Me.vikaToTxt.Size = New System.Drawing.Size(253, 30)
        Me.vikaToTxt.Text = "Vikatxt"
        '
        'RekNrotoTXT
        '
        Me.RekNrotoTXT.Font = New System.Drawing.Font("Arial Narrow", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RekNrotoTXT.Image = CType(resources.GetObject("RekNrotoTXT.Image"), System.Drawing.Image)
        Me.RekNrotoTXT.Name = "RekNrotoTXT"
        Me.RekNrotoTXT.Size = New System.Drawing.Size(253, 30)
        Me.RekNrotoTXT.Text = "RekNro"
        '
        'IlmNimi
        '
        Me.IlmNimi.Font = New System.Drawing.Font("Arial Narrow", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IlmNimi.Image = CType(resources.GetObject("IlmNimi.Image"), System.Drawing.Image)
        Me.IlmNimi.Name = "IlmNimi"
        Me.IlmNimi.Size = New System.Drawing.Size(253, 30)
        Me.IlmNimi.Text = "IlmNimi"
        '
        'IlmPuhNro
        '
        Me.IlmPuhNro.Font = New System.Drawing.Font("Arial Narrow", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IlmPuhNro.Image = CType(resources.GetObject("IlmPuhNro.Image"), System.Drawing.Image)
        Me.IlmPuhNro.Name = "IlmPuhNro"
        Me.IlmPuhNro.Size = New System.Drawing.Size(253, 30)
        Me.IlmPuhNro.Text = "IlmPuh"
        '
        'IlmjaAikaa
        '
        Me.IlmjaAikaa.Font = New System.Drawing.Font("Arial Narrow", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IlmjaAikaa.Image = CType(resources.GetObject("IlmjaAikaa.Image"), System.Drawing.Image)
        Me.IlmjaAikaa.Name = "IlmjaAikaa"
        Me.IlmjaAikaa.Size = New System.Drawing.Size(253, 30)
        Me.IlmjaAikaa.Text = "IlmPvm+aikaa"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(250, 6)
        '
        'btPoista
        '
        Me.btPoista.Font = New System.Drawing.Font("Arial Narrow", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btPoista.Name = "btPoista"
        Me.btPoista.Size = New System.Drawing.Size(253, 30)
        Me.btPoista.Text = "Poista tietokannasta"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1277, 1000)
        Me.Panel1.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Viat, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ToolStrip1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1277, 1000)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ToolStrip1.Font = New System.Drawing.Font("Arial Narrow", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VikojaYHT, Me.cbrekNro, Me.ToolStripSeparator1, Me.ToolStripLabel1, Me.cbShowLimit})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1277, 76)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'VikojaYHT
        '
        Me.VikojaYHT.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.VikojaYHT.Font = New System.Drawing.Font("Arial Narrow", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VikojaYHT.ForeColor = System.Drawing.Color.Black
        Me.VikojaYHT.Name = "VikojaYHT"
        Me.VikojaYHT.Size = New System.Drawing.Size(219, 73)
        Me.VikojaYHT.Text = "ToolStripLabel1"
        Me.VikojaYHT.ToolTipText = "Korjaamattomia vikoja yhteensä"
        '
        'cbrekNro
        '
        Me.cbrekNro.AutoSize = False
        Me.cbrekNro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbrekNro.DropDownWidth = 120
        Me.cbrekNro.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.cbrekNro.Font = New System.Drawing.Font("Arial Narrow", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbrekNro.IntegralHeight = False
        Me.cbrekNro.MaxDropDownItems = 15
        Me.cbrekNro.MaxLength = 120
        Me.cbrekNro.Name = "cbrekNro"
        Me.cbrekNro.Size = New System.Drawing.Size(160, 50)
        Me.cbrekNro.Sorted = True
        Me.cbrekNro.ToolTipText = "Näytä valitun auton viat"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 76)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(323, 73)
        Me.ToolStripLabel1.Text = "Näytettävien vikojen määrä max"
        '
        'cbShowLimit
        '
        Me.cbShowLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbShowLimit.Font = New System.Drawing.Font("Arial Black", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbShowLimit.Items.AddRange(New Object() {"10", "30", "50", "100", "200", "300"})
        Me.cbShowLimit.Name = "cbShowLimit"
        Me.cbShowLimit.Size = New System.Drawing.Size(121, 76)
        '
        'TehdytVikailmoitukset
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1277, 1000)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TehdytVikailmoitukset"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "TehdytVikailmoitukset"
        Me.IlmoitetutViat.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Viat As System.Windows.Forms.ListBox
    Friend WithEvents IlmoitetutViat As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents vikaToTxt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RekNrotoTXT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IlmNimi As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IlmPuhNro As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IlmjaAikaa As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btPoista As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents VikojaYHT As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cbrekNro As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cbShowLimit As System.Windows.Forms.ToolStripComboBox
End Class
