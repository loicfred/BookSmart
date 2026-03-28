<%@ Page Title="Register | BookSmart" Language="C#" ValidateRequest="false" MasterPageFile="~/accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="BookSmart.accounts.register" %>

<asp:Content ContentPlaceHolderID="MASTERHEAD" runat="server"> 
    <title>Register | BookSmart</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <h2 class="mt-1">Register</h2>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />

    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

    <asp:Panel runat="server" ID="details_input">
        <div class="mb-3">
            <label>Username:</label>
            <div style="position: relative;">
                <span style="position: absolute; left: 8px; top: 50%; transform: translateY(-50%); color: gray; pointer-events: none; font-family: sans-serif;">@</span>
                <asp:TextBox ID="txtUsername" runat="server" Style="padding-left: 25px;" MaxLength="64" CssClass="form-control" />
            </div>
        </div>

        <div class="mb-3">
            <label>Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" MaxLength="64" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>First Name:</label>
            <asp:TextBox ID="txtFname" runat="server" MaxLength="64" CssClass="form-control" />
            <label>Last Name:</label>
            <asp:TextBox ID="txtLname" runat="server" MaxLength="64" CssClass="form-control" />
        </div>
        <br />
        <div class="mb-3">
            <label>New Password:</label>
            <asp:TextBox ID="txtPassword" runat="server" MaxLength="32" TextMode="Password" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>Confirm Password:</label>
            <asp:TextBox ID="txtConfirmPassword" runat="server" MaxLength="32" TextMode="Password" CssClass="form-control" />
        </div>

        <!--<div class="mb-3">
            <div class="g-recaptcha" data-sitekey="6LdizH8rAAAAAHuApny3ZkYYeEideAU6-LMdUBIX"></div>
            <script src="https://www.google.com/recaptcha/api.js" async defer></script>
        </div>-->

        <asp:Button ID="btnRegister" runat="server" Text="Register" style="align-self: center;" CssClass="btn btn-success form-control" OnClick="btnRegister_Click" />
    </asp:Panel>

    <asp:Panel runat="server" ID="code_verification" Visible="false">
        <div class="mb-3" style="display: flex; gap: 5px;">
            <asp:TextBox ID="C1" runat="server" TextMode="SingleLine" MaxLength="1" onkeydown="onBackspace()" oninput="moveToNext(this)" CssClass="form-control" />
            <asp:TextBox ID="C2" runat="server" TextMode="SingleLine" MaxLength="1" onkeydown="onBackspace()" oninput="moveToNext(this)" CssClass="form-control" />
            <asp:TextBox ID="C3" runat="server" TextMode="SingleLine" MaxLength="1" onkeydown="onBackspace()" oninput="moveToNext(this)" CssClass="form-control" />
            <asp:TextBox ID="C4" runat="server" TextMode="SingleLine" MaxLength="1" onkeydown="onBackspace()" oninput="moveToNext(this)" CssClass="form-control" />
            <asp:TextBox ID="C5" runat="server" TextMode="SingleLine" MaxLength="1" onkeydown="onBackspace()" oninput="moveToNext(this)" CssClass="form-control" />
            <asp:TextBox ID="C6" runat="server" TextMode="SingleLine" MaxLength="1" onkeydown="onBackspace()" oninput="moveToNext(this)" CssClass="form-control" />
            <script type="text/javascript">
                function moveToNext(currentInput) {
                    if (!/^\d?$/.test(currentInput.value)) currentInput.value = "";
                    if (currentInput.value.length > 0) {
                        if (currentInput.id === 'MASTERCONTENT_C1') {
                            $('#MASTERCONTENT_C2').focus();
                        } else if (currentInput.id === 'MASTERCONTENT_C2') {
                            $('#MASTERCONTENT_C3').focus();
                        } else if (currentInput.id === 'MASTERCONTENT_C3') {
                            $('#MASTERCONTENT_C4').focus();
                        } else if (currentInput.id === 'MASTERCONTENT_C4') {
                            $('#MASTERCONTENT_C5').focus();
                        } else if (currentInput.id === 'MASTERCONTENT_C5') {
                            $('#MASTERCONTENT_C6').focus();
                        } else if (currentInput.id === 'MASTERCONTENT_C6') {
                            $('#MASTERCONTENT_btnConfirm').focus();
                        }
                    }
                }
                function onBackspace(currentInput) {
                    if (event.key === "Backspace") {
                        if (currentInput.id === 'MASTERCONTENT_C6') {
                            $('#MASTERCONTENT_C5').focus();
                        } else if (currentInput.id === 'MASTERCONTENT_C5') {
                            $('#MASTERCONTENT_C4').focus();
                        } else if (currentInput.id === 'MASTERCONTENT_C4') {
                            $('#MASTERCONTENT_C3').focus();
                        } else if (currentInput.id === 'MASTERCONTENT_C3') {
                            $('#MASTERCONTENT_C2').focus();
                        } else if (currentInput.id === 'MASTERCONTENT_C2') {
                            $('#MASTERCONTENT_C1').focus();
                        }
                    }
                }
            </script>
        </div>
        <div style="display: flex; justify-content: center; gap: 5px;">
            <asp:Button ID="btnResend" runat="server" Text="Resend Code" CssClass="btn btn-secondary" OnClick="btnConfirm_Click" />
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="btn btn-success" OnClick="btnConfirm_Click" />
        </div>
    </asp:Panel>
</asp:Content>
