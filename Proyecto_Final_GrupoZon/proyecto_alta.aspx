<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="proyecto_alta.aspx.vb" Inherits="Proyecto_Final_GrupoZon.proyecto_alta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/estilos_alta.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <title>Proyectos</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="padre">

            <div class="contenedor">

                <div class="head">
                    <img src="img/logo pez fondo claro.png" class="pez"/>
                </div>

                <div class="elementos form-group">
                  <asp:Label ID="Label2" runat="server" Text="Titulo"></asp:Label>
                  <asp:TextBox ID="txtTitulo" runat="server" class="form-control"></asp:TextBox>
                </div>

               <div class="elementos form-group">
                  <asp:Label ID="Label1" runat="server" Text="Comercial"></asp:Label>
                  <asp:TextBox ID="txtComercial" runat="server" class="form-control"></asp:TextBox>
               </div>

               <div class="elementos form-group">
                  <asp:Label ID="Label3" runat="server" Text="Fecha Pedido"></asp:Label>
                  <asp:TextBox ID="txtFecha_pedido" runat="server" type="date" class="form-control"></asp:TextBox>
               </div>

              <div class="elementos form-group">
                  <asp:Label ID="Label4" runat="server" Text="Fecha Evento"></asp:Label>
                  <asp:TextBox ID="txtFecha_evento" runat="server" type="datetime-local" class="form-control"></asp:TextBox>
              </div>

              <div class="elementos form-group">
                  <asp:Label ID="Label5" runat="server" Text="Cliente"></asp:Label>
                  <asp:DropDownList ID="lstLista" runat="server"></asp:DropDownList>
              </div>

              <div class="elementos">
                  <asp:Button ID="btnEnviar" runat="server" Text="Button" />
              </div>

            </div>
          </div>
    </form>
</body>
</html>
