<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="EditMember" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="phHead">
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.4/themes/smoothness/jquery-ui.css" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.1.0/jquery.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.4/jquery-ui.min.js"></script>
    <script>
        function datepicker_init() {
            $("#phContent_textBoxBirthDate").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "yy-mm-dd",
                showOtherMonths: true,
                selectOtherMonths: true
            });
            $("#phContent_textBoxDeathDate").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "yy-mm-dd",
                showOtherMonths: true,
                selectOtherMonths: true
            });
            $("#phContent_textBoxEffectiveDate").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: "yy-mm-dd",
                showOtherMonths: true,
                selectOtherMonths: true
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="phContent">
    <h1>Edit Member</h1>
    <div class="form">
        <div class="fields">
            <div>
                <asp:Label ID="labelTitle" runat="server" AssociatedControlID="textBoxTitle" CssClass="col1">Title:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxTitle" runat="server" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelLastName" runat="server" AssociatedControlID="textBoxLastName" CssClass="col1">Last Name:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxLastName" runat="server" />
                </span>
                <asp:RequiredFieldValidator ControlToValidate="textBoxLastName" Text="*" CssClass="asterisk" runat="server" />
            </div>
            <div>
                <asp:Label ID="labelFirstName" runat="server" AssociatedControlID="textBoxFirstName" CssClass="col1">First Name:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxFirstName" runat="server" />
                </span>
                <asp:RequiredFieldValidator ControlToValidate="textBoxFirstName" Text="*" CssClass="asterisk" runat="server" />
            </div>
            <div>
                <asp:Label ID="labelInitial" runat="server" AssociatedControlID="textBoxInitial" CssClass="col1">Initial:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxInitial" runat="server" MaxLength="1" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelGender" runat="server" AssociatedControlID="radioButtonsGender" CssClass="col1">Gender:</asp:Label><!--
             --><span class="col2">
                    <asp:RadioButtonList ID="radioButtonsGender" runat="server" RepeatLayout="Flow" CssClass="radio" RepeatDirection="Horizontal">
                        <asp:ListItem Value="f" Text="Female" />
                        <asp:ListItem Value="m" Text="Male" />
                    </asp:RadioButtonList>
                </span>
            </div>
            <div>
                <asp:Label ID="labelBirthDate" runat="server" AssociatedControlID="textBoxBirthDate" CssClass="col1">Birth Date:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxBirthDate" runat="server" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelDeathDate" runat="server" AssociatedControlID="textBoxDeathDate" CssClass="col1">Death Date:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxDeathDate" runat="server" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelSin" runat="server" AssociatedControlID="textBoxSin" CssClass="col1">SIN:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxSin" runat="server" MaxLength="9" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelMaritalStatus" runat="server" AssociatedControlID="dropDownMaritalStatus" CssClass="col1">Marital Status:</asp:Label><!--
             --><span class="col2">
                    <asp:DropDownList ID="dropDownMaritalStatus" runat="server" DataTextField="Value" DataValueField="Key" AppendDataBoundItems="True">
                        <asp:ListItem Selected="True" Text="" />
                    </asp:DropDownList>
                </span>
            </div>
            <div>
                <asp:Label ID="labelComments" runat="server" AssociatedControlID="textBoxComments" CssClass="col1">Comments:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxComments" runat="server" />
                </span>
            </div>
        </div>
        <div class="fields">
            <div>
                <asp:Label ID="labelAddress" runat="server" AssociatedControlID="textBoxAddress" CssClass="col1">Address:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxAddress" runat="server" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelCity" runat="server" AssociatedControlID="textBoxCity" CssClass="col1">City:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxCity" runat="server" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelPostalCode" runat="server" AssociatedControlID="textBoxPostalCode" CssClass="col1">Postal Code:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxPostalCode" runat="server" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelProvinceState" runat="server" AssociatedControlID="textBoxProvinceState" CssClass="col1">Province/State:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxProvinceState" runat="server" MaxLength="2" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelCountry" runat="server" AssociatedControlID="textBoxCountry" CssClass="col1">Country:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxCountry" runat="server" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelHomePhone" runat="server" AssociatedControlID="textBoxHomePhone" CssClass="col1">Home Phone:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxHomePhone" runat="server" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelWorkPhone" runat="server" AssociatedControlID="textBoxWorkPhone" CssClass="col1">Work Phone:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxWorkPhone" runat="server" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelCellPhone" runat="server" AssociatedControlID="textBoxCellPhone" CssClass="col1">Cell Phone:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxCellPhone" runat="server" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelFax" runat="server" AssociatedControlID="textBoxFax" CssClass="col1">Fax:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxFax" runat="server" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelEmail" runat="server" AssociatedControlID="textBoxEmail" CssClass="col1">Email:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxEmail" runat="server" />
                </span>
            </div>
        </div>
        <div class="fields">
            <div>
                <asp:Label ID="labelMemberNumber" runat="server" AssociatedControlID="textBoxMemberNumber" CssClass="col1">Member ID:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxMemberNumber" runat="server" MaxLength="12" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelMemberType" runat="server" AssociatedControlID="dropDownMemberType" CssClass="col1">Member Type:</asp:Label><!--
             --><span class="col2">
                    <asp:DropDownList ID="dropDownMemberType" runat="server" DataTextField="Value" DataValueField="Key" AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="" />
                    </asp:DropDownList>
                </span>
            </div>
            <div>
                <asp:Label ID="labelMemberStatus" runat="server" AssociatedControlID="dropDownMemberStatus" CssClass="col1">Member Status:</asp:Label><!--
             --><span class="col2">
                    <asp:DropDownList ID="dropDownMemberStatus" runat="server" DataTextField="Value" DataValueField="Key" AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="" />
                    </asp:DropDownList>
                </span>
            </div>
            <div>
                <asp:Label ID="labelEffectiveDate" runat="server" AssociatedControlID="textBoxEffectiveDate" CssClass="col1">Effective Date:</asp:Label><!--
             --><span class="col2">
                    <asp:TextBox ID="textBoxEffectiveDate" runat="server" />
                </span>
            </div>
            <div>
                <asp:Label ID="labelMemberStanding" runat="server" AssociatedControlID="dropDownMemberStanding" CssClass="col1">Standing:</asp:Label><!--
             --><span class="col2">
                    <asp:DropDownList ID="dropDownMemberStanding" runat="server" DataTextField="Value" DataValueField="Key" AppendDataBoundItems="true">
                        <asp:ListItem Selected="True" Text="" />
                    </asp:DropDownList>
                </span>
            </div>
        </div>
        <div class="buttons">
            <asp:Button ID="buttonSubmit" Text="Save Changes" runat="server" OnClick="buttonSubmit_Click" />
            <asp:Button ID="buttonCancel" Text="Cancel" runat="server" CausesValidation="false" OnClick="buttonCancel_Click" />
        </div>
    </div>
    <div class="messages">
        <asp:Label ID="labelMessage" runat="server" EnableViewState="False" CssClass="error" />
    </div>
</asp:Content>