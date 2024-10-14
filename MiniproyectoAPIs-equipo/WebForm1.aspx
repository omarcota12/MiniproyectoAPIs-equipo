<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="MiniproyectoAPIs_equipo.WebForm1" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>Búsqueda de Videos en YouTube</title>
     <style>
        .video-item {
            margin: 10px;
            display: inline-block;
        }
        .video-item img {
            width: 120px; /* Ajusta el tamaño de la miniatura */
            height: 90px; /* Ajusta el tamaño de la miniatura */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtSearch" runat="server" placeholder="Ingrese un término de búsqueda" />
            <asp:Button ID="btnSearch" runat="server" Text="Buscar" OnClick="btnSearch_Click" />
            <div id="resultsPanel" runat="server"></div>
        </div>
    </form>
</body>
</html>
