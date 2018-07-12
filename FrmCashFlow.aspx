<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmCashFlow.aspx.vb" Inherits="AIS.FrmCashFlow" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Cash Flow</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            Width="200px" ClientInstanceName="DDLJob" Theme="MetropolisBlue">
        </dx:ASPxComboBox>
    </td>     
</tr>
<tr>
    <td>Periode</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLBulan" runat="server" 
            ClientInstanceName="DDLBulan" Theme="MetropolisBlue">
        </dx:ASPxComboBox>
    </td>
    <td></td>
    <td>Tahun</td>
    <td>:</td>
    <td>
        <dx:ASPxSpinEdit ID="TxtTahun" runat="server" Number="0" LargeIncrement="1" 
            AllowMouseWheel="False" AllowNull="False" NullText="0" Width="80px">
        </dx:ASPxSpinEdit>        
    </td>
</tr>
<tr>
    <td colspan="2"></td>
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
