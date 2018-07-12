<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmKontrak.aspx.vb" Inherits="AIS.FrmKontrak" %>
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
    <td style="font-size:30px; text-decoration:underline">Job</td>
</tr>
</table>
</div>

<table>
<tr>
    <td style="padding-top:10px" class="font1">Filter by:</td>
</tr>
<tr>
    <td>
        <asp:TextBox ID="TxtFind" runat="server" Width="300px" placeholder="Cari berdasarkan nama proyek" CssClass="font1"></asp:TextBox>
    </td>
    <td>
        <dx:ASPxButton ID="BtnFind" runat="server" Text="CARI" 
            Theme="MetropolisBlue">
        </dx:ASPxButton>   
    </td>
    <td></td>    
</tr>
</table>

<div class="font1">
<table style="width: 100%">
<tr>
    <td style="padding-top:5px"></td>
</tr>
<tr>
    <td>
        <asp:GridView ID="GridData" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
            ShowHeaderWhenEmpty="True" AllowPaging="True" AllowSorting="True"
            PageSize="50">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="JobNo" HeaderText="Job No" 
                    HeaderStyle-Width="50px" ItemStyle-Width = "50px">
                </asp:BoundField>
                <asp:BoundField DataField="JobNm" HeaderText="Nama Proyek" 
                        HeaderStyle-Width="200px" ItemStyle-Width = "200px">
                </asp:BoundField>
                <asp:BoundField DataField="Deskripsi" HeaderText="Deskripsi" 
                    HeaderStyle-Width="500px" ItemStyle-Width = "500px">
                </asp:BoundField>
                <asp:BoundField DataField="CompanyId" HeaderText="Kontraktor" 
                    HeaderStyle-Width="350px" ItemStyle-Width = "350px">
                </asp:BoundField>
                <%--<asp:TemplateField HeaderText="Kontraktor" HeaderStyle-Width="250px" ItemStyle-Width = "250px">
                    <ItemTemplate>
                        <asp:Label ID="CompanyNm" runat="server" Text='<%# Bind("CompanyNm") %>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="StatusJob" HeaderText="Status" 
                    HeaderStyle-Width="70px" ItemStyle-Width = "70px">                
                </asp:BoundField>
                <asp:BoundField DataField="Kategori" HeaderText="Kategori" 
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
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>

</asp:Content>
