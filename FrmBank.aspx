<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FrmBank.aspx.vb" Inherits="AIS.FrmBank" MasterPageFile="~/Site1.Master" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
$(function () {
    $("[id*=GridBank] td").hover(function () {
        $("td", $(this).closest("tr")).addClass("hover_row");
    }, function () {
        $("td", $(this).closest("tr")).removeClass("hover_row");
    });
});
</script>
</style>
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
                            Text="OK" Theme="MetropolisBlue" Width="75px">
                            <ClientSideEvents Click="function(s, e) { ErrMsg.Hide();}" />
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
    <td style="font-size:30px; text-decoration:underline">Daftar Bank</td>
</tr>
</table>
</div>

<div class="font1">
<table>
<tr>
    <td>Bank</td>
    <td style="text-align:center">:</td>
    <td>    
        <asp:TextBox ID="TxtBank" runat="server" Width="200px" autocomplete="off" 
            MaxLength="20" CssClass="font1" TabIndex="1"></asp:TextBox>
    </td>
</tr>
<tr>
    <td>
        <asp:TextBox ID="TxtAction" runat="server" Width="35px"  Visible="False" 
            Text="NEW"></asp:TextBox>
    </td>
    <td></td>
    <td>
        <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN" 
            Width="75px" Theme="MetropolisBlue" TabIndex="2">
        </dx:ASPxButton>     
        <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL" 
            Theme="MetropolisBlue" Width="75px" TabIndex="3">
        </dx:ASPxButton>          
    </td>
</tr>
</table>

<table style="width: 710px">
<tr>
    <td>
        <asp:GridView ID="GridBank" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
            AllowSorting="True" ShowHeaderWhenEmpty="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
            <Columns>
                <asp:BoundField DataField="Bank" HeaderText="Bank" HeaderStyle-Width="200px" ItemStyle-Width = "200px"/>
                <asp:ButtonField CommandName="BtnUpdate" Text="SELECT" HeaderStyle-Width="45px" ItemStyle-Width = "45px"/>
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
</div>

</asp:Content>