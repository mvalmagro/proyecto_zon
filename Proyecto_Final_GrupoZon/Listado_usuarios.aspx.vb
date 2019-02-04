Imports System.Data.SqlClient

Public Class Listado_usuarios
    Inherits System.Web.UI.Page

    Private oDataTable As New DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CargarDatos()

    End Sub

    Private Sub grdviewUsuarios_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdviewUsuarios.RowDataBound
        'Ocultamos la primera columna del Grid (ID):
        e.Row.Cells(1).Visible = False

        'Ocultamos la última columna del Grid (Fecha de ultimo logon):
        e.Row.Cells(6).Visible = False
    End Sub

    Private Sub grdviewUsuarios_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles grdviewUsuarios.RowEditing
        Dim idUsuarioEditar As String

        'Capturamos el ID del usuario que queremos editar y del cual vamos a mostrar los datos en su "ficha de usuario":
        idUsuarioEditar = grdviewUsuarios.Rows(e.NewEditIndex).Cells(1).Text

        'Abrimos la URL correspondiente pasándole un parámetro:
        Response.Redirect("ficha_usuario.aspx?Valor=" & idUsuarioEditar)

    End Sub



    Private Sub grdviewUsuarios_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdviewUsuarios.RowDeleting
        Dim idUsuario As String
        Dim nombreUsuario As String
        Dim fechaLastLogon As Date

        idUsuario = grdviewUsuarios.Rows(e.RowIndex).Cells(1).Text
        nombreUsuario = grdviewUsuarios.Rows(e.RowIndex).Cells(2).Text
        fechaLastLogon = grdviewUsuarios.Rows(e.RowIndex).Cells(6).Text

        'If Session("usuario").Equals(nombreUsuario) = False Then

        If ComprobarUltimoLogonUsuario(fechaLastLogon) = True Then

            If MsgBox("¿Estás seguro que desea eliminar al usuario " & nombreUsuario & "?", MsgBoxStyle.OkCancel, "Eliminación de usuario") = MsgBoxResult.Ok Then
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

                Response.Redirect("Listado_usuarios.aspx") '-->Recargamos la página

            Else
                e.Cancel = True
            End If

        Else
            MsgBox("No se puede eliminar al usuario " & nombreUsuario & " porque inició sesión hace menos de dos días.", MsgBoxStyle.Exclamation, "Eliminación de usuario")
            e.Cancel = True

        End If

        'Else
        'MsgBox("No se puede eliminar al usuario " & nombreUsuario & " porque se trata del usuario de la sesión actual.", MsgBoxStyle.Exclamation, "Eliminación de usuario")
        'e.Cancel = True
        ' End If

    End Sub

#Region "Funciones"

    Private Function ComprobarUltimoLogonUsuario(l_fechaLastLogon As Date) As Boolean
        Dim fechaActual As Date
        Dim i As Integer

        fechaActual = Today

        'Sumamos dos días a la fecha de  ultimo login del usuario  y la comparamos con la fecha actual:
        If l_fechaLastLogon.AddDays(2).CompareTo(fechaActual) <= 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub CargarDatos()

        'Realizamos la conexión a la BBDD:
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oSqlDataReader As SqlDataReader

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        myCmd = New SqlCommand("SELECT u.id, u.nombre AS 'Usuario', u.nombre_real AS 'Nombre', u.email AS 'Correo electronico', r.nombre AS 'Rol', ultimo_logon FROM usuarios u, roles r WHERE u.id_rol=r.id", oConexion)

        oSqlDataReader = myCmd.ExecuteReader
        oDataTable.Load(oSqlDataReader)


        grdviewUsuarios.DataSource = oDataTable
        grdviewUsuarios.DataBind()

        oConexion.Close()

    End Sub

#End Region

End Class