<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDer_Pasos
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDer_Pasos))
        Me.lblFuncion = New System.Windows.Forms.Label()
        Me.txtFuncion = New System.Windows.Forms.TextBox()
        Me.lblH = New System.Windows.Forms.Label()
        Me.txtH = New System.Windows.Forms.TextBox()
        Me.btnLimpiar = New System.Windows.Forms.Button()
        Me.btnCalcular = New System.Windows.Forms.Button()
        Me.txtResultados = New System.Windows.Forms.TextBox()
        Me.a = New System.Windows.Forms.Label()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblFuncion
        '
        Me.lblFuncion.AutoSize = True
        Me.lblFuncion.BackColor = System.Drawing.SystemColors.Window
        Me.lblFuncion.Font = New System.Drawing.Font("Neon 80s", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFuncion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFuncion.Location = New System.Drawing.Point(29, 24)
        Me.lblFuncion.Name = "lblFuncion"
        Me.lblFuncion.Size = New System.Drawing.Size(301, 31)
        Me.lblFuncion.TabIndex = 44
        Me.lblFuncion.Text = "Ingresar la ecuación:"
        '
        'txtFuncion
        '
        Me.txtFuncion.Font = New System.Drawing.Font("Neon 80s", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFuncion.Location = New System.Drawing.Point(508, 18)
        Me.txtFuncion.Name = "txtFuncion"
        Me.txtFuncion.Size = New System.Drawing.Size(378, 45)
        Me.txtFuncion.TabIndex = 46
        '
        'lblH
        '
        Me.lblH.AutoSize = True
        Me.lblH.BackColor = System.Drawing.SystemColors.Window
        Me.lblH.Font = New System.Drawing.Font("Neon 80s", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblH.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblH.Location = New System.Drawing.Point(29, 100)
        Me.lblH.Name = "lblH"
        Me.lblH.Size = New System.Drawing.Size(361, 31)
        Me.lblH.TabIndex = 48
        Me.lblH.Text = "Determinar el valor de h:"
        '
        'txtH
        '
        Me.txtH.Font = New System.Drawing.Font("Neon 80s", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtH.Location = New System.Drawing.Point(508, 94)
        Me.txtH.Name = "txtH"
        Me.txtH.Size = New System.Drawing.Size(378, 45)
        Me.txtH.TabIndex = 50
        '
        'btnLimpiar
        '
        Me.btnLimpiar.Font = New System.Drawing.Font("Neon 80s", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiar.ForeColor = System.Drawing.Color.Black
        Me.btnLimpiar.Location = New System.Drawing.Point(725, 167)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(173, 69)
        Me.btnLimpiar.TabIndex = 53
        Me.btnLimpiar.Text = "Limpiar"
        Me.btnLimpiar.UseVisualStyleBackColor = True
        '
        'btnCalcular
        '
        Me.btnCalcular.BackColor = System.Drawing.Color.White
        Me.btnCalcular.Font = New System.Drawing.Font("Neon 80s", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCalcular.ForeColor = System.Drawing.Color.Black
        Me.btnCalcular.Location = New System.Drawing.Point(496, 167)
        Me.btnCalcular.Name = "btnCalcular"
        Me.btnCalcular.Size = New System.Drawing.Size(173, 69)
        Me.btnCalcular.TabIndex = 52
        Me.btnCalcular.Text = "Calcular"
        Me.btnCalcular.UseVisualStyleBackColor = False
        '
        'txtResultados
        '
        Me.txtResultados.BackColor = System.Drawing.SystemColors.Window
        Me.txtResultados.Font = New System.Drawing.Font("Neon 80s", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResultados.Location = New System.Drawing.Point(25, 326)
        Me.txtResultados.Multiline = True
        Me.txtResultados.Name = "txtResultados"
        Me.txtResultados.ReadOnly = True
        Me.txtResultados.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtResultados.Size = New System.Drawing.Size(873, 493)
        Me.txtResultados.TabIndex = 56
        '
        'a
        '
        Me.a.AutoSize = True
        Me.a.BackColor = System.Drawing.SystemColors.Window
        Me.a.Font = New System.Drawing.Font("Neon 80s", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.a.ForeColor = System.Drawing.SystemColors.ControlText
        Me.a.Location = New System.Drawing.Point(35, 265)
        Me.a.Name = "a"
        Me.a.Size = New System.Drawing.Size(158, 31)
        Me.a.TabIndex = 57
        Me.a.Text = "Resultado:"
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.SystemColors.Window
        Me.PictureBox5.Location = New System.Drawing.Point(25, 256)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(184, 54)
        Me.PictureBox5.TabIndex = 58
        Me.PictureBox5.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.BackColor = System.Drawing.SystemColors.Window
        Me.PictureBox3.Location = New System.Drawing.Point(496, 85)
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(402, 63)
        Me.PictureBox3.TabIndex = 51
        Me.PictureBox3.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.Window
        Me.PictureBox1.Location = New System.Drawing.Point(11, 90)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(455, 62)
        Me.PictureBox1.TabIndex = 49
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.SystemColors.Window
        Me.PictureBox2.Location = New System.Drawing.Point(496, 12)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(402, 61)
        Me.PictureBox2.TabIndex = 47
        Me.PictureBox2.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.SystemColors.Window
        Me.PictureBox4.Location = New System.Drawing.Point(11, 11)
        Me.PictureBox4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(455, 62)
        Me.PictureBox4.TabIndex = 45
        Me.PictureBox4.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = Global.Aplicaciones_Calculo.My.Resources.Resources.justBG
        Me.PictureBox6.Location = New System.Drawing.Point(-117, -5)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(1142, 918)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox6.TabIndex = 59
        Me.PictureBox6.TabStop = False
        '
        'frmDer_Pasos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(921, 861)
        Me.Controls.Add(Me.a)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.txtResultados)
        Me.Controls.Add(Me.btnLimpiar)
        Me.Controls.Add(Me.btnCalcular)
        Me.Controls.Add(Me.txtH)
        Me.Controls.Add(Me.PictureBox3)
        Me.Controls.Add(Me.lblH)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtFuncion)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.lblFuncion)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmDer_Pasos"
        Me.Text = "Derivadas por Pasos"
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblFuncion As Label
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents txtFuncion As TextBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents lblH As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents txtH As TextBox
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents btnCalcular As Button
    Friend WithEvents txtResultados As TextBox
    Friend WithEvents a As Label
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents PictureBox6 As PictureBox
End Class
