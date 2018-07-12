<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmEntryPD.aspx.vb" Inherits="AIS.FrmEntryPD" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
    .AllUpper input
    {text-transform: uppercase;}
</style>
<script type="text/javascript">
function tb1_OnKeyUp(s, e) {
    var code = e.htmlEvent.keyCode;
    if (code > 40 || code < 37)
        s.SetText(s.GetText().toUpperCase());
}
$(function () {
    $("[id*=GridPD] td").hover(function () {
        $("td", $(this).closest("tr")).addClass("hover_row");
    }, function () {
        $("td", $(this).closest("tr")).removeClass("hover_row");
    });
    $("[id*=GridView] td").hover(function () {
        $("td", $(this).closest("tr")).addClass("hover_row");
    }, function () {
        $("td", $(this).closest("tr")).removeClass("hover_row");
    });
});
function checkAll(objRef) {
    var GridView = objRef.parentNode.parentNode.parentNode;
    var inputList = GridView.getElementsByTagName("input");
    for (var i = 0; i < inputList.length; i++) {
        //Get the Cell To find out ColumnIndex
        var row = inputList[i].parentNode.parentNode;
        if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
            if (objRef.checked) {
                //If the header checkbox is checked
                //check all checkboxes
                inputList[i].checked = true;
            }
            else {
                //If the header checkbox is checked
                //uncheck all checkboxes
                inputList[i].checked = false;
            }
        }
    }
};
</script>
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
    <dx:ASPxPopupControl ID="ErrMsg" runat="server" CloseAction="CloseButton" 
        CloseOnEscape="True" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ErrMsg"
        HeaderText="Information" PopupAnimationType="None" EnableViewState="False" 
        Width="500px" Theme="MetropolisBlue" PopupElementID="ErrMsg">
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
            Width="700px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div align="center">
                    <table>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TxtAction" runat="server" Text="NEW" 
                                BorderColor="White" BorderStyle="None" ForeColor="White" Width="30px" 
                                Enabled="False" BackColor="White">
                            </asp:TextBox>
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
                            <dx:ASPxComboBox ID="DDLAlokasi1" runat="server" ValueType="System.String" 
                                CssClass="font1" Width="450px" ClientInstanceName="DDLAlokasi1" 
                                Theme="MetropolisBlue" AutoPostBack="true">
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
                                CssClass="font1" MaxLength="300">
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
                            <dx:ASPxSpinEdit ID="TxtHrgSatuan" runat="server" DecimalPlaces="0" 
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" >
                            <SpinButtons ShowIncrementButtons="False"/>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="4" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSave1" runat="server" Text="SIMPAN" CausesValidation="False"
                                Theme="MetropolisBlue" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancel1" runat="server" Text="BATAL"
                                Theme="MetropolisBlue" Width="80px" AutoPostBack="False" CausesValidation="False">
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

<div style="font-family:Segoe UI Light">
<table>
<tr>
    <td style="font-size:30px; text-decoration:underline">
        <asp:Label ID="LblJudul" runat="server" Text="Permintaan Dana"></asp:Label>
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="LblAction" runat="server" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblJobNo" runat="server" Visible="false"></asp:Label>
    </td>    
    <td>
        <asp:Label ID="LblNoPD" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblSource" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblRKD" runat="server" Text="" Visible="false"></asp:Label>
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
    <td>No. BAP/Ref Lapangan</td>
    <td>:</td>
    <td colspan="4">
        <dx:ASPxTextBox ID="TxtNoRef" runat="server" Width="100%" CssClass="AllUpper" MaxLength="50" >
            <ClientSideEvents KeyUp="tb1_OnKeyUp" />
        </dx:ASPxTextBox>      
    </td>    
    <td colspan="2"></td>
    <td align="center" bgcolor="silver" colspan="3" width="500px">Bayar Kepada</td>   
