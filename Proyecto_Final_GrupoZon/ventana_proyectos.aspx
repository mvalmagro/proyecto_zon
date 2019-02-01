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
        <div class="top">
    <div class="padre">
        <div class="calendarios">
            <div class="calen">
                <asp:Calendar ID="Calendar1" runat="server" cssclass="calendario"></asp:Calendar>
            </div>
            <div class="calen">
               <asp:Calendar ID="Calendar2" runat="server" cssclass="calendario"></asp:Calendar>
            </div>
        </div>
        <div class="elementos">
            <div class="pantallas">

            </div>
            <div class="pantallas">

            </div>
            <div class="pantallas">

            </div>
            <div class="pantallas">

            </div>
        </div>
    </div>
            </div>
                </form>

    <script src="Scripts/jquery-3.3.1.min.js"></script>
            <script src="Scripts/bootstrap.min.js"></script>
       




</body>
</html>
