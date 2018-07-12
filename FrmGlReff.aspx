<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmGlReff.aspx.vb" Inherits="AIS.FrmGlReff" %>
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
            HeaderText="Data Entry" Width="500px" Theme="MetropolisBlue" PopupElementID="ModalEntry">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <div align="center">
                <table width="400px">
                <tr>
                    <td align="left">Site</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxComboBox ID="DDLSite" runat="server" ValueType="System.String" 
                            Theme="MetropolisBlue">
                            <Items>
                                <dx:ListEditItem Selected="True" Text="Member1" Value="Member1" />
                                <dx:ListEditItem Text="Member2" Value="Member2" />
                                <dx:ListEditItem Text="KSO" Value="KSO" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Member</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtMember" runat="server" Width="100px" MaxLength="10" AutoCompleteType="Disabled">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </dx:ASPxTextBox>  
                    </td>
                </tr>
                <tr>
                    <td align="left">Kasir</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtKasir" runat="server" Width="200px" MaxLength="30" AutoCompleteType="Disabled">
                        </dx:ASPxTextBox>  
                    </td>
                </tr>
                <tr>
                    <td align="left">Image</td>
                    <td align="left">:</td>
                    <td align="left"> 
                        <asp:FileUpload ID="FileUpload1" runat="server" Width="350px" />
                    </td>
                    <td>
                        <asp:Label ID="LblImage" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td>Resolusi 135x80 pixel</td>
                </tr>
                <tr>
                    <td colspan="3" align="right" 
                        style="border-bottom:2px; padding-bottom: 5px; border-top-style: solid; border-top-width: 2px; border-top-color: #0000FF; padding-top: 5px;">
                        <asp:Label ID="LblAction" runat="server" Text="" Visible="false"></asp:Label>
                        <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN" UseSubmitBehavior="false" 
                            Width="75px" Theme="MetropolisBlue">
                        </dx:ASPxButton>     
                        <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL" UseSubmitBehavior="false" 
                            Theme="MetropolisBlue" Width="75px"
                            CausesValidation="False">
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
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">GL Reference</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            CssClass="font1" Width="250px" 
            ClientInstanceName="DDLJob" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td colspan="3" style="padding-top:10px">
        <dx:ASPxButton ID="BtnAdd" runat="server" Text="TAMBAH" 
            Theme="MetropolisBlue" Width="75px" UseSubmitBehavior="false">
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
            <asp:BoundField DataField="Site" HeaderText="Site" HeaderStyle-Width="100px" ItemStyle-Width = "100px" 
                SortExpression="Site" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Height="20px"> 
            </asp:BoundField>
            <asp:BoundField DataField="Member" HeaderText="Member" HeaderStyle-Width="100px"
                ItemStyle-Width="100px" SortExpression="Initial" HeaderStyle-HorizontalAlign="Left">                        
            </asp:BoundField>
            <asp:BoundField DataField="Kasir" HeaderText="Kasir" HeaderStyle-Width="150px"
                ItemStyle-Width="150px" SortExpression="Kasir" HeaderStyle-HorizontalAlign="Left">                        
            </asp:BoundField>
            <asp:ImageField DataImageUrlField="Logo" HeaderText="Image" HeaderStyle-Width="447px"
                ItemStyle-Width="447px" SortExpression="Logo">
            </asp:ImageField>
            <asp:ButtonField CommandName="BtnUpdate" Text="SELECT" HeaderStyle-Width="45px">                                          
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
