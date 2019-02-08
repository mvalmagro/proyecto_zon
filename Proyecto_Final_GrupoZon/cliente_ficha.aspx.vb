Imports System.Data.SqlClient

Public Class cliente_ficha
    Inherits System.Web.UI.Page

    Private idCliente As String
    Private nombreComercial As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        idCliente = Request.QueryString("Valor")
        CargarDatos()

    End Sub

#Region "Funciones"

    Private Sub CargarDatos()

        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oDataAdapter As SqlDataAdapter
        Dim oDataSet As New DataSet

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        myCmd = New SqlCommand("SELECT * FROM clientes WHERE id=" & idCliente, oConexion)
        oDataAdapter = New SqlDataAdapter(myCmd)
        oDataAdapter.Fill(oDataSet, "clientes")
        oDataAdapter = Nothing
        myCmd = Nothing

        txtNombreComercial.Text = oDataSet.Tables("clientes").Rows(0)("nombre_comercial")



        oConexion.Close()
    End Sub

#End Region

End Class