Imports System.Data.SqlClient

Public Class Alta_usuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CargarRoles()

    End Sub

    Private Sub bttAlta_Click(sender As Object, e As EventArgs) Handles bttAlta.Click
        Dim usuario As String
        Dim password1 As String
        Dim password2 As String
        Dim nombreyapellidos As String
        Dim email As String
        Dim rol As String

        usuario = txtUsuario.Text
        password1 = txtPassword.Text
        password2 = txtPasswordConfirm.Text
        nombreyapellidos = txtNombre.Text
        email = txtEmail.Text
        rol = dropDownRol.SelectedValue


        If usuario.Equals("") = True Or password1.Equals("") = True Or password2.Equals("") = True Or nombreyapellidos.Equals("") = True Then

            MsgBox("Debe rellenar los campos obligatorios", MsgBoxStyle.Exclamation, "Proceso de alta de usuario fallido.")
            'Response.Redirect("Alta_usuario.aspx") '-->Recargamos la página

        Else
            'Comprobar que el nombre de usuario a dar de alta no existe:
            If ComprobarDuplicidadUsuario(usuario) = True Then
                MsgBox("El usuario introducido ya existe.", MsgBoxStyle.Exclamation, "Proceso de alta de usuario fallido.")
                'Response.Redirect("Alta_usuario.aspx") '-->Recargamos la página

            Else
                'Comprobar que existe coincidencia de contraseña:
                If password1.Equals(password2) = False Then
                    MsgBox("Las contraseñas introducidas no coinciden.", MsgBoxStyle.Exclamation, "Proceso de alta de usuario fallido.")
                    'Response.Redirect("Alta_usuario.aspx") '-->Recargamos la página

                End If

                'Comprobar la seguridad de contraseña

                'Ejecutar query para el alta

            End If






        End If


    End Sub


#Region "Funciones"

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

            dropDownRol.Items.Insert(i, lstItem)

            lstItem = Nothing
            oDataRow = Nothing
            i = i + 1
        End While

        oDataSet = Nothing

    End Sub


    Public Function ComprobarDuplicidadUsuario(l_usuario) As Boolean
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oDataAdapter As SqlDataAdapter
        Dim oDataSet As DataSet
        Dim i As Integer
        Dim oDataRow As DataRow


        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        myCmd = New SqlCommand("SELECT nombre FROM usuarios", oConexion)

        oDataAdapter = New SqlDataAdapter(myCmd)

        oDataSet = New DataSet
        oDataAdapter.Fill(oDataSet, "usuarios")

        oDataAdapter = Nothing

        oConexion.Close()

        i = 0
        While i < oDataSet.Tables("usuarios").Rows.Count
            oDataRow = oDataSet.Tables("usuarios").Rows(i)

            If oDataRow("nombre").Equals(l_usuario) = True Then
                Return True '-->Usuario duplicado y cortamos aquí la ejecución de esta función.
            End If

            oDataRow = Nothing
            i = i + 1
        End While


        Return False


    End Function




#End Region





End Class