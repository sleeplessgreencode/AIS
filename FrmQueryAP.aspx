<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmQueryAP.aspx.vb" Inherits="AIS.FrmQueryAP" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Query Hutang KO</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            Width="300px" ClientInstanceName="DDLJob" Theme="MetropolisBlue">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Vendor</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLVendor" runat="server" ValueType="System.String" 
            Width="300px" ClientInstanceName="DDLVendor" Theme="MetropolisBlue">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Status</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLStatus" runat="server" ValueType="System.String" 
            Width="300px" ClientInstanceName="DDLStatus" Theme="MetropolisBlue">
            <Items>
                <dx:ListEditItem Selected="true" Text="All" Value="All" />
                <dx:ListEditItem Text="Outstanding KO" Value="Outstanding KO" />
                <dx:ListEditItem Text="Outstanding Invoice" Value="Outstanding Invoice" />
                <dx:ListEditItem Text="Settle KO" Value="Settle KO" />
                <dx:ListEditItem Text="Settle Invoice" Value="Settle Invoice" />
            </Items>
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
                <dx:GridViewDataTextColumn FieldName="TotalKO" Width="150" Caption="Amount KO (Rp)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PaymentKO" Width="150" Caption="Payment KO (Rp)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RemainingKO" Width="150" Caption="Remaining KO (Rp)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TotalInv" Width="150" Caption="Amount Invoice (Rp)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PaymentInv" Width="150" Caption="Payment Invoice (Rp)" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RemainingInv" Width="150" Caption="Remaining Invoice (Rp)" HeaderStyle-Wrap="True" >
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <Templates>
                <DetailRow>
                    <dx:ASPxGridView ID="InvoiceGrid" runat="server" KeyFieldName="NoKO;InvNo" Width="100%" 
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
                        <Templates>
                            <DetailRow>
                                <dx:ASPxGridView ID="BLEGrid" runat="server" Width="100%" 
                                Theme="MetropolisBlue" OnBeforePerformDataSelect="BLEGrid_DataSelect"> 
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="InvNo" Width="150" Caption="Invoice">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="NoPD" Width="150" Caption="No PD">
                                            <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="BLEAmount" Width="150" Caption="Payment Amount (Rp)" HeaderStyle-Wrap="True">
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
                                        <dx:ASPxSummaryItem FieldName="BLEAmount" SummaryType="Sum" />
                                    </TotalSummary>
                                </dx:ASPxGridView>
                            </DetailRow>
                        </Templates>
                        <Settings ShowFilterRow="True" ShowFooter="true" HorizontalScrollBarMode="Visible" ShowHeaderFilterButton="true" />
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
                            <dx:ASPxSummaryItem FieldName="Total" SummaryType="Sum" />
                            <dx:ASPxSummaryItem FieldName="PaymentAmount" SummaryType="Sum" />
                        </TotalSummary>
                    </dx:ASPxGridView>
                </DetailRow>
            </Templates>
            <GroupSummary>
                <dx:ASPxSummaryItem FieldName="RemainingInv" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="RemainingKO" SummaryType="Sum" />
            </GroupSummary>
            <Settings ShowFilterRow="True" ShowFooter="true" ShowGroupPanel="true" HorizontalScrollBarMode="Visible" ShowHeaderFilterButton="true" />
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
                <dx:ASPxSummaryItem FieldName="TotalInv" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="TotalKO" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="PaymentKO" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="PaymentInv" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="RemainingInv" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="RemainingKO" SummaryType="Sum" />
            </TotalSummary>
        </dx:ASPxGridView>
    </td>
</tr>
<tr>
    <td>
        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" FileName="QueryAP" GridViewID="Grid">
        </dx:ASPxGridViewExporter>
    </td>
</tr>
</table>
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>
</asp:Content>
