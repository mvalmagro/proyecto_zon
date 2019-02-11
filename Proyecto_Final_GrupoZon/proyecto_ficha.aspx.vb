Imports System.Data.SqlClient

Public Class proyecto_ficha
    Inherits System.Web.UI.Page
    Dim idTitulo As String
    Dim titulo As String
    Dim comercial As String
    Dim fecha_pedido As Date
    Dim fecha_evento As Date
    Dim idCliente As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        idTitulo = Request.QueryString("Valor")
        pintarDatos()
        cargarClientes()

    End Sub

    Private Sub pintarDatos()
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oDataAdapter As SqlDataAdapter
        Dim oDataSet As New DataSet
        Dim i As Integer

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        myCmd = New SqlCommand("SELECT titulo, comercial, fecha_pedido, fecha_evento from proyectos where id=" & idTitulo, oConexion)
        oDataAdapter = New SqlDataAdapter(myCmd)
        oDataAdapter.Fill(oDataSet, "proyectos")
        oDataAdapter = Nothing
        myCmd = Nothing

        oConexion.Close()

        titulo = oDataSet.Tables("proyectos").Rows(0)("titulo")
        comercial = oDataSet.Tables("proyectos").Rows(0)("comercial")
        fecha_pedido = oDataSet.Tables("proyectos").Rows(0)("fecha_pedido")
        fecha_evento = oDataSet.Tables("proyectos").Rows(0)("fecha_evento")

        txtTitulo.Text = titulo
        txtComercial.Text = comercial
        txtFecha_pedido.Text = fecha_pedido.ToString("yyyy-MM-dd")
        txtFecha_evento.Text = fecha_evento.ToString("yyyy-MM-dd HH:mm").Replace(" ", "T")




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
        Dim sw As Boolean

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

        i = 0
        sw = True
        While i < lstLista.Items.Count And sw = True

            If lstLista.Items(i).Value = idTitulo Then
                lstLista.SelectedIndex = i
                sw = False
            Else
                i = i + 1
            End If

        End While
    End Sub

End Class