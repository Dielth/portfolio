Public Class frmDer_Cadena
    Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click
        ' Verificar que se hayan ingresado ambas funciones
        If String.IsNullOrEmpty(txtFuncion_1.Text) Or String.IsNullOrEmpty(txtFuncion_2.Text) Then
            MessageBox.Show("Por favor, ingrese ambas funciones.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Verificar que se haya seleccionado una operación
        If Not rd_Multiply.Checked And Not rd_Divide.Checked Then
            MessageBox.Show("Por favor, seleccione una operación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Obtener las funciones ingresadas
        Dim u As String = txtFuncion_1.Text
        Dim v As String = txtFuncion_2.Text

        ' Calcular la derivada según la operación seleccionada
        Dim resultado As String = ""
        If rd_Multiply.Checked Then
            resultado = CalcularDerivadaProducto(u, v)
        ElseIf rd_Divide.Checked Then
            resultado = CalcularDerivadaCociente(u, v)
        End If

        ' Mostrar el resultado en el TextBox
        txtResultados.Text = resultado
    End Sub

    Private Function CalcularDerivadaProducto(u As String, v As String) As String
        Dim resultado As New System.Text.StringBuilder()

        ' Derivar u y v
        Dim uDerivada As String = DerivarFuncion(u)
        Dim vDerivada As String = DerivarFuncion(v)

        ' Mostrar pasos intermedios
        resultado.AppendLine($"{u}={ExpandirExpresion(u)}")
        resultado.AppendLine($"{v}={ExpandirExpresion(v)}")
        resultado.AppendLine($"u'(x) = {uDerivada}")
        resultado.AppendLine($"v'(x) = {vDerivada}")

        ' Aplicar la regla del producto
        resultado.AppendLine(vbCrLf & "Aplicamos la regla del producto:")
        resultado.AppendLine($"f'(x) = {uDerivada}·{v} + {u}·{vDerivada}")

        ' Resultado final
        resultado.AppendLine(vbCrLf & "La derivada de f(x) es:")
        resultado.AppendLine($"f'(x) = {uDerivada}·{v} + {u}·{vDerivada}")

        Return resultado.ToString()
    End Function

    Private Function CalcularDerivadaCociente(u As String, v As String) As String
        Dim resultado As New System.Text.StringBuilder()

        ' Derivar u y v
        Dim uDerivada As String = DerivarFuncion(u)
        Dim vDerivada As String = DerivarFuncion(v)

        ' Mostrar pasos intermedios
        resultado.AppendLine($"{u}={ExpandirExpresion(u)}")
        resultado.AppendLine($"{v}={ExpandirExpresion(v)}")
        resultado.AppendLine($"u'(x) = {uDerivada}")
        resultado.AppendLine($"v'(x) = {vDerivada}")

        ' Aplicar la regla del cociente
        resultado.AppendLine(vbCrLf & "Aplicamos la regla del cociente:")
        resultado.AppendLine($"f'(x) = [{uDerivada}·{v} - {u}·{vDerivada}] / ({v})^2")

        ' Resultado final
        resultado.AppendLine(vbCrLf & "La derivada de f(x) es:")
        resultado.AppendLine($"f'(x) = [{uDerivada}·{v} - {u}·{vDerivada}] / ({v})^2")

        Return resultado.ToString()
    End Function

    Private Function DerivarFuncion(funcion As String) As String
        ' Si la función es una potencia de binomio, aplicar la regla de la cadena
        If funcion.Contains("^") AndAlso funcion.Contains("(") Then
            Dim base As String = funcion.Substring(funcion.IndexOf("(") + 1, funcion.LastIndexOf(")") - funcion.IndexOf("(") - 1)
            Dim exponente As Integer = Integer.Parse(funcion.Substring(funcion.LastIndexOf("^") + 1))
            Dim derivadaBase As String = DerivarPolinomio(base)

            Return $"{exponente}({base})^{exponente - 1}·({derivadaBase})"
        End If

        ' Si no, derivar como un polinomio simple
        Return DerivarPolinomio(funcion)
    End Function

    Private Function DerivarPolinomio(polinomio As String) As String
        Dim terminos As New List(Of String)()
        Dim signo As Char = "+"
        Dim terminoActual As String = ""

        For Each c As Char In polinomio
            If c = "+"c OrElse c = "-"c Then
                If terminoActual <> "" Then
                    terminos.Add(signo & terminoActual)
                End If
                signo = c
                terminoActual = ""
            Else
                terminoActual &= c
            End If
        Next

        If terminoActual <> "" Then
            terminos.Add(signo & terminoActual)
        End If

        Dim resultado As New System.Text.StringBuilder()

        For Each termino In terminos
            Dim signoTermino As Char = termino(0)
            Dim contenido As String = termino.Substring(1).Trim()

            Dim partes As String() = contenido.Split(New String() {"x^", "x"}, StringSplitOptions.RemoveEmptyEntries)
            Dim coeficiente As Double = If(partes(0) = "", 1, Double.Parse(partes(0)))
            Dim exponente As Double = If(partes.Length > 1, Double.Parse(partes(1)), If(contenido.Contains("x"), 1, 0))

            If exponente = 0 Then
                Continue For
            End If

            Dim nuevoCoeficiente As Double = coeficiente * exponente
            Dim nuevoExponente As Double = exponente - 1

            ' Asignar el signo correcto
            If signoTermino = "-"c Then
                nuevoCoeficiente *= -1
            End If

            If nuevoCoeficiente > 0 AndAlso resultado.Length > 0 Then
                resultado.Append("+")
            ElseIf nuevoCoeficiente < 0 Then
                resultado.Append("-")
                nuevoCoeficiente = Math.Abs(nuevoCoeficiente)
            End If

            resultado.Append(nuevoCoeficiente.ToString())

            If nuevoExponente > 0 Then
                resultado.Append("x")
                If nuevoExponente > 1 Then
                    resultado.Append("^").Append(nuevoExponente)
                End If
            End If
        Next

        Return If(resultado.Length = 0, "0", resultado.ToString())
    End Function

    Private Function ExpandirExpresion(expresion As String) As String
        ' Simplemente devuelve la expresión sin cambios (puedes implementar expansión si es necesario)
        Return expresion
    End Function

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        ' Limpiar todos los campos
        txtFuncion_1.Clear()
        txtFuncion_2.Clear()
        txtResultados.Clear()
        rd_Multiply.Checked = False
        rd_Divide.Checked = False
    End Sub


    ' unico cambio/implementacion06042025

    Private Sub frmDer_Cadena_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Valores precargados
        txtFuncion_1.Text = "(7x^5-4x^3)^5"
        txtFuncion_2.Text = "(6x^2 + 8)^6"
        rd_Multiply.Checked = True
    End Sub

End Class