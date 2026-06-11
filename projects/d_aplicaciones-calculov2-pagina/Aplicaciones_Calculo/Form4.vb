Imports System.Math
Imports System.Text
Imports System.Text.RegularExpressions

Public Class frm_derTri

    ' Función para convertir grados a radianes
    Private Function GradosARadianes(grados As Double) As Double
        Return grados * (PI / 180)
    End Function

    ' Función para cálculos básicos (sin, cos, tan, arcsin, arccos, arctan, cot, sec, csc)
    Private Function CalcularFuncionBasica(funcion As String, x As Double) As Double
        Dim valorEnRadianes As Double = GradosARadianes(x)
        Select Case funcion.ToLower()
            Case "sin" : Return Sin(valorEnRadianes)
            Case "cos" : Return Cos(valorEnRadianes)
            Case "tan" : Return Tan(valorEnRadianes)
            Case "cot" : Return 1 / Tan(valorEnRadianes)
            Case "sec" : Return 1 / Cos(valorEnRadianes)
            Case "csc" : Return 1 / Sin(valorEnRadianes)
            Case "arcsin" : Return Asin(x) * (180 / PI) ' Convierte a grados
            Case "arccos" : Return Acos(x) * (180 / PI)
            Case "arctan" : Return Atan(x) * (180 / PI)
            Case Else : Throw New ArgumentException("Función no reconocida.")
        End Select
    End Function

    ' Función principal para derivar ecuaciones
    Private Function DerivarEcuacionUniversal(ecuacion As String) As String
        Dim pasos As New StringBuilder()
        Try
            ' Normalizar la entrada
            Dim ecuacionLimpia As String = ecuacion.ToLower().Replace(" ", "").Replace("sen", "sin")

            ' Verificar si es un caso especial como ((sin(4x^3))(cos(9x^3)^3))^8
            Dim patronEspecial As String = "^\(\((sin|cos|tan|cot|sec|csc)\((\d+)x\^(\d+)\)\)\((sin|cos|tan|cot|sec|csc)\((\d+)x\^(\d+)\)\^(\d+)\)\)\^(\d+)$"
            Dim matchEspecial As Match = Regex.Match(ecuacionLimpia, patronEspecial)

            If matchEspecial.Success Then
                ' Es un caso especial del tipo ((func1(ax^b))(func2(cx^d)^e))^f
                Dim func1 As String = matchEspecial.Groups(1).Value
                Dim coef1 As Integer = Integer.Parse(matchEspecial.Groups(2).Value)
                Dim exp1 As Integer = Integer.Parse(matchEspecial.Groups(3).Value)
                Dim func2 As String = matchEspecial.Groups(4).Value
                Dim coef2 As Integer = Integer.Parse(matchEspecial.Groups(5).Value)
                Dim exp2 As Integer = Integer.Parse(matchEspecial.Groups(6).Value)
                Dim expFunc2 As Integer = Integer.Parse(matchEspecial.Groups(7).Value)
                Dim expTotal As Integer = Integer.Parse(matchEspecial.Groups(8).Value)

                ' Formatear para la presentación
                Dim func1Formato As String = $"{func1}({coef1}x{FormatearExponente(exp1)})"
                Dim func2Formato As String = $"{func2}({coef2}x{FormatearExponente(exp2)})^{expFunc2}"

                ' Calcular derivadas
                Dim derivFunc1 As String = DerivarFuncionEspecial(func1, coef1, exp1)
                Dim derivFunc2 As String = DerivarFuncionCompuestaEspecial(func2, coef2, exp2, expFunc2)

                ' Aplicar regla del producto
                Dim derivProducto As String = $"{derivFunc1} * {func2Formato} + {func1Formato} * ({derivFunc2})"

                ' Aplicar regla de la potencia para el exponente exterior
                Dim derivadaFinal As String = $"{expTotal}*(({func1Formato})({func2Formato}))^{expTotal - 1} * ({derivProducto})"

                ' Generar los pasos
                pasos.AppendLine($"d/dx[{func1Formato}] = {derivFunc1}")
                pasos.AppendLine($"d/dx[{func2Formato}] = {derivFunc2}")
                pasos.AppendLine($"d/dx[({func1Formato})({func2Formato})] = {derivProducto}")
                pasos.AppendLine($"y´={derivadaFinal}")

                Return pasos.ToString()
            Else
                ' Para otros casos, usar el enfoque más genérico
                pasos.AppendLine($"Ecuación original: {ecuacion}")
                pasos.AppendLine("-----------------------------------")
                pasos.AppendLine(DerivarPorReglas(ecuacionLimpia))
                Return pasos.ToString()
            End If
        Catch ex As Exception
            Return $"Error: {ex.Message}"
        End Try
    End Function

    ' Función para derivar funciones trigonométricas específicas con coeficientes
    Private Function DerivarFuncionEspecial(funcion As String, coef As Integer, exp As Integer) As String
        Dim derivadaFunc As String = ""
        Select Case funcion.ToLower()
            Case "sin" : derivadaFunc = $"cos({coef}x{FormatearExponente(exp)})"
            Case "cos" : derivadaFunc = $"-sin({coef}x{FormatearExponente(exp)})"
            Case "tan" : derivadaFunc = $"sec²({coef}x{FormatearExponente(exp)})"
            Case "cot" : derivadaFunc = $"-csc²({coef}x{FormatearExponente(exp)})"
            Case "sec" : derivadaFunc = $"sec({coef}x{FormatearExponente(exp)}) * tan({coef}x{FormatearExponente(exp)})"
            Case "csc" : derivadaFunc = $"-csc({coef}x{FormatearExponente(exp)}) * cot({coef}x{FormatearExponente(exp)})"
        End Select

        ' Calcular la derivada del argumento interno
        Dim nuevoCoef As Integer = coef * exp
        Dim nuevoExp As Integer = exp - 1
        Dim derivadaArg As String = If(nuevoExp = 1, $"{nuevoCoef}x", $"{nuevoCoef}x{FormatearExponente(nuevoExp)}")

        Return $"{derivadaFunc} * {derivadaArg}"
    End Function

    ' Función para derivar funciones trigonométricas compuestas (con potencia)
    Private Function DerivarFuncionCompuestaEspecial(funcion As String, coef As Integer, exp As Integer, expFunc As Integer) As String
        Dim derivadaInterior As String = ""
        Select Case funcion.ToLower()
            Case "sin" : derivadaInterior = $"cos({coef}x{FormatearExponente(exp)})"
            Case "cos" : derivadaInterior = $"-sin({coef}x{FormatearExponente(exp)})"
            Case "tan" : derivadaInterior = $"sec²({coef}x{FormatearExponente(exp)})"
            Case "cot" : derivadaInterior = $"-csc²({coef}x{FormatearExponente(exp)})"
            Case "sec" : derivadaInterior = $"sec({coef}x{FormatearExponente(exp)}) * tan({coef}x{FormatearExponente(exp)})"
            Case "csc" : derivadaInterior = $"-csc({coef}x{FormatearExponente(exp)}) * cot({coef}x{FormatearExponente(exp)})"
        End Select

        ' Aplicar regla de la potencia
        Dim termPotencia As String = $"{expFunc}*{funcion}({coef}x{FormatearExponente(exp)})^{expFunc - 1}"

        ' Derivada del argumento interno
        Dim nuevoCoef As Integer = coef * exp
        Dim nuevoExp As Integer = exp - 1
        Dim derivadaArg As String = If(nuevoExp = 1, $"{nuevoCoef}x", $"{nuevoCoef}x{FormatearExponente(nuevoExp)}")

        Return $"{termPotencia} * {derivadaInterior} * {derivadaArg}"
    End Function

    ' Función para formatear exponentes como superíndices
    Private Function FormatearExponente(exp As Integer) As String
        If exp = 1 Then
            Return ""
        ElseIf exp = 2 Then
            Return "²"
        ElseIf exp = 3 Then
            Return "³"
        Else
            Return $"^{exp}"
        End If
    End Function

    ' Aplica reglas de derivación generales
    Private Function DerivarPorReglas(ecuacion As String) As String
        Dim pasos As New StringBuilder()

        ' Paso 1: Derivar funciones trigonométricas compuestas
        Dim patronTrig As String = "(sin|cos|tan|cot|sec|csc|arcsin|arccos|arctan)\(([^)]+)\)"
        For Each match As Match In Regex.Matches(ecuacion, patronTrig)
            Dim funcion As String = match.Groups(1).Value
            Dim argumento As String = match.Groups(2).Value
            Dim derivadaFuncion As String = DerivarFuncionTrig(funcion, argumento)
            Dim derivadaArgumento As String = DerivarArgumento(argumento)
            pasos.AppendLine($"d/dx[{funcion}({argumento})] = {derivadaFuncion} * {derivadaArgumento}")
        Next

        ' Paso 2: Derivar productos (regla del producto)
        If ecuacion.Contains("*") Then
            Dim terminos() As String = ecuacion.Split("*"c)
            If terminos.Length = 2 Then
                Dim u As String = terminos(0)
                Dim v As String = terminos(1)
                pasos.AppendLine($"d/dx[{u}*{v}] = d/dx[{u}]*{v} + {u}*d/dx[{v}]")
            End If
        End If

        ' Paso 3: Derivar potencias (regla de la cadena + potencia)
        Dim patronPotencia As String = "(\(?.+?\)?)\^(\d+)"
        For Each match As Match In Regex.Matches(ecuacion, patronPotencia)
            Dim base As String = match.Groups(1).Value
            Dim exponente As Integer = Integer.Parse(match.Groups(2).Value)
            pasos.AppendLine($"d/dx[{base}^{exponente}] = {exponente}*{base}^{exponente - 1} * d/dx[{base}]")
        Next

        Return pasos.ToString()
    End Function

    ' Derivadas de funciones trigonométricas (reglas básicas)
    Private Function DerivarFuncionTrig(funcion As String, argumento As String) As String
        Select Case funcion.ToLower()
            Case "sin" : Return $"cos({argumento})"
            Case "cos" : Return $"-sin({argumento})"
            Case "tan" : Return $"sec²({argumento})"
            Case "cot" : Return $"-csc²({argumento})"
            Case "sec" : Return $"sec({argumento}) * tan({argumento})"
            Case "csc" : Return $"-csc({argumento}) * cot({argumento})"
            Case "arcsin" : Return $"1/√(1 - {argumento}²)"
            Case "arccos" : Return $"-1/√(1 - {argumento}²)"
            Case "arctan" : Return $"1/({argumento}² + 1)"
            Case Else : Return "0"
        End Select
    End Function

    ' Derivada del argumento interno (ej: 4x³ → 12x²)
    Private Function DerivarArgumento(argumento As String) As String
        If argumento.Contains("x") Then
            Dim match As Match = Regex.Match(argumento, "(\d*)x\^?(\d*)")
            If match.Success Then
                Dim coef As Integer = If(String.IsNullOrEmpty(match.Groups(1).Value), 1, Integer.Parse(match.Groups(1).Value))
                Dim exp As Integer = If(String.IsNullOrEmpty(match.Groups(2).Value), 1, Integer.Parse(match.Groups(2).Value))
                Dim nuevoCoef As Integer = coef * exp
                Dim nuevoExp As Integer = exp - 1
                Return If(nuevoExp = 1, $"{nuevoCoef}x", $"{nuevoCoef}x^{nuevoExp}")
            End If
        End If
        Return "1"
    End Function

    ' Evento para calcular derivadas
    Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click
        Try
            txtResultados.Text = DerivarEcuacionUniversal(txtFuncionTri.Text)
        Catch ex As Exception
            txtResultados.Text = $"Error: {ex.Message}"
        End Try
    End Sub

    ' Evento para limpiar campos
    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        txtShowTri.Clear()
        txtFuncionTri.Clear()
        txtResultados.Clear()
    End Sub

    ' Evento para cálculos básicos (al presionar Enter en txtShowTri)
    Private Sub txtShowTri_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtShowTri.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            e.Handled = True ' Evita el sonido de error al presionar Enter
            Try
                Dim entrada As String = txtShowTri.Text.Trim()
                If String.IsNullOrWhiteSpace(entrada) Then Exit Sub
                entrada = entrada.Replace("sen", "sin").Replace("exp", "e^")
                Dim partes() As String = entrada.Split("("c)
                If partes.Length <> 2 Then Throw New FormatException("Formato inválido. Ejemplo: sin(30)")
                Dim funcion As String = partes(0).ToLower().Trim()
                Dim valorStr As String = partes(1).Replace(")", "").Trim()
                Dim x As Double
                If Not Double.TryParse(valorStr, x) Then
                    Throw New FormatException("Número inválido en la entrada.")
                End If
                txtShowTri.Text = CalcularFuncionBasica(funcion, x).ToString("F6")
            Catch ex As Exception
                txtShowTri.Text = $"Error: {ex.Message}"
            End Try
        End If
    End Sub


    ' unico cambio nuevo 06042025

    Private Sub frm_derTri_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Valores precargados
        txtShowTri.Text = "sin(30)"
        txtFuncionTri.Text = "((sen(4x^3))(cos(9x^3)^3))^8"
    End Sub


End Class