<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmExportJurnal.aspx.vb" Inherits="AIS.FrmExportJurnal" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Export Jurnal Harian</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            Width="200px" ClientInstanceName="DDLJob" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Periode</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="TxtTgl1" ClientInstanceName="TxtTgl1" runat="server" 
            Theme="MetropolisBlue" DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            AutoPostBack="True">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>
    </td>
    <td></td>
    <td align="center">s.d.</td>
    <td></td>
    <td>
        <dx:ASPxDateEdit ID="TxtTgl2" ClientInstanceName="TxtTgl2" runat="server" 
            Theme="MetropolisBlue" DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            AutoPostBack="True">
            <DateRangeSettings StartDateEditID="TxtTgl1"></DateRangeSettings>
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>
    </td>
</tr>
<tr>
    <td>Site</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLSite" runat="server" ValueType="System.String" 
            Width="200px" ClientInstanceName="DDLSite" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>    
    <td colspan="2"></td>
    <td>
        <dx:ASPxButton ID="BtnExport" runat="server" Text="EXPORT" 
            Theme="MetropolisBlue" Width="75px">
        </dx:ASPxButton>
    </td>
</tr>
</table>

<table width="100%">
<tr>
<td>  
    <dx:ASPxGridView ID="Grid" ClientInstanceName="Grid" runat="server" 
        KeyFieldName="NoKO" Theme="MetropolisBlue" AutoGenerateColumns="False" Width="100%"
        OnCustomUnboundColumnData="Grid_CustomUnboundColumnData">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Member" Width="80" Caption="Member" />
            <dx:GridViewDataTextColumn FieldName="Nota" Width="50" Caption="Jenis" />
            <dx:GridViewDataTextColumn FieldName="LedgerNo" Width="50" Caption="No." />
            <dx:GridViewDataTextColumn FieldName="NoJurnal" Width="120" Caption="No. Jurnal">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="TglJurnal" Width="100" Caption="Tgl Jurnal">
                <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yyyy}"></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Bulan" Width="50" Caption="Bulan" />
            <dx:GridViewDataTextColumn FieldName="Tahun" Width="50" Caption="Tahun" />
            <dx:GridViewDataTextColumn FieldName="Site" Width="80" Caption="Site" />
            <dx:GridViewDataTextColumn FieldName="Nama" Width="200" Caption="Identitas">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Identitas" Width="80" Caption="Identitas" />
            <dx:GridViewDataTextColumn FieldName="PC" Width="80" Caption="PC" />
            <dx:GridViewDataTextColumn FieldName="Uraian" Width="300" Caption="Uraian">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="NoReg" Width="100" Caption="No. Reg Int">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="AccNo" Width="100" Caption="Account">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="PC" Width="50" Caption="PC" />
            <dx:GridViewDataTextColumn FieldName="AccName" Width="200" Caption="Account Deskripsi">
                <Settings AutoFilterCondition="Contains" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Debet" Width="150" Caption="Debet" HeaderStyle-Wrap="True">
                <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Kredit" Width="150" Caption="Kredit" HeaderStyle-Wrap="True">
                <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="DebetBalance" Width="150" Caption="D_Balance" HeaderStyle-Wrap="True">
                <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="KreditBalance" Width="150" Caption="K_Balance" HeaderStyle-Wrap="True">
                <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Deviasi" Width="120" UnboundType="Decimal" Caption="Deviasi" HeaderStyle-Wrap="True">
                <PropertiesTextEdit DisplayFormatString="{0:N0}" />
            </dx:GridViewDataTextColumn>
        </Columns>
        <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowFooter="true" HorizontalScrollBarMode="Visible" />
        <SettingsBehavior ColumnResizeMode="Control" />
        <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Position="Right" PageSize="20">
            <PageSizeItemSettings Items="10, 20, 50" />
        </SettingsPager>
    </dx:ASPxGridView>
</td>    
</tr>      
<tr>
<td>
    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" FileName="ExportJurnal" GridViewID="Grid">
    </dx:ASPxGridViewExporter>
</td>
</tr>      
</table>   
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>
</asp:Content>
