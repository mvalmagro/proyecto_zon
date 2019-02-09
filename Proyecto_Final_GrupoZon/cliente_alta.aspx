<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cliente_alta.aspx.vb" Inherits="Proyecto_Final_GrupoZon.cliente_alta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/estilos_proyecto.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <title>Alta de clientes</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="padre">

            <div class="contenedor">

                <div class="head">
                    <img src="img/logo pez fondo claro.png" class="pez"/>
                </div>

                <div class="elementos form-group">
                    <asp:Label ID="txtCliente" runat="server" Text="Label" Font-Size="XX-Large">Alta de cliente</asp:Label>
                </div>

                <div class="elementos form-group">
                    <asp:Label ID="Label1" runat="server" Text="Label">Nombre comercial:</asp:Label>
                    <asp:TextBox ID="txtNombreComercial" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
                </div>

               <div class="elementos form-group">
                    <asp:Label ID="Label2" runat="server" Text="Label">Nombre fiscal:</asp:Label>
                    <asp:TextBox ID="txtNombreFiscal" runat="server" class="form-control" MaxLength="100"></asp:TextBox>
               </div>

               <div class="elementos form-group">
                    <asp:Label ID="Label3" runat="server" Text="Label">CIF:</asp:Label>
                    <asp:TextBox ID="txtCif" runat="server" class="form-control" MaxLength="9"></asp:TextBox>
               </div>

              <div class="elementos form-group">
                    <asp:Label ID="Label4" runat="server" Text="Label">Persona de contacto:</asp:Label>
                    <asp:TextBox ID="txtPersonaContacto" runat="server" class="form-control" MaxLength="60"></asp:TextBox>
              </div>

              <div class="elementos form-group">
                    <asp:Label ID="Label5" runat="server" Text="Label">Email:</asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" class="form-control" MaxLength="60" TextMode="Email"></asp:TextBox>
              </div>

              <div class="elementos form-group">
                    <asp:Label ID="Label6" runat="server" Text="Label">Teléfono:</asp:Label>
                    <asp:TextBox ID="txtTelefono" runat="server" class="form-control" MaxLength="20" TextMode="Phone"></asp:TextBox>
              </div>

              <div class="elementos form-group">
                    <asp:Label ID="Label7" runat="server" Text="Label">Sector:</asp:Label>
                    <asp:TextBox ID="txtSector" runat="server" class="form-control" MaxLength="60"></asp:TextBox>
              </div>

               <div class="elementos form-group">
                    <asp:Label ID="Label8" runat="server" Text="Label">Logo del cliente:</asp:Label>
                    <asp:FileUpload ID="fileupLogoCli" runat="server"  />
              </div>

              <div class="elementos form-group">
                  <asp:Button ID="btnAlta" runat="server" Text="Alta" />
                  </div>

              <div class="elementos form-group">
                  <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
              </div>

            </div>
          </div>
    </form>
</body>
</html>
