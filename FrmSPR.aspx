<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmSPR.aspx.vb" Inherits="AIS.FrmSPR" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>
<%@ Register Assembly="MetaBuilders.WebControls" Namespace="MetaBuilders.WebControls"
    TagPrefix="mb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <dx:ASPxButton ID="BtnKontrak" runat="server" Text="TAMBAH SPR" 
                    Theme="MetropolisBlue">
                </dx:ASPxButton>
            </td>
        </tr>
    </table>
    <table>
        
    </table>
    <cc1:msgBox ID="MsgBox1" runat="server" />
    <mb:DialogWindow ID="DialogWindow1" runat="server" CenterWindow="True" 
            Resizable="True" WindowHeight="600px" WindowWidth="900px">
    </mb:DialogWindow>
</div>
</asp:Content>
