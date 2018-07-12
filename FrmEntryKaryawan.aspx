<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmEntryKaryawan.aspx.vb" Inherits="AIS.FrmEntryKaryawan" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            height: 39px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
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
    <dx:ASPxPopupControl ID="PopEntKeluarga" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopEntKeluarga"
        HeaderText="Data Entry Keluarga" PopupAnimationType="Fade" 
            Width="700px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue" AllowDragging="True">
        <ClientSideEvents EndCallback="function(s, e) { PopEntKeluarga.Show(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div align="center">
                    <table>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TxtActKeluarga" runat="server" Text="" 
                                BorderColor="White" BorderStyle="None" ForeColor="White" Width="30px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">No.</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtNoUrutKeluarga" runat="server" Width="30px" 
                                ClientInstanceName="TxtNoUrutKeluarga" Enabled="false" CssClass="font1">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>       
                    <tr>
                        <td align="left">Hubungan Keluarga</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxComboBox ID="TxtHubKeluarga" runat="server" Theme="MetropolisBlue">
                                <Items>
                                    <dx:ListEditItem Text="Ayah" Value="Ayah" />
                                    <dx:ListEditItem Text="Ibu" Value="Ibu" />
                                    <dx:ListEditItem Text="Saudara Kandung" Value="Saudara Kandung" />
                                    <dx:ListEditItem Text="Suami/Istri" Value="Suami/Istri" />
                                    <dx:ListEditItem Text="Anak" Value="Anak" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Nama</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtNamaKeluarga" runat="server" Width="100%" 
                                ClientInstanceName="TxtNamaKeluarga" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Jenis Kelamin</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxComboBox ID="TxtJenisKelaminKeluarga" runat="server" ValueType="System.String" 
                                CssClass="font1" ClientInstanceName="TxtJenisKelaminKeluarga" Theme="MetropolisBlue">
                                <Items>
                                    <dx:ListEditItem Text="Pria" Value="L" />
                                    <dx:ListEditItem Text="Wanita" Value="P" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">Tanggal Lahir</td>
                        <td valign="top">:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxDateEdit ID="TxtTglLahirKeluarga" runat="server" Theme="MetropolisBlue">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Pekerjaan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtPekerjaanKeluarga" runat="server" Width="100%" 
                                ClientInstanceName="TxtPekerjaanKeluarga" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Perusahaan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtPerusahaanKeluarga" runat="server" Width="100%" 
                                ClientInstanceName="TxtPerusahaanKeluarga" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="4" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSaveKeluarga" runat="server" Text="SIMPAN" CausesValidation="false"
                                Theme="MetropolisBlue" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancelKeluarga" runat="server" Text="BATAL"
                                Theme="MetropolisBlue" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) { PopEntKeluarga.Hide();}" />
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
    <dx:ASPxPopupControl ID="PopEntPendidikan" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopEntPendidikan"
        HeaderText="Data Entry Pendidikan" PopupAnimationType="Fade" 
            Width="700px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue" AllowDragging="True">
        <ClientSideEvents EndCallback="function(s, e) { PopEntPendidikan.Show(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <div align="center">
                    <table>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TxtActPendidikan" runat="server" Text="" 
                                BorderColor="White" BorderStyle="None" ForeColor="White" Width="30px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">No.</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtNoUrutPendidikan" runat="server" Width="30px" 
                                ClientInstanceName="TxtNoUrutPendidikan" Enabled="false" CssClass="font1">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>       
                    <tr>
                        <td align="left">Tingkat Pendidikan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxComboBox ID="TxtTgkPendidikan" runat="server" Theme="MetropolisBlue">
                                <Items>
                                    <dx:ListEditItem Text="SD" Value="SD" />
                                    <dx:ListEditItem Text="SMP" Value="SMP" />
                                    <dx:ListEditItem Text="SMA" Value="SMA" />
                                    <dx:ListEditItem Text="D1" Value="D1" />
                                    <dx:ListEditItem Text="D2" Value="D2" />
                                    <dx:ListEditItem Text="D3" Value="D3" />
                                    <dx:ListEditItem Text="S1" Value="S1" />
                                    <dx:ListEditItem Text="S2" Value="S2" />
                                    <dx:ListEditItem Text="S3" Value="S3" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Tanggal Dimulai</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxDateEdit ID="TxtPrdAwalPendidikan" runat="server" Theme="MetropolisBlue">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Tanggal Berakhir</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxDateEdit ID="TxtPrdAkhirPendidikan" runat="server" Theme="MetropolisBlue">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Nama Institusi</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtInstitusiPendidikan" runat="server" Width="100%" 
                                ClientInstanceName="TxtInstitusiPendidikan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Alamat Institusi</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtAlamatInstitusiPendidikan" runat="server" Width="100%" 
                                ClientInstanceName="TxtAlamatInstitusiPendidikan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Jurusan Pendidikan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtJurusanPendidikan" runat="server" Width="100%" 
                                ClientInstanceName="TxtJurusanPendidikan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Kelulusan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxComboBox ID="TxtLlsTdkLlsPendidikan" runat="server" Theme="MetropolisBlue">
                                <Items>
                                    <dx:ListEditItem Text="Lulus" Value="Lulus" />
                                    <dx:ListEditItem Text="Tidak Lulus" Value="Tidak Lulus" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Nilai</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtNilaiPendidikan" runat="server" Width="100%" 
                                ClientInstanceName="TxtNilaiPendidikan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">No Ijazah</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtNoIjazahPendidikan" runat="server" Width="100%" 
                                ClientInstanceName="TxtNoIjazahPendidikan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="4" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSavePendidikan" runat="server" Text="SIMPAN" CausesValidation="false"
                                Theme="MetropolisBlue" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancelPendidikan" runat="server" Text="BATAL"
                                Theme="MetropolisBlue" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) { PopEntPendidikan.Hide();}" />
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
    <dx:ASPxPopupControl ID="PopEntKetrampilan" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopEntKetrampilan"
        HeaderText="Data Entry Ketrampilan" PopupAnimationType="Fade" 
            Width="700px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue" AllowDragging="True">
        <ClientSideEvents EndCallback="function(s, e) { PopEntKetrampilan.Show(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                <div align="center">
                    <table>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TxtActKetrampilan" runat="server" Text="" 
                                BorderColor="White" BorderStyle="None" ForeColor="White" Width="30px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">No.</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtNoUrutKetrampilan" runat="server" Width="30px" 
                                ClientInstanceName="TxtNoUrutKetrampilan" Enabled="false" CssClass="font1">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>       
                    <tr>
                        <td align="left">Nama Ketrampilan/Kursus/Pelatihan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtNamaKetrampilan" runat="server" Width="100%" 
                                ClientInstanceName="TxtNamaKetrampilan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Tanggal Dimulai</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxDateEdit ID="TxtPrdAwalKetrampilan" runat="server" Theme="MetropolisBlue">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Tanggal Berakhir</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxDateEdit ID="TxtPrdAkhirKetrampilan" runat="server" Theme="MetropolisBlue">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Nama Sertifikat</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtNamaSertifikatKetrampilan" runat="server" Width="100%" 
                                ClientInstanceName="TxtNamaSertifikatKetrampilan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Level/Grade</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtGradeKetrampilan" runat="server" Width="100%" 
                                ClientInstanceName="TxtGradeKetrampilan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">No Sertifikat</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtNoSertifikatKetrampilan" runat="server" Width="100%" 
                                ClientInstanceName="TxtNoSertifikatKetrampilan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Nama Institusi</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtInstitusiKetrampilan" runat="server" Width="100%" 
                                ClientInstanceName="TxtInstitusiKetrampilan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="4" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSaveKetrampilan" runat="server" Text="SIMPAN" CausesValidation="false"
                                Theme="MetropolisBlue" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancelKetrampilan" runat="server" Text="BATAL"
                                Theme="MetropolisBlue" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) { PopEntKetrampilan.Hide();}" />
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
    <dx:ASPxPopupControl ID="PopEntRwytPekerjaan" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopEntRwytPekerjaan"
        HeaderText="Data Entry Riwayat Pekerjaan" PopupAnimationType="Fade" 
            Width="700px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue" AllowDragging="True">
        <ClientSideEvents EndCallback="function(s, e) { PopEntRwytPekerjaan.Show(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                <div align="center">
                    <table>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TxtActRwytPekerjaan" runat="server" Text="" 
                                BorderColor="White" BorderStyle="None" ForeColor="White" Width="30px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">No.</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtNoUrutRwytPekerjaan" runat="server" Width="30px" 
                                ClientInstanceName="TxtNoUrutRwytPekerjaan" Enabled="false" CssClass="font1">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>       
                    <tr>
                        <td align="left">Tanggal Dimulai</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxDateEdit ID="TxtPrdAwalRwytPekerjaan" runat="server" Theme="MetropolisBlue">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Tanggal Berakhir</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxDateEdit ID="TxtPrdAkhirRwytPekerjaan" runat="server" Theme="MetropolisBlue">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Nama Perusahaan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtPerusahaanRwytPekerjaan" runat="server" Width="100%" 
                                ClientInstanceName="TxtPerusahaanRwytPekerjaan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Alamat Perusahaan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtAlamatRwytPekerjaan" runat="server" Width="100%" 
                                ClientInstanceName="TxtAlamatRwytPekerjaan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Bidang Industri</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtIndustriRwytPekerjaan" runat="server" Width="100%" 
                                ClientInstanceName="TxtIndustriRwytPekerjaan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Jabatan Terakhir</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtJabatanRwytPekerjaan" runat="server" Width="100%" 
                                ClientInstanceName="TxtJabatanRwytPekerjaan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Lokasi Kerja</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtLokasiKerjaRwytPekerjaan" runat="server" Width="100%" 
                                ClientInstanceName="TxtLokasiKerjaRwytPekerjaan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Gaji Pokok</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtGajiRwytPekerjaan" runat="server" Width="100%" 
                                ClientInstanceName="TxtGajiRwytPekerjaan" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Fasilitas Tunjangan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxMemo ID="TxtTunjanganRwytPekerjaan" runat="server" Height="35px" Width="445px" 
                                CssClass="font1" MaxLength="255">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Uraian Pekerjaan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxMemo ID="TxtUraianRwytPekerjaan" runat="server" Height="35px" Width="445px" 
                                CssClass="font1" MaxLength="255">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="4" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSaveRwytPekerjaan" runat="server" Text="SIMPAN" CausesValidation="false"
                                Theme="MetropolisBlue" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancelRwytPekerjaan" runat="server" Text="BATAL"
                                Theme="MetropolisBlue" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) { PopEntRwytPekerjaan.Hide();}" />
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
    <dx:ASPxPopupControl ID="PopEntRwytPekerjaanMinarta" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopEntRwytPekerjaanMinarta"
        HeaderText="Data Entry Riwayat Pekerjaan Minarta" PopupAnimationType="Fade" 
            Width="700px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue" AllowDragging="True">
        <ClientSideEvents EndCallback="function(s, e) { PopEntRwytPekerjaanMinarta.Show(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl6" runat="server">
                <div align="center">
                    <table>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TxtActRwytPekerjaanMinarta" runat="server" Text="" 
                                BorderColor="White" BorderStyle="None" ForeColor="White" Width="30px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">No.</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtNoUrutRwytPekerjaanMinarta" runat="server" Width="30px" 
                                ClientInstanceName="TxtNoUrutRwytPekerjaanMinarta" Enabled="false" CssClass="font1">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>       
                    <tr>
                        <td align="left">Tanggal Dimulai</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxDateEdit ID="TxtPrdAwalRwytPekerjaanMinarta" runat="server" Theme="MetropolisBlue">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Tanggal Berakhir</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxDateEdit ID="TxtPrdAkhirRwytPekerjaanMinarta" runat="server" Theme="MetropolisBlue">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Divisi</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtDivisiRwytPekerjaanMinarta" runat="server" Width="100%" 
                                ClientInstanceName="TxtDivisiRwytPekerjaanMinarta" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Subdivisi</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtSubdivisiRwytPekerjaanMinarta" runat="server" Width="100%" 
                                ClientInstanceName="TxtSubdivisiRwytPekerjaanMinarta" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Lokasi Kerja</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtLokasiKerjaRwytPekerjaanMinarta" runat="server" Width="100%" 
                                ClientInstanceName="TxtLokasiKerjaRwytPekerjaanMinarta" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Jabatan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtJabatanRwytPekerjaanMinarta" runat="server" Width="100%" 
                                ClientInstanceName="TxtJabatanRwytPekerjaanMinarta" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Golongan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtGolonganRwytPekerjaanMinarta" runat="server" Width="100%" 
                                ClientInstanceName="TxtGolonganRwytPekerjaanMinarta" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Grade</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtGradeRwytPekerjaanMinarta" runat="server" Width="100%" 
                                ClientInstanceName="TxtGradeRwytPekerjaanMinarta" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Gaji Pokok</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtGajiRwytPekerjaanMinarta" runat="server" Width="100%" 
                                ClientInstanceName="TxtGajiRwytPekerjaanMinarta" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Fasilitas Tunjangan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxMemo ID="TxtTunjanganRwytPekerjaanMinarta" runat="server" Height="35px" Width="445px" 
                                CssClass="font1" MaxLength="255">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">KPI</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtKPIRwytPekerjaanMinarta" runat="server" Width="100%" 
                                ClientInstanceName="TxtKPIRwytPekerjaanMinarta" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Tes Kesehatan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxComboBox ID="TxtTesKesehatanRwytPekerjaanMinarta" runat="server" ValueType="System.String" 
                                CssClass="font1" ClientInstanceName="TxtTesKesehatanRwytPekerjaanMinarta" Theme="MetropolisBlue">
                                <Items>
                                    <dx:ListEditItem Text="Sudah Pernah" Value="Sudah Pernah" />
                                    <dx:ListEditItem Text="Belum Pernah" Value="Belum Pernah" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Hasil Tes Kesehatan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxMemo ID="TxtHasilKesehatanRwytPekerjaanMinarta" runat="server" Height="35px" Width="445px" 
                                CssClass="font1" MaxLength="255">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Tes Psikologi</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxComboBox ID="TxtTesPsikologiRwytPekerjaanMinarta" runat="server" ValueType="System.String" 
                                CssClass="font1" ClientInstanceName="TxtTesPsikologiRwytPekerjaanMinarta" Theme="MetropolisBlue">
                                <Items>
                                    <dx:ListEditItem Text="Sudah Pernah" Value="Sudah Pernah" />
                                    <dx:ListEditItem Text="Belum Pernah" Value="Belum Pernah" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Hasil Tes Psikologi</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxMemo ID="TxtHasilPsikologiRwytPekerjaanMinarta" runat="server" Height="35px" Width="445px" 
                                CssClass="font1" MaxLength="255">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Atasan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxTextBox ID="TxtAtasanRwytPekerjaanMinarta" runat="server" Width="100%" 
                                ClientInstanceName="TxtAtasanRwytPekerjaanMinarta" MaxLength="50">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Uraian Pekerjaan</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxMemo ID="TxtUraianRwytPekerjaanMinarta" runat="server" Height="35px" Width="445px" 
                                CssClass="font1" MaxLength="255">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="4" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSaveRwytPekerjaanMinarta" runat="server" Text="SIMPAN" CausesValidation="false"
                                Theme="MetropolisBlue" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancelRwytPekerjaanMinarta" runat="server" Text="BATAL"
                                Theme="MetropolisBlue" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) { PopEntRwytPekerjaanMinarta.Hide();}" />
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
                <asp:label ID="LblJudul" runat="server" Text="Form Entry Karyawan"></asp:label>
            </td>
        </tr>
    </table>
