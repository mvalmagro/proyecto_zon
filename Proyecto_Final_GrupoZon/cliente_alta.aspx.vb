Imports System.Data.SqlClient

Public Class cliente_alta
    Inherits System.Web.UI.Page

    Private nombre_comercial As String
    Private nombre_fiscal As String
    Private cif As String
    Private persona_contacto As String
    Private email As String
    Private telefono As String
    Private sector As String
    Private path_logo As String

    Private Sub btnAlta_Click(sender As Object, e As EventArgs) Handles btnAlta.Click

        If CargarImagen() = True Then '-->Tenemos que controlar con este IF que no nos hayan adjuntado archivos que no sean imágenes.

            CargarAtributos()
            LanzarInsert()
            Response.Redirect("clientes_listado.aspx")
        End If

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("clientes_listado.aspx")
    End Sub



#Region "Funciones"

    Private Sub CargarAtributos()

        nombre_comercial = txtNombreComercial.Text
        nombre_fiscal = txtNombreFiscal.Text
        cif = txtCif.Text
        persona_contacto = txtPersonaContacto.Text
        email = txtEmail.Text
        telefono = txtTelefono.Text
        sector = txtSector.Text

    End Sub

    Private Sub LanzarInsert()
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim query As String

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        query = "INSERT INTO clientes(nombre_comercial,nombre_fiscal,cif,persona_contacto,email,telefono,sector,path_logo) VALUES('" & nombre_comercial & "','" & nombre_fiscal & "','" & cif & "','" & persona_contacto & "','" & email & "','" & telefono & "','" & sector & "','" & path_logo & "')"

        myCmd = New SqlCommand(query, oConexion)

        myCmd.ExecuteNonQuery()

        oConexion.Close()

    End Sub

    Private Function CargarImagen() As Boolean
        'Este procedimiento se usa para subir archivos al servidor. 
        'Está programado para que funcione para subir 1 fichero o N ficheros.

        'Obtenemos el path de la carpeta, alojada en el servidor, donde queremos guardar los ficheros a cargar:
        Dim dirPath As String = System.Web.HttpContext.Current.Server.MapPath("~") & "/images/"

        If fileupLogoCli.HasFiles = True Then '-->Necesitamos comprobar si hay algún fichero cargado.

            'Nos guardamos la colección de ficheros que se han cargado en esta variable:
            Dim ficheros As HttpFileCollection = Request.Files


            For j = 0 To ficheros.Count - 1
                'Nos guardamos en esta variable el fichero j de la colección:
                Dim fichero As HttpPostedFile = ficheros(j)

                If CheckExtension(fileupLogoCli.FileName) = True Then
                    'Nos apuntamos el path para luego cargarlo en la BBDD:
                    path_logo = "images/cliente_" & DateTime.Now.ToString("yyyyMMddHHmmssfffffff") & "." & GetExtension(fileupLogoCli.FileName)

                    'Subimos el fichero al servidor:
                    fichero.SaveAs(dirPath & "cliente_" & DateTime.Now.ToString("yyyyMMddHHmmssfffffff") & "." & GetExtension(fileupLogoCli.FileName))
                Else
                    MsgBox("Solo puede cargar ficheros con extensiones .jpg .jpeg .gif o .png")
                    Return False
                End If
            Next

        End If

        Return True

    End Function

    Private Function GetExtension(pathCompleto As String) As String
        'Función para obtener la extensión del archivo a almacena

        Dim cadena() As String

        cadena = pathCompleto.Split(".")

        Return cadena(cadena.Count - 1)
    End Function

    Private Function CheckExtension(pathCompleto As String) As Boolean
        Dim cadena() As String
        Dim extension As String
        '.jpg, .jpeg, .gif, .png

        cadena = pathCompleto.Split(".")

        extension = cadena(cadena.Count - 1)

        If (extension.Equals("jpg") = True) Or (extension.Equals("jpeg") = True) Or (extension.Equals("gif") = True) Or (extension.Equals("png") = True) Then
            Return True
        Else
            Return False
        End If

    End Function

#End Region

End Class