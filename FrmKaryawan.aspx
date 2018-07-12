<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmKaryawan.aspx.vb" Inherits="AIS.FrmKaryawan" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnGridFocusedRowChanged() {
//            LblDivisi.SetText("Loading...");
//            LblSubdivisi.SetText("Loading...");
//            LblJabatan.SetText("Loading...");
//            LblGolongan.SetText("Loading...");
//            LblGrade.SetText("Loading...");
//            LblUraianPekerjaan.SetText("Loading...");
            GridMaster.GetRowValues(GridMaster.GetFocusedRowIndex(), 'NIK;Alamat;NoTelp;Email;Divisi;Sub Divisi;Jabatan;Golongan;Grade;Uraian Pekerjaan;Foto', OnGetRowValues);
//            Creates an array to store data (Pendidikan, Ketrampilan dll) based on RowValues NIK
//            Executes function to SetText based on this array (used the OnGetRowValues() way of doing things)
        }
        function OnGetRowValues(values) {
            LblAlamat.SetText(values[1]);
            LblNoTelp.SetText(values[2]);
            LblEmail.SetText(values[3]);
            LblDivisi.SetText(values[4]);
            LblSubdivisi.SetText(values[5]);
            LblJabatan.SetText(values[6]);
            LblGolongan.SetText(values[7]);
            LblGrade.SetText(values[8]);
            LblUraianPekerjaan.SetText(values[9]);
            LblPasFoto.SetImageUrl(values[10]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
    <dx:ASPxPopupControl ID="PopInformasi" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopKonfirmasi"
        HeaderText="Status Karyawan" PopupAnimationType="Fade" 
            Width="700px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue" AllowDragging="True">
        <ClientSideEvents EndCallback="function(s, e) { PopInformasi.Show(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <div align="center">
                    <table>
                    <tr>
                        <td align="center">
                            <dx:ASPxLabel runat="server" ID="LblInformasi" Theme="MetropolisBlue">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="4" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">                       
                            <dx:ASPxButton ID="BtnInformasi" runat="server" Text="OK"
                                Theme="MetropolisBlue" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) { PopKonfirmasi.Hide();}" />
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
    <dx:ASPxPopupControl ID="PopKonfirmasi" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopKonfirmasi"
        HeaderText="Perubahan Status Karyawan" PopupAnimationType="Fade" 
            Width="700px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue" AllowDragging="True">
        <ClientSideEvents EndCallback="function(s, e) { PopKonfirmasi.Show(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div align="center">
                    <table>
                    <tr>
                        <td align="center">
                            <dx:ASPxLabel runat="server" ID="LblKonfirmasi" Theme="MetropolisBlue">
                            </dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="4" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnKonfirmasi" runat="server" Text="LANJUTKAN" CausesValidation="false"
                                Theme="MetropolisBlue" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL"
                                Theme="MetropolisBlue" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) { PopKonfirmasi.Hide();}" />
                            </dx:ASPxButton>   
                        </td>
                    </tr>
                    </table>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div>
