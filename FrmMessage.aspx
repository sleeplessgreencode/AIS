<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmMessage.aspx.vb" Inherits="AIS.FrmMessage" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Broadcast Notification</div>

<div>
<table>
<tr>
    <td>Notification Message</td>
    <td>:</td>
    <td colspan="2"> 
        <dx:ASPxTextBox ID="TxtMessage" runat="server" Width="400px" MaxLength="100">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxTextBox>
    </td>    
</tr>
<tr>
    <td>Hide Notification After</td>
    <td>:</td>
    <td> 
        <dx:ASPxDateEdit ID="TxtDate" runat="server" DisplayFormatString="dd-MMM-yyyy"
            Theme="MetropolisBlue">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>
    </td>    
    <td>
        <dx:ASPxTimeEdit ID="TxtTime" runat="server" Theme="MetropolisBlue">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxTimeEdit>
    </td>
</tr>
<tr>
    <td colspan="2"></td>
    <td style="padding-top:5px">
        <dx:ASPxButton ID="BtnSave" runat="server" Text="SAVE" 
            Theme="MetropolisBlue">
        </dx:ASPxButton>
    </td>
    <td style="padding-top:5px">
        <dx:ASPxButton ID="BtnClear" runat="server" Text="CLEAR NOTIFICATION" 
            Theme="MetropolisBlue" CausesValidation="false">
        </dx:ASPxButton>
    </td>
</tr>
</table>

<asp:GridView ID="GridView1" runat="server">
</asp:GridView>
</div>
<cc1:msgBox ID="msgBox1" runat="server" /> 

</asp:Content>
