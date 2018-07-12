<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmSetupAkses.aspx.vb" Inherits="AIS.FrmSetupAkses" %>
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
    <dx:ASPxPopupControl ID="DelMsg" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="DelMsg"
        HeaderText="Delete Confirmation" PopupAnimationType="Fade" EnableViewState="False" 
            Width="500px" PopupElementID="DelMsg" CloseOnEscape="True" 
        Theme="MetropolisBlue">
        <ClientSideEvents Init="function(s, e) { BtnCancel.Focus();  }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div style="text-align:center; font-size:large; font-family:Segoe UI Light;">
                    <asp:Label ID="LblDel" runat="server"></asp:Label>
                    <br /> <br />
                    <div align="center">
                        <dx:ASPxButton ID="BtnDel" runat="server" ClientInstanceName="BtnDel"
                            Text="HAPUS" Theme="MetropolisBlue" Width="75px" AutoPostBack="true">
                            <ClientSideEvents Click="function(s, e) { DelMsg.Hide();}" />
                        </dx:ASPxButton>                       
                        <dx:ASPxButton ID="BtnCancel" runat="server" AutoPostBack="False" ClientInstanceName="BtnCancel"
                            Text="BATAL" Theme="MetropolisBlue" Width="75px">
                            <ClientSideEvents Click="function(s, e) { DelMsg.Hide();}" />
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
    <td style="font-size:30px; text-decoration:underline">Setup Akses</td>
</tr>
</table>
</div>

<div class="font1">
<table>
<tr>
    <td>Filter by</td>
    <td>:</td>
    <td>
        <asp:TextBox ID="TxtFind" runat="server" Width="300px" 
            placeholder="Cari berdasarkan UserName" CssClass="font1" TabIndex="1"></asp:TextBox>
    </td>
    <td>
        <dx:ASPxButton ID="BtnFind" runat="server" Text="CARI" 
            Theme="MetropolisBlue" TabIndex="2">
        </dx:ASPxButton>   
    </td>    
</tr>
</table>

<table>
<tr>
<td style="padding-top:5px">
    <dx:ASPxButton ID="BtnAdd" runat="server" Text="TAMBAH" 
        Theme="MetropolisBlue" TabIndex="3">
    </dx:ASPxButton>
</td>
</tr>

</table>

<table style="width: 100%">
<tr>
<td>
    <asp:GridView ID="GridData" runat="server" AutoGenerateColumns="False"               
        CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
        ShowHeaderWhenEmpty="True" 
        PageSize="50" ShowFooter="True" 
        AllowPaging="True">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>                        
            <asp:TemplateField HeaderText = "No." ItemStyle-Width = "30px">
                <ItemTemplate>
                    <asp:Label ID="LblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>      
            </asp:TemplateField>
            <asp:BoundField DataField="UserID" HeaderText="User ID" HeaderStyle-Width="100px" ItemStyle-Width = "100px">                    
            </asp:BoundField>
            <asp:BoundField DataField="UserName" HeaderText="User Name" HeaderStyle-Width="400px" ItemStyle-Width = "400px">                    
            </asp:BoundField>
            <asp:ButtonField CommandName="BtnUpdate" Text="SELECT"  HeaderStyle-Width="45px" />                                          
            <asp:ButtonField CommandName="BtnDelete" Text="DELETE" HeaderStyle-Width="45px" />   
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

</div>

</asp:Content>
