<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmVendor.aspx.vb" Inherits="AIS.FrmVendor" %>
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
<div>
    <dx:ASPxPopupControl ID="ModalEntry" runat="server" CloseOnEscape="True" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ModalEntry"
            HeaderText="Data Entry" Width="950px" Theme="MetropolisBlue" PopupElementID="ModalEntry">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div align="center">
                    <table width="900px">      
                    <tr>
                        <td align="right" colspan="7" 
                            style="border-bottom:2px; border-bottom-style:solid; border-bottom-color:#0000FF; padding-bottom: 5px; padding-top: 5px; border-bottom-width: 2px;">
                            <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN" UseSubmitBehavior="false"
                                Theme="MetropolisBlue" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL"
                                Theme="MetropolisBlue" Width="80px" AutoPostBack="False" CausesValidation="False">
                                <ClientSideEvents Click="function(s, e) { ModalEntry.Hide();}" />
                            </dx:ASPxButton>   
                        </td>
                    </tr>     
                    <tr>
                        <td align="left">Nama</td>
                        <td align="left">:</td>
                        <td align="left"> 
                            <dx:ASPxTextBox ID="TxtNama" runat="server" Width="250px" MaxLength="100"
                                ClientInstanceName="TxtNama" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td align="left">Telepon/HP</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtPhone1" runat="server" Width="250px" MaxLength="20"
                                ClientInstanceName="TxtPhone1" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="2" valign="top" align="left">Alamat</td>
                        <td rowspan="2" valign="top" align="left">:</td>
                        <td rowspan="2" valign="top" align="left">
                            <dx:ASPxMemo ID="TxtAlamat" runat="server" Height="40px" Width="400px" MaxLength="255">
                            </dx:ASPxMemo>       
                        </td>
                        <td></td>
                        <td align="left">Telepon/HP</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtPhone2" runat="server" Width="250px" MaxLength="20"
                                ClientInstanceName="TxtPhone2" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>    
                    </tr>
                    <tr>
                        <td></td>
                        <td align="left">Telepon/HP</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtPhone3" runat="server" Width="250px" MaxLength="20"
                                ClientInstanceName="TxtPhone3" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Kota/Kabupaten</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtKota" runat="server" Width="250px" MaxLength="50"
                                ClientInstanceName="TxtKota" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td align="left">Propinsi</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtPropinsi" runat="server" Width="300px" MaxLength="50"
                                ClientInstanceName="TxtPropinsi" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Fax</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtFax" runat="server" Width="250px" MaxLength="20"
                                ClientInstanceName="TxtFax" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td align="left">Email</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtEmail1" runat="server" Width="300px" MaxLength="100"
                                ClientInstanceName="TxtEmail1" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">NPWP</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtNPWP" runat="server" Width="300px" MaxLength="20"
                                ClientInstanceName="TxtNPWP" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td align="left">Email</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtEmail3" runat="server" Width="300px" MaxLength="100"
                                ClientInstanceName="TxtEmail3" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Contact Person</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtCP" runat="server" Width="300px" MaxLength="100"
                                ClientInstanceName="TxtCP" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                        <td align="left">Email</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtEmail2" runat="server" Width="300px" MaxLength="100"
                                ClientInstanceName="TxtEmail2" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Kategori</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxComboBox ID="DDLKategori" runat="server" ValueType="System.String" 
                                Width="250px" ClientInstanceName="DDLKategori" Theme="MetropolisBlue">
                            </dx:ASPxComboBox>
                        </td>
                        <td></td>
                        <td align="left">Bidang Usaha</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtUsaha" runat="server" Width="300px"
                                ClientInstanceName="TxtUsaha" MaxLength="50" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">PKP</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxCheckBox ID="CheckPKP" runat="server">
                            </dx:ASPxCheckBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="silver" colspan="4" width="500px" height="20px">Informasi Rekening</td>   
                    </tr>
                    <tr>
                        <td align="left">No. Rekening</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtNoRek" runat="server" Width="300px" MaxLength="30"
                                ClientInstanceName="TxtNoRek" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="left">Atas Nama</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtAN" runat="server" Width="300px" MaxLength="100"
                                ClientInstanceName="TxtAN" AutoCompleteType="Disabled" 
                                Theme="MetropolisBlue">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td align="left">Bank</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxComboBox ID="DDLBank" runat="server" ValueType="System.String" 
                                CssClass="font1" Width="250px" 
                                ClientInstanceName="DDLBank" Theme="MetropolisBlue">
                            </dx:ASPxComboBox>
                        </td>
                        <td></td>
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
    <td style="font-size:30px; text-decoration:underline">Daftar Vendor</td>
</tr>
</table>
</div>

<div class="font1">
<table>
<tr>
    <td>Filter berdasarkan</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLFilter" runat="server" ValueType="System.String" Theme="MetropolisBlue">
            <Items>
                <dx:ListEditItem Text="Nama" Value="VendorNm" Selected="true" />
                <dx:ListEditItem Text="Kategori" Value="Kategori" />
                <dx:ListEditItem Text="Bidang Usaha" Value="BidangUsaha" />
                <dx:ListEditItem Text="Kota/Kabupaten" Value="Kota" />
                <dx:ListEditItem Text="Propinsi" Value="Propinsi" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxTextBox ID="TxtFilter" runat="server" Width="250px" 
            ClientInstanceName="TxtFilter" MaxLength="100" AutoCompleteType="Disabled" 
            Theme="MetropolisBlue">
        </dx:ASPxTextBox>
    </td>
    <td>
        <dx:ASPxButton ID="BtnFilter" runat="server" Text="FILTER" 
            Theme="MetropolisBlue">
        </dx:ASPxButton>   
    </td>
    <td>
        <asp:Label ID="LblAction" runat="server" Text="" Visible="false"></asp:Label>
    </td>
</tr>
</table>

<table style="width:100%">
<tr>
    <td style="padding-top:10px">
        <dx:ASPxButton ID="BtnAdd" runat="server" Text="TAMBAH" 
            Theme="MetropolisBlue" CausesValidation="False">
        </dx:ASPxButton>  
    </td>
</tr>
<tr>
    <td>
        <asp:GridView ID="GridData" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="Vertical" AllowSorting="true" 
            ShowHeaderWhenEmpty="True" AllowPaging="True" PageSize="30">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="VendorID" HeaderText="Vendor ID" HeaderStyle-Width="50px" ItemStyle-Width="50px" SortExpression="VendorId">
                </asp:BoundField>
                <asp:BoundField DataField="VendorNm" HeaderText="Nama" HeaderStyle-Width="220px"
                ItemStyle-Width = "220px" SortExpression="VendorNm">
                </asp:BoundField>
                <asp:TemplateField HeaderText="Alamat" HeaderStyle-Width="300px" ItemStyle-Width = "300px" SortExpression="Alamat">
                    <ItemTemplate>
                        <asp:Label ID="LblUraian" runat="server" Text='<%# Eval("Alamat").ToString().Replace(vbCRLF, "<br />") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Kota" HeaderText="Kota/Kabupaten" HeaderStyle-Width="150px" ItemStyle-Width="150px" SortExpression="Kota">
                </asp:BoundField>
                <asp:BoundField DataField="Propinsi" HeaderText="Propinsi" HeaderStyle-Width="150px" ItemStyle-Width="150px" SortExpression="Propinsi">
                </asp:BoundField>
                <asp:BoundField DataField="Kategori" HeaderText="Kategori" HeaderStyle-Width="150px" ItemStyle-Width="150px" SortExpression="Kategori">
                </asp:BoundField>
                <asp:BoundField DataField="BidangUsaha" HeaderText="Bidang Usaha" HeaderStyle-Width="200px" ItemStyle-Width="200px" SortExpression="BidangUsaha">
                </asp:BoundField>
                <asp:ButtonField CommandName="BtnUpdate" Text="SELECT" HeaderStyle-Width="45px">
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
        </asp:GridView>
    </td>
</tr>
</table>   
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>
</asp:Content>
