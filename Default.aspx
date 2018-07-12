<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="AIS._Default" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .font1
        {font-family:'Segoe UI'; font-size: 12px;}
        .style1
        {
            height: 123px;
            width: 427px;
        }
        .style2
        {
            height: 231px;
            width: 427px;
        }
        
        .style3
        {
            width: 222px;
        }
        
    </style>
</head>
<body class="font1">
    <form id="form1" runat="server">
    <div align="center">
    <table>
    <tr>
        <td class="style1">
        </td>
    </tr>
    <tr>
        <td class="style2"  style="background-image: url('Images/login-bg.jpg'); background-repeat: no-repeat">
        <table>
        <tr>
            <td style="padding-bottom:65px"></td>
        </tr>
        </table>
        <table>
        <tr>
            <td align="right" class="style3">User ID</td>
            <td>:</td>
            <td>
                <dx:ASPxTextBox ID="TxtUser" runat="server" Width="150px" 
                    ClientInstanceName="TxtUser" TabIndex="1" MaxLength="20" CssClass="font1" AutoCompleteType="Disabled" Theme="Moderno">
                    <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="style3">Password</td>
            <td>:</td>
            <td>
                <dx:ASPxTextBox ID="TxtPasswd" runat="server" Width="150px" 
                    ClientInstanceName="TxtPasswd" TabIndex="2" MaxLength="15" CssClass="font1" Password="True" Theme="Moderno">
                    <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                        <RequiredField IsRequired="true" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td class="style3"></td>
            <td></td>
            <td style="padding-top:7px">
                <dx:ASPxButton ID="BtnLogin" runat="server" Text="LOGIN" Theme="Moderno" UseSubmitBehavior="true" 
                    Height="27px" Width="150px" TabIndex="3">                    
                </dx:ASPxButton>
            </td>
        </tr>
        </table>
        </td>
    </tr>
    </table>
    </div>
    <cc1:msgBox ID="MsgBox1" runat="server" />
    </form>
</body>
</html>
