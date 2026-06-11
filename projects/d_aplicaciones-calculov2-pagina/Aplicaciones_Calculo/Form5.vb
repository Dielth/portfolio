'Codigo Original acorde solo a los rangos, para el programa de maximos y minimos_1

'Imports org.mariuszgromada.math.mxparser
'Imports System.Math

'Public Class frm_MAXMIN
'    Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click
'        lstResultados.Items.Clear()
'        Dim funcionStr As String = txtFuncionMax.Text.Trim()

'        ' Validar entrada vacía
'        If String.IsNullOrEmpty(funcionStr) Then
'            MessageBox.Show("Ingrese una función válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            Return
'        End If

'        ' Configurar función y derivadas
'        Dim x As New Argument("x")
'        Dim funcion As New Expression(funcionStr, x)

'        ' Verificar sintaxis antes de calcular
'        If Not funcion.checkSyntax() Then
'            MessageBox.Show($"Error en la función: {funcion.getErrorMessage()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            Return
'        End If

'        Try
'            ' Calcular derivada
'            Dim derivada As New Expression("der(" & funcionStr & ", x)", x)
'            Dim min As Double = -10, max As Double = 10, paso As Double = 0.1

'            ' Ajustar rango según ejemplos
'            If funcionStr.Replace(" ", "").ToLower() = "x^3/3+x^2-8*x" Then
'                min = -5 : max = 5
'            ElseIf funcionStr.Replace(" ", "").ToLower() = "(80-2*x)/2*(50-2*x)*x" Then
'                min = 0 : max = 40
'            End If

'            ' Lista para almacenar puntos críticos
'            Dim puntosCriticos As New List(Of Double)
'            EncontrarRaices(derivada, min, max, paso, puntosCriticos)

'            If puntosCriticos.Count = 0 Then
'                MessageBox.Show($"No se encontraron puntos críticos en el rango [{min} a {max}].", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
'                Return
'            End If

'            ' Segunda derivada para clasificar los puntos críticos
'            Dim segundaDerivada As New Expression("der(der(" & funcionStr & ", x), x)", x)

'            For Each xCritico In puntosCriticos
'                x.setArgumentValue(xCritico)
'                Dim tipo As String = If(segundaDerivada.calculate() > 0, "Mínimo", If(segundaDerivada.calculate() < 0, "Máximo", "Punto de silla"))
'                lstResultados.Items.Add(New ListViewItem({xCritico.ToString("0.000"), tipo, funcion.calculate().ToString("0.000")}))
'            Next

'        Catch ex As Exception
'            MessageBox.Show($"Error crítico: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    End Sub

'    Private Sub EncontrarRaices(derivada As Expression, min As Double, max As Double, paso As Double, ByRef raices As List(Of Double))
'        Dim x As Argument = derivada.getArgument("x")
'        x.setArgumentValue(min)
'        Dim signoAnterior As Integer = Math.Sign(derivada.calculate())

'        For xi As Double = min + paso To max Step paso
'            x.setArgumentValue(xi)
'            Dim valorActual As Double = derivada.calculate()

'            If Double.IsNaN(valorActual) Then Continue For

'            Dim signoActual As Integer = Math.Sign(valorActual)
'            If signoActual <> signoAnterior Then
'                Dim raiz As Double = RefinarRaiz(derivada, xi - paso, xi, 0.0001)
'                If Not Double.IsNaN(raiz) AndAlso Not raices.Exists(Function(r) Math.Abs(r - raiz) < 0.001) Then
'                    raices.Add(raiz)
'                End If
'            End If
'            signoAnterior = signoActual
'        Next
'    End Sub

'    Private Function RefinarRaiz(f As Expression, a As Double, b As Double, tolerancia As Double) As Double
'        Dim x As Argument = f.getArgument("x")
'        x.setArgumentValue(a)
'        Dim fa As Double = f.calculate()
'        x.setArgumentValue(b)
'        Dim fb As Double = f.calculate()

'        If Math.Sign(fa) = Math.Sign(fb) Then Return Double.NaN

'        Do
'            Dim c As Double = (a + b) / 2
'            x.setArgumentValue(c)
'            Dim fc As Double = f.calculate()

'            If fc = 0 Then Return c
'            If Math.Sign(fc) = Math.Sign(fa) Then
'                a = c
'            Else
'                b = c
'            End If
'        Loop While Math.Abs(b - a) > tolerancia

