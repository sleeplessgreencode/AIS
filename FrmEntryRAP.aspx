<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmEntryRAP.aspx.vb" Inherits="AIS.FrmEntryRAP" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.font1
{font-family:Tahoma; font-size: 12px;}        
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div>
    <dx:ASPxPopupControl ID="ErrMsg" runat="server" CloseAction="CloseButton" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="ErrMsg"
        HeaderText="Information" PopupAnimationType="Fade" EnableViewState="False" 
            Width="500px" PopupElementID="ErrMsg" CloseOnEscape="True" 
        Theme="MetropolisBlue">
        <ClientSideEvents Init="function(s, e) { BtnClose.Focus();  }" />
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
    <td style="font-size:30px; text-decoration:underline">
        <asp:Label ID="LblJudul" runat="server"></asp:Label>
    </td>
</tr>
<tr>
    <td>
        <asp:Label ID="LblAction" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblJobNo" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblJobNm" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblKdRAP" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblSource" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblVersi" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblAlokasi" runat="server" Text="" Visible="false"></asp:Label>
    </td>
</tr>
</table>
</div>

<div class="font1">
<table>
<tr>
    <td>Tipe</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLTipe" runat="server" 
            CssClass="font1" Width="200px" 
            ClientInstanceName="DDLTipe" TabIndex="1" Theme="MetropolisBlue" AutoPostBack="True">
            <Items>
                <dx:ListEditItem Text="Pilih salah satu" Value="0" />
                <dx:ListEditItem Text="Header" Value="Header" />
                <dx:ListEditItem Text="Detail" Value="Detail" />
            </Items>
        </dx:ASPxComboBox>
    </td>
    <td></td>
    <td>
        <asp:Label ID="LblHeader" runat="server" Text="Header"></asp:Label>
    </td>
    <td>
        <asp:Label ID="LblDot" runat="server" Text=":"></asp:Label>
    </td>
    <td>
        <dx:ASPxComboBox ID="DDLHeader" runat="server" 
            CssClass="font1" Width="500px" 
            ClientInstanceName="DDLHeader" TabIndex="0" Theme="MetropolisBlue">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Alokasi</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLAlokasi" runat="server" 
            CssClass="font1" Width="200px" 
            ClientInstanceName="DDLAlokasi" TabIndex="2" Theme="MetropolisBlue" Enabled="False">            
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Kode RAP</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtKdRAP" runat="server" Width="100px" MaxLength="10" TabIndex="3">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxTextBox>
    </td>
</tr>
<tr>
    <td>Uraian Pekerjaan</td>
    <td>:</td>
    <td colspan="5">
        <dx:ASPxTextBox ID="TxtUraian" runat="server" Width="500px" MaxLength="200" TabIndex="4">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxTextBox>
    </td>
</tr>
<tr>
    <td>Volume</td>
    <td>:</td>
    <td>
        <dx:ASPxSpinEdit ID="TxtVol" runat="server" DecimalPlaces="3" 
            DisplayFormatString="{0:N3}" Number="0" MaxLength="12" Width="150px" 
            TabIndex="5" Enabled="False">                 
        <SpinButtons ShowIncrementButtons="False"/>
        </dx:ASPxSpinEdit>
    </td>
</tr>    
<tr>
    <td>Satuan</td>
    <td>:</td>
    <td>
        <dx:ASPxTextBox ID="TxtUom" runat="server" Width="50px" MaxLength="15" TabIndex="6" Enabled="false">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxTextBox>
    </td>
</tr>
<tr>
    <td>Harga Satuan</td>
    <td>:</td>
    <td>
        <dx:ASPxSpinEdit ID="TxtHrgSatuan" runat="server" DecimalPlaces="2" 
            DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" 
            TabIndex="7" Enabled="False">
        <SpinButtons ShowIncrementButtons="False"/>
        </dx:ASPxSpinEdit>
    </td>
</tr>
<tr>
    <td><asp:Label ID="LblRAB" runat="server" Text="Harga Satuan (RAB)"></asp:Label></td>
    <td><asp:Label ID="LblRAB1" runat="server" Text=":"></asp:Label></td>
    <td>
        <dx:ASPxSpinEdit ID="TxtHrgRAB" runat="server" DecimalPlaces="2" 
            DisplayFormatString="{0:N0}" Number="0" MaxLength="17" Width="200px" 
            TabIndex="8" Enabled="False">
        <SpinButtons ShowIncrementButtons="False"/>
        </dx:ASPxSpinEdit>
    </td>
</tr>
<tr>
    <td></td>    
    <td></td>
    <td>
        <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN" 
            Theme="MetropolisBlue" TabIndex="9" Width="75px">
        </dx:ASPxButton>     
        <dx:ASPxButton ID="BtnCancel" runat="server" Text="BATAL" 
            Theme="MetropolisBlue" TabIndex="10" Width="75px" CausesValidation="False">
        </dx:ASPxButton>                  
    </td>
</tr>
</table>

</div>
</asp:Content>
