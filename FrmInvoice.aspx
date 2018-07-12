<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmInvoice.aspx.vb" Inherits="AIS.FrmInvoice" %>
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
<div>
    <dx:ASPxPopupControl ID="ModalEntry" runat="server" CloseOnEscape="True" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ModalEntry"
            HeaderText="Data Entry" Width="700px" Theme="MetropolisBlue" PopupElementID="ModalEntry">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <div align="center">
                <table width="650px">
                <tr>
                    <td align="left">No. KO</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxComboBox ID="DDLKo" runat="server" Width="300px" DropDownWidth="650"
                            DropDownStyle="DropDownList" ValueField="NoKO"
                            ValueType="System.String" TextFormatString="{0} ({2})" IncrementalFilteringMode="Contains"
                            Theme="MetropolisBlue">
                            <Columns>
                                <dx:ListBoxColumn FieldName="NoKO" Width="100px" Caption="No KO" />
                                <dx:ListBoxColumn FieldName="TglKO" Width="100px" Caption="Tgl KO"  />
                                <dx:ListBoxColumn FieldName="VendorNm" Width="250px" Caption="Vendor" />
                                <dx:ListBoxColumn FieldName="TotalKO" Width="100px" Caption="Total KO (Rp)" />
                            </Columns>
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings> 
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">No. Invoice</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtInvNo" runat="server" Width="300px" MaxLength="50" AutoCompleteType="Disabled">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings> 
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Tgl Invoice</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxDateEdit ID="TxtTglInv" runat="server" DisplayFormatString="dd-MMM-yyyy"
                            Theme="MetropolisBlue">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td align="left">Jatuh Tempo</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxDateEdit ID="TxtDueDate" runat="server" DisplayFormatString="dd-MMM-yyyy"
                            Theme="MetropolisBlue">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                    <td align="left">No. Faktur Pajak</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtFP" runat="server" Width="200px" MaxLength="30" AutoPostBack="true" AutoCompleteType="Disabled">
                            <MaskSettings Mask="999.999-99.99999999" IncludeLiterals="All" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Tgl Faktur Pajak</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxDateEdit ID="TxtTglFP" runat="server" DisplayFormatString="dd-MMM-yyyy"
                            Theme="MetropolisBlue">
                        </dx:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
                   <td align="left">Total</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtTotal" runat="server" Width="200px" AutoPostBack="true">
                            <MaskSettings Mask="&lt;0..99999999999999g&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">PPN</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtPPN" runat="server" Width="200px">
                            <MaskSettings Mask="&lt;0..99999999999999g&gt;" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">Keterangan</td>
                    <td align="left" valign="top">:</td>
                    <td align="left">
                        <dx:ASPxMemo ID="TxtKeterangan" runat="server" Height="40px" Width="300px" MaxLength="255">
                        </dx:ASPxMemo>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="right" 
                        style="border-bottom:2px; border-bottom-style:solid; border-bottom-color:#0000FF; padding-bottom: 5px; 
                        border-top-style: solid; border-top-width: 2px; border-top-color: #0000FF; padding-top: 5px;">
                        <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN"
                            Theme="MetropolisBlue" Width="80px" UseSubmitBehavior="False">
                        </dx:ASPxButton>                  
                        <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL"
                            Theme="MetropolisBlue" Width="80px" CausesValidation="False"
                            UseSubmitBehavior="False">
                        </dx:ASPxButton>   
                    </td>
                </tr>
                </table>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div>

<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Invoice</div>

<div>
<table>
<tr>
    <td>
        <asp:Label ID="Action" runat="server" Text="" Visible="false"></asp:Label>
    </td>
</tr>
</table>

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
    <td>Filter by</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLField1" runat="server" Width="200px" ClientInstanceName="DDLField1" 
         Theme="MetropolisBlue">
            <Items>
                <dx:ListEditItem Text="" Value="" Selected="True" />
                <dx:ListEditItem Text="No. KO" Value="A.NoKO" />
                <dx:ListEditItem Text="No. Invoice" Value="InvNo" />
                <dx:ListEditItem Text="Vendor" Value="VendorNm" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxTextBox ID="TxtFind1" runat="server" Width="200px">
        </dx:ASPxTextBox>
        <dx:ASPxDateEdit ID="TglFilter1" runat="server"
            DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            Theme="MetropolisBlue" Visible="False">
        </dx:ASPxDateEdit>
    </td>
    <td>
        <dx:ASPxButton ID="BtnFind" runat="server" Text="FILTER" 
            Theme="MetropolisBlue">
        </dx:ASPxButton>   
    </td>   
</tr>
</table>

<table>
<tr>
    <td style="padding-top:10px">
        <dx:ASPxButton ID="BtnAdd" runat="server" Text="TAMBAH" 
            Theme="MetropolisBlue" Width="75px">
        </dx:ASPxButton>
    </td>
</tr>
<tr>
<td>  
    <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False"               
        CellPadding="4" ForeColor="#333333" GridLines="Vertical" AllowSorting="true"
        ShowHeaderWhenEmpty="True" ShowFooter="true">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>     
            <asp:BoundField DataField="NoKO" HeaderText="No. KO" HeaderStyle-Width="150px" ItemStyle-Width = "150px" 
                SortExpression="NoKO" HeaderStyle-Height="20px"> 
            </asp:BoundField>
            <asp:BoundField DataField="VendorNm" HeaderText="Vendor" HeaderStyle-Width="250px"
                ItemStyle-Width="250px" SortExpression="VendorNm">                        
            </asp:BoundField>
            <asp:BoundField DataField="InvNo" HeaderText="No. Invoice" HeaderStyle-Width="150px" ItemStyle-Width = "150px" 
                SortExpression="InvNo" HeaderStyle-Height="20px"> 
            </asp:BoundField>
            <asp:BoundField DataField="InvDate" HeaderText="Tgl Invoice" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="100px"  SortExpression="InvDate" DataFormatString="{0:dd-MMM-yyyy}">                        
            </asp:BoundField>
            <asp:BoundField DataField="DueDate" HeaderText="Jatuh Tempo" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="100px"  SortExpression="DueDate" DataFormatString="{0:dd-MMM-yyyy}">                        
            </asp:BoundField>
            <asp:BoundField DataField="Total" HeaderText="Total (Rp)" HeaderStyle-Width="150px" 
                ItemStyle-Width="150px" SortExpression="Total" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">                        
            </asp:BoundField>
            <asp:BoundField DataField="PPN" HeaderText="PPN (Rp)" HeaderStyle-Width="100px" 
                ItemStyle-Width="100px" SortExpression="Total" DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">                        
            </asp:BoundField>
             <asp:BoundField DataField="FPNo" HeaderText="No. FP" HeaderStyle-Width="150px" ItemStyle-Width = "150px" ItemStyle-HorizontalAlign="Center" 
                SortExpression="FPNo" HeaderStyle-Height="20px"> 
            </asp:BoundField>
            <asp:BoundField DataField="FPDate" HeaderText="Tgl FP" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Width="100px"  SortExpression="FPDate" DataFormatString="{0:dd-MMM-yyyy}">                        
            </asp:BoundField>
            <asp:TemplateField HeaderText="Keterangan">
                <ItemTemplate>
                    <asp:Label ID="LblKeterangan" runat="server" Text='<%# Eval("Keterangan").ToString().Replace(vbCRLF, "<br />") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle CssClass="hiddencol" />
                <ItemStyle CssClass="hiddencol" />
            </asp:TemplateField>
            <asp:ButtonField CommandName="BtnUpdate" Text="UPDATE" HeaderStyle-Width="45px" ItemStyle-HorizontalAlign="Center">                                          
            </asp:ButtonField>
            <asp:ButtonField CommandName="BtnDelete" Text="DELETE" HeaderStyle-Width="45px" ItemStyle-HorizontalAlign="Center">                                          
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