<div class="title" style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">
    <%--<dx:ASPxPopupControl ID="PopUpTambahKaryawan" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopEntry"
        HeaderText="Tambah Data Karyawan" PopupAnimationType="Fade" 
            Width="700px" PopupElementID="PopUpTambahKaryawan" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <div align="center">
                    <table style="width: 100%">
                        <tr>
                            <td align="left">
                                NIK
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <dx:ASPxTextBox ID="TxtNIK" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Nama
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <dx:ASPxTextBox ID="TxtNama" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Jenis Kelamin
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <dx:ASPxRadioButtonList ID="ASPxRadioButtonList3" runat="server" 
                                    Theme="MetropolisBlue" RepeatDirection="Horizontal">
                                    <Items>
                                        <dx:ListEditItem Text="Laki-Laki" Value="Laki-Laki" />
                                        <dx:ListEditItem Text="Perempuan" Value="Perempuan" />
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Tempat Lahir
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <dx:ASPxTextBox ID="TxtTmpLahir" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Tanggal Lahir
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <dx:ASPxDateEdit ID="TxtTglLahir" runat="server"
                                    DisplayFormatString="dd-MMM-yyyy" Theme="MetropolisBlue">
                                    <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                         <tr>
                            <td align="left">
                                Kewarganegaraan
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <dx:ASPxTextBox ID="TxtWN" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Status Perkawinan
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                &nbsp;<dx:ASPxRadioButtonList ID="ASPxRadioButtonList1" runat="server" 
                                    RepeatDirection="Horizontal" Theme="MetropolisBlue">
                                    <Items>
                                        <dx:ListEditItem Text="Lajang" Value="Lajang" />
                                        <dx:ListEditItem Text="Menikah" Value="Menikah" />
                                        <dx:ListEditItem Text="Janda/Duda" Value="Janda/Duda" />
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Agama
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <dx:ASPxRadioButtonList ID="ASPxRadioButtonList2" runat="server" 
                                    EnableTheming="True" RepeatDirection="Horizontal" Theme="MetropolisBlue">
                                    <Items>
                                        <dx:ListEditItem Text="Islam" Value="Islam" />
                                        <dx:ListEditItem Text="Hindu" Value="Hindu" />
                                        <dx:ListEditItem Text="Buddha" Value="Buddha" />
                                        <dx:ListEditItem Text="Kristen" Value="Kristen" />
                                        <dx:ListEditItem Text="Katolik" Value="Katolik" />
                                        <dx:ListEditItem Text="Lainnya" Value="Lainnya" />
                                    </Items>
                                </dx:ASPxRadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Alamat Sesuai Kartu Identitas
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <dx:ASPxMemo ID="TxtAlamat" runat="server" Height="71px" MaxLength="200" 
                                    Theme="MetropolisBlue" Width="170px">
                                </dx:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Alamat Domisili Sekarang
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <dx:ASPxMemo ID="TxtAlamatSurat" runat="server" Height="71px" MaxLength="200" 
                                    Width="170px">
                                </dx:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Email
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <dx:ASPxTextBox ID="TxtEmail" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                No Telephone/HP
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <dx:ASPxTextBox ID="TxtNoTelp" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                Lokasi kerja saat ini
                            </td>
                            <td>
                                :
                            </td>
                            <td align="left">
                                <dx:ASPxTextBox ID="TxtLokasiKerja" runat="server">
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                                <dx:ASPxButton ID="BtnSimpan" runat="server" Text="SIMPAN"
                                    Theme="MetropolisBlue" Width="80px" UseSubmitBehavior="false">
                                </dx:ASPxButton>                       
                                <dx:ASPxButton ID="BtnBatal" runat="server" Text="BATAL" CausesValidation="false"
                                    Theme="MetropolisBlue" Width="80px"  UseSubmitBehavior="false">
                                </dx:ASPxButton>   
                            </td>
                        </tr>
                    </table>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl--%>
    Data Karyawan
