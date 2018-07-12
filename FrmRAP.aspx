<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmRAP.aspx.vb" Inherits="AIS.FrmRAP" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    $(function () {
        $("[id*=GridRAP] td").hover(function () {
            $("td", $(this).closest("tr")).addClass("hover_row");
        }, function () {
            $("td", $(this).closest("tr")).removeClass("hover_row");
        });
    });
    function OpenNewTab() {
        document.forms[0].target = "_blank";
        setTimeout(function () { window.document.forms[0].target = ''; }, 0);
    };
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
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
        HeaderText="Delete Confirmation" PopupAnimationType="Fade" EnableViewState="False" 
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
                            Text="HAPUS" Theme="MetropolisBlue" Width="75px" AutoPostBack="true">
                        </dx:ASPxButton>                       
                        <dx:ASPxButton ID="BtnCancel" runat="server" AutoPostBack="False" ClientInstanceName="BtnCancel"
                            Text="BATAL" Theme="MetropolisBlue" Width="75px">
                            <ClientSideEvents Click="function(s, e) { DelMsg.Hide();}" />
                        </dx:ASPxButton>    
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</div>
<div>
    <dx:ASPxPopupControl ID="VersionEntry" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="VersionEntry"
        HeaderText="Tambah Versi" PopupAnimationType="Fade"
            Width="500px" PopupElementID="VersionEntry" CloseOnEscape="True" 
        Theme="MetropolisBlue">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <div align="center" class="font1">
                <table>
                <tr>
                    <td align="left">Perubahan</td>
                    <td align="left">:</td>
                    <td align="left">
                        <asp:RadioButton ID="RdoMajor" runat="server" AutoPostBack="True" 
                            GroupName="VersiGroup" Text="Major" />
                    </td>
                    <td></td>
                    <td align="left">
                        <asp:RadioButton ID="RdoMinor" runat="server" AutoPostBack="True" 
                            GroupName="VersiGroup" Text="Minor" />
                    </td>
                </tr>
                <tr>
                    <td align="left">Versi terkini</td>
                    <td align="left">:</td>
                    <td align="left" colspan="3">
                        <dx:ASPxTextBox ID="TxtCurrVersi" runat="server" Width="250px" Enabled="False">
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">Rubah menjadi versi</td>
                    <td align="left">:</td>
                    <td align="left" colspan="3">
                        <dx:ASPxTextBox ID="TxtVersi" runat="server" Width="250px" Enabled="False">
                        </dx:ASPxTextBox>  
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td align="left">
                    <dx:ASPxButton ID="BtnSave" runat="server" ClientInstanceName="BtnSave"
                        Text="SIMPAN" Theme="MetropolisBlue" Width="75px" AutoPostBack="true">
                        <ClientSideEvents Click="function(s, e) {}"></ClientSideEvents>
                    </dx:ASPxButton>                       
                    <dx:ASPxButton ID="BtnCcl" runat="server" AutoPostBack="False" ClientInstanceName="BtnCcl"
                        Text="BATAL" Theme="MetropolisBlue" Width="75px">
                        <ClientSideEvents Click="function(s, e) { VersionEntry.Hide();}"></ClientSideEvents>
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
    <td style="font-size:30px; text-decoration:underline">RAP</td>
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
            CssClass="font1" Width="300px" 
            ClientInstanceName="DDLAlokasi" TabIndex="3" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Versi</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLVersion" runat="server" ValueType="System.String" 
            CssClass="font1" Width="300px" 
            ClientInstanceName="DDLVersion" TabIndex="2" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Filter by</td>
    <td>:</td>
    <td>
        <asp:TextBox ID="TxtFind" runat="server" Width="300px" 
            placeholder="Cari berdasarkan uraian" CssClass="font1" TabIndex="4"></asp:TextBox>
    </td>
    <td>
        <dx:ASPxButton ID="BtnFind" runat="server" Text="CARI" 
            Theme="MetropolisBlue" TabIndex="5">
        </dx:ASPxButton>   
    </td>    
</tr>
</table>

<table>
<tr>
<td style="padding-top:10px">
    <dx:ASPxButton ID="BtnAdd" runat="server" Text="TAMBAH DATA" 
        Theme="MetropolisBlue" Width="75px">
    </dx:ASPxButton>
</td>
<td style="padding-top:10px">
    <dx:ASPxButton ID="BtnVersi" runat="server" Text="TAMBAH VERSI" 
        Theme="MetropolisBlue" Width="75px">
    </dx:ASPxButton>
</td>
<td style="padding-top:10px">
    <dx:ASPxButton ID="BtnPrint" runat="server" Text="PRINT" 
        Theme="MetropolisBlue" Width="75px">
        <ClientSideEvents Click="function(s,e) { OpenNewTab(); }" />
    </dx:ASPxButton>
