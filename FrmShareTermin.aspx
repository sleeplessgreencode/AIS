<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmShareTermin.aspx.vb" Inherits="AIS.FrmShareTermin" %>
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
    <dx:ASPxPopupControl ID="PopEntry" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopEntry"
        HeaderText="Data Entry" PopupAnimationType="Fade" 
            Width="700px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue">
        <ClientSideEvents Init="function(s, e) {}" EndCallback="function(s, e) { PopEntry.Show(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div align="center">
                    <table>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TxtAction" runat="server" Text="" 
                                BorderColor="White" BorderStyle="None" ForeColor="White" Width="30px"></asp:TextBox>
                        </td>
                    </tr> 
                    <tr>
                        <td align="left">Tanggal</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxDateEdit ID="TglShare" runat="server" CssClass="font1" 
                                DisplayFormatString="dd-MMM-yyyy" TabIndex="1"
                                Theme="MetropolisBlue">
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="LblMember1" runat="server" Text=""></asp:Label>
                        </td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtTermin1" runat="server" DecimalPlaces="2" 
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" 
                                TabIndex="4">
                            <SpinButtons ShowIncrementButtons="False"/>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">KSO</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtTerminKSO" runat="server" DecimalPlaces="2" 
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" 
                                TabIndex="5">
                            <SpinButtons ShowIncrementButtons="False"/>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="LblMember2" runat="server" Text=""></asp:Label>
                        </td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtTermin2" runat="server" DecimalPlaces="2" 
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" 
                                TabIndex="6">
                            <SpinButtons ShowIncrementButtons="False"/>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Keterangan</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtKeterangan" runat="server" Width="400px" MaxLength="255" TabIndex="2">
                            </dx:ASPxTextBox>
                        </td>    
                    </tr>       
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="3" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN"
                                Theme="MetropolisBlue" TabIndex="7" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL" CausesValidation="false"
                                Theme="MetropolisBlue" TabIndex="8" Width="80px" AutoPostBack="False">
                            </dx:ASPxButton>   
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    </table>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div>

<div style="font-family:Segoe UI Light">
<table>
<tr>
    <td style="font-size:30px; text-decoration:underline">Monitoring Share Termin</td>
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
</table>

<table>
<tr>
<td style="padding-top:5px">
    <dx:ASPxButton ID="BtnAdd" runat="server" Text="TAMBAH" 
        Theme="MetropolisBlue" TabIndex="2" Width="80px">
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
        PageSize="50" ShowFooter="True">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>                        
            <asp:TemplateField HeaderText = "No." ItemStyle-Width="30px" HeaderStyle-Width="30px">
                <ItemTemplate>
                    <asp:Label ID="LblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>      
            </asp:TemplateField>
            <asp:BoundField DataField="TglShare" HeaderText="Tanggal" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                DataFormatString="{0:dd-MMM-yyyy}">                        
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Keterangan" HeaderText="Keterangan" HeaderStyle-Width="400px" ItemStyle-Width = "400px">                    
            </asp:BoundField>
            <asp:BoundField DataField="ShareMember1" HeaderText="Member1" HeaderStyle-Width="150px" ItemStyle-Width = "150px" 
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">                    
            </asp:BoundField>
            <asp:BoundField DataField="ShareKSO" HeaderText="KSO" HeaderStyle-Width="150px" ItemStyle-Width = "150px" 
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">                    
            </asp:BoundField>
            <asp:BoundField DataField="ShareMember2" HeaderText="Member2" HeaderStyle-Width="150px" ItemStyle-Width = "150px" 
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">                    
            </asp:BoundField>
            <asp:ButtonField CommandName="BtnUpdate" Text="SELECT"  HeaderStyle-Width="45px" />                                          
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
