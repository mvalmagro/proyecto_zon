Imports System.Data.SqlClient

Public Class Login
    Inherits System.Web.UI.Page

    Private usuario As String
    Private password As String
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
                    End If

                    oDataRow = Nothing
                    i = i + 1
                End While

                If correcto = True Then
                    Session("usuario") = usuario
                    Session("password") = password
                    Response.Redirect("ventana_proyectos.aspx")

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

        myCmd = New SqlCommand("SELECT * FROM usuarios", oConexion)

        oDataAdapter = New SqlDataAdapter(myCmd)

        oDataAdapter.Fill(oDataSet, "usuarios")

        oDataAdapter = Nothing

        oConexion.Close()
    End Sub

End Class