'        Return (a + b) / 2
'    End Function

'    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
'        txtFuncionMax.Clear()
'        lstResultados.Items.Clear()
'    End Sub

'    Private Sub frm_MAXMIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        txtFuncionMax.Text = "x^3/3 + x^2 - 8*x"
'        lstResultados.View = View.Details
'        lstResultados.Columns.Add("x", 100)
'        lstResultados.Columns.Add("Max/Min", 150)
'        lstResultados.Columns.Add("Valor", 150)
'    End Sub
'End Class



'Nuevo Codigo con autoajuste de rangos para maximos y minimos_2 (funcional pero con algunos errores,
'es casi el mismo que el 3, pero con algunas diferencias)

'Imports org.mariuszgromada.math.mxparser
'Imports System.Math

'Public Class frm_MAXMIN
'    ' Controladores de eventos (declarados automáticamente por el diseñador)
'    Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click
'        lstResultados.Items.Clear()
'        Dim funcionStr As String = txtFuncionMax.Text.Trim()

'        ' Validar entrada vacía
'        If String.IsNullOrEmpty(funcionStr) Then
'            MessageBox.Show("Ingrese una función válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            Return
'        End If

'        ' Configurar función y derivadas
'        Dim x As New Argument("x")
'        Dim funcion As New Expression(funcionStr, x)

'        ' Verificar sintaxis
'        If Not funcion.checkSyntax() Then
'            MessageBox.Show($"Error en la función: {funcion.getErrorMessage()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            Return
'        End If

'        ' Calcular puntos críticos con rangos ajustados automáticamente
'        Try
'            ' Derivada de la función
'            Dim derivada As New Expression("der(" & funcionStr & ", x)", x)
'            Dim min As Double = -10, max As Double = 10, paso As Double = 0.1
'            Dim maxRango As Double = 1000 ' Límite de expansión del rango
'            Dim puntosCriticos As New List(Of Double)

'            ' Buscar puntos críticos en un rango inicial
'            Dim encontrado As Boolean = False

'            While Not encontrado AndAlso max - min <= maxRango
'                ' Buscar raíces dentro del rango actual
'                EncontrarRaices(derivada, min, max, paso, puntosCriticos)

'                If puntosCriticos.Count > 0 Then
'                    encontrado = True
'                Else
'                    ' Si no se encuentran puntos críticos, ampliamos el rango
'                    min -= 10
'                    max += 10
'                End If
'            End While

'            ' Si no se encontraron puntos críticos
'            If puntosCriticos.Count = 0 Then
'                MessageBox.Show($"No se encontraron puntos críticos dentro de un rango razonable.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
'                Return
'            End If

'            ' Segunda derivada para clasificar los puntos críticos
'            Dim segundaDerivada As New Expression("der(der(" & funcionStr & ", x), x)", x)

'            ' Evaluar y clasificar los puntos críticos encontrados
'            For Each xCritico In puntosCriticos
'                x.setArgumentValue(xCritico)
'                Dim tipo As String = If(segundaDerivada.calculate() > 0, "Mínimo", If(segundaDerivada.calculate() < 0, "Máximo", "Punto de silla"))
'                lstResultados.Items.Add(New ListViewItem({xCritico.ToString("0.000"), tipo, funcion.calculate().ToString("0.000")}))
'            Next

'        Catch ex As Exception
'            MessageBox.Show($"Error crítico: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    End Sub

'    ' Método para encontrar raíces (puntos críticos) de la derivada
'    Private Sub EncontrarRaices(derivada As Expression, min As Double, max As Double, paso As Double, ByRef raices As List(Of Double))
'        Dim x As Argument = derivada.getArgument("x")
'        x.setArgumentValue(min)
'        Dim signoAnterior As Integer = Math.Sign(derivada.calculate())

'        For xi As Double = min + paso To max Step paso
'            x.setArgumentValue(xi)
'            Dim valorActual As Double = derivada.calculate()

'            If Double.IsNaN(valorActual) Then Continue For

'            Dim signoActual As Integer = Math.Sign(valorActual)
'            If signoActual <> signoAnterior Then
'                Dim raiz As Double = RefinarRaiz(derivada, xi - paso, xi, 0.00001)
'                If Not Double.IsNaN(raiz) AndAlso Not raices.Exists(Function(r) Math.Abs(r - raiz) < 0.001) Then
'                    raices.Add(raiz)
'                End If
'            End If
'            signoAnterior = signoActual
'        Next
'    End Sub

