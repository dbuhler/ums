<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Show.aspx.cs" Inherits="ShowAllPersons" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="phHead">
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="phContent">
    <h1>All Persons</h1>
    <asp:GridView ID="gridViewData" runat="server" AutoGenerateColumns="False" CssClass="record">
        <Columns>
            <asp:BoundField DataField="LastName" DataFormatString="{0}" HeaderText="Last Name" />
            <asp:BoundField DataField="FirstName" DataFormatString="{0}" HeaderText="First Name" />
            <asp:BoundField DataField="Initial" DataFormatString="{0}" HeaderText="Initial" />
            <asp:BoundField DataField="BirthDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Birth Date" />
            <asp:HyperLinkField Text="View" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Persons/View.aspx?id={0}" />
            <asp:HyperLinkField Text="Edit" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Persons/Edit.aspx?id={0}" />
            <asp:HyperLinkField Text="Make Member" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Members/Add.aspx?id={0}" />
            <asp:HyperLinkField Text="Delete" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Persons/Delete.aspx?id={0}" />
        </Columns>
    </asp:GridView>
</asp:Content>