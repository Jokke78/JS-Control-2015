<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Kalusto
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
        Me.raporteissaEI = New System.Windows.Forms.RadioButton()
        Me.vKatsVahtiEI = New System.Windows.Forms.RadioButton()
        Me.vKatsVahtiKYLLA = New System.Windows.Forms.RadioButton()
        Me.piilorekistPVM = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TSEIKYLLÄ = New System.Windows.Forms.RadioButton()
        Me.TSkaikki = New System.Windows.Forms.RadioButton()
        Me.TSvoimassa = New System.Windows.Forms.RadioButton()
        Me.vNastat = New System.Windows.Forms.RadioButton()
        Me.vTalvi = New System.Windows.Forms.RadioButton()
        Me.raporteissakylla = New System.Windows.Forms.RadioButton()
        Me.vKesa = New System.Windows.Forms.RadioButton()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.btnLisaaUusiAuto = New System.Windows.Forms.Button()
        Me.UusiRekistPVM = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.UusiRekNro = New System.Windows.Forms.TextBox()
        Me.UuusiAutoNro = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.DGW = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.vRekkari = New System.Windows.Forms.Label()
        Me.vVastuuKuli = New System.Windows.Forms.ComboBox()
        Me.lanlalas = New System.Windows.Forms.Label()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.vRekPVM = New System.Windows.Forms.DateTimePicker()
        Me.vAutoNro = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.GroupBox4.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DGW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'raporteissaEI
        '
        Me.raporteissaEI.AutoSize = True
        Me.raporteissaEI.Location = New System.Drawing.Point(93, 27)
        Me.raporteissaEI.Margin = New System.Windows.Forms.Padding(4)
        Me.raporteissaEI.Name = "raporteissaEI"
        Me.raporteissaEI.Size = New System.Drawing.Size(40, 22)
        Me.raporteissaEI.TabIndex = 1
        Me.raporteissaEI.TabStop = True
        Me.raporteissaEI.Text = "EI"
        Me.raporteissaEI.UseVisualStyleBackColor = True
        '
        'vKatsVahtiEI
        '
        Me.vKatsVahtiEI.AutoSize = True
        Me.vKatsVahtiEI.Location = New System.Drawing.Point(107, 27)
        Me.vKatsVahtiEI.Margin = New System.Windows.Forms.Padding(4)
        Me.vKatsVahtiEI.Name = "vKatsVahtiEI"
        Me.vKatsVahtiEI.Size = New System.Drawing.Size(63, 22)
        Me.vKatsVahtiEI.TabIndex = 1
        Me.vKatsVahtiEI.TabStop = True
        Me.vKatsVahtiEI.Text = "POIS"
        Me.vKatsVahtiEI.UseVisualStyleBackColor = True
        '
        'vKatsVahtiKYLLA
        '
        Me.vKatsVahtiKYLLA.AutoSize = True
        Me.vKatsVahtiKYLLA.Location = New System.Drawing.Point(8, 27)
        Me.vKatsVahtiKYLLA.Margin = New System.Windows.Forms.Padding(4)
        Me.vKatsVahtiKYLLA.Name = "vKatsVahtiKYLLA"
        Me.vKatsVahtiKYLLA.Size = New System.Drawing.Size(91, 22)
        Me.vKatsVahtiKYLLA.TabIndex = 0
        Me.vKatsVahtiKYLLA.TabStop = True
        Me.vKatsVahtiKYLLA.Text = "Käytössä"
        Me.vKatsVahtiKYLLA.UseVisualStyleBackColor = True
        '
        'piilorekistPVM
        '
        Me.piilorekistPVM.AutoSize = True
        Me.piilorekistPVM.Location = New System.Drawing.Point(11, 172)
        Me.piilorekistPVM.Name = "piilorekistPVM"
        Me.piilorekistPVM.Size = New System.Drawing.Size(56, 18)
        Me.piilorekistPVM.TabIndex = 22
        Me.piilorekistPVM.Text = "Label4"
        Me.piilorekistPVM.Visible = False
        '
        'Button1
        '
        Me.Button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Button1.BackColor = System.Drawing.Color.Lime
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button1.Font = New System.Drawing.Font("Courier New", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(7, 211)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(134, 51)
        Me.Button1.TabIndex = 21
        Me.Button1.Text = "TALLENNA"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Button2)
        Me.GroupBox4.Controls.Add(Me.TSEIKYLLÄ)
        Me.GroupBox4.Controls.Add(Me.TSkaikki)
        Me.GroupBox4.Controls.Add(Me.TSvoimassa)
        Me.GroupBox4.Location = New System.Drawing.Point(717, 7)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox4.Size = New System.Drawing.Size(278, 136)
        Me.GroupBox4.TabIndex = 20
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Näytä"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(160, 36)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(108, 51)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "PÄIVITÄ"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TSEIKYLLÄ
        '
        Me.TSEIKYLLÄ.AutoSize = True
        Me.TSEIKYLLÄ.Location = New System.Drawing.Point(12, 101)
        Me.TSEIKYLLÄ.Margin = New System.Windows.Forms.Padding(4)
        Me.TSEIKYLLÄ.Name = "TSEIKYLLÄ"
        Me.TSEIKYLLÄ.Size = New System.Drawing.Size(165, 22)
        Me.TSEIKYLLÄ.TabIndex = 2
        Me.TSEIKYLLÄ.Text = "Rapoteista poistetut"
        Me.TSEIKYLLÄ.UseVisualStyleBackColor = True
        '
        'TSkaikki
        '
        Me.TSkaikki.AutoSize = True
        Me.TSkaikki.Checked = True
        Me.TSkaikki.Location = New System.Drawing.Point(12, 74)
        Me.TSkaikki.Margin = New System.Windows.Forms.Padding(4)
        Me.TSkaikki.Name = "TSkaikki"
        Me.TSkaikki.Size = New System.Drawing.Size(76, 22)
        Me.TSkaikki.TabIndex = 1
        Me.TSkaikki.TabStop = True
        Me.TSkaikki.Text = "KAIKKI"
        Me.TSkaikki.UseVisualStyleBackColor = True
        '
        'TSvoimassa
        '
        Me.TSvoimassa.AutoSize = True
        Me.TSvoimassa.Location = New System.Drawing.Point(12, 42)
        Me.TSvoimassa.Margin = New System.Windows.Forms.Padding(4)
        Me.TSvoimassa.Name = "TSvoimassa"
        Me.TSvoimassa.Size = New System.Drawing.Size(140, 22)
        Me.TSvoimassa.TabIndex = 0
        Me.TSvoimassa.Text = "Vain raporteissa"
        Me.TSvoimassa.UseVisualStyleBackColor = True
        '
        'vNastat
        '
        Me.vNastat.AutoSize = True
        Me.vNastat.Location = New System.Drawing.Point(170, 27)
        Me.vNastat.Margin = New System.Windows.Forms.Padding(4)
        Me.vNastat.Name = "vNastat"
        Me.vNastat.Size = New System.Drawing.Size(86, 22)
        Me.vNastat.TabIndex = 2
        Me.vNastat.TabStop = True
        Me.vNastat.Text = "NASTAT"
        Me.vNastat.UseVisualStyleBackColor = True
        '
        'vTalvi
        '
        Me.vTalvi.AutoSize = True
        Me.vTalvi.Location = New System.Drawing.Point(86, 27)
        Me.vTalvi.Margin = New System.Windows.Forms.Padding(4)
        Me.vTalvi.Name = "vTalvi"
        Me.vTalvi.Size = New System.Drawing.Size(79, 22)
        Me.vTalvi.TabIndex = 1
        Me.vTalvi.TabStop = True
        Me.vTalvi.Text = "KITKAT"
        Me.vTalvi.UseVisualStyleBackColor = True
        '
        'raporteissakylla
        '
        Me.raporteissakylla.AutoSize = True
        Me.raporteissakylla.Location = New System.Drawing.Point(8, 27)
        Me.raporteissakylla.Margin = New System.Windows.Forms.Padding(4)
        Me.raporteissakylla.Name = "raporteissakylla"
        Me.raporteissakylla.Size = New System.Drawing.Size(68, 22)
        Me.raporteissakylla.TabIndex = 0
        Me.raporteissakylla.TabStop = True
        Me.raporteissakylla.Text = "Näkyy"
        Me.raporteissakylla.UseVisualStyleBackColor = True
        '
        'vKesa
        '
        Me.vKesa.AutoSize = True
        Me.vKesa.Location = New System.Drawing.Point(8, 27)
        Me.vKesa.Margin = New System.Windows.Forms.Padding(4)
        Me.vKesa.Name = "vKesa"
        Me.vKesa.Size = New System.Drawing.Size(70, 22)
        Me.vKesa.TabIndex = 0
        Me.vKesa.TabStop = True
        Me.vKesa.Text = "KESÄ"
        Me.vKesa.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.BackgroundImage = Global.JS_Control_2015.My.Resources.Resources.Taustakuva1
        Me.TabPage3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabPage3.Location = New System.Drawing.Point(4, 27)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(1002, 269)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "DRIVECO"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.BackgroundImage = Global.JS_Control_2015.My.Resources.Resources.Taustakuva1
        Me.TabPage4.Controls.Add(Me.btnLisaaUusiAuto)
        Me.TabPage4.Controls.Add(Me.UusiRekistPVM)
        Me.TabPage4.Controls.Add(Me.Label7)
        Me.TabPage4.Controls.Add(Me.UusiRekNro)
        Me.TabPage4.Controls.Add(Me.UuusiAutoNro)
        Me.TabPage4.Controls.Add(Me.Label6)
        Me.TabPage4.Controls.Add(Me.Label5)
        Me.TabPage4.Location = New System.Drawing.Point(4, 27)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(1002, 269)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "LISÄÄ UUSI AUTO"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'btnLisaaUusiAuto
        '
        Me.btnLisaaUusiAuto.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnLisaaUusiAuto.BackColor = System.Drawing.Color.Lime
        Me.btnLisaaUusiAuto.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnLisaaUusiAuto.Font = New System.Drawing.Font("Courier New", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLisaaUusiAuto.Location = New System.Drawing.Point(215, 117)
        Me.btnLisaaUusiAuto.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLisaaUusiAuto.Name = "btnLisaaUusiAuto"
        Me.btnLisaaUusiAuto.Size = New System.Drawing.Size(134, 51)
        Me.btnLisaaUusiAuto.TabIndex = 22
        Me.btnLisaaUusiAuto.Text = "LISÄÄ"
        Me.btnLisaaUusiAuto.UseVisualStyleBackColor = False
        Me.btnLisaaUusiAuto.Visible = False
        '
        'UusiRekistPVM
        '
        Me.UusiRekistPVM.Location = New System.Drawing.Point(215, 84)
        Me.UusiRekistPVM.Name = "UusiRekistPVM"
        Me.UusiRekistPVM.Size = New System.Drawing.Size(200, 26)
        Me.UusiRekistPVM.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(47, 90)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(163, 18)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "REKISTERÖINTI PVM"
        '
        'UusiRekNro
        '
        Me.UusiRekNro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.UusiRekNro.Location = New System.Drawing.Point(215, 50)
        Me.UusiRekNro.MaxLength = 7
        Me.UusiRekNro.Name = "UusiRekNro"
        Me.UusiRekNro.Size = New System.Drawing.Size(100, 26)
        Me.UusiRekNro.TabIndex = 3
        '
        'UuusiAutoNro
        '
        Me.UuusiAutoNro.Location = New System.Drawing.Point(215, 18)
        Me.UuusiAutoNro.MaxLength = 5
        Me.UuusiAutoNro.Name = "UuusiAutoNro"
        Me.UuusiAutoNro.Size = New System.Drawing.Size(100, 26)
        Me.UuusiAutoNro.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(47, 57)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(158, 18)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "REKISTERINUMERO"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 21)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(159, 18)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "UUSI AUTONUMERO"
        '
        'TabPage2
        '
        Me.TabPage2.BackgroundImage = Global.JS_Control_2015.My.Resources.Resources.Taustakuva1
        Me.TabPage2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabPage2.Location = New System.Drawing.Point(4, 27)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(1002, 269)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "TabPage2"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.raporteissaEI)
        Me.GroupBox3.Controls.Add(Me.raporteissakylla)
        Me.GroupBox3.Location = New System.Drawing.Point(452, 172)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox3.Size = New System.Drawing.Size(141, 62)
        Me.GroupBox3.TabIndex = 19
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Raporteissa"
        '
        'DGW
        '
        Me.DGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGW.Location = New System.Drawing.Point(13, 39)
        Me.DGW.Margin = New System.Windows.Forms.Padding(4)
        Me.DGW.Name = "DGW"
        Me.DGW.Size = New System.Drawing.Size(1010, 306)
        Me.DGW.TabIndex = 19
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial Black", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(20, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 33)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "KALUSTO"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.vKatsVahtiEI)
        Me.GroupBox2.Controls.Add(Me.vKatsVahtiKYLLA)
        Me.GroupBox2.Location = New System.Drawing.Point(452, 88)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(173, 62)
        Me.GroupBox2.TabIndex = 18
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Katsastusvahti"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.vNastat)
        Me.GroupBox1.Controls.Add(Me.vTalvi)
        Me.GroupBox1.Controls.Add(Me.vKesa)
        Me.GroupBox1.Location = New System.Drawing.Point(452, 7)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(257, 62)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Renkaat"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 16)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 18)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Rekisterinumero"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'vRekkari
        '
        Me.vRekkari.AutoSize = True
        Me.vRekkari.BackColor = System.Drawing.Color.Transparent
        Me.vRekkari.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vRekkari.Location = New System.Drawing.Point(176, 16)
        Me.vRekkari.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.vRekkari.Name = "vRekkari"
        Me.vRekkari.Size = New System.Drawing.Size(74, 18)
        Me.vRekkari.TabIndex = 10
        Me.vRekkari.Text = "ABC-123"
        Me.vRekkari.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'vVastuuKuli
        '
        Me.vVastuuKuli.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.vVastuuKuli.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.vVastuuKuli.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vVastuuKuli.FormattingEnabled = True
        Me.vVastuuKuli.Location = New System.Drawing.Point(179, 80)
        Me.vVastuuKuli.Margin = New System.Windows.Forms.Padding(4)
        Me.vVastuuKuli.Name = "vVastuuKuli"
        Me.vVastuuKuli.Size = New System.Drawing.Size(265, 26)
        Me.vVastuuKuli.TabIndex = 14
        '
        'lanlalas
        '
        Me.lanlalas.AutoSize = True
        Me.lanlalas.BackColor = System.Drawing.Color.Transparent
        Me.lanlalas.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lanlalas.Location = New System.Drawing.Point(11, 83)
        Me.lanlalas.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lanlalas.Name = "lanlalas"
        Me.lanlalas.Size = New System.Drawing.Size(115, 18)
        Me.lanlalas.TabIndex = 13
        Me.lanlalas.Text = "Vastuukuljettaja"
        Me.lanlalas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabPage1
        '
        Me.TabPage1.BackgroundImage = Global.JS_Control_2015.My.Resources.Resources.Taustakuva1
        Me.TabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.TabPage1.Controls.Add(Me.piilorekistPVM)
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.vVastuuKuli)
        Me.TabPage1.Controls.Add(Me.vRekkari)
        Me.TabPage1.Controls.Add(Me.lanlalas)
        Me.TabPage1.Controls.Add(Me.vRekPVM)
        Me.TabPage1.Controls.Add(Me.vAutoNro)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 27)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1002, 269)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "PERUSTIEDOT"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'vRekPVM
        '
        Me.vRekPVM.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.vRekPVM.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vRekPVM.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.vRekPVM.Location = New System.Drawing.Point(179, 124)
        Me.vRekPVM.Margin = New System.Windows.Forms.Padding(4)
        Me.vRekPVM.Name = "vRekPVM"
        Me.vRekPVM.Size = New System.Drawing.Size(156, 26)
        Me.vRekPVM.TabIndex = 16
        '
        'vAutoNro
        '
        Me.vAutoNro.AutoSize = True
        Me.vAutoNro.BackColor = System.Drawing.Color.Transparent
        Me.vAutoNro.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.vAutoNro.Location = New System.Drawing.Point(176, 49)
        Me.vAutoNro.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.vAutoNro.Name = "vAutoNro"
        Me.vAutoNro.Size = New System.Drawing.Size(35, 18)
        Me.vAutoNro.TabIndex = 12
        Me.vAutoNro.Text = "122"
        Me.vAutoNro.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 49)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 18)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "Autonumero"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 130)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(134, 18)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Rekisteröinti PVM"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(13, 352)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1010, 300)
        Me.TabControl1.TabIndex = 20
        '
        'Kalusto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackgroundImage = Global.JS_Control_2015.My.Resources.Resources.Taustakuva1
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1058, 964)
        Me.ControlBox = False
        Me.Controls.Add(Me.DGW)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TabControl1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "Kalusto"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Kalusto"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.DGW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents raporteissaEI As System.Windows.Forms.RadioButton
    Friend WithEvents vKatsVahtiEI As System.Windows.Forms.RadioButton
    Friend WithEvents vKatsVahtiKYLLA As System.Windows.Forms.RadioButton
    Friend WithEvents piilorekistPVM As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TSEIKYLLÄ As System.Windows.Forms.RadioButton
    Friend WithEvents TSkaikki As System.Windows.Forms.RadioButton
    Friend WithEvents TSvoimassa As System.Windows.Forms.RadioButton
    Friend WithEvents vNastat As System.Windows.Forms.RadioButton
    Friend WithEvents vTalvi As System.Windows.Forms.RadioButton
    Friend WithEvents raporteissakylla As System.Windows.Forms.RadioButton
    Friend WithEvents vKesa As System.Windows.Forms.RadioButton
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents btnLisaaUusiAuto As System.Windows.Forms.Button
    Friend WithEvents UusiRekistPVM As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents UusiRekNro As System.Windows.Forms.TextBox
    Friend WithEvents UuusiAutoNro As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents DGW As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents vRekkari As System.Windows.Forms.Label
    Friend WithEvents vVastuuKuli As System.Windows.Forms.ComboBox
    Friend WithEvents lanlalas As System.Windows.Forms.Label
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents vRekPVM As System.Windows.Forms.DateTimePicker
    Friend WithEvents vAutoNro As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
End Class