'    ' Método para refinar la raíz mediante el método de bisección
'    Private Function RefinarRaiz(f As Expression, a As Double, b As Double, tolerancia As Double) As Double
'        Dim x As Argument = f.getArgument("x")
'        x.setArgumentValue(a)
'        Dim fa As Double = f.calculate()
'        x.setArgumentValue(b)
'        Dim fb As Double = f.calculate()

'        If Math.Sign(fa) = Math.Sign(fb) Then Return Double.NaN

'        Do
'            Dim c As Double = (a + b) / 2
'            x.setArgumentValue(c)
'            Dim fc As Double = f.calculate()

'            If fc = 0 Then Return c
'            If Math.Sign(fc) = Math.Sign(fa) Then
'                a = c
'            Else
'                b = c
'            End If
'        Loop While Math.Abs(b - a) > tolerancia

'        Return (a + b) / 2
'    End Function

'    ' Botón para limpiar la entrada y los resultados
'    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
'        txtFuncionMax.Clear()
'        lstResultados.Items.Clear()
'    End Sub

'    ' Cargar interfaz con ejemplo
'    Private Sub frm_MAXMIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        ' Precargar ejemplo y configurar interfaz
'        txtFuncionMax.Text = "x^3/3 + x^2 - 8*x"
'        lstResultados.View = View.Details
'        lstResultados.Columns.Add("x", 100)
'        lstResultados.Columns.Add("Tipo", 150)
'        lstResultados.Columns.Add("f(x)", 150)
'    End Sub
'End Class


''nueva correccion de codigo mejora con un uso mejor del rango_3
''(pero con la desventaja de que se queda muy limitado en los rangos el programa)

'Imports org.mariuszgromada.math.mxparser
'Imports System.Math

'Public Class frm_MAXMIN
'    ' Controladores de eventos (declarados automáticamente por el diseñador)
'    Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click
'        lstResultados.Items.Clear()
'        Dim funcionStr As String = txtFuncionMax.Text.Trim()

'        ' Validar entrada vacía
'        If String.IsNullOrEmpty(funcionStr) Then
'            MessageBox.Show("Ingrese una función válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            Return
'        End If

'        ' Configurar función y derivadas
'        Dim x As New Argument("x")
'        Dim funcion As New Expression(funcionStr, x)

'        ' Verificar sintaxis
'        If Not funcion.checkSyntax() Then
'            MessageBox.Show($"Error en la función: {funcion.getErrorMessage()}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            Return
'        End If

'        ' Calcular puntos críticos con rangos ajustados automáticamente
'        Try
'            ' Derivada de la función
'            Dim derivada As New Expression("der(" & funcionStr & ", x)", x)
'            Dim min As Double = -10, max As Double = 10, paso As Double = 0.1
'            Dim maxRango As Double = 1000 ' Límite de expansión del rango
'            Dim puntosCriticos As New List(Of Double)

'            ' Buscar puntos críticos en un rango inicial
'            Dim encontrado As Boolean = False

'            While Not encontrado AndAlso max - min <= maxRango
'                ' Buscar raíces dentro del rango actual
'                EncontrarRaices(derivada, min, max, paso, puntosCriticos)

'                If puntosCriticos.Count > 0 Then
'                    encontrado = True
'                Else
'                    ' Si no se encuentran puntos críticos, ampliamos el rango
'                    min -= 10
'                    max += 10
'                End If
'            End While

'            ' Si no se encontraron puntos críticos
'            If puntosCriticos.Count = 0 Then
'                MessageBox.Show($"No se encontraron puntos críticos dentro de un rango razonable.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
'                Return
'            End If

'            ' Segunda derivada para clasificar los puntos críticos
'            Dim segundaDerivada As New Expression("der(der(" & funcionStr & ", x), x)", x)

'            ' Evaluar y clasificar los puntos críticos encontrados
'            For Each xCritico In puntosCriticos
'                x.setArgumentValue(xCritico)
'                Dim tipo As String = If(segundaDerivada.calculate() > 0, "Mínimo", If(segundaDerivada.calculate() < 0, "Máximo", "Punto de silla"))
'                lstResultados.Items.Add(New ListViewItem({xCritico.ToString("0.000"), tipo, funcion.calculate().ToString("0.000")}))
'            Next

