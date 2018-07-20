<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmEntrySPR.aspx.vb" Inherits="AIS.FrmEntrySPR" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</div> <%--Message Box--%>
<div>
    <dx:ASPxPopupControl ID="PopEntry" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="PopEntry"
        HeaderText="Data Entry" PopupAnimationType="Fade" 
            Width="700px" PopupElementID="PopEntry" CloseOnEscape="True" 
        Height="200px" Theme="MetropolisBlue" AllowDragging="True">
        <ClientSideEvents Init="function(s, e) { DDLRap.Focus(); }" EndCallback="function(s, e) { PopEntry.Show(); }" />
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
                        <td align="left">No.</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtNo" runat="server" Width="30px" 
                                ClientInstanceName="TxtNo" Enabled="false" CssClass="font1">
                            </dx:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Alokasi</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxComboBox ID="DDLAlokasi" runat="server" ValueType="System.String" 
                                CssClass="font1" Width="450px" AutoPostBack="true" 
                                ClientInstanceName="DDLAlokasi" Theme="MetropolisBlue">
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Kode RAP</td>
                        <td>:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxComboBox ID="DDLRap" runat="server" ValueType="System.String" 
                                CssClass="font1" Width="450px" 
                                ClientInstanceName="DDLRap" Theme="MetropolisBlue">
                            </dx:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" valign="top">Uraian</td>
                        <td valign="top">:</td>
                        <td align="left" colspan="4">
                            <dx:ASPxMemo ID="TxtUraian" runat="server" Height="100px" Width="445px" 
                                CssClass="font1" MaxLength="255">
                            </dx:ASPxMemo>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Volume</td>
                        <td>:</td>
                        <td align="left">
                            <dx:ASPxSpinEdit ID="TxtVol" runat="server" DecimalPlaces="3" 
                                DisplayFormatString="{0:N3}" Number="0" MaxLength="10" Width="80px" 
                                CssClass="font1">
                            <SpinButtons ShowIncrementButtons="False"/>
                            </dx:ASPxSpinEdit>     
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Satuan</td>
                        <td style="width:7px;">:</td>
                        <td align="left">
                            <dx:ASPxTextBox ID="TxtUom" runat="server" Width="60px" 
                                ClientInstanceName="TxtUom" MaxLength="15">
                            </dx:ASPxTextBox>
                        </td>
                        <td></td>
                    </tr>
                    <tr><td></td></tr>
                    <tr>
                        <td colspan="4" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:10px;">
                            <dx:ASPxButton ID="BtnSaveMaterial" runat="server" Text="SIMPAN" CausesValidation="false"
                                Theme="MetropolisBlue" Width="80px">
                            </dx:ASPxButton>                       
                            <dx:ASPxButton ID="BtnCancelMaterial" runat="server" Text="BATAL"
                                Theme="MetropolisBlue" Width="80px" AutoPostBack="False">
                                <ClientSideEvents Click="function(s, e) { PopEntry.Hide();}" />
                            </dx:ASPxButton>   
                        </td>
                    </tr>
                    </table>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div> <%--Pop Entry--%>
<div style="font-family:Segoe UI Light">
    <table>
        <tr>
            <td style="font-size:30px; text-decoration:underline">
                <asp:Label ID="LblJudul" runat="server" Text="Surat Permintaan Material/Alat"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LblAction" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LblJobNo" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LblNoSPR" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="LblTglSPR" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
</div>

