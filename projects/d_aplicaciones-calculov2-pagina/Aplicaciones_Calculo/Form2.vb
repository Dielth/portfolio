Public Class frmOpciones_Inicio


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnWay5_Click(sender As Object, e As EventArgs) Handles btnWay5.Click
        Dim frmMaxMin As New frm_MAXMIN
        frm_MAXMIN.Show()
    End Sub

    Private Sub btnWay2_Click(sender As Object, e As EventArgs) Handles btnWay2.Click
        ' Opción 1: Si el formulario está en el namespace "DerivadaProducto"
        Dim frmDerivadaProducto As New DerivadaProducto.frmDerPr3()
        frmDerivadaProducto.Show() ' Muestra el formulario
    End Sub

    Private Sub btnWay3_Click(sender As Object, e As EventArgs) Handles btnWay3.Click
        Dim frmCadena As New frmDer_Cadena
        frmDer_Cadena.Show()
    End Sub

    Private Sub btnWay4_Click(sender As Object, e As EventArgs) Handles btnWay4.Click
        Dim frmTrigonometrica As New frm_derTri
        frm_derTri.Show()
    End Sub

    Private Sub btnWay1_Click(sender As Object, e As EventArgs) Handles btnWay1.Click
        Dim frm4Pasos As New frmDer_Pasos
        frmDer_Pasos.Show()
    End Sub
End Class