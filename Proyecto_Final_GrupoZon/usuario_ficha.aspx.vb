Imports System.Data.SqlClient

Public Class usuario_ficha
    Inherits System.Web.UI.Page

    Private idusuario As String
    Private usuario As String
    Private password_actual As String
    Private nombre As String
    Private email As String
    Private id_rol As String
    Private ultimoLogon As Date
    Private id_privilegios() As String
    Private nombre_rol As String
    Private nombre_privilegios() As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        idusuario = Request.QueryString("Valor")
        CargarDatos()

        '===DEBUG=======================================
        'MsgBox("id usuario:" & idusuario)
        'MsgBox("usuario:" & usuario)
        'MsgBox("password_actual:" & password_actual)
        'MsgBox("nombre:" & nombre)
        'MsgBox("email:" & email)
        'MsgBox("id_rol:" & id_rol)
        'MsgBox("ultimoLogon" & ultimoLogon)
        'MsgBox("nombre_rol:" & nombre_rol)

        'Dim i As Integer = 0

        'While i < id_privilegios.Length
        '    MsgBox("Privilegios:" & id_privilegios(i) & " - " & nombre_privilegios(i))

        '    i = i + 1
        'End While
        ' ===fin=======================================

        PintarDatos()

        lstPrivilegiosActuales.ForeColor = Drawing.Color.Green
        lstPrivilegiosRestantes.ForeColor = Drawing.Color.Red

    End Sub

#Region "Funciones principales"

    Private Sub CargarDatos()

        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oDataAdapter As SqlDataAdapter
        Dim oDataSet As New DataSet
        Dim i As Integer

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        myCmd = New SqlCommand("SELECT u.*, r.nombre AS nombrerol FROM usuarios u, roles r WHERE r.id=u.id_rol and u.id=" & idusuario, oConexion)
        oDataAdapter = New SqlDataAdapter(myCmd)
        oDataAdapter.Fill(oDataSet, "usuarios")
        oDataAdapter = Nothing
        myCmd = Nothing

        myCmd = New SqlCommand("SELECT pu.id_privilegio, p.nombre FROM privilegios_usuarios pu, privilegios p WHERE p.id=pu.id_privilegio and id_usuario=" & idusuario, oConexion)
        oDataAdapter = New SqlDataAdapter(myCmd)
        oDataAdapter.Fill(oDataSet, "privilegios_usuarios")
        oDataAdapter = Nothing
        myCmd = Nothing

        oConexion.Close()


        usuario = oDataSet.Tables("usuarios").Rows(0)("nombre")
        password_actual = oDataSet.Tables("usuarios").Rows(0)("password")
        nombre = oDataSet.Tables("usuarios").Rows(0)("nombre_real")
        email = oDataSet.Tables("usuarios").Rows(0)("email").ToString
        id_rol = oDataSet.Tables("usuarios").Rows(0)("id_rol")
        ultimoLogon = oDataSet.Tables("usuarios").Rows(0)("ultimo_logon")
        nombre_rol = oDataSet.Tables("usuarios").Rows(0)("nombrerol")

        ReDim id_privilegios((oDataSet.Tables("privilegios_usuarios").Rows.Count) - 1)
        ReDim nombre_privilegios(id_privilegios.Count - 1)
        i = 0
        While i < id_privilegios.Count
            id_privilegios(i) = oDataSet.Tables("privilegios_usuarios").Rows(i)("id_privilegio")
            nombre_privilegios(i) = oDataSet.Tables("privilegios_usuarios").Rows(i)("nombre")
            i = i + 1
        End While

    End Sub

    Private Sub PintarDatos()
        Dim i As Integer
        Dim sw As Boolean
        Dim oListItem As ListItem

        txtUsuario.Text = usuario
        txtNombreyApellidos.Text = nombre
        txtEmail.Text = email


        '====Cargamos los roles en el control DropDown"====
        CargarRoles()
        i = 0
        sw = True
        While i < dropdownRol.Items.Count And sw = True

            If dropdownRol.Items(i).Value = id_rol Then
                dropdownRol.SelectedIndex = i
                sw = False
            Else
                i = i + 1
            End If

        End While
        '====================================================


        '=====Cargamos los Privilegios que tiene actualmente el usuario en el ListBox correspondiente:
        i = 0
        While i < id_privilegios.Count
            oListItem = New ListItem(nombre_privilegios(i), id_privilegios(i))

            lstPrivilegiosActuales.Items.Add(oListItem)

            oListItem = Nothing
            i = i + 1
        End While
        '==============================================================================================


        '==Cargamos los Privilegios restantes, es decir, de los que el usuario correspondiente NO dispone:
        PintarPrivilegiosRestantes()
        '==================================================================================================

    End Sub

#End Region

#Region "Sub Funciones"

    Private Sub CargarRoles()
        'Realizamos la conexión a la BBDD:

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

        myCmd = New SqlCommand("SELECT id, nombre FROM roles", oConexion)

        oDataAdapter = New SqlDataAdapter(myCmd)

        oDataSet = New DataSet
        oDataAdapter.Fill(oDataSet, "roles")

        oDataAdapter = Nothing

        oConexion.Close()

        'dropDownRol.Items.Clear()

        i = 0
        While i < oDataSet.Tables("roles").Rows.Count
            oDataRow = oDataSet.Tables("roles").Rows(i)

            lstItem = New ListItem(oDataRow("nombre"), oDataRow("id"))

            dropdownRol.Items.Insert(i, lstItem)

            lstItem = Nothing
            oDataRow = Nothing
            i = i + 1
        End While

        oDataSet = Nothing

    End Sub

    Private Sub PintarPrivilegiosRestantes()
        'Realizamos la conexión a la BBDD:

        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oDataAdapter As SqlDataAdapter
        Dim oDataSet As DataSet
        Dim oListItem As ListItem
        Dim i As Integer

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open() '-->abrimos la conexión

        myCmd = New SqlCommand("SELECT id, nombre FROM privilegios WHERE id<>all(SELECT id_privilegio FROM privilegios_usuarios WHERE id_usuario=" & idusuario & ")", oConexion)

        oDataAdapter = New SqlDataAdapter(myCmd)

        oDataSet = New DataSet
        oDataAdapter.Fill(oDataSet, "privilegios_restantes")

        oDataAdapter = Nothing

        oConexion.Close()

        i = 0
        While i < oDataSet.Tables("privilegios_restantes").Rows.Count

            oListItem = New ListItem(oDataSet.Tables("privilegios_restantes").Rows(i)("nombre"), oDataSet.Tables("privilegios_restantes").Rows(i)("id"))

            lstPrivilegiosRestantes.Items.Add(oListItem)

            oListItem = Nothing
            i = i + 1
        End While

        oDataSet = Nothing

    End Sub

#End Region

End Class