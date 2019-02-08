Imports System.Data.SqlClient

Public Class proyecto_alta
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cargarClientes()
    End Sub

    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        Dim titulo As String
        Dim comercial As String
        Dim fecha_pedido As Date
        Dim fecha_evento As Date
        Dim cliente As String

        titulo = txtTitulo.Text
        comercial = txtComercial.Text
        fecha_pedido = txtFecha_pedido.Text
        fecha_evento = txtFecha_evento.Text
        cliente = lstLista.SelectedValue

        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        'Dim oDataAdapter As SqlDataAdapter
        Dim oDataSet As New DataSet
        Dim query As String

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open() '-->abrimos la conexión
        Try
            query = "INSERT INTO proyectos values('" & titulo & "', '" & comercial & "', '" & fecha_pedido & "', '" & fecha_evento & "', '" & cliente & "')"
            myCmd = New SqlCommand(query, oConexion)

            myCmd.ExecuteNonQuery()
        Catch ex As SqlException

            If ex.Number = 2627 Then
                MsgBox("Nombre de proyecto duplicado")
            End If
        End Try

        oConexion.Close()
    End Sub


    Private Sub cargarClientes()
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oDataAdapter As SqlDataAdapter
        Dim oDataSet As DataSet
        Dim i As Integer
        Dim oDataRow As DataRow
        Dim lstItem As ListItem

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open() '-->abrimos la conexión

        myCmd = New SqlCommand("SELECT id, nombre_comercial FROM clientes", oConexion)

        oDataAdapter = New SqlDataAdapter(myCmd)

        oDataSet = New DataSet
        oDataAdapter.Fill(oDataSet, "clientes")

        oDataAdapter = Nothing

        oConexion.Close()

        i = 0

        While i < oDataSet.Tables("clientes").Rows.Count
            oDataRow = oDataSet.Tables("clientes").Rows(i)
            lstItem = New ListItem(oDataRow("nombre_comercial"), oDataRow("id"))

            lstLista.Items.Insert(i, lstItem)

            lstItem = Nothing
            oDataRow = Nothing
            i = i + 1
        End While

        oDataSet = Nothing

    End Sub
End Class