<div class="font1">
    <table>
        <tr>    
            <td colspan="3" align="center" bgcolor="silver" style="height:20px; font-weight:bold">Informasi Dasar</td>   
            <td style="width:25px"></td>
        </tr>
        <tr>    
            <td>No SPR</td>   
            <td>:</td>
            <td>
                <dx:ASPxTextBox runat="server" ID="TxtNoSPR" Enabled="false" width="100%" MaxLength="20">
                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                        <RequiredField IsRequired="true" />
                </ValidationSettings>  
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td>Tgl SPR</td>
            <td>:</td>
            <td>
                <dx:ASPxDateEdit ID="TglSPR" runat="server" CssClass="font1"
                    DisplayFormatString="dd-MMM-yyyy" Width="200px" 
                    Theme="MetropolisBlue">
                    <ValidationSettings Display="Dynamic">
                        <RequiredField IsRequired="True"/>
                    </ValidationSettings>            
                </dx:ASPxDateEdit>
                <asp:Label ID="LblTglSPR1" runat="server" Text="1/1/1900" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>    
            <td>Job No</td>   
            <td>:</td>
            <td>
                <dx:ASPxTextBox runat="server" ID="TxtJobNo" Width="100%" Enabled="false">
                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                        <RequiredField IsRequired="true" />
                </ValidationSettings>  
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>    
            <td>Kepada</td>   
            <td>:</td>
            <td>
                <dx:ASPxTextBox runat="server" ID="TxtKepada" Width="100%" MaxLength="50">
                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                        <RequiredField IsRequired="true" />
                </ValidationSettings>  
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td>Tgl Penggunaan</td>
            <td>:</td>
            <td>
                <dx:ASPxDateEdit ID="TglPenggunaan" runat="server" CssClass="font1"
                    DisplayFormatString="dd-MMM-yyyy" Width="200px" 
                    Theme="MetropolisBlue">
                    <ValidationSettings Display="Dynamic">
                        <RequiredField IsRequired="True"/>
                    </ValidationSettings>            
                </dx:ASPxDateEdit>
            </td>
        </tr>
        <tr>    
            <td>Untuk pekerjaan</td>   
            <td>:</td>
            <td>
                <dx:ASPxMemo ID="TxtUtkPekerjaan" runat="server" Theme="MetropolisBlue" width="100%" MaxLength="100">
                <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                        <RequiredField IsRequired="true" />
                </ValidationSettings>
                </dx:ASPxMemo>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="right" style="border-top:2px; border-top-style:solid; border-top-color:Black; padding-top:5px;">
                <dx:ASPxButton ID="BtnSaveSPR" runat="server" Text="SIMPAN" 
                    Theme="MetropolisBlue" Width="75px">
                </dx:ASPxButton>
                <dx:ASPxButton ID="BtnCancelSPR" runat="server" Text="BATAL" CausesValidation="False"
                    Theme="MetropolisBlue" Width="75px">
                </dx:ASPxButton>          
            </td>
        </tr>
    </table>
</div>
<div>
    <table>
        <tr>
            <td align="center" bgcolor="silver" style="height:20px; font-weight:bold;">P  E  R  M  I  N  T  A  A  N&nbsp; &nbsp;M  A  T  E  R  I  A  L</td>
        </tr>
        <tr>
            <td style="border: 2px solid #C0C0C0">
                <table>
                    <tr>
                        <td style="padding-bottom:5px">
                            <dx:ASPxButton ID="BtnAddMaterial" runat="server" Text="TAMBAH"
                                Theme="MetropolisBlue" Width="80px" AutoPostBack="False" 
                                CausesValidation="False">
                            </dx:ASPxButton>                       
                        </td>           
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridDataMaterial" runat="server" AutoGenerateColumns="False"               
                                CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
                                PageSize="20" ShowHeaderWhenEmpty="True">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>              
                                    <asp:BoundField DataField="NoUrut" HeaderText="No."  HeaderStyle-Width="35px" ItemStyle-Width = "35px" ItemStyle-HorizontalAlign="Center">     
                                    </asp:BoundField>
                                    <asp:BoundField DataField="KdRAP" HeaderText="Kode RAP" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                        
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Uraian" HeaderStyle-Width="550px" ItemStyle-Width = "550px">
                                        <ItemTemplate>
                                            <asp:Label ID="LblUraian" runat="server" Text='<%# Eval("Uraian").ToString().Replace(vbCRLF, "<br />") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Vol" HeaderText="Vol"  HeaderStyle-Width="80px" ItemStyle-Width = "80px" 
                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N3}">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Uom" HeaderText="Sat" HeaderStyle-Width= "30px" ItemStyle-Width = "30px" ItemStyle-HorizontalAlign="Center">                        
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Alokasi" HeaderText="Alokasi" 
                                            HeaderStyle-Width="30px" ItemStyle-Width = "30px">
                                        <HeaderStyle CssClass="hiddencol" />
                                        <ItemStyle CssClass="hiddencol" />
                                    </asp:BoundField>
                                    <asp:ButtonField CommandName="BtnUpdMaterial" Text="SELECT" HeaderStyle-Width="45px"/>                                          
                                    <asp:ButtonField CommandName="BtnDelMaterial" Text="DELETE" HeaderStyle-Width="45px"/>                                                                 
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
            </td>
        </tr>
    </table> <%--Grid Material--%>
</div>
</asp:Content>