</td>
<td style="padding-top:10px">    
</td>
</tr>
</table>

<table>
<tr>
<td>  
    <asp:GridView ID="GridRAP" runat="server" AutoGenerateColumns="False"               
        CellPadding="4" ForeColor="#333333" GridLines="Both" 
        ShowHeaderWhenEmpty="True" ShowFooter="True" OnDataBound="OnDataBound" style="margin-top: 0px">
        <%--<AlternatingRowStyle BackColor="White" ForeColor="#284775" />--%>
        <Columns>                        
            <asp:BoundField DataField="Tipe" HeaderText="Tipe">     
                <HeaderStyle CssClass="hiddencol" />
                <ItemStyle CssClass="hiddencol" />                                               
            </asp:BoundField>
            <asp:BoundField DataField="KdRAP" HeaderText="Kode RAP" HeaderStyle-Width="50px" ItemStyle-Width = "50px">                                       
            </asp:BoundField>
            <asp:BoundField DataField="NoUrut" HeaderText="No Urut" >                                                                
                <HeaderStyle CssClass="hiddencol" />
                <ItemStyle CssClass="hiddencol" />
            </asp:BoundField>
            <asp:BoundField DataField="Uraian" HeaderText="Uraian" HeaderStyle-Width="450px" ItemStyle-Width = "450px">
            </asp:BoundField>
            <asp:BoundField DataField="Uom" HeaderText="Sat" HeaderStyle-Width="35px" ItemStyle-Width = "35px">                        
            </asp:BoundField>
            <asp:BoundField DataField="Vol" HeaderText="Vol" HeaderStyle-Width="50px"
                DataFormatString="{0:N3}" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right">                        
            </asp:BoundField>
            <asp:BoundField DataField="HrgSatuan" 
                HeaderText="Harga Satuan (Rp)" HeaderStyle-Width="80px"
                DataFormatString="{0:N2}" ItemStyle-Width="80px" HeaderStyle-BackColor="#3399FF"  
                ItemStyle-HorizontalAlign="Right" ItemStyle-BackColor="#A1DCF2">                        
            </asp:BoundField>
            <asp:TemplateField HeaderText="Jumlah Harga (Rp)" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" 
                HeaderStyle-BackColor="#3399FF" ItemStyle-BackColor="#A1DCF2">
                <ItemTemplate>
                    <asp:Label ID="LblJumlah" Text='' runat="server" Width="100px" />                                
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bobot (%)" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="70px" 
                HeaderStyle-BackColor="#3399FF" ItemStyle-BackColor="#A1DCF2">
                <ItemTemplate>
                    <asp:Label ID="LblBobotRAP" runat="server" Width="70px" />                                
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="HrgRAB" 
                HeaderText="Harga Satuan (Rp)" HeaderStyle-Width="80px"
                DataFormatString="{0:N2}" ItemStyle-Width="80px" 
                ItemStyle-HorizontalAlign="Right" HeaderStyle-BackColor="#33CC33" ItemStyle-BackColor="#99FF99">
            </asp:BoundField>
            <asp:TemplateField HeaderText="Jumlah Harga (Rp)" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="100px" 
                HeaderStyle-BackColor="#33CC33" ItemStyle-BackColor="#99FF99">
                <ItemTemplate>
                    <asp:Label ID="LblJumlah" Text='' runat="server" Width="100px" />                                
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bobot (%)" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="70px" 
                HeaderStyle-BackColor="#33CC33" ItemStyle-BackColor="#99FF99">
                <ItemTemplate>
                    <asp:Label ID="LblBobotRAB" Text='' runat="server" Width="70px" />                                
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="RAP / RAB (%)" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Right">
                <ItemTemplate>
                    <asp:Label ID="LblBobot" Text='' runat="server" Width="70px" />                                
                </ItemTemplate>
                <HeaderStyle Width="70px" />
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField>
            <asp:ButtonField CommandName="BtnInsert" Text="INSERT" HeaderStyle-Width="45px">
            </asp:ButtonField>
            <asp:ButtonField CommandName="BtnUpdate" Text="SELECT" HeaderStyle-Width="45px">                                          
            </asp:ButtonField>
            <asp:ButtonField CommandName="BtnDelete" Text="DELETE" HeaderStyle-Width="45px">                                          
            </asp:ButtonField>
            <asp:ButtonField HeaderStyle-Width="20px" CommandName="BtnUp" Text="&#x25B2;" >                                          
            </asp:ButtonField>
            <asp:ButtonField HeaderStyle-Width="20px" CommandName="BtnDown" Text="&#x25BC;" >                        
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
</div>

</asp:Content>
