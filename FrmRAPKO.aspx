<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmRAPKO.aspx.vb" Inherits="AIS.FrmRAPKO" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Penyerapan RAP Fisik Dengan KO</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td colspan="3">
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" Width="300px" 
            ClientInstanceName="DDLJob" Theme="MetropolisBlue">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <dx:ASPxButton ID="BtnPrint" runat="server" Text="PRINT" 
            Theme="MetropolisBlue" Width="75px">
        </dx:ASPxButton>  
    </td>
</tr>
</table>

<table>
<tr>
    <td>
        <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="true" ToolPanelView="None" 
        HasToggleParameterPanelButton="False" HasToggleGroupTreeButton="False" />
    </td>
</tr>
</table>

</div>

</asp:Content>