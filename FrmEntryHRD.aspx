<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmEntryHRD.aspx.vb" Inherits="AIS.FrmEntryHRD" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .font1
    {font-family:Tahoma; font-size: 12px;}        
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div>
    <dx:ASPxPopupControl ID="errMsg" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="msgBox"
        HeaderText="Information" PopupAnimationType="Fade" EnableViewState="False" 
            Width="507px" PopupElementID="errMsg" CloseOnEscape="True">
        <ClientSideEvents Init="function(s, e) { bClose.Focus();  }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <div style="text-align:center; font-size:large; font-family:Segoe UI Light;">
                    <asp:Label ID="lblErr" runat="server" Text=""></asp:Label>
                    <br /> <br />
                    <div align="center">
                        <dx:ASPxButton ID="bClose" runat="server" AutoPostBack="False" ClientInstanceName="bClose"
                            Text="OK" Theme="SoftOrange">
                            <ClientSideEvents Click="function(s, e) { msgBox.Hide();}" />
                        </dx:ASPxButton>                       
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div>

<div style="font-family:Segoe UI Light">
<table>
<tr>
    <td style="font-size:30px; text-decoration:underline">
        <asp:Label ID="lblJudul" runat="server" Text="">Data Karyawan</asp:Label>
    </td>
</tr>
</table>
</div>

