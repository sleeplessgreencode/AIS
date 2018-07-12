<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmChangePasswd.aspx.vb" Inherits="AIS.FrmChangePasswd" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.font1
{font-family:Tahoma; font-size: 12px;}        
</style>
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
    <td style="font-size:30px; text-decoration:underline">Change Password</td>
</tr>
</table>
</div>

<div class="font1">
<table>
<tr>
    <td>Old Password</td>
    <td>:</td>
    <td> 
        <dx:ASPxTextBox ID="TxtOldPw" runat="server" Width="250px" MaxLength="15" 
            TabIndex="1" Password="True">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxTextBox>
    </td>    
    <td></td>
</tr>
<tr>
    <td>New Password</td>
    <td>:</td>
    <td> 
        <dx:ASPxTextBox ID="TxtNewPw" runat="server" Width="250px" MaxLength="15" 
            TabIndex="2" Password="True">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>            
        </dx:ASPxTextBox>
    </td>    
</tr>
<tr>
    <td>Confirm Password</td>
    <td>:</td>
    <td> 
        <dx:ASPxTextBox ID="TxtConfirmPw" runat="server" Width="250px" MaxLength="15" 
            TabIndex="3" Password="True">         
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>        
        </dx:ASPxTextBox>
    </td>   
</tr>
<tr>
    <td colspan="2"></td>
    <td style="padding-top:5px">
        <dx:ASPxButton ID="BtnSave" runat="server" Text="SIMPAN" 
            Theme="MetropolisBlue" TabIndex="4">
        </dx:ASPxButton>
    </td>
</tr>

</table>
  

</div>

</asp:Content>
