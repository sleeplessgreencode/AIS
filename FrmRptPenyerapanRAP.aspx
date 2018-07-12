<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmRptPenyerapanRAP.aspx.vb" Inherits="AIS.FrmRptPenyerapanRAP" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
    <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="true" 
        ToolPanelView="None" HasDrilldownTabs="False" 
        HasDrillUpButton="False" HasToggleGroupTreeButton="False" 
        HasToggleParameterPanelButton="False" Width="100%" PrintMode="Pdf" 
        HasExportButton="False"/>
</div>
</asp:Content>
