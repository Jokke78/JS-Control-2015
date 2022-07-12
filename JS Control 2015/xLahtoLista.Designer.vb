<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class xLahtoLista
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(xLahtoLista))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel()
        Me.Viikonpaiva = New System.Windows.Forms.Label()
        Me.PVM = New System.Windows.Forms.DateTimePicker()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.DGW = New System.Windows.Forms.DataGridView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.esimiesToimet = New System.Windows.Forms.GroupBox()
        Me.DGW2 = New System.Windows.Forms.DataGridView()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.rbTalviloma = New System.Windows.Forms.RadioButton()
        Me.rbKesaLoma = New System.Windows.Forms.RadioButton()
        Me.rbMuuvapaa = New System.Windows.Forms.RadioButton()
        Me.rbSaikku = New System.Windows.Forms.RadioButton()
        Me.lomaLoppu = New System.Windows.Forms.DateTimePicker()
        Me.lomaAlku = New System.Windows.Forms.DateTimePicker()
        Me.lomaKuljettaja = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ValittuRivi = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.riviNumero = New System.Windows.Forms.Label()
        Me.indexKuljettaja = New System.Windows.Forms.Label()
        Me.indexAuto = New System.Windows.Forms.Label()
        Me.btTallenna = New System.Windows.Forms.Button()
        Me.AutonNumero = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPunNro = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tvLopetettu = New System.Windows.Forms.TextBox()
        Me.tvAloitettu = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cbAuto = New System.Windows.Forms.ComboBox()
        Me.cbKuljettaja = New System.Windows.Forms.ComboBox()
        Me.TVloppuaika = New System.Windows.Forms.TextBox()
        Me.TVAlkuaika = New System.Windows.Forms.TextBox()
        Me.TVnLyhenne = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.DGW, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.esimiesToimet.SuspendLayout()
        CType(Me.DGW2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1058, 964)
        Me.Panel1.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 494.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 964.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 964.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 964.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1058, 964)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.DGW, 0, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(490, 960)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 4
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 274.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.Viikonpaiva, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.PVM, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Button1, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Button2, 3, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(2, 2)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(486, 32)
        Me.TableLayoutPanel3.TabIndex = 0
        '
        'Viikonpaiva
        '
        Me.Viikonpaiva.AutoSize = True
        Me.Viikonpaiva.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Viikonpaiva.Font = New System.Drawing.Font("Arial Narrow", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Viikonpaiva.Location = New System.Drawing.Point(276, 0)
        Me.Viikonpaiva.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Viikonpaiva.Name = "Viikonpaiva"
        Me.Viikonpaiva.Size = New System.Drawing.Size(142, 32)
        Me.Viikonpaiva.TabIndex = 0
        Me.Viikonpaiva.Text = "Label1"
        Me.Viikonpaiva.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PVM
        '
        Me.PVM.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PVM.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PVM.Location = New System.Drawing.Point(2, 2)
        Me.PVM.Margin = New System.Windows.Forms.Padding(2)
        Me.PVM.Name = "PVM"
        Me.PVM.Size = New System.Drawing.Size(270, 29)
        Me.PVM.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(422, 2)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(29, 28)
        Me.Button1.TabIndex = 2
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(455, 2)
        Me.Button2.Margin = New System.Windows.Forms.Padding(2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(29, 28)
        Me.Button2.TabIndex = 3
        Me.Button2.UseVisualStyleBackColor = True
        '
        'DGW
        '
        Me.DGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGW.Location = New System.Drawing.Point(2, 38)
        Me.DGW.Margin = New System.Windows.Forms.Padding(2)
        Me.DGW.Name = "DGW"
        Me.DGW.Size = New System.Drawing.Size(484, 913)
        Me.DGW.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.esimiesToimet)
        Me.Panel2.Controls.Add(Me.ValittuRivi)
        Me.Panel2.Controls.Add(Me.Button3)
        Me.Panel2.Controls.Add(Me.riviNumero)
        Me.Panel2.Controls.Add(Me.indexKuljettaja)
        Me.Panel2.Controls.Add(Me.indexAuto)
        Me.Panel2.Controls.Add(Me.btTallenna)
        Me.Panel2.Controls.Add(Me.AutonNumero)
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Controls.Add(Me.txtPunNro)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.cbAuto)
        Me.Panel2.Controls.Add(Me.cbKuljettaja)
        Me.Panel2.Controls.Add(Me.TVloppuaika)
        Me.Panel2.Controls.Add(Me.TVAlkuaika)
        Me.Panel2.Controls.Add(Me.TVnLyhenne)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(496, 2)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(2)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(560, 960)
        Me.Panel2.TabIndex = 1
        '
        'esimiesToimet
        '
        Me.esimiesToimet.Controls.Add(Me.DGW2)
        Me.esimiesToimet.Controls.Add(Me.Button4)
        Me.esimiesToimet.Controls.Add(Me.rbTalviloma)
        Me.esimiesToimet.Controls.Add(Me.rbKesaLoma)
        Me.esimiesToimet.Controls.Add(Me.rbMuuvapaa)
        Me.esimiesToimet.Controls.Add(Me.rbSaikku)
        Me.esimiesToimet.Controls.Add(Me.lomaLoppu)
        Me.esimiesToimet.Controls.Add(Me.lomaAlku)
        Me.esimiesToimet.Controls.Add(Me.lomaKuljettaja)
        Me.esimiesToimet.Controls.Add(Me.Label10)
        Me.esimiesToimet.Location = New System.Drawing.Point(13, 481)
        Me.esimiesToimet.Name = "esimiesToimet"
        Me.esimiesToimet.Size = New System.Drawing.Size(485, 469)
        Me.esimiesToimet.TabIndex = 21
        Me.esimiesToimet.TabStop = False
        Me.esimiesToimet.Text = "Poissaolokirjaus"
        '
        'DGW2
        '
        Me.DGW2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGW2.Location = New System.Drawing.Point(14, 225)
        Me.DGW2.Name = "DGW2"
        Me.DGW2.Size = New System.Drawing.Size(451, 226)
        Me.DGW2.TabIndex = 23
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.Lime
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.Button4.Font = New System.Drawing.Font("Arial Black", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.Location = New System.Drawing.Point(221, 177)
        Me.Button4.Margin = New System.Windows.Forms.Padding(2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(205, 43)
        Me.Button4.TabIndex = 22
        Me.Button4.Text = "TALLENNA"
        Me.Button4.UseVisualStyleBackColor = False
        Me.Button4.Visible = False
        '
        'rbTalviloma
        '
        Me.rbTalviloma.AutoSize = True
        Me.rbTalviloma.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbTalviloma.Location = New System.Drawing.Point(222, 150)
        Me.rbTalviloma.Name = "rbTalviloma"
        Me.rbTalviloma.Size = New System.Drawing.Size(105, 22)
        Me.rbTalviloma.TabIndex = 15
        Me.rbTalviloma.TabStop = True
        Me.rbTalviloma.Text = "Talvilomalla"
        Me.rbTalviloma.UseVisualStyleBackColor = True
        '
        'rbKesaLoma
        '
        Me.rbKesaLoma.AutoSize = True
        Me.rbKesaLoma.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbKesaLoma.Location = New System.Drawing.Point(14, 150)
        Me.rbKesaLoma.Name = "rbKesaLoma"
        Me.rbKesaLoma.Size = New System.Drawing.Size(112, 22)
        Me.rbKesaLoma.TabIndex = 14
        Me.rbKesaLoma.TabStop = True
        Me.rbKesaLoma.Text = "Kesälomalla"
        Me.rbKesaLoma.UseVisualStyleBackColor = True
        '
        'rbMuuvapaa
        '
        Me.rbMuuvapaa.AutoSize = True
        Me.rbMuuvapaa.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbMuuvapaa.Location = New System.Drawing.Point(222, 122)
        Me.rbMuuvapaa.Name = "rbMuuvapaa"
        Me.rbMuuvapaa.Size = New System.Drawing.Size(102, 22)
        Me.rbMuuvapaa.TabIndex = 13
        Me.rbMuuvapaa.TabStop = True
        Me.rbMuuvapaa.Text = "Muu vapaa"
        Me.rbMuuvapaa.UseVisualStyleBackColor = True
        '
        'rbSaikku
        '
        Me.rbSaikku.AutoSize = True
        Me.rbSaikku.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbSaikku.ForeColor = System.Drawing.Color.Black
        Me.rbSaikku.Location = New System.Drawing.Point(14, 122)
        Me.rbSaikku.Name = "rbSaikku"
        Me.rbSaikku.Size = New System.Drawing.Size(148, 22)
        Me.rbSaikku.TabIndex = 12
        Me.rbSaikku.TabStop = True
        Me.rbSaikku.Text = "Sairauspoissaolo"
        Me.rbSaikku.UseVisualStyleBackColor = True
        '
        'lomaLoppu
        '
        Me.lomaLoppu.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lomaLoppu.Location = New System.Drawing.Point(222, 90)
        Me.lomaLoppu.Name = "lomaLoppu"
        Me.lomaLoppu.Size = New System.Drawing.Size(206, 26)
        Me.lomaLoppu.TabIndex = 11
        '
        'lomaAlku
        '
        Me.lomaAlku.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lomaAlku.Location = New System.Drawing.Point(10, 90)
        Me.lomaAlku.Name = "lomaAlku"
        Me.lomaAlku.Size = New System.Drawing.Size(206, 26)
        Me.lomaAlku.TabIndex = 10
        '
        'lomaKuljettaja
        '
        Me.lomaKuljettaja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.lomaKuljettaja.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lomaKuljettaja.FormattingEnabled = True
        Me.lomaKuljettaja.Location = New System.Drawing.Point(122, 26)
        Me.lomaKuljettaja.Margin = New System.Windows.Forms.Padding(2)
        Me.lomaKuljettaja.Name = "lomaKuljettaja"
        Me.lomaKuljettaja.Size = New System.Drawing.Size(305, 35)
        Me.lomaKuljettaja.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(5, 29)
        Me.Label10.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(113, 27)
        Me.Label10.TabIndex = 3
        Me.Label10.Text = "Kuljettaja"
        '
        'ValittuRivi
        '
        Me.ValittuRivi.AutoSize = True
        Me.ValittuRivi.Location = New System.Drawing.Point(384, 439)
        Me.ValittuRivi.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ValittuRivi.Name = "ValittuRivi"
        Me.ValittuRivi.Size = New System.Drawing.Size(53, 16)
        Me.ValittuRivi.TabIndex = 20
        Me.ValittuRivi.Text = "Label10"
        Me.ValittuRivi.Visible = False
        '
        'Button3
        '
        Me.Button3.Image = CType(resources.GetObject("Button3.Image"), System.Drawing.Image)
        Me.Button3.Location = New System.Drawing.Point(462, 145)
        Me.Button3.Margin = New System.Windows.Forms.Padding(2)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(27, 25)
        Me.Button3.TabIndex = 19
        Me.Button3.UseVisualStyleBackColor = True
        '
        'riviNumero
        '
        Me.riviNumero.AutoSize = True
        Me.riviNumero.Location = New System.Drawing.Point(386, 421)
        Me.riviNumero.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.riviNumero.Name = "riviNumero"
        Me.riviNumero.Size = New System.Drawing.Size(53, 16)
        Me.riviNumero.TabIndex = 18
        Me.riviNumero.Text = "Label10"
        Me.riviNumero.Visible = False
        '
        'indexKuljettaja
        '
        Me.indexKuljettaja.AutoSize = True
        Me.indexKuljettaja.Location = New System.Drawing.Point(281, 439)
        Me.indexKuljettaja.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.indexKuljettaja.Name = "indexKuljettaja"
        Me.indexKuljettaja.Size = New System.Drawing.Size(52, 16)
        Me.indexKuljettaja.TabIndex = 17
        Me.indexKuljettaja.Text = "Label11"
        Me.indexKuljettaja.Visible = False
        '
        'indexAuto
        '
        Me.indexAuto.AutoSize = True
        Me.indexAuto.Location = New System.Drawing.Point(281, 421)
        Me.indexAuto.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.indexAuto.Name = "indexAuto"
        Me.indexAuto.Size = New System.Drawing.Size(53, 16)
        Me.indexAuto.TabIndex = 16
        Me.indexAuto.Text = "Label10"
        Me.indexAuto.Visible = False
        '
        'btTallenna
        '
        Me.btTallenna.BackColor = System.Drawing.Color.Lime
        Me.btTallenna.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btTallenna.Font = New System.Drawing.Font("Arial Black", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btTallenna.Location = New System.Drawing.Point(13, 421)
        Me.btTallenna.Margin = New System.Windows.Forms.Padding(2)
        Me.btTallenna.Name = "btTallenna"
        Me.btTallenna.Size = New System.Drawing.Size(205, 43)
        Me.btTallenna.TabIndex = 15
        Me.btTallenna.Text = "TALLENNA"
        Me.btTallenna.UseVisualStyleBackColor = False
        Me.btTallenna.Visible = False
        '
        'AutonNumero
        '
        Me.AutonNumero.AutoSize = True
        Me.AutonNumero.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AutonNumero.Location = New System.Drawing.Point(150, 261)
        Me.AutonNumero.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.AutonNumero.Name = "AutonNumero"
        Me.AutonNumero.Size = New System.Drawing.Size(0, 18)
        Me.AutonNumero.TabIndex = 14
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 261)
        Me.Label9.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(78, 16)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Autonumero"
        '
        'txtPunNro
        '
        Me.txtPunNro.AutoSize = True
        Me.txtPunNro.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPunNro.Location = New System.Drawing.Point(150, 184)
        Me.txtPunNro.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.txtPunNro.Name = "txtPunNro"
        Me.txtPunNro.Size = New System.Drawing.Size(0, 18)
        Me.txtPunNro.TabIndex = 12
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 184)
        Me.Label8.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(94, 16)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Puhelinnumero"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.tvLopetettu)
        Me.GroupBox1.Controls.Add(Me.tvAloitettu)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 299)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(2)
        Me.GroupBox1.Size = New System.Drawing.Size(318, 118)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Kirjautuminen"
        '
        'tvLopetettu
        '
        Me.tvLopetettu.Enabled = False
        Me.tvLopetettu.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvLopetettu.Location = New System.Drawing.Point(154, 64)
        Me.tvLopetettu.Margin = New System.Windows.Forms.Padding(2)
        Me.tvLopetettu.Name = "tvLopetettu"
        Me.tvLopetettu.Size = New System.Drawing.Size(93, 35)
        Me.tvLopetettu.TabIndex = 7
        Me.tvLopetettu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'tvAloitettu
        '
        Me.tvAloitettu.Enabled = False
        Me.tvAloitettu.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tvAloitettu.Location = New System.Drawing.Point(153, 25)
        Me.tvAloitettu.Margin = New System.Windows.Forms.Padding(2)
        Me.tvAloitettu.Name = "tvAloitettu"
        Me.tvAloitettu.Size = New System.Drawing.Size(93, 35)
        Me.tvAloitettu.TabIndex = 6
        Me.tvAloitettu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 75)
        Me.Label7.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(133, 16)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Työvuoro lopetettu klo"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(17, 30)
        Me.Label6.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(129, 16)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Työvuoro aloitettu klo"
        '
        'cbAuto
        '
        Me.cbAuto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbAuto.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbAuto.FormattingEnabled = True
        Me.cbAuto.Location = New System.Drawing.Point(153, 215)
        Me.cbAuto.Margin = New System.Windows.Forms.Padding(2)
        Me.cbAuto.Name = "cbAuto"
        Me.cbAuto.Size = New System.Drawing.Size(154, 35)
        Me.cbAuto.TabIndex = 9
        '
        'cbKuljettaja
        '
        Me.cbKuljettaja.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbKuljettaja.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbKuljettaja.FormattingEnabled = True
        Me.cbKuljettaja.Location = New System.Drawing.Point(153, 140)
        Me.cbKuljettaja.Margin = New System.Windows.Forms.Padding(2)
        Me.cbKuljettaja.Name = "cbKuljettaja"
        Me.cbKuljettaja.Size = New System.Drawing.Size(305, 35)
        Me.cbKuljettaja.TabIndex = 8
        '
        'TVloppuaika
        '
        Me.TVloppuaika.Enabled = False
        Me.TVloppuaika.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TVloppuaika.Location = New System.Drawing.Point(153, 101)
        Me.TVloppuaika.Margin = New System.Windows.Forms.Padding(2)
        Me.TVloppuaika.Name = "TVloppuaika"
        Me.TVloppuaika.Size = New System.Drawing.Size(117, 35)
        Me.TVloppuaika.TabIndex = 7
        Me.TVloppuaika.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TVAlkuaika
        '
        Me.TVAlkuaika.Enabled = False
        Me.TVAlkuaika.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TVAlkuaika.Location = New System.Drawing.Point(153, 65)
        Me.TVAlkuaika.Margin = New System.Windows.Forms.Padding(2)
        Me.TVAlkuaika.Name = "TVAlkuaika"
        Me.TVAlkuaika.Size = New System.Drawing.Size(117, 35)
        Me.TVAlkuaika.TabIndex = 6
        Me.TVAlkuaika.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TVnLyhenne
        '
        Me.TVnLyhenne.Enabled = False
        Me.TVnLyhenne.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TVnLyhenne.Location = New System.Drawing.Point(153, 27)
        Me.TVnLyhenne.Margin = New System.Windows.Forms.Padding(2)
        Me.TVnLyhenne.Name = "TVnLyhenne"
        Me.TVnLyhenne.Size = New System.Drawing.Size(117, 35)
        Me.TVnLyhenne.TabIndex = 5
        Me.TVnLyhenne.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(48, 95)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 27)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Loppuu"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(48, 65)
        Me.Label4.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 27)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Alkaa"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(10, 143)
        Me.Label3.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(113, 27)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Kuljettaja"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(10, 223)
        Me.Label2.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 27)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Auto"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 35)
        Me.Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 27)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Työvuoro"
        '
        'xLahtoLista
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1058, 964)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "xLahtoLista"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "VanhaLahtoLista"
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.DGW, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.esimiesToimet.ResumeLayout(False)
        Me.esimiesToimet.PerformLayout()
        CType(Me.DGW2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Viikonpaiva As System.Windows.Forms.Label
    Friend WithEvents PVM As System.Windows.Forms.DateTimePicker
    Friend WithEvents DGW As System.Windows.Forms.DataGridView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents AutonNumero As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtPunNro As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tvLopetettu As System.Windows.Forms.TextBox
    Friend WithEvents tvAloitettu As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cbAuto As System.Windows.Forms.ComboBox
    Friend WithEvents cbKuljettaja As System.Windows.Forms.ComboBox
    Friend WithEvents TVloppuaika As System.Windows.Forms.TextBox
    Friend WithEvents TVAlkuaika As System.Windows.Forms.TextBox
    Friend WithEvents TVnLyhenne As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents indexKuljettaja As System.Windows.Forms.Label
    Friend WithEvents indexAuto As System.Windows.Forms.Label
    Friend WithEvents btTallenna As System.Windows.Forms.Button
    Friend WithEvents riviNumero As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents ValittuRivi As System.Windows.Forms.Label
    Friend WithEvents esimiesToimet As System.Windows.Forms.GroupBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents rbTalviloma As System.Windows.Forms.RadioButton
    Friend WithEvents rbKesaLoma As System.Windows.Forms.RadioButton
    Friend WithEvents rbMuuvapaa As System.Windows.Forms.RadioButton
    Friend WithEvents rbSaikku As System.Windows.Forms.RadioButton
    Friend WithEvents lomaLoppu As System.Windows.Forms.DateTimePicker
    Friend WithEvents lomaAlku As System.Windows.Forms.DateTimePicker
    Friend WithEvents lomaKuljettaja As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents DGW2 As System.Windows.Forms.DataGridView
End Class
