Imports System.Data.SqlClient

Public Class Baja_usuario
    Inherits System.Web.UI.Page

    Private oDataTable As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CargarDatos()

    End Sub

    Private Sub Baja_usuario_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete

    End Sub

#Region "Funciones"

    Private Sub CargarDatos()

        'Realizamos la conexión a la BBDD:
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oSqlDataReader As SqlDataReader

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        myCmd = New SqlCommand("SELECT u.id, u.nombre AS 'usuario', u.nombre_real AS 'nombre y apellidos', u.email, r.nombre AS 'rol' FROM usuarios u, roles r WHERE u.id_rol=r.id", oConexion)

        oSqlDataReader = myCmd.ExecuteReader
        oDataTable.Load(oSqlDataReader)


        grdviewUsuarios.DataSource = oDataTable
        grdviewUsuarios.DataBind()

        oConexion.Close()

    End Sub

    Private Sub grdviewUsuarios_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdviewUsuarios.RowDataBound
        'Ocultamos la primera columna del Grid:
        e.Row.Cells(1).Visible = False
    End Sub

    Private Sub grdviewUsuarios_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdviewUsuarios.RowDeleting
        Dim idUsuario As String
        Dim nombreUsuario As String

        idUsuario = grdviewUsuarios.Rows(e.RowIndex).Cells(1).Text
        nombreUsuario = grdviewUsuarios.Rows(e.RowIndex).Cells(2).Text

        If MsgBox("¿Estás seguro que desea eliminar al usuario " & nombreUsuario, MsgBoxStyle.OkCancel, "Eliminación de usuario") = MsgBoxResult.Ok Then
            'Se lanza la eliminación del usuario en la tabla de "privilegios_usuarios":
            Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
            Dim oConexion As New SqlConnection
            Dim myCmd As SqlCommand

            oConexion = New SqlConnection(cadenaConexion)

            oConexion.Open()

            myCmd = New SqlCommand("DELETE FROM privilegios_usuarios WHERE id_usuario=" & idUsuario, oConexion)
            myCmd.ExecuteNonQuery()
            myCmd = Nothing

            myCmd = New SqlCommand("DELETE FROM usuarios WHERE id=" & idUsuario, oConexion)
            myCmd.ExecuteNonQuery()
            myCmd = Nothing

            oConexion.Close()

            Response.Redirect("Baja_usuario.aspx") '-->Recargamos la página


        Else
            e.Cancel = True
        End If

    End Sub


#End Region


End Class