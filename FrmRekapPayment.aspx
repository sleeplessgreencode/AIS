<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmRekapPayment.aspx.vb" Inherits="AIS.FrmRekapPayment" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.font1
{font-family:Tahoma; font-size: 12px;}        
</style>
<script type="text/javascript">
    function OpenNewTab() {
        document.forms[0].target = "_blank";
        setTimeout(function () { window.document.forms[0].target = ''; }, 0);
    };
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

<div style="font-family:Segoe UI Light">
<table>
<tr>
    <td style="font-size:30px; text-decoration:underline">Rekap Pembayaran</td>
</tr>
</table>
</div>

<div class="font1">
<table>
<tr>
    <td>Tgl Pembayaran</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="PrdAwal" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" 
            Theme="MetropolisBlue" TabIndex="2">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>        
    </td>
    <td>s.d.</td>
    <td>
        <dx:ASPxDateEdit ID="PrdAkhir" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" 
            Theme="MetropolisBlue" TabIndex="3">
            <DateRangeSettings StartDateEditID="PrdAwal" />
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxDateEdit>
    </td>
    <td>
        &nbsp;</td>
</tr>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td colspan="3">
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            CssClass="font1" Width="100%" 
            ClientInstanceName="DDLJob" TabIndex="1" Theme="MetropolisBlue">
            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic" SetFocusOnError="True">
                <RequiredField IsRequired="true" />
            </ValidationSettings>
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <dx:ASPxButton ID="BtnPrint" runat="server" Text="PRINT" 
            Theme="MetropolisBlue" Width="75px" TabIndex="5">
            <ClientSideEvents Click="function(s,e) { OpenNewTab(); }" />
        </dx:ASPxButton>  
    </td>
</tr>
</table>

</div>

</asp:Content>
