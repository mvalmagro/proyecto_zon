Imports System.Data.SqlClient
Imports System.IO
Imports System.Reflection

Public Class cliente_ficha
    Inherits System.Web.UI.Page

    Private idCliente As String
    Private nombreComercial As String
    Private nombreFiscal As String
    Private cif As String
    Private personaContacto As String
    Private email As String
    Private telefono As String
    Private sector As String

    Private path_logo As String
    Private path_logo_anterior As New String("")

    Private Sub cliente_ficha_Init(sender As Object, e As EventArgs) Handles Me.Init
        'Guardamos el idCliente, el cual se nos ha pasado por parámetro a esta Page desde la Page anterior:
        idCliente = Request.QueryString("Valor")

        'Cargamos el resto de atributos de la clase:
        CargarDatos()

        'Pintamos los datos en las cajas de texto y mostramos la imagen:
        PintarDatos()

    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click

        If CargarImagen() = True Then '-->Tenemos que controlar con este IF que no nos hayan adjuntado archivos que no sean imágenes.

            If path_logo_anterior.Equals("") <> True Then
                Dim oDirectoryInfo As New DirectoryInfo(path_logo_anterior)

                'Debemos borrar la imagen anterior para no dejar archivos residuales en el server:
                If System.IO.File.Exists(oDirectoryInfo.FullName) = True Then
                    System.IO.File.Delete(oDirectoryInfo.FullName)
                End If

            End If

            ActualizarAtributos()
            LanzarUpdate()
            Response.Redirect("clientes_listado.aspx")
        Else

        End If

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Response.Redirect("clientes_listado.aspx")
    End Sub

#Region "Funciones"

    Private Sub CargarDatos()

        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim oDataAdapter As SqlDataAdapter
        Dim oDataSet As New DataSet

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        myCmd = New SqlCommand("SELECT * FROM clientes WHERE id=" & idCliente, oConexion)
        oDataAdapter = New SqlDataAdapter(myCmd)
        oDataAdapter.Fill(oDataSet, "clientes")
        oDataAdapter = Nothing
        myCmd = Nothing

        nombreComercial = oDataSet.Tables("clientes").Rows(0)("nombre_comercial")
        nombreFiscal = TratarValoresNulos(oDataSet.Tables("clientes").Rows(0)("nombre_fiscal"))
        cif = TratarValoresNulos(oDataSet.Tables("clientes").Rows(0)("cif"))
        personaContacto = TratarValoresNulos(oDataSet.Tables("clientes").Rows(0)("persona_contacto"))
        email = TratarValoresNulos(oDataSet.Tables("clientes").Rows(0)("email"))
        telefono = TratarValoresNulos(oDataSet.Tables("clientes").Rows(0)("telefono"))
        sector = TratarValoresNulos(oDataSet.Tables("clientes").Rows(0)("sector"))
        path_logo = TratarValoresNulos(oDataSet.Tables("clientes").Rows(0)("path_logo"))

        oConexion.Close()
    End Sub

    Private Sub PintarDatos()
        Dim aux_pathLogo As String

        txtNombreComercial.Text = nombreComercial
        txtNombreFiscal.Text = nombreFiscal
        txtCif.Text = cif
        txtPersonaContacto.Text = personaContacto
        txtEmail.Text = email
        txtTelefono.Text = telefono
        txtSector.Text = sector


        If path_logo.Equals("") = True Then
            imgLogo.Dispose()
            imgLogo.Visible = False

        Else
            aux_pathLogo = "~/" & path_logo
            imgLogo.ImageUrl = aux_pathLogo
        End If

    End Sub


    Private Function TratarValoresNulos(valor As Object) As String
        'Utilizamos esta función para tratar los NULL de la BBDD.

        If valor.Equals(DBNull.Value) = True Then
            valor = ""
            Return valor
        Else
            Return valor
        End If

    End Function

    Private Function CargarImagen() As Boolean
        'Este procedimiento se usa para subir archivos al servidor. 
        'Está programado para que funcione para subir 1 fichero o N ficheros.

        'Obtenemos el path de la carpeta, alojada en el servidor, donde queremos guardar los ficheros a cargar:
        Dim dirPath As String = System.Web.HttpContext.Current.Server.MapPath("~") & "/images/"
        Dim nombreAleatorio As String

        If fileupLogoCli.HasFiles = True Then '-->Necesitamos comprobar si hay algún fichero cargado.

            'Nos guardamos la colección de ficheros que se han cargado en esta variable:
            Dim ficheros As HttpFileCollection = Request.Files


            For j = 0 To ficheros.Count - 1
                'Nos guardamos en esta variable el fichero j de la colección:
                Dim fichero As HttpPostedFile = ficheros(j)

                If CheckExtension(fileupLogoCli.FileName) = True Then '-->Con este if comprobamos que el fichero sea '.jpg, .jpeg, .gif, .png

                    nombreAleatorio = DateTime.Now.ToString("yyyyMMddHHmmssfffffff")

                    'Nos guardamos la ruta de la imagen antigua para luego borrarla del servidor y no dejar archivos residuales:
                    path_logo_anterior = path_logo

                    'Nos apuntamos el path para luego cargarlo en la BBDD:
                    path_logo = "images/cliente_" & nombreAleatorio & "." & GetExtension(fileupLogoCli.FileName)

                    'Subimos el fichero al servidor:
                    fichero.SaveAs(dirPath & "cliente_" & nombreAleatorio & "." & GetExtension(fileupLogoCli.FileName))
                    'fichero.SaveAs(dirPath & "cliente_" & DateTime.Now.ToString("yyyyMMddHHmmssfffffff") & "." & GetExtension(fileupLogoCli.FileName))
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

        cadena = pathCompleto.Split(".")

        extension = cadena(cadena.Count - 1)

        If (extension.Equals("jpg") = True) Or (extension.Equals("jpeg") = True) Or (extension.Equals("gif") = True) Or (extension.Equals("png") = True) Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub ActualizarAtributos()
        nombreComercial = txtNombreComercial.Text
        nombreFiscal = txtNombreFiscal.Text
        cif = txtCif.Text
        personaContacto = txtPersonaContacto.Text
        email = txtEmail.Text
        telefono = txtTelefono.Text
        sector = txtSector.Text
    End Sub

    Private Sub LanzarUpdate()
        Dim cadenaConexion As String = "Server=pmssql100.dns-servicio.com;Database=6438944_zon;User Id=jrcmvaa;Password=Ssaleoo9102;"
        Dim oConexion As New SqlConnection
        Dim myCmd As SqlCommand
        Dim query As String

        oConexion = New SqlConnection(cadenaConexion)

        oConexion.Open()

        query = "UPDATE clientes SET nombre_comercial='" & nombreComercial & "' , nombre_fiscal='" & nombreFiscal & "' , cif='" & cif & "' , persona_contacto='" & personaContacto & "' , email='" & email & "' , telefono='" & telefono & "' , sector='" & sector & "', path_logo='" & path_logo & "' WHERE id=" & idCliente

        myCmd = New SqlCommand(query, oConexion)

        myCmd.ExecuteNonQuery()

        oConexion.Close()


    End Sub

#End Region

End Class