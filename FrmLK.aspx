<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmLK.aspx.vb" Inherits="AIS.FrmLK" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type = "text/javascript">
    function OpenNewTab() {
        document.forms[0].target = "_blank";
        setTimeout(function () { window.document.forms[0].target = ''; }, 0);
    };
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Laporan Posisi Keuangan (Neraca)</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            Width="200px" ClientInstanceName="DDLJob" Theme="MetropolisBlue" 
            AutoPostBack="true">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Site</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLSite" runat="server" ValueType="System.String" 
            Width="200px" ClientInstanceName="DDLSite" Theme="MetropolisBlue">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
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
        <dx:ASPxButton ID="BtnPrint" runat="server" Text="PRINT" 
            Theme="MetropolisBlue" Width="75px">
            <ClientSideEvents Click="function(s,e) { OpenNewTab(); }" />
        </dx:ASPxButton>
    </td>
</tr>
</table>

<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>

</asp:Content>
