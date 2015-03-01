<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Find.aspx.cs" Inherits="FindPerson" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="phHead">
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="phContent">
    <h1>Find Person</h1>
    <div class="form">
        <div class="fields">
            <div>
                <asp:Label ID="labelLastName" runat="server" AssociatedControlID="textBoxLastName" CssClass="col1">Last Name:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxLastName" runat="server" />
                </span>
                <asp:RequiredFieldValidator Text="*" CssClass="asterisk" ControlToValidate="textBoxLastName" runat="server" />
            </div>
            <div>
                <asp:Label ID="labelFirstName" runat="server" AssociatedControlID="textBoxFirstName" CssClass="col1">First Name:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxFirstName" runat="server" />
                </span>
            </div>
        </div>
        <div class="buttons">
            <asp:Button ID="buttonSubmit" Text="Search" runat="server" OnClick="buttonSubmit_Click" />
        </div>
    </div>
    <asp:GridView ID="gridViewData" runat="server" AutoGenerateColumns="False" CssClass="record">
        <Columns>
            <asp:BoundField DataField="LastName" DataFormatString="{0}" HeaderText="Last Name" />
            <asp:BoundField DataField="FirstName" DataFormatString="{0}" HeaderText="First Name" />
            <asp:BoundField DataField="Initial" DataFormatString="{0}" HeaderText="Initial" />
            <asp:BoundField DataField="BirthDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Birth Date" />
            <asp:HyperLinkField Text="View" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Persons/View.aspx?id={0}" />
            <asp:HyperLinkField Text="Edit" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Persons/Edit.aspx?id={0}" />
            <asp:HyperLinkField Text="Delete" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/Persons/Delete.aspx?id={0}" />
        </Columns>
    </asp:GridView>
</asp:Content>