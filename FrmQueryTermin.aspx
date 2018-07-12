<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmQueryTermin.aspx.vb" Inherits="AIS.FrmQueryTermin" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Query Termin</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td colspan="3">
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" Width="300px"
            ClientInstanceName="DDLJob" Theme="MetropolisBlue">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
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
        <dx:ASPxButton ID="BtnPreview" runat="server" Text="PREVIEW" Width="80px" 
            Theme="MetropolisBlue">
        </dx:ASPxButton>
        <dx:ASPxButton ID="BtnExport" runat="server" Text="EXPORT" Width="80px" 
            Theme="MetropolisBlue">
        </dx:ASPxButton>
    </td>
</tr>
</table>

<table width="100%">
<tr>
<td>
    <dx:ASPxGridView ID="Grid" ClientInstanceName="Grid" runat="server" 
        KeyFieldName="LedgerNo" Theme="MetropolisBlue" AutoGenerateColumns="False" Width="100%"
        OnHeaderFilterFillItems="Grid_HeaderFilterFillItems" OnCustomUnboundColumnData="Grid_CustomUnboundColumnData">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="JobNo" Width="90" Caption="Job No" >
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="JobNm" Width="200" Caption="Project">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Jenis" Width="100" Caption="Jenis">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Bulan" Width="100" Caption="Bulan" HeaderStyle-Wrap="True" Visible="false">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="NmBulan" Width="100" Caption="Bulan" HeaderStyle-Wrap="True" UnboundType="String">
                <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewBandColumn Caption="Rencana">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Persentase1" Width="120" Caption="Persentase (%)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Netto1" Width="200" Caption="Netto (Rp)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewBandColumn>
            <dx:GridViewBandColumn Caption="Realisasi">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Persentase2" Width="120" Caption="Persentase (%)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N2}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Netto2" Width="200" Caption="Netto (Rp)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <HeaderStyle HorizontalAlign="Center" />
            </dx:GridViewBandColumn>
        </Columns>
        <GroupSummary>
            <dx:ASPxSummaryItem FieldName="Netto1" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="Netto2" SummaryType="Sum" />
        </GroupSummary>
        <Settings ShowFilterRow="True" ShowFooter="true" ShowGroupPanel="true" ShowHeaderFilterButton="true" HorizontalScrollBarMode="Visible" />
        <SettingsPopup>
            <HeaderFilter Height="300" Width="450">
                <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="768" MinHeight="300" />
            </HeaderFilter>
        </SettingsPopup>  
        <SettingsBehavior ColumnResizeMode="Control" EnableCustomizationWindow="true" AllowEllipsisInText="true" />
        <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Position="Right" PageSize="20">
            <PageSizeItemSettings Items="10, 20, 50" />
        </SettingsPager>
        <TotalSummary>
            <dx:ASPxSummaryItem FieldName="Netto1" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="Netto2" SummaryType="Sum" />
            <dx:ASPxSummaryItem FieldName="Persentase2" SummaryType="Sum" />
        </TotalSummary>
    </dx:ASPxGridView>
</td>    
</tr>    
</table>
<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" FileName="QueryTermin" GridViewID="Grid">
</dx:ASPxGridViewExporter>

<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>
</asp:Content>