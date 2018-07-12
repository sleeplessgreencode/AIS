<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmTipeForm.aspx.vb" Inherits="AIS.FrmTipeForm" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    $(function () {
        $("[id*=GridBiaya] td").hover(function () {
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
    <td style="font-size:30px; text-decoration:underline">Daftar Tipe Form</td>
</tr>
</table>
</div>

<div class="font1">
<table>
<tr>
    <td>Tipe Form</td>
    <td style="text-align:center">:</td>
    <td>    
        <asp:TextBox ID="TxtForm" runat="server" Width="40px" autocomplete="off" 
            MaxLength="3" CssClass="font1" TabIndex="1"></asp:TextBox>
    </td>
</tr>
<tr>
    <td>Alokasi</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLAlokasi" runat="server" ValueType="System.String" 
            CssClass="font1" Width="300px" 
            ClientInstanceName="DDLAlokasi" TabIndex="2" Theme="MetropolisBlue">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Keterangan</td>
    <td style="text-align:center">:</td>
    <td>
        <asp:TextBox ID="TxtKeterangan" runat="server" Width="500px" autocomplete="off" CssClass="font1" MaxLength="200" TabIndex="3"></asp:TextBox>
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
            Width="75px" Theme="MetropolisBlue" TabIndex="4">
        </dx:ASPxButton>     
        <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL" 
            Theme="MetropolisBlue" Width="75px" TabIndex="5">
        </dx:ASPxButton>          
    </td>
</tr>
</table>

<table style="width: 710px">
<tr>
    <td>
        <asp:GridView ID="GridBiaya" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
            ShowHeaderWhenEmpty="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
            <Columns>
                <asp:BoundField DataField="TipeForm" HeaderText="TipeForm" HeaderStyle-Width="50px"
                ItemStyle-Width = "50px"/>
                <asp:BoundField DataField="Alokasi" HeaderText="Alokasi" HeaderStyle-Width="50px"
                ItemStyle-Width = "50px"/>
                <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" HeaderStyle-Width="500px"
                ItemStyle-Width = "500px" />
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
        </asp:GridView>
    </td>
</tr>
</table>     
</div>
</asp:Content>
