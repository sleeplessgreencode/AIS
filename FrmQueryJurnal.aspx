<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmQueryJurnal.aspx.vb" Inherits="AIS.FrmQueryJurnal" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Query Jurnal Harian</div>

<div>
<table>
<tr>
<td>
    <dx:ASPxButton ID="BtnPreview" runat="server" Text="PREVIEW" Width="80px" 
        Theme="MetropolisBlue">
    </dx:ASPxButton>
</td>
</tr>
</table>

<table style="width: 100%">
<tr>
    <td>
        <dx:ASPxGridView ID="Grid" ClientInstanceName="Grid" runat="server" 
            KeyFieldName="NoJurnal" Theme="MetropolisBlue" AutoGenerateColumns="False" 
            Width="100%" OnHeaderFilterFillItems="Grid_HeaderFilterFillItems">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="JobNo" Width="90" Caption="Job No" FixedStyle="Left">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="JobNm" Width="150" Caption="Project" FixedStyle="Left" >
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NoJurnal" Width="150" Caption="No. Jurnal" FixedStyle="Left">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TglJurnal" Width="100" Caption="Tgl Jurnal" FixedStyle="Left">
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yyyy}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PC" Width="50" Caption="PC">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Site" Width="100" Caption="Site">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Member" Width="100" Caption="Member">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Nota" Width="100" Caption="Nota">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Identitas" Width="80" Caption="Identitas">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Nama" Width="200" Caption="Nama">
                    <Settings AutoFilterCondition="Contains" AllowHeaderFilter="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NoReg" Width="100" Caption="No. Reg Int">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AccNo" Width="100" Caption="Account">
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AccName" Width="200" Caption="Account Deskripsi">
                    <Settings AutoFilterCondition="Contains" AllowHeaderFilter="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataMemoColumn FieldName="Uraian" Width="300" Caption="Uraian">
                    <Settings AutoFilterCondition="Contains" AllowHeaderFilter="False" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataTextColumn FieldName="DK" Width="50" Caption="DK" >
                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Debet" Width="150" Caption="Debet" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Kredit" Width="150" Caption="Kredit" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DebetBalance" Width="150" Caption="D_Balance" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="KreditBalance" Width="150" Caption="K_Balance" HeaderStyle-Wrap="True">
                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataMemoColumn FieldName="ApprovedBy" Width="150" Caption="Approved By">
                    <Settings AutoFilterCondition="Contains" AllowHeaderFilter="False" />
                </dx:GridViewDataMemoColumn>
                <dx:GridViewDataTextColumn FieldName="TimeApproved" Width="120" Caption="Approved On">
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yy HH:mm}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <GroupSummary>
                <dx:ASPxSummaryItem FieldName="Debet" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="Kredit" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="DebetBalance" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="KreditBalance" SummaryType="Sum" />
            </GroupSummary>
            <Settings ShowFilterRow="true" ShowFooter="true" ShowGroupPanel="true" HorizontalScrollBarMode="Visible" ShowHeaderFilterButton="true" />  
            <SettingsPopup>
                <HeaderFilter Height="300" Width="450">
                    <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="768" MinHeight="300" />
                </HeaderFilter>
            </SettingsPopup>    
            <SettingsBehavior ColumnResizeMode="Control" EnableCustomizationWindow="true" EnableRowHotTrack="true" />
            <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Position="Right" PageSize="50">
                <PageSizeItemSettings Items="10, 20, 50" />
            </SettingsPager>
            <Styles>
                <RowHotTrack BackColor="#4796CE"></RowHotTrack>
            </Styles>
            <TotalSummary>
                <dx:ASPxSummaryItem FieldName="Debet" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="Kredit" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="DebetBalance" SummaryType="Sum" />
                <dx:ASPxSummaryItem FieldName="KreditBalance" SummaryType="Sum" />
            </TotalSummary>
        </dx:ASPxGridView>
    </td>
</tr>
</table>
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>

</asp:Content>