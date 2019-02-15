<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="usuario_ficha.aspx.vb" Inherits="Proyecto_Final_GrupoZon.usuario_ficha" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/estilos_ficha_usuario.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <title>Usuario Ficha</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="padre">
            <div class="contenedor">
                <div class="head">
                    <img src="img/logo pez fondo claro.png" class="pez"/>
                </div>

                <div class="elementos form-group">
                    <asp:Label ID="Label1" runat="server" Text="Label">Usuario:</asp:Label>
                    <asp:TextBox ID="txtUsuario" runat="server" class="form-control"></asp:TextBox>
                </div>

                <div class="elementos form-group">
                    <asp:Label ID="Label2" runat="server" Text="Label">Nueva password:</asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" class="form-control"></asp:TextBox>
                </div>

                <div class="elementos form-group">
                    <asp:Label ID="Label3" runat="server" Text="Label">Confirmar nueva password:</asp:Label>
                    <asp:TextBox ID="txtConfirmarPassword" runat="server" class="form-control"></asp:TextBox>
                </div>

                <div class="elementos form-group">
                    <asp:Label ID="Label5" runat="server" Text="Label">Nombre y apellidos:</asp:Label>
                    <asp:TextBox ID="txtNombreyApellidos" runat="server" class="form-control"></asp:TextBox>
                </div>

                <div class="elementos form-group">
                    <asp:Label ID="Label4" runat="server" Text="Label">Email:</asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                </div>

                <div class="elementos form-group">
                    <asp:Label ID="Label6" runat="server" Text="Label">Rol:</asp:Label>
                    <asp:DropDownList ID="dropdownRol" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>

                <div class="elementos form-group">
                    <asp:Label ID="Label7" runat="server" Text="Privilegios"></asp:Label>
                </div>

                <div class="elementos form-group">
                    <asp:ListBox ID="lstPrivilegiosActuales" runat="server" class="form-control"></asp:ListBox>
                    <asp:ListBox ID="lstPrivilegiosRestantes" runat="server" class="form-control"></asp:ListBox>
                </div>

                <div class="elementos form-group">
                    <asp:Button ID="Button1" runat="server" Text="Button" CssClass="btn btn-danger" />
                </div>
            </div>
        </div>
        
    </form>
</body>
</html>
