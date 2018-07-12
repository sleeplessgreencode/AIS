<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmApprovalTermin.aspx.vb" Inherits="AIS.FrmApprovalTermin" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>
<%@ Register Assembly="MetaBuilders.WebControls" Namespace="MetaBuilders.WebControls"
    TagPrefix="mb" %>

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
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Approval Penerimaan Termin</div>

<div>

<table>
<tr>
<td>
    <asp:Label ID="LblAction" runat="server" Text="" Visible="false"></asp:Label>
</td>
</tr>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            Width="300px" ClientInstanceName="DDLJob" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>    
</tr>
</table>

<dx:ASPxPageControl ID="TabPage" runat="server" ActiveTabIndex="2" Theme="Moderno" 
        Paddings-PaddingTop="10px">
<TabPages>
<dx:TabPage Text="Termin Induk" ClientEnabled="true">
<ContentCollection>
<dx:ContentControl ID="ContentControl1" runat="server">
    <table>
    <tr>
        <td>
            <table>
            <tr>
            <td style="width:690px"></td>
            <td>
                <dx:ASPxTextBox ID="TxtTitle" runat="server" Width="255px" Enabled="false" Text="NILAI KONTRAK INC. ADDENDUM" HorizontalAlign="Center" BackColor="#FFFF99">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtBruto" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtFisik" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtPPN" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtPPH" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtNetto" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            </tr>
            </table>
        </td>
    </tr>
    <tr>
    <td>
        <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False"               
            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
            ShowHeaderWhenEmpty="True" PageSize="10">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>                        
                <asp:TemplateField HeaderText = "No." HeaderStyle-Width="30px">
                    <ItemTemplate>
                        <asp:Label ID="LblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Width="30px" />
                    </ItemTemplate>

<HeaderStyle Width="30px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>      
                </asp:TemplateField>
                <asp:BoundField DataField="LedgerNo" HeaderText="">                    
                    <HeaderStyle CssClass="hiddencol" />
                    <ItemStyle CssClass="hiddencol" />
                </asp:BoundField>
                <asp:TemplateField HeaderText = "Tgl Cair" HeaderStyle-Width="100px">
                    <ItemTemplate>
                       <asp:Label ID="LblTglCair" runat="server" Text='<%# Format(Eval("TglCair"), "dd-MMM-yyyy") %>' Width="100px" />
                    </ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>      
                </asp:TemplateField>
                <asp:TemplateField HeaderText = "No. BAP" HeaderStyle-Width="150px" >
                    <ItemTemplate>
                       <asp:Label ID="LblNoBAP" runat="server" Text='<%# If(Len(Eval("NoBAP").ToString()) > 20, Mid(Eval("NoBAP").ToString(), 1, 20) & " " & Mid(Eval("NoBAP").ToString(), 21, Len(Eval("NoBAP").ToString()) - 20), Eval("NoBAP").ToString()) %>' Width="150px" Style="text-align:left;" />
                    </ItemTemplate>

<HeaderStyle Width="150px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>      
                </asp:TemplateField>
                <asp:TemplateField HeaderText = "Uraian" HeaderStyle-Width="250px" >
                    <ItemTemplate>
                       <asp:Label ID="LblUraian" runat="server" Text='<%# Eval("Uraian") %>' Width="250px" Style="text-align:left;" />
                    </ItemTemplate>

<HeaderStyle Width="250px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>      
                </asp:TemplateField>
                <asp:TemplateField HeaderText = "Bruto BOQ (Rp)" HeaderStyle-Width="120px" >
                    <ItemTemplate>
                       <asp:Label ID="LblBrutoBOQ" runat="server" Text='<%#String.Format("{0:N0}", Eval("BrutoBOQ")) %>' Width="120px" Style="text-align:right;" />
                    </ItemTemplate>

<HeaderStyle Width="120px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>      
                </asp:TemplateField>
                <asp:TemplateField HeaderText = "Uang Muka (Rp)" HeaderStyle-Width="120px" >
                    <ItemTemplate>
                       <asp:Label ID="LblUM" runat="server" Text='<%#String.Format("{0:N0}", Eval("UM")) %>' Width="120px" Style="text-align:right;" />
                    </ItemTemplate>

<HeaderStyle Width="120px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>      
                </asp:TemplateField>
                <asp:TemplateField HeaderText = "Retensi (Rp)" HeaderStyle-Width="120px" >
                    <ItemTemplate>
                       <asp:Label ID="LblRetensi" runat="server" Text='<%#String.Format("{0:N0}", Eval("Retensi")) %>' Width="120px" Style="text-align:right;" />
                    </ItemTemplate>

<HeaderStyle Width="120px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>      
                </asp:TemplateField>
                <asp:TemplateField HeaderText = "Netto BOQ (Rp)" HeaderStyle-Width="120px" >
                    <ItemTemplate>
                       <asp:Label ID="LblNettBOQ" runat="server" Text='<%#String.Format("{0:N0}", Eval("TerminInduk")) %>' Width="120px" Style="text-align:right;" />
                    </ItemTemplate>

<HeaderStyle Width="120px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Center"></ItemStyle>      
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fisik (Rp)" HeaderStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Label ID="LblTtlFisik" Text='<%#String.Format("{0:N0}", Eval("TerminInduk") / 1.1) %>' runat="server" Width="120px" Style="text-align:right;" />                                
                    </ItemTemplate>

<HeaderStyle Width="120px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Right"></ItemStyle>                            
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PPN (Rp)" HeaderStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Label ID="LblTtlPPN" Text='<%#String.Format("{0:N0}", (Eval("TerminInduk") / 1.1) * (10 / 100)) %>' runat="server" Width="120px" Style="text-align:right;" />                                
                    </ItemTemplate>

