﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ventana_proyectos.aspx.vb" Inherits="Proyecto_Final_GrupoZon.ventana_proyectos" %>

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

		    <div class="calendario_contenedor">
			    <div class="calendario">
                    <div class="asp">
                        <asp:Calendar ID="Calendar1" runat="server" Width="100%" Height="300px" CssClass="aspCalendario" BorderStyle="Solid" TitleStyle-CssClass="titulo_calendario" Font-Bold="True" Font-Names="Agency FB" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
<TitleStyle CssClass="titulo_calendario" BackColor="#FF9933" BorderColor="#FF9933" BorderStyle="None"></TitleStyle>
                        </asp:Calendar>
                    </div>
                    
			    </div>
			    <div class="calendario">
				    <div class="asp">
                        <asp:Calendar ID="Calendar2" runat="server" Width="100%" Height="300px" CssClass="aspCalendario" BorderStyle="Solid" TitleStyle-CssClass="titulo_calendario" Font-Bold="True" Font-Names="Agency FB" Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
<TitleStyle CssClass="titulo_calendario" BackColor="#FF9933" BorderColor="#FF9933" BorderStyle="None"></TitleStyle>
                        </asp:Calendar>
                    </div>
			    </div>
		    </div>

		    <div class="elementos_contenedor">
			    <div class="elemento">
                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="False" ForeColor="Black">Clientes</asp:LinkButton>
			    </div>
			    <div class="elemento">
                    <asp:Button ID="Button1" runat="server" Text="Button" Width="100%" Height="100%" BackColor="#984A59" BorderColor="#984A59" BorderStyle="None" CssClass="boton" />
			    </div>
			    <div class="elemento">
				    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
			    </div>
			    <div class="elemento">
				    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                    <button id="boton1" runat="server">PRUEBA</button>
                    <input type="date" name="name" value="" id="htmlcalen" runat="server"/>
			    </div>
		    </div>

	    </div>
      </form>

    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
       




</body>
</html>
