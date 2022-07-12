<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class KayttoestettyLOMAKE
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(61, 129)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(934, 52)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "TOIMINTO POIS KÄYTÖSTÄ VÄLIAIKAISESTI"
        '
        'KayttoestettyLOMAKE
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(26.0!, 52.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1240, 768)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Arial Black", 27.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(13, 12, 13, 12)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "KayttoestettyLOMAKE"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "KayttoestettyLOMAKE"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
