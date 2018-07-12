<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmProgress.aspx.vb" Inherits="AIS.FrmProgress" %>
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
                <table width="600px">
                <tr>
                    <td align="left">Tahun</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxSpinEdit ID="TxtTahun" runat="server" Number="0" LargeIncrement="1" 
                            AllowMouseWheel="False" AllowNull="False" NullText="0" Width="80px">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>        
                        </dx:ASPxSpinEdit>  
                    </td>
                </tr>
                <tr>
                    <td align="left">Bulan</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxComboBox ID="DDLBulan" runat="server" 
                            ClientInstanceName="DDLBulan" Theme="MetropolisBlue">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>    
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Rencana Fisik Kumulatif (%)</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtRencanaK" runat="server" Width="200px">
                            <MaskSettings Mask="<0..999g>.<000..999>" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Realisasi Fisik Kumulatif (%)</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtRealisasiK" runat="server" Width="200px">
                            <MaskSettings Mask="<0..999g>.<000..999>" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Realisasi Keuangan Kumulatif (%)</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtRealisasiKeuK" runat="server" Width="200px">
                            <MaskSettings Mask="<0..999g>.<000..999>" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Rencana Fisik Tahun Berjalan (%)</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtRencanaTB" runat="server" Width="200px">
                            <MaskSettings Mask="<0..999g>.<000..999>" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Realisasi Fisik Tahun Berjalan (%)</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtRealisasiTB" runat="server" Width="200px">
                            <MaskSettings Mask="<0..999g>.<000..999>" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Realisasi Keuangan Tahun Berjalan (%)</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxTextBox ID="TxtRealisasiKeuTB" runat="server" Width="200px">
                            <MaskSettings Mask="<0..999g>.<000..999>" />
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="right" 
                        style="border-bottom:2px; padding-bottom: 5px; border-top-style: solid; border-top-width: 2px; border-top-color: #0000FF; padding-top: 5px;">
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
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Progress Fisik</div>

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

<table>
<tr>
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
        ShowHeaderWhenEmpty="True">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>                        
            <asp:BoundField DataField="Tahun" HeaderText="Tahun" HeaderStyle-Width="60px" ItemStyle-Width = "60px" HeaderStyle-Height="20px"
                ItemStyle-HorizontalAlign="Center"> 
            </asp:BoundField>
            <asp:BoundField DataField="Bulan" HeaderText="Bulan" HeaderStyle-Width="80px"
                ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80px" HeaderStyle-HorizontalAlign="Left">
                <HeaderStyle CssClass="hiddencol" />
                <ItemStyle CssClass="hiddencol" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Bulan" HeaderStyle-Height="20px" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:Label ID="LblBulanNm" Text="" runat="server" Width="100px" Style="text-align:center;" />                                
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RencanaK" HeaderText="Rencana Fisik Kumulatif (%)" HeaderStyle-Width="100px"
                ItemStyle-Width="100px" DataFormatString="{0:N3}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                HeaderStyle-BackColor="#3399FF" ItemStyle-BackColor="#A1DCF2">                        
            </asp:BoundField>
            <asp:BoundField DataField="RealisasiK" HeaderText="Realisasi Fisik Kumulatif (%)" HeaderStyle-Width="100px"
                ItemStyle-Width="100px" DataFormatString="{0:N3}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                HeaderStyle-BackColor="#3399FF" ItemStyle-BackColor="#A1DCF2">                        
            </asp:BoundField>
            <asp:BoundField DataField="RealisasiKeuK" HeaderText="Realisasi Keuangan Kumulatif (%)" HeaderStyle-Width="100px"
                ItemStyle-Width="100px" DataFormatString="{0:N3}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                HeaderStyle-BackColor="#3399FF" ItemStyle-BackColor="#A1DCF2">                        
            </asp:BoundField>
            <asp:BoundField DataField="RencanaTB" HeaderText="Rencana Fisik Tahun Berjalan (%)" HeaderStyle-Width="100px"
                ItemStyle-Width="100px" DataFormatString="{0:N3}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                HeaderStyle-BackColor="#33CC33" ItemStyle-BackColor="#99FF99">                        
            </asp:BoundField>
            <asp:BoundField DataField="RealisasiTB" HeaderText="Realisasi Fisik Tahun Berjalan (%)" HeaderStyle-Width="100px"
                ItemStyle-Width="100px" DataFormatString="{0:N3}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                HeaderStyle-BackColor="#33CC33" ItemStyle-BackColor="#99FF99">                        
            </asp:BoundField>
            <asp:BoundField DataField="RealisasiKeuTB" HeaderText="Realisasi Keuangan Tahun Berjalan (%)" HeaderStyle-Width="100px"
                ItemStyle-Width="100px" DataFormatString="{0:N3}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                HeaderStyle-BackColor="#33CC33" ItemStyle-BackColor="#99FF99">                        
            </asp:BoundField>
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
