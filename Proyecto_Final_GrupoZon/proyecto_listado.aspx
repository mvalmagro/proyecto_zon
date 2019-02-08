﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="proyecto_listado.aspx.vb" Inherits="Proyecto_Final_GrupoZon.proyecto_listado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <link href="Content/estilos_proyecto.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="https://use.fontawesome.com/releases/v5.7.1/css/all.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Listado</h1>

            <br />

            <asp:GridView ID="gridProyecto" runat="server" class="table" HeaderStyle-CssClass="thead-dark" AutoGenerateDeleteButton="True" AutoGenerateEditButton="True"></asp:GridView>

            <asp:Button ID="btnAlta" runat="server" Text="Button" />

        </div>
    </form>
</body>
</html>
