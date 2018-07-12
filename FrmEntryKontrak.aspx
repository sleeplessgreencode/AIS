<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmEntryKontrak.aspx.vb" Inherits="AIS.FrmEntryKontrak" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    $(function () {
        $("[id*=GridView] td").hover(function () {
            $("td", $(this).closest("tr")).addClass("hover_row");
        }, function () {
            $("td", $(this).closest("tr")).removeClass("hover_row");
        });
    });
    $(function () {
        $("[id*=GridAdd] td").hover(function () {
            $("td", $(this).closest("tr")).addClass("hover_row");
        }, function () {
            $("td", $(this).closest("tr")).removeClass("hover_row");
        });
    });
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div style="font-family:Segoe UI Light; padding-bottom: 5px;">
<table>
<tr>
    <td style="font-size:30px; text-decoration:underline">
        <asp:Label ID="LblJudul" runat="server" Text=""></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblAction" runat="server" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblJobNo" runat="server" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblTglKontrak" runat="server" Visible="false"></asp:Label>
    </td>
</tr>
</table>
</div>

<div style="padding-bottom: 5px;">
<table>
<tr>
    <td> 
        <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN" 
            Width="75px" Theme="MetropolisBlue">
        </dx:ASPxButton>     
        <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL" 
            Theme="MetropolisBlue" Width="75px" CausesValidation="False">
        </dx:ASPxButton>          
    </td>
</tr>
</table>
</div>

<div class="font1">
<dx:ASPxPageControl ID="TabPage" runat="server" ActiveTabIndex="3" 
        Theme="MetropolisBlue">
    <TabPages>
        <dx:TabPage Text="Data Proyek" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl1" runat="server">
                    <table>
                    <tr>
                        <td>Job No</td>
                        <td>:</td>
                        <td> 
                            <dx:ASPxTextBox ID="TxtJobNo" runat="server" Width="250px" CssClass="font1"  
                                MaxLength="8" Enabled="False"></dx:ASPxTextBox>
                        </td>  
                        <td></td>
                        <td rowspan="2" valign="top">Deskripsi</td>
                        <td rowspan="2" valign="top">:</td>
                        <td rowspan="2"> 
                            <dx:ASPxMemo ID="TxtDesc" runat="server" Height="40px" Width="400px" MaxLength="200">
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxMemo>
                        </td>    
                    </tr>
                    <tr>
                        <td>Nama Proyek</td>
                        <td>:</td>
                        <td> 
                            <dx:ASPxTextBox ID="TxtNama" runat="server" Width="400px" CssClass="font1"  
                                MaxLength="50" Enabled="False"></dx:ASPxTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Lokasi</td>
                        <td>:</td>
                        <td> 
                            <dx:ASPxTextBox ID="TxtLokasi" runat="server" Width="400px" CssClass="font1"  
                                MaxLength="100" Enabled="False"></dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td>Instansi</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxTextBox ID="TxtInstansi" runat="server" Width="400px" MaxLength="100">
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Status Proyek</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxComboBox ID="DDLStatus" runat="server" Theme="MetropolisBlue">
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>    
                        <td></td>  
                        <td>Kategori</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxComboBox ID="DDLKategori" runat="server" Theme="MetropolisBlue" SelectedIndex="0">
                                <Items>
                                    <dx:ListEditItem Text="Primary" Value="Primary" />
                                    <dx:ListEditItem Text="Secondary" Value="Secondary" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>                      
                    </tr>
                    <tr>
                        <td>Kontraktor</td>
                        <td>:</td>
                        <td> 
                            <dx:ASPxTextBox ID="TxtKontraktor" runat="server" Width="400px" 
                                CssClass="font1"  MaxLength="100">
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td valign="top">Image</td>
                        <td valign="top">:</td>
                        <td valign="top"> 
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="350px" 
                                CssClass="font1"/>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                        <td rowspan="3">
                            <asp:Image ID="Image1" runat="server" Height = "78" Width = "447" />
                        </td>
                    </tr>
                    </table>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Data Kontrak/Addendum" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl2" runat="server">
                <table>
                <tr>
                    <td>
                        <dx:ASPxButton ID="BtnAddendum" runat="server" Text="TAMBAH ADDENDUM" 
                            Theme="MetropolisBlue" Visible="false">
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="BtnCclAdd" runat="server" Text="BATAL" Visible="false" 
                            Theme="MetropolisBlue" Width="75px" CausesValidation="False">
                        </dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td>No Kontrak/Addendum</td>
                    <td>:</td>
                    <td> 
                        <dx:ASPxTextBox ID="TxtNoKontrak" runat="server" Width="300px" 
                            CssClass="font1"  MaxLength="50">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                    <td></td>
                    <td>Tgl Kontrak/Addendum</td>
                    <td>:</td>
                    <td>
                        <dx:ASPxDateEdit ID="TglKontrak" runat="server" CssClass="font1" 
                            DisplayFormatString="dd-MMM-yyyy"
                            Theme="MetropolisBlue">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Addendum Ke</td>
                    <td>:</td>
                    <td> 
                        <dx:ASPxSpinEdit ID="TxtAddendumKe" runat="server" DecimalPlaces="0" 
                            DisplayFormatString="{0:N0}" Number="0" MaxLength="1" Width="70px" 
                            NullText="0" Enabled="False">
                        </dx:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td>Tgl Mulai Kontrak</td>
                    <td>:</td>
                    <td> 
                        <dx:ASPxDateEdit ID="PrdAwal" runat="server" CssClass="font1" 
                            DisplayFormatString="dd-MMM-yyyy" Theme="MetropolisBlue" AutoPostBack="true">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </td>
                    <td></td>
                    <td>Tgl Selesai Kontrak</td>
                    <td>:</td>
                    <td>
                        <dx:ASPxDateEdit ID="PrdAkhir" runat="server" CssClass="font1" 
                            DisplayFormatString="dd-MMM-yyyy" AutoPostBack="True" 
                            Theme="MetropolisBlue">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Jumlah Hari</td>
                    <td>:</td>
                    <td> 
                        <dx:ASPxSpinEdit ID="TxtHari" runat="server" DecimalPlaces="0" 
                            DisplayFormatString="{0:N0}" Number="0" MaxLength="5" Width="70px" 
                            NullText="0" AllowMouseWheel="False" MaxValue="99999" AutoPostBack="true">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxSpinEdit>
                    </td>
                    <td></td>
                    <td>Jumlah Minggu</td>
                    <td>:</td>
                    <td> 
                        <dx:ASPxSpinEdit ID="TxtMinggu" runat="server" DecimalPlaces="0" 
                            DisplayFormatString="{0:N0}" Number="0" MaxLength="5" Width="70px" 
                            NullText="0" AllowMouseWheel="False" MaxValue="99999">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxSpinEdit>
                    </td>
                </tr>
                <tr>
                    <td>Bruto Kontrak (Rp)</td>
                    <td>:</td>
                    <td> 
                        <dx:ASPxSpinEdit ID="TxtBruto" runat="server" 
                            DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" 
                            AllowNull="False" AutoPostBack="True" Increment="0" 
                            LargeIncrement="0" NumberType="Integer" MaxValue="9999999999999">
                        <SpinButtons ShowIncrementButtons="False"/>
                        <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                            <RequiredField IsRequired="true" />
                        </ValidationSettings>
                        </dx:ASPxSpinEdit>
                    </td>
                    <td></td>
                    <td>Netto Kontrak (Rp)</td>
                    <td>:</td>
                    <td>
                        <dx:ASPxSpinEdit ID="TxtNetto" runat="server" DecimalPlaces="2" 
                            DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" Enabled="False">
                        </dx:ASPxSpinEdit>
                    </td>    
                    <td>Exc. PPN & Exc. PPH</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <dx:ASPxMemo ID="TxtRemarkAddendum" runat="server" Height="71px" Width="400px" MaxLength="255" 
                            Visible="false" Caption="Keterangan Addendum" CaptionSettings-Position="Top">
                        </dx:ASPxMemo>
                    </td>
                </tr>
                </table>
                <table>
                <tr><td></td></tr>
                <tr>
                    <td align="center" bgcolor="silver" style="height:20px; font-weight:bold">HISTORY ADDENDUM</td>   
                </tr>
                <tr>
                    <td>  
                        <asp:GridView ID="GridAdd" runat="server" AutoGenerateColumns="False" 
                            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                            ShowHeaderWhenEmpty="True" AllowPaging="False" AllowSorting="False">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="NoKontrak" HeaderText="No Kontrak/Addendum" 
                                    HeaderStyle-Width="150px" ItemStyle-Width = "150px">                
<HeaderStyle Width="150px"></HeaderStyle>

<ItemStyle Width="150px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TglKontrak" HeaderText="Tgl Kontrak/Addendum" HeaderStyle-Width="80px" ItemStyle-Width = "80px"
                                    DataFormatString="{0:dd-MMM-yyyy}">                        
<HeaderStyle Width="80px"></HeaderStyle>

                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="AddendumKe" HeaderText="Addendum Ke" 
                                    HeaderStyle-Width="70px" ItemStyle-Width = "70px" ItemStyle-HorizontalAlign="Center">                
<HeaderStyle Width="70px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Bruto" HeaderText="Bruto (Rp)" HeaderStyle-Width="120px" ItemStyle-Width = "120px"
                                    DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">                        
<HeaderStyle Width="120px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="120px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Netto" HeaderText="Netto (Rp)" HeaderStyle-Width="120px" ItemStyle-Width = "120px"
                                    DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">                        
<HeaderStyle Width="120px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="120px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Hari" HeaderText="Jumlah Hari" ItemStyle-HorizontalAlign="Right" 
                                    HeaderStyle-Width="70px" ItemStyle-Width = "70px">                                         
<HeaderStyle Width="70px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="70px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PrdAwal" HeaderText="Tgl Mulai Kontrak" 
                                    HeaderStyle-Width="80px" ItemStyle-Width = "80px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}">
<HeaderStyle Width="80px"></HeaderStyle>

                                    <ItemStyle HorizontalAlign="Center" />         
                                </asp:BoundField>
                                <asp:BoundField DataField="PrdAkhir" HeaderText="Tgl Selesai Kontrak" 
                                    HeaderStyle-Width="80px" ItemStyle-Width = "80px" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}">     
<HeaderStyle Width="80px"></HeaderStyle>

                                    <ItemStyle HorizontalAlign="Center" />         
                                </asp:BoundField>
                                <asp:BoundField DataField="RemarkAddendum" HeaderText="Keterangan Addendum" 
                                    HeaderStyle-Width="350px" ItemStyle-Width = "350px">                
<HeaderStyle Width="350px"></HeaderStyle>

<ItemStyle Width="350px"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
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
        <dx:TabPage Text="DIPA" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl5" runat="server">
                <table>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="TxtNo" runat="server" Width="50px" CssClass="font1" Visible="false"></dx:ASPxTextBox>
                    </td>
                </tr>
                <tr><td>
                    <asp:Label ID="LblDipa" runat="server" Text="NEW" Visible="false"></asp:Label></td></tr>
                <tr>
                    <td></td>
                    <td>
                        <dx:ASPxSpinEdit ID="TxtTahun" runat="server" Caption="Tahun" CaptionSettings-Position="Top"
                            MaxLength="4" Width="100px" AllowNull="False" Increment="1" Enabled="false"
                            LargeIncrement="0" NumberType="Integer" MaxValue="9999">
                        <SpinButtons ShowIncrementButtons="true"/>
<CaptionSettings Position="Top"></CaptionSettings>
                        </dx:ASPxSpinEdit>
                    </td>
                    <td>
                        <dx:ASPxSpinEdit ID="TxtDIPA" runat="server" Caption="Bruto (Rp)" CaptionSettings-Position="Top"
                            DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" 
                            AllowNull="False" Increment="0" Enabled="false" 
                            LargeIncrement="0" NumberType="Integer" MaxValue="9999999999999">
                        <SpinButtons ShowIncrementButtons="False"/>

<CaptionSettings Position="Top"></CaptionSettings>
                        </dx:ASPxSpinEdit>
                    </td>
                    <td valign="bottom">
                        <dx:ASPxButton ID="BtnAdd" runat="server" Text="TAMBAH" 
                            Theme="MetropolisBlue" Width="75px" CausesValidation="False">
                        </dx:ASPxButton>
                        <dx:ASPxButton ID="BtnCcl" runat="server" Text="BATAL" Visible="false" 
                            Theme="MetropolisBlue" Width="75px" CausesValidation="False">
                        </dx:ASPxButton>
                    </td>
                </tr>
                </table>
                <table>
                <tr>
                    <td>
                        <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False" 
                            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                            ShowHeaderWhenEmpty="True">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="NoUrut" HeaderText="No" 
                                    HeaderStyle-Width="30px" ItemStyle-Width = "30px" ItemStyle-HorizontalAlign="Center">
<HeaderStyle Width="30px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Tahun" HeaderText="Tahun" ItemStyle-HorizontalAlign="Center" 
                                    HeaderStyle-Width="50px" ItemStyle-Width = "50px">
<HeaderStyle Width="50px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Budget" HeaderText="Bruto (Rp)" 
                                    HeaderStyle-Width="150px" ItemStyle-Width = "150px" DataFormatString="{0:N0}" Itemstyle-HorizontalAlign="right">
<HeaderStyle Width="150px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right" Width="150px"></ItemStyle>
                                </asp:BoundField>     
                                <asp:ButtonField CommandName="BtnUpdate" Text="SELECT" HeaderStyle-Width="45px">
<HeaderStyle Width="45px"></HeaderStyle>
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="BtnDelete" Text="DELETE" HeaderStyle-Width="45px">
<HeaderStyle Width="45px"></HeaderStyle>
                                </asp:ButtonField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
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
        <dx:TabPage Text="Data KSO" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl3" runat="server">
                <table>
                <tr>
                    <td colspan="2"> 
                        <dx:ASPxComboBox ID="DDLKso" runat="server" Caption="KSO" 
                            CssClass="font1" Width="70px" 
                            ClientInstanceName="DDLStatus" Theme="MetropolisBlue" CaptionSettings-Position="Left">
                            <Items>
                                <dx:ListEditItem Text="Yes" Value="1" />
                                <dx:ListEditItem Text="No" Value="0" Selected="True" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                    <td colspan="2">
                        <dx:ASPxComboBox ID="DDLManajerial" runat="server" Theme="MetropolisBlue" Caption="Tipe Manajerial"
                            SelectedIndex="0" AutoPostBack="true" CaptionSettings-Position="Left">
                            <Items>
                                <dx:ListEditItem Text="Single" Value="Single" />
                                <dx:ListEditItem Text="JO Partial" Value="JO Partial" />
                                <dx:ListEditItem Text="JO Integrated" Value="JO Integrated" />
                            </Items>
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                    </td>                  
                </tr>
                <tr><td></td></tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td width="100px" style="border-bottom-style: solid; border-bottom-width: 2px">Nama</td>
                    <td width="90px" style="border-bottom-style: solid; border-bottom-width: 2px">Share (%)</td>
                    <td width="150px" style="border-bottom-style: solid; border-bottom-width: 2px">Bruto (Rp)</td>
                    <td width="320px" style="border-bottom-style: solid; border-bottom-width: 2px">Keterangan</td>
                </tr>
                <tr>
                    <td valign="top">
                        <dx:ASPxRadioButton ID="RdoOwn1" runat="server" GroupName="Own">
                        </dx:ASPxRadioButton>                        
                    </td>
                    <td valign="top">LEADER</td>
                    <td valign="top" width="100px">
                        <dx:ASPxTextBox ID="TxtMember1" runat="server" Width="200px">
                        </dx:ASPxTextBox>
                    </td>
                    <td valign="top" width="90px">
                        <dx:ASPxTextBox ID="TxtShare1" runat="server" Width="80px">
                            <MaskSettings Mask="&lt;0..999g&gt;.&lt;00..99&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                    <td valign="top" width="150px">
                        <dx:ASPxTextBox ID="TxtBruto1" runat="server" Width="120px">
                            <MaskSettings Mask="&lt;0..99999999999999g&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                    <td width="320px">
                        <dx:ASPxMemo ID="Remark1" runat="server" Height="40px" Width="300px">
                        </dx:ASPxMemo>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <dx:ASPxRadioButton ID="RdoOwn2" runat="server" GroupName="Own">
                        </dx:ASPxRadioButton>                        
                    </td>
                    <td valign="top">MEMBER</td>
                    <td valign="top">
                        <dx:ASPxTextBox ID="TxtMember2" runat="server" Width="200px">
                        </dx:ASPxTextBox>
                    </td>
                    <td valign="top">
                        <dx:ASPxTextBox ID="TxtShare2" runat="server" Width="80px">
                            <MaskSettings Mask="&lt;0..999g&gt;.&lt;00..99&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                    <td valign="top">
                        <dx:ASPxTextBox ID="TxtBruto2" runat="server" Width="120px">
                            <MaskSettings Mask="&lt;0..99999999999999g&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                    <td>
                        <dx:ASPxMemo ID="Remark2" runat="server" Height="40px" Width="300px">
                        </dx:ASPxMemo>
                    </td>
                </tr>                
                </table>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Rekening Termin & Lapangan" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl4" runat="server">
                <table>
                    <tr>
                        <td align="center" bgcolor="silver" colspan="7" height="20" 
                            style="font-weight:bold">Rekening Termin
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>No. Rekening</td>
                        <td>:</td>
                        <td> 
                            <dx:ASPxTextBox ID="TxtNoRekInduk" runat="server" Width="300px" CssClass="font1"  
                                MaxLength="30">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td>Atas Nama</td>
                        <td>:</td>
                        <td> 
                            <dx:ASPxTextBox ID="TxtANInduk" runat="server" Width="300px" CssClass="font1"  
                                MaxLength="100">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Bank</td>
                        <td>:</td>
                        <td> 
                            <dx:ASPxComboBox ID="DDLBankInduk" runat="server" ValueType="System.String" 
                                CssClass="font1" Width="300px" 
                                ClientInstanceName="DDLBankInduk" Theme="MetropolisBlue">
                            </dx:ASPxComboBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td align="center" bgcolor="silver" colspan="7" height="20" 
                            style="font-weight:bold">Data Admin Keuangan Lapangan
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Nama</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxTextBox ID="TxtNama1" runat="server" Width="300px" CssClass="font1"  
                                MaxLength="100"></dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td>No. Rekening</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxTextBox ID="TxtNoRek" runat="server" Width="300px" CssClass="font1"  
                                MaxLength="30"></dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Alamat</td>
                        <td>:</td>
                        <td> 
                            <dx:ASPxTextBox ID="TxtAlamat" runat="server" Width="300px" CssClass="font1"  
                                MaxLength="255"></dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td>Atas Nama</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxTextBox ID="TxtAN" runat="server" Width="300px" CssClass="font1"  
                                MaxLength="100"></dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Telepon</td>
                        <td>:</td>
                        <td> 
                            <dx:ASPxTextBox ID="TxtTelepon" runat="server" Width="300px" CssClass="font1"  
                                MaxLength="20"></dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td>Bank</td>
                        <td>:</td>
                        <td>
                            <dx:ASPxComboBox ID="DDLBank" runat="server" ValueType="System.String" 
                                CssClass="font1" Width="300px" 
                                ClientInstanceName="DDLBank" Theme="MetropolisBlue">
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td>NPWP</td>
                        <td>:</td>
                        <td> 
                            <dx:ASPxTextBox ID="TxtNPWP" runat="server" Width="300px" CssClass="font1"  
                                MaxLength="20"></dx:ASPxTextBox>
                        </td>
                    </tr>                    
                </table>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Data PHO/FHO" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl6" runat="server">
                <table>
                <tr>
                    <td colspan="7" align="center" bgcolor="silver" style="height:20px; font-weight:bold">DATA PHO</td>   
                </tr>
                <tr>
                    <td>No. PHO</td> 
                    <td>:</td>
                    <td>
                        <dx:ASPxTextBox ID="TxtNoPHO" runat="server" Width="300px" 
                            CssClass="font1"  MaxLength="100">
                        </dx:ASPxTextBox>
                    </td>
                    <td></td>
                    <td>Tgl PHO</td>
                    <td>:</td>
                    <td>
                        <dx:ASPxDateEdit ID="TglPHO" runat="server" CssClass="font1" 
                            DisplayFormatString="dd-MMM-yyyy"
                            Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr><td></td></tr>
                <tr>
                    <td colspan="7" align="center" bgcolor="silver" style="height:20px; font-weight:bold">DATA FHO</td>   
                </tr>
                <tr>   
                    <td>No. FHO</td> 
                    <td>:</td>
                    <td>
                        <dx:ASPxTextBox ID="TxtNoFHO" runat="server" Width="300px" 
                            CssClass="font1"  MaxLength="100">
                        </dx:ASPxTextBox>
                    </td>
                    <td></td>
                    <td>Tgl FHO</td>
                    <td>:</td>
                    <td>
                        <dx:ASPxDateEdit ID="TglFHO" runat="server" CssClass="font1" 
                            DisplayFormatString="dd-MMM-yyyy" Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td>Upload FHO (PDF File)</td>
                    <td>:</td>
                    <td>
                        <asp:FileUpload ID="FHOUpload" runat="server" CssClass="font1" Width="300px" />
                    </td>
                    <td></td>
                    <td>
                        <asp:LinkButton ID="LnkView" runat="server" Text="Download PDF" OnClick="View" 
                            Visible="False"></asp:LinkButton>
                        <asp:Label ID="LblPath" runat="server" Text="" Visible="False"></asp:Label>
                    </td>
                </tr>
                </table>
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>
</asp:Content>