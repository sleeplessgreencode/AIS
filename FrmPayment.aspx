<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmPayment.aspx.vb" Inherits="AIS.FrmPayment" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Pembayaran Permintaan Dana</div>

<div>
<table style="width: 100%">
<tr>
<td>
<dx:ASPxPageControl ID="TabPage" runat="server" ActiveTabIndex="0" 
        Theme="MetropolisBlue" Width="100%">
    <TabPages>
        <dx:TabPage Text="Yang Belum DiBayar" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl1" runat="server">
                <table style="width: 100%">
                <tr>
                    <td>
                        <dx:ASPxGridView ID="Grid" ClientInstanceName="Grid" runat="server" EnableCallBacks="false" 
                            KeyFieldName="NoPD" Theme="MetropolisBlue" AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <dx:GridViewCommandColumn Width="80">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="Payment" Text="PAYMENT" />
                                </CustomButtons>
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="JobNo" Width="70" Caption="Job No">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="JobNm" Width="150" Caption="Project" >
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NoPD" Width="150" Caption="No. PD" >
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TglPD" Width="100" Caption="Tgl PD"  >
                                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yyyy}" Width="150"></PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Alokasi" Width="70" Caption="Alokasi">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TipeForm" Width="60" Caption="Tipe Form"  HeaderStyle-Wrap="True">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NoKO" Width="80" Caption="No KO">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Deskripsi" Width="300" Caption="Deskripsi" >
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TotalPD" Width="125" Caption="TotalPD" >
                                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="ApprovedByDK" Width="150" Caption="Approved By DK">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TimeApprovedDK" Width="150" Caption="PD Approved On (DK)" HeaderStyle-Wrap="True" >
                                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yyyy HH:mm}"></PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>                                
                            </Columns>
                            <Settings ShowFilterRow="True" ShowFooter="true" HorizontalScrollBarMode="Visible" ShowHeaderFilterButton="true" />
                            <SettingsPopup>
                                <HeaderFilter Height="300" Width="450">
                                    <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="768" MinHeight="300" />
                                </HeaderFilter>
                            </SettingsPopup>  
                            <SettingsBehavior ColumnResizeMode="Control" EnableCustomizationWindow="true" />
                            <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Position="Right" PageSize="20">
                                <PageSizeItemSettings Items="10, 20, 50" />
                            </SettingsPager>
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="TotalPD" SummaryType="Sum" />
                            </TotalSummary>
                        </dx:ASPxGridView>
                    </td>    
                </tr>    
                </table>   
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Yang Sudah DiBayar" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl2" runat="server">
                <table style="width: 100%">
                <tr>
                    <td>  
                        <dx:ASPxGridView ID="Grid1" ClientInstanceName="Grid1" runat="server" EnableCallBacks="false" 
                            KeyFieldName="NoPD" Theme="MetropolisBlue" AutoGenerateColumns="False" Width="100%">
                            <Columns>       
                                <dx:GridViewCommandColumn Width="50">
                                <CustomButtons>
                                    <dx:GridViewCommandColumnCustomButton ID="Copy" Text="COPY" />
                                </CustomButtons>
                                </dx:GridViewCommandColumn>                         
                                <dx:GridViewDataTextColumn FieldName="LedgerNo" Width="50" Caption="Ledger No" Visible="false" />
                                <dx:GridViewDataTextColumn FieldName="TglBayar" Width="100" Caption="Tgl Bayar" >
                                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yyyy}" Width="150"></PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="TipeForm" Width="60" Caption="Tipe Form" HeaderStyle-Wrap="True">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Alokasi" Width="70" Caption="Alokasi">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NoPD" Width="150" Caption="No. PD">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="JobNm" Width="150" Caption="Project">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="JobNo" Width="70" Caption="Job No">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="AtasNama" Width="200" Caption="Nama Penerima">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NoRek" Width="150" Caption="Rekening Penerima">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Bank" Width="80" Caption="Bank Penerima" HeaderStyle-Wrap="True">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Amount" Width="150" Caption="Nilai (Rp)" >
                                    <Settings ShowFilterRowMenu="True" AllowHeaderFilter="False" />
                                    <PropertiesTextEdit DisplayFormatString="{0:N0}"></PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CaraBayar" Width="100" Caption="Media" UnboundType="String">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="JenisTrf" Width="100" Caption="Via" UnboundType="String">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NoCG" Width="90" Caption="No CEK/BG">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="RekId" Width="150" Caption="Rekening Pengirim">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NoKO" Width="90" Caption="No. KO" >    
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Keterangan" Width="300" Caption="Keterangan">
                                    <Settings AutoFilterCondition="Contains" HeaderFilterMode="CheckedList" />
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Settings ShowFilterRow="True" ShowFooter="true" HorizontalScrollBarMode="Visible" ShowHeaderFilterButton="true" />
                            <SettingsPopup>
                                <HeaderFilter Height="300" Width="450">
                                    <SettingsAdaptivity Mode="OnWindowInnerWidth" SwitchAtWindowInnerWidth="768" MinHeight="300" />
                                </HeaderFilter>
                            </SettingsPopup>  
                            <SettingsBehavior ColumnResizeMode="Control" EnableCustomizationWindow="true" />                 
                            <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Position="Right" PageSize="20">
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
</td>
</tr>
</table>
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>

</asp:Content>
