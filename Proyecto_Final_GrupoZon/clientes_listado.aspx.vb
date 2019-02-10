Imports System.Data.SqlClient

Public Class clientes_listado
    Inherits System.Web.UI.Page

    Private oDataTable As New DataTable

    Private Sub clientes_listado_Init(sender As Object, e As EventArgs) Handles Me.Init
        CargarDatos()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("acceso") = True Then
            'Permito el acceso
        Else
            'Si no tienes acceso a la aplicación, te reedirijo a la página del login:
            Response.Redirect("Default.aspx")
        End If
    End Sub



    Private Sub grdviewClientes_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdviewClientes.RowDataBound
        'Ocultamos la primera columna del Grid (ID):
        e.Row.Cells(1).Visible = False

        'Ocultamos la última columna del Grid (path_logo)
        e.Row.Cells(9).Visible = False

    End Sub
    Private Sub grdviewClientes_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles grdviewClientes.RowEditing
        Dim idClienteEditar As String

        'Capturamos el ID del usuario que queremos editar y del cual vamos a mostrar los datos en su "ficha de usuario":
        idClienteEditar = grdviewClientes.Rows(e.NewEditIndex).Cells(1).Text

        'Abrimos la URL correspondiente pasándole un parámetro:
        Response.Redirect("cliente_ficha.aspx?Valor=" & idClienteEditar)
    End Sub

    Private Sub grdviewClientes_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdviewClientes.RowDeleting
        Dim idCliente As String
        'Dim nombreCliente As String

        idCliente = grdviewClientes.Rows(e.RowIndex).Cells(1).Text
        'nombreCliente = grdviewClientes.Rows(e.RowIndex).Cells(2).Text

        If MsgBox("¿Estás seguro que desea eliminar al cliente este registro?", MsgBoxStyle.OkCancel, "Eliminación de cliente") = MsgBoxResult.Ok Then
            'Se lanza la eliminación del usuario en la tabla de "privilegios_usuarios":
            Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
            Dim oConexion As New SqlConnection
            Dim myCmd As SqlCommand
            Dim query As String
            Dim numClientes As Integer = 0

            oConexion = New SqlConnection(cadenaConexion)

            oConexion.Open()

            'Lo primero que debemos comprobar es si el cliente tiene asignado algún proyecto:
            query = "SELECT COUNT(*) FROM proyectos WHERE id_cliente=" & idCliente
            myCmd = New SqlCommand(query, oConexion)
            numClientes = myCmd.ExecuteScalar()
            myCmd = Nothing

            If numClientes = 0 Then
                myCmd = New SqlCommand("DELETE FROM clientes WHERE id=" & idCliente, oConexion)
                myCmd.ExecuteNonQuery()
                myCmd = Nothing

                Response.Redirect("clientes_listado.aspx") '-->Recargamos la página
            Else
                MsgBox("No es posible eliminar el cliente especificado puesto que tiene proyectos asociados.")
            End If


            oConexion.Close()

        Else
            e.Cancel = True
        End If

    End Sub



    Private Sub btnAlta_Click(sender As Object, e As EventArgs) Handles btnAlta.Click
        'Response.Write("<script>window.open('cliente_alta.aspx','popup','width=550,height=600') </script>")
        Response.Redirect("cliente_alta.aspx")
    End Sub




#Region "Funciones"
    Public Sub CargarDatos()
        'Realizamos la conexión a la BBDD:
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oSqlDataReader As SqlDataReader

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        myCmd = New SqlCommand("SELECT id, nombre_comercial AS 'Nombre comercial', nombre_fiscal AS 'Nombre fiscal', cif AS 'CIF', persona_contacto AS 'Persona de contacto', email AS 'Correo electrónico', telefono AS 'Teléfono', sector AS 'Sector', path_logo FROM clientes", oConexion)

        oSqlDataReader = myCmd.ExecuteReader
        oDataTable.Load(oSqlDataReader)

        grdviewClientes.DataSource = oDataTable

        grdviewClientes.DataBind()

        oConexion.Close()


        'Pintamos la imagen del logo dentro de la celda del nombre comercial del cliente:
        Dim i As Integer = 0
        Dim path As String
        While i < grdviewClientes.Rows.Count
            path = grdviewClientes.Rows.Item(i).Cells.Item(9).Text

            ' "&nbsp;"-->significa que la celda en la que se pinta la ruta(path) del logo, está vacia.
            If path.Equals("&nbsp;") <> True Then
                grdviewClientes.Rows.Item(i).Cells.Item(2).Text = "<img src=" & path & " border=""1"" width=""40"" height=""40"">" & grdviewClientes.Rows.Item(i).Cells.Item(2).Text
            End If
            i = i + 1

        End While

    End Sub




#End Region
End Class