</tr>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td colspan="3">
        <dx:ASPxTextBox ID="TxtJob" runat="server" Width="100%" Enabled="False" CssClass="font1"></dx:ASPxTextBox>        
    </td>
    <td colspan="7"></td>
    <td>No Kontrak/PO</td>
    <td>:</td>
    <td>        
        <dx:ASPxComboBox ID="DDLKo" runat="server" ValueType="System.String" 
            CssClass="font1" Width="450px" 
            ClientInstanceName="DDLKo" Theme="MetropolisBlue" 
            Enabled="False" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Tgl Permintaan</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="TglPD" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            Theme="MetropolisBlue" AutoPostBack="True">
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
            MaxLength="100"></dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td>Deskripsi</td>    
    <td>:</td>
    <td colspan="9">
        <dx:ASPxTextBox ID="TxtDesc" runat="server" Width="100%" MaxLength="200" >
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxTextBox>
    </td>
    <td></td>
    <td>ID</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtVendor" runat="server" Width="100%" Enabled="false">
        </dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td>Periode</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="PrdAwal" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            Theme="MetropolisBlue">
        </dx:ASPxDateEdit>        
    </td>
    <td></td><td>s.d.</td><td></td>
    <td>
        <dx:ASPxDateEdit ID="PrdAkhir" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" Width="200px" Theme="MetropolisBlue">
        </dx:ASPxDateEdit>
    </td>
    <td></td>
    <td>Minggu Ke</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtMinggu" runat="server" Width="50px" CssClass="font1"></dx:ASPxTextBox>    
    </td>
    <td></td>
    <td>Nama</td>
    <td>:</td>
    <td>       
        <dx:ASPxTextBox ID="TxtNama" runat="server" Width="100%" CssClass="font1" Enabled="false"
            MaxLength="100">
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
        <dx:ASPxComboBox ID="DDLAlokasi" runat="server" ValueType="System.String" 
            CssClass="font1" Width="100%" Enabled="false"
            ClientInstanceName="DDLAlokasi" Theme="MetropolisBlue" 
            AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
    <td></td>
    <td>Tipe Form</td>
    <td>:</td>
    <td colspan="5">
        <dx:ASPxComboBox ID="DDLForm" runat="server" ValueType="System.String" 
            CssClass="font1" Width="100%" 
            ClientInstanceName="DDLForm" Theme="MetropolisBlue" 
            AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
    <td></td>
    <td>Alamat</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtAlamat" runat="server" Width="100%" CssClass="font1" Enabled="false"
            MaxLength="255">
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
            MaxLength="20">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <asp:CheckBox ID="CBInvoice" runat="server" Text="Invoice/Kwitansi" />
    </td>
    <td></td>
    <td colspan="3">
        <asp:CheckBox ID="CBFP" runat="server" Text="Faktur Pajak" />
    </td>
    <td colspan="5"></td>
    <td>NPWP</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNPWP" runat="server" Width="100%" CssClass="font1" 
            MaxLength="20" Enabled="false"></dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <asp:CheckBox ID="CBSJ" runat="server" Text="Surat Jalan/Tanda Terima Lapangan" />
    </td>
    <td></td>
    <td colspan="3">
        <asp:CheckBox ID="CBBayar" runat="server" Text="Berita Acara Pembayaran" />
    </td>
    <td colspan="5"></td>
    <td align="center" bgcolor="silver" colspan="3">Rekening Penerima</td>
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <asp:CheckBox ID="CBPO" runat="server" Text="Copy PO" />
    </td>
    <td></td>
    <td colspan="3">
        <asp:CheckBox ID="CBFisik" runat="server" 
            Text="Berita Acara Opname Pekerjaan" />
    </td>
    <td colspan="5"></td>
    <td>No. Rekening</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoRek" runat="server" Width="100%" CssClass="font1" 
            Enabled="false" MaxLength="30">
             <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>          
        </dx:ASPxTextBox>     
    </td>
</tr>
<tr>
    <td colspan="2" rowspan="2"></td>
    <td rowspan="2">
        <asp:Image ID="ImgReject" runat="server" Visible="False" ImageAlign="Right" ImageUrl="~/Images/reject.jpg" />
    </td>    
    <td></td>
    <td colspan="8">
        <asp:Label ID="LblRejectBy" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>Atas Nama</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtAN" runat="server" Width="100%" CssClass="font1" 
            Enabled="false" MaxLength="100">
             <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>          
        </dx:ASPxTextBox>     
    </td>
</tr>
<tr>
    <td></td>
    <td colspan="8">
        <asp:Label ID="LblRejectOn" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>Bank</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLBank" runat="server" ValueType="System.String" 
            CssClass="font1" Width="100%" Enabled="false"
            ClientInstanceName="DDLBank" Theme="MetropolisBlue">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td colspan="15" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:5px;">
        <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN" 
            Theme="MetropolisBlue" Width="75px">
        </dx:ASPxButton>     
        <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL" CausesValidation="False"
            Theme="MetropolisBlue" Width="75px">
        </dx:ASPxButton>          
    </td>
</tr>
<tr><td style="height:10px"></td></tr>
</table>

<dx:ASPxPageControl ID="TabPage" runat="server" ActiveTabIndex="0" AutoPostBack="true"
        Theme="MetropolisBlue">
    <TabPages>
        <dx:TabPage Text="Uraian PD" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl1" runat="server">
                <table>
                <tr>
                    <td align="center" bgcolor="silver" style="height:20px; font-weight:bold">U R A I A N&nbsp; &nbsp;P E R M I N T A A N&nbsp; &nbsp;D A N A</td>
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
                            </td>           
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridPD" runat="server" AutoGenerateColumns="False"               
                                    CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                                    ShowHeaderWhenEmpty="True">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>              
                                        <asp:BoundField DataField="NoUrut" HeaderText="No."  HeaderStyle-Width="35px" ItemStyle-Width = "35px" ItemStyle-HorizontalAlign="Center">     
