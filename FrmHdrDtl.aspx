<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmHdrDtl.aspx.vb" Inherits="AIS.FrmHdrDtl" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Header Detail</div>

<div>
<table>
<tr>
<td>
    <dx:ASPxButton ID="BtnPreview" runat="server" Text="PREVIEW" Width="80px" 
        Theme="MetropolisBlue">
    </dx:ASPxButton>
    <dx:ASPxButton ID="BtnExport" runat="server" Text="EXPORT" Width="80px" 
        Theme="MetropolisBlue">
    </dx:ASPxButton>
</td>
</tr>
</table>

<table style="width: 100%">
<tr>
<td>
    <dx:ASPxGridView ID="Grid" ClientInstanceName="Grid" runat="server" EnableCallBacks="false" 
        KeyFieldName="NoKO" Theme="MetropolisBlue" AutoGenerateColumns="False" Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="JobNo" Width="90" Caption="Job No" >
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="JobNm" Width="120" Caption="Project">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="NoKO" Width="100" Caption="No. KO">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TglKO" Width="100" Caption="Tgl KO">
                <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yyyy}"></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="KategoriId" Width="180" Caption="Tipe KO">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="VendorNm" Width="180" Caption="Vendor">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsBehavior ColumnResizeMode="Control" EnableCustomizationWindow="true" AllowFocusedRow="true" />   
        <ClientSideEvents FocusedRowChanged="function(s, e) {InvoiceGrid.PerformCallback(s.GetFocusedRowIndex()); }" />     
        <Settings ShowFilterRow="True" ShowFooter="true" ShowGroupPanel="true" HorizontalScrollBarMode="Visible" ShowHeaderFilterButton="true" />
        <SettingsPopup>
            <HeaderFilter Height="300" Width="450">
                <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="768" MinHeight="300" />
            </HeaderFilter>
        </SettingsPopup>  
        <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Position="Right" PageSize="10">
            <PageSizeItemSettings Items="10, 20, 50" />
        </SettingsPager>
    </dx:ASPxGridView>
</td>
</tr>
</table>

<table>
<tr>
<td>
    <dx:ASPxGridView ID="InvoiceGrid" runat="server" ClientInstanceName="InvoiceGrid" KeyFieldName="NoKO;InvNo" 
    Width="100%" Theme="MetropolisBlue"> 
        <Columns>
            <dx:GridViewDataTextColumn FieldName="NoKO" Width="100" Caption="No. KO">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="InvNo" Width="150" Caption="Invoice">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="InvDate" Width="100" Caption="Tgl Invoice">
                <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yyyy}"></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="DueDate" Width="100" Caption="Due Date">
                <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yyyy}"></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Total" Width="150" Caption="Total Invoice (Rp)" HeaderStyle-Wrap="True">
                <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PPN" Width="150" Caption="PPN (Rp)" HeaderStyle-Wrap="True">
                <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FPNo" Width="150" Caption="No. Faktur Pajak" HeaderStyle-Wrap="True">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="FPDate" Width="100" Caption="Tgl Faktur Pajak" HeaderStyle-Wrap="True">
                <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yyyy}"></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PaymentAmount" Width="150" Caption="Payment Amount (Rp)" HeaderStyle-Wrap="True">
                <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
</td>
</tr>
</table>
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>
</asp:Content>
