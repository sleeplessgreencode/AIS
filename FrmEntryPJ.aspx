<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmEntryPJ.aspx.vb" Inherits="AIS.FrmEntryPJ" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
$(function () {
    $("[id*=GridPD] td").hover(function () {
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
</div>
<div>
    <dx:ASPxPopupControl ID="PopEntry" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopEntry"
        HeaderText="Data Entry" PopupAnimationType="Fade" 
            Width="1180px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue">
        <ClientSideEvents Init="function(s, e) {}" EndCallback="function(s, e) { PopEntry.Show(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div>
                    <table>
                    <tr>
                        <td colspan="4" style="font-size:30px; text-decoration:underline"> Permintaan Dana</td>
                        <td bgcolor="#c0c0c0"></td>
                        <td></td>
                        <td colspan="3" style="font-size:30px; text-decoration:underline"> Pertanggungjawaban Dana</td>
                    </tr>
                    <tr>
                        <td class="style1">
                            <asp:TextBox ID="TxtAction" runat="server" Text="" 
                                BorderColor="White" BorderStyle="None" ForeColor="White" Width="30px"></asp:TextBox>
                        </td>
                        <td colspan="3"></td>
                        <td bgcolor="#c0c0c0"></td>
                    </tr>       
                    <tr>
                        <td class="style1">No.</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxTextBox ID="TxtNo" runat="server" Width="30px" 
                                ClientInstanceName="TxtNo" Enabled="false" CssClass="font1">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td bgcolor="#c0c0c0"></td>
                        <td></td>
                        <td>No.</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxTextBox ID="TxtNo1" runat="server" Width="30px" 
                                ClientInstanceName="TxtNo1" Enabled="false" CssClass="font1" >
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="style1">Kode RAP</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxTextBox ID="TxtRAP" runat="server" Width="450px" 
                                ClientInstanceName="TxtRAP" Enabled="false" CssClass="font1">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td bgcolor="#c0c0c0"></td>
                        <td></td>
                        <td>Kode</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxTextBox ID="TxtRAP1" runat="server" Width="450px" 
                                ClientInstanceName="TxtRAP1" Enabled="false" CssClass="font1">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="style1">Uraian</td>
                        <td valign="top">:</td>
                        <td>
                            <dx:ASPxMemo ID="TxtUraian" runat="server" Height="100px" Width="450px" 
                                CssClass="font1" Enabled="false">
                            </dx:ASPxMemo>
                        </td>
                        <td></td>
                        <td bgcolor="#c0c0c0"></td>
                        <td></td>
                        <td valign="top">Uraian</td>
                        <td valign="top">:</td>
                        <td>
                            <dx:ASPxMemo ID="TxtUraian1" runat="server" Height="100px" Width="450px" 
                                CssClass="font1" TabIndex="1">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">Volume</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxSpinEdit ID="TxtVol" runat="server" DecimalPlaces="3" 
                                DisplayFormatString="{0:N3}" Number="0" MaxLength="10" Width="80px" 
                                CssClass="font1" Enabled="false">
                            <SpinButtons ShowIncrementButtons="False"/>
                            </dx:ASPxSpinEdit>     
                        </td>
                        <td></td>
                        <td bgcolor="#c0c0c0"></td>
                        <td></td>
                        <td>Volume</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxSpinEdit ID="TxtVol1" runat="server" DecimalPlaces="2" 
                                DisplayFormatString="{0:N2}" Number="0" MaxLength="10" Width="80px" 
                                TabIndex="2" CssClass="font1">
                            <SpinButtons ShowIncrementButtons="False"/>
                            </dx:ASPxSpinEdit>     
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">Satuan</td>
                        <td style="width:7px;">:</td>
                        <td>
                            <dx:ASPxTextBox ID="TxtUom" runat="server" Width="50px" 
                                ClientInstanceName="TxtUom" MaxLength="15" Enabled="false">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td bgcolor="#c0c0c0"></td>
                        <td></td>
                        <td>Satuan</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxTextBox ID="TxtUom1" runat="server" Width="50px" 
                                ClientInstanceName="TxtUom1" TabIndex="4" MaxLength="15" Enabled="false">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">Harga Satuan</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxSpinEdit ID="TxtHrgSatuan" runat="server" DecimalPlaces="2" 
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" 
                                Enabled="false">
                            <SpinButtons ShowIncrementButtons="False"/>
                            </dx:ASPxSpinEdit>
                        </td>
                        <td></td>
                        <td bgcolor="#c0c0c0"></td>
                        <td></td>
                        <td>Harga Satuan</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxSpinEdit ID="TxtHrgSatuan1" runat="server" DecimalPlaces="2" 
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" 
                                TabIndex="3">
                            <SpinButtons ShowIncrementButtons="False"/>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="9" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSave1" runat="server" Text="SIMPAN" CausesValidation="false"
                                Theme="MetropolisBlue" TabIndex="4" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancel1" runat="server" Text="BATAL"
                                Theme="MetropolisBlue" TabIndex="5" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) { PopEntry.Hide();}" />
                            </dx:ASPxButton>   
                        </td>
                    </tr>
                    </table>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div>
<div>
    <dx:ASPxPopupControl ID="PopEntry1" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopEntry1"
        HeaderText="Data Entry" PopupAnimationType="Fade" 
            Width="700px" PopupElementID="PopEntry1" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <div align="center">
                    <table>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TxtAction1" runat="server" Text="NEW" 
                                BorderColor="White" BorderStyle="None" ForeColor="White" Width="30px" 
                                Enabled="False" BackColor="White">
                            </asp:TextBox>
                        </td>
                    </tr>       
                    <tr>
                        <td align="left">No.</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtNo2" runat="server" Width="30px" 
                                ClientInstanceName="TxtNo2" Enabled="false" CssClass="font1">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Kode RAP</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxComboBox ID="DDLRap1" runat="server" ValueType="System.String" 
                                CssClass="font1" Width="450px" 
                                ClientInstanceName="DDLRap1" TabIndex="1" Theme="MetropolisBlue" 
                                SelectedIndex="0">
                                <Items>
                                    <dx:ListEditItem Selected="True" Text="Pengembalian Saldo" Value="C.00" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">Uraian</td>
                        <td valign="top">:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxMemo ID="TxtUraian2" runat="server" Height="100px" Width="445px" 
                                CssClass="font1" TabIndex="2" MaxLength="255">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Volume</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtVol2" runat="server" DecimalPlaces="3" 
                                DisplayFormatString="{0:N3}" Number="0" MaxLength="10" Width="80px" 
                                TabIndex="3" CssClass="font1" Enabled="false">
                            <SpinButtons ShowIncrementButtons="False"/>
                            </dx:ASPxSpinEdit>     
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Satuan</td>
                        <td style="width:7px;">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtUom2" runat="server" Width="60px" 
                                ClientInstanceName="TxtUom2" TabIndex="4" MaxLength="15" Enabled="false">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="left">Harga Satuan</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtHrgSatuan2" runat="server" DecimalPlaces="0" 
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" 
                                TabIndex="5">
                            <SpinButtons ShowIncrementButtons="False"/>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="4" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSave2" runat="server" Text="SIMPAN" CausesValidation="False"
                                Theme="MetropolisBlue" TabIndex="6" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancel2" runat="server" Text="BATAL"
                                Theme="MetropolisBlue" TabIndex="7" Width="80px" AutoPostBack="False" CausesValidation="False">
                                <ClientSideEvents Click="function(s, e) { PopEntry1.Hide();}" />
                            </dx:ASPxButton>   
                        </td>
                    </tr>
                    </table>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div>

<div style="font-family:Segoe UI Light">
<table>
<tr>
    <td style="font-size:30px; text-decoration:underline">
        <asp:Label ID="LblJudul" runat="server" Text="Pertanggungjawaban Dana"></asp:Label>
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
        <asp:Label ID="LblNoPD" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblOverride" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblAlokasi" runat="server" Text="" Visible="false"></asp:Label>
    </td>
</tr>
</table>
</div>

<div class="font1">
<table>
<tr>
    <td>No PD</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoPD" runat="server" Width="200px" Enabled="False" CssClass="font1"></dx:ASPxTextBox>        
    </td>
    <td></td>
    <td>No. PJ</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoPJ" runat="server" Width="100%" Enabled="False" CssClass="font1"></dx:ASPxTextBox>      
    </td>    
    <td colspan="5"></td>
    <td align="center" bgcolor="silver" colspan="3" width="500px">Bayar Kepada</td>   
</tr>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtJob" runat="server" Width="100%" Enabled="False" CssClass="font1"></dx:ASPxTextBox>        
    </td>
    <td></td>
    <td>No. Ref Lapangan</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoRef" runat="server" Width="100%" Enabled="False" CssClass="font1"></dx:ASPxTextBox>      
    </td>  
    <td colspan="5"></td>
    <td>No Kontrak/PO</td>
    <td>:</td>
    <td>        
        <dx:ASPxTextBox ID="TxtKo" runat="server" Width="450px"
            Enabled="False">
        </dx:ASPxTextBox>           
    </td>
</tr>
<tr>
    <td>Tgl Permintaan</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="TglPD" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            Theme="MetropolisBlue" Enabled="False">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxDateEdit>
    </td>    
    <td colspan="9"></td>
    <td>No Tagihan</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoTagihan" runat="server" Width="100%" CssClass="font1" 
            MaxLength="20" Enabled="False"></dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td>Deskripsi</td>    
    <td>:</td>
    <td colspan="9">
        <dx:ASPxTextBox ID="TxtDesc" runat="server" Width="100%" MaxLength="200" 
            Enabled="False">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxTextBox>
    </td>
    <td></td>
    <td>ID</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtVendor" runat="server" Width="100%"
            Enabled="False">
        </dx:ASPxTextBox>   
    </td>
</tr>
<tr>
    <td>Periode</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="PrdAwal" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            Theme="MetropolisBlue" Enabled="False">
        </dx:ASPxDateEdit>        
    </td>
    <td></td><td>s.d.</td><td></td>
    <td>
        <dx:ASPxDateEdit ID="PrdAkhir" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            Enabled="False" Theme="MetropolisBlue">
        </dx:ASPxDateEdit>
    </td>
    <td></td>
    <td>Minggu Ke</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtMinggu" runat="server" Width="50px" Enabled="False" CssClass="font1"></dx:ASPxTextBox>    
    </td>
    <td></td>
    <td>Nama</td>
    <td>:</td>
    <td>       
        <dx:ASPxTextBox ID="TxtNama" runat="server" Width="100%" CssClass="font1" Enabled="false"
            TabIndex="9" MaxLength="100">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td>Alokasi</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtAlokasi" runat="server" Width="100%"
            Enabled="False">
        </dx:ASPxTextBox>        
    </td>
    <td></td>
    <td>Tipe Form</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtForm" runat="server" Width="100%"
            Enabled="False">
        </dx:ASPxTextBox>   
    </td>
    <td colspan="5"></td>
    <td>Alamat</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtAlamat" runat="server" Width="100%" CssClass="font1" Enabled="false"
            TabIndex="10" MaxLength="255">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td colspan="2"></td>
    <td align="center" bgcolor="silver" colspan="5">Bukti Pendukung</td>
    <td colspan="5"></td>
    <td>Telepon</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtTelepon" runat="server" Width="100%" CssClass="font1" Enabled="false"
            TabIndex="11" MaxLength="20">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <asp:CheckBox ID="CBInvoice" runat="server" Text="Invoice/Kwitansi" 
            TabIndex="2" />
    </td>
    <td></td>
    <td colspan="3">
        <asp:CheckBox ID="CBFP" runat="server" Text="Faktur Pajak" TabIndex="5" />
    </td>
    <td colspan="5"></td>
    <td>NPWP</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNPWP" runat="server" Width="100%" CssClass="font1" TabIndex="12" MaxLength="20" Enabled="false"></dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <asp:CheckBox ID="CBSJ" runat="server" Text="Surat Jalan/Tanda Terima Lapangan" 
            TabIndex="3" />
    </td>
    <td></td>
    <td colspan="3">
        <asp:CheckBox ID="CBBayar" runat="server" Text="Berita Acara Pembayaran" 
            TabIndex="6" />
    </td>
    <td colspan="5"></td>
    <td align="center" bgcolor="silver" colspan="3">Rekening Penerima</td>
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <asp:CheckBox ID="CBPO" runat="server" Text="Copy PO" TabIndex="4" />
    </td>
    <td></td>
    <td colspan="3">
        <asp:CheckBox ID="CBFisik" runat="server" 
            Text="Berita Acara Opname Pekerjaan" TabIndex="7" />
    </td>
    <td colspan="5"></td>
    <td>No. Rekening</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoRek" runat="server" Width="100%" CssClass="font1" 
            Enabled="false" MaxLength="30"></dx:ASPxTextBox>     
    </td>
</tr>
<tr>
    <td colspan="12"></td>    
    <td>Atas Nama</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtAN" runat="server" Width="100%" CssClass="font1" 
            Enabled="false" MaxLength="100"></dx:ASPxTextBox>     
    </td>
</tr>
<tr>
    <td>Tgl PJ</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="TglPJ" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" TabIndex="1" Width="200px" 
            Theme="MetropolisBlue">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxDateEdit>
    </td>    
    <td colspan="9"></td>    
    <td>Bank</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtBank" runat="server" Width="100%" CssClass="font1" 
            Enabled="false" MaxLength="20"></dx:ASPxTextBox>     
    </td>
</tr>
<tr>
    <td colspan="15" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:5px;">
        <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN" 
            Theme="MetropolisBlue" Width="75px" TabIndex="14">
        </dx:ASPxButton>     
        <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL" CausesValidation="False"
            Theme="MetropolisBlue" Width="75px" TabIndex="15">
        </dx:ASPxButton>          
    </td>
</tr>
<tr><td style="height:5px"></td></tr>

</table>

<table>
<tr>
    <td align="center" bgcolor="silver" style="height:20px">Entry Uraian</td>
</tr>
<tr>
    <td style="border: 2px solid #C0C0C0">
        <table>
        <tr>
            <td style="padding-bottom:5px">
                <dx:ASPxButton ID="BtnAdd" runat="server" Text="TAMBAH"
                    Theme="MetropolisBlue" Width="80px" AutoPostBack="True" 
                    CausesValidation="False">
                </dx:ASPxButton>                       
                <dx:ASPxButton ID="BtnFillUraian" runat="server" Text="Isi Uraian Apabila = PD"
                    Theme="MetropolisBlue" Width="80px" AutoPostBack="True" 
                    CausesValidation="False">
                </dx:ASPxButton>                       
            </td>           
        </tr>
        <tr>
            <td style="padding-bottom:5px">
                <asp:GridView ID="GridPD" runat="server" AutoGenerateColumns="False"               
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
                        <asp:ButtonField CommandName="BtnUpdate" Text="SELECT" HeaderStyle-Width="45px"/>                                          
                        <%--<asp:ButtonField CommandName="BtnDelete" Text="DELETE" HeaderStyle-Width="45px"/>--%>
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
        <table>
        <tr>
            <td valign="top" rowspan="2" style="width:815px">
                <dx:ASPxMemo ID="TxtEditSaldo" runat="server" Height="40px" Width="500px" Caption="Remark Edit Saldo" 
                    CaptionSettings-Position="Top" MaxLength="255" Visible="false" Enabled="false">
                </dx:ASPxMemo>
            </td>
            <td style="text-align:center; font-family:Tahoma; font-size:12px; font-weight:bold">Dana yang tersedia dilapangan</td>
            <td>
                <dx:ASPxTextBox ID="TxtDana" runat="server" Width="250px" CssClass="font1" 
                    Enabled="False" HorizontalAlign="Right"></dx:ASPxTextBox>  
            </td>
        </tr>          
        <tr>
            <td style="text-align:center; font-family:Tahoma; font-size:12px; font-weight:bold">Saldo</td>
            <td>
                <dx:ASPxTextBox ID="TxtSaldo" runat="server" Width="250px" CssClass="font1" Enabled="false" HorizontalAlign="Right"></dx:ASPxTextBox>  
            </td>
        </tr>  
        </table>
    </td>
</tr>
</table>
</div>

</asp:Content>