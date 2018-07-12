<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmRPPM.aspx.vb" Inherits="AIS.FrmRPPM" %>
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
    <dx:ASPxPopupControl ID="PopEntry" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopEntry"
        HeaderText="Data Entry" PopupAnimationType="Fade" Width="700px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue">
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
                        <td align="left">Periode dari</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxDateEdit ID="TxtTgl1" runat="server"
                                DisplayFormatString="dd-MMM-yyyy" Theme="MetropolisBlue">
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Periode sampai</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxDateEdit ID="TxtTgl2" runat="server"
                                DisplayFormatString="dd-MMM-yyyy" Theme="MetropolisBlue">
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                                <DateRangeSettings StartDateEditID="TxtTgl1" />
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Progress Induk (%)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtInduk" runat="server" DecimalPlaces="3" 
                                DisplayFormatString="{0:N3}" Number="0" MaxLength="6" Width="200px">
                            <SpinButtons ShowIncrementButtons="True"/>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Progress Share (%)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtShare" runat="server" DecimalPlaces="3" 
                                DisplayFormatString="{0:N3}" Number="0" MaxLength="6" Width="200px">
                            <SpinButtons ShowIncrementButtons="True"/>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="3" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN"
                                Theme="MetropolisBlue" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL" CausesValidation="false"
                                Theme="MetropolisBlue" Width="80px">
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

<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">RPPM</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            Width="300px" ClientInstanceName="DDLJob" Theme="MetropolisBlue" AutoPostBack="true">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td colspan="2"></td>
    <td style="padding-top:5px">
        <dx:ASPxButton ID="BtnAdd" runat="server" Text="TAMBAH" 
            Theme="MetropolisBlue" Width="80px">
        </dx:ASPxButton>
    </td>
</tr>
</table>

<table style="width: 100%">
<tr>
<td>
    <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False"               
        CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
        ShowHeaderWhenEmpty="True" PageSize="50" ShowFooter="True" AllowPaging="True">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>                        
            <asp:BoundField DataField="Tgl1" HeaderText="Periode Dari" HeaderStyle-Width="100px" ItemStyle-Width = "100px" DataFormatString="{0:dd-MMM-yyyy}">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Tgl2" HeaderText="Periode Sampai" HeaderStyle-Width="100px" ItemStyle-Width = "100px" DataFormatString="{0:dd-MMM-yyyy}">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Induk" HeaderText="Progress Induk (%)" HeaderStyle-Width="100px" ItemStyle-Width = "100px" 
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N3}">                    
            </asp:BoundField>
            <asp:BoundField DataField="Share" HeaderText="Progress Share (%)" HeaderStyle-Width="100px" ItemStyle-Width = "100px" 
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N3}">                    
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

<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>

</asp:Content>
