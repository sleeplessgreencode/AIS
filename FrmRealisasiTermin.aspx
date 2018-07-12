<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmRealisasiTermin.aspx.vb" Inherits="AIS.FrmRealisasiTermin" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Monitoring DIPA Termin</div>

<div>
<table>
<tr>
    <td>Periode</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="PrdAwal" runat="server" 
            DisplayFormatString="dd-MMM-yyyy" Theme="MetropolisBlue">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>        
    </td>
    <td>s.d.</td>
    <td>
        <dx:ASPxDateEdit ID="PrdAkhir" runat="server"
            DisplayFormatString="dd-MMM-yyyy" Theme="MetropolisBlue">
            <DateRangeSettings StartDateEditID="PrdAwal"></DateRangeSettings>
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>
    </td>
</tr>
<tr>
    <td colspan="2" style="padding-top:10px"></td>
    <td>
        <dx:ASPxButton ID="BtnPrint" runat="server" Text="PREVIEW" 
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

<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>

</asp:Content>
