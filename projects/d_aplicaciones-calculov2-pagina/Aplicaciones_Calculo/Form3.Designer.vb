<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmDer_Cadena
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDer_Cadena))
        Me.a = New System.Windows.Forms.Label()
        Me.txtResultados = New System.Windows.Forms.TextBox()
        Me.btnLimpiar = New System.Windows.Forms.Button()
        Me.btnCalcular = New System.Windows.Forms.Button()
        Me.txtFuncion_1 = New System.Windows.Forms.TextBox()
        Me.lblFuncion = New System.Windows.Forms.Label()
        Me.txtFuncion_2 = New System.Windows.Forms.TextBox()
        Me.rd_Multiply = New System.Windows.Forms.RadioButton()
        Me.rd_Divide = New System.Windows.Forms.RadioButton()
        Me.lblOperador = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'a
        '
        Me.a.AutoSize = True
        Me.a.BackColor = System.Drawing.SystemColors.Window
        Me.a.Font = New System.Drawing.Font("Neon 80s", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.a.ForeColor = System.Drawing.SystemColors.ControlText
        Me.a.Location = New System.Drawing.Point(41, 278)
        Me.a.Name = "a"
        Me.a.Size = New System.Drawing.Size(158, 31)
        Me.a.TabIndex = 70
        Me.a.Text = "Resultado:"
        '
        'txtResultados
        '
        Me.txtResultados.BackColor = System.Drawing.SystemColors.Window
        Me.txtResultados.Font = New System.Drawing.Font("Neon 80s", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtResultados.Location = New System.Drawing.Point(31, 353)
        Me.txtResultados.Multiline = True
        Me.txtResultados.Name = "txtResultados"
        Me.txtResultados.ReadOnly = True
        Me.txtResultados.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtResultados.Size = New System.Drawing.Size(863, 483)
        Me.txtResultados.TabIndex = 69
        '
        'btnLimpiar
        '
        Me.btnLimpiar.Font = New System.Drawing.Font("Neon 80s", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLimpiar.ForeColor = System.Drawing.Color.Black
        Me.btnLimpiar.Location = New System.Drawing.Point(721, 269)
        Me.btnLimpiar.Name = "btnLimpiar"
        Me.btnLimpiar.Size = New System.Drawing.Size(173, 69)
        Me.btnLimpiar.TabIndex = 68
        Me.btnLimpiar.Text = "Limpiar"
        Me.btnLimpiar.UseVisualStyleBackColor = True
        '
        'btnCalcular
        '
        Me.btnCalcular.BackColor = System.Drawing.Color.White
        Me.btnCalcular.Font = New System.Drawing.Font("Neon 80s", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCalcular.ForeColor = System.Drawing.Color.Black
        Me.btnCalcular.Location = New System.Drawing.Point(492, 269)
        Me.btnCalcular.Name = "btnCalcular"
        Me.btnCalcular.Size = New System.Drawing.Size(173, 69)
        Me.btnCalcular.TabIndex = 67
        Me.btnCalcular.Text = "Calcular"
        Me.btnCalcular.UseVisualStyleBackColor = False
        '
        'txtFuncion_1
        '
        Me.txtFuncion_1.Font = New System.Drawing.Font("Neon 80s", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFuncion_1.Location = New System.Drawing.Point(43, 153)
        Me.txtFuncion_1.Name = "txtFuncion_1"
        Me.txtFuncion_1.Size = New System.Drawing.Size(374, 45)
        Me.txtFuncion_1.TabIndex = 61
        '
        'lblFuncion
        '
        Me.lblFuncion.AutoSize = True
        Me.lblFuncion.BackColor = System.Drawing.SystemColors.Window
        Me.lblFuncion.Font = New System.Drawing.Font("Neon 80s", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFuncion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFuncion.Location = New System.Drawing.Point(355, 33)
        Me.lblFuncion.Name = "lblFuncion"
        Me.lblFuncion.Size = New System.Drawing.Size(506, 87)
        Me.lblFuncion.TabIndex = 59
        Me.lblFuncion.Text = "Ingrese la ecuación en los recuadros" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "y elija el tipo de operacion a realizar en" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "la regla de la cadena"
        Me.lblFuncion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFuncion_2
        '
        Me.txtFuncion_2.Font = New System.Drawing.Font("Neon 80s", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFuncion_2.Location = New System.Drawing.Point(468, 153)
        Me.txtFuncion_2.Name = "txtFuncion_2"
        Me.txtFuncion_2.Size = New System.Drawing.Size(409, 45)
        Me.txtFuncion_2.TabIndex = 72
        '
        'rd_Multiply
        '
        Me.rd_Multiply.AutoSize = True
        Me.rd_Multiply.Font = New System.Drawing.Font("Neon 80s", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rd_Multiply.Location = New System.Drawing.Point(543, 218)
        Me.rd_Multiply.Name = "rd_Multiply"
        Me.rd_Multiply.Size = New System.Drawing.Size(42, 29)
        Me.rd_Multiply.TabIndex = 74
        Me.rd_Multiply.TabStop = True
        Me.rd_Multiply.Text = "*"
        Me.rd_Multiply.UseVisualStyleBackColor = True
        '
        'rd_Divide
        '
        Me.rd_Divide.AutoSize = True
        Me.rd_Divide.Font = New System.Drawing.Font("Neon 80s", 13.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rd_Divide.Location = New System.Drawing.Point(618, 220)
        Me.rd_Divide.Name = "rd_Divide"
        Me.rd_Divide.Size = New System.Drawing.Size(45, 29)
        Me.rd_Divide.TabIndex = 75
        Me.rd_Divide.TabStop = True
        Me.rd_Divide.Text = "/"
        Me.rd_Divide.UseVisualStyleBackColor = True
        '
        'lblOperador
        '
        Me.lblOperador.AutoSize = True
        Me.lblOperador.BackColor = System.Drawing.SystemColors.Window
        Me.lblOperador.Font = New System.Drawing.Font("Neon 80s", 16.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOperador.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOperador.Location = New System.Drawing.Point(26, 218)
        Me.lblOperador.Name = "lblOperador"
        Me.lblOperador.Size = New System.Drawing.Size(475, 29)
        Me.lblOperador.TabIndex = 76
        Me.lblOperador.Text = "Elija el operador: Multiplicar o Dividir :"
        Me.lblOperador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.Window
        Me.PictureBox1.Location = New System.Drawing.Point(456, 147)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(433, 61)
        Me.PictureBox1.TabIndex = 73
        Me.PictureBox1.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.BackColor = System.Drawing.SystemColors.Window
        Me.PictureBox5.Location = New System.Drawing.Point(31, 269)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(184, 54)
        Me.PictureBox5.TabIndex = 71
        Me.PictureBox5.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.SystemColors.Window
        Me.PictureBox2.Location = New System.Drawing.Point(31, 147)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(398, 61)
        Me.PictureBox2.TabIndex = 62
        Me.PictureBox2.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.BackColor = System.Drawing.SystemColors.Window
        Me.PictureBox4.Location = New System.Drawing.Point(329, 26)
        Me.PictureBox4.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(566, 104)
        Me.PictureBox4.TabIndex = 60
        Me.PictureBox4.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.Aplicaciones_Calculo.My.Resources.Resources.justBG
        Me.PictureBox3.Location = New System.Drawing.Point(-369, -125)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(1659, 1111)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox3.TabIndex = 77
        Me.PictureBox3.TabStop = False
        '
        'frmDer_Cadena
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(921, 861)
        Me.Controls.Add(Me.rd_Divide)
        Me.Controls.Add(Me.rd_Multiply)
        Me.Controls.Add(Me.lblOperador)
        Me.Controls.Add(Me.txtFuncion_2)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.a)
        Me.Controls.Add(Me.PictureBox5)
        Me.Controls.Add(Me.txtResultados)
        Me.Controls.Add(Me.btnLimpiar)
        Me.Controls.Add(Me.btnCalcular)
        Me.Controls.Add(Me.txtFuncion_1)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.lblFuncion)
        Me.Controls.Add(Me.PictureBox4)
        Me.Controls.Add(Me.PictureBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmDer_Cadena"
        Me.Text = "Derivadas por Metodo de la Cadena"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents a As Label
    Friend WithEvents PictureBox5 As PictureBox
    Friend WithEvents txtResultados As TextBox
    Friend WithEvents btnLimpiar As Button
    Friend WithEvents btnCalcular As Button
    Friend WithEvents txtFuncion_1 As TextBox
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents lblFuncion As Label
    Friend WithEvents PictureBox4 As PictureBox
    Friend WithEvents txtFuncion_2 As TextBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents rd_Multiply As RadioButton
    Friend WithEvents rd_Divide As RadioButton
    Friend WithEvents lblOperador As Label
    Friend WithEvents PictureBox3 As PictureBox
End Class
