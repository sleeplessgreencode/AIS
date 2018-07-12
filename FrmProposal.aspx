<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmProposal.aspx.vb" Inherits="AIS.FrmProposal" %>
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
                            Text="OK" Theme="MetropolisBlue" Width="75px" CausesValidation="False">
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
            Width="750px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue">
        <ClientSideEvents Init="function(s, e) {}" EndCallback="function(s, e) { PopEntry.Show(); }" />
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
                        <td align="left">Job No</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtJobNo" runat="server" Width="250px" CssClass="font1" MaxLength="8" TabIndex="1">
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField IsRequired="True"/>
                                </ValidationSettings>    
                            </dx:ASPxTextBox>
                        </td>    
                    </tr>
                    <tr>
                        <td align="left">Nama Proyek</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtNama" runat="server" Width="400px" CssClass="font1" MaxLength="50" TabIndex="2">
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField IsRequired="True"/>
                                </ValidationSettings>    
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left">Deskripsi</td>
                        <td valign="top" align="left">:</td>
                        <td align="left">
                            <dx:ASPxMemo ID="TxtDesc" runat="server" Height="50px" Width="600px" 
                                CssClass="font1" TabIndex="4" MaxLength="200">
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField IsRequired="True"/>
                                </ValidationSettings>    
                            </dx:ASPxMemo>
                        </td>    
                    </tr>
                    <tr>
                        <td align="left">Lokasi</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtLokasi" runat="server" Width="600px" CssClass="font1" MaxLength="100" TabIndex="5" 
                                NullText="Isi lengkap dengan kabupaten/kotamadya dan propinsi">
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField IsRequired="True"/>
                                </ValidationSettings>    
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Instansi</td>
                        <td align="left">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtInstansi" runat="server" Width="600px" CssClass="font1" MaxLength="100" TabIndex="6">
                                <ValidationSettings Display="Dynamic">
                                    <RequiredField IsRequired="True"/>
                                </ValidationSettings>    
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="3" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN"
                                Theme="MetropolisBlue" TabIndex="7" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL" CausesValidation="false"
                                Theme="MetropolisBlue" TabIndex="8" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) { PopEntry.Hide();}" />
                            </dx:ASPxButton>   
                        </td>
                    </tr>
                    <tr>
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
    <td style="font-size:30px; text-decoration:underline">Proposal</td>
</tr>
<tr>
    <td style="padding-top:10px" class="font1">Filter by:</td>
</tr>
<tr>
    <td>
        <asp:TextBox ID="TxtFind" runat="server" Width="300px" placeholder="Cari berdasarkan nama proyek" CssClass="font1"></asp:TextBox>
    </td>
    <td>
        <dx:ASPxButton ID="BtnFind" runat="server" Text="CARI" 
            Theme="MetropolisBlue" CausesValidation="False">
        </dx:ASPxButton>   
    </td>
    <td></td>    
</tr>
</table>
</div>

<div class="font1">
<table style="width: 100%">
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
            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
            ShowHeaderWhenEmpty="True" AllowPaging="True" AllowSorting="True"
            PageSize="15">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="JobNo" HeaderText="Job No" 
                    HeaderStyle-Width="50px" ItemStyle-Width = "50px">
                </asp:BoundField>
                <asp:BoundField DataField="JobNm" HeaderText="Nama Proyek" 
                        HeaderStyle-Width="150px" ItemStyle-Width = "150px">
                </asp:BoundField>
                <asp:TemplateField HeaderText="Deskripsi" HeaderStyle-Width="500px" ItemStyle-Width = "500px">
                    <ItemTemplate>
                        <asp:Label ID="LblDeskripsi" runat="server" Text='<%# Eval("Deskripsi").ToString().Replace(vbCRLF, "<br />") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Lokasi" HeaderText="Lokasi" 
                        HeaderStyle-Width="150px" ItemStyle-Width = "150px">
                </asp:BoundField>
                <asp:BoundField DataField="Instansi" HeaderText="Instansi" 
                        HeaderStyle-Width="350px" ItemStyle-Width = "350px">
                </asp:BoundField>
                <asp:BoundField DataField="StatusJob" HeaderText="Status" 
                    HeaderStyle-Width="70px" ItemStyle-Width = "70px">                
                </asp:BoundField>
                <asp:ButtonField CommandName="BtnUpdate" Text="SELECT" HeaderStyle-Width="45px"/>
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

</div>

</asp:Content>