'        Catch ex As Exception
'            MessageBox.Show($"Error crítico: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'        End Try
'    End Sub

'    ' Método para encontrar raíces (puntos críticos) de la derivada
'    Private Sub EncontrarRaices(derivada As Expression, min As Double, max As Double, paso As Double, ByRef raices As List(Of Double))
'        Dim x As Argument = derivada.getArgument("x")
'        x.setArgumentValue(min)
'        Dim signoAnterior As Integer = Math.Sign(derivada.calculate())

'        For xi As Double = min + paso To max Step paso
'            x.setArgumentValue(xi)
'            Dim valorActual As Double = derivada.calculate()

'            If Double.IsNaN(valorActual) Then Continue For

'            Dim signoActual As Integer = Math.Sign(valorActual)
'            If signoActual <> signoAnterior Then
'                Dim raiz As Double = RefinarRaiz(derivada, xi - paso, xi, 0.00001)
'                If Not Double.IsNaN(raiz) AndAlso Not raices.Exists(Function(r) Math.Abs(r - raiz) < 0.001) Then
'                    raices.Add(raiz)
'                End If
'            End If
'            signoAnterior = signoActual
'        Next
'    End Sub

'    ' Método para refinar la raíz mediante el método de bisección
'    Private Function RefinarRaiz(f As Expression, a As Double, b As Double, tolerancia As Double) As Double
'        Dim x As Argument = f.getArgument("x")
'        x.setArgumentValue(a)
'        Dim fa As Double = f.calculate()
'        x.setArgumentValue(b)
'        Dim fb As Double = f.calculate()

'        If Math.Sign(fa) = Math.Sign(fb) Then Return Double.NaN

'        Do
'            Dim c As Double = (a + b) / 2
'            x.setArgumentValue(c)
'            Dim fc As Double = f.calculate()

'            If fc = 0 Then Return c
'            If Math.Sign(fc) = Math.Sign(fa) Then
'                a = c
'            Else
'                b = c
'            End If
'        Loop While Math.Abs(b - a) > tolerancia

'        Return (a + b) / 2
'    End Function

'    ' Botón para limpiar la entrada y los resultados
'    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
'        txtFuncionMax.Clear()
'        lstResultados.Items.Clear()
'    End Sub

'    ' Cargar interfaz con ejemplo
'    Private Sub frm_MAXMIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        ' Precargar ejemplo y configurar interfaz
'        txtFuncionMax.Text = "x^3/3 + x^2 - 8*x"
'        lstResultados.View = View.Details
'        lstResultados.Columns.Add("x", 100)
'        lstResultados.Columns.Add("Tipo", 150)
'        lstResultados.Columns.Add("f(x)", 150)
'    End Sub
'End Class



'Mejora de Codigo_4 de max y minimos, este es el MEJOR(eso pense, pero esto esta precargado de rangos :( )

'Imports org.mariuszgromada.math.mxparser
'Imports System.Math
'Imports System.ComponentModel

'Public Class frm_MAXMIN
'    ' Declaramos el BackgroundWorker
'    Private WithEvents backgroundWorker As New BackgroundWorker

'    ' Evento Load del formulario
'    Private Sub frm_MAXMIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        ' Activar ReportProgress para el BackgroundWorker
'        backgroundWorker.WorkerReportsProgress = True

'        ' Precargar ejemplo y configurar la interfaz
'        txtFuncionMax.Text = "x^3/3 + x^2 - 8*x"
'        lstResultados.View = View.Details
'        lstResultados.Columns.Add("x", 100)
'        lstResultados.Columns.Add("Tipo", 150)
'        lstResultados.Columns.Add("f(x)", 150)
'    End Sub

'    ' Evento para iniciar los cálculos cuando el usuario hace clic en "Calcular"
'    Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click
'        ' Limpiar los resultados anteriores
'        lstResultados.Items.Clear()

'        ' Tomar la función ingresada
'        Dim funcionStr As String = txtFuncionMax.Text.Trim()

'        ' Validar entrada vacía
'        If String.IsNullOrEmpty(funcionStr) Then
'            MessageBox.Show("Ingrese una función válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
'            Return
'        End If

'        ' Iniciar el BackgroundWorker
'        If Not backgroundWorker.IsBusy Then
'            backgroundWorker.RunWorkerAsync(funcionStr)
'        End If
'    End Sub