</div>
<div>
    <table>
        <tr>
            <td>
                <dx:ASPxButton ID="BtnTambah" runat="server" Text="Tambah" Width="80px" Theme="MetropolisBlue">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td>
                <dx:ASPxGridView ID="GridMaster" ClientInstanceName="GridMaster" runat="server" KeyFieldName="NIK" PreviewFieldName="Divisi" EnableRowCache="false" AutoGenerateColumns="true" Theme="MetropolisBlue" Width="100%" OnDataBound="GridMaster_DataBound" OnCustomButtonCallback="GridMaster_CustomButtonCallback">
                <SettingsAdaptivity AdaptivityMode="HideDataCells" />
                <SettingsPager PageSize="5"></SettingsPager>
                <SettingsBehavior AllowFocusedRow="true" />
                <ClientSideEvents FocusedRowChanged="function(s, e) {OnGridFocusedRowChanged();GridMinarta.PerformCallback(s.GetFocusedRowIndex());GridRwytPekerjaan.PerformCallback(s.GetFocusedRowIndex());GridPendidikan.PerformCallback(s.GetFocusedRowIndex());GridKetrampilan.PerformCallback(s.GetFocusedRowIndex());GridIdentitas.PerformCallback(s.GetFocusedRowIndex());GridKeluarga.PerformCallback(s.GetFocusedRowIndex());}" />
                </dx:ASPxGridView>
            </td>
        </tr>
    </table>
    
    <table style="width: 100%; height: 200px">
        <tr>
            <td align="center" style="width:240px; vertical-align:top">
                <dx:ASPxImage runat="server" ID="PasFoto" Width="120px" Height="120px" Theme="MetropolisBlue" ClientInstanceName="LblPasFoto" ImageUrl="~/Images/PasFotoDefault.jpg">
                </dx:ASPxImage>
            </td>
            <td style="width:5px">
            </td>
            <td style="width:200px; vertical-align:top" >
                <table>
                    <tr>
                        <td>
                            <b>Alamat:</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel1" ClientInstanceName="LblAlamat" runat="server" Text="" Width="150px"></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>No Telp:</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" ClientInstanceName="LblNoTelp" runat="server" Text=""></dx:ASPxLabel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Email:</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel3" ClientInstanceName="LblEmail" runat="server" Text=""></dx:ASPxLabel>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width:5px">
            </td>
            <td style="vertical-align: top">
                <dx:ASPxPageControl ID="ASPxPageControl1" runat="server" ActiveTabIndex="0" 
                    EnableTheming="True" TabPosition="Top" Theme="MetropolisBlue">
                    <TabPages>
                        <dx:TabPage Text="Pekerjaan">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server">
                                <b>Divisi:</b><br />
                                <dx:ASPxLabel ID="ASPxLabel4" ClientInstanceName="LblDivisi" runat="server" Text=""></dx:ASPxLabel>
                                <br /><br />

                                <b>Sub Divisi:</b><br />
                                <dx:ASPxLabel ID="ASPxLabel5" ClientInstanceName="LblSubdivisi" runat="server" Text=""></dx:ASPxLabel>
                                <br /><br />

                                <b>Jabatan:</b><br />
                                <dx:ASPxLabel ID="ASPxLabel6" ClientInstanceName="LblJabatan" runat="server" Text=""></dx:ASPxLabel>
                                <br /><br />

                                <b>Golongan:</b><br />
                                <dx:ASPxLabel ID="ASPxLabel7" ClientInstanceName="LblGolongan" runat="server" Text=""></dx:ASPxLabel>
                                <br /><br />

                                <b>Grade:</b><br />
                                <dx:ASPxLabel ID="ASPxLabel8" ClientInstanceName="LblGrade" runat="server" Text=""></dx:ASPxLabel>
                                <br /><br />
                                
                                <b>Periode:</b><br />
                                LabelPeriode<br /><br />

                                <b>Uraian Pekerjaan:</b><br />
                                <dx:ASPxLabel ID="ASPxLabel9" ClientInstanceName="LblUraianPekerjaan" runat="server" Text=""></dx:ASPxLabel>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Riwayat di Minarta">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <dx:ASPxGridView ID="GridMinarta" ClientInstanceName="GridMinarta" runat="server" KeyFieldName="NIK" EnableRowCache="true" AutoGenerateColumns="true" Theme="MetropolisBlue" Width="100%" OnCustomCallback="GridMinarta_CustomCallback" OnDataBound="GridMinarta_DataBound">
                                    <SettingsBehavior AllowFocusedRow="true" />
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Riwayat Pekerjaan">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <dx:ASPxGridView ID="GridRwytPekerjaan" ClientInstanceName="GridRwytPekerjaan" runat="server" KeyFieldName="NIK" EnableRowCache="true" AutoGenerateColumns="true" Theme="MetropolisBlue" Width="100%" OnCustomCallback="GridRwytPekerjaan_CustomCallback" OnDataBound="GridRwytPekerjaan_DataBound">
                                    <SettingsBehavior AllowFocusedRow="true" />
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Data Pendidikan">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <dx:ASPxGridView ID="GridPendidikan" ClientInstanceName="GridPendidikan" runat="server" KeyFieldName="NIK" EnableRowCache="true" AutoGenerateColumns="true" Theme="MetropolisBlue" Width="100%" OnCustomCallback="GridPendidikan_CustomCallback" OnDataBound="GridPendidikan_DataBound">
                                    <SettingsBehavior AllowFocusedRow="true" />
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Data Ketrampilan">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <dx:ASPxGridView ID="GridKetrampilan" ClientInstanceName="GridKetrampilan" runat="server" KeyFieldName="NIK" EnableRowCache="true" AutoGenerateColumns="true" Theme="MetropolisBlue" Width="100%" OnCustomCallback="GridKetrampilan_CustomCallback" OnDataBound="GridKetrampilan_DataBound">
                                    <SettingsBehavior AllowFocusedRow="true" />
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Identitas">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <dx:ASPxGridView ID="GridIdentitas" ClientInstanceName="GridIdentitas" runat="server" KeyFieldName="NIK" EnableRowCache="true" AutoGenerateColumns="true" Theme="MetropolisBlue" Width="100%" OnCustomCallback="GridIdentitas_CustomCallback" OnDataBound="GridIdentitas_DataBound">
                                    <SettingsBehavior AllowFocusedRow="true" />
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Text="Data Keluarga">
                            <ContentCollection>
                                <dx:ContentControl>
                                    <dx:ASPxGridView ID="GridKeluarga" ClientInstanceName="GridKeluarga" runat="server" KeyFieldName="NIK" EnableRowCache="true" AutoGenerateColumns="true" Theme="MetropolisBlue" Width="100%" OnCustomCallback="GridKeluarga_CustomCallback">
                                    <SettingsBehavior AllowFocusedRow="true" />
                                    </dx:ASPxGridView>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
            </td>
        </tr>
        <%--<tr>
            <td>
                <dx:ASPxMemo runat="server" ID="DetailNotes" ClientInstanceName="DetailNotes" Width="100%" Height="170" ReadOnly="true" />
            </td>
        </tr>--%>    
    </table>
    <cc1:msgBox ID="msgBox1" runat="server" /> 
</div>
</asp:Content>

