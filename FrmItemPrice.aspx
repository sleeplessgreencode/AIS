<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmItemPrice.aspx.vb" Inherits="AIS.FrmItemPrice" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Daftar Harga</div>
<div>

<table style="width: 100%">
<tr>
    <td>
        <dx:ASPxGridView ID="Grid" ClientInstanceName="Grid" runat="server" 
            KeyFieldName="SINo" Width="100%" Theme="MetropolisBlue" 
            AutoGenerateColumns="False" style="font-size: 9pt">
            <Columns>
                <dx:GridViewDataColumn FieldName="Uraian" Width="250px" Caption="Uraian" Settings-AutoFilterCondition="Contains" />
                <dx:GridViewDataColumn FieldName="Uom" Width="50px" Caption="U.O.M" />
                <dx:GridViewDataTextColumn FieldName="HrgSatuan" 
                    PropertiesTextEdit-DisplayFormatString="{0:N0}" Width="80px"  
                    Caption="Harga Satuan" />
                <dx:GridViewDataColumn FieldName="VendorNm" Width="150px" Caption="Vendor" Settings-AutoFilterCondition="Contains" />
                <dx:GridViewDataColumn FieldName="NoKO" Width="100px" Caption="No KO" />
                <dx:GridViewDataTextColumn FieldName="TglKO" PropertiesTextEdit-DisplayFormatString="{0:dd-MMM-yyyy}" 
                    Width="100px" Caption="Tgl KO" />
                <dx:GridViewDataColumn FieldName="JobNm" Width="200px" Caption="Nama Proyek" Settings-AutoFilterCondition="Contains" />
            </Columns>
            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowFooter="true" />
            <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Position="Right" PageSize="50">
                <PageSizeItemSettings Items="10, 20, 50" />
            </SettingsPager>
        </dx:ASPxGridView>
    </td>
</tr>
</table>
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>
</asp:Content>
