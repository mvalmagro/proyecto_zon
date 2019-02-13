Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class usuario_listado
    Inherits System.Web.UI.Page

    Private query As String
    Private nFiltros As Integer = 0
    Private filtrosDetalle As String


    Private oDataTable As New DataTable

    Private Sub usuario_listado_Init(sender As Object, e As EventArgs) Handles Me.Init

        query = Request.QueryString("parametro")
        nFiltros = Request.QueryString("numfiltros")
        filtrosDetalle = Request.QueryString("filtrosDetalle")

        If query Is Nothing Then
            CargarDatos()
            RellenarDropDownCriterios()
        Else
            CargarDatos(query)
            RellenarDropDownCriterios()

            If nFiltros = 1 Then
                lblFiltros.Text = "Hay " & nFiltros & " filtro aplicado."
            Else
                lblFiltros.Text = "Hay " & nFiltros & " filtros aplicados."
            End If

            btnEliminarFiltros.Visible = True
        End If

        'Pintamos la query de los FILTROS en una label oculta:
        lblQuery.Text = query

        'Pintamos los filtros desglosados en una label oculta:
        lblFiltrosDetalle.Text = filtrosDetalle

        PintarFiltrosDetalle()


    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ControlDeAcceso()
    End Sub

    Private Sub btnEliminarFiltros_Click(sender As Object, e As EventArgs) Handles btnEliminarFiltros.Click
        Response.Redirect("usuario_listado.aspx")
        btnEliminarFiltros.Visible = False
        lblFiltros.Text = "No hay filtros establecidos."
    End Sub

    Private Sub btnFiltrar_Click(sender As Object, e As EventArgs) Handles btnFiltrar.Click
        Dim l_criterio As String
        Dim l_filtro As String
        Dim nuevoFiltro As String
        Dim operadorLogico As String
        Dim filtDetalle As String

        l_criterio = dropDownCriterios.SelectedValue
        l_filtro = txtFiltro.Text


        If l_criterio.Equals("nombre_rol") = True Then
            l_criterio = "r.nombre"
        Else
            l_criterio = "u." & l_criterio
        End If


        'Vamos a hacer la comprobación para saber si hay que insertar un "and" o un "or":
        If query Is Nothing Then

            operadorLogico = "and"
        Else
            If query.Contains(l_criterio & "=") = True Then
                operadorLogico = "or"
            Else
                operadorLogico = "and"
            End If
        End If
        '=================================================================================


        nuevoFiltro = "(" & l_criterio & "=" & "'" & l_filtro & "')"

        filtDetalle = lblFiltrosDetalle.Text & nuevoFiltro & ";"

        'Montamos la query:
        If query Is Nothing Then
            query = "SELECT u.id, u.nombre AS 'Usuario', u.nombre_real AS 'Nombre', u.email AS 'Correo electronico', r.nombre AS 'Rol', ultimo_logon FROM usuarios u, roles r WHERE (u.id_rol=r.id)" & " " & operadorLogico & " " & "(" & nuevoFiltro & ")"
        Else
            query = query.Substring(0, query.Length - 1)

            query = query & " " & operadorLogico & " " & nuevoFiltro & ")"
        End If

        nFiltros = nFiltros + 1

        Response.Redirect("usuario_listado.aspx?parametro=" & query & "&numfiltros=" & nFiltros & "&filtrosDetalle=" & filtDetalle)

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
        Response.Redirect("usuario_ficha.aspx?Valor=" & idUsuarioEditar)

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

                Response.Redirect("usuario_listado.aspx") '-->Recargamos la página

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

    Private Sub btnAlta_Click(sender As Object, e As EventArgs) Handles btnAlta.Click
        Response.Redirect("usuario_alta.aspx")
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

    Private Sub CargarDatos(Optional querySelect As String = "Select u.id, u.nombre As 'Usuario', u.nombre_real AS 'Nombre', u.email AS 'Correo electronico', r.nombre AS 'Rol', ultimo_logon FROM usuarios u, roles r WHERE u.id_rol=r.id")

        'Realizamos la conexión a la BBDD:
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oSqlDataReader As SqlDataReader


        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        myCmd = New SqlCommand(querySelect, oConexion)

        oSqlDataReader = myCmd.ExecuteReader
        oDataTable.Load(oSqlDataReader)


        grdviewUsuarios.DataSource = oDataTable
        grdviewUsuarios.DataBind()

        oConexion.Close()

    End Sub


    Public Sub RellenarDropDownCriterios()
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim oDataAdapter As SqlDataAdapter
        Dim oDataSet As New DataSet
        Dim i As Integer

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        oDataAdapter = New SqlDataAdapter("SELECT TOP 1 usuarios.nombre AS Nombre, nombre_real AS 'Nombre y apellidos', email AS 'Correo eletronico', roles.nombre AS 'Rol' FROM usuarios, roles WHERE usuarios.id_rol=roles.id", oConexion)
        oDataAdapter.Fill(oDataSet, "usuarios")


        For Each oDataColum As DataColumn In oDataSet.Tables("usuarios").Columns
            dropDownCriterios.Items.Add(New ListItem(oDataColum.ColumnName))
        Next

        i = 0
        While i < dropDownCriterios.Items.Count
            Select Case dropDownCriterios.Items(i).Text
                Case "Nombre"
                    dropDownCriterios.Items(i).Text = "Usuario"
                    dropDownCriterios.Items(i).Value = "nombre"
                Case "Nombre y apellidos"
                    dropDownCriterios.Items(i).Text = "Nombre y apellidos"
                    dropDownCriterios.Items(i).Value = "nombre_real"
                Case "Correo eletronico"
                    dropDownCriterios.Items(i).Text = "Correo eletronico"
                    dropDownCriterios.Items(i).Value = "email"
                Case "Rol"
                    dropDownCriterios.Items(i).Text = "Rol"
                    dropDownCriterios.Items(i).Value = "nombre_rol"
            End Select

            i = i + 1
        End While

    End Sub

    Private Sub ControlDeAcceso()
        If Session("acceso") = True Then
            'Permito el acceso

            If Session("gestion_usuarios") = True Then
                'Permito el acceso
            Else
                'Si no tienes privilegios para la "Gestión de Usuarios", no te permito el acceso:

                MsgBox("El usuario " & Session("usuario") & " no dispone de permisos para acceder a este apartado.", MsgBoxStyle.Information, "Acceso denegado.")

                Response.Redirect("calendario.aspx")
            End If

        Else
            'Si no tienes acceso a la aplicación, te reedirijo a la página del login:
            Response.Redirect("Default.aspx")
        End If
    End Sub


    Private Sub PintarFiltrosDetalle()
        Dim cadena As String
        Dim aux As String
        Dim aux2() As String
        Dim aux3() As String
        Dim i As Integer = 0

        Dim criterio As String
        Dim valor As String
        Dim filtroTratado As String

        cadena = lblFiltros.Text.Substring(0, lblFiltros.Text.Count - 1)
        aux = filtrosDetalle

        If aux <> Nothing Then

            aux2 = aux.Split(";")

            While i < aux2.Count - 1

                aux3 = aux2(i).Split("=")

                criterio = aux3(aux3.Count - 2)
                valor = aux3(aux3.Count - 1)

                criterio = criterio.Substring(1, criterio.Length - 1)
                valor = valor.Substring(1, valor.Length - 3)

                'Tratamos el criterio
                Select Case criterio
                    Case "u.nombre"
                        criterio = "Usuario"
                    Case "u.nombre_real"
                        criterio = "Nombre y apellidos"
                    Case "u.email"
                        criterio = "Correo electronico"
                    Case "r.nombre"
                        criterio = "Rol"
                End Select

                filtroTratado = criterio & ": " & valor


                If nFiltros = 1 Or i = 0 Then
                    cadena = cadena & "(" & filtroTratado & ")"
                Else
                    cadena = cadena.Substring(0, cadena.Count - 1) & " , " & filtroTratado & ")"
                End If


                filtroTratado = Nothing
                i = i + 1
            End While

            lblFiltros.Text = cadena

        End If

    End Sub



#End Region

End Class