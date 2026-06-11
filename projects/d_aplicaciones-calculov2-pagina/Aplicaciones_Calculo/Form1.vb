Imports System.Text
Imports System.Text.RegularExpressions

Public Class frmDer_Pasos
    Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click
        ' Verificar que se ha ingresado una función
        If String.IsNullOrWhiteSpace(txtFuncion.Text) Then
            MessageBox.Show("Por favor, ingrese una función polinómica.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Verificar que se ha ingresado un valor para h
        If String.IsNullOrWhiteSpace(txtH.Text) Then
            MessageBox.Show("Por favor, ingrese un valor para h.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim h As Double
        If Not Double.TryParse(txtH.Text, h) Then
            MessageBox.Show("El valor de h debe ser un número.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Obtener la función ingresada y limpiarla
        Dim funcion As String = txtFuncion.Text.Trim()

        ' Calcular la derivada
        CalcularDerivada(funcion, h)
    End Sub

    Private Sub CalcularDerivada(funcion As String, h As Double)
        txtResultados.Clear()

        ' Extraer los términos del polinomio
        Dim terminos As List(Of TerminoPolinomio) = ExtraerTerminos(funcion)

        ' Paso 1: Identificar la función f(x)
        txtResultados.AppendText("Paso 1: Identificar nuestra función f(x)" & vbCrLf)
        txtResultados.AppendText("f(x) = " & funcion & vbCrLf & vbCrLf)

        ' Paso 2: Calcular f(x + h)
        txtResultados.AppendText("Paso 2: Calcular f(x + h)" & vbCrLf)
        txtResultados.AppendText("f(x + h) = " & ReemplazarX(funcion, "(x + h)") & vbCrLf)
        txtResultados.AppendText("Desarrollar usando binomio de Newton:" & vbCrLf)

        ' Expandir la expresión f(x + h)
        Dim fxhExpandido As String = ExpandirFuncion(terminos)
        txtResultados.AppendText(fxhExpandido & vbCrLf & vbCrLf)

        ' Paso 3: Calcular la diferencia f(x + h) - f(x)
        txtResultados.AppendText("Paso 3: Calcular la diferencia f(x + h) - f(x)" & vbCrLf)
        txtResultados.AppendText("" & vbCrLf)

        ' Calcular y mostrar la diferencia
        Dim terminosDiferencia As List(Of TerminoPolinomio) = CalcularDiferencia(terminos)
        Dim diferencia As String = FormatearPolinomioDiferencia(terminosDiferencia)
        txtResultados.AppendText("f(x+h) - f(x) = " & diferencia & vbCrLf & vbCrLf)

        ' Paso 4: Calcular la derivada [f(x + h) - f(x)] / h
        txtResultados.AppendText("Paso 4: Calcular la derivada [f(x + h) - f(x)] / h" & vbCrLf)

        ' Calcular y mostrar la derivada
        Dim terminosDerivada As List(Of TerminoPolinomio) = CalcularDerivadaFinal(terminosDiferencia, h)
        Dim derivada As String = FormatearPolinomioDerivada(terminosDerivada)
        txtResultados.AppendText("f'(x) = " & derivada & vbCrLf)
    End Sub

    Private Function ExtraerTerminos(funcion As String) As List(Of TerminoPolinomio)
        Dim terminos As New List(Of TerminoPolinomio)

        ' Patrón para capturar términos como 3x^2, -4x, 5, etc.
        Dim patron As String = "([+-]?\s*\d*\.?\d*)(x(?:\^(\d+))?)?"
        Dim coincidencias As MatchCollection = Regex.Matches(funcion, patron)

        For Each coincidencia As Match In coincidencias
            If String.IsNullOrWhiteSpace(coincidencia.Value) Then Continue For

            Dim coeficienteStr As String = coincidencia.Groups(1).Value.Replace(" ", "")
            Dim variable As String = coincidencia.Groups(2).Value
            Dim exponenteStr As String = coincidencia.Groups(3).Value

            ' Determinar coeficiente (manejar casos como "+x" o "-x")
            Dim coeficiente As Double = 1
            If coeficienteStr = "-" Then
                coeficiente = -1
            ElseIf coeficienteStr = "+" OrElse coeficienteStr = "" Then
                coeficiente = 1
            Else
                Double.TryParse(coeficienteStr, coeficiente)
            End If

            ' Determinar exponente
            Dim exponente As Integer = 0
            If Not String.IsNullOrEmpty(variable) Then
                exponente = If(String.IsNullOrEmpty(exponenteStr), 1, Integer.Parse(exponenteStr))
            End If

            terminos.Add(New TerminoPolinomio(coeficiente, exponente))
        Next

        Return terminos
    End Function

    Private Function ReemplazarX(funcion As String, reemplazo As String) As String
        ' Reemplaza las ocurrencias de "x" en la función
        Return Regex.Replace(funcion, "(?<!\w)x(?!\w)", reemplazo)
    End Function

    Private Function ExpandirFuncion(terminos As List(Of TerminoPolinomio)) As String
        Dim resultado As New StringBuilder()
        resultado.AppendLine("-->")

        ' Expandir cada término usando el binomio de Newton
        For Each termino In terminos
            Dim expansion As String = ExpandirTermino(termino)
            resultado.AppendLine(expansion)
        Next

        ' Combinar todos los términos expandidos
        Dim terminosCombinados As New List(Of TerminoPolinomio)()
        For Each termino In terminos
            terminosCombinados.AddRange(ExpandirTerminoConBinomio(termino))
        Next

        ' Agrupar términos similares
        Dim terminosAgrupados = AgruparTerminosSimilares(terminosCombinados)

        ' Primera línea: Mostrar todos los términos expandidos
        resultado.AppendLine("f(x+h) = " & FormatearPolinomioExpandido(terminosAgrupados))

        ' Segunda línea: Agrupar términos con la misma potencia de h
        resultado.AppendLine("f(x+h) = " & FormatearPolinomioAgrupadoPorH(terminosAgrupados))

        Return resultado.ToString()
    End Function

    Private Function ExpandirTermino(termino As TerminoPolinomio) As String
        Dim coeficiente As Double = termino.Coeficiente
        Dim exponente As Integer = termino.Exponente

        If exponente = 0 Then
            Return $"{coeficiente}"
        End If

        ' Generar la expansión del binomio de Newton para (x + h)^n
        Dim coefStr As String = If(Math.Abs(coeficiente) = 1, If(coeficiente < 0, "-", ""), coeficiente.ToString())

        Select Case exponente
            Case 1
                Return $"{coefStr}(x + h)"
            Case 2
                Return $"{coefStr}(x² + 2xh + h²)"
            Case 3
                Return $"{coefStr}(x³ + 3x²h + 3xh² + h³)"
            Case 4
                Return $"{coefStr}(x⁴ + 4x³h + 6x²h² + 4xh³ + h⁴)"
            Case 5
                Return $"{coefStr}(x⁵ + 5x⁴h + 10x³h² + 10x²h³ + 5xh⁴ + h⁵)"
            Case 6
                Return $"{coefStr}(x⁶ + 6x⁵h + 15x⁴h² + 20x³h³ + 15x²h⁴ + 6xh⁵ + h⁶)"
            Case 7
                Return $"{coefStr}(x⁷ + 7x⁶h + 21x⁵h² + 35x⁴h³ + 35x³h⁴ + 21x²h⁵ + 7xh⁶ + h⁷)"
            Case Else
                ' Para exponentes mayores, mostrar formato genérico
                Return $"{coefStr}(x + h)^{exponente} (expandido)"
        End Select
    End Function

    Private Function ExpandirTerminoConBinomio(termino As TerminoPolinomio) As List(Of TerminoPolinomio)
        Dim resultado As New List(Of TerminoPolinomio)()
        Dim coeficiente As Double = termino.Coeficiente
        Dim exponente As Integer = termino.Exponente

        If exponente = 0 Then
            resultado.Add(New TerminoPolinomio(coeficiente, 0, 0))
            Return resultado
        End If

        ' Aplicar el binomio de Newton: (x+h)^n = Σ(k=0 to n) C(n,k) * x^(n-k) * h^k
        For k As Integer = 0 To exponente
            Dim coefBinomial As Long = ObtenerCoeficienteBinomial(exponente, k)
            Dim nuevoCoef As Double = coeficiente * coefBinomial
            Dim exponenteX As Integer = exponente - k
            Dim exponenteH As Integer = k

            resultado.Add(New TerminoPolinomio(nuevoCoef, exponenteX, exponenteH))
        Next

        Return resultado
    End Function

    Private Function ObtenerCoeficienteBinomial(n As Integer, k As Integer) As Long
        If k < 0 OrElse k > n Then Return 0
        If k = 0 OrElse k = n Then Return 1

        Dim resultado As Long = 1
        For i As Integer = 1 To Math.Min(k, n - k)
            resultado = resultado * n / i
            n -= 1
        Next

        Return resultado
    End Function

    Private Function AgruparTerminosSimilares(terminos As List(Of TerminoPolinomio)) As List(Of TerminoPolinomio)
        Dim resultado As New Dictionary(Of String, TerminoPolinomio)()

        For Each termino In terminos
            Dim clave As String = $"{termino.ExponenteX}_{termino.ExponenteH}"

            If resultado.ContainsKey(clave) Then
                ' Sumar coeficientes para términos con los mismos exponentes
                resultado(clave).Coeficiente += termino.Coeficiente
            Else
                ' Añadir nuevo término
                resultado.Add(clave, New TerminoPolinomio(termino.Coeficiente, termino.ExponenteX, termino.ExponenteH))
            End If
        Next

        ' Ordenar términos por exponente de x (descendente) y luego por exponente de h (ascendente)
        Return resultado.Values.OrderByDescending(Function(t) t.ExponenteX).ThenBy(Function(t) t.ExponenteH).ToList()
    End Function

    Private Function FormatearPolinomioExpandido(terminos As List(Of TerminoPolinomio)) As String
        If terminos.Count = 0 Then Return "0"

        Dim resultado As New StringBuilder()
        Dim primerTermino As Boolean = True

        For Each termino In terminos
            Dim coef As Double = termino.Coeficiente

            ' Ignorar términos con coeficiente cero
            If Math.Abs(coef) < 0.00001 Then Continue For

            ' Añadir signo
            If primerTermino Then
                If coef < 0 Then
                    resultado.Append("-")
                End If
            Else
                resultado.Append(If(coef < 0, " - ", " + "))
            End If

            ' Añadir coeficiente (excepto si es 1 y hay una variable)
            Dim coefAbs As Double = Math.Abs(coef)
            Dim hayVariable As Boolean = termino.ExponenteX > 0 Or termino.ExponenteH > 0

            If coefAbs <> 1 Or Not hayVariable Then
                resultado.Append(coefAbs)
            End If

            ' Añadir variables con sus exponentes
            If termino.ExponenteX > 0 Then
                resultado.Append("x")
                If termino.ExponenteX > 1 Then
                    resultado.Append("^" & termino.ExponenteX)
                End If
            End If

            If termino.ExponenteH > 0 Then
                resultado.Append("h")
                If termino.ExponenteH > 1 Then
                    resultado.Append("^" & termino.ExponenteH)
                End If
            End If

            primerTermino = False
        Next

        Return resultado.ToString()
    End Function

    Private Function FormatearPolinomioAgrupadoPorH(terminos As List(Of TerminoPolinomio)) As String
        If terminos.Count = 0 Then Return "0"

        ' Agrupar términos por potencia de h
        Dim gruposPorH As New Dictionary(Of Integer, List(Of TerminoPolinomio))()

        For Each termino In terminos
            Dim exponenteH As Integer = termino.ExponenteH

            If Not gruposPorH.ContainsKey(exponenteH) Then
                gruposPorH.Add(exponenteH, New List(Of TerminoPolinomio)())
            End If

            gruposPorH(exponenteH).Add(termino)
        Next

        ' Ordenar grupos por potencia de h
        Dim exponentesH As List(Of Integer) = gruposPorH.Keys.OrderBy(Function(k) k).ToList()

        Dim resultado As New StringBuilder()
        Dim primerGrupo As Boolean = True

        For Each exponenteH In exponentesH
            Dim termiosDelGrupo As List(Of TerminoPolinomio) = gruposPorH(exponenteH)

            ' Ignorar grupos vacíos
            If termiosDelGrupo.Count = 0 Then Continue For

            ' Factorizar h^k
            Dim factorH As String = If(exponenteH = 0, "", If(exponenteH = 1, "h", "h^" & exponenteH))

            ' Terminos dentro del paréntesis (sin h)
            Dim terminosFactorizados As New StringBuilder()
            Dim primerTermino As Boolean = True

            For Each termino In termiosDelGrupo.OrderByDescending(Function(t) t.ExponenteX)
                Dim coef As Double = termino.Coeficiente

                ' Ignorar términos con coeficiente cero
                If Math.Abs(coef) < 0.00001 Then Continue For

                ' Añadir signo
                If primerTermino Then
                    If coef < 0 Then
                        terminosFactorizados.Append("-")
                    End If
                Else
                    terminosFactorizados.Append(If(coef < 0, " - ", " + "))
                End If

                ' Añadir coeficiente (excepto si es 1 y hay una variable)
                Dim coefAbs As Double = Math.Abs(coef)
                Dim hayVariable As Boolean = termino.ExponenteX > 0

                If coefAbs <> 1 Or Not hayVariable Then
                    terminosFactorizados.Append(coefAbs)
                End If

                ' Añadir variable x con su exponente
                If termino.ExponenteX > 0 Then
                    terminosFactorizados.Append("x")
                    If termino.ExponenteX > 1 Then
                        terminosFactorizados.Append("^" & termino.ExponenteX)
                    End If
                End If

                primerTermino = False
            Next

            ' Añadir grupo al resultado
            If Not primerGrupo Then
                resultado.Append(" + ")
            End If

            ' Si hay más de un término, ponerlo entre paréntesis
            Dim contenidoParentesis As String = terminosFactorizados.ToString()

            If exponenteH = 0 Or termiosDelGrupo.Count = 1 Then
                resultado.Append(contenidoParentesis)
                If exponenteH > 0 Then
                    resultado.Append(factorH)
                End If
            Else
                resultado.Append("(")
                resultado.Append(contenidoParentesis)
                resultado.Append(")")
                resultado.Append(factorH)
            End If

            primerGrupo = False
        Next

        Return resultado.ToString()
    End Function

    Private Function FormatearPolinomioDiferencia(terminos As List(Of TerminoPolinomio)) As String
        Dim resultado As New StringBuilder()

        ' Agrupar términos por potencia de h
        Dim gruposPorH As New Dictionary(Of Integer, List(Of TerminoPolinomio))()

        For Each termino In terminos
            Dim exponenteH As Integer = termino.ExponenteH

            If Not gruposPorH.ContainsKey(exponenteH) Then
                gruposPorH.Add(exponenteH, New List(Of TerminoPolinomio)())
            End If

            gruposPorH(exponenteH).Add(termino)
        Next

        ' Ordenar grupos por potencia de h
        Dim exponentesH As List(Of Integer) = gruposPorH.Keys.OrderBy(Function(k) k).ToList()

        Dim primerGrupo As Boolean = True

        ' Primero escribir términos con h^1
        If gruposPorH.ContainsKey(1) Then
            Dim terminosH1 As String = FormatearGrupoTerminos(gruposPorH(1), False)
            If Not String.IsNullOrEmpty(terminosH1) Then
                resultado.Append(terminosH1)
                If Not terminosH1.EndsWith("h") Then
                    resultado.Append("h")
                End If
                primerGrupo = False
            End If
        End If

        ' Luego términos con potencias de h más altas (h^2, h^3, etc.)
        For Each exponenteH In exponentesH.Where(Function(e) e > 1).OrderBy(Function(e) e)
            Dim terminosDelGrupo As String = FormatearGrupoTerminos(gruposPorH(exponenteH), True)
            If Not String.IsNullOrEmpty(terminosDelGrupo) Then
                If Not primerGrupo Then
                    resultado.Append(" + ")
                End If
                resultado.Append(terminosDelGrupo)
                resultado.Append("h^" & exponenteH)
                primerGrupo = False
            End If
        Next

        If resultado.Length = 0 Then
            Return "0"
        End If

        Return resultado.ToString()
    End Function

    Private Function FormatearGrupoTerminos(terminos As List(Of TerminoPolinomio), usarParentesis As Boolean) As String
        Dim resultado As New StringBuilder()
        Dim primerTermino As Boolean = True

        For Each termino In terminos.OrderByDescending(Function(t) t.ExponenteX)
            Dim coef As Double = termino.Coeficiente

            ' Ignorar términos con coeficiente cero
            If Math.Abs(coef) < 0.00001 Then Continue For

            ' Añadir signo
            If primerTermino Then
                If coef < 0 Then
                    resultado.Append("-")
                End If
            Else
                resultado.Append(If(coef < 0, " - ", " + "))
            End If

            ' Añadir coeficiente (excepto si es 1 y hay una variable)
            Dim coefAbs As Double = Math.Abs(coef)
            Dim hayVariable As Boolean = termino.ExponenteX > 0

            If coefAbs <> 1 Or Not hayVariable Then
                resultado.Append(coefAbs)
            End If

            ' Añadir variable x con su exponente
            If termino.ExponenteX > 0 Then
                resultado.Append("x")
                If termino.ExponenteX > 1 Then
                    resultado.Append("^" & termino.ExponenteX)
                End If
            End If

            primerTermino = False
        Next

        Dim contenido As String = resultado.ToString()

        If String.IsNullOrEmpty(contenido) Then
            Return ""
        End If

        ' Si hay más de un término y se requiere paréntesis, ponerlo entre paréntesis
        If usarParentesis And terminos.Count > 1 Then
            Return "(" & contenido & ")"
        Else
            Return contenido
        End If
    End Function

    Private Function CalcularDiferencia(terminos As List(Of TerminoPolinomio)) As List(Of TerminoPolinomio)
        Dim resultado As New List(Of TerminoPolinomio)()

        ' Para cada término en la expansión, mantenemos solo los términos que tienen h
        For Each termino In ExpandirFuncionCompleta(terminos)
            If termino.ExponenteH > 0 Then
                resultado.Add(New TerminoPolinomio(termino.Coeficiente, termino.ExponenteX, termino.ExponenteH))
            End If
        Next

        Return AgruparTerminosSimilares(resultado)
    End Function

    Private Function ExpandirFuncionCompleta(terminos As List(Of TerminoPolinomio)) As List(Of TerminoPolinomio)
        Dim resultado As New List(Of TerminoPolinomio)()

        For Each termino In terminos
            resultado.AddRange(ExpandirTerminoConBinomio(termino))
        Next

        Return AgruparTerminosSimilares(resultado)
    End Function

    Private Function CalcularDerivadaFinal(terminosDiferencia As List(Of TerminoPolinomio), h As Double) As List(Of TerminoPolinomio)
        Dim resultado As New List(Of TerminoPolinomio)()

        ' Cálculo del límite: dividir todos los términos por h y eliminar los términos con h
        For Each termino In terminosDiferencia
            If termino.ExponenteH > 0 Then
                Dim nuevoCoeficiente As Double = termino.Coeficiente / termino.ExponenteH  ' Dividir por el exponente de h para derivación
                Dim nuevoExponenteH As Integer = termino.ExponenteH - 1

                If h = 0 And nuevoExponenteH = 0 Then
                    ' Si h = 0, solo mantener términos donde h tiene exponente 1 (después de dividir)
                    resultado.Add(New TerminoPolinomio(nuevoCoeficiente, termino.ExponenteX, nuevoExponenteH))
                ElseIf h <> 0 Then
                    resultado.Add(New TerminoPolinomio(nuevoCoeficiente, termino.ExponenteX, nuevoExponenteH))
                End If
            End If
        Next

        ' Simplificar eliminando términos con h
        If h = 0 Then
            Return resultado
        Else
            Dim derivadaSimplificada As New List(Of TerminoPolinomio)()
            For Each termino In resultado
                If termino.ExponenteH = 0 Then
                    derivadaSimplificada.Add(New TerminoPolinomio(termino.Coeficiente, termino.ExponenteX, 0))
                End If
            Next
            Return AgruparTerminosSimilares(derivadaSimplificada)
        End If
    End Function

    Private Function FormatearPolinomioDerivada(terminos As List(Of TerminoPolinomio)) As String
        If terminos.Count = 0 Then Return "0"

        Dim resultado As New StringBuilder()
        Dim primerTermino As Boolean = True

        For Each termino In terminos.OrderByDescending(Function(t) t.ExponenteX)
            Dim coef As Double = termino.Coeficiente

            ' Ignorar términos con coeficiente cero
            If Math.Abs(coef) < 0.00001 Then Continue For

            ' Añadir signo
            If primerTermino Then
                If coef < 0 Then
                    resultado.Append("-")
                End If
            Else
                resultado.Append(If(coef < 0, " - ", " + "))
            End If

            ' Añadir coeficiente (excepto si es 1 y hay una variable)
            Dim coefAbs As Double = Math.Abs(coef)
            Dim hayVariable As Boolean = termino.ExponenteX > 0

            If coefAbs <> 1 Or Not hayVariable Then
                resultado.Append(coefAbs)
            End If

            ' Añadir variables con sus exponentes
            If termino.ExponenteX > 0 Then
                resultado.Append("x")
                If termino.ExponenteX > 1 Then
                    resultado.Append("^" & termino.ExponenteX)
                End If
            End If

            primerTermino = False
        Next

        Return resultado.ToString()
    End Function

    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        txtFuncion.Clear()
        txtH.Clear()
        txtResultados.Clear()
    End Sub

    Private Sub frmDerivadas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Derivadas por Pasos"
        txtResultados.Multiline = True
        txtResultados.ScrollBars = ScrollBars.Vertical
        txtResultados.ReadOnly = True

        ' Valores precargados(unico cambio06042025)
        txtFuncion.Text = "12x^7-2x^3"
        txtH.Text = "0"

    End Sub
End Class

' Clase para representar un término de un polinomio
Public Class TerminoPolinomio
    Public Property Coeficiente As Double
    Public Property ExponenteX As Integer
    Public Property ExponenteH As Integer

    Public Sub New(coef As Double, expX As Integer)
        Coeficiente = coef
        ExponenteX = expX
        ExponenteH = 0
    End Sub

    Public Sub New(coef As Double, expX As Integer, expH As Integer)
        Coeficiente = coef
        ExponenteX = expX
        ExponenteH = expH
    End Sub

    Public ReadOnly Property Exponente As Integer
        Get
            Return ExponenteX
        End Get
    End Property
End Class