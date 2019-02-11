Imports System.Data.SqlClient

Public Class proyecto_listado
    Inherits System.Web.UI.Page

    Private oDataTable As New DataTable

    Private Sub gridProyecto_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gridProyecto.RowDataBound
        e.Row.Cells(1).Visible = False
        e.Row.Cells(5).Visible = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        llenarGrid()
    End Sub

    Private Sub llenarGrid()
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oSqlDataReader As SqlDataReader

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        myCmd = New SqlCommand("SELECT p.*, c.nombre_comercial from proyectos p, clientes c where p.id_cliente=c.id", oConexion)

        oSqlDataReader = myCmd.ExecuteReader
        oDataTable.Load(oSqlDataReader)


        gridProyecto.DataSource = oDataTable
        gridProyecto.DataBind()

        oConexion.Close()
    End Sub

    Private Sub btnAlta_Click(sender As Object, e As EventArgs) Handles btnAlta.Click
        Response.Write("<script>window.open('proyecto_alta.aspx','popup','width=550,height=600') </script>")
    End Sub

    Private Sub gridProyecto_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gridProyecto.RowDeleting
        Dim nombreProyecto As String

        nombreProyecto = gridProyecto.Rows(e.RowIndex).Cells(2).Text
        If MsgBox("¿Estás seguro que desea eliminar al usuario " & nombreProyecto & "?", MsgBoxStyle.OkCancel, "Eliminación de usuario") = MsgBoxResult.Ok Then

            Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
            Dim oConexion As New SqlConnection
            Dim myCmd As SqlCommand

            oConexion = New SqlConnection(cadenaConexion)

            oConexion.Open()

            myCmd = New SqlCommand("DELETE FROM proyectos WHERE titulo=" & nombreProyecto, oConexion)
            myCmd.ExecuteNonQuery()
            myCmd = Nothing

            oConexion.Close()

            Response.Redirect("proyecto_listado.aspx") '-->Recargamos la página

        End If
    End Sub
End Class