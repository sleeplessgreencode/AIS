<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmDPPD.aspx.vb" Inherits="AIS.FrmDPPD" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type = "text/javascript">
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
    <td style="font-size:30px; text-decoration:underline">Daftar Permintaan Dana & Pertanggungjawaban Dana</td>
</tr>
</table>
</div>

<div class="font1">
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td colspan="2">
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            CssClass="font1" Width="100%" 
            ClientInstanceName="DDLJob" Theme="MetropolisBlue">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>       
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Alokasi</td>
    <td>:</td>
    <td colspan="2">
        <dx:ASPxComboBox ID="DDLAlokasi" runat="server" ValueType="System.String" 
            CssClass="font1" Width="100%" 
            ClientInstanceName="DDLAlokasi" Theme="MetropolisBlue">
            <ValidationSettings Display="Dynamic">
                <RequiredField IsRequired="True"/>
            </ValidationSettings>       
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Periode</td>
    <td>:</td>
    <td>
        <dx:ASPxDateEdit ID="PrdAwal" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" 
            Theme="MetropolisBlue">
        </dx:ASPxDateEdit>        
    </td>
    <td>s.d.</td>
    <td>
        <dx:ASPxDateEdit ID="PrdAkhir" runat="server" CssClass="font1" 
            DisplayFormatString="dd-MMM-yyyy" Theme="MetropolisBlue">
            <DateRangeSettings StartDateEditID="PrdAwal"></DateRangeSettings>
        </dx:ASPxDateEdit>
    </td>
    <td>
        <asp:CheckBox ID="CbAll" runat="server" Text="All Periode" />
    </td>
</tr>
<tr>
    <td colspan="2"></td>
    <td>
        <dx:ASPxButton ID="BtnPrint" runat="server" Text="PRINT" 
            Theme="MetropolisBlue" Width="75px">
            <ClientSideEvents Click="function(s,e) { OpenNewTab(); }" />
        </dx:ASPxButton>  
    </td>
</tr>
</table>

</div>

</asp:Content>
