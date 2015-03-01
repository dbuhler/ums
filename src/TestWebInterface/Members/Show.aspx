<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="ShowAllMembers" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="phHead">
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="phContent">
    <h1>All Members</h1>
    <asp:GridView ID="gridViewData" runat="server" AutoGenerateColumns="False" CssClass="record">
        <Columns>
            <asp:BoundField DataField="Person.LastName" DataFormatString="{0}" HeaderText="Last Name" />
            <asp:BoundField DataField="Person.FirstName" DataFormatString="{0}" HeaderText="First Name" />
            <asp:BoundField DataField="Person.Initial" DataFormatString="{0}" HeaderText="Initial" />
            <asp:BoundField DataField="Person.BirthDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Birth Date" />
            <asp:BoundField DataField="MemberNumber" DataFormatString="{0}" HeaderText="Member Number" />
            <asp:HyperLinkField Text="View" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Members/View.aspx?id={0}" />
            <asp:HyperLinkField Text="Edit" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Members/Edit.aspx?id={0}" />
            <asp:HyperLinkField Text="Delete" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Members/Delete.aspx?id={0}" />
        </Columns>
    </asp:GridView>
</asp:Content>