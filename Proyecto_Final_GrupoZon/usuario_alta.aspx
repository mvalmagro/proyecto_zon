<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="usuario_alta.aspx.vb" Inherits="Proyecto_Final_GrupoZon.usuario_alta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/estilos_proyecto.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" />
    <title>Alta usuario</title>
   

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


        <div class="padre">

            <div class="contenedor">

                <div class="head">
                    <img src="img/logo pez fondo claro.png" class="pez"/>
                </div>

                <div class="elementos form-group">
                    <asp:Label ID="lblTitulo" runat="server" Text="Label" Font-Size="XX-Large">Alta de usuario</asp:Label>
                </div>

                <div class="elementos form-group">
                    <asp:Label ID="Label1" runat="server" Text="Label">Usuario:</asp:Label>
                    <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
                </div>

               <div class="elementos form-group">
                    <asp:Label ID="Label2" runat="server" Text="Label">Password:</asp:Label>
                    <div>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        <i class="fa fa-eye" onmousedown="visible('img1')" onmouseup="oculto('img1')"></i>
                    </div>
               </div>

               <div class="elementos form-group">
                    <asp:Label ID="Label3" runat="server" Text="Label">Confirmar password:</asp:Label>
                    <div>
                        <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password"></asp:TextBox>
                        <i class="fa fa-eye" onmousedown="visible('img2')" onmouseup="oculto('img2')"></i>
                    </div>
               </div>

              <div class="elementos form-group">
                    <asp:Label ID="Label4" runat="server" Text="Label">Nombre y apellidos:</asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
              </div>

                
              <div class="elementos form-group">
                    <asp:Label ID="Label5" runat="server" Text="Label">Email:</asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
              </div>

                
              <div class="elementos form-group">
                    <asp:Label ID="Label6" runat="server" Text="Label">Rol:</asp:Label>
                    <asp:DropDownList ID="dropDownRol" runat="server">
                    </asp:DropDownList>
              </div>

             <div class="elementos form-group">
                    <asp:Button ID="bttAlta" runat="server" Text="Dar de alta" />
              </div>

            </div>
          </div>
        
    </form>
    
</body>


</html>
