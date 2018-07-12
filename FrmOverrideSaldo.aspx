<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmOverrideSaldo.aspx.vb" Inherits="AIS.FrmOverrideSaldo" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Override Saldo PJ</div>

<div>
<table>
<tr>
    <td>No. PJ</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoPJ" runat="server" Width="250px" 
            ClientInstanceName="TxtNoPD" AutoCompleteType="Disabled" 
            Theme="MetropolisBlue" AutoPostBack="true">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxTextBox>
    </td>
</tr>
<tr>
    <td>Saldo PJ</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtSaldo1" runat="server" Width="250px" Enabled="false" Text="0" 
            HorizontalAlign="Right">
            <MaskSettings Mask="&lt;0..99999999999g&gt;" />
        </dx:ASPxTextBox>                                  
    </td>
    <td></td>
    <td>Override Menjadi</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtSaldo2" runat="server" Width="250px" Text="0" 
            HorizontalAlign="Right">
            <MaskSettings Mask="&lt;0..99999999999g&gt;" />
        </dx:ASPxTextBox>                                  
    </td>
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <dx:ASPxMemo ID="TxtRemark" runat="server" Height="40px" Width="300px" MaxLength="255" 
            Caption="Remark Override Saldo" CaptionSettings-Position="Top">
        </dx:ASPxMemo>
    </td>
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN" UseSubmitBehavior="false" 
            CausesValidation="false"  Width="75px" Theme="MetropolisBlue">
        </dx:ASPxButton>        
    </td>
</tr>
</table>
</div>
<cc1:msgBox ID="MsgBox1" runat="server" />
</asp:Content>
