<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmJurnalApproval.aspx.vb" Inherits="AIS.FrmJurnalApproval" %>
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
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Approval Jurnal Harian</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            Width="200px" ClientInstanceName="DDLJob" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Periode</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="TxtTgl1" ClientInstanceName="TxtTgl1" runat="server" 
            Theme="MetropolisBlue" DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            AutoPostBack="True">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>
    </td>
    <td></td>
    <td align="center">s.d.</td>
    <td></td>
    <td>
        <dx:ASPxDateEdit ID="TxtTgl2" ClientInstanceName="TxtTgl2" runat="server" 
            Theme="MetropolisBlue" DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            AutoPostBack="True">
            <DateRangeSettings StartDateEditID="TxtTgl1"></DateRangeSettings>
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>
    </td>
</tr>
<tr>
    <td>Site</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLSite" runat="server" ValueType="System.String" 
            Width="200px" ClientInstanceName="DDLSite" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Filter by</td>
    <td>:</td>
    <td colspan="5">
        <dx:ASPxTextBox ID="TxtFind" runat="server" Width="100%"
            ClientInstanceName="TxtFind" MaxLength="100" AutoCompleteType="Disabled" 
            NullText="Cari berdasarkan no. jurnal" NullTextStyle-Font-Italic="true" 
            Theme="MetropolisBlue">
        </dx:ASPxTextBox>
    </td>
    <td>
        <dx:ASPxButton ID="BtnFind" runat="server" Text="CARI" 
            Theme="MetropolisBlue">
        </dx:ASPxButton>   
    </td>    
</tr>
</table>

<table>
<tr>
<td>  
    <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False"               
        CellPadding="4" ForeColor="#333333" GridLines="Vertical" AllowSorting="true"
        ShowHeaderWhenEmpty="True">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>     
            <asp:TemplateField HeaderText = "No." HeaderStyle-Width="30px" ItemStyle-Width="30">
                <ItemTemplate>
                    <asp:Label ID="LblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>      
            </asp:TemplateField>
            <asp:BoundField DataField="NoJurnal" HeaderText="No. Jurnal" HeaderStyle-Width="150px" ItemStyle-Width = "150px" 
                SortExpression="NoJurnal" HeaderStyle-Height="20px"> 
            </asp:BoundField>
            <asp:BoundField DataField="TglJurnal" HeaderText="Tgl Jurnal" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="100px" DataFormatString="{0:dd-MMM-yyyy}">                        
            </asp:BoundField>
            <asp:BoundField DataField="Nama" HeaderText="Identitas" HeaderStyle-Width="200px"
                ItemStyle-Width="200px" SortExpression="Nama">                        
            </asp:BoundField>
            <asp:BoundField DataField="DebetBalance" HeaderText="Debet" HeaderStyle-Width="150px" 
                ItemStyle-Width="150px" SortExpression="DebetBalance" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">                        
            </asp:BoundField>
            <asp:BoundField DataField="KreditBalance" HeaderText="Kredit" HeaderStyle-Width="150px" 
                ItemStyle-Width="150px" SortExpression="KreditBalance" DataFormatString="{0:N0}" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">                        
            </asp:BoundField>
            <asp:TemplateField HeaderText="Deviasi" HeaderStyle-Width="150px" ItemStyle-Width = "150px"
                ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="LblDeviasi" Text='<%# string.Format("{0:N0}",Eval("DebetBalance") - Eval("KreditBalance")) %>' runat="server"/>                                
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right"></ItemStyle>                            
            </asp:TemplateField>
            <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" HeaderStyle-Width="100px"
                ItemStyle-Width="100px" SortExpression="ApprovedBy" ItemStyle-HorizontalAlign="Center">                        
            </asp:BoundField>
            <asp:BoundField DataField="TimeApproved" HeaderText="Approved On" HeaderStyle-Width="100px"
                ItemStyle-Width="100px" SortExpression="TimeApproved" DataFormatString="{0:dd-MMM-yyyy}"
                ItemStyle-HorizontalAlign="Center">                       
            </asp:BoundField>
            <asp:ButtonField CommandName="BtnView" Text="VIEW" HeaderStyle-Width="45px" ItemStyle-HorizontalAlign="Center">                                          
            </asp:ButtonField>
            <asp:ButtonField CommandName="BtnApprove" Text="" HeaderStyle-Width="45px" ItemStyle-HorizontalAlign="Center">                                          
            </asp:ButtonField>
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
