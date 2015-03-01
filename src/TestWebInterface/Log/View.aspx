<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="ViewLog" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="phHead">
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="phContent">
    <h1>View Change Log</h1>
    <asp:GridView ID="gridViewData" runat="server" AutoGenerateColumns="False" >
        <Columns>
            <asp:BoundField DataField="Time" DataFormatString="{0:yyyy-MM-dd, HH:mm:ss}" HeaderText="Time" />
            <asp:BoundField DataField="Table" DataFormatString="{0}" HeaderText="Table" />
            <asp:BoundField DataField="RecordID" DataFormatString="{0}" HeaderText="Record" />
            <asp:BoundField DataField="Field" DataFormatString="{0}" HeaderText="Field" />
            <asp:BoundField DataField="OldValue" DataFormatString="{0}" HeaderText="Old Value" />
            <asp:BoundField DataField="NewValue" DataFormatString="{0}" HeaderText="New Value" />
            <asp:BoundField DataField="User" DataFormatString="{0}" HeaderText="User" />
        </Columns>
    </asp:GridView>
</asp:Content>