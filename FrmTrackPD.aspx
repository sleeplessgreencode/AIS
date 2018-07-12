<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="FrmTrackPD.aspx.vb" Inherits="AIS.FrmTrackPD" %>
<%@ Register assembly="DevExpress.Web.v17.2, Version=17.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
<%@ Register assembly="msgBox" namespace="BunnyBear" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="title" 
        style="border-bottom-style: solid; border-bottom-width: 2px; border-bottom-color: #C0C0C0; margin-bottom: 5px;">Tracking PD</div>

<div>
<table>
<tr>
    <td>Job No</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLJob" runat="server" ValueType="System.String" 
            Width="300px" ClientInstanceName="DDLJob" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
<tr>
    <td>Alokasi</td>
    <td>:</td>
    <td>
        <dx:ASPxComboBox ID="DDLAlokasi" runat="server" ValueType="System.String" 
            Width="300px" ClientInstanceName="DDLAlokasi" Theme="MetropolisBlue" AutoPostBack="True">
        </dx:ASPxComboBox>
    </td>
</tr>
</table>

<table style="width: 100%">
<tr>
    <td>  
        <dx:ASPxGridView ID="Grid" ClientInstanceName="Grid" runat="server" 
            KeyFieldName="NoPD" Theme="MetropolisBlue" AutoGenerateColumns="False" Width="100%">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="NoPD" Width="150" Caption="No. PD" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TglPD" Width="100" Caption="Tgl PD"  >
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yy}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TipeForm" Width="60" Caption="Tipe Form"  HeaderStyle-Wrap="True">
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TimeApprovedAK" Width="110" Caption="AK" >
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yy HH:mm}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TimeApprovedKK" Width="110" Caption="KK" >
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yy HH:mm}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TimeApprovedKT" Width="110" Caption="KT" >
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yy HH:mm}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TimeApprovedDP" Width="110" Caption="DP" >
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yy HH:mm}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TimeApprovedDK" Width="110" Caption="DK" >
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yy HH:mm}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TglBayar" Width="110" Caption="KS" >
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yy HH:mm}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PjTimeApprovedAK" Width="110" Caption="PJ-AK" >
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yy HH:mm}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PjTimeApprovedKK" Width="110" Caption="PJ-KK" >
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yy HH:mm}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="RejectBy" Width="150" Caption="Reject By" >
                    <Settings AutoFilterCondition="Contains" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TimeReject" Width="110" Caption="Reject On" >
                    <PropertiesTextEdit DisplayFormatString="{0:dd-MMM-yy HH:mm}"></PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" ShowFooter="true" HorizontalScrollBarMode="Visible" />
            <SettingsBehavior ColumnResizeMode="Control" />
            <SettingsPager Position="Bottom" PageSizeItemSettings-Visible="true" PageSizeItemSettings-Position="Right" PageSize="20">
                <PageSizeItemSettings Items="10, 20, 50" />
            </SettingsPager>
        </dx:ASPxGridView>
    </td>    
</tr>    
</table>   
<cc1:msgBox ID="msgBox1" runat="server" /> 
</div>

</asp:Content>
