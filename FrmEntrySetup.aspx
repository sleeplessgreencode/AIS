<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmEntrySetup.aspx.vb" Inherits="AIS.FrmEntrySetup" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    $(function () {
        $("[id*=GridData] td").hover(function () {
            $("td", $(this).closest("tr")).addClass("hover_row");
        }, function () {
            $("td", $(this).closest("tr")).removeClass("hover_row");
        });
    });
</script>    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="font1">
<table>
<tr>
    <td colspan="4" style="border-bottom:2px; border-bottom-style:solid; border-bottom-color:Black; padding-bottom:5px;font-size:30px; text-decoration:underline; font-family:Segoe UI Light">Setup Akses</td>
    <td colspan="3" align="right" style="border-bottom:2px; border-bottom-style:solid; border-bottom-color:Black; padding-bottom:5px;">
        <asp:TextBox ID="TxtAction" runat="server" Width="35px"  Visible="False" 
            Text="NEW"></asp:TextBox>
        <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN" 
            Width="75px" Theme="MetropolisBlue" TabIndex="23">
        </dx:ASPxButton>     
        <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL" 
            Theme="MetropolisBlue" Width="75px" TabIndex="24" CausesValidation="False">
        </dx:ASPxButton>          
    </td>
</tr>
<tr>
    <td>User ID</td>
    <td>:</td>
    <td> 
        <dx:ASPxTextBox ID="TxtUserID" runat="server" Width="250px" MaxLength="20" TabIndex="1">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxTextBox>
    </td>    
    <td></td>
    <td>User Name</td>
    <td>:</td>
    <td> 
        <dx:ASPxTextBox ID="TxtUserName" runat="server" Width="250px" MaxLength="30" 
            TabIndex="2">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxTextBox>
    </td>    
</tr>
<tr>
    <td>Password</td>
    <td>:</td>
    <td> 
        <dx:ASPxTextBox ID="TxtPassword" runat="server" Width="250px" MaxLength="15" TabIndex="3" Password="True">            
        </dx:ASPxTextBox>
    </td>    
    <td></td>
    <td>Confirm Password</td>
    <td>:</td>
    <td> 
        <dx:ASPxTextBox ID="TxtConfirmPw" runat="server" Width="250px" MaxLength="15" TabIndex="4" Password="True">            
        </dx:ASPxTextBox>
    </td>   
</tr>
<tr>
    <td colspan="2"></td>
    <td>*Kosongkan jika tidak ingin merubah password</td>
    <td></td>
    <td colspan="2"></td>
    <td></td>
</tr>
<tr><td></td></tr>
<tr>
<td colspan="7">
<dx:ASPxPageControl ID="TabPage" runat="server" ActiveTabIndex="0" 
        Theme="MetropolisBlue">
    <TabPages>
        <dx:TabPage Text="Akses Menu" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl1" runat="server">
                <table>
                <tr>
                    <td colspan="7" align="left" style="padding-top:10px">    
                        <dx:ASPxButton ID="BtnAllMenu" runat="server" Text="PILIH SEMUA MENU" 
                            Width="75px" Theme="MetropolisBlue">
                        </dx:ASPxButton>  
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="center" colspan="7" style="background-color:Silver; height:20px; font-weight:bold" >AKSES MENU BASIC DATA</td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbAlokasi" runat="server" Text="Alokasi" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbBank" runat="server" Text="Bank" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbRekening" runat="server" Text="Rekening Pengirim" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbTipeForm" runat="server" Text="Tipe Form" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbVendor" runat="server" Text="Vendor" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbKategori" runat="server" Text="Kategori" />
                    </td>    
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbProposal" runat="server" Text="Proposal" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbJob" runat="server" Text="Job" />
                    </td>    
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbNPWP" runat="server" Text="NPWP" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                    </td>    
                </tr>
                <tr>
                    <td align="center" colspan="7" style="background-color:Silver; height:20px; font-weight:bold" >AKSES MENU ENTRY</td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbRAP" runat="server" Text="RAP" />
                    </td>    
                </tr>
                <tr>
                    <td></td>
                    <td colspan="6" style="background-color:#FFFFCC; height:20px; font-weight:bold">PROCUREMENT</td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbEntrySPR" runat="server" Text="Permintaan Material/Alat" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbApprovalSPR" runat="server" Text="Approval Permintaan Material/Alat" />
                    </td>
                    <td></td>    
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbKO" runat="server" Text="Kontrak/Purchase Order" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbApprovalKO" runat="server" Text="Approval Kontrak/Purchase Order" />
                    </td>
                    <td></td>    
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbInvoice" runat="server" Text="Invoice" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbClosingKO" runat="server" Text="Penutupan Kontrak" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbKOAddendum" runat="server" Text="KO Addendum" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbTrackingKO" runat="server" Text="Tracking KO" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>         
                         <asp:CheckBox ID="CbCancelPO" runat="server" Text="Pembatalan KO" />               
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="6" style="background-color:#FFFFCC; height:20px; font-weight:bold" >PD/PJ</td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbPD" runat="server" Text="Permintaan Dana" />
                    </td>
                    <td></td>    
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbApprovalPD_KK" runat="server" Text="Approval Permintaan Dana (KK)" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbApprovalPD_KT" runat="server" Text="Approval Permintaan Dana (KT)" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbApprovalPD_TBP" runat="server" Text="Approval Permintaan Dana (TBP)" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbApprovalPD_DP" runat="server" Text="Approval Permintaan Dana (DP)" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbApprovalPD_DK" runat="server" Text="Approval Permintaan Dana (DK)" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbRejectPD" runat="server" Text="Reject Permintaan Dana" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbPJ" runat="server" Text="Pertanggungjawaban Dana" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbApprovalPJ" runat="server" Text="Approval Pertanggungjawaban Dana (KK)" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbPaymentPD" runat="server" Text="Pembayaran Permintaan Dana" />
                    </td>
                    <td></td> 
                    <td colspan="2"></td>                    
                </tr>
                <tr>
                    <td></td>
                    <td colspan="6" style="background-color:#CCCCCC; height:20px; font-weight:bold" ></td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbRPPM" runat="server" Text="Progress Fisik" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbRPPM1" runat="server" Text="RPPM" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbTermin" runat="server" Text="Penerimaan Termin" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbRencanaTermin" runat="server" Text="Rencana Termin" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbApprovalTermin" runat="server" Text="Approval Penerimaan Termin" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="7" style="background-color:Silver; height:20px; font-weight:bold" >AKSES MENU ACCOUNTING</td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbCOA" runat="server" Text="Chart Of Account" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbGlReff" runat="server" Text="GL Reference" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbIdentitas" runat="server" Text="Resume Identitas" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbJurnalEntry" runat="server" Text="Entry Jurnal Harian" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbJurnalApproval" runat="server" Text="Approval Jurnal Harian" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbExportJurnal" runat="server" Text="Export Jurnal Harian" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbQueryJurnal" runat="server" Text="Query Jurnal Harian" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="7" style="background-color:Silver; height:20px; font-weight:bold" >AKSES MENU REPORT</td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="6" style="background-color:#FFFFCC; height:20px; font-weight:bold">PROCUREMENT</td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbRekapKO" runat="server" Text="Summary Rekap KO" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbTrackKO" runat="server" Text="Tracking KO" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbInvReceipt" runat="server" Text="Rekap Invoice Supplier" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbItemPrice" runat="server" Text="Daftar Harga" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbQueryAP" runat="server" Text="Query Hutang KO" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbRAPKO" runat="server" Text="Penyerapan RAP Fisik dengan KO" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="6" style="background-color:#FFFFCC; height:20px; font-weight:bold">PD/PJ</td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbDPPD" runat="server" Text="Daftar Permintaan & Pertanggungjawaban Dana" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbRekapPayment" runat="server" Text="Rekap Pembayaran" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbSerapRAP" runat="server" Text="Penyerapan RAP" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbQueryPD" runat="server" Text="Query PD" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="6" style="background-color:#CCCCCC; height:20px; font-weight:bold" ></td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbRealisasiTermin" runat="server" Text="Realisasi Penerimaan Termin" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbSRPengeluaran" runat="server" Text="Summary Rekap Pengeluaran" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbCashFlow" runat="server" Text="Cash Flow" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbQueryTermin" runat="server" Text="Query Termin" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbPiuProgress" runat="server" Text="Piutang Progress" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>                    
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="6" style="background-color:#FFFFCC; height:20px; font-weight:bold">ACCOUNTING</td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbResumePK" runat="server" Text="Resume Posisi Keuangan & L/R Komprehensif" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbLapKeu" runat="server" Text="Laporan Posisi Keuangan (Neraca)" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbLR" runat="server" Text="Laporan Laba Rugi Komprehensif" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbBukuBesar" runat="server" Text="Buku Besar" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbBukuTambahan" runat="server" Text="Buku Tambahan" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbNeracaMutasi" runat="server" Text="Neraca Mutasi" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbNotaAkuntansi" runat="server" Text="Nota Akuntansi" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="center" colspan="7" style="background-color:Silver; height:20px; font-weight:bold" >AKSES MENU TOOLS</td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbTrackPD" runat="server" Text="Tracking PD" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbOverrideSaldo" runat="server" Text="Override Saldo" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbSetup" runat="server" Text="Setup Akses" />
                    </td>
                    <td></td>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbPasswd" runat="server" Text="Change Password" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>
                        <asp:CheckBox ID="CbMessage" runat="server" Text="Broadcast Notification" />
                    </td>
                    <td></td>
                </tr>
                </table>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Akses Alokasi" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl2" runat="server">
                <table>
                <tr>
                    <td align="left" style="padding-top:10px">
                        <dx:ASPxButton ID="BtnAllAlokasi" runat="server" Text="PILIH SEMUA ALOKASI" 
                            Width="75px" Theme="MetropolisBlue">
                        </dx:ASPxButton>  
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False"               
                            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                            PageSize="50" ShowFooter="True" 
                            ShowHeaderWhenEmpty="True">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>     
                                <asp:TemplateField HeaderText="" HeaderStyle-Width="20px" ItemStyle-Width = "20px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CbSelect" runat="server" Checked='<%# GetValue(Eval("IsChecked")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>         
                                <asp:BoundField DataField="Alokasi" HeaderText="Alokasi"  HeaderStyle-Width="80px" ItemStyle-Width = "80px" ItemStyle-HorizontalAlign="Center">     
                                </asp:BoundField>
                                <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" HeaderStyle-Width="400px" ItemStyle-Width = "400px">                        
                                </asp:BoundField>                                                                               
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="False" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView> 
                    </td>
                </tr>
                </table>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Akses Job" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl3" runat="server">
                <table>
                <tr>
                    <td align="left" style="padding-top:10px">
                        <dx:ASPxButton ID="BtnAllJob" runat="server" Text="PILIH SEMUA JOB" 
                            Width="75px" Theme="MetropolisBlue">
                        </dx:ASPxButton>  
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridData" runat="server" AutoGenerateColumns="False"               
                            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                            PageSize="50" ShowFooter="True" 
                            ShowHeaderWhenEmpty="True">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>     
                                <asp:TemplateField HeaderText="" HeaderStyle-Width="20px" ItemStyle-Width = "20px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CbSelect" runat="server" Checked='<%# GetValue(Eval("IsChecked")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>         
                                <asp:BoundField DataField="JobNo" HeaderText="Job No."  HeaderStyle-Width="100px" ItemStyle-Width = "100px" ItemStyle-HorizontalAlign="Center">     
                                </asp:BoundField>
                                <asp:BoundField DataField="JobNm" HeaderText="Nama Proyek" HeaderStyle-Width="400px" ItemStyle-Width = "400px">                        
                                </asp:BoundField>                                                                               
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="False" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView> 
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