<HeaderStyle Width="35px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="35px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Tipe" HeaderText="Tipe">
                                            <HeaderStyle CssClass="hiddencol" />
                                            <ItemStyle CssClass="hiddencol" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="KdRAP" HeaderText="Kode RAP" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                        
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Uraian" HeaderStyle-Width="550px" ItemStyle-Width = "550px">
                                            <ItemTemplate>
                                                <asp:Label ID="LblUraian" runat="server" Text='<%# Eval("Uraian").ToString().Replace(vbCRLF, "<br />") %>'></asp:Label>
                                            </ItemTemplate>

<HeaderStyle Width="550px"></HeaderStyle>

<ItemStyle Width="550px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Vol" HeaderText="Vol"  HeaderStyle-Width="80px" ItemStyle-Width = "80px" 
                                        ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N3}">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="80px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Uom" HeaderText="Sat" HeaderStyle-Width= "30px" ItemStyle-Width = "30px" ItemStyle-HorizontalAlign="Center">                        
<HeaderStyle Width="30px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HrgSatuan" HeaderText="Harga Satuan (Rp)" 
                                            HeaderStyle-Width="200px" ItemStyle-Width = "200px" DataFormatString="{0:N0}" Itemstyle-HorizontalAlign="right">
<HeaderStyle Width="200px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="200px"></ItemStyle>
                                        </asp:BoundField>                        
                                        <asp:TemplateField HeaderText="Jumlah Harga (Rp)" HeaderStyle-Width="250px" ItemStyle-Width = "250px">
                                            <ItemTemplate>
                                                <asp:Label ID="LblJumlah" Text='<%# string.Format("{0:N0}",Eval("Vol") * Eval("HrgSatuan")) %>' runat="server"/>                                
                                            </ItemTemplate>

<HeaderStyle Width="250px"></HeaderStyle>

                                            <ItemStyle HorizontalAlign="Right"></ItemStyle>                            
                                        </asp:TemplateField>
                                        <asp:ButtonField CommandName="BtnUpdate" Text="SELECT" HeaderStyle-Width="45px">                                          
