<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GenQRCode.aspx.vb" Inherits="AIS.GenQRCode" %>

<%@ Register Assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
    <dx:ASPxButton ID="btnGenerate" runat="server" Text="Generate">
    </dx:ASPxButton>
    <hr />
    <asp:PlaceHolder ID="plBarCode" runat="server" />
    <cc1:msgBox ID="MsgBox1" runat="server" />
    </form>
</body>
</html>
