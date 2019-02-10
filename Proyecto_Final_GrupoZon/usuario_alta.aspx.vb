Imports System.Data.SqlClient

Public Class usuario_alta
    Inherits System.Web.UI.Page

    Private Sub usuario_alta_Init(sender As Object, e As EventArgs) Handles Me.Init
        CargarRoles()
    End Sub

    Private Sub usuario_alta_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("acceso") = True Then
            'Permito el acceso

            If Session("gestion_usuarios") = True Then
                'Permito el acceso
            Else
                'Si no tienes privilegios para la "Gestión de Usuarios", no te permito el acceso:
                Response.Redirect("calendario.aspx")
            End If

        Else
            'Si no tienes acceso a la aplicación, te reedirijo a la página del login:
            Response.Redirect("Default.aspx")
        End If
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
            'Response.Redirect("usuario_alta.aspx") '-->Recargamos la página

        Else
            'Comprobar que el nombre de usuario a dar de alta no existe:
            If ComprobarDuplicidadUsuario(usuario) = True Then
                MsgBox("El usuario introducido ya existe.", MsgBoxStyle.Exclamation, "Proceso de alta de usuario fallido.")
                'Response.Redirect("usuario_alta.aspx") '-->Recargamos la página

            Else
                'Comprobar que existe coincidencia de contraseña:
                If password1.Equals(password2) = False Then
                    MsgBox("Las contraseñas introducidas no coinciden.", MsgBoxStyle.Exclamation, "Proceso de alta de usuario fallido.")
                    'Response.Redirect("usuario_alta.aspx") '-->Recargamos la página

                Else
                    'Comprobar la seguridad de contraseña
                    If ContrasenaValida(password1) = False Then
                        MsgBox("La contraseña debe contener mayusculas, minusculas, digitos, caracteres especiales y, como mínimo, una longitud de 8 caracteres.", MsgBoxStyle.Exclamation, "Proceso de alta de usuario fallido.")
                        'Response.Redirect("usuario_alta.aspx") '-->Recargamos la página
                    Else
                        'Ejecutar query para el alta
                        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
                        Dim oConexion As New SqlConnection
                        Dim myCmd As SqlCommand
                        Dim query As String
                        Dim oDataAdapter As SqlDataAdapter
                        Dim oDataSet As DataSet
                        Dim oDataRow As DataRow
                        Dim i As Integer
                        Dim id_usuario As String
                        Dim id_privilegio As String

                        oConexion = New SqlConnection(cadenaConexion)
                        oConexion.Open()

                        query = "INSERT INTO usuarios VALUES('" & usuario & "','" & password1 & "','" & nombreyapellidos & "','" & email & "'," & rol & ",'" & #1900-01-01# & "') SELECT SCOPE_IDENTITY()"

                        myCmd = New SqlCommand(query, oConexion)

                        'Recuperamos el id de usuario, el cual es un autoincremental, generado a partir de la anterior query:
                        id_usuario = myCmd.ExecuteScalar()

                        myCmd = Nothing

                        'Ahora necesitamos darle permisos al usuario según el perfil especificado:

                        query = "SELECT id_privilegios FROM privilegios_roles WHERE id_rol=" & rol & ";"

                        myCmd = New SqlCommand(query, oConexion)

                        oDataAdapter = New SqlDataAdapter(myCmd)

                        oDataSet = New DataSet
                        oDataAdapter.Fill(oDataSet, "privilegios_roles")


                        i = 0
                        While i < oDataSet.Tables("privilegios_roles").Rows.Count
                            oDataRow = oDataSet.Tables("privilegios_roles").Rows(i)
                            id_privilegio = oDataRow("id_privilegios")

                            query = "INSERT INTO privilegios_usuarios VALUES(" & id_usuario & "," & id_privilegio & ")"
                            myCmd = New SqlCommand(query, oConexion)

                            myCmd.ExecuteNonQuery()

                            oDataRow = Nothing
                            i = i + 1
                        End While

                        oDataSet = Nothing



                        oConexion.Close()

                    End If


                End If


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

            If lstItem.Text.Equals("Personalizado") <> True Then
                dropDownRol.Items.Insert(i, lstItem)
            End If

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

    Private Function ContrasenaValida(l_password As String) As Boolean
        Dim digitos As Integer
        Dim minusculas As Integer
        Dim mayusculas As Integer
        Dim otroschar As Integer
        Dim j As Integer

        For j = 0 To l_password.Length - 1
            'Preguntamos si es un número
            If IsNumeric(l_password.Substring(j, 1)) Then
                digitos += 1
            Else
                'Preguntamos si es una mayuscula
                If Asc(l_password.Substring(j, 1)) >= 65 And Asc(l_password.Substring(j, 1)) <= 90 Then
                    mayusculas += 1
                Else
                    'Preguntamos si es una minúscula
                    If Asc(l_password.Substring(j, 1)) >= 97 And Asc(l_password.Substring(j, 1)) <= 122 Then
                        minusculas += 1
                    Else
                        'Si no se ha cumplido ninguna de las anteriores condiciones, es que es otro dígito.
                        otroschar += 1
                    End If
                End If
            End If
        Next

        If l_password.Count >= 8 And digitos >= 1 And minusculas >= 1 And mayusculas >= 1 And otroschar >= 1 Then
            Return True
        Else
            Return False
        End If

    End Function




#End Region





End Class