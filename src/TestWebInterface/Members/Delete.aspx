<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Delete.aspx.cs" Inherits="DeleteMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="phContent" Runat="Server">
    <h1>Delete Member</h1>
    <p>Are you sure you want to delete the member record for <asp:Label ID="labelName" runat="server" Font-Bold="True" />?</p>
    <div class="buttons">
        <asp:Button ID="buttonDelete" Text="Yes, delete it" runat="server" OnClick="buttonDelete_Click" />
        <asp:Button ID="buttonCancel" Text="No, don&rsquo;t delete it" runat="server" OnClick="buttonCancel_Click" />
    </div>
</asp:Content>

