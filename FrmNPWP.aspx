<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmNPWP.aspx.vb" Inherits="AIS.FrmNPWP" %>
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
            HeaderText="Data Entry" Width="700px" Theme="MetropolisBlue" PopupElementID="ModalEntry">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <div align="center">
                <table width="650px">
                <tr>
                    <td align="left">Job No</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtJobNo" runat="server" Width="100px" Enabled="false">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Nama Proyek</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtJobNm" runat="server" Width="300px" Enabled="false">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Atas Nama</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtNPWPName" runat="server" Width="300px" AutoCompleteType="Disabled" MaxLength="100">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Nomor NPWP</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtNPWP" runat="server" Width="200px" MaxLength="50" AutoCompleteType="Disabled">
                            <MaskSettings Mask="99.999.999.9-999.999" IncludeLiterals="All" />
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">Alamat</td>
                    <td align="left" valign="top">:</td>
                    <td align="left">
                        <dx:ASPxMemo ID="TxtAlamat" runat="server" Height="40px" Width="300px" MaxLength="255">
                        </dx:ASPxMemo>
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
                            UseSubmitBehavior="False">
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
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">NPWP</div>

<div>
<table>
<tr>
    <td>
        <asp:Label ID="Action" runat="server" Text="" Visible="false"></asp:Label>
    </td>
</tr>
</table>

<table>
<tr>
<td>
    <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
        ShowHeaderWhenEmpty="True" AllowPaging="True" AllowSorting="True"
        PageSize="50">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="JobNo" HeaderText="Job No" 
                HeaderStyle-Width="50px" ItemStyle-Width = "50px">
            </asp:BoundField>
            <asp:BoundField DataField="JobNm" HeaderText="Nama Proyek" 
                    HeaderStyle-Width="300px" ItemStyle-Width = "300px">
            </asp:BoundField>
            <asp:BoundField DataField="NPWPName" HeaderText="Atas Nama" 
                    HeaderStyle-Width="250px" ItemStyle-Width = "250px">
            </asp:BoundField>
            <asp:TemplateField HeaderText="Alamat">
                <ItemTemplate>
                    <asp:Label ID="LblAlamat" runat="server" Text='<%# Eval("NPWPAddress").ToString().Replace(vbCRLF, "<br />") %>' Width="300px"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="NPWPCompany" HeaderText="Nomor NPWP" 
                    HeaderStyle-Width="150px" ItemStyle-Width = "150px">
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
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>

</asp:Content>