<div class="font1">
<dx:ASPxPageControl ID="TabPage" runat="server" ActiveTabIndex="0" 
        EnableHierarchyRecreation="True" Theme="MetropolisBlueBlue">
    <TabPages>
        <dx:TabPage Text="Personal">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl1" runat="server">
                    <table>
                        <tr>
                            <td>NIP</td>
                            <td>:</td>
                            <td> 
                                <asp:TextBox ID="txtNIP" runat="server" Width="100px" autocomplete="off" 
                                    CssClass="font1" TabIndex="1" MaxLength="0" Enabled="false"></asp:TextBox>
                            </td>    
                            <td></td>
                            <td>Nama Lengkap</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtNama" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="2" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>KTP</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtKTP" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="3" MaxLength="16"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>Alamat</td>
                            <td>:</td>
                            <td><asp:TextBox ID="txtAlamat" runat="server" Width="400px" autocomplete="off" 
                                    CssClass="font1" TabIndex="4" MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kelurahan</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtKelurahan" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="5" MaxLength="30"></asp:TextBox>
                            </td>    
                            <td></td>
                            <td>Kecamatan</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtKecamatan" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="6" MaxLength="30"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Kota</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtKota" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="7" MaxLength="30"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>Kode Pos</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtKodePos" runat="server" Width="100px" autocomplete="off" 
                                    CssClass="font1" TabIndex="8" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Jenis Kelamin</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="DDLKelamin" runat="server" Width="100px" CssClass="font1" 
                                    AutoPostBack="false" TabIndex="9">
                                    <asp:ListItem Value="L">Laki-laki</asp:ListItem>
                                    <asp:ListItem Value="P">Perempuan</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td>Agama</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="DDLAgama" runat="server" Width="150px" CssClass="font1" 
                                    AutoPostBack="false" TabIndex="10">
                                    <asp:ListItem>Islam</asp:ListItem>
                                    <asp:ListItem>Kristen</asp:ListItem>
                                    <asp:ListItem>Katolik</asp:ListItem>
                                    <asp:ListItem>Buddha</asp:ListItem>
                                    <asp:ListItem>Hindu</asp:ListItem>
                                    <asp:ListItem>Kong Hu Cu</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Tempat Lahir</td>
                            <td>:</td>
                            <td>    
                                <asp:TextBox ID="txtTmpLahir" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="11" MaxLength="30"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>Tanggal Lahir</td>
                            <td>:</td>
                            <td>
                                <dx:ASPxDateEdit ID="tglLahir" runat="server" CssClass="font1" 
                                    DisplayFormatString="dd MMMM yyyy" TabIndex="12"></dx:ASPxDateEdit>
                            </td>    
                        </tr>
                        <tr>
                            <td>Status</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="DDLStatus" runat="server" Width="150px" CssClass="font1" 
                                    AutoPostBack="false" TabIndex="13">
                                    <asp:ListItem Value="S">Lajang</asp:ListItem>
                                    <asp:ListItem Value="M">Menikah</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td>Jumlah Anak</td>
                            <td>:</td>
                            <td>
                                <dx:ASPxSpinEdit ID="jmlAnak" runat="server" DecimalPlaces="0" Number="0" 
                                    MaxLength="1" Width="50px" 
                                    TabIndex="14" Enabled="True" LargeIncrement="1" NumberType="Integer">
                                <SpinButtons ShowIncrementButtons="False"/>
                                </dx:ASPxSpinEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>Warga Negara</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtWN" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="15" MaxLength="20" Text="Indonesia"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>Suku</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtSuku" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="16" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>HP</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtHP" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="17" MaxLength="20"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>Telepon</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtTelp" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="18" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Pendidikan</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtPendidikan" runat="server" Width="100px" autocomplete="off" 
                                    CssClass="font1" TabIndex="19" MaxLength="5" placeholder="SMK,D3,S1,S2,dst"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>Jurusan</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtJurusan" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="20" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Email</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="21" MaxLength="50"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>KK</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtKK" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="22" MaxLength="16"></asp:TextBox>
                            </td>
                        </tr>
                        </table>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Kepegawaian">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl2" runat="server">
                    <table>
                        <tr>
                            <td>Tanggal Masuk</td>
                            <td>:</td>
                            <td> 
                                <dx:ASPxDateEdit ID="tglMasuk" runat="server" CssClass="font1" 
                                    DisplayFormatString="dd MMMM yyyy" TabIndex="1"></dx:ASPxDateEdit>
                            </td>    
                            <td></td>
                            <td>Tanggal Keluar</td>
                            <td>:</td>
                            <td>
                                <dx:ASPxDateEdit ID="tglKeluar" runat="server" CssClass="font1" 
                                    DisplayFormatString="dd MMMM yyyy" TabIndex="2" Enabled="true"></dx:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>Tanggal Awal Kontrak</td>
                            <td>:</td>
                            <td>
                                <dx:ASPxDateEdit ID="tglAwalKontrak" runat="server" CssClass="font1" 
                                    DisplayFormatString="dd MMMM yyyy" TabIndex="3"></dx:ASPxDateEdit>
                            </td>
                            <td></td>
                            <td>Tanggal Akhir Kontrak</td>
                            <td>:</td>
                            <td>
                                <dx:ASPxDateEdit ID="tglAkhirKontrak" runat="server" CssClass="font1" 
                                    DisplayFormatString="dd MMMM yyyy" TabIndex="4"></dx:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>Klasifikasi Karyawan</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="DDLKlasifikasi" runat="server" Width="150px" CssClass="font1" 
                                    AutoPostBack="false" TabIndex="5">
                                    <asp:ListItem>Kantor</asp:ListItem>
                                    <asp:ListItem>Proyek</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td>Lokasi</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtLokasi" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="6" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Status Karyawan</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="DDLStsKerja" runat="server" Width="150px" CssClass="font1" 
                                    AutoPostBack="false" TabIndex="7">
                                    <asp:ListItem>Kontrak</asp:ListItem>
                                    <asp:ListItem>Permanen</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td>Status Pajak</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="DDLStsPajak" runat="server" Width="150px" CssClass="font1" 
                                    AutoPostBack="false" TabIndex="8">
                                    <asp:ListItem>TK0</asp:ListItem>
                                    <asp:ListItem>TK1</asp:ListItem>
                                    <asp:ListItem>TK2</asp:ListItem>
                                    <asp:ListItem>TK3</asp:ListItem>
                                    <asp:ListItem>K0</asp:ListItem>
                                    <asp:ListItem>K1</asp:ListItem>
                                    <asp:ListItem>K2</asp:ListItem>
                                    <asp:ListItem>K3</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>NPWP</td>
                            <td>:</td>
                            <td>
                                <dx:ASPxTextBox ID="txtNPWP" runat="server" Width="250px" MaxLength="20" 
                                    Native="True" TabIndex="9">
                                    <MaskSettings Mask="99.999.999.9-999.999" />
                                </dx:ASPxTextBox>
                            </td>
                            <td></td>
                            <td>BPJS Kes</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtBPJS" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="10" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>No. KPA</td>
                            <td>:</td>
                            <td>
                                 <asp:TextBox ID="txtNoKPA" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="11" MaxLength="20"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>Tanggal KPA</td>
                            <td>:</td>
                            <td>
                                <dx:ASPxDateEdit ID="tglKPA" runat="server" CssClass="font1" 
                                    DisplayFormatString="dd MMMM yyyy" TabIndex="12"></dx:ASPxDateEdit>
                            </td>
                        </tr>
                        <tr>
                            <td>JKK</td>
                            <td>:</td>
                            <td>
                                 <asp:TextBox ID="txtJKK" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="13" MaxLength="20"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>JKM</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtJKM" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="14" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>JHT</td>
                            <td>:</td>
                            <td>
                                 <asp:TextBox ID="txtJHT" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="15" MaxLength="20"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>JP</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtJP" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="16" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Status Perjanjian Kerja</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="DDLPKWTT" runat="server" Width="250px" CssClass="font1" 
                                    AutoPostBack="false" TabIndex="17">
                                    <asp:ListItem Value="PKWT">Perjanjian Kerja Waktu Tertentu</asp:ListItem>
                                    <asp:ListItem Value="PKWTT">Perjanjian Kerja Waktu Tidak Tertentu</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td>Terdaftar Di</td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="DDLTerdaftar" runat="server" Width="250px" CssClass="font1" 
                                    AutoPostBack="false" TabIndex="18">
                                    <asp:ListItem Value="Alkatec">Alkatec Mandiri Kencana</asp:ListItem>
                                    <asp:ListItem Value="Minarta">Minarta Duta Hutama</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">Alasan Keluar</td>
                            <td valign="top">:</td>
                            <td>
                                <asp:TextBox ID="txtKeluar" runat="server" Width="250px" autocomplete="off" 
                                    CssClass="font1" TabIndex="19" MaxLength="200" TextMode="MultiLine" Height="50px" 
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Bank">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl3" runat="server">
                    <table>
                        <tr>
                            <td>Bank</td>
                            <td>:</td>
                            <td> 
                                <asp:DropDownList ID="DDLBank" runat="server" Width="200px" CssClass="font1" 
                                    AutoPostBack="false" TabIndex="1">
                                </asp:DropDownList>
                            </td>    
                            <td></td>
                            <td>Cabang</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtCabang" runat="server" Width="200px" autocomplete="off" 
                                    CssClass="font1" TabIndex="2" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Rekening</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtRek" runat="server" Width="200px" autocomplete="off" 
                                    CssClass="font1" TabIndex="3" MaxLength="30"></asp:TextBox>
                            </td>
                            <td></td>
                            <td>Atas Nama</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtAN" runat="server" Width="200px" autocomplete="off" 
                                    CssClass="font1" TabIndex="4" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>                        
                    </table>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>

<table>
<tr>
    <td>
        <dx:ASPxButton ID="bSave" runat="server" Text="SIMPAN" 
            Theme="SoftOrange" Width="75px">
        </dx:ASPxButton>     
        <dx:ASPxButton ID="bCancel" runat="server" Text="BATAL" 
            Theme="SoftOrange" Width="75px">
        </dx:ASPxButton>          
    </td>
</tr>
</table>

</div>
</asp:Content>