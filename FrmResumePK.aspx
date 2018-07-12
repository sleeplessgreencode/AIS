<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmResumePK.aspx.vb" Inherits="AIS.FrmResumePK" %>
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
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Resume Posisi Keuangan & L/R Komprehensif</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            Width="200px" ClientInstanceName="DDLJob" Theme="MetropolisBlue">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Periode</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLBulan" runat="server" 
            ClientInstanceName="DDLBulan" Theme="MetropolisBlue">
        </dx:ASPxComboBox>
    </td>
    <td></td>
    <td>Tahun</td>
    <td>:</td>
    <td>
        <dx:ASPxSpinEdit ID="TxtTahun" runat="server" Number="0" LargeIncrement="1" 
            AllowMouseWheel="False" AllowNull="False" NullText="0" Width="80px">
        </dx:ASPxSpinEdit>        
    </td>
    <td></td>
    <td>Tampilkan</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLView" runat="server" 
            ClientInstanceName="DDLView" Theme="MetropolisBlue">
            <Items>
                <dx:ListEditItem Text="Mutasi Bulan Ini" Value="0" Selected="true" />
                <dx:ListEditItem Text="Mutasi s.d. Bulan Ini" Value="1" />
            </Items>
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <dx:ASPxButton ID="BtnProcess" runat="server" Text="PROCESS" 
            Theme="MetropolisBlue" Width="75px">
        </dx:ASPxButton>
    </td>
</tr>
</table>

<table>
<tr>
<td>  
    <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False"               
        CellPadding="4" ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>     
            <asp:BoundField DataField="AccNo" HeaderText="Account" HeaderStyle-Width="100px" ItemStyle-Width = "100px" 
                SortExpression="AccNo" HeaderStyle-Height="20px" HeaderStyle-HorizontalAlign="Left"> 
            </asp:BoundField>
            <asp:BoundField DataField="AccName" HeaderText="Uraian" HeaderStyle-Width="350px" HeaderStyle-HorizontalAlign="Left"
                ItemStyle-Width="350px" SortExpression="AccName">                        
            </asp:BoundField>
            <asp:BoundField DataField="SaldoKSO" HeaderText="KSO" HeaderStyle-Width="150px"
                ItemStyle-Width="150px" SortExpression="SaldoKSO" DataFormatString="{0:#,##0;(#,##0)}"
                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">                        
            </asp:BoundField>
            <asp:BoundField DataField="SaldoMember1" HeaderText="Member1" HeaderStyle-Width="150px"
                ItemStyle-Width="150px" SortExpression="SaldoMember1" DataFormatString="{0:#,##0;(#,##0)}" 
                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">                        
            </asp:BoundField>
            <asp:BoundField DataField="SaldoMember2" HeaderText="Member2" HeaderStyle-Width="150px"
                ItemStyle-Width="150px" SortExpression="SaldoMember2" DataFormatString="{0:#,##0;(#,##0)}" 
                HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">                        
            </asp:BoundField>
            <asp:TemplateField HeaderText="Konsolidasi" HeaderStyle-Width="150px" ItemStyle-Width = "150px" SortExpression="Konsolidasi" 
                ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="LblKonsolidasi" Text='<%# string.Format("{0:#,##0;(#,##0)}", Eval("SaldoKSO") + Eval("SaldoMember1") + Eval("SaldoMember2")) %>' runat="server"/>                                
                </ItemTemplate>
            </asp:TemplateField>
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

<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>

</asp:Content>