'    ' Evento que se ejecuta cuando BackgroundWorker está trabajando en segundo plano
'    Private Sub backgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles backgroundWorker.DoWork
'        Dim funcionStr As String = CType(e.Argument, String)
'        Dim x As New Argument("x")
'        Dim funcion As New Expression(funcionStr, x)

'        ' Verificar sintaxis
'        If Not funcion.checkSyntax() Then
'            backgroundWorker.ReportProgress(0, $"Error en la función: {funcion.getErrorMessage()}")
'            Return
'        End If

'        ' Calcular derivada
'        Dim derivada As New Expression("der(" & funcionStr & ", x)", x)
'        Dim min As Double = -10, max As Double = 10, paso As Double = 0.1

'        ' Ajustar el rango según ejemplos específicos
'        If funcionStr.Replace(" ", "").ToLower() = "x^3/3+x^2-8*x" Then
'            min = -5 : max = 5
'        ElseIf funcionStr.Replace(" ", "").ToLower() = "(80-2*x)/2*(50-2*x)*x" Then
'            min = 0 : max = 40
'        End If

'        ' Lista para almacenar puntos críticos
'        Dim puntosCriticos As New List(Of Double)

'        ' Llamamos a la función EncontrarRaices para encontrar puntos críticos
'        EncontrarRaices(derivada, min, max, paso, puntosCriticos)

'        ' Reportar si no se encontraron puntos críticos
'        If puntosCriticos.Count = 0 Then
'            backgroundWorker.ReportProgress(0, $"No se encontraron puntos críticos en el rango [{min} a {max}].")
'            Return
'        End If

'        ' Segunda derivada para clasificar los puntos críticos
'        Dim segundaDerivada As New Expression("der(der(" & funcionStr & ", x), x)", x)

'        ' Reportar los resultados al hilo principal
'        For Each xCritico In puntosCriticos
'            x.setArgumentValue(xCritico)
'            Dim tipo As String = If(segundaDerivada.calculate() > 0, "Mínimo", If(segundaDerivada.calculate() < 0, "Máximo", "Punto de silla"))
'            backgroundWorker.ReportProgress(1, New Object() {xCritico.ToString("0.000"), tipo, funcion.calculate().ToString("0.000")})
'        Next
'    End Sub

'    ' Actualizar la interfaz de usuario con los resultados cuando se recibe información del BackgroundWorker
'    Private Sub backgroundWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles backgroundWorker.ProgressChanged
'        If e.ProgressPercentage = 0 Then
'            ' Mostrar mensajes de error o información
'            MessageBox.Show(CType(e.UserState, String), "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
'        ElseIf e.ProgressPercentage = 1 Then
'            ' Convertir el arreglo de objetos a un arreglo de cadenas
'            Dim result As Object() = CType(e.UserState, Object())
'            Dim strResult As String() = Array.ConvertAll(result, Function(o) o.ToString())
'            lstResultados.Items.Add(New ListViewItem(strResult))
'        End If
'    End Sub

'    ' Este evento se ejecuta cuando el trabajo en segundo plano ha terminado
'    Private Sub backgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles backgroundWorker.RunWorkerCompleted
'        ' Se completó el proceso (aquí puedes manejar tareas post-proceso si es necesario)
'    End Sub

'    ' Función para encontrar las raíces de la derivada (puntos críticos)
'    Private Sub EncontrarRaices(derivada As Expression, min As Double, max As Double, paso As Double, ByRef puntosCriticos As List(Of Double))
'        Dim x As Argument = derivada.getArgument("x")
'        x.setArgumentValue(min)
'        Dim signoAnterior As Integer = Math.Sign(derivada.calculate())

'        For xi As Double = min + paso To max Step paso
'            x.setArgumentValue(xi)
'            Dim valorActual As Double = derivada.calculate()

'            If Double.IsNaN(valorActual) Then Continue For

'            Dim signoActual As Integer = Math.Sign(valorActual)
'            If signoActual <> signoAnterior Then
'                Dim raiz As Double = RefinarRaiz(derivada, xi - paso, xi, 0.00001)
'                If Not Double.IsNaN(raiz) AndAlso Not puntosCriticos.Exists(Function(r) Math.Abs(r - raiz) < 0.001) Then
'                    puntosCriticos.Add(raiz)
'                End If
'            End If
'            signoAnterior = signoActual
'        Next
'    End Sub

