<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmViewPDF.aspx.vb" Inherits="AIS.FrmViewPDF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
</head>
<body>
    <div>
    <object data='<%=Session("ViewPDF")%>' type="application/pdf" style="height:calc(100vh - 15px)" width="100%">      
    </object>
    </div>
</body>
</html>