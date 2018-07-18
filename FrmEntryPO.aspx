<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmEntryPO.aspx.vb" Inherits="AIS.FrmEntryPO" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

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
<div>
    <dx:ASPxPopupControl ID="ErrMsg" runat="server" CloseAction="CloseButton" CloseOnEscape="True" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ErrMsg"
        HeaderText="Information" PopupAnimationType="None" EnableViewState="False" Width="500px" Theme="MetropolisBlue">
        <ClientSideEvents PopUp="function(s, e) { BtnClose.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <div style="text-align:center; font-size:large; font-family:Segoe UI Light;">
                    <asp:Label ID="LblErr" runat="server" Text=""></asp:Label>
                    <br /> <br />
                    <div align="center">
                        <dx:ASPxButton ID="BtnClose" runat="server" AutoPostBack="False" ClientInstanceName="BtnClose"
                            Text="OK" Theme="MetropolisBlue" Width="75px">
                            <ClientSideEvents Click="function(s, e) { ErrMsg.Hide();}" />
                        </dx:ASPxButton>
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div> <%--Message Box--%>
<div>
    <dx:ASPxPopupControl ID="PopEntry" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopEntry"
        HeaderText="Data Entry" PopupAnimationType="Fade" 
            Width="700px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue" AllowDragging="True">
        <ClientSideEvents Init="function(s, e) { DDLRap.Focus(); }" EndCallback="function(s, e) { PopEntry.Show(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div align="center">
                    <table>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TxtAction" runat="server" Text="" 
                                BorderColor="White" BorderStyle="None" ForeColor="White" Width="30px"></asp:TextBox>
                        </td>
                    </tr>       
                    <tr>
                        <td align="left">No.</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtNo" runat="server" Width="30px" 
                                ClientInstanceName="TxtNo" Enabled="false" CssClass="font1">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Alokasi</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxComboBox ID="DDLAlokasi" runat="server" ValueType="System.String" 
                                CssClass="font1" Width="450px" AutoPostBack="true" 
                                ClientInstanceName="DDLAlokasi" Theme="MetropolisBlue">
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Kode RAP</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxComboBox ID="DDLRap" runat="server" ValueType="System.String" 
                                CssClass="font1" Width="450px" 
                                ClientInstanceName="DDLRap" Theme="MetropolisBlue">
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">Uraian</td>
                        <td valign="top">:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxMemo ID="TxtUraian" runat="server" Height="100px" Width="445px" 
                                CssClass="font1" MaxLength="255">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Volume</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtVol" runat="server" DecimalPlaces="3" 
                                DisplayFormatString="{0:N3}" Number="0" MaxLength="10" Width="80px" 
                                CssClass="font1">
                            <SpinButtons ShowIncrementButtons="False"/>
                            </dx:ASPxSpinEdit>     
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Satuan</td>
                        <td style="width:7px;">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtUom" runat="server" Width="60px" 
                                ClientInstanceName="TxtUom" MaxLength="15">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="left">Harga Satuan</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtHrgSatuan" runat="server" DecimalPlaces="2" 
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px">
                            <SpinButtons ShowIncrementButtons="False"/>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="4" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSave1" runat="server" Text="SIMPAN" CausesValidation="false"
                                Theme="MetropolisBlue" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancel1" runat="server" Text="BATAL"
                                Theme="MetropolisBlue" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) { PopEntry.Hide();}" />
                            </dx:ASPxButton>   
                        </td>
                    </tr>
                    </table>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div> <%--Pop Entry--%>

<div style="font-family:Segoe UI Light">
<table>
<tr>
    <td style="font-size:30px; text-decoration:underline">
        <asp:Label ID="LblJudul" runat="server" Text="Purchase Order"></asp:Label>
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="LblAction" runat="server" Visible="False"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblJobNo" runat="server" Text="" Visible="false"></asp:Label>
    </td>    
    <td>
        <asp:Label ID="LblNoKO" runat="server" Text="" Visible="false"></asp:Label>
    </td>
     <td>
        <asp:Label ID="LblPPN" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblTglKO" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblMix" runat="server" Text="" Visible="false"></asp:Label>
    </td>
</tr>
</table>
</div> <%--Label--%>

<div class="font1">
<table>
<tr>    
    <td colspan="3" align="center" bgcolor="silver" style="height:20px; font-weight:bold">Penerima PO</td>   
    <td style="width:25px"></td>
    <td>Tgl PO</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="TglKO" runat="server" CssClass="font1"
            DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            Theme="MetropolisBlue">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxDateEdit>
        <asp:Label ID="LblTglKO1" runat="server" Text="1/1/1900" Visible="false"></asp:Label>
    </td>
