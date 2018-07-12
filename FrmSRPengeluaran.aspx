<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmSRPengeluaran.aspx.vb" Inherits="AIS.FrmSRPengeluaran" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

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
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Summary Rekap Pengeluaran</div>

<div>
<table>
<tr>
    <td>Status Job</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLStatus" runat="server" ValueType="System.String" Width="250px" 
            ClientInstanceName="DDLStatus" Theme="MetropolisBlue">
            <Items>
                <dx:ListEditItem Text="On Going" Selected="true" Value="Pelaksanaan" />
                <dx:ListEditItem Text="Pemeliharaan (Pra FHO)" Value="Pemeliharaan" />
                <dx:ListEditItem Text="Closed (Pasca FHO)"  Value="Closed" />
            </Items>
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Alokasi</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLAlokasi" runat="server" ValueType="System.String" Width="250px" 
            ClientInstanceName="DDLAlokasi" Theme="MetropolisBlue">
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
        <dx:ASPxDateEdit ID="PrdAwal" runat="server" Width="250px"
            DisplayFormatString="dd-MMM-yyyy" 
            Theme="MetropolisBlue">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>        
    </td>
    <td>s.d.</td>
    <td>
        <dx:ASPxDateEdit ID="PrdAkhir" runat="server" Width="250px"
            DisplayFormatString="dd-MMM-yyyy" 
            Theme="MetropolisBlue">
            <DateRangeSettings StartDateEditID="PrdAwal" />
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>
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
</div>

</asp:Content>