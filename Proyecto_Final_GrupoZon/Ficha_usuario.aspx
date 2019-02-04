<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Ficha_usuario.aspx.vb" Inherits="Proyecto_Final_GrupoZon.Ficha_usuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <h1>Ficha de usuario</h1>

            <asp:Label ID="Label1" runat="server" Text="Label">Usuario:</asp:Label>
            <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
            
            <br />

            <asp:Label ID="Label2" runat="server" Text="Label">Nueva password:</asp:Label>
            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
            
            <br />

            <asp:Label ID="Label3" runat="server" Text="Label">Confirmar nueva password:</asp:Label>
            <asp:TextBox ID="txtConfirmarPassword" runat="server"></asp:TextBox>

            <br />

            <asp:Label ID="Label5" runat="server" Text="Label">Nombre y apellidos:</asp:Label>
            <asp:TextBox ID="txtNombreyApellidos" runat="server"></asp:TextBox>

            <br />

            <asp:Label ID="Label4" runat="server" Text="Label">Email:</asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>

             <br />

            <asp:Label ID="Label6" runat="server" Text="Label">Rol:</asp:Label>
            <asp:DropDownList ID="dropdownRol" runat="server"></asp:DropDownList>

            <br />

            <asp:Label ID="Label7" runat="server" Text="Label">Privilegios</asp:Label>
            <br />
            <asp:ListBox ID="lstPrivilegiosActuales" runat="server"></asp:ListBox>
            <asp:ListBox ID="lstPrivilegiosRestantes" runat="server"></asp:ListBox>

            <br />

            <asp:Button ID="Button1" runat="server" Text="Button" />
        </div>
        
    </form>
</body>
</html>
