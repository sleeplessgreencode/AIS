<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmInvoiceReceipt.aspx.vb" Inherits="AIS.FrmInvoiceReceipt" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Rekap Invoice Supplier</div>

<div>
<table>
<tr>
    <td>Tgl Entry</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="PrdAwal" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" 
            Theme="MetropolisBlue" TabIndex="2">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>        
    </td>
    <td>s.d.</td>
    <td>
        <dx:ASPxDateEdit ID="PrdAkhir" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" 
            Theme="MetropolisBlue" TabIndex="3">
            <DateRangeSettings StartDateEditID="PrdAwal"></DateRangeSettings>
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>
    </td>
</tr>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td colspan="3">
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" Theme="MetropolisBlue" Width="100%">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Vendor</td>
    <td>:</td>
    <td colspan="3">
        <dx:ASPxComboBox ID="DDLVendor" runat="server" ValueType="System.String" Theme="MetropolisBlue" Width="100%">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Sort By</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLSort" runat="server" ValueType="System.String" Theme="MetropolisBlue">
            <Items>
                <dx:ListEditItem Text="By Vendor" Value="D.VendorNm" />
                <dx:ListEditItem Text="By Project" Value="A.JobNo" Selected="true" />
            </Items>
        </dx:ASPxComboBox>
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