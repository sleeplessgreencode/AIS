<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmTrackingKO.aspx.vb" Inherits="AIS.FrmTrackingKO" %>
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
                    <td align="left">Status</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxComboBox ID="DDLStatus" runat="server" ValueType="System.String" 
                            Width="100px" ClientInstanceName="DDLStatus" Theme="MetropolisBlue" 
                            SelectedIndex="0">
                            <Items>
                                <dx:ListEditItem Selected="True" Text="In Process" Value="0" />
                                <dx:ListEditItem Text="Delivered" Value="1" />
                            </Items>
                        </dx:ASPxComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">Keterangan</td>
                    <td align="left" valign="top">:</td>
                    <td align="left" rowspan="2" valign="top">
                        <dx:ASPxMemo ID="TxtKeterangan" runat="server" Height="75px" Width="400px">
                        </dx:ASPxMemo>
                    </td>
                </tr>
                <tr><td></td></tr>
                <tr>
                    <td align="left">Tanggal</td>
                    <td align="left">:</td>
                    <td align="left">
                        <dx:ASPxDateEdit ID="TxtTanggal" runat="server" DisplayFormatString="dd-MMM-yyyy" Width="200px" 
                            Theme="MetropolisBlue">
                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>      
                        </dx:ASPxDateEdit>
                    </td>
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
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Tracking KO</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            Width="300px" ClientInstanceName="DDLJob" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>    
</tr>
<tr>
    <td>No KO</td>
    <td>:</td>
    <td>
        <%--<dx:ASPxComboBox ID="DDLKo" runat="server" ValueType="System.String" 
            Width="300px" ClientInstanceName="DDLNoKO" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>--%>
        <dx:ASPxComboBox ID="DDLKo" runat="server" Width="300px" DropDownWidth="550"
            DropDownStyle="DropDownList" ValueField="NoKO"
            ValueType="System.String" TextFormatString="{0} ({2})" IncrementalFilteringMode="Contains"
            Theme="MetropolisBlue" AutoPostBack="true">
            <Columns>
                <dx:ListBoxColumn FieldName="NoKO" Width="100px" Caption="No KO" />
                <dx:ListBoxColumn FieldName="TglKO" Width="100px" Caption="Tgl KO"  />
                <dx:ListBoxColumn FieldName="VendorNm" Width="150px" Caption="Vendor" />
                <dx:ListBoxColumn FieldName="TotalKO" Width="100px" Caption="Total KO (Rp)" />
            </Columns>
        </dx:ASPxComboBox>
    </td>    
    <td></td>
    <td>Tgl KO</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="TglKO" runat="server" DisplayFormatString="dd-MMM-yyyy" Enabled="false" 
            Width="200px" Theme="MetropolisBlue">
        </dx:ASPxDateEdit>
    </td>
</tr>
<tr>
    <td colspan="7" align="center" bgcolor="silver" style="font-weight:bold">Lokasi Pengiriman</td>
</tr>
<tr>
    <td>Nama</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtNamaKirim" runat="server" Width="100%" Enabled="false">
        </dx:ASPxTextBox>        
    </td>
    <td></td>
    <td>Alamat</td>
    <td>:</td>
    <td rowspan="2" valign="top">
        <dx:ASPxMemo ID="TxtAlamatKirim" runat="server" Height="45px" Width="400px" Enabled="false">
        </dx:ASPxMemo>
    </td>
</tr>
<tr>    
    <td>Telepon</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtTeleponKirim" runat="server" Width="100%" Enabled="false">
        </dx:ASPxTextBox>  
    </td>
</tr>
<tr>
    <td colspan="3" style="padding-top:10px">
        <dx:ASPxButton ID="BtnAdd" runat="server" Text="ADD STATUS" 
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
            <asp:TemplateField HeaderText = "No." HeaderStyle-Width="30px" ItemStyle-Width="30px">
                <ItemTemplate>
                    <asp:Label ID="LblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>      
            </asp:TemplateField>  
            <asp:BoundField DataField="Trace#" HeaderText="Trace#">
                <HeaderStyle CssClass="hiddencol" />
                <ItemStyle CssClass="hiddencol" />       
            </asp:BoundField>              
            <asp:TemplateField HeaderText="Keterangan" HeaderStyle-Width="400px" ItemStyle-Width = "400px">
                <ItemTemplate>
                    <asp:Label ID="LblKeterangan" runat="server" Text='<%# Eval("Keterangan").ToString().Replace(vbCRLF, "<br />") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Tanggal" HeaderText="Tanggal" HeaderStyle-Width="100px" ItemStyle-Width = "100px" 
                ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd-MMM-yyyy}">                        
            </asp:BoundField>
            <asp:TemplateField HeaderText="Status" HeaderStyle-Width="80px" ItemStyle-Width = "80px">
                <ItemTemplate>
                    <asp:Label ID="LblStatus" runat="server" Text='<%# If(Eval("Status").ToString()="0","In Process","Delivered") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField CommandName="BtnUpdate" Text="UPDATE" HeaderStyle-Width="45px">                                          
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