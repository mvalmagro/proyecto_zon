﻿Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page

    Private id_usuario As String
    Private usuario As String
    Private password As String
    Private rol As String
    Private privilegios() As String

    Private oDataSet As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CargarTabla()
    End Sub

    Private Sub bttLogin_Click(sender As Object, e As EventArgs) Handles bttLogin.Click
        usuario = txtUsuario.Text
        password = txtPassword.Text

        If usuario.Equals("") = True Then
            lblErrorLogin.Text = "Debe introducir al menos el usuario"
            lblErrorLogin.Visible = True
        Else


            If password.Equals("") = True Then
                lblErrorLogin.Text = "Debe introducir la contraseña"
                lblErrorLogin.Visible = True

            Else
                'Realizamos la comprobación
                Dim oDataRow As DataRow
                Dim i As Integer
                Dim correcto As Boolean

                i = 0
                correcto = False
                While i < oDataSet.Tables("usuarios").Rows.Count And correcto = False
                    oDataRow = oDataSet.Tables("usuarios").Rows(i)

                    If oDataRow("nombre").Equals(usuario) = True And oDataRow("password").Equals(password) = True Then
                        correcto = True
                        id_usuario = oDataRow("id")
                        usuario = oDataRow("nombre")
                        password = oDataRow("password")
                        rol = oDataRow("nombre_rol")
                    End If

                    oDataRow = Nothing
                    i = i + 1
                End While

                If correcto = True Then
                    CargarVariablesSession()
                    Response.Redirect("calendario.aspx")

                Else
                    lblErrorLogin.Text = "Usuario/contraseña incorrectos."
                    lblErrorLogin.Visible = True
                End If


            End If

        End If

    End Sub

    Private Sub CargarTabla()
        'Realizamos la conexión a la BBDD:

        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oDataAdapter As SqlDataAdapter

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open() '-->abrimos la conexión

        myCmd = New SqlCommand("SELECT u.*,  r.nombre AS nombre_rol FROM usuarios u, roles r WHERE u.id_rol=r.id", oConexion)

        oDataAdapter = New SqlDataAdapter(myCmd)

        oDataAdapter.Fill(oDataSet, "usuarios")

        oDataAdapter = Nothing

        oConexion.Close()
    End Sub

    Private Sub CargarVariablesSession()
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oDataAdapter As SqlDataAdapter
        Dim query As String
        Dim i As Integer

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        query = "SELECT pu.id_usuario, pu.id_privilegio, p.nombre AS nombre_privilegio FROM privilegios_usuarios pu, privilegios p WHERE pu.id_privilegio=p.id and pu.id_usuario=" & id_usuario

        myCmd = New SqlCommand(query, oConexion)

        oDataAdapter = New SqlDataAdapter(myCmd)
        oDataAdapter.Fill(oDataSet, "privilegios_usuarios")

        oDataAdapter = Nothing

        oConexion.Close()

        ReDim privilegios(oDataSet.Tables("privilegios_usuarios").Rows.Count - 1)
        i = 0
        While i < privilegios.Count

            privilegios(i) = oDataSet.Tables("privilegios_usuarios").Rows(i)("nombre_privilegio")
            i = i + 1
        End While


        Session("usuario") = usuario
        Session("rol") = rol
        Session("privilegios") = privilegios

        'Añadimos variables "Session" según los privilegios del usuario: 
        i = 0
        While i < privilegios.Count

            Session(privilegios(i)) = True

            i = i + 1
        End While

    End Sub

End Class