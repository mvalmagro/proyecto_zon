Imports System.Data.SqlClient

Public Class proyecto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        'Dim oDataAdapter As SqlDataAdapter
        Dim oDataSet As New DataSet
        Dim query As String

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open() '-->abrimos la conexión

        query = "INSERT INTO [proyectos] (id,titulo,comercial,fecha_pedido,fecha_evento,id_cliente)values(@id,@titulo,@comercial,@fecha_pedido,@fecha_evento,@id_cliente)"
        myCmd = New SqlCommand(query, oConexion)

        Dim titulo As String = txtTitulo.Text
        myCmd.Parameters.AddWithValue(titulo, titulo)
        myCmd.Parameters.AddWithValue(txtComercial, comercial)
        myCmd.Parameters.AddWithValue(txtFecha_pedido, fecha_pedido)
        myCmd.Parameters.AddWithValue(txtFecha_evento, fecha_evento)

        oConexion.Close()
    End Sub
End Class