<HeaderStyle Width="120px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Right"></ItemStyle>                            
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PPh Final (Rp)" HeaderStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Label ID="LblTtlPPH" Text='<%#String.Format("{0:N0}", (Eval("TerminInduk") / 1.1) * (3 / 100)) %>' runat="server" Width="120px" Style="text-align:right;" />                                
                    </ItemTemplate>

<HeaderStyle Width="120px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Right"></ItemStyle>                            
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Netto Penerimaan (Rp)" HeaderStyle-Width="120px">
                    <ItemTemplate>
                        <asp:Label ID="LblTtlNetto" Text='<%#String.Format("{0:N0}", Eval("TerminInduk") - (Eval("TerminInduk") / 1.1) * (10 / 100) - (Eval("TerminInduk") / 1.1) * (3 / 100)) %>' runat="server" Width="120px" Style="text-align:right;" />                                
                    </ItemTemplate>

<HeaderStyle Width="120px"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Right"></ItemStyle>                            
                </asp:TemplateField>
                <asp:BoundField DataField="UserApproval" HeaderText="Approved By" HeaderStyle-Width="100px" ItemStyle-Width = "100px">                    
                </asp:BoundField>
                <asp:BoundField DataField="TimeApproval" HeaderText="Approved On" HeaderStyle-Width="150px" ItemStyle-Width = "150px" DataFormatString="{0:dd-MMM-yyyy}">                    
                </asp:BoundField>
                <asp:ButtonField CommandName="BtnApproval" Text=""  HeaderStyle-Width="45px" />    
                <asp:ButtonField CommandName="BtnPDF" Text="PDF"  HeaderStyle-Width="45px" />    
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
    <tr>
        <td>
            <table>
            <tr>
            <td style="width:302px"></td>
            <td>
                <dx:ASPxTextBox ID="TxtTitle1" runat="server" Width="255px" Enabled="false" Text="JUMLAH PENERIMAAN" HorizontalAlign="Center" BackColor="#99FF99">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TtlBrutoBOQ1" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TtlUM1" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TtlRetensi1" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtBruto1" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtFisik1" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtPPN1" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtPPH1" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtNetto1" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table>
            <tr>
            <td style="width:690px"></td>
            <td>
                <dx:ASPxTextBox ID="TxtTitle2" runat="server" Width="255px" Enabled="false" Text="SISA TERMIN" HorizontalAlign="Center" BackColor="#99CCFF">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtBruto2" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtFisik2" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtPPN2" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtPPH2" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            <td>
                <dx:ASPxTextBox ID="TxtNetto2" runat="server" Width="125px" Enabled="false" Text="0" HorizontalAlign="Right">
                </dx:ASPxTextBox>
            </td>
            </tr>
            </table>
        </td>
    </tr>  
    </table>
</dx:ContentControl>
</ContentCollection>
</dx:TabPage>
<dx:TabPage Text="Termin Member" ClientEnabled="true">
<ContentCollection>
<dx:ContentControl ID="ContentControl2" runat="server">
    <table width="100%">
    <tr>
    <td>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"               
            CellPadding="4" ForeColor="#333333" GridLines="Vertical" ShowFooter="true" 
            ShowHeaderWhenEmpty="True" PageSize="10">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>                        
                <asp:TemplateField HeaderText = "No." HeaderStyle-Width="30px" ItemStyle-Width="30px">
                    <ItemTemplate>
                        <asp:Label ID="LblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>      
                </asp:TemplateField>
                <asp:BoundField DataField="LedgerNo" HeaderText="">                    
                    <HeaderStyle CssClass="hiddencol" />
                    <ItemStyle CssClass="hiddencol" />
                </asp:BoundField>
                <asp:BoundField DataField="TglCair" HeaderText="Tanggal Cair" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                    DataFormatString="{0:dd-MMM-yyyy}">                        
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="NoBAP" HeaderText="No. BAP" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                    
                </asp:BoundField>
                <asp:BoundField DataField="Uraian" HeaderText="Uraian" HeaderStyle-Width="250px" ItemStyle-Width = "250px">                    
                </asp:BoundField>
                <asp:BoundField DataField="TerminMember1" HeaderText="Member1 (Rp)" HeaderStyle-Width="120px" ItemStyle-Width = "120px" 
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">                    
                </asp:BoundField>
                <asp:BoundField DataField="TerminMember2" HeaderText="Member2 (Rp)" HeaderStyle-Width="120px" ItemStyle-Width = "120px" 
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">                    
                </asp:BoundField>
                <asp:BoundField DataField="CadanganKSO" HeaderText="Cadangan KSO (Rp)" HeaderStyle-Width="120px" ItemStyle-Width = "120px" 
                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">                    
                </asp:BoundField>
                <asp:BoundField DataField="UserApproval" HeaderText="Approved By" HeaderStyle-Width="100px" ItemStyle-Width = "100px">                    
                </asp:BoundField>
                <asp:BoundField DataField="TimeApproval" HeaderText="Approved On" HeaderStyle-Width="150px" ItemStyle-Width = "150px" DataFormatString="{0:dd-MMM-yyyy}">                    
                </asp:BoundField>
                <asp:ButtonField CommandName="BtnApproval" Text=""  HeaderStyle-Width="45px" />  
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

</div>
<cc1:msgBox ID="MsgBox1" runat="server" />
<mb:DialogWindow ID="DialogWindow1" runat="server" CenterWindow="True" 
        Resizable="True" WindowHeight="600px" WindowWidth="900px">
</mb:DialogWindow>
</asp:Content>
