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

    Private Sub usuario_ficha_Init(sender As Object, e As EventArgs) Handles Me.Init
        idusuario = Request.QueryString("Valor")
        CargarDatos()

        PintarDatos()

        lstPrivilegiosActuales.ForeColor = Drawing.Color.Green
        lstPrivilegiosRestantes.ForeColor = Drawing.Color.Red

        If dropdownRol.SelectedItem.Text <> "Personalizado" Then
            btnArrowCompletoDerecha.Enabled = False
            btnArrowCompletoIzquierda.Enabled = False
            btnArrowDerecha.Enabled = False
            btnArrowIzquierda.Enabled = False
        End If

        txtPassword.Enabled = False
        txtConfirmarPassword.Enabled = False

        txtUsuario.Enabled = False

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("usuario_listado.aspx")
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim query As String


        If chkPassword.Checked = True Then
            '***VAMOS A CAMBIAR  LA PASSWORD***
            Dim pass1 As String
            Dim pass2 As String

            pass1 = txtPassword.Text
            pass2 = txtConfirmarPassword.Text

            If pass1.Equals(pass2) = True Then

                If ContrasenaValida(pass1) = True Then
                    query = "UPDATE usuarios SET password='" & pass1 & "' WHERE id=" & idusuario

                    oConexion = New SqlConnection(cadenaConexion)

                    oConexion.Open()

                    myCmd = New SqlCommand(query, oConexion)
                    myCmd.ExecuteNonQuery()
                    myCmd = Nothing

                    oConexion.Close()

                    Response.Redirect("usuario_listado.aspx")
                Else
                    MsgBox("La contraseña debe contener mayusculas, minusculas, digitos, caracteres especiales y, como mínimo, una longitud de 8 caracteres.", MsgBoxStyle.Exclamation, "Proceso de cambio de password fallido.")
                End If

            Else

                MsgBox("Las contraseñas introducidas no coinciden.", MsgBoxStyle.Exclamation, "Proceso de cambio de password fallido.")

            End If


        Else
            '***VAMOS A MODIFICAR LOS DATOS DEL USUARIO***
            Dim i As Integer

            '==========================================Capturamos datos:
            nombre = txtNombreyApellidos.Text
            email = txtEmail.Text
            id_rol = dropdownRol.SelectedValue


            ReDim id_privilegios(lstPrivilegiosActuales.Items.Count - 1)

            While i < id_privilegios.Count

                id_privilegios(i) = lstPrivilegiosActuales.Items(i).Value

                i = i + 1
            End While
            '============================================================


            oConexion = New SqlConnection(cadenaConexion)

            oConexion.Open()


            'Eliminamos todos los registros corespondientes al usuario en la tabla de "privilegios_usuarios":
            query = "DELETE FROM privilegios_usuarios WHERE id_usuario=" & idusuario
            myCmd = New SqlCommand(query, oConexion)
            myCmd.ExecuteNonQuery()
            myCmd = Nothing
            '=================================================================================================


            '===============================Incluimos al usuario dentro de la tabla de "privilegios_usuarios" con sus nuevos privilegios:
            i = 0
            While i < id_privilegios.Count
                query = "INSERT INTO privilegios_usuarios(id_usuario,id_privilegio) VALUES(" & idusuario & "," & id_privilegios(i) & ")"

                myCmd = New SqlCommand(query, oConexion)

                myCmd.ExecuteNonQuery()

                myCmd = Nothing
                i = i + 1
            End While
            '=============================================================================================================================

            'Ahora realizamos el modify en la tabla de usuarios:
            query = "UPDATE usuarios SET nombre_real='" & nombre & "' , email='" & email & "' , id_rol='" & id_rol & "' WHERE id=" & idusuario
            myCmd = New SqlCommand(query, oConexion)
            myCmd.ExecuteNonQuery()
            '===================================================

            oConexion.Close()

            Response.Redirect("usuario_listado.aspx")

        End If





    End Sub


    Private Sub chkPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkPassword.CheckedChanged

        If chkPassword.Checked = True Then
            txtPassword.Enabled = True
            txtConfirmarPassword.Enabled = True

            txtNombreyApellidos.Enabled = False
            txtEmail.Enabled = False
            lstPrivilegiosActuales.Enabled = False
            lstPrivilegiosRestantes.Enabled = False
            dropdownRol.Enabled = False
            btnArrowCompletoDerecha.Enabled = False
            btnArrowCompletoIzquierda.Enabled = False
            btnArrowDerecha.Enabled = False
            btnArrowIzquierda.Enabled = False

            btnModificar.Text = "Cambiar contraseña"
        Else
            txtPassword.Enabled = False
            txtConfirmarPassword.Enabled = False

            txtNombreyApellidos.Enabled = True
            txtEmail.Enabled = True
            lstPrivilegiosActuales.Enabled = True
            lstPrivilegiosRestantes.Enabled = True
            dropdownRol.Enabled = True
            btnArrowCompletoDerecha.Enabled = True
            btnArrowCompletoIzquierda.Enabled = True
            btnArrowDerecha.Enabled = True
            btnArrowIzquierda.Enabled = True

            btnModificar.Text = "Modificar datos"
        End If

    End Sub


    Private Sub dropdownRol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dropdownRol.SelectedIndexChanged

        If dropdownRol.SelectedItem.Text <> "Personalizado" Then
            btnArrowCompletoDerecha.Enabled = False
            btnArrowCompletoIzquierda.Enabled = False
            btnArrowDerecha.Enabled = False
            btnArrowIzquierda.Enabled = False

            lstPrivilegiosActuales.Items.Clear()
            lstPrivilegiosRestantes.Items.Clear()


            CargarPrivilegiosSegunRol(dropdownRol.SelectedValue)
        Else
            btnArrowCompletoDerecha.Enabled = True
            btnArrowCompletoIzquierda.Enabled = True
            btnArrowDerecha.Enabled = True
            btnArrowIzquierda.Enabled = True
        End If



    End Sub


    Private Sub btnArrowCompletoDerecha_Click(sender As Object, e As EventArgs) Handles btnArrowCompletoDerecha.Click
        Dim i As Integer
        Dim oListItemCollection As ListItemCollection

        oListItemCollection = lstPrivilegiosActuales.Items

        i = 0
        While i < lstPrivilegiosActuales.Items.Count

            lstPrivilegiosRestantes.Items.Add(oListItemCollection.Item(i))
            i = i + 1
        End While
        lstPrivilegiosActuales.Items.Clear()

    End Sub


    Private Sub btnArrowCompletoIzquierda_Click(sender As Object, e As EventArgs) Handles btnArrowCompletoIzquierda.Click
        Dim i As Integer
        Dim oListItemCollection As ListItemCollection

        oListItemCollection = lstPrivilegiosRestantes.Items

        i = 0
        While i < lstPrivilegiosRestantes.Items.Count

            lstPrivilegiosActuales.Items.Add(oListItemCollection.Item(i))
            i = i + 1
        End While
        lstPrivilegiosRestantes.Items.Clear()
    End Sub



    Private Sub btnArrowDerecha_Click(sender As Object, e As EventArgs) Handles btnArrowDerecha.Click
        Dim ElementosEliminar As ArrayList = New ArrayList

        For Each item As ListItem In lstPrivilegiosActuales.Items
            If item.Selected Then
                lstPrivilegiosRestantes.Items.Add(New ListItem(item.Text, item.Value))
                ElementosEliminar.Add(item)
            End If
        Next
        RemoveElements(lstPrivilegiosActuales, ElementosEliminar)

    End Sub


    Private Sub btnArrowIzquierda_Click(sender As Object, e As EventArgs) Handles btnArrowIzquierda.Click
        Dim ElementosEliminar As ArrayList = New ArrayList

        For Each item As ListItem In lstPrivilegiosRestantes.Items
            If item.Selected Then
                lstPrivilegiosActuales.Items.Add(New ListItem(item.Text, item.Value))
                ElementosEliminar.Add(item)
            End If
        Next
        RemoveElements(lstPrivilegiosRestantes, ElementosEliminar)
    End Sub