'    ' Función para refinar una raíz usando el método de bisección
'    Private Function RefinarRaiz(f As Expression, a As Double, b As Double, tolerancia As Double) As Double
'        Dim x As Argument = f.getArgument("x")
'        x.setArgumentValue(a)
'        Dim fa As Double = f.calculate()
'        x.setArgumentValue(b)
'        Dim fb As Double = f.calculate()

'        If Math.Sign(fa) = Math.Sign(fb) Then Return Double.NaN

'        Do
'            Dim c As Double = (a + b) / 2
'            x.setArgumentValue(c)
'            Dim fc As Double = f.calculate()

'            If fc = 0 Then Return c
'            If Math.Sign(fc) = Math.Sign(fa) Then
'                a = c
'            Else
'                b = c
'            End If
'        Loop While Math.Abs(b - a) > tolerancia

'        Return (a + b) / 2
'    End Function

'    ' Evento para limpiar la entrada y los resultados
'    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
'        txtFuncionMax.Clear()
'        lstResultados.Items.Clear()
'    End Sub
'End Class

'Mejora de Codigo_4 de max y minimos, este es el MEJOR, eso creo, en efecto funciona,
'lo unico que hay que hacer es cambiar el rango segun lo amerite la situacion, de momento no :)

Imports org.mariuszgromada.math.mxparser
Imports System.Math
Imports System.ComponentModel

