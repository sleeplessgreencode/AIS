<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmRptNeracaMutasi.aspx.vb" Inherits="AIS.FrmRptNeracaMutasi" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
    <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="true" 
        ToolPanelView="None" HasDrilldownTabs="False" 
        HasDrillUpButton="False" HasToggleGroupTreeButton="False" 
        HasToggleParameterPanelButton="False" Width="100%" PrintMode="ActiveX" 
        HasExportButton="False" PageZoomFactor="115" />
</div>
</asp:Content>
