<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmQueryPD.aspx.vb" Inherits="AIS.FrmQueryPD" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Query PD/PJ</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            Width="300px" ClientInstanceName="DDLJob" Theme="MetropolisBlue">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Alokasi</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLAlokasi" runat="server" ValueType="System.String" 
            Width="100%" ClientInstanceName="DDLAlokasi" Theme="MetropolisBlue">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxComboBox>
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

<table style="width: 100%">
<tr>
    <td>
        <dx:ASPxGridView ID="Grid" ClientInstanceName="Grid" runat="server" 
            KeyFieldName="NoPD" Theme="MetropolisBlue" AutoGenerateColumns="False" Width="100%" OnHeaderFilterFillItems="Grid_HeaderFilterFillItems"
            OnCustomUnboundColumnData="Grid_CustomUnboundColumnData">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="NoPD" Width="120" Caption="No PD">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TglPD" Width="100" Caption="Tgl PD">
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yyyy}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NoRef" Width="150" Caption="No Ref. Lapangan">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TipeForm" Width="70" Caption="Tipe Form" HeaderStyle-Wrap="True">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Deskripsi" Width="200" Caption="Deskripsi">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NoKO" Width="100" Caption="No. KO">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="SubTotal" Visible="false" />
                <dx:GridViewDataTextColumn FieldName="DiscAmount" Visible="false" />
                <dx:GridViewDataTextColumn FieldName="PPN" Visible="false" />
                <dx:GridViewDataTextColumn FieldName="TotalKO" Width="150" UnboundType="Decimal" Caption="Total KO (Rp)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NoTagihan" Width="150" Caption="No. Tagihan">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NoRek" Width="150" Caption="Rekening Penerima" HeaderStyle-Wrap="True">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AtasNama" Width="150" Caption="Atas Nama">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TotalPD" Width="150" Caption="Total PD (Rp)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TglBayar" Width="100" Caption="Tgl Bayar">
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yyyy}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="JenisTrf" Width="80" Caption="Via">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="BLEAmount" Width="150" Caption="Total Pembayaran (Rp)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RekId" Width="150" Caption="Rekening Pengirim" HeaderStyle-Wrap="True">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NoPJ" Width="120" Caption="No PJ">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TglPJ" Width="100" Caption="Tgl PJ">
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yyyy}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TotalPJ" Width="150" Caption="Total PJ (Rp)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Saldo" Width="150" Caption="Saldo (Rp)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <DetailRow>
                    <dx:ASPxPageControl ID="TabPage" runat="server" ActiveTabIndex="0" Theme="Default" Width="100%">
                    <TabPages>
                    <dx:TabPage Text="Uraian PD" ClientEnabled="true">
                    <ContentCollection>
                        <dx:ContentControl runat="server">
                        <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="detailGrid" runat="server" KeyFieldName="NoPD" Width="100%" 
                                Theme="MetropolisBlue" OnBeforePerformDataSelect="detailGrid_DataSelect" 
                                OnCustomUnboundColumnData="detailGrid_CustomUnboundColumnData"> 
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="KdRAP" Width="80" Caption="Kode RAP">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Uraian" Width="300" Caption="Uraian PD">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Vol" Width="80" Caption="Volume PD">
                                            <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                                            <PropertiesTextEdit DisplayFormatString="{0:N3}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Uom" Width="80" Caption="Uom" />
                                        <dx:GridViewDataTextColumn FieldName="HrgSatuan" Width="120" Caption="Hrg Satuan PD (Rp)" HeaderStyle-Wrap="True">
                                            <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                                            <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SubTotalPD" Width="120" UnboundType="Decimal" Caption="SubTotal PD (Rp)" HeaderStyle-Wrap="True">
                                            <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                                            <PropertiesTextEdit DisplayFormatString="{0:N0}" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PjUraian" Width="300" Caption="Uraian PJ">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="PjVol" Width="80" Caption="Volume PJ">
                                            <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                                            <PropertiesTextEdit DisplayFormatString="{0:N3}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Uom" Width="80" Caption="Uom" />
                                        <dx:GridViewDataTextColumn FieldName="PjHrgSatuan" Width="120" Caption="Hrg Satuan PJ (Rp)" HeaderStyle-Wrap="True">
                                            <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                                            <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="SubTotalPJ" Width="120" UnboundType="Decimal" Caption="SubTotal PJ (Rp)" HeaderStyle-Wrap="True">
                                            <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                                            <PropertiesTextEdit DisplayFormatString="{0:N0}" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" ShowFooter="true" HorizontalScrollBarMode="Visible" ShowHeaderFilterButton="true" />
                                    <SettingsBehavior ColumnResizeMode="Control" EnableCustomizationWindow="true" />
                                    <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Position="Right" PageSize="10">
                                        <PageSizeItemSettings Items="10, 20, 50" />
                                    </SettingsPager>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="SubTotalPD" SummaryType="Sum" />
                                        <dx:ASPxSummaryItem FieldName="SubTotalPJ" SummaryType="Sum" />
                                    </TotalSummary>
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                        </table>
                        </dx:ContentControl>
                    </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Invoice" ClientEnabled="true">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl1" runat="server">
                        <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="InvoiceGrid" runat="server" KeyFieldName="NoPD" Width="100%" 
                                Theme="MetropolisBlue" OnBeforePerformDataSelect="InvoiceGrid_DataSelect"> 
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
                                    <Settings ShowFilterRow="True" ShowFooter="true" HorizontalScrollBarMode="Visible" ShowHeaderFilterButton="true" />
                                    <SettingsBehavior ColumnResizeMode="Control" EnableCustomizationWindow="true" />
                                    <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Position="Right" PageSize="10">
                                        <PageSizeItemSettings Items="10, 20, 50" />
                                    </SettingsPager>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="Total" SummaryType="Sum" />
                                        <dx:ASPxSummaryItem FieldName="PaymentAmount" SummaryType="Sum" />
                                    </TotalSummary>
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                        </table>
                        </dx:ContentControl>
                    </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Pembayaran" ClientEnabled="true">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl2" runat="server">
                        <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="BLEGrid" runat="server" KeyFieldName="NoPD" Width="100%" 
                                Theme="MetropolisBlue" OnBeforePerformDataSelect="BLEGrid_DataSelect"> 
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="TglBayar" Width="100" Caption="Tgl Bayar">
                                            <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yyyy}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="AtasNama" Width="300" Caption="Nama Penerima">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NoRek" Width="120" Caption="Rekening Penerima">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Bank" Width="100" Caption="Bank Penerima" HeaderStyle-Wrap="True">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Amount" Width="150" Caption="Nilai Bayar (Rp)" HeaderStyle-Wrap="True">
                                            <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                                            <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="CaraBayar" Width="100" Caption="Media">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="JenisTrf" Width="100" Caption="Via">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NoCG" Width="100" Caption="No Cek/BG">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="RekId" Width="250" Caption="Rekening Pengirim">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NoKO" Width="100" Caption="No. KO">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="Keterangan" Width="500" Caption="Keterangan">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Settings ShowFilterRow="True" ShowFooter="true" HorizontalScrollBarMode="Visible" ShowHeaderFilterButton="true" />
                                    <SettingsBehavior ColumnResizeMode="Control" EnableCustomizationWindow="true" />
                                    <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Position="Right" PageSize="10">
                                        <PageSizeItemSettings Items="10, 20, 50" />
                                    </SettingsPager>
                                    <TotalSummary>
                                        <dx:ASPxSummaryItem FieldName="Amount" SummaryType="Sum" />
                                    </TotalSummary>
                                </dx:ASPxGridView>
                            </td>
                        </tr>
                        </table>
                        </dx:ContentControl>
                    </ContentCollection>
                    </dx:TabPage>
                    </TabPages>
                    </dx:ASPxPageControl>
                </DetailRow>
            </Templates>
            <Settings ShowFilterRow="true" ShowFooter="true" HorizontalScrollBarMode="Visible" ShowHeaderFilterButton="true" />  
            <SettingsPopup>
                <HeaderFilter Height="300" Width="450">
                    <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="768" MinHeight="300" />
                </HeaderFilter>
            </SettingsPopup>    
            <SettingsDetail ShowDetailRow="true" ExportMode="Expanded" />
            <SettingsBehavior ColumnResizeMode="Control" EnableCustomizationWindow="true" />
            <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Position="Right" PageSize="10">
                <PageSizeItemSettings Items="10, 20, 50" />
            </SettingsPager>
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="TotalPD" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="TotalPJ" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="BLEAmount" SummaryType="Sum" />
            </TotalSummary>
        </dx:ASPxGridView>
    </td>
</tr>
<tr>
    <td>
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" FileName="QueryPD" GridViewID="Grid">
        </dx:ASPxGridViewExporter>
    </td>
</tr>
</table>
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>

</asp:Content>
