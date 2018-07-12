<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmApprovalKO.aspx.vb" Inherits="AIS.FrmApprovalKO" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
$(function () {
    $("[id*=GridData] td").hover(function () {
        $("td", $(this).closest("tr")).addClass("hover_row");
    }, function () {
        $("td", $(this).closest("tr")).removeClass("hover_row");
    });
    $("[id*=GridView1] td").hover(function () {
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
    <dx:ASPxPopupControl ID="ConfirmMsg" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ConfirmMsg"
        HeaderText="Approval Confirmation" PopupAnimationType="Fade" EnableViewState="False" 
            Width="500px" PopupElementID="ConfirmMsg" CloseOnEscape="True" 
        Theme="MetropolisBlue">
        <ClientSideEvents Init="function(s, e) { BtnCancel.Focus();  }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div style="text-align:center; font-size:large; font-family:Segoe UI Light;">
                    <asp:Label ID="LblApproval" runat="server"></asp:Label>
                    <br /> <br />
                    <div align="center">
                        <dx:ASPxButton ID="BtnConfirm" runat="server" ClientInstanceName="BtnConfirm"
                            Text="APPROVE" Theme="MetropolisBlue" Width="75px" AutoPostBack="true">
                            <ClientSideEvents Click="function(s, e) { ConfirmMsg.Hide();}" />
                        </dx:ASPxButton>                       
                        <dx:ASPxButton ID="BtnCancel" runat="server" AutoPostBack="False" ClientInstanceName="BtnCancel"
                            Text="BATAL" Theme="MetropolisBlue" Width="75px">
                            <ClientSideEvents Click="function(s, e) { ConfirmMsg.Hide();}" />
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
    <td style="font-size:30px; text-decoration:underline">Approval Kontrak/Purchase Order</td>
</tr>
</table>
</div>

<div class="font1">
<table>
<tr>
    <td>Tgl KO</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="PrdAwal" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" AutoPostBack="true" Width="200px" 
            Theme="MetropolisBlue" TabIndex="1">
        </dx:ASPxDateEdit>        
    </td>
    <td>s.d.</td>
    <td>
        <dx:ASPxDateEdit ID="PrdAkhir" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" AutoPostBack="true" Width="200px"  
            Theme="MetropolisBlue">
            <DateRangeSettings StartDateEditID="PrdAwal"></DateRangeSettings>
        </dx:ASPxDateEdit>
    </td>
</tr>
</table>
<table>
<tr>
    <td>Filter by</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLField1" runat="server" 
            CssClass="font1" Width="200px" 
            ClientInstanceName="DDLField1" Theme="MetropolisBlue">
            <Items>
                <dx:ListEditItem Text="Pilih salah satu" Value="0" Selected="True" />
                <dx:ListEditItem Text="Job No" Value="A.JobNo" />
                <dx:ListEditItem Text="No. KO" Value="NoKO" />
                <dx:ListEditItem Text="Vendor" Value="VendorNm" />
                <dx:ListEditItem Text="Tipe KO" Value="KategoriId" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td colspan="2">
        <dx:ASPxComboBox ID="DDLFilterBy1" runat="server" ValueType="System.String" 
            CssClass="font1" Width="150px" 
            ClientInstanceName="DDLFilterBy1" Theme="MetropolisBlue">
            <Items>
                <dx:ListEditItem Text="Equals" Value="0" Selected="True" />
                <dx:ListEditItem Text="Contains" Value="1" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxTextBox ID="TxtFind1" runat="server" Width="200px" CssClass="font1">
        </dx:ASPxTextBox>
    </td>
    <td rowspan="2" valign="bottom">
        <dx:ASPxButton ID="BtnFind" runat="server" Text="FILTER" 
            Theme="MetropolisBlue">
        </dx:ASPxButton>   
    </td>   
</tr>
</table>

<table>
<tr>
<td>
<dx:ASPxPageControl ID="TabPage" runat="server" ActiveTabIndex="0" 
        Theme="MetropolisBlue">
    <TabPages>
        <dx:TabPage Text="Yang Belum Di Approve" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl1" runat="server">
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
                                <asp:TemplateField HeaderText = "No." HeaderStyle-Width="30px" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:Label ID="LblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>      
                                </asp:TemplateField>
                                <asp:BoundField DataField="JobNo" HeaderText="Job No" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="JobNm" HeaderText="Nama Proyek" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="NoKO" HeaderText="No. KO" HeaderStyle-Width="100px" ItemStyle-Width = "100px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TglKO" HeaderText="Tgl KO" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                                    DataFormatString="{0:dd-MMM-yyyy}">                        
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VendorNm" HeaderText="Vendor" HeaderStyle-Width="250px" ItemStyle-Width = "250px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="KategoriId" HeaderText="Tipe KO" HeaderStyle-Width="100px" ItemStyle-Width = "100px" 
                                ItemStyle-HorizontalAlign="Center">                    
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Total KO (Rp)" HeaderStyle-Width="250px" ItemStyle-Width = "250px">
                                    <ItemTemplate>
                                        <asp:Label ID="LblTotal" Text='<%# string.Format("{0:N0}",(Eval("SubTotal") - Eval("DiscAmount")) + Eval("PPN")) %>' runat="server"/>                                
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>                            
                                </asp:TemplateField>
                                <asp:BoundField DataField="ApprovedBy" HeaderText="KO Approved By" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TimeApproved" HeaderText="KO Approved On" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                                    DataFormatString="{0:dd-MMM-yyyy}">                        
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:ButtonField CommandName="BtnUpdate" Text="SELECT"  HeaderStyle-Width="45px" />                                          
                                <asp:ButtonField CommandName="BtnApprove" Text="APPROVE" HeaderStyle-Width="45px" />   
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
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
        <dx:TabPage Text="Yang Sudah Di Approve" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl2" runat="server">
                <table style="width: 100%">
                <tr>
                    <td>  
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"               
                            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                            ShowHeaderWhenEmpty="True" 
                            PageSize="50" ShowFooter="True" 
                            AllowPaging="True">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>                        
                                <asp:TemplateField HeaderText = "No." HeaderStyle-Width="30px" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:Label ID="LblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>      
                                </asp:TemplateField>
                                <asp:BoundField DataField="JobNo" HeaderText="Job No" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="JobNm" HeaderText="Nama Proyek" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="NoKO" HeaderText="No. KO" HeaderStyle-Width="100px" ItemStyle-Width = "100px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TglKO" HeaderText="Tgl KO" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                                    DataFormatString="{0:dd-MMM-yyyy}">                        
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VendorNm" HeaderText="Vendor" HeaderStyle-Width="250px" ItemStyle-Width = "250px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="KategoriId" HeaderText="Tipe KO" HeaderStyle-Width="100px" ItemStyle-Width = "100px" 
                                ItemStyle-HorizontalAlign="Center">                    
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Total KO (Rp)" HeaderStyle-Width="250px" ItemStyle-Width = "250px">
                                    <ItemTemplate>
                                        <asp:Label ID="LblTotal" Text='<%# string.Format("{0:N0}",(Eval("SubTotal") - Eval("DiscAmount")) + Eval("PPN")) %>' runat="server"/>                                
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>                            
                                </asp:TemplateField>
                                <asp:BoundField DataField="ApprovedBy" HeaderText="KO Approved By" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TimeApproved" HeaderText="KO Approved On" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                                    DataFormatString="{0:dd-MMM-yyyy}">                        
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:ButtonField CommandName="BtnUpdate" Text="SELECT"  HeaderStyle-Width="45px" />         
                                <asp:ButtonField CommandName="BtnUnApprove" Text="UNAPPROVE" HeaderStyle-Width="45px" />                                    
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
                </dx:ContentControl>
            </ContentCollection>
        </dx:TabPage>
    </TabPages>
</dx:ASPxPageControl>
</td>
</tr>
</table>

</div>

</asp:Content>