Public Class frm_MAXMIN
    ' Declaramos el BackgroundWorker
    Private WithEvents backgroundWorker As New BackgroundWorker

    ' Evento Load del formulario
    Private Sub frm_MAXMIN_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        backgroundWorker.WorkerReportsProgress = True

        ' Precargar ejemplo y configurar la interfaz
        txtFuncionMax.Text = "x^3/3 + x^2 - 8*x"
        lstResultados.View = View.Details
        lstResultados.Columns.Add("x", 100)
        lstResultados.Columns.Add("Tipo", 150)
        lstResultados.Columns.Add("f(x)", 150)
    End Sub

    ' Evento para iniciar los cálculos al hacer clic en "Calcular"
    Private Sub btnCalcular_Click(sender As Object, e As EventArgs) Handles btnCalcular.Click
        lstResultados.Items.Clear()
        Dim funcionStr As String = txtFuncionMax.Text.Trim()
        If String.IsNullOrEmpty(funcionStr) Then
            MessageBox.Show("Ingrese una función válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        If Not backgroundWorker.IsBusy Then
            backgroundWorker.RunWorkerAsync(funcionStr)
        End If
    End Sub

    ' BackgroundWorker: DoWork (se ejecuta en segundo plano)
    Private Sub backgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles backgroundWorker.DoWork
        Dim funcionStr As String = CType(e.Argument, String)
        Dim x As New Argument("x")
        Dim funcion As New Expression(funcionStr, x)

        ' Verificar sintaxis
        If Not funcion.checkSyntax() Then
            backgroundWorker.ReportProgress(0, $"Error en la función: {funcion.getErrorMessage()}")
            Return
        End If

        ' Calcular la derivada de la función
        Dim derivada As New Expression("der(" & funcionStr & ", x)", x)

        ' Parámetros de rango: se parte de un rango inicial y se expande automáticamente
        Dim initialMin As Double = -10, initialMax As Double = 10, paso As Double = 0.1
        ' Establecemos un límite razonable (para evitar búsquedas infinitas en funciones periódicas)
        Dim maxRango As Double = 100
        Dim currentMin As Double = initialMin, currentMax As Double = initialMax

        ' Lista para almacenar puntos críticos acumulados en todas las expansiones
        Dim puntosCriticos As New List(Of Double)

        ' Bucle de expansión: se recorre el rango hasta que currentMax alcance maxRango
        Do
            Dim currentPoints As New List(Of Double)
            EncontrarRaices(derivada, currentMin, currentMax, paso, currentPoints)
            ' Acumular nuevos puntos (sin duplicados)
            For Each pt In currentPoints
                If Not puntosCriticos.Exists(Function(r) Math.Abs(r - pt) < 0.001) Then
                    puntosCriticos.Add(pt)
                End If
            Next
            currentMin -= 10
            currentMax += 10
        Loop While currentMax < maxRango

        ' Si no se encontraron puntos críticos en el rango final, se informa
        If puntosCriticos.Count = 0 Then
            backgroundWorker.ReportProgress(0, "No se encontraron puntos críticos en un rango razonable.")
            Return
        End If

        ' Calcular la segunda derivada para clasificar los puntos críticos
        Dim segundaDerivada As New Expression("der(der(" & funcionStr & ", x), x)", x)

        ' Reportar cada resultado (punto crítico, tipo y valor de la función)
        For Each xCritico In puntosCriticos
            x.setArgumentValue(xCritico)
            Dim tipo As String = If(segundaDerivada.calculate() > 0, "Mínimo", If(segundaDerivada.calculate() < 0, "Máximo", "Punto de silla"))
            backgroundWorker.ReportProgress(1, New Object() {xCritico.ToString("0.000"), tipo, funcion.calculate().ToString("0.000")})
        Next
    End Sub

    ' BackgroundWorker: ProgressChanged (actualiza la UI)
    Private Sub backgroundWorker_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles backgroundWorker.ProgressChanged
        If e.ProgressPercentage = 0 Then
            MessageBox.Show(CType(e.UserState, String), "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ElseIf e.ProgressPercentage = 1 Then
            Dim result As Object() = CType(e.UserState, Object())
            Dim strResult As String() = Array.ConvertAll(result, Function(o) o.ToString())
            lstResultados.Items.Add(New ListViewItem(strResult))
        End If
    End Sub

    ' BackgroundWorker: RunWorkerCompleted (opcional para tareas post-proceso)
    Private Sub backgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles backgroundWorker.RunWorkerCompleted
        ' Aquí se pueden realizar acciones después de que finalice el cálculo.
    End Sub

    ' Función para encontrar raíces (puntos críticos) en un intervalo dado
    Private Sub EncontrarRaices(derivada As Expression, min As Double, max As Double, paso As Double, ByRef puntosCriticos As List(Of Double))
        Dim x As Argument = derivada.getArgument("x")
        Dim tolerance As Double = 0.001
        Dim firstIteration As Boolean = True
        Dim prevSign As Integer = 0

        For xi As Double = min To max Step paso
            x.setArgumentValue(xi)
            Dim currentVal As Double = derivada.calculate()
            Dim currentAbs As Double = Abs(currentVal)
            Dim currentSign As Integer = Math.Sign(currentVal)

            ' Si el valor absoluto es muy pequeño, se considera un candidato a raíz
            If currentAbs < tolerance Then
                If Not puntosCriticos.Exists(Function(r) Math.Abs(r - xi) < paso) Then
                    puntosCriticos.Add(xi)
                End If
            ElseIf Not firstIteration AndAlso currentSign <> prevSign Then
                ' Cambio de signo detectado: refinar la raíz en el intervalo [xi-paso, xi]
                Dim root As Double = RefinarRaiz(derivada, xi - paso, xi, 0.00001)
                If Not Double.IsNaN(root) AndAlso Not puntosCriticos.Exists(Function(r) Math.Abs(r - root) < tolerance) Then
                    puntosCriticos.Add(root)
                End If
            End If

            prevSign = currentSign
            firstIteration = False
        Next
    End Sub

    ' Función para refinar una raíz usando el método de bisección
    Private Function RefinarRaiz(f As Expression, a As Double, b As Double, tolerancia As Double) As Double
        Dim x As Argument = f.getArgument("x")
        x.setArgumentValue(a)
        Dim fa As Double = f.calculate()
        x.setArgumentValue(b)
        Dim fb As Double = f.calculate()

        If Math.Sign(fa) = Math.Sign(fb) Then Return Double.NaN

        Do
            Dim c As Double = (a + b) / 2
            x.setArgumentValue(c)
            Dim fc As Double = f.calculate()
            If Abs(fc) < tolerancia Then Return c
            If Math.Sign(fc) = Math.Sign(fa) Then
                a = c
            Else
                b = c
            End If
        Loop While Math.Abs(b - a) > tolerancia

        Return (a + b) / 2
    End Function

    ' Botón para limpiar la entrada y los resultados
    Private Sub btnLimpiar_Click(sender As Object, e As EventArgs) Handles btnLimpiar.Click
        txtFuncionMax.Clear()
        lstResultados.Items.Clear()
    End Sub
End Class

