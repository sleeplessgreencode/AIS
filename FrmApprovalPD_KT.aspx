<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmApprovalPD_KT.aspx.vb" Inherits="AIS.FrmApprovalPD_KT" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
$(function () {
    $("[id*=GridPD] td").hover(function () {
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
                        <dx:ASPxButton ID="BtnDel" runat="server" ClientInstanceName="BtnDel"
                            Text="YES" Theme="MetropolisBlue" Width="75px" AutoPostBack="true">
                            <ClientSideEvents Click="function(s, e) { DelMsg.Hide();}" />
                        </dx:ASPxButton>                       
                        <dx:ASPxButton ID="BtnCancel" runat="server" AutoPostBack="False" ClientInstanceName="BtnCancel"
                            Text="NO" Theme="MetropolisBlue" Width="75px">
                            <ClientSideEvents Click="function(s, e) { DelMsg.Hide();}" />
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
    <td style="font-size:30px; text-decoration:underline">Approval Permintaan Dana (KT)</td>
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
    <td>Alokasi</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLAlokasi" runat="server" ValueType="System.String" 
            CssClass="font1" Width="300px" Enabled="false" 
            ClientInstanceName="DDLAlokasi" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Filter by</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLField1" runat="server" 
            CssClass="font1" Width="300px" 
            ClientInstanceName="DDLField1" TabIndex="2" Theme="MetropolisBlue" 
            AutoPostBack="True">
            <Items>
                <dx:ListEditItem Text="Pilih salah satu" Value="0" Selected="True" />
                <dx:ListEditItem Text="No. PD" Value="NoPD" />
                <dx:ListEditItem Text="No. Ref Lapangan" Value="NoRef" />
                <dx:ListEditItem Text="Tanggal PD" Value="TglPD" />
                <dx:ListEditItem Text="Tipe Form" Value="TipeForm" />
                <dx:ListEditItem Text="Periode Dari" Value="PrdAwal" />
                <dx:ListEditItem Text="Periode Sampai" Value="PrdAkhir" />
                <dx:ListEditItem Text="Deskripsi" Value="Deskripsi" />
                <dx:ListEditItem Text="Total PD" Value="TotalPD" />
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
            Theme="MetropolisBlue" TabIndex="4">
        </dx:ASPxButton>   
    </td>   
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <dx:ASPxComboBox ID="DDLField2" runat="server" 
            CssClass="font1" Width="300px" 
            ClientInstanceName="DDLField2" TabIndex="2" Theme="MetropolisBlue" 
            AutoPostBack="True">
            <Items>
                <dx:ListEditItem Text="Pilih salah satu" Value="0" Selected="True" />
                <dx:ListEditItem Text="No. PD" Value="NoPD" />
                <dx:ListEditItem Text="No. Ref Lapangan" Value="NoRef" />
                <dx:ListEditItem Text="Tanggal PD" Value="TglPD" />
                <dx:ListEditItem Text="Tipe Form" Value="TipeForm" />
                <dx:ListEditItem Text="Periode Dari" Value="PrdAwal" />
                <dx:ListEditItem Text="Periode Sampai" Value="PrdAkhir" />
                <dx:ListEditItem Text="Deskripsi" Value="Deskripsi" />
                <dx:ListEditItem Text="Total PD" Value="TotalPD" />
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

<table style="width: 100%">
<tr>
    <td>  
        <asp:GridView ID="GridPD" runat="server" AutoGenerateColumns="False"               
            CellPadding="4" ForeColor="#333333" GridLines="Vertical" 
            ShowHeaderWhenEmpty="True" 
            PageSize="50" ShowFooter="True" 
            AllowPaging="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>                        
                <asp:BoundField DataField="NoPD" HeaderText="No. PD" HeaderStyle-Width="130px" ItemStyle-Width = "130px">                    
                </asp:BoundField>
                <asp:BoundField DataField="NoRef" HeaderText="No. Ref Lapangan" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                    
                </asp:BoundField>
                <asp:BoundField DataField="TglPD" HeaderText="Tgl PD" HeaderStyle-Width="80px" ItemStyle-Width = "80px"
                    DataFormatString="{0:dd-MMM-yyyy}">                        
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Alokasi" HeaderText="Alokasi" HeaderStyle-Width="60px" ItemStyle-Width = "60px" 
                ItemStyle-HorizontalAlign="Center">                    
                </asp:BoundField>
                <asp:BoundField DataField="TipeForm" HeaderText="Tipe Form" HeaderStyle-Width="60px" ItemStyle-Width = "60px" 
                ItemStyle-HorizontalAlign="Center">                    
                </asp:BoundField>
                <asp:BoundField DataField="PrdAwal" HeaderText="Periode Dari" HeaderStyle-Width="80px" ItemStyle-Width = "80px"
                    DataFormatString="{0:dd-MMM-yyyy}">                        
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PrdAkhir" HeaderText="Periode Sampai" HeaderStyle-Width="80px" ItemStyle-Width = "80px"
                    DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-HorizontalAlign="Center">                                       
                </asp:BoundField>
                <asp:BoundField DataField="Deskripsi" HeaderText="Deskripsi" HeaderStyle-Width="400px" ItemStyle-Width = "400px"/>
                <asp:BoundField DataField="TotalPD" HeaderText="Total PD (Rp)" HeaderStyle-Width="100px" ItemStyle-Width = "100px"
                    DataFormatString="{0:N0}" ItemStyle-HorizontalAlign="Right">                        
                </asp:BoundField>
                <asp:BoundField DataField="ApprovedByKT" HeaderText="PD Approved By (KT)" HeaderStyle-Width="80px" ItemStyle-Width = "80px">                    
                </asp:BoundField>
                <asp:BoundField DataField="TimeApprovedKT" HeaderText="PD Approved On (KT)" HeaderStyle-Width="80px" ItemStyle-Width = "80px"
                    DataFormatString="{0:dd-MMM-yyyy}">                        
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:ButtonField CommandName="BtnUpdate" Text="SELECT"  HeaderStyle-Width="45px" />                                          
                <asp:ButtonField CommandName="BtnApprove" Text="" HeaderStyle-Width="45px" />   
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
