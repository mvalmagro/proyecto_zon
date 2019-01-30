<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Proyecto_Final_GrupoZon.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="estilos.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <title>Login</title>
</head>

<body>
    <form id="form1" runat="server">
<div class="container container-login">
            <div class="row">
                <div class="col-12 login-form-2">
                    <h3>GrupoZon</h3>
                        <div class="form-group">
                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="bttLogin" runat="server" Text="Login" CssClass="btnSubmit" />
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblErrorLogin" runat="server" Text="" Visible="False" ForeColor="White"></asp:Label>
                        </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