<HeaderStyle Width="45px"></HeaderStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField CommandName="BtnDelete" Text="DELETE" HeaderStyle-Width="45px">                                                                 
<HeaderStyle Width="45px"></HeaderStyle>
                                        </asp:ButtonField>
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
                            <td valign="top" rowspan="3" style="width:500px">
                                <dx:ASPxMemo ID="TxtEditSaldo" runat="server" Height="40px" Width="500px" Caption="Remark Edit Saldo" 
                                CaptionSettings-Position="Top" MaxLength="255" Visible="false">
                                </dx:ASPxMemo>
                            </td>
                            <td></td>
                            <td style="text-align:center; font-family:Tahoma; font-size:12px; font-weight:bold">Sub Total</td>
                            <td>
                                <dx:ASPxTextBox ID="TxtSubTotal" runat="server" Width="200px" CssClass="font1" 
                                    Enabled="false" HorizontalAlign="Right" Text="0"></dx:ASPxTextBox>  
                            </td>
                        </tr>
                        <tr>
                            <td style="width:260px; text-align:center; font-family:Tahoma; font-size:12px; font-weight:bold">Saldo sebelumnya dari No</td>
                            <td>
                                <dx:ASPxTextBox ID="TxtNoPJ" runat="server" Width="200px" CssClass="font1" Enabled="false" HorizontalAlign="Center"></dx:ASPxTextBox>     
                            </td>
                            <td>
                                <dx:ASPxTextBox ID="TxtSaldo" runat="server" Width="200px" CssClass="font1" Enabled="false" Text="0" 
                                    HorizontalAlign="Right" AutoPostBack="true">
                                    <MaskSettings Mask="&lt;0..99999999999g&gt;" />
                                </dx:ASPxTextBox>                                  
                            </td>
                            <%--<td>
                                <dx:ASPxButton ID="EditLink" runat="server" Text="EDIT" RenderMode="Link" CausesValidation="false">
                                </dx:ASPxButton>
                            </td>--%>
                        </tr>          
                        <tr>
                            <td></td>
                            <td style="text-align:center; font-family:Tahoma; font-size:12px; font-weight:bold">Total Permintaan</td>
                            <td>
                                <dx:ASPxTextBox ID="TxtTotal" runat="server" Width="200px" CssClass="font1" 
                                    Enabled="false" HorizontalAlign="Right" Text="0"></dx:ASPxTextBox>  
                            </td>
                        </tr>  
                        </table>
                    </td>
                </tr>
                </table>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Invoice" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl2" runat="server">
                <table>
                <tr>
                    <td align="center" bgcolor="silver" style="height:20px; font-weight:bold">L I S T&nbsp; &nbsp;I N V O I C E</td>
                </tr>
                <tr>
                    <td style="border: 2px solid #C0C0C0">
                        <table>
                        <tr>
                            <td>Total Invoice</td>
                            <td>:</td>
                            <td>
                                <dx:ASPxTextBox ID="TxtTotalInvoice" runat="server" Width="150px" Enabled="false">
                                </dx:ASPxTextBox>
                            </td>
                            <td style="width:20px;"></td>
                            <td>Total Centang</td>
                            <td>:</td>
                            <td>
                                <dx:ASPxTextBox ID="TxtTotalCentang" runat="server" Width="150px" Enabled="false">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        </table>
                        <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False"               
                                    CellPadding="4" ForeColor="#333333" GridLines="None"
                                    ShowHeaderWhenEmpty="True">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>              
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                              <asp:CheckBox ID="checkAll" runat="server" onclick = "checkAll(this);" 
                                                    AutoPostBack="true" OnCheckedChanged="chkAll_CheckedChanged" />
                                            </HeaderTemplate>                                            
                                           <ItemTemplate>
                                             <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" 
                                                OnCheckedChanged="chk1_CheckedChanged" Checked='<%# GetValue(Eval("IsChecked")) %>' />
                                           </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText = "No." HeaderStyle-Width="30px" ItemStyle-Width="30px">
                                            <ItemTemplate>
                                                <asp:Label ID="LblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>      
                                        </asp:TemplateField>  
                                        <asp:BoundField DataField="NoKO" HeaderText="No. KO" HeaderStyle-Width="100px" ItemStyle-Width = "100px" 
                                            HeaderStyle-Height="20px" HeaderStyle-HorizontalAlign="Left"> 
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VendorNm" HeaderText="Vendor" HeaderStyle-Width="250px"
                                            ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="Left">                        
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InvNo" HeaderText="No. Invoice" HeaderStyle-Width="150px" ItemStyle-Width = "150px" 
                                            HeaderStyle-Height="20px" HeaderStyle-HorizontalAlign="Left"> 
                                        </asp:BoundField>
                                        <asp:BoundField DataField="InvDate" HeaderText="Tgl Invoice" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="100px"  DataFormatString="{0:dd-MMM-yyyy}">                        
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DueDate" HeaderText="Jatuh Tempo" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="100px"  DataFormatString="{0:dd-MMM-yyyy}">                        
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Total (Rp)" HeaderStyle-Width="150px" 
                                            ItemStyle-Width="150px" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">                        
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PPN" HeaderText="PPN (Rp)" HeaderStyle-Width="100px" 
                                            ItemStyle-Width="100px" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">                        
                                        </asp:BoundField>
                                         <asp:BoundField DataField="FPNo" HeaderText="No. FP" HeaderStyle-Width="150px" ItemStyle-Width = "150px" ItemStyle-HorizontalAlign="Center" 
                                            HeaderStyle-Height="20px"> 
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FPDate" HeaderText="Tgl FP" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-Width="100px"  DataFormatString="{0:dd-MMM-yyyy}">                        
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Total Bayar" HeaderStyle-Width="120px" ItemStyle-Width = "120px" 
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <dx:ASPxTextBox ID="TxtPayment" runat="server" Text='<%# Eval("PaymentAmount") %>'
                                                    HorizontalAlign="Right" ValidationSettings-ErrorDisplayMode="None" OnValueChanged="TxtPay_Changed" AutoPostBack="true">
                                                    <MaskSettings Mask="&lt;0..99999999999g&gt;" />
                                                </dx:ASPxTextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TotalPayment" HeaderText="Total Sdh Terbayar" HeaderStyle-Width="100px" 
                                            ItemStyle-Width="100px" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle CssClass="hiddencol" />
                                            <ItemStyle CssClass="hiddencol" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OriginalPayment" HeaderText="Total Bayar Awal" HeaderStyle-Width="100px" 
                                            ItemStyle-Width="100px" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle CssClass="hiddencol" />
                                            <ItemStyle CssClass="hiddencol" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView> 
                            </td>
                        </tr>                 
                        </table>
                    </td>
                </tr>
                </table>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
<cc1:msgBox ID="MsgBox1" runat="server" />
</div>

</asp:Content>