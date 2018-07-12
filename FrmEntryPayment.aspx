<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmEntryPayment.aspx.vb" Inherits="AIS.FrmEntryPayment" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
    
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Pembayaran Permintaan Dana</div>

<div>
<table>
<tr>
    <td>
        <asp:Label ID="LblAction" runat="server" Visible="False"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblNoPD" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblKSO" runat="server" Text="" Visible="false"></asp:Label>
    </td>
</tr>
</table>
</div>

<div>
<table>
<tr>
    <td>No PD</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoPD" runat="server" Width="250px" Enabled="False"></dx:ASPxTextBox>        
    </td>
    <td></td>
    <td>No. Ref Lapangan</td>
    <td>:</td>
    <td colspan="4">
        <dx:ASPxTextBox ID="TxtNoRef" runat="server" Width="100%" Enabled="False"></dx:ASPxTextBox>      
    </td>    
    <td colspan="2"></td>
    <td align="center" bgcolor="silver" colspan="3" width="500px">Bayar Kepada</td>   
</tr>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td colspan="3">
        <dx:ASPxTextBox ID="TxtJob" runat="server" Width="100%" Enabled="False"></dx:ASPxTextBox>        
    </td>
    <td colspan="7"></td>
    <td>No Kontrak/PO</td>
    <td>:</td>
    <td>        
        <dx:ASPxTextBox ID="TxtKo" runat="server" Width="100%" Enabled="False"></dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td>Tgl Permintaan</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="TglPD" runat="server" 
            DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            Theme="MetropolisBlue" Enabled="False">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>      
        </dx:ASPxDateEdit>
    </td>    
    <td colspan="9"></td>
    <td>No Tagihan</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoTagihan" runat="server" Width="100%" 
             MaxLength="20" Enabled="False"></dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td>Deskripsi</td>    
    <td>:</td>
    <td colspan="9">
        <dx:ASPxTextBox ID="TxtDesc" runat="server" Width="100%" MaxLength="200" 
            Enabled="False">           
        </dx:ASPxTextBox>
    </td>
    <td></td>
    <td>ID</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtVendor" runat="server" Width="100%" Enabled="False"> </dx:ASPxTextBox>     
    </td>
</tr>
<tr>   
    <td colspan="12"></td>
    <td>Nama</td>
    <td>:</td>
    <td>       
        <dx:ASPxTextBox ID="TxtNama" runat="server" Width="100%" Enabled="false" MaxLength="100">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>          
        </dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td>Alokasi</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtAlokasi" runat="server" Width="100%" Enabled="false"></dx:ASPxTextBox> 
    </td>
    <td></td>
    <td>Tipe Form</td>
    <td>:</td>
    <td colspan="5">
        <dx:ASPxTextBox ID="TxtForm" runat="server" Width="300px" Enabled="false"></dx:ASPxTextBox> 
    </td>
    <td></td>
    <td>Alamat</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtAlamat" runat="server" Width="100%" Enabled="false"
            MaxLength="255">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>              
        </dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td colspan="11" align="center" bgcolor="silver" style="height:20px">Detail Pembayaran</td>    
    <td></td>
    <td>Telepon</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtTelepon" runat="server" Width="100%" Enabled="false"
            MaxLength="20">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>        
        </dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td>Sub Total</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtSubTotal" runat="server" Width="100%" Enabled="false" Text="0"></dx:ASPxTextBox>   
    </td>
    <td colspan="8" bgcolor="#E2E2E2" align="center">Rekening Pengirim</td>
    <td></td>
    <td>NPWP</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNPWP" runat="server" Width="100%" MaxLength="20" Enabled="false"></dx:ASPxTextBox>        
    </td>
</tr>
<tr>
    <td>Saldo sebelumnya</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoPJ" runat="server" Width="100%" Enabled="false"></dx:ASPxTextBox>   
    </td>
    <td></td>
    <td>Rekening ID</td>
    <td>:</td>
    <td colspan="5">
        <dx:ASPxComboBox ID="DDLRek" runat="server" 
            Width="100%" ClientInstanceName="DDLRek" Theme="MetropolisBlue" 
            AutoPostBack="True" Enabled="False">            
        </dx:ASPxComboBox>
    </td>
    <td></td>
    <td align="center" bgcolor="silver" colspan="3" style="height:20px">Rekening Penerima</td>
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <dx:ASPxTextBox ID="TxtSaldo" runat="server" Width="100%" Enabled="false" Text="0"></dx:ASPxTextBox>   
    </td>
    <td></td>
    <td>No. Rekening</td>
    <td>:</td>
    <td colspan="5">
        <dx:ASPxTextBox ID="TxtNoRekKirim" runat="server" Width="100%" 
            Enabled="false"></dx:ASPxTextBox>     
    </td>
    <td></td>
    <td>No. Rekening</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoRek" runat="server" Width="100%"></dx:ASPxTextBox>     
    </td>
</tr>
<tr>
    <td style="border-top:2px; border-top-color: Black; border-style: solid none double none;">Total Permintaan</td>
    <td style="border-top:2px; border-top-color: Black; border-style: solid none double none;">:</td>
    <td style="border-top:2px; border-top-color: Black; border-style: solid none double none;">
        <dx:ASPxTextBox ID="TxtTotal" runat="server" Width="100%" Text="0" ValidationSettings-ErrorDisplayMode="None">
            <MaskSettings Mask="&lt;-999999999999999..999999999999999g&gt;" />
        </dx:ASPxTextBox>  
        <dx:ASPxTextBox ID="TxtTotal1" runat="server" Width="100%" Text="0" ValidationSettings-ErrorDisplayMode="None" Visible="false" />
    </td>
    <td></td>    
    <td>Atas Nama</td>
    <td>:</td>
    <td colspan="5">
        <dx:ASPxTextBox ID="TxtANKirim" runat="server" Width="100%"
            Enabled="false" MaxLength="100"></dx:ASPxTextBox>     
    </td>
    <td></td>
    <td>Atas Nama</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtAN" runat="server" Width="100%"
            MaxLength="100"></dx:ASPxTextBox>     
    </td>
</tr>
<tr>
    <td>Tanggal Bayar</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="TglBayar" runat="server"
            DisplayFormatString="dd-MMM-yyyy" Width="100%" 
            Theme="MetropolisBlue">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>    
        </dx:ASPxDateEdit>
    </td>    
    <td></td>    
    <td>Bank</td>
    <td>:</td>
    <td colspan="5">
        <dx:ASPxTextBox ID="TxtBankKirim" runat="server" Width="100%" 
            Enabled="false" MaxLength="100"></dx:ASPxTextBox>     
    </td>
    <td></td>
    <td>Bank</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtBank" runat="server" Width="100%"
            MaxLength="20"></dx:ASPxTextBox>     
    </td>
</tr>
<tr>
    <td>Via</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLVia" runat="server" 
            Width="100%" ClientInstanceName="DDLVia" Theme="MetropolisBlue" 
            AutoPostBack="True">
            <Items>
                <dx:ListEditItem Text="Pilih salah satu" Value="0" />
                <dx:ListEditItem Text="Tunai/Setor" Value="CASH" />
                <dx:ListEditItem Text="Transfer" Value="TRF" />
                <dx:ListEditItem Text="Cheque/Giro" Value="CG" />
                <dx:ListEditItem Text="Tidak Dibayar" Value="NOT_PAID" />
            </Items>
        </dx:ASPxComboBox>
    </td>    
    <td></td>
    <td>No. Cheque/Giro</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtCG" runat="server" Width="100%" Enabled="false"></dx:ASPxTextBox>   
    </td>
</tr>
<tr>
    <td>Jenis Transfer</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLTransfer" runat="server" 
            Width="100%" ClientInstanceName="DDLTransfer" Theme="MetropolisBlue" 
            Enabled="False">
            <Items>
                <dx:ListEditItem Text="Pilih salah satu" Value="0" />
                <dx:ListEditItem Text="Kliring" Value="Kliring" />
                <dx:ListEditItem Text="Online" Value="Online" />
                <dx:ListEditItem Text="RTGS" Value="RTGS" />
                <dx:ListEditItem Text="Sesama BNI" Value="Sesama BNI" />
            </Items>
        </dx:ASPxComboBox>
    </td>    
    <td></td>    
</tr>
<tr>
    <td>Sumber Kas</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLKas" runat="server" 
            Width="100%" Enabled="false"
            ClientInstanceName="DDLKas" Theme="MetropolisBlue">
            <Items>
                <dx:ListEditItem Text="Pilih salah satu" Value="0" />
                <dx:ListEditItem Text="JAKARTA" Value="JKT" />
                <dx:ListEditItem Text="MAKASSAR" Value="MKS" />
                <dx:ListEditItem Text="LAPANGAN" Value="LAP" />
            </Items>
        </dx:ASPxComboBox>
    </td>    
</tr>
<tr>
    <td>Nama Penerima</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtPenerima" runat="server" Width="100%" Enabled="false"></dx:ASPxTextBox>   
    </td>
</tr>
<tr>
    <td valign="top">Keterangan</td>
    <td valign="top">:</td>
    <td colspan="5">
        <dx:ASPxMemo ID="TxtKeterangan" runat="server" Height="71px" Width="100%" MaxLength="255">
        </dx:ASPxMemo>        
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
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>

</asp:Content>