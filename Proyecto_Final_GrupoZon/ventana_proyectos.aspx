<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ventana_proyectos.aspx.vb" Inherits="Proyecto_Final_GrupoZon.ventana_proyectos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
        <link href="Content/materialize.min.css" rel="stylesheet" />
    <link href="Content/esitlos_ventana_proyectos.css" rel="stylesheet" />

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>


<body>
    <form id="form1" runat="server" class="form-container">
        <div class="container-fluid">
            <div class="row">
                    <div class="col-6">
                        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                    </div>
                    <div class="col-6">
                        

                    </div>
                    
                </div>
            
            <div class="row">
                <div class="col-6">
                    <asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
                    <p>HOLA MANUEL</p>
                </div>
           </div>
      </div>

    </form>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
            <script src="Scripts/bootstrap.min.js"></script>
        <script src="Content/materialize.min.js"></script>




</body>


</html>
