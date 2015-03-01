<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Clear.aspx.cs" Inherits="ClearLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="phContent" Runat="Server">
    <h1>Clear Change Log</h1>
    <p>Are you sure you want to clear the change log?</p>
    <div class="buttons">
        <asp:Button ID="buttonDelete" Text="Yes, delete it" runat="server" OnClick="buttonDelete_Click" />
        <asp:Button ID="buttonCancel" Text="No, don&rsquo;t delete it" runat="server" OnClick="buttonCancel_Click" />
    </div>
</asp:Content>

