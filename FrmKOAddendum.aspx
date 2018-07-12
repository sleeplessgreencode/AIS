<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmKOAddendum.aspx.vb" Inherits="AIS.FrmKOAddendum" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    $(function () {
        $("[id*=GridData1] td").hover(function () {
            $("td", $(this).closest("tr")).addClass("hover_row");
        }, function () {
            $("td", $(this).closest("tr")).removeClass("hover_row");
        });
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
    <td style="font-size:30px; text-decoration:underline">KO Addendum/Revisi</td>
</tr>
</table>
</div>

<div class="font1">
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            CssClass="font1" Width="300px" 
            ClientInstanceName="DDLJob" TabIndex="1" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>No KO</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLKo" runat="server" ValueType="System.String" 
            CssClass="font1" Width="300px" 
            ClientInstanceName="DDLKo" TabIndex="2" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
</table>

<table>
<tr>
<td style="padding-top:10px">
    <dx:ASPxButton ID="BtnAdd" runat="server" Text="TAMBAH ADDENDUM/REVISI" 
        Theme="MetropolisBlue" TabIndex="5">
    </dx:ASPxButton>
    <asp:Label ID="LblAdd" runat="server" Visible="False"></asp:Label>
</td>
</tr>
<tr>
    <td align="center" bgcolor="silver" style="height:20px; font-weight:bold">CURRENT ADDENDUM/REVISI</td>   
</tr>
<tr>
    <td>  
        <asp:GridView ID="GridData1" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
            ShowHeaderWhenEmpty="True" AllowSorting="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="NoKO" HeaderText="No. KO" 
                    HeaderStyle-Width="100px" ItemStyle-Width = "100px">
                </asp:BoundField>
                <asp:BoundField DataField="TglKO" HeaderText="Tgl KO" HeaderStyle-Width="80px" ItemStyle-Width = "80px"
                    DataFormatString="{0:dd-MMM-yyyy}">                        
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Penerima Kontrak" HeaderStyle-Width="250px" ItemStyle-Width = "250px">
                    <ItemTemplate>
                        <asp:Label ID="VendorNm" runat="server" Text='<%# Bind("VendorNm") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="KategoriId" HeaderText="Tipe KO" HeaderStyle-Width="100px" ItemStyle-Width = "100px" 
                ItemStyle-HorizontalAlign="Center">                    
                </asp:BoundField>
                <asp:BoundField DataField="AddendumKe" HeaderText="Addendum/Revisi Ke" 
                    HeaderStyle-Width="70px" ItemStyle-Width = "70px" ItemStyle-HorizontalAlign="Center">                
                </asp:BoundField>
                <asp:TemplateField HeaderText="Total KO (Rp)" HeaderStyle-Width="150px" ItemStyle-Width = "150px">
                <ItemTemplate>
                    <asp:Label ID="LblTotal" Text='<%# string.Format("{0:N0}",Eval("SubTotal")) %>' runat="server"/>                                
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right"></ItemStyle>                            
                </asp:TemplateField>
                <asp:BoundField DataField="ApprovedBy" HeaderText="KO Approved By" HeaderStyle-Width="100px" ItemStyle-Width = "100px">                    
                </asp:BoundField>
                <asp:BoundField DataField="TimeApproved" HeaderText="KO Approved On" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                    DataFormatString="{0:dd-MMM-yyyy}">                        
                <ItemStyle HorizontalAlign="Center" />
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
<tr><td></td></tr>
<tr>
    <td align="center" bgcolor="silver" style="height:20px; font-weight:bold">HISTORY ADDENDUM/REVISI</td>   
</tr>
<tr>
    <td>  
        <asp:GridView ID="GridData" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
            ShowHeaderWhenEmpty="True" AllowPaging="True" AllowSorting="True"
            PageSize="50">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="NoKO" HeaderText="No. KO" 
                    HeaderStyle-Width="100px" ItemStyle-Width = "100px">
                </asp:BoundField>
                <asp:BoundField DataField="TglKO" HeaderText="Tgl KO" HeaderStyle-Width="80px" ItemStyle-Width = "80px"
                    DataFormatString="{0:dd-MMM-yyyy}">                        
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Penerima Kontrak" HeaderStyle-Width="250px" ItemStyle-Width = "250px">
                    <ItemTemplate>
                        <asp:Label ID="VendorNm" runat="server" Text='<%# Bind("VendorNm") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="KategoriId" HeaderText="Tipe KO" HeaderStyle-Width="100px" ItemStyle-Width = "100px" 
                ItemStyle-HorizontalAlign="Center">                    
                </asp:BoundField>
                <asp:BoundField DataField="AddendumKe" HeaderText="Addendum/Revisi Ke" 
                    HeaderStyle-Width="70px" ItemStyle-Width = "70px" ItemStyle-HorizontalAlign="Center">                
                </asp:BoundField>
                <asp:TemplateField HeaderText="Total KO (Rp)" HeaderStyle-Width="150px" ItemStyle-Width = "150px">
                <ItemTemplate>
                    <asp:Label ID="LblTotal" Text='<%# string.Format("{0:N0}",Eval("SubTotal")) %>' runat="server"/>                                
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right"></ItemStyle>                            
                </asp:TemplateField>
                <asp:BoundField DataField="ApprovedBy" HeaderText="KO Approved By" HeaderStyle-Width="100px" ItemStyle-Width = "100px">                    
                </asp:BoundField>
                <asp:BoundField DataField="TimeApproved" HeaderText="KO Approved On" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                    DataFormatString="{0:dd-MMM-yyyy}">                        
                <ItemStyle HorizontalAlign="Center" />
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
