<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmEntryJurnal.aspx.vb" Inherits="AIS.FrmEntryJurnal" %>
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
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
    <dx:ASPxPopupControl ID="ModalEntry" runat="server" CloseOnEscape="True" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ModalEntry"
            HeaderText="Data Entry" Width="600px" Theme="MetropolisBlue" PopupElementID="ModalEntry">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <div align="center">
                <table width="500px">
                <tr>
                    <td align="left" colspan="3">
                        <dx:ASPxTextBox ID="TxtNo" runat="server" Width="30px"
                            ClientInstanceName="TxtNo" Visible="false" Theme="MetropolisBlue">
                        </dx:ASPxTextBox>
                    </td>
                </tr>           
                <tr>
                    <td align="left">Account</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxComboBox ID="DDLAccount" runat="server" ValueType="System.String" 
                            Theme="MetropolisBlue" Width="400px">          
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>                 
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">Uraian</td>
                    <td align="left" valign="top">:</td>
                    <td align="left">
                        <dx:ASPxMemo ID="TxtUraian" runat="server" Height="60px" Width="400px" MaxLength="255">
                        </dx:ASPxMemo>
                    </td>
                </tr>
                <tr>
                    <td align="left">Debet</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtDebet" runat="server" Width="200px">
                            <MaskSettings Mask="&lt;0..99999999999999g&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Kredit</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtKredit" runat="server" Width="200px">
                            <MaskSettings Mask="&lt;0..99999999999999g&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="right" 
                        style="border-bottom:2px; border-bottom-style:solid; border-bottom-color:#0000FF; padding-bottom: 5px; 
                        border-top-style: solid; border-top-width: 2px; border-top-color: #0000FF; padding-top: 5px;">
                        <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN"
                            Theme="MetropolisBlue" Width="80px" UseSubmitBehavior="False">
                        </dx:ASPxButton>                  
                        <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL"
                            Theme="MetropolisBlue" Width="80px" CausesValidation="False"
                            UseSubmitBehavior="False" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) { ModalEntry.Hide();}" />
                        </dx:ASPxButton>   
                    </td>
                </tr>
                </table>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div>

<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Entry Jurnal Harian</div>

<div>
<table>
<tr>
    <td>
        <asp:Label ID="Action" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="JobNo" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="Source" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="Action1" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="LedgerNo" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="Tgl1" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="Tgl2" runat="server" Text="" Visible="false"></asp:Label>
    </td>
</tr>
</table>

<table>
<tr>
    <td colspan="7" align="right" style="border-bottom:2px; border-bottom-style:solid; border-bottom-color:#C0C0C0; border-bottom-width: 3px; padding-bottom: 5px;">
        <dx:ASPxButton ID="BtnSave1" runat="server" Text="SIMPAN"
            Theme="MetropolisBlue" Width="80px" UseSubmitBehavior="false">
        </dx:ASPxButton>                       
        <dx:ASPxButton ID="BtnCancel1" runat="server" Text="BATAL"
            Theme="MetropolisBlue" Width="80px" CausesValidation="False" UseSubmitBehavior="false">
        </dx:ASPxButton>   
    </td>
</tr>
<tr><td></td></tr>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtJobNo" runat="server" Width="200px" Enabled="false">
        </dx:ASPxTextBox>
    </td>
    <td style="width:30px;" colspan="3"></td>
    <td>
        <dx:ASPxTextBox ID="TxtNoJurnal1" runat="server" Width="200px" Visible="false">
        </dx:ASPxTextBox>
    </td>
</tr>
<tr>
    <td>No. Jurnal</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoJurnal" runat="server" Width="200px" Enabled="false">
        </dx:ASPxTextBox>
    </td>
    <td style="width:30px;"></td>
    <td>Tgl Jurnal</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="TxtTglJurnal" runat="server" DisplayFormatString="dd-MMM-yyyy"
            Theme="MetropolisBlue">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>
    </td>
</tr>
<tr>
    <td>Site</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtSite" runat="server" Width="200px" Enabled="false">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxTextBox>
    </td>
    <td></td>
    <td>Member</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtMember" runat="server" Width="80px" Enabled="false">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxTextBox>
    </td>
</tr>
<tr>
    <td>Nota</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLNota" runat="server" ValueType="System.String" 
            Theme="MetropolisBlue" SelectedIndex="0" Width="80px">          
            <Items>
                <dx:ListEditItem Text="KM" Value="KM" />
                <dx:ListEditItem Text="KK" Value="KK" />
                <dx:ListEditItem Text="BK" Value="BK" />
                <dx:ListEditItem Text="BM" Value="BM" />
                <dx:ListEditItem Text="MM" Value="MM" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td></td>
    <td>Identitas</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLIdentitas" runat="server" ValueType="System.String" 
            Theme="MetropolisBlue" SelectedIndex="0">          
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>No Reg</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNoReg" runat="server" Width="200px" MaxLength="20">
        </dx:ASPxTextBox>   
    </td>
    <td></td>
</tr>
</table>

<table>
<tr><td></td></tr>
<tr>
    <td align="center" style="border-style: solid none solid none; font-size: 20px; font-family: 'Segoe UI'; padding-left: 5px; border-top-width: 3px; border-bottom-width: 3px; border-top-color: #C0C0C0; border-bottom-color: #C0C0C0;">
    U R A I A N&nbsp;&nbsp;&nbsp;&nbsp;J U R N A L</td>
</tr>
<tr>
    <td style="border: 2px solid #C0C0C0">
        <table>
        <tr>
            <td style="padding-bottom:5px">
                <dx:ASPxButton ID="BtnAdd" runat="server" Text="TAMBAH"
                    Theme="MetropolisBlue" Width="80px" AutoPostBack="True" 
                    CausesValidation="True" UseSubmitBehavior="False">
                </dx:ASPxButton>                       
            </td>       
        </tr>
        <tr>
            <td style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #507CD1">
                <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False"               
                    CellPadding="4" ForeColor="#333333" GridLines="Vertical"
                    ShowHeaderWhenEmpty="True" ShowFooter="True">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>     
                        <asp:BoundField DataField="NoUrut" HeaderText="No." HeaderStyle-Width="30px" ItemStyle-Width = "30px" 
                            HeaderStyle-Height="20px" ItemStyle-HorizontalAlign="Center"> 
                        </asp:BoundField>                        
                        <asp:BoundField DataField="AccNo" HeaderText="Account" HeaderStyle-Width="80px" ItemStyle-Width = "80px" 
                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Height="20px"> 
                        </asp:BoundField>
                        <asp:BoundField DataField="AccName" HeaderText="Deskripsi" HeaderStyle-Width="250px" ItemStyle-Width = "250px" 
                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-Height="20px"> 
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Uraian" HeaderStyle-Width="400px" ItemStyle-Width = "400px" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="LblUraian" runat="server" Text='<%# Eval("Uraian").ToString().Replace(vbCRLF, "<br />") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Debet" HeaderText="Debet" HeaderStyle-Width="150px" ItemStyle-Width = "150px" 
                            DataFormatString="{0:N0}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Height="20px"> 
                        </asp:BoundField>
                        <asp:BoundField DataField="Kredit" HeaderText="Kredit" HeaderStyle-Width="150px" ItemStyle-Width = "150px" 
                            DataFormatString="{0:N0}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" HeaderStyle-Height="20px"> 
                        </asp:BoundField>
                        <asp:ButtonField CommandName="BtnUpdate" Text="UPDATE" HeaderStyle-Width="45px">                                          
                        </asp:ButtonField>
                        <asp:ButtonField CommandName="BtnDelete" Text="DELETE" HeaderStyle-Width="45px">                                          
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
    </td>    
</tr>            
</table>
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>
</asp:Content>
