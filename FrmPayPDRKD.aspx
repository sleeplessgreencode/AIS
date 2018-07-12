<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmPayPDRKD.aspx.vb" Inherits="AIS.FrmPayPDRKD" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    $(function () {
        $("[id*=GridPD] td").hover(function () {
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

<div style="font-family:Segoe UI Light">
<table>
<tr>
    <td style="font-size:30px; text-decoration:underline">Pembayaran Permintaan Dana</td>
</tr>
</table>
</div>

<div class="font1">
<table>
<tr>
    <td>Filter by</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLField1" runat="server" 
            CssClass="font1" Width="300px" 
            ClientInstanceName="DDLField1" TabIndex="2" Theme="MetropolisBlue" 
            AutoPostBack="True">
            <Items>
                <dx:ListEditItem Text="Pilih salah satu" Value="0" Selected="True" />
                <dx:ListEditItem Text="Job No" Value="A.JobNo" />
                <dx:ListEditItem Text="Nama Proyek" Value="JobNm" />
                <dx:ListEditItem Text="No. PD" Value="NoPD" />
                <dx:ListEditItem Text="No. Ref Lapangan" Value="NoRef" />
                <dx:ListEditItem Text="Tanggal PD" Value="TglPD" />
                <dx:ListEditItem Text="Alokasi" Value="Alokasi" />
                <dx:ListEditItem Text="Tipe Form" Value="TipeForm" />
                <dx:ListEditItem Text="Tanggal Pembayaran" Value="TglBayar" />
                <dx:ListEditItem Text="Deskripsi" Value="Deskripsi" />
                <dx:ListEditItem Text="Total PD" Value="TotalPD" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxComboBox ID="DDLFilterBy1" runat="server" ValueType="System.String" 
            CssClass="font1" Width="200px" 
            ClientInstanceName="DDLFilterBy1" Theme="MetropolisBlue">
            <Items>
                <dx:ListEditItem Text="Equals" Value="0" Selected="True" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxTextBox ID="TxtFind1" runat="server" Width="200px" CssClass="font1">
        </dx:ASPxTextBox>
        <dx:ASPxDateEdit ID="TglFilter1" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            Theme="MetropolisBlue" Visible="False">
        </dx:ASPxDateEdit>
    </td>
    <td rowspan="2" valign="bottom">
        <dx:ASPxButton ID="BtnFind" runat="server" Text="FILTER" 
            Theme="MetropolisBlue" TabIndex="4">
        </dx:ASPxButton>   
    </td>   
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <dx:ASPxComboBox ID="DDLField2" runat="server" 
            CssClass="font1" Width="300px" 
            ClientInstanceName="DDLField2" TabIndex="2" Theme="MetropolisBlue" 
            AutoPostBack="True">
            <Items>
                <dx:ListEditItem Text="Pilih salah satu" Value="0" Selected="True" />
                <dx:ListEditItem Text="Job No" Value="A.JobNo" />
                <dx:ListEditItem Text="Nama Proyek" Value="JobNm" />
                <dx:ListEditItem Text="No. PD" Value="NoPD" />
                <dx:ListEditItem Text="No. Ref Lapangan" Value="NoRef" />
                <dx:ListEditItem Text="Tanggal PD" Value="TglPD" />
                <dx:ListEditItem Text="Alokasi" Value="Alokasi" />
                <dx:ListEditItem Text="Tipe Form" Value="TipeForm" />
                <dx:ListEditItem Text="Tanggal Pembayaran" Value="TglBayar" />
                <dx:ListEditItem Text="Deskripsi" Value="Deskripsi" />
                <dx:ListEditItem Text="Total PD" Value="TotalPD" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxComboBox ID="DDLFilterBy2" runat="server" ValueType="System.String" 
            CssClass="font1" Width="200px" 
            ClientInstanceName="DDLFilterBy2" Theme="MetropolisBlue">
            <Items>
                <dx:ListEditItem Text="Equals" Value="0" Selected="True" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxTextBox ID="TxtFind2" runat="server" Width="200px" CssClass="font1">
        </dx:ASPxTextBox>
        <dx:ASPxDateEdit ID="TglFilter2" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            Theme="MetropolisBlue" Visible="False">
        </dx:ASPxDateEdit>
    </td>
</tr>
</table>

<table>
<tr>
<td>
<dx:ASPxPageControl ID="TabPage" runat="server" ActiveTabIndex="0" 
        Theme="MetropolisBlue">
    <TabPages>
        <dx:TabPage Text="Yang Belum DiBayar" ClientEnabled="true">
            <ContentCollection>
                <dx:ContentControl ID="ContentControl1" runat="server">
                <table style="width: 100%">
                <tr>
                    <td>  
                        <asp:GridView ID="GridPD" runat="server" AutoGenerateColumns="False"               
                            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                            ShowHeaderWhenEmpty="True" 
                            PageSize="50" ShowFooter="True" 
                            AllowPaging="True">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>      
                                <asp:TemplateField HeaderText="Project" HeaderStyle-Width="200px" ItemStyle-Width = "200px">
                                    <ItemTemplate>
                                        <asp:Label ID="LblProject" runat="server" Text='<%# Eval("JobNo").ToString() + " - " + Eval("JobNm").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>               
                                <asp:BoundField DataField="NoPD" HeaderText="No. PD" HeaderStyle-Width="130px" ItemStyle-Width = "130px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TglPD" HeaderText="Tgl PD" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                                    DataFormatString="{0:dd-MMM-yyyy}">                        
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Alokasi" HeaderText="Alokasi" HeaderStyle-Width="60px" ItemStyle-Width = "60px" 
                                ItemStyle-HorizontalAlign="Center">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TipeForm" HeaderText="Tipe Form" HeaderStyle-Width="50px" ItemStyle-Width = "50px" 
                                ItemStyle-HorizontalAlign="Center">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TglBayar" HeaderText="Tanggal Pembayaran" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                                    DataFormatString="{0:dd-MMM-yyyy}">                        
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Deskripsi" HeaderText="Deskripsi" HeaderStyle-Width="400px" ItemStyle-Width = "400px"/>
                                <asp:BoundField DataField="TotalPD" HeaderText="Total PD (Rp)" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                                    DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">                        
                                </asp:BoundField>
                                <asp:BoundField DataField="ApprovedByDK" HeaderText="PD Approved By (DK)" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TimeApprovedDK" HeaderText="PD Approved On (DK)" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                                    DataFormatString="{0:dd-MMM-yyyy}">                        
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Kasir" HeaderText="PD Payment By" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TglKasir" HeaderText="PD Payment On" HeaderStyle-Width="80px" ItemStyle-Width = "80px"
                                    DataFormatString="{0:dd-MMM-yyyy}">                        
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:ButtonField CommandName="BtnPay" Text="PAYMENT" HeaderStyle-Width="45px" />   
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
        <dx:TabPage Text="Yang Sudah DiBayar" ClientEnabled="true">
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
                                <asp:TemplateField HeaderText="Project" HeaderStyle-Width="200px" ItemStyle-Width = "200px">
                                    <ItemTemplate>
                                        <asp:Label ID="LblProject" runat="server" Text='<%# Eval("JobNo").ToString() + " - " + Eval("JobNm").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                   
                                <asp:BoundField DataField="NoPD" HeaderText="No. PD" HeaderStyle-Width="130px" ItemStyle-Width = "130px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TglPD" HeaderText="Tgl PD" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                                    DataFormatString="{0:dd-MMM-yyyy}">                        
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Alokasi" HeaderText="Alokasi" HeaderStyle-Width="60px" ItemStyle-Width = "60px" 
                                ItemStyle-HorizontalAlign="Center">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TipeForm" HeaderText="Tipe Form" HeaderStyle-Width="50px" ItemStyle-Width = "50px" 
                                ItemStyle-HorizontalAlign="Center">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TglBayar" HeaderText="Tanggal Pembayaran" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                                    DataFormatString="{0:dd-MMM-yyyy}">                        
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Deskripsi" HeaderText="Deskripsi" HeaderStyle-Width="400px" ItemStyle-Width = "400px"/>
                                <asp:BoundField DataField="TotalPD" HeaderText="Total PD (Rp)" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                                    DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">                        
                                </asp:BoundField>
                                <asp:BoundField DataField="ApprovedByDK" HeaderText="PD Approved By (DK)" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TimeApprovedDK" HeaderText="PD Approved On (DK)" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                                    DataFormatString="{0:dd-MMM-yyyy}">                        
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Kasir" HeaderText="PD Payment By" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                    
                                </asp:BoundField>
                                <asp:BoundField DataField="TglKasir" HeaderText="PD Payment On" HeaderStyle-Width="80px" ItemStyle-Width = "80px"
                                    DataFormatString="{0:dd-MMM-yyyy}">                        
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:ButtonField CommandName="BtnPay" Text="PAYMENT" HeaderStyle-Width="45px" />   
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
