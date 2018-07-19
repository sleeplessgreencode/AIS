<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmSPR.aspx.vb" Inherits="AIS.FrmSPR" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>
<%@ Register Assembly="MetaBuilders.WebControls" Namespace="MetaBuilders.WebControls"
    TagPrefix="mb" %>

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
</div>

<div style="font-family:Segoe UI Light">
    <table>
        <tr>
            <td style="font-size:30px; text-decoration:underline">Daftar Surat Permintaan Material/Alat</td>
        </tr>
    </table>
</div>
<div>
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
    </table>
    <table>
        <tr>
            <td style="padding-top:5px">
                <dx:ASPxButton ID="BtnAddSPR" runat="server" Text="TAMBAH SPR" 
                    Theme="MetropolisBlue">
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
                        <asp:BoundField DataField="NoSPR" HeaderText="No. SPR" HeaderStyle-Width="100px" ItemStyle-Width = "100px">
                        <ItemStyle HorizontalAlign="Center" />                    
                        </asp:BoundField>
                        <asp:BoundField DataField="TglSPR" HeaderText="Tgl SPR" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                            DataFormatString="{0:dd-MMM-yyyy}">                        
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                       <asp:BoundField DataField="UtkPekerjaan" HeaderText="Untuk Pekerjaan" HeaderStyle-Width="400px" ItemStyle-Width = "400px">                    
                        </asp:BoundField>
                        <asp:BoundField DataField="ApprovedBy" HeaderText="SPR Approved By" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                    
                        </asp:BoundField>
                        <asp:BoundField DataField="TimeApproved" HeaderText="SPR Approved On" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                            DataFormatString="{0:dd-MMM-yyyy}">                        
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:ButtonField CommandName="BtnSelect" Text="SELECT"  HeaderStyle-Width="45px" />                                          
                        <asp:ButtonField CommandName="BtnPrint" Text="PRINT" HeaderStyle-Width="45px" />
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
