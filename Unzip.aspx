<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site1.Master" CodeBehind="Unzip.aspx.vb" Inherits="AIS.Unzip" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:FileUpload ID="FileUpload1" runat="server" />
<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" />
<hr />
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#3AC0F2"
    HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2">
    <Columns>
        <asp:BoundField DataField="FileName" HeaderText="File Name" />
        <asp:BoundField DataField="CompressedSize" HeaderText="Compressed Size (Bytes)" />
        <asp:BoundField DataField="UncompressedSize" HeaderText="Uncompressed Size (Bytes)" />
    </Columns>
</asp:GridView>
</asp:Content>
