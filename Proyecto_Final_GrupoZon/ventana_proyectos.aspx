<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ventana_proyectos.aspx.vb" Inherits="Proyecto_Final_GrupoZon.ventana_proyectos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
        
    <link href="Content/esitlos_ventana_proyectos.css" rel="stylesheet" />

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" class="form-container">

    <div class="padre">
        <div class="calendarios">
            <div class="calen">
                <asp:Calendar ID="Calendar1" runat="server" Height="5px" Width="5px"></asp:Calendar>
            </div>
            <div class="calen">
               <asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
            </div>
        </div>
        <div class="elementos">
            <div class="pantallas">
                <asp:Label ID="Label1" runat="server" Text="Elemento1"></asp:Label>
            </div>
            <div class="pantallas">
                <asp:Label ID="Label2" runat="server" Text="Elemento2"></asp:Label>
            </div>
            <div class="pantallas">
                <asp:Label ID="Label3" runat="server" Text="Elemento3"></asp:Label>
            </div>
            <div class="pantallas">
                <asp:Label ID="Label4" runat="server" Text="Elemento4"></asp:Label>
            </div>
        </div>
    </div>
                </form>

    <script src="Scripts/jquery-3.3.1.min.js"></script>
            <script src="Scripts/bootstrap.min.js"></script>
       




</body>
</html>
