<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.tview = New System.Windows.Forms.TreeView()
        Me.KellonAika = New System.Windows.Forms.Timer(Me.components)
        Me.TulostuskoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KirjauduToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SuljeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TiedostoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SuljeFormejaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.Klo = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.SC = New System.Windows.Forms.SplitContainer()
        Me.MainToolS = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.wLahtolista = New System.Windows.Forms.ToolStripButton()
        Me.w2viikkoa = New System.Windows.Forms.ToolStripButton()
        Me.wAutoInfo = New System.Windows.Forms.ToolStripButton()
        Me.wTiedotteet = New System.Windows.Forms.ToolStripButton()
        Me.wPesuhalli = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.AnnaPalautetta = New System.Windows.Forms.ToolStripLabel()
        Me.txtKeskitys = New System.Windows.Forms.ToolStripTextBox()
        Me.SpostiPohja = New System.Windows.Forms.RichTextBox()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SC.Panel1.SuspendLayout()
        Me.SC.SuspendLayout()
        Me.MainToolS.SuspendLayout()
        Me.SuspendLayout()
        '
        'tview
        '
        Me.tview.BackColor = System.Drawing.Color.White
        Me.tview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tview.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tview.Location = New System.Drawing.Point(0, 0)
        Me.tview.Name = "tview"
        Me.tview.Size = New System.Drawing.Size(307, 1015)
        Me.tview.TabIndex = 0
        '
        'KellonAika
        '
        Me.KellonAika.Enabled = True
        Me.KellonAika.Interval = 1000
        '
        'TulostuskoToolStripMenuItem
        '
        Me.TulostuskoToolStripMenuItem.Name = "TulostuskoToolStripMenuItem"
        Me.TulostuskoToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F10), System.Windows.Forms.Keys)
        Me.TulostuskoToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.TulostuskoToolStripMenuItem.Text = "tulostusko"
        '
        'KirjauduToolStripMenuItem
        '
        Me.KirjauduToolStripMenuItem.Name = "KirjauduToolStripMenuItem"
        Me.KirjauduToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.KirjauduToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.KirjauduToolStripMenuItem.Text = "Kirjaudu"
        '
        'SuljeToolStripMenuItem
        '
        Me.SuljeToolStripMenuItem.Name = "SuljeToolStripMenuItem"
        Me.SuljeToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.F1), System.Windows.Forms.Keys)
        Me.SuljeToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.SuljeToolStripMenuItem.Text = "Sulje"
        '
        'TiedostoToolStripMenuItem
        '
        Me.TiedostoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SuljeToolStripMenuItem, Me.KirjauduToolStripMenuItem, Me.TulostuskoToolStripMenuItem, Me.SuljeFormejaToolStripMenuItem})
        Me.TiedostoToolStripMenuItem.Name = "TiedostoToolStripMenuItem"
        Me.TiedostoToolStripMenuItem.Size = New System.Drawing.Size(65, 20)
        Me.TiedostoToolStripMenuItem.Text = "Tiedosto"
        '
        'SuljeFormejaToolStripMenuItem
        '
        Me.SuljeFormejaToolStripMenuItem.Name = "SuljeFormejaToolStripMenuItem"
        Me.SuljeFormejaToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F7), System.Windows.Forms.Keys)
        Me.SuljeFormejaToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.SuljeFormejaToolStripMenuItem.Text = "sulje formeja"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TiedostoToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1680, 24)
        Me.MenuStrip1.TabIndex = 9
        Me.MenuStrip1.Text = "MenuStrip1"
        Me.MenuStrip1.Visible = False
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Font = New System.Drawing.Font("Courier New", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ToolStripLabel2.ForeColor = System.Drawing.Color.Red
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(373, 28)
        Me.ToolStripLabel2.Text = "paina F1 kirjautuaksesi ohjelmaan"
        '
        'Klo
        '
        Me.Klo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.Klo.Font = New System.Drawing.Font("Courier New", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Klo.ForeColor = System.Drawing.Color.White
        Me.Klo.Name = "Klo"
        Me.Klo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Klo.Size = New System.Drawing.Size(75, 28)
        Me.Klo.Text = "xxxsx"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 31)
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 31)
        Me.ToolStripSeparator5.Visible = False
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 31)
        Me.ToolStripSeparator4.Visible = False
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 31)
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 31)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 31)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 31)
        '
        'SC
        '
        Me.SC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SC.Location = New System.Drawing.Point(0, 0)
        Me.SC.Name = "SC"
        '
        'SC.Panel1
        '
        Me.SC.Panel1.Controls.Add(Me.tview)
        Me.SC.Size = New System.Drawing.Size(1676, 1015)
        Me.SC.SplitterDistance = 307
        Me.SC.TabIndex = 10
        '
        'MainToolS
        '
        Me.MainToolS.BackColor = System.Drawing.Color.Black
        Me.MainToolS.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.MainToolS.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.MainToolS.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton6, Me.ToolStripSeparator1, Me.wLahtolista, Me.ToolStripSeparator2, Me.w2viikkoa, Me.ToolStripSeparator3, Me.wAutoInfo, Me.ToolStripSeparator7, Me.wTiedotteet, Me.ToolStripSeparator4, Me.wPesuhalli, Me.ToolStripSeparator5, Me.ToolStripButton5, Me.ToolStripSeparator6, Me.Klo, Me.AnnaPalautetta, Me.ToolStripLabel2, Me.txtKeskitys})
        Me.MainToolS.Location = New System.Drawing.Point(0, 1015)
        Me.MainToolS.Name = "MainToolS"
        Me.MainToolS.Size = New System.Drawing.Size(1676, 31)
        Me.MainToolS.TabIndex = 8
        Me.MainToolS.Text = "ToolStrip1"
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
        Me.ToolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(28, 28)
        Me.ToolStripButton6.Text = "ToolStripButton6"
        Me.ToolStripButton6.ToolTipText = "KIRJAUDU OHJELMAAN [F1]"
        '
        'wLahtolista
        '
        Me.wLahtolista.BackColor = System.Drawing.Color.Black
        Me.wLahtolista.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.wLahtolista.Image = CType(resources.GetObject("wLahtolista.Image"), System.Drawing.Image)
        Me.wLahtolista.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.wLahtolista.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.wLahtolista.Name = "wLahtolista"
        Me.wLahtolista.Size = New System.Drawing.Size(28, 28)
        Me.wLahtolista.Text = "ToolStripButton2"
        Me.wLahtolista.ToolTipText = "Näytä LÄHTÖLISTA"
        '
        'w2viikkoa
        '
        Me.w2viikkoa.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.w2viikkoa.Image = CType(resources.GetObject("w2viikkoa.Image"), System.Drawing.Image)
        Me.w2viikkoa.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.w2viikkoa.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.w2viikkoa.Name = "w2viikkoa"
        Me.w2viikkoa.Size = New System.Drawing.Size(28, 28)
        Me.w2viikkoa.Text = "ToolStripButton1"
        Me.w2viikkoa.ToolTipText = "Käytä KAHDEN VIIKON TYÖT"
        '
        'wAutoInfo
        '
        Me.wAutoInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.wAutoInfo.Image = CType(resources.GetObject("wAutoInfo.Image"), System.Drawing.Image)
        Me.wAutoInfo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.wAutoInfo.Name = "wAutoInfo"
        Me.wAutoInfo.Size = New System.Drawing.Size(28, 28)
        Me.wAutoInfo.Text = "ToolStripButton1"
        Me.wAutoInfo.ToolTipText = "Näytä AUTOINFO"
        '
        'wTiedotteet
        '
        Me.wTiedotteet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.wTiedotteet.Image = CType(resources.GetObject("wTiedotteet.Image"), System.Drawing.Image)
        Me.wTiedotteet.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.wTiedotteet.Name = "wTiedotteet"
        Me.wTiedotteet.Size = New System.Drawing.Size(28, 28)
        Me.wTiedotteet.Text = "Tiedotteet"
        Me.wTiedotteet.Visible = False
        '
        'wPesuhalli
        '
        Me.wPesuhalli.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.wPesuhalli.Image = CType(resources.GetObject("wPesuhalli.Image"), System.Drawing.Image)
        Me.wPesuhalli.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.wPesuhalli.Name = "wPesuhalli"
        Me.wPesuhalli.Size = New System.Drawing.Size(28, 28)
        Me.wPesuhalli.Text = "Pesuhallin käyttöaste"
        Me.wPesuhalli.Visible = False
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.BackColor = System.Drawing.Color.Black
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(28, 28)
        Me.ToolStripButton5.Text = "ToolStripButton5"
        Me.ToolStripButton5.ToolTipText = "PYSÄYTÄ SIVUJEN VAIHTUMINEN"
        '
        'AnnaPalautetta
        '
        Me.AnnaPalautetta.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.AnnaPalautetta.Font = New System.Drawing.Font("Courier New", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AnnaPalautetta.ForeColor = System.Drawing.Color.White
        Me.AnnaPalautetta.Image = CType(resources.GetObject("AnnaPalautetta.Image"), System.Drawing.Image)
        Me.AnnaPalautetta.Name = "AnnaPalautetta"
        Me.AnnaPalautetta.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.AnnaPalautetta.Size = New System.Drawing.Size(24, 28)
        Me.AnnaPalautetta.ToolTipText = "ANNA PALAUTETTA"
        Me.AnnaPalautetta.Visible = False
        '
        'txtKeskitys
        '
        Me.txtKeskitys.BackColor = System.Drawing.Color.Black
        Me.txtKeskitys.MaxLength = 20
        Me.txtKeskitys.Name = "txtKeskitys"
        Me.txtKeskitys.Size = New System.Drawing.Size(100, 31)
        '
        'SpostiPohja
        '
        Me.SpostiPohja.Location = New System.Drawing.Point(1110, 47)
        Me.SpostiPohja.Name = "SpostiPohja"
        Me.SpostiPohja.Size = New System.Drawing.Size(512, 523)
        Me.SpostiPohja.TabIndex = 12
        Me.SpostiPohja.Text = ""
        Me.SpostiPohja.Visible = False
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1676, 1046)
        Me.ControlBox = false
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.SC)
        Me.Controls.Add(Me.MainToolS)
        Me.Controls.Add(Me.SpostiPohja)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.IsMdiContainer = true
        Me.KeyPreview = true
        Me.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "Main"
        Me.Text = "JS Control 2015"
        Me.TopMost = true
        Me.MenuStrip1.ResumeLayout(false)
        Me.MenuStrip1.PerformLayout
        Me.SC.Panel1.ResumeLayout(false)
        CType(Me.SC,System.ComponentModel.ISupportInitialize).EndInit
        Me.SC.ResumeLayout(false)
        Me.MainToolS.ResumeLayout(false)
        Me.MainToolS.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents tview As System.Windows.Forms.TreeView
    Friend WithEvents KellonAika As System.Windows.Forms.Timer
    Friend WithEvents TulostuskoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KirjauduToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SuljeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TiedostoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents AnnaPalautetta As System.Windows.Forms.ToolStripLabel
    Friend WithEvents Klo As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents wPesuhalli As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents wTiedotteet As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents wAutoInfo As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents w2viikkoa As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents wLahtolista As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripButton
    Friend WithEvents SC As System.Windows.Forms.SplitContainer
    Friend WithEvents MainToolS As System.Windows.Forms.ToolStrip
    Friend WithEvents SuljeFormejaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SpostiPohja As System.Windows.Forms.RichTextBox
    Friend WithEvents txtKeskitys As System.Windows.Forms.ToolStripTextBox

End Class