</div>
<div class="font1">
    <table>
        <tr>
            <td>
                <asp:Label ID="LblAction" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="11" align="left" style="padding-top:5px;">
                <dx:ASPxButton ID="BtnSaveDataEntry" runat="server" Text="SIMPAN" 
                    Theme="MetropolisBlue" Width="75px">
                </dx:ASPxButton>
                <dx:ASPxButton ID="BtnCancelDataEntry" runat="server" Text="BATAL" CausesValidation="False"
                    Theme="MetropolisBlue" Width="75px">
                </dx:ASPxButton>          
            </td>
        </tr>
        <tr>
            <td colspan="5" align="center" bgcolor="silver" style="height:20px; font-weight:bold">
                Data Pribadi
            </td>
            <td>
            </td>
            <td colspan="5" align="center" bgcolor="silver" style="height:20px; font-weight:bold">
                Identitas Personal
            </td>
        </tr>
        <tr>
            <td>
                NIK
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtNIK" Theme="MetropolisBlue" 
                    Width="100px">
                </dx:aspxTextBox>
            </td>
            <td>
            </td>
            <td>
                KTP
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtNoKTP" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Nama
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtNama" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
            <td>
            </td>
            <td>
                Berlaku dari
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <table>
                <tr>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAwalKTP" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                    <td>
                         s/d
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAkhirKTP" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Jenis Kelamin
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:ASPxRadioButtonList runat="server" ID="RblJenisKelamin" 
                    RepeatDirection="Horizontal" Theme="MetropolisBlue">
                    <Items>
                        <dx:ListEditItem Text="Pria" Value="L" />
                        <dx:ListEditItem Text="Wanita" Value="P" />
                    </Items>
                </dx:ASPxRadioButtonList>
            </td>
            <td>
            </td>
            <td>
                Diterbitkan oleh
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <table>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="TxtDiterbitkanOlehKTP" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Tempat, Tanggal Lahir
            </td>
            <td>
                :
            </td>
            <td>
                <dx:aspxTextBox runat="server" id="TxtTempatLahir" Theme="MetropolisBlue" 
                    Width="100%" >
                </dx:aspxTextBox>
            </td>
            <td colspan="2">
                <dx:ASPxDateEdit runat="server" ID="TxtTanggalLahir" Theme="MetropolisBlue" Width="100%">
                  </dx:ASPxDateEdit>
            </td>
            <td>
            </td>
            <td>
                Passport
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtNoPassport" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Kewarganegaraan
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtWN" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
            <td>
            </td>
            <td>
                Berlaku dari
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <table>
                <tr>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAwalPassport" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                    <td>
                         s/d
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAkhirPassport" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Status Pernikahan
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <asp:UpdatePanel runat="server" ID="UpdPanelStsNikah">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RblStsNikah" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <table width="100%">
                            <tr>
                                <td style="padding: 0px">
                                    <dx:ASPxRadioButtonList runat="server" ID="RblStsNikah" Theme="MetropolisBlue"
                                        RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RblStsNikah_SelectedIndexChanged">
                                        <Items>
                                            <dx:ListEditItem Text="Lajang" Value="Lajang" />
                                            <dx:ListEditItem Text="Janda/Duda" Value="Janda/Duda" />
                                            <dx:ListEditItem Text="Menikah" Value="Menikah" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit runat="server" ID="TxtTglNikah" ClientInstanceName="TxtTglNikah" Theme="MetropolisBlue" Visible="false"
                                        Width="100%">
                                    </dx:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
            </td>
            <td>
                Diterbitkan oleh
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <table>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="TxtDiterbitkanOlehPassport" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Agama
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:ASPxRadioButtonList runat="server" ID="RblAgama" 
                    RepeatDirection="Horizontal" Theme="MetropolisBlue">
                    <Items>
                        <dx:ListEditItem Text="Islam" Value="Islam" />
                        <dx:ListEditItem Text="Kristen" Value="Kristen" />
                        <dx:ListEditItem Text="Katolik" Value="Katolik" />
                        <dx:ListEditItem Text="Hindu" Value="Hindu" />
                        <dx:ListEditItem Text="Buddha" Value="Buddha" />
                    </Items>
                </dx:ASPxRadioButtonList>
            </td>
            <td>
            </td>
            <td>
                No NPWP
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtNoNPWP" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Divisi
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtDivisi" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
            <td>
            </td>
            <td>
                Berlaku dari
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <table>
                <tr>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAwalNPWP" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                    <td>
                         s/d
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAkhirNPWP" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Subdivisi
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtSubdivisi" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
            <td>
            </td>
            <td>
                Diterbitkan oleh
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <table>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="TxtDiterbitkanOlehNPWP" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Jabatan
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtJabatan" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
            <td>
            </td>
            <td>
                No KK
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtNoKK" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Golongan
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtGolongan" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
            <td>
            </td>
            <td>
                Berlaku dari
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <table>
                <tr>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAwalKK" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                    <td>
                         s/d
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAkhirKK" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Grade
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtGrade" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
            <td>
            </td>
            <td>
                Diterbitkan oleh
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <table>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="TxtDiterbitkanOlehKK" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Tanggal Mulai Menjabat
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:ASPxDateEdit ID="TxtPrdAwal" runat="server" Theme="MetropolisBlue">
                </dx:ASPxDateEdit>
            </td>
            <td>
            </td>
            <td>
                SIM A
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtNoSIMA" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Uraian Pekerjaan
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:ASPxMemo ID="TxtUraian" runat="server" Theme="MetropolisBlue" Width="100%">
                </dx:ASPxMemo>
            </td>
            <td>
            </td>
            <td>
                Berlaku dari
            </td>
            <td>
                :
            </td>
            <td colspan="5">
                <table>
                <tr>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAwalSIMA" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                    <td>
                         s/d
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAkhirSIMA" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Alamat Sesuai Kartu Identitas
            </td>
            <td class="style1">
                :
            </td>
            <td colspan="3" class="style1">
                <dx:ASPxMemo runat="server" ID="TxtAlamat" Theme="MetropolisBlue" Width="100%">
                </dx:ASPxMemo>
            </td>
            <td>
            </td>
            <td>
                Diterbitkan oleh
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <table>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="TxtDiterbitkanOlehSIMA" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Provinsi
            </td>
            <td class="style1">
                :
            </td>
            <td colspan="3" class="style1">
                <dx:ASPxTextBox ID="TxtProvinsi" runat="server" Theme="MetropolisBlue">
                </dx:ASPxTextBox>
            </td>
            <td>
            </td>
            <td>
                SIM B
            </td>
            <td>
                :
            </td>
            <td colspan="3" class="style1">
                <dx:aspxTextBox runat="server" id="TxtNoSIMB" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                Kota
            </td>
            <td class="style1">
                :
            </td>
            <td colspan="3" class="style1">
                <dx:ASPxTextBox ID="TxtKota" runat="server" Theme="MetropolisBlue">
                </dx:ASPxTextBox>
            </td>
            <td>
            </td>
            <td>
                Berlaku dari
            </td>
            <td>
                :
            </td>
            <td colspan="5">
                <table>
                <tr>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAwalSIMB" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                    <td>
                         s/d
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAkhirSIMB" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Alamat Surat Menyurat
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:ASPxMemo runat="server" ID="TxtAlamatSurat" Theme="MetropolisBlue" Width="100%">
                </dx:ASPxMemo>
            </td>
            <td>
            </td>
            <td>
                Diterbitkan oleh
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <table>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="TxtDiterbitkanOlehSIMB" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Alamat Email
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtEmail" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
            <td>
            </td>
            <td>
                SIM C
            </td>
            <td>
                :
            </td>
            <td colspan="3" class="style1">
                <dx:aspxTextBox runat="server" id="TxtNoSIMC" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
        </tr>
        <tr>
            <td>
                No Telp/HP
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtNoTelp" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
            <td>
            </td>
            <td>
                Berlaku dari
            </td>
            <td>
                :
            </td>
            <td colspan="5">
                <table>
                <tr>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAwalSIMC" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                    <td>
                         s/d
                    </td>
                    <td>
                        <dx:ASPxDateEdit ID="TxtPrdAkhirSIMC" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Lokasi Kerja Saat Ini
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <dx:aspxTextBox runat="server" id="TxtLokasiKerja" Theme="MetropolisBlue" 
                    Width="100%">
                </dx:aspxTextBox>
            </td>
            <td>
            </td>
            <td>
                Diterbitkan oleh
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <table>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="TxtDiterbitkanOlehSIMC" runat="server" Theme="MetropolisBlue">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                Foto
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <asp:FileUpload runat="server" ID="PasFoto" width="100%"/>
            </td>
            <td>
            </td>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
            <td colspan="3">
                <asp:Image runat="server" ID="PasFotoDefault" Height="240px" Width="240px" />
            </td>
            <td>
            </td>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td>
                Scan KTP
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <asp:FileUpload runat="server" ID="FileKTP" width="100%"/>
            </td>
            <td>
            </td>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td>
                Scan Kartu Keluarga
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <asp:FileUpload runat="server" ID="FileKK" width="100%"/>
            </td>
            <td>
            </td>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td>
                Scan NPWP
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <asp:FileUpload runat="server" ID="FileNPWP" width="100%"/>
            </td>
            <td>
            </td>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td>
                Scan Ijazah Pendidikan Terakhir
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <asp:FileUpload runat="server" ID="FileIjazah" width="100%"/>
            </td>
            <td>
            </td>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td>
                Scan Transkrip Nilai
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <asp:FileUpload runat="server" ID="FileTranskripNilai" width="100%"/>
            </td>
            <td>
            </td>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td>
                Scan Sertifikat Keahlian
            </td>
            <td>
                :
            </td>
            <td colspan="3">
                <asp:FileUpload runat="server" ID="FileSertifikat" AllowMultiple="true" width="100%"/>
            </td>
            <td>
            </td>
            <td colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="11" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:5px;">
            </td>
        </tr>
        <tr>
            <td colspan="11" align="center" bgcolor="silver" style="height:20px; font-weight:bold">
                Data Keluarga
            </td>
        </tr>
        <tr>
            <td style="border: 2px solid #C0C0C0" colspan="11">
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="BtnPopUpKeluarga" runat="server" Text="TAMBAH" Theme="MetropolisBlue" AutoPostBack="false" CausesValidation="false"></dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridDataKeluarga" runat="server" AutoGenerateColumns="False"               
                                CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                                PageSize="20" ShowFooter="True" 
                                ShowHeaderWhenEmpty="True">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="NoUrutKeluarga" HeaderText="No."  HeaderStyle-Width="35px" ItemStyle-Width = "35px" ItemStyle-HorizontalAlign="Center">     
                                    </asp:BoundField>              
                                    <asp:BoundField DataField="HubKeluarga" HeaderText="Hubungan Keluarga"  HeaderStyle-Width="150px" ItemStyle-Width = "150px" ItemStyle-HorizontalAlign="Center">     
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NamaKeluarga" HeaderText="Nama" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JenisKelaminKeluarga" HeaderText="Jenis Kelamin" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TglLahirKeluarga" HeaderText="Tanggal Lahir" HeaderStyle-Width="150px" ItemStyle-Width = "150px" DataFormatString="{0:d}">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PekerjaanKeluarga" HeaderText="Pekerjaan" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PerusahaanKeluarga" HeaderText="Perusahaan" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField> 
                                    <asp:ButtonField CommandName="BtnUpdKeluarga" Text="SELECT" HeaderStyle-Width="45px"/>                                          
                                    <asp:ButtonField CommandName="BtnDelKeluarga" Text="DELETE" HeaderStyle-Width="45px"/>                                                                 
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
        <tr>
            <td colspan="11" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:5px;">
            </td>
        </tr>
        <tr>
            <td colspan="11" align="center" bgcolor="silver" style="height:20px; font-weight:bold">
                Data Pendidikan
            </td>
        </tr>
        <tr>
            <td style="border: 2px solid #C0C0C0" colspan="11">
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="BtnPopUpPendidikan" runat="server" Text="TAMBAH" Theme="MetropolisBlue" AutoPostBack="false" CausesValidation="false"></dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridDataPendidikan" runat="server" AutoGenerateColumns="False"               
                                CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                                PageSize="20" ShowFooter="True" 
                                ShowHeaderWhenEmpty="True">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="NoUrutPendidikan" HeaderText="No."  HeaderStyle-Width="35px" ItemStyle-Width = "35px" ItemStyle-HorizontalAlign="Center">     
                                    </asp:BoundField>              
                                    <asp:BoundField DataField="TgkPendidikan" HeaderText="Tingkat Pendidikan"  HeaderStyle-Width="80px" ItemStyle-Width = "80px" ItemStyle-HorizontalAlign="Center">     
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PrdAwalPendidikan" HeaderText="Periode Dimulai" HeaderStyle-Width="80px" ItemStyle-Width = "80px" DataFormatString="{0:d}">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PrdAkhirPendidikan" HeaderText="Periode Berakhir" HeaderStyle-Width="80px" ItemStyle-Width = "80px" DataFormatString="{0:d}">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InstitusiPendidikan" HeaderText="Nama Institusi" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AlamatInstitusiPendidikan" HeaderText="Alamat Institusi" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JurusanPendidikan" HeaderText="Jurusan" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LlsTdkLlsPendidikan" HeaderText="Kelulusan" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NilaiPendidikan" HeaderText="Nilai (IPK)" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NoIjazahPendidikan" HeaderText="No. Ijazah" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField> 
                                    <asp:ButtonField CommandName="BtnUpdPendidikan" Text="SELECT" HeaderStyle-Width="45px"/>                                          
                                    <asp:ButtonField CommandName="BtnDelPendidikan" Text="DELETE" HeaderStyle-Width="45px"/>                                                                 
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
        <tr>
            <td colspan="11" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:5px;">
            </td>
        </tr>
        <tr>
            <td colspan="11" align="center" bgcolor="silver" style="height:20px; font-weight:bold">
                Data Ketrampilan
            </td>
        </tr>
        <tr>
            <td style="border: 2px solid #C0C0C0" colspan="11">
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="BtnPopUpKetrampilan" runat="server" Text="TAMBAH" Theme="MetropolisBlue" AutoPostBack="false" CausesValidation="false"></dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridDataKetrampilan" runat="server" AutoGenerateColumns="False"               
                                CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                                PageSize="20" ShowFooter="True" 
                                ShowHeaderWhenEmpty="True">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="NoUrutKetrampilan" HeaderText="No."  HeaderStyle-Width="35px" ItemStyle-Width = "35px" ItemStyle-HorizontalAlign="Center">     
                                    </asp:BoundField>              
                                    <asp:BoundField DataField="NamaKetrampilan" HeaderText="Nama Keahlian/Ketrampilan/Kursus/Pelatihan"  HeaderStyle-Width="200px" ItemStyle-Width = "200px">     
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PrdAwalKetrampilan" HeaderText="Periode Dimulai" HeaderStyle-Width="80px" ItemStyle-Width = "80px" DataFormatString="{0:d}">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PrdAkhirKetrampilan" HeaderText="Periode Berakhir" HeaderStyle-Width="80px" ItemStyle-Width = "80px" DataFormatString="{0:d}">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NamaSertifikatKetrampilan" HeaderText="Nama Sertifikat" HeaderStyle-Width="200px" ItemStyle-Width = "200px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GradeKetrampilan" HeaderText="Level/Grade" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NoSertifikatKetrampilan" HeaderText="No. Sertifikat" HeaderStyle-Width="200px" ItemStyle-Width = "200px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="InstitusiKetrampilan" HeaderText="Nama Institusi" HeaderStyle-Width="200px" ItemStyle-Width = "200px">                        
                                    </asp:BoundField>  
                                    <asp:ButtonField CommandName="BtnUpdKetrampilan" Text="SELECT" HeaderStyle-Width="45px"/>                                          
                                    <asp:ButtonField CommandName="BtnDelKetrampilan" Text="DELETE" HeaderStyle-Width="45px"/>                                                                 
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
        <tr>
            <td colspan="11" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:5px;">
            </td>
        </tr>
        <tr>
            <td colspan="11" align="center" bgcolor="silver" style="height:20px; font-weight:bold">
                Data Riwayat Pekerjaan
            </td>
        </tr>
        <tr>
            <td style="border: 2px solid #C0C0C0" colspan="11">
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="BtnPopUpRwytPekerjaan" runat="server" Text="TAMBAH" Theme="MetropolisBlue" AutoPostBack="false" CausesValidation="false"></dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridDataRwytPekerjaan" runat="server" AutoGenerateColumns="False"               
                                CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                                PageSize="20" ShowFooter="True" 
                                ShowHeaderWhenEmpty="True">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="NoUrutRwytPekerjaan" HeaderText="No."  HeaderStyle-Width="35px" ItemStyle-Width = "35px" ItemStyle-HorizontalAlign="Center">     
                                    </asp:BoundField>              
                                    <asp:BoundField DataField="PrdAwalRwytPekerjaan" HeaderText="Periode Dimulai"  HeaderStyle-Width="80px" ItemStyle-Width = "80px" DataFormatString="{0:d}">     
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PrdAkhirRwytPekerjaan" HeaderText="Periode Berakhir" HeaderStyle-Width="80px" ItemStyle-Width = "80px" DataFormatString="{0:d}">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PerusahaanRwytPekerjaan" HeaderText="Nama Perusahaan" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AlamatRwytPekerjaan" HeaderText="Alamat Perusahaan" HeaderStyle-Width="200px" ItemStyle-Width = "200px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="IndustriRwytPekerjaan" HeaderText="Bidang Industri" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JabatanRwytPekerjaan" HeaderText="Jabatan Terakhir" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LokasiKerjaRwytPekerjaan" HeaderText="Lokasi Kerja" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="GajiRwytPekerjaan" HeaderText="Gaji Pokok" HeaderStyle-Width="80px" ItemStyle-Width = "80px" DataFormatString="{0:N}">                        
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="TunjanganRwytPekerjaan" HeaderText="Fasilitas Tunjangan" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="UraianRwytPekerjaan" HeaderText="Uraian Pekerjaan" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField>  
                                    <asp:ButtonField CommandName="BtnUpdRwytPekerjaan" Text="SELECT" HeaderStyle-Width="45px"/>                                          
                                    <asp:ButtonField CommandName="BtnDelRwytPekerjaan" Text="DELETE" HeaderStyle-Width="45px"/>                                                                 
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
        <tr>
            <td colspan="11" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:5px;">
            </td>
        </tr>
        <tr>
            <td colspan="11" align="center" bgcolor="silver" style="height:20px; font-weight:bold">
                Data Riwayat Pekerjaan Minarta
            </td>
        </tr>
        <tr>
            <td style="border: 2px solid #C0C0C0" colspan="11">
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxButton ID="BtnPopUpRwytPekerjaanMinarta" runat="server" Text="TAMBAH" Theme="MetropolisBlue" AutoPostBack="false" CausesValidation="false"></dx:ASPxButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridDataRwytPekerjaanMinarta" runat="server" AutoGenerateColumns="False"               
                                CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                                PageSize="20" ShowFooter="True" 
                                ShowHeaderWhenEmpty="True">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="NoUrutRwytPekerjaanMinarta" HeaderText="No."  HeaderStyle-Width="35px" ItemStyle-Width = "35px" ItemStyle-HorizontalAlign="Center">     
                                    </asp:BoundField>              
                                    <asp:BoundField DataField="PrdAwalRwytPekerjaanMinarta" HeaderText="Periode Dimulai"  HeaderStyle-Width="150px" ItemStyle-Width = "150px" DataFormatString="{0:d}">     
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PrdAkhirRwytPekerjaanMinarta" HeaderText="Periode Berakhir" HeaderStyle-Width="150px" ItemStyle-Width = "150px" DataFormatString="{0:d}">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="DivisiRwytPekerjaanMinarta" HeaderText="Divisi" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubdivisiRwytPekerjaanMinarta" HeaderText="Subdivisi" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LokasiKerjaRwytPekerjaanMinarta" HeaderText="Lokasi Kerja" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JabatanRwytPekerjaanMinarta" HeaderText="Jabatan" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GolonganRwytPekerjaanMinarta" HeaderText="Golongan" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="GradeRwytPekerjaanMinarta" HeaderText="Grade" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="GajiRwytPekerjaanMinarta" HeaderText="Gaji Pokok" HeaderStyle-Width="150px" ItemStyle-Width = "150px" DataFormatString="{0:N}">                        
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="TunjanganRwytPekerjaanMinarta" HeaderText="Fasilitas Tunjangan" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="KPIRwytPekerjaanMinarta" HeaderText="Nilai KPI" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="TesKesehatanRwytPekerjaanMinarta" HeaderText="Tes Kesehatan" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="HasilKesehatanRwytPekerjaanMinarta" HeaderText="Hasil Tes Kesehatan" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="TesPsikologiRwytPekerjaanMinarta" HeaderText="Tes Psikologi" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="HasilPsikologiRwytPekerjaanMinarta" HeaderText="Hasil Tes Psikologi" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="AtasanRwytPekerjaanMinarta" HeaderText="Atasan" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField> 
                                    <asp:BoundField DataField="UraianRwytPekerjaanMinarta" HeaderText="Uraian Pekerjaan" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                        
                                    </asp:BoundField>  
                                    <asp:ButtonField CommandName="BtnUpdRwytPekerjaanMinarta" Text="SELECT" HeaderStyle-Width="45px"/>                                          
                                    <asp:ButtonField CommandName="BtnDelRwytPekerjaanMinarta" Text="DELETE" HeaderStyle-Width="45px"/>                                                                 
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
</div>
</asp:Content>
