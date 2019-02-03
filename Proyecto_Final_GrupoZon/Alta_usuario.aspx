<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Alta_usuario.aspx.vb" Inherits="Proyecto_Final_GrupoZon.Alta_usuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" />

        <script type ="text/javascript">
            function visible(boton) {

                if (boton == "img1") {
                    document.getElementById('<%= txtPassword.ClientID%>').setAttribute("type", "text");
                }
                else {
                    document.getElementById('<%= txtPasswordConfirm.ClientID%>').setAttribute("type", "text");
                }
                
            }  

            function oculto(boton) {
                if (boton == "img1") {
                    document.getElementById('<%= txtPassword.ClientID%>').setAttribute("type", "password");
                }
                else {
                    document.getElementById('<%= txtPasswordConfirm.ClientID%>').setAttribute("type", "password");
                }
                
            }  
            

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Alta usuario</h1>

            <br />

            <asp:Label ID="Label1" runat="server" Text="Label">Usuario:</asp:Label>
            <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
            
            <br />

            <asp:Label ID="Label2" runat="server" Text="Label">Password:</asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <i class="fa fa-eye" onmousedown="visible('img1')" onmouseup="oculto('img1')"></i>
           
            <br />

            <asp:Label ID="Label3" runat="server" Text="Label">Confirmar password:</asp:Label>
            <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password"></asp:TextBox>
            <i class="fa fa-eye" onmousedown="visible('img2')" onmouseup="oculto('img2')"></i>
            
            <br />

            <asp:Label ID="Label4" runat="server" Text="Label">Nombre y apellidos:</asp:Label>
            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            
            <br />

            <asp:Label ID="Label5" runat="server" Text="Label">Email:</asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            
            <br />

            <asp:DropDownList ID="dropDownRol" runat="server">
  
            </asp:DropDownList>

            <br />

            <asp:Button ID="bttAlta" runat="server" Text="Dar de alta" />

        </div>
        
    </form>
    
</body>


</html>