</tr>
<tr>
    <td>ID</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLVendor" runat="server" ValueType="System.String" 
            CssClass="font1" Width="100%" 
            ClientInstanceName="DDLVendor" Theme="MetropolisBlue" 
            AutoPostBack="True">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>       
        </dx:ASPxComboBox>
    </td>
    <td></td>
    <td>No PO</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoKO" runat="server" Width="200px" CssClass="font1" Enabled="false">            
        </dx:ASPxTextBox>  
    </td>
</tr>
<tr>
    <td>Nama</td>
    <td>:</td>
    <td>       
        <dx:ASPxTextBox ID="TxtNama" runat="server" Width="100%" CssClass="font1" Enabled="false">            
        </dx:ASPxTextBox>        
    </td>
    <td></td>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtJob" runat="server" Width="200px" Enabled="False" CssClass="font1">
        </dx:ASPxTextBox>        
    </td> 
</tr>
<tr>
    <td rowspan="2" valign="top">Alamat</td>
    <td rowspan="2" valign="top">:</td>
    <td rowspan="2" valign="top">
        <dx:ASPxMemo ID="TxtAlamat" runat="server" Height="45px" Width="400px" Enabled="false"
            CssClass="font1">
        </dx:ASPxMemo>
    </td>
    <td></td>
    <td>No SPR</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLSPR" runat="server" ValueType="System.String" 
            CssClass="font1" Width="100%" 
            ClientInstanceName="DDLSPR" Theme="MetropolisBlue" 
            AutoPostBack="True">      
        </dx:ASPxComboBox>       
    </td> 
</tr>
<tr>
    <td></td>
    <td>Kategori</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtKategori" runat="server" Width="100%" CssClass="font1" Enabled="false">
        </dx:ASPxTextBox>     
        <%--<dx:ASPxComboBox ID="DDLKategori" runat="server" ValueType="System.String" 
            CssClass="font1" Width="250px" 
            ClientInstanceName="DDLKategori" TabIndex="2" Theme="MetropolisBlue" 
            AutoPostBack="True">
        </dx:ASPxComboBox>--%>
    </td>    
</tr>
<tr>   
    <td>Telepon</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtTelepon" runat="server" Width="100%" CssClass="font1" Enabled="false">            
        </dx:ASPxTextBox>        
    </td>
    <td></td>
    <td>Bidang Usaha</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtUsaha" runat="server" Width="100%" CssClass="font1" Enabled="false">
        </dx:ASPxTextBox>  
        <%--<dx:ASPxComboBox ID="DDLSubKategori" runat="server" ValueType="System.String" 
            CssClass="font1" Width="250px" 
            ClientInstanceName="DDLSubKategori" TabIndex="3" Theme="MetropolisBlue">
        </dx:ASPxComboBox>--%>
    </td>       
</tr>
<tr>
    <td>NPWP</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNPWP" runat="server" Width="100%" CssClass="font1" Enabled="false"></dx:ASPxTextBox>        
    </td>
    <td></td>
    <td colspan="3" align="center" bgcolor="silver" style="font-weight:bold">Lokasi Pengiriman</td> 
</tr>
<tr>
    <td>UP</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtUP" runat="server" Width="100%" CssClass="font1" Enabled="false"></dx:ASPxTextBox>  
    </td>
    <td></td>
    <td>Nama</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNamaKirim" runat="server" Width="100%" CssClass="font1" MaxLength="100">
        </dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td colspan="4"></td>
</tr>
<tr>    
    <td colspan="4"></td>
    <td rowspan="2" valign="top">Alamat</td>
    <td rowspan="2" valign="top">:</td>
    <td rowspan="2" valign="top">
        <dx:ASPxMemo ID="TxtAlamatKirim" runat="server" Height="45px" Width="400px" MaxLength="255"
            CssClass="font1">
        </dx:ASPxMemo>
    </td>
</tr>
<tr>
</tr>
<tr>
    <td colspan="4"></td>
    <td>Telepon</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtTeleponKirim" runat="server" Width="100%" 
            CssClass="font1" MaxLength="20">
        </dx:ASPxTextBox>  
    </td>
</tr>
<tr>    
    <td colspan="4"></td>
    <td colspan="3" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:5px;">
        <asp:CheckBox ID="CheckBoxMA" runat="server" Text="Material Approval" />&nbsp;
        <asp:CheckBox ID="CheckBoxRAP" runat="server" Text="RAP" />
    </td>
</tr>
<tr>
    <td colspan="7" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:5px;">
        <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN" 
            Theme="MetropolisBlue" Width="75px">
        </dx:ASPxButton>
        <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL" CausesValidation="False"
            Theme="MetropolisBlue" Width="75px">
        </dx:ASPxButton>          
    </td>
</tr>
</table>

<table>
    <tr>
        <td align="center" bgcolor="silver" style="height:20px; font-weight:bold;">O  R  D  E  R&nbsp; &nbsp;L  I  S  T</td>
    </tr>
    <tr>
        <td style="border: 2px solid #C0C0C0">
            <table>
                <tr>
                    <td style="padding-bottom:5px">
                        <dx:ASPxButton ID="BtnAdd" runat="server" Text="TAMBAH"
                            Theme="MetropolisBlue" Width="80px" AutoPostBack="False" 
                            CausesValidation="False">
                        </dx:ASPxButton>                       
                    </td>           
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GridData" runat="server" AutoGenerateColumns="False"               
                            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                            PageSize="20" ShowFooter="True" 
                            ShowHeaderWhenEmpty="True">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>              
                                <asp:BoundField DataField="NoUrut" HeaderText="No."  HeaderStyle-Width="35px" ItemStyle-Width = "35px" ItemStyle-HorizontalAlign="Center">     
                                </asp:BoundField>
                                <asp:BoundField DataField="KdRAP" HeaderText="Kode RAP" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                        
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Uraian" HeaderStyle-Width="550px" ItemStyle-Width = "550px">
                                    <ItemTemplate>
                                        <asp:Label ID="LblUraian" runat="server" Text='<%# Eval("Uraian").ToString().Replace(vbCRLF, "<br />") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Vol" HeaderText="Vol"  HeaderStyle-Width="80px" ItemStyle-Width = "80px" 
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N3}">
                                </asp:BoundField>
                                <asp:BoundField DataField="Uom" HeaderText="Sat" HeaderStyle-Width= "30px" ItemStyle-Width = "30px" ItemStyle-HorizontalAlign="Center">                        
                                </asp:BoundField>
                                <asp:BoundField DataField="HrgSatuan" HeaderText="Harga Satuan (Rp)" 
                                    HeaderStyle-Width="200px" ItemStyle-Width = "200px" DataFormatString="{0:N0}" Itemstyle-HorizontalAlign="right">
                                </asp:BoundField>                        
                                <asp:TemplateField HeaderText="Jumlah Harga (Rp)" HeaderStyle-Width="250px" ItemStyle-Width = "250px">
                                    <ItemTemplate>
                                        <asp:Label ID="LblJumlah" Text='<%# string.Format("{0:N0}",Eval("Vol") * Eval("HrgSatuan")) %>' runat="server"/>                                
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>                            
                                </asp:TemplateField>
                                <asp:BoundField DataField="Alokasi" HeaderText="Alokasi" 
                                    HeaderStyle-Width="30px" ItemStyle-Width = "30px">
                                    <HeaderStyle CssClass="hiddencol" />
                                    <ItemStyle CssClass="hiddencol" />
                                </asp:BoundField>  
                                <asp:ButtonField CommandName="BtnUpdate" Text="SELECT" HeaderStyle-Width="45px"/>                                          
                                <asp:ButtonField CommandName="BtnDelete" Text="DELETE" HeaderStyle-Width="45px"/>                                                                 
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
        </td>
    </tr>
</table>
        <table>
            <tr>
                <td style="width:25px"></td>
                <td style="width:60px"></td>
                <td style="width:630px; text-align:right; font-family:Tahoma; font-size:12px; font-weight:bold">Disc (%)</td>
                <td></td>
                <td style="width:30px"></td>
                <td>
                    <dx:ASPxSpinEdit ID="TxtDiscPersen" runat="server" DecimalPlaces="2" 
                        DisplayFormatString="{0:N2}" Number="0" MaxLength="6" Width="200px" 
                        CssClass="font1" HorizontalAlign="Right" AutoPostBack="True" NullText="0" MaxValue="100">
                    <SpinButtons ShowIncrementButtons="False"/>
                    </dx:ASPxSpinEdit>   
                </td>
                <td>
                    <dx:ASPxSpinEdit ID="TxtDiscNominal" runat="server" DecimalPlaces="0" 
                        DisplayFormatString="{0:N0}" Number="0" Width="230px" 
                        CssClass="font1" HorizontalAlign="Right" AutoPostBack="true" NullText="0">
                    <SpinButtons ShowIncrementButtons="False"/>
                    </dx:ASPxSpinEdit>   
                </td>
            </tr>          
            <tr>
                <td colspan="5"></td>
                <td style="width:200px; text-align:center; font-family:Tahoma; font-size:12px; font-weight:bold">PPN</td>
                <td>
                    <dx:ASPxSpinEdit ID="TxtPPN" runat="server" DecimalPlaces="0" 
                        DisplayFormatString="{0:N0}" Number="0" Width="230px" 
                        CssClass="font1" HorizontalAlign="Right" AutoPostBack="true" NullText="0">
                    <SpinButtons ShowIncrementButtons="False"/>
                    </dx:ASPxSpinEdit>   
                </td>
                <td>
                    <dx:ASPxCheckBox ID="CbOverride" runat="server" Text="Override PPN" Width="100px" Enabled="false" AutoPostBack="true">
                    </dx:ASPxCheckBox>
                </td>
            </tr>
            <tr>
                <td colspan="5"></td>
                <td style="width:200px; text-align:center; font-family:Tahoma; font-size:12px; font-weight:bold">Grand Total</td>
                <td>
                    <dx:ASPxTextBox ID="TxtTotal" runat="server" Width="230px" CssClass="font1" 
                        Enabled="false" HorizontalAlign="Right" Text="0"></dx:ASPxTextBox>  
                </td>
            </tr>  
        </table>
     <%--Grid Data--%>

<table>
<tr>
    <td colspan="7" align="center" bgcolor="silver" style="height:20px; font-weight:bold">Persyaratan & Sanksi</td>   
</tr>
<tr>
    <td colspan="3">1. Wajib memenuhi persyaratan Kesehatan & Keselamatan Kerja (K3)&nbsp;<asp:CheckBox 
            ID="CheckBoxK3" runat="server"/></td>
    <td style="width:25px"></td>
</tr>
<tr>
    <td colspan="3">2. Syarat Teknis</td>
    <td></td>
    <td colspan="3">5. Syarat Pembayaran</td>
</tr>
<tr>
    <td colspan="3">
        <dx:ASPxMemo ID="TxtSyaratTeknis" runat="server" Height="80px" Width="100%" MaxLength="255"
            CssClass="font1">
        </dx:ASPxMemo>        
    </td>
    <td></td>
    <td colspan="3">
        <table>
        <tr>
            <td>
                <asp:CheckBox ID="CBInvoice" runat="server" Text="Invoice/Kwitansi" />
            </td>
            <td></td>
            <td>
                <asp:CheckBox ID="CBFP" runat="server" Text="Faktur Pajak" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="CBSJ" runat="server" Text="Surat Jalan/Tanda Terima Lapangan" 
                    Width="219px"/>
            </td>
            <td></td>
            <td>
                <asp:CheckBox ID="CBBayar" runat="server" Text="Berita Acara Pembayaran" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:CheckBox ID="CBPO" runat="server" Text="Copy PO" />
            </td>
            <td></td>
            <td colspan="3">
                <asp:CheckBox ID="CBFisik" runat="server" 
                    Text="Berita Acara Opname Pekerjaan" />
            </td>
        </tr>
        </table>
    </td>
</tr>
<tr>
    <td colspan="3">3. Jadwal Pengiriman</td>
    <td></td>
    <td colspan="3">6. Sanksi</td>
</tr>
<tr>
    <td colspan="3">
        <dx:ASPxMemo ID="TxtJadwalPengiriman" runat="server" Height="80px" Width="100%" MaxLength="255"
            CssClass="font1">
        </dx:ASPxMemo>
    </td>
    <td></td>
    <td colspan="3">
        <dx:ASPxMemo ID="TxtSanksi" runat="server" Height="80px" Width="100%" MaxLength="255"
            CssClass="font1">
        </dx:ASPxMemo>
    </td>
</tr>
<tr>
    <td colspan="3">4. Jadwal Pembayaran</td>
    <td></td>
    <td colspan="3">7. Keterangan Lainnya</td>
</tr>
<tr>
    <td colspan="3">
        <dx:ASPxMemo ID="TxtJadwalPembayaran" runat="server" Height="80px" Width="100%" MaxLength="255"
            CssClass="font1">
        </dx:ASPxMemo>
    </td>
    <td></td>
    <td colspan="3">
        <dx:ASPxMemo ID="TxtKeterangan" runat="server" Height="80px" Width="100%" MaxLength="255"
            CssClass="font1">
        </dx:ASPxMemo>
    </td>
</tr>
</table>
</div>

</asp:Content>