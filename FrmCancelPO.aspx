<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmCancelPO.aspx.vb" Inherits="AIS.FrmCancelPO" %>
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
    <dx:ASPxPopupControl ID="DelMsg" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="DelMsg"
        HeaderText="Approval Confirmation" PopupAnimationType="Fade" EnableViewState="False" 
            Width="500px" PopupElementID="DelMsg" CloseOnEscape="True" 
        Theme="MetropolisBlue">
        <ClientSideEvents Init="function(s, e) { BtnCancel.Focus();  }" />
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div style="text-align:center; font-size:large; font-family:Segoe UI Light;">
                    <asp:Label ID="LblDel" runat="server"></asp:Label>
                    <br /> <br />
                    <div align="center">
                        <table>
                        <tr>
                            <td align="left" valign="top">Alasan Reject</td>
                            <td align="left" valign="top">:</td>
                            <td>
                                <dx:ASPxMemo ID="CancelMemo" runat="server" Height="50px" Width="500px" 
                                    MaxLength="100">
                                    <ValidationSettings Display="Dynamic">
                                        <RequiredField IsRequired="True"/>
                                    </ValidationSettings>            
                                </dx:ASPxMemo>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                            <td align="left">
                                <dx:ASPxButton ID="BtnDel" runat="server" ClientInstanceName="BtnDel"
                                    Text="APPROVE" Theme="MetropolisBlue" Width="75px" AutoPostBack="true">
                                    <ClientSideEvents Click="function(s, e) { DelMsg.Hide();}" />
                                </dx:ASPxButton>                       
                                <dx:ASPxButton ID="BtnCancel" runat="server" AutoPostBack="False" ClientInstanceName="BtnCancel"
                                    Text="BATAL" Theme="MetropolisBlue" Width="75px">
                                    <ClientSideEvents Click="function(s, e) { DelMsg.Hide();}" />
                                </dx:ASPxButton>  
                            </td>
                        </tr>
                        </table>
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div>

<div style="font-family:Segoe UI Light">
<table>
<tr>
    <td style="font-size:30px; text-decoration:underline">Pembatalan KO</td>
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
<tr>
    <td>Filter by</td>
    <td>:</td>
    <td>
        <asp:TextBox ID="TxtFind" runat="server" Width="300px" 
            placeholder="Cari berdasarkan No KO" CssClass="font1" TabIndex="2"></asp:TextBox>
    </td>
    <td>
        <dx:ASPxButton ID="BtnFind" runat="server" Text="CARI" 
            Theme="MetropolisBlue" TabIndex="3">
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
            <asp:BoundField DataField="NoKO" HeaderText="No. KO" HeaderStyle-Width="70px" ItemStyle-Width = "70px">                    
            </asp:BoundField>
            <asp:BoundField DataField="TglKO" HeaderText="Tgl KO" HeaderStyle-Width="80px" ItemStyle-Width = "80px"
                DataFormatString="{0:dd-MMM-yyyy}">                        
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="VendorNm" HeaderText="Vendor" HeaderStyle-Width="200px" ItemStyle-Width = "200px">                    
            </asp:BoundField>
            <asp:BoundField DataField="KategoriId" HeaderText="Tipe KO" HeaderStyle-Width="60px" ItemStyle-Width = "60px" 
            ItemStyle-HorizontalAlign="Center">                    
            </asp:BoundField>
            <asp:TemplateField HeaderText="Total KO (Rp)">
                <ItemTemplate>
                    <asp:Label ID="LblTotal" Text='<%# string.Format("{0:N0}",(Eval("SubTotal") - Eval("DiscAmount")) + Eval("PPN")) %>' runat="server" Width="120px"/>                                
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right"></ItemStyle>                            
            </asp:TemplateField>
            <asp:BoundField DataField="PaymentAmount" HeaderText="Telah Terbayar (Rp)" HeaderStyle-Width="120px" ItemStyle-Width = "120px" 
                DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">                    
            </asp:BoundField>
            <asp:BoundField DataField="CancelReason" HeaderText="Alasan Batal" HeaderStyle-Width="150px" ItemStyle-Width = "150px">                    
            </asp:BoundField>
            <asp:BoundField DataField="CanceledBy" HeaderText="Canceled By" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                    
            </asp:BoundField>
            <asp:BoundField DataField="TimeCancel" HeaderText="Canceled On" HeaderStyle-Width="80px" ItemStyle-Width = "80px"
                DataFormatString="{0:dd-MMM-yyyy}">                        
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:ButtonField CommandName="BtnUpdate" Text="SELECT"  HeaderStyle-Width="45px" />                                          
            <asp:ButtonField CommandName="BtnCancel" Text="CANCEL" HeaderStyle-Width="45px" />   
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