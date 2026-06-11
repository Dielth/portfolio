'Aqui empieza el codigo que jala chido

Namespace DerivadaProducto
    Public Class frmDerPr3
        Inherits Form ' Heredar de Form

        Public Sub New()
            ' Inicializar los componentes del formulario
            InitializeComponent()
        End Sub

        Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click
            ' Obtener los polinomios de los TextBox
            Dim polinomioAB As String = txtFuncionAB.Text.Trim()
            Dim polinomioCD As String = txtFuncionCD.Text.Trim()

            If String.IsNullOrEmpty(polinomioAB) Or String.IsNullOrEmpty(polinomioCD) Then
                MessageBox.Show("Por favor ingrese ambos polinomios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Mostrar la función original
            txtResultados.Clear()
            txtResultados.AppendText("y = (" & FormatearPolinomioConEspacios(polinomioAB) & ")(" & FormatearPolinomioConEspacios(polinomioCD) & ")" & vbCrLf & vbCrLf)

            ' Calcular y mostrar las derivadas
            Dim derivadaAB As String = CalcularDerivadaPolinomio(polinomioAB)
            Dim derivadaCD As String = CalcularDerivadaPolinomio(polinomioCD)

            txtResultados.AppendText("y' = (" & FormatearPolinomioConEspacios(polinomioAB) & ")(" & FormatearPolinomioConEspacios(derivadaCD) & ") + (" & FormatearPolinomioConEspacios(polinomioCD) & ")(" & FormatearPolinomioConEspacios(derivadaAB) & ")" & vbCrLf & vbCrLf)

            ' Mostrar los pasos detallados para u(x) y v(x)
            txtResultados.AppendText("Las derivadas para:" & vbCrLf & vbCrLf)

            txtResultados.AppendText("u(x):" & vbCrLf)
            MostrarPasosDerivada(polinomioAB, txtResultados)
            txtResultados.AppendText("u'(x) = " & FormatearPolinomioConEspacios(derivadaAB) & vbCrLf & vbCrLf)

            txtResultados.AppendText("v(x):" & vbCrLf)
            MostrarPasosDerivada(polinomioCD, txtResultados)
            txtResultados.AppendText("v'(x) = " & FormatearPolinomioConEspacios(derivadaCD) & vbCrLf & vbCrLf)

            ' Aplicar regla del producto
            txtResultados.AppendText("Se aplica la regla del producto:" & vbCrLf)
            txtResultados.AppendText("y' = u·v' + u'·v" & vbCrLf)
            txtResultados.AppendText("y' = (" & FormatearPolinomioConEspacios(derivadaAB) & ")(" & FormatearPolinomioConEspacios(polinomioCD) & ") + (" & FormatearPolinomioConEspacios(polinomioAB) & ")(" & FormatearPolinomioConEspacios(derivadaCD) & ")" & vbCrLf & vbCrLf)

            ' Mostrar los pasos de multiplicación polinomios
            txtResultados.AppendText("Para (" & FormatearPolinomioConEspacios(derivadaAB) & ")(" & FormatearPolinomioConEspacios(polinomioCD) & "):" & vbCrLf)
            Dim resultado1 As List(Of String) = MultiplicarPolinomios(derivadaAB, polinomioCD)
            For Each paso In resultado1
                txtResultados.AppendText(paso & vbCrLf)
            Next
            txtResultados.AppendText(vbCrLf)

            txtResultados.AppendText("Para (" & FormatearPolinomioConEspacios(polinomioAB) & ")(" & FormatearPolinomioConEspacios(derivadaCD) & "):" & vbCrLf)
            Dim resultado2 As List(Of String) = MultiplicarPolinomios(polinomioAB, derivadaCD)
            For Each paso In resultado2
                txtResultados.AppendText(paso & vbCrLf)
            Next
            txtResultados.AppendText(vbCrLf)

            ' Sumar los términos semejantes
            Dim terminosFinal As Dictionary(Of Integer, Double) = New Dictionary(Of Integer, Double)()
            AgregarTerminos(terminosFinal, derivadaAB, polinomioCD)
            AgregarTerminos(terminosFinal, polinomioAB, derivadaCD)

            txtResultados.AppendText("Se agrupan y juntan términos semejantes:" & vbCrLf)
            txtResultados.AppendText("y' = " & FormatearPolinomioConEspacios(FormatearPolinomio(terminosFinal)) & vbCrLf & vbCrLf)

            txtResultados.AppendText("La derivada del producto es:" & vbCrLf)
            txtResultados.AppendText("y' = " & FormatearPolinomioConEspacios(SimplificarPolinomio(terminosFinal)) & vbCrLf)
        End Sub

        Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
            txtFuncionAB.Clear()
            txtFuncionCD.Clear()
            txtResultados.Clear()
        End Sub

        ' Función para calcular la derivada de un polinomio
        Private Function CalcularDerivadaPolinomio(polinomio As String) As String
            Dim terminos As List(Of String) = ObtenerTerminos(polinomio)
            Dim derivada As New List(Of String)

            For Each termino In terminos
                Dim derivadaTermino As String = DerivarTermino(termino)
                If derivadaTermino <> "0" Then
                    derivada.Add(derivadaTermino)
                End If
            Next

            If derivada.Count = 0 Then
                Return "0"
            End If

            Return String.Join("+", derivada)
        End Function

        ' Función para mostrar los pasos de la derivada
        Private Sub MostrarPasosDerivada(polinomio As String, txtBox As TextBox)
            Dim terminos As List(Of String) = ObtenerTerminos(polinomio)

            For Each termino In terminos
                Dim paso As String = DerivarTerminoConPasos(termino)
                txtBox.AppendText(paso & vbCrLf)
            Next
        End Sub

        ' Función para obtener los términos de un polinomio
        Private Function ObtenerTerminos(polinomio As String) As List(Of String)
            Dim terminos As New List(Of String)

            ' Reemplazar - por +-
            polinomio = polinomio.Replace("-", "+-")
            If polinomio.StartsWith("+-") Then
                polinomio = polinomio.Substring(1)
            End If

            ' Dividir por +
            Dim terminosArray As String() = polinomio.Split("+"c)

            For Each termino In terminosArray
                If Not String.IsNullOrEmpty(termino) Then
                    terminos.Add(termino.Trim())
                End If
            Next

            Return terminos
        End Function

        ' Función para derivar un término
        Private Function DerivarTermino(termino As String) As String
            If Not termino.Contains("x") Then
                ' Es una constante, su derivada es 0
                Return "0"
            End If

            Dim signo As Double = 1
            If termino.StartsWith("-") Then
                signo = -1
                termino = termino.Substring(1)
            End If

            Dim coeficiente As Double = 1
            Dim exponente As Integer = 1

            ' Extraer coeficiente
            Dim partesCoef As String() = termino.Split("x"c)
            If partesCoef.Length > 0 AndAlso Not String.IsNullOrEmpty(partesCoef(0)) Then
                Double.TryParse(partesCoef(0), coeficiente)
            End If
            coeficiente *= signo ' Aplicar el signo

            ' Extraer exponente
            If termino.Contains("^") Then
                Dim partesExp As String() = termino.Split("^"c)
                If partesExp.Length > 1 Then
                    Integer.TryParse(partesExp(1), exponente)
                End If
            End If

            ' Calcular derivada
            Dim nuevoCoef As Double = coeficiente * exponente
            Dim nuevoExp As Integer = exponente - 1

            If nuevoCoef = 0 Then
                Return "0"
            End If

            If nuevoExp = 0 Then
                Return nuevoCoef.ToString()
            ElseIf nuevoExp = 1 Then
                Return nuevoCoef.ToString() & "x"
            Else
                Return nuevoCoef.ToString() & "x^" & nuevoExp.ToString()
            End If
        End Function

        ' Función para derivar un término y mostrar pasos
        Private Function DerivarTerminoConPasos(terminoOriginal As String) As String
            If Not terminoOriginal.Contains("x") Then
                Return $"{terminoOriginal} = 0"
            End If

            Dim termino As String = terminoOriginal
            Dim signo As Double = 1
            If termino.StartsWith("-") Then
                signo = -1
                termino = termino.Substring(1)
            End If

            Dim coeficiente As Double = 1
            Dim exponente As Integer = 1

            ' Extraer coeficiente
            Dim partesCoef As String() = termino.Split("x"c)
            If partesCoef.Length > 0 AndAlso Not String.IsNullOrEmpty(partesCoef(0)) Then
                Double.TryParse(partesCoef(0), coeficiente)
            End If
            coeficiente *= signo ' Aplicar el signo

            ' Extraer exponente
            If termino.Contains("^") Then
                Dim partesExp As String() = termino.Split("^"c)
                If partesExp.Length > 1 Then
                    Integer.TryParse(partesExp(1), exponente)
                End If
            End If

            ' Calcular derivada
            Dim nuevoCoef As Double = coeficiente * exponente
            Dim nuevoExp As Integer = exponente - 1

            Dim resultadoPaso As String = If(nuevoCoef = 0, "0", FormatearTerminoDerivada(nuevoCoef, nuevoExp))
            Return $"{terminoOriginal} = {resultadoPaso}"
        End Function

        ' Función auxiliar para formatear términos derivados
        Private Function FormatearTerminoDerivada(coeficiente As Double, exponente As Integer) As String
            If exponente = 0 Then
                Return coeficiente.ToString()
            ElseIf exponente = 1 Then
                Return $"{coeficiente}x"
            Else
                Return $"{coeficiente}x^{exponente}"
            End If
        End Function

        ' Función para multiplicar dos polinomios
        Private Function MultiplicarPolinomios(poly1 As String, poly2 As String) As List(Of String)
            Dim terminos1 As List(Of String) = ObtenerTerminos(poly1)
            Dim terminos2 As List(Of String) = ObtenerTerminos(poly2)
            Dim resultados As New List(Of String)

            For Each t1 In terminos1
                For Each t2 In terminos2
                    Dim resultado As String = MultiplicarTerminos(t1, t2)
                    resultados.Add("(" & t1 & ") * (" & t2 & ") = " & resultado)
                Next
            Next

            Return resultados
        End Function

        ' Función para multiplicar dos términos
        Private Function MultiplicarTerminos(termino1 As String, termino2 As String) As String
            ' Extraer coeficientes y exponentes
            Dim coef1, coef2 As Double
            Dim exp1, exp2 As Integer

            ExtraerCoeficienteYExponente(termino1, coef1, exp1)
            ExtraerCoeficienteYExponente(termino2, coef2, exp2)

            ' Multiplicar coeficientes y sumar exponentes
            Dim nuevoCoef As Double = coef1 * coef2
            Dim nuevoExp As Integer = exp1 + exp2

            If nuevoCoef = 0 Then
                Return "0"
            End If

            If nuevoExp = 0 Then
                Return nuevoCoef.ToString()
            ElseIf nuevoExp = 1 Then
                Return nuevoCoef.ToString() & "x"
            Else
                Return nuevoCoef.ToString() & "x^" & nuevoExp.ToString()
            End If
        End Function

        ' Función para extraer coeficiente y exponente de un término
        Private Sub ExtraerCoeficienteYExponente(termino As String, ByRef coeficiente As Double, ByRef exponente As Integer)
            coeficiente = 1
            exponente = 0
            Dim signo As Double = 1

            If termino.StartsWith("-") Then
                signo = -1
                termino = termino.Substring(1)
            End If

            If Not termino.Contains("x") Then
                ' Es una constante
                Double.TryParse(termino, coeficiente)
                coeficiente *= signo
                Return
            End If

            ' Extraer coeficiente
            Dim partesCoef As String() = termino.Split("x"c)
            If partesCoef.Length > 0 AndAlso partesCoef(0).Trim() <> "" Then
                Double.TryParse(partesCoef(0), coeficiente)
            Else
                coeficiente = 1
            End If
            coeficiente *= signo

            ' Extraer exponente
            exponente = 1 ' Por defecto, si solo hay x
            If termino.Contains("^") Then
                Dim partesExp As String() = termino.Split("^"c)
                If partesExp.Length > 1 Then
                    Integer.TryParse(partesExp(1), exponente)
                End If
            End If
        End Sub

        ' Función para agregar términos a un diccionario
        Private Sub AgregarTerminos(dict As Dictionary(Of Integer, Double), poly1 As String, poly2 As String)
            Dim terminos1 As List(Of String) = ObtenerTerminos(poly1)
            Dim terminos2 As List(Of String) = ObtenerTerminos(poly2)

            For Each t1 In terminos1
                For Each t2 In terminos2
                    Dim coef1, coef2 As Double
                    Dim exp1, exp2 As Integer

                    ExtraerCoeficienteYExponente(t1, coef1, exp1)
                    ExtraerCoeficienteYExponente(t2, coef2, exp2)

                    Dim nuevoCoef As Double = coef1 * coef2
                    Dim nuevoExp As Integer = exp1 + exp2

                    If dict.ContainsKey(nuevoExp) Then
                        dict(nuevoExp) += nuevoCoef
                    Else
                        dict.Add(nuevoExp, nuevoCoef)
                    End If
                Next
            Next
        End Sub

        ' Función para formatear un polinomio desde un diccionario
        Private Function FormatearPolinomio(dict As Dictionary(Of Integer, Double)) As String
            If dict.Count = 0 Then
                Return "0"
            End If

            Dim terminos As New List(Of String)

            ' Ordenamos los exponentes de mayor a menor
            Dim exponentes As List(Of Integer) = dict.Keys.ToList()
            exponentes.Sort()
            exponentes.Reverse()

            For Each exp In exponentes
                Dim coef As Double = dict(exp)

                ' Si el coeficiente es 0, se ignora
                If Math.Abs(coef) < 0.0001 Then
                    Continue For
                End If

                Dim termino As String

                If exp = 0 Then
                    termino = coef.ToString()
                ElseIf exp = 1 Then
                    termino = coef.ToString() & "x"
                Else
                    termino = coef.ToString() & "x^" & exp.ToString()
                End If

                terminos.Add(termino)
            Next

            Return String.Join(" + ", terminos)
        End Function

        ' Función para simplificar un polinomio final
        Private Function SimplificarPolinomio(dict As Dictionary(Of Integer, Double)) As String
            If dict.Count = 0 Then
                Return "0"
            End If

            Dim terminos As New List(Of String)

            ' Ordenamos los exponentes de mayor a menor
            Dim exponentes As List(Of Integer) = dict.Keys.ToList()
            exponentes.Sort()
            exponentes.Reverse()

            For Each exp In exponentes
                Dim coef As Double = dict(exp)

                ' Si el coeficiente es 0, se ignora
                If Math.Abs(coef) < 0.0001 Then
                    Continue For
                End If

                Dim termino As String

                If exp = 0 Then
                    termino = coef.ToString()
                ElseIf exp = 1 Then
                    termino = coef.ToString() & "x"
                Else
                    termino = coef.ToString() & "x^" & exp.ToString()
                End If

                ' Si es el primer término o es negativo
                If terminos.Count = 0 Then
                    terminos.Add(termino)
                Else
                    If termino.StartsWith("-") Then
                        terminos.Add(termino)
                    Else
                        terminos.Add("+ " & termino)
                    End If
                End If
            Next

            Return String.Join(" ", terminos)
        End Function

        ' Función para formatear polinomios con espacios
        Private Function FormatearPolinomioConEspacios(polinomio As String) As String
            Dim terminos As List(Of String) = ObtenerTerminos(polinomio)
            Dim terminosFormateados As New List(Of String)

            For Each termino In terminos
                terminosFormateados.Add(termino)
            Next

            Return String.Join(" + ", terminosFormateados).Replace("+ -", "- ")
        End Function

        ' Designer code
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDerPr3))
            Me.lblFuncionAB = New System.Windows.Forms.Label()
            Me.txtFuncionAB = New System.Windows.Forms.TextBox()
            Me.txtFuncionCD = New System.Windows.Forms.TextBox()
            Me.btnCalcular = New System.Windows.Forms.Button()
            Me.btnLimpiar = New System.Windows.Forms.Button()
            Me.txtResultados = New System.Windows.Forms.TextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Label5 = New System.Windows.Forms.Label()
            Me.Label6 = New System.Windows.Forms.Label()
            Me.PictureBox5 = New System.Windows.Forms.PictureBox()
            Me.PictureBox1 = New System.Windows.Forms.PictureBox()
            Me.GroupBox1 = New System.Windows.Forms.GroupBox()
            CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox1.SuspendLayout()
            Me.SuspendLayout()
            '
            'lblFuncionAB
            '
            Me.lblFuncionAB.AutoSize = True
            Me.lblFuncionAB.Font = New System.Drawing.Font("Neon 80s", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.lblFuncionAB.Location = New System.Drawing.Point(14, 17)
            Me.lblFuncionAB.Name = "lblFuncionAB"
            Me.lblFuncionAB.Size = New System.Drawing.Size(807, 31)
            Me.lblFuncionAB.TabIndex = 0
            Me.lblFuncionAB.Text = "Ingresar la ecuación por partes:    ab(cd'+dc')+ca(ab'+ba')"
            '
            'txtFuncionAB
            '
            Me.txtFuncionAB.Font = New System.Drawing.Font("Neon 80s", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtFuncionAB.Location = New System.Drawing.Point(111, 60)
            Me.txtFuncionAB.Name = "txtFuncionAB"
            Me.txtFuncionAB.Size = New System.Drawing.Size(321, 42)
            Me.txtFuncionAB.TabIndex = 1
            '
            'txtFuncionCD
            '
            Me.txtFuncionCD.Font = New System.Drawing.Font("Neon 80s", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtFuncionCD.Location = New System.Drawing.Point(499, 60)
            Me.txtFuncionCD.Name = "txtFuncionCD"
            Me.txtFuncionCD.Size = New System.Drawing.Size(362, 42)
            Me.txtFuncionCD.TabIndex = 2
            '
            'btnCalcular
            '
            Me.btnCalcular.Font = New System.Drawing.Font("Neon 80s", 13.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnCalcular.Location = New System.Drawing.Point(523, 157)
            Me.btnCalcular.Name = "btnCalcular"
            Me.btnCalcular.Size = New System.Drawing.Size(161, 46)
            Me.btnCalcular.TabIndex = 3
            Me.btnCalcular.Text = "Calcular"
            Me.btnCalcular.UseVisualStyleBackColor = True
            '
            'btnLimpiar
            '
            Me.btnLimpiar.Font = New System.Drawing.Font("Neon 80s", 13.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.btnLimpiar.Location = New System.Drawing.Point(720, 157)
            Me.btnLimpiar.Name = "btnLimpiar"
            Me.btnLimpiar.Size = New System.Drawing.Size(161, 46)
            Me.btnLimpiar.TabIndex = 4
            Me.btnLimpiar.Text = "Limpiar"
            Me.btnLimpiar.UseVisualStyleBackColor = True
            '
            'txtResultados
            '
            Me.txtResultados.BackColor = System.Drawing.SystemColors.Window
            Me.txtResultados.Font = New System.Drawing.Font("Neon 80s", 19.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.txtResultados.Location = New System.Drawing.Point(12, 248)
            Me.txtResultados.Multiline = True
            Me.txtResultados.Name = "txtResultados"
            Me.txtResultados.ReadOnly = True
            Me.txtResultados.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
            Me.txtResultados.Size = New System.Drawing.Size(873, 493)
            Me.txtResultados.TabIndex = 5
            '
            'Label1
            '
            Me.Label1.AutoSize = True
            Me.Label1.BackColor = System.Drawing.SystemColors.Window
            Me.Label1.Font = New System.Drawing.Font("Neon 80s", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label1.Location = New System.Drawing.Point(25, 177)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(158, 31)
            Me.Label1.TabIndex = 6
            Me.Label1.Text = "Resultado:"
            '
            'Label2
            '
            Me.Label2.AutoSize = True
            Me.Label2.Font = New System.Drawing.Font("Neon 80s", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label2.Location = New System.Drawing.Point(25, 63)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(44, 31)
            Me.Label2.TabIndex = 7
            Me.Label2.Text = "y="
            '
            'Label3
            '
            Me.Label3.AutoSize = True
            Me.Label3.Font = New System.Drawing.Font("Neon 80s", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label3.Location = New System.Drawing.Point(172, 105)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(32, 31)
            Me.Label3.TabIndex = 8
            Me.Label3.Text = "a"
            '
            'Label4
            '
            Me.Label4.AutoSize = True
            Me.Label4.Font = New System.Drawing.Font("Neon 80s", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label4.Location = New System.Drawing.Point(342, 105)
            Me.Label4.Name = "Label4"
            Me.Label4.Size = New System.Drawing.Size(32, 31)
            Me.Label4.TabIndex = 9
            Me.Label4.Text = "b"
            '
            'Label5
            '
            Me.Label5.AutoSize = True
            Me.Label5.Font = New System.Drawing.Font("Neon 80s", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label5.Location = New System.Drawing.Point(610, 105)
            Me.Label5.Name = "Label5"
            Me.Label5.Size = New System.Drawing.Size(30, 31)
            Me.Label5.TabIndex = 10
            Me.Label5.Text = "c"
            '
            'Label6
            '
            Me.Label6.AutoSize = True
            Me.Label6.Font = New System.Drawing.Font("Neon 80s", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.Label6.Location = New System.Drawing.Point(789, 105)
            Me.Label6.Name = "Label6"
            Me.Label6.Size = New System.Drawing.Size(32, 31)
            Me.Label6.TabIndex = 11
            Me.Label6.Text = "d"
            '
            'PictureBox5
            '
            Me.PictureBox5.BackColor = System.Drawing.SystemColors.Window
            Me.PictureBox5.Location = New System.Drawing.Point(12, 166)
            Me.PictureBox5.Name = "PictureBox5"
            Me.PictureBox5.Size = New System.Drawing.Size(184, 54)
            Me.PictureBox5.TabIndex = 59
            Me.PictureBox5.TabStop = False
            '
            'PictureBox1
            '
            Me.PictureBox1.Image = Global.Aplicaciones_Calculo.My.Resources.Resources.justBG
            Me.PictureBox1.Location = New System.Drawing.Point(-107, -126)
            Me.PictureBox1.Name = "PictureBox1"
            Me.PictureBox1.Size = New System.Drawing.Size(1699, 1316)
            Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
            Me.PictureBox1.TabIndex = 60
            Me.PictureBox1.TabStop = False
            '
            'GroupBox1
            '
            Me.GroupBox1.BackColor = System.Drawing.Color.Linen
            Me.GroupBox1.Controls.Add(Me.Label6)
            Me.GroupBox1.Controls.Add(Me.Label5)
            Me.GroupBox1.Controls.Add(Me.Label4)
            Me.GroupBox1.Controls.Add(Me.Label3)
            Me.GroupBox1.Controls.Add(Me.Label2)
            Me.GroupBox1.Controls.Add(Me.Label1)
            Me.GroupBox1.Controls.Add(Me.txtResultados)
            Me.GroupBox1.Controls.Add(Me.btnLimpiar)
            Me.GroupBox1.Controls.Add(Me.btnCalcular)
            Me.GroupBox1.Controls.Add(Me.txtFuncionCD)
            Me.GroupBox1.Controls.Add(Me.txtFuncionAB)
            Me.GroupBox1.Controls.Add(Me.lblFuncionAB)
            Me.GroupBox1.Controls.Add(Me.PictureBox5)
            Me.GroupBox1.Location = New System.Drawing.Point(6, 16)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(907, 792)
            Me.GroupBox1.TabIndex = 61
            Me.GroupBox1.TabStop = False
            '
            'frmDerPr3
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(921, 861)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.PictureBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MaximizeBox = False
            Me.Name = "frmDerPr3"
            Me.Text = "Derivada de un Producto"
            CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox1.ResumeLayout(False)
            Me.GroupBox1.PerformLayout()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents lblFuncionAB As Label
        Friend WithEvents txtFuncionAB As TextBox
        Friend WithEvents txtFuncionCD As TextBox
        Friend WithEvents btnCalcular As Button
        Friend WithEvents btnLimpiar As Button
        Friend WithEvents txtResultados As TextBox
        Friend WithEvents Label1 As Label
        Friend WithEvents Label2 As Label
        Friend WithEvents Label3 As Label
        Friend WithEvents Label4 As Label
        Friend WithEvents Label5 As Label
        Friend WithEvents Label6 As Label
        Friend WithEvents PictureBox5 As PictureBox

        Private Sub frmDerPr3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

            ' Valores precargados, unico cambio(06042025)
            txtFuncionAB.Text = "9x^7 - 6x^5 + 3"
            txtFuncionCD.Text = "8x^5 - 4x^4"

        End Sub

        Friend WithEvents PictureBox1 As PictureBox
        Friend WithEvents GroupBox1 As GroupBox
    End Class
End Namespace