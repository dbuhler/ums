<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="ViewPerson" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phHead" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="phContent" Runat="Server">
    <h1>View Person</h1>
    <div class="form">
        <div class="fields">
            <div>
                <asp:Label ID="labelTitle" runat="server" AssociatedControlID="textBoxTitle" CssClass="col1">Title:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxTitle" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelLastName" runat="server" AssociatedControlID="textBoxLastName" CssClass="col1">Last Name:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxLastName" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelFirstName" runat="server" AssociatedControlID="textBoxFirstName" CssClass="col1">First Name:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxFirstName" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelInitial" runat="server" AssociatedControlID="textBoxInitial" CssClass="col1">Initial:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxInitial" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelGender" runat="server" AssociatedControlID="radioButtonsGender" CssClass="col1">Gender:</asp:Label><!--
             --><span class="col2">
                    <asp:RadioButtonList ID="radioButtonsGender" runat="server" RepeatLayout="Flow" CssClass="radio" RepeatDirection="Horizontal" Enabled="False">
                        <asp:ListItem Value="f" Text="Female" />
                        <asp:ListItem Value="m" Text="Male" />
                    </asp:RadioButtonList>
                </span>
            </div>
            <div>
                <asp:Label ID="labelBirthDate" runat="server" AssociatedControlID="textBoxBirthDate" CssClass="col1">Birth Date:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxBirthDate" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelDeathDate" runat="server" AssociatedControlID="textBoxDeathDate" CssClass="col1">Death Date:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxDeathDate" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelSin" runat="server" AssociatedControlID="textBoxSin" CssClass="col1">SIN:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxSin" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelMaritalStatus" runat="server" AssociatedControlID="dropDownMaritalStatus" CssClass="col1">Marital Status:</asp:Label><!--
             --><span class="col2">
                    <asp:DropDownList ID="dropDownMaritalStatus" runat="server" DataTextField="Value" DataValueField="Key" Enabled="False" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelComments" runat="server" AssociatedControlID="textBoxComments" CssClass="col1">Comments:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxComments" runat="server" ReadOnly="true" />
                </span>
            </div>
        </div>
        <div class="fields">
            <div>
                <asp:Label ID="labelAddress" runat="server" AssociatedControlID="textBoxAddress" CssClass="col1">Address:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxAddress" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelCity" runat="server" AssociatedControlID="textBoxCity" CssClass="col1">City:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxCity" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelPostalCode" runat="server" AssociatedControlID="textBoxPostalCode" CssClass="col1">Postal Code:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxPostalCode" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelProvinceState" runat="server" AssociatedControlID="textBoxProvinceState" CssClass="col1">Province/State:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxProvinceState" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelCountry" runat="server" AssociatedControlID="textBoxCountry" CssClass="col1">Country:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxCountry" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelHomePhone" runat="server" AssociatedControlID="textBoxHomePhone" CssClass="col1">Home Phone:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxHomePhone" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelWorkPhone" runat="server" AssociatedControlID="textBoxWorkPhone" CssClass="col1">Work Phone:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxWorkPhone" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelCellPhone" runat="server" AssociatedControlID="textBoxCellPhone" CssClass="col1">Cell Phone:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxCellPhone" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelFax" runat="server" AssociatedControlID="textBoxFax" CssClass="col1">Fax:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxFax" runat="server" ReadOnly="true" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelEmail" runat="server" AssociatedControlID="textBoxEmail" CssClass="col1">Email:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxEmail" runat="server" ReadOnly="true" />
                </span>
            </div>
        </div>
        <div class="buttons">
            <asp:Button ID="buttonEdit" Text="Edit Record" runat="server" OnClick="buttonEdit_Click" />
            <asp:Button ID="buttonDelete" Text="Delete Record" runat="server" OnClick="buttonDelete_Click" />
        </div>
    </div>
</asp:Content>