#Region "Funciones principales"

    Private Sub CargarPrivilegiosSegunRol(l_idrol As String)
        Dim id_privilegios_no_rol() As String
        Dim nombre_privilegios_no_rol() As String

        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oDataAdapter As SqlDataAdapter
        Dim oDataSet As New DataSet
        Dim i As Integer
        Dim j As Integer
        Dim oListItem As ListItem
        Dim sw As Boolean

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()


        myCmd = New SqlCommand("Select pr.id_privilegios As id_privilegio, p.nombre As nombre FROM privilegios_roles pr, privilegios p WHERE pr.id_privilegios=p.id And id_rol=" & l_idrol, oConexion)
        oDataAdapter = New SqlDataAdapter(myCmd)
        oDataAdapter.Fill(oDataSet, "privilegios_rol")
        oDataAdapter = Nothing
        myCmd = Nothing

        myCmd = New SqlCommand("Select id, nombre FROM privilegios", oConexion)
        oDataAdapter = New SqlDataAdapter(myCmd)
        oDataAdapter.Fill(oDataSet, "privilegios_no_rol")
        oDataAdapter = Nothing
        myCmd = Nothing


        oConexion.Close()

        ReDim id_privilegios((oDataSet.Tables("privilegios_rol").Rows.Count) - 1)
        ReDim nombre_privilegios(id_privilegios.Count - 1)
        i = 0
        While i < id_privilegios.Count
            id_privilegios(i) = oDataSet.Tables("privilegios_rol").Rows(i)("id_privilegio")
            nombre_privilegios(i) = oDataSet.Tables("privilegios_rol").Rows(i)("nombre")
            i = i + 1
        End While


        ReDim id_privilegios_no_rol((oDataSet.Tables("privilegios_no_rol").Rows.Count) - 1)
        ReDim nombre_privilegios_no_rol((oDataSet.Tables("privilegios_no_rol").Rows.Count) - 1)
        i = 0
        While i < id_privilegios_no_rol.Count
            id_privilegios_no_rol(i) = oDataSet.Tables("privilegios_no_rol").Rows(i)("id")
            nombre_privilegios_no_rol(i) = oDataSet.Tables("privilegios_no_rol").Rows(i)("nombre")
            i = i + 1
        End While

        '=====Cargamos los Privilegios que tiene actualmente el perfil seleccionado en el ListBox correspondiente:
        i = 0
        While i < id_privilegios.Count
            oListItem = New ListItem(nombre_privilegios(i), id_privilegios(i))

            lstPrivilegiosActuales.Items.Add(oListItem)

            oListItem = Nothing
            i = i + 1
        End While
        '==============================================================================================

        '==Cargamos en el ListBox correspondiente los Privilegios que NO tiene actualmente el perfil seleccionado:
        i = 0
        j = 0
        sw = True
        oListItem = Nothing
        While (i < id_privilegios_no_rol.Count)

            While (j < id_privilegios.Count) And (sw = True)

                If id_privilegios_no_rol(i).Equals(id_privilegios(j)) = True Then
                    sw = False
                Else
                    j = j + 1
                End If

            End While

            If sw = True Then
                'Insertamos id_privilegios_no_rol(i)
                oListItem = New ListItem(nombre_privilegios_no_rol(i), id_privilegios_no_rol(i))
                lstPrivilegiosRestantes.Items.Add(oListItem)
                oListItem = Nothing
            End If

            sw = True
            j = 0
            i = i + 1
        End While

        '==================================================================================================


    End Sub

    Private Sub CargarDatos()

        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oDataAdapter As SqlDataAdapter
        Dim oDataSet As New DataSet
        Dim i As Integer

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        myCmd = New SqlCommand("Select u.*, r.nombre As nombrerol FROM usuarios u, roles r WHERE r.id=u.id_rol And u.id=" & idusuario, oConexion)
        oDataAdapter = New SqlDataAdapter(myCmd)
        oDataAdapter.Fill(oDataSet, "usuarios")
        oDataAdapter = Nothing
        myCmd = Nothing

        myCmd = New SqlCommand("Select pu.id_privilegio, p.nombre FROM privilegios_usuarios pu, privilegios p WHERE p.id=pu.id_privilegio And id_usuario=" & idusuario, oConexion)
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


    Private Sub RemoveElements(ByRef ListBox As ListBox, ByVal Elements As ArrayList)

        For Each item As ListItem In Elements
            ListBox.Items.Remove(item)
        Next

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