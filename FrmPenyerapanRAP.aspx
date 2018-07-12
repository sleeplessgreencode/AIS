<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmPenyerapanRAP.aspx.vb" Inherits="AIS.FrmPenyerapanRAP" %>
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
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Penyerapan RAP</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td colspan="2">
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" Width="250px" 
            ClientInstanceName="DDLJob" Theme="MetropolisBlue">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>     
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Alokasi</td>
    <td>:</td>
    <td colspan="2">
        <dx:ASPxComboBox ID="DDLAlokasi" runat="server" ValueType="System.String" Width="250px" 
            ClientInstanceName="DDLAlokasi" Theme="MetropolisBlue">
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
            <ClientSideEvents Click="function(s,e) { OpenNewTab(); }" />
        </dx:ASPxButton>  
    </td>
</tr>
</table>
</div>

</asp:Content>