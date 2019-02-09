﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="usuario_listado.aspx.vb" Inherits="Proyecto_Final_GrupoZon.usuario_listado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="Content/estilos_proyecto.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <title>Listado usuarios</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Usuarios</h1>

            <br />

            <asp:GridView ID="grdviewUsuarios" runat="server" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True" CssClass="table">
                <HeaderStyle CssClass="thead-dark" />
            </asp:GridView>
            
            <br />

            <asp:Button ID="btnAlta" runat="server" Text="Altas de usuarios" />
        </div>
        
    </form>
</body>
</html>
