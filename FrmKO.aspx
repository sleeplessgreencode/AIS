<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmKO.aspx.vb" Inherits="AIS.FrmKO" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>
<%@ Register Assembly="MetaBuilders.WebControls" Namespace="MetaBuilders.WebControls"
    TagPrefix="mb" %>

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
    <dx:ASPxPopupControl ID="ModalPDF" runat="server" CloseOnEscape="True" Modal="True"
            PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ModalPDF"
            HeaderText="Upload/View PDF" Width="600px" Theme="MetropolisBlue" PopupElementID="ModalPDF">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <div align="center">
                <table width="500px">
                <tr>
                    <td align="left">
                        <asp:Label ID="LblKO" runat="server" Text="" Font-Size="Medium"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">Upload PDF File</td>
                    <td>:</td>
                    <td>
                        <asp:FileUpload ID="PDFUpload" runat="server" CssClass="font1" Width="300px" />
                    </td>
                    <td></td>
                    <td>
                        <asp:LinkButton ID="LnkView" runat="server" Text="View PDF" OnClick="View" 
                            Visible="False"></asp:LinkButton>
                        <asp:Label ID="LblPath" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="right" 
                        style="border-bottom:2px; border-bottom-style:solid; border-bottom-color:#0000FF; padding-bottom: 5px; 
                        border-top-style: solid; border-top-width: 2px; border-top-color: #0000FF; padding-top: 5px;">
                        <dx:ASPxButton ID="BtnClearPDF" runat="server" Text="HAPUS PDF"
                            Theme="MetropolisBlue" Width="80px" UseSubmitBehavior="False">
                        </dx:ASPxButton>                  
                        <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN"
                            Theme="MetropolisBlue" Width="80px" UseSubmitBehavior="False">
                        </dx:ASPxButton>                  
                        <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL"
                            Theme="MetropolisBlue" Width="80px" CausesValidation="False"
                            UseSubmitBehavior="False" AutoPostBack="False">
                            <ClientSideEvents Click="function(s, e) { ModalPDF.Hide();}" />
                        </dx:ASPxButton>   
                    </td>
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
    <td style="font-size:30px; text-decoration:underline">Daftar Kontrak/Purchase Order</td>
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
            ClientInstanceName="DDLJob" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Filter by</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLField1" runat="server" 
            CssClass="font1" Width="300px" 
            ClientInstanceName="DDLField1" Theme="MetropolisBlue" 
            AutoPostBack="True">
            <Items>
                <dx:ListEditItem Text="Pilih salah satu" Value="0" Selected="True" />
                <dx:ListEditItem Text="No. KO" Value="NoKO" />
                <dx:ListEditItem Text="Tgl KO" Value="TglKO" />
                <dx:ListEditItem Text="Vendor" Value="VendorNm" />
                <dx:ListEditItem Text="Kategori" Value="KategoriId" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxComboBox ID="DDLFilterBy1" runat="server" ValueType="System.String" 
            CssClass="font1" Width="200px" 
            ClientInstanceName="DDLFilterBy1" Theme="MetropolisBlue">
            <Items>
                <dx:ListEditItem Text="Equals" Value="0" Selected="True" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxTextBox ID="TxtFind1" runat="server" Width="200px" CssClass="font1">
        </dx:ASPxTextBox>
        <dx:ASPxDateEdit ID="TglFilter1" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            Theme="MetropolisBlue" Visible="False">
        </dx:ASPxDateEdit>
    </td>
    <td rowspan="2" valign="bottom">
        <dx:ASPxButton ID="BtnFind" runat="server" Text="FILTER" 
            Theme="MetropolisBlue">
        </dx:ASPxButton>   
    </td>   
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <dx:ASPxComboBox ID="DDLField2" runat="server" 
            CssClass="font1" Width="300px" 
            ClientInstanceName="DDLField2" Theme="MetropolisBlue" 
            AutoPostBack="True">
            <Items>
                <dx:ListEditItem Text="Pilih salah satu" Value="0" Selected="True" />
                <dx:ListEditItem Text="No. KO" Value="NoKO" />
                <dx:ListEditItem Text="Tgl KO" Value="TglKO" />
                <dx:ListEditItem Text="Vendor" Value="VendorNm" />
                <dx:ListEditItem Text="Kategori" Value="KategoriId" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxComboBox ID="DDLFilterBy2" runat="server" ValueType="System.String" 
            CssClass="font1" Width="200px" 
            ClientInstanceName="DDLFilterBy2" Theme="MetropolisBlue">
            <Items>
                <dx:ListEditItem Text="Equals" Value="0" Selected="True" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td>
        <dx:ASPxTextBox ID="TxtFind2" runat="server" Width="200px" CssClass="font1">
        </dx:ASPxTextBox>
        <dx:ASPxDateEdit ID="TglFilter2" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" Width="200px" 
            Theme="MetropolisBlue" Visible="False">
        </dx:ASPxDateEdit>
    </td>
</tr>
</table>

<table>
<tr>
<td style="padding-top:5px">
    <dx:ASPxButton ID="BtnKontrak" runat="server" Text="TAMBAH KONTRAK" 
        Theme="MetropolisBlue">
    </dx:ASPxButton>
</td>
<td style="padding-top:5px">
    <dx:ASPxButton ID="BtnPo" runat="server" Text="TAMBAH PO" 
        Theme="MetropolisBlue" Width="127px">
    </dx:ASPxButton>
</td>
<td style="padding-top:5px">
    <dx:ASPxButton ID="BtnMix" runat="server" Text="TAMBAH KOMIX" 
        Theme="MetropolisBlue" Width="127px">
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
        PageSize="50" ShowFooter="True" 
        AllowPaging="True">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>                        
            <asp:TemplateField HeaderText = "No." HeaderStyle-Width="30px" ItemStyle-Width="30px">
                <ItemTemplate>
                    <asp:Label ID="LblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>      
            </asp:TemplateField>
            <asp:BoundField DataField="NoKO" HeaderText="No. KO" HeaderStyle-Width="100px" ItemStyle-Width = "100px">
            <ItemStyle HorizontalAlign="Center" />                    
            </asp:BoundField>
            <asp:BoundField DataField="TglKO" HeaderText="Tgl KO" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                DataFormatString="{0:dd-MMM-yyyy}">                        
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="VendorNm" HeaderText="Vendor" HeaderStyle-Width="250px" ItemStyle-Width = "250px">                    
            </asp:BoundField>
            <asp:BoundField DataField="KategoriId" HeaderText="Tipe KO" HeaderStyle-Width="100px" ItemStyle-Width = "100px" 
            ItemStyle-HorizontalAlign="Center">                    
            </asp:BoundField>
            <asp:BoundField DataField="AddendumKe" HeaderText="Add/Revisi Ke" HeaderStyle-Width="80px" ItemStyle-Width = "80px" 
            ItemStyle-HorizontalAlign="Center">                    
            </asp:BoundField>
            <asp:TemplateField HeaderText="Total KO (Rp)" HeaderStyle-Width="150px" ItemStyle-Width = "150px">
                <ItemTemplate>
                    <asp:Label ID="LblTotal" Text='<%# string.Format("{0:N0}",(Eval("SubTotal") - Eval("DiscAmount")) + Eval("PPN")) %>' runat="server"/>                                
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right"></ItemStyle>                            
            </asp:TemplateField>
            <asp:BoundField DataField="ApprovedBy" HeaderText="KO Approved By" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                    
            </asp:BoundField>
            <asp:BoundField DataField="TimeApproved" HeaderText="KO Approved On" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                DataFormatString="{0:dd-MMM-yyyy}">                        
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:ButtonField CommandName="BtnUpdate" Text="SELECT"  HeaderStyle-Width="45px" />                                          
            <asp:ButtonField CommandName="BtnPrint" Text="PRINT" HeaderStyle-Width="45px" />   
            <asp:ButtonField CommandName="BtnPDF" Text="PDF" HeaderStyle-Width="45px" /> 
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
<cc1:msgBox ID="MsgBox1" runat="server" />
<mb:DialogWindow ID="DialogWindow1" runat="server" CenterWindow="True" 
        Resizable="True" WindowHeight="600px" WindowWidth="900px">
</mb:DialogWindow>
</div>

</asp:Content>
