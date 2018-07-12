<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmRencanaTermin.aspx.vb" Inherits="AIS.FrmRencanaTermin" %>
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
        HeaderText="Data Entry" PopupAnimationType="Fade" 
            Width="700px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div align="center">
                    <table>
                    <tr>
                        <td align="left">Jenis Termin</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxComboBox ID="DDLJenis" runat="server" ValueType="System.String" Theme="MetropolisBlue">
                            <Items>
                                <dx:ListEditItem Text="" Value="" Selected="true" />
                                <dx:ListEditItem Text="Uang Muka" Value="UM" />
                                <dx:ListEditItem Text="Termin" Value="Termin" />
                            </Items>
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Rencana Tgl Jatuh Tempo</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxDateEdit ID="TxtTglRencana" runat="server"
                                DisplayFormatString="dd-MMM-yyyy" Theme="MetropolisBlue" AutoPostBack="true">
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxDateEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Uraian</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtUraian" runat="server" Width="400px" MaxLength="255">
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxTextBox>
                        </td>    
                    </tr>
                    <tr>
                        <td align="left">Persentase Rencana (%)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtPersentase" runat="server" DecimalPlaces="2" NullText="0.00"
                                DisplayFormatString="{0:N2}" Number="0" MaxLength="17" Width="100px" AutoPostBack="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Pembayaran Opname s.d. Saat Ini (A)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtA" runat="server" DecimalPlaces="2" NullText="0"
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" ReadOnly="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Pembayaran Opname Lalu (B)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtB" runat="server" DecimalPlaces="2" NullText="0"
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" ReadOnly="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Nilai Pekerjaan BAP periode saat ini (C = A - B)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtC" runat="server" DecimalPlaces="2" NullText="0"
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" ReadOnly="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Potongan Uang Muka (D = C x n%)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtD" runat="server" DecimalPlaces="2" NullText="0.00"
                                DisplayFormatString="{0:N2}" Number="0" MaxLength="17" Width="100px" AutoPostBack="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2"></td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtUM" runat="server" DecimalPlaces="2" NullText="0"
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" ReadOnly="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Retensi (E = C x 5%)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtE" runat="server" DecimalPlaces="2" NullText="0"
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" ReadOnly="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Total Potongan (F = D + E)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtF" runat="server" DecimalPlaces="2" NullText="0"
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" ReadOnly="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Pembayaran Fisik BAP ini (G = C - F)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtG" runat="server" DecimalPlaces="2" NullText="0"
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" ReadOnly="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">PPN (H = G x 10%)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtH" runat="server" DecimalPlaces="2" NullText="0"
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" ReadOnly="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Total Nett inc PPN (I = G + H)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtI" runat="server" DecimalPlaces="2" NullText="0"
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" ReadOnly="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">PPH (J = G x 3%)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtJ" runat="server" DecimalPlaces="2" NullText="0"
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" ReadOnly="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Netto Inc. PPN (K = I - J)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtK" runat="server" DecimalPlaces="2" NullText="0"
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" ReadOnly="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Netto Exc. PPN (L = K - H)</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtL" runat="server" DecimalPlaces="2" NullText="0"
                                DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" ReadOnly="true">
                                <SpinButtons ShowIncrementButtons="False"/>
                                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                    <RequiredField IsRequired="true" />
                                </ValidationSettings>
                            </dx:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="3" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN"
                                Theme="MetropolisBlue" Width="80px" UseSubmitBehavior="false">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL" CausesValidation="false"
                                Theme="MetropolisBlue" Width="80px"  UseSubmitBehavior="false">
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
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Rencana Termin</div>

<div>
<table>
<tr>
<td>
    <asp:Label ID="LblAction" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="LblTglRencana" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="LblJenis" runat="server" Text="" Visible="false"></asp:Label>
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

<table width="100%">
<tr>
<td>
    <dx:ASPxButton ID="BtnAdd" runat="server" Text="TAMBAH" Theme="MetropolisBlue" Width="80px">
    </dx:ASPxButton>
</td>
</tr>
<tr>
<td>
    <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False"               
        CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
        ShowHeaderWhenEmpty="True" >
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>                        
            <asp:TemplateField HeaderText = "No." HeaderStyle-Width="30px">
                <ItemTemplate>
                    <asp:Label ID="LblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Width="30px" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>      
            </asp:TemplateField>
            <asp:BoundField DataField="LedgerNo" HeaderText="">                    
                <HeaderStyle CssClass="hiddencol" />
                <ItemStyle CssClass="hiddencol" />
            </asp:BoundField>
            <asp:BoundField DataField="TglRencana" HeaderText="Tgl Rencana Jth Tempo" HeaderStyle-Width="100px" ItemStyle-Width = "100px" DataFormatString="{0:dd-MMM-yyyy}">                        
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Jenis" HeaderText="Jenis" HeaderStyle-Width="80px" ItemStyle-Width = "80px">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Uraian" HeaderText="Uraian" HeaderStyle-Width="250px" ItemStyle-Width = "250px">                    
            </asp:BoundField>
            <asp:BoundField DataField="Persentase" HeaderText="Persentase (%)" HeaderStyle-Width="100px" ItemStyle-Width = "100px" 
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}">       
            </asp:BoundField>        
            <asp:BoundField DataField="Bruto" HeaderText="Bruto (Rp)" HeaderStyle-Width="150px" ItemStyle-Width = "150px" 
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">       
            </asp:BoundField>        
            <asp:BoundField DataField="Netto" HeaderText="Netto (Rp)" HeaderStyle-Width="150px" ItemStyle-Width = "150px" 
            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">       
            </asp:BoundField>        
            <asp:ButtonField CommandName="BtnUpdate" Text="SELECT"  HeaderStyle-Width="45px" />    
            <asp:ButtonField CommandName="BtnDelete" Text="DELETE"  HeaderStyle-Width="45px" />                                                                                
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
<cc1:msgBox ID="MsgBox1" runat="server" />
</asp:Content>