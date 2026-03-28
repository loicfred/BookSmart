<%@ Page Title="Login | BookSmart" Language="C#" ValidateRequest="false" MasterPageFile="~/accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="BookSmart.accounts.login" %>

<asp:Content ContentPlaceHolderID="MASTERHEAD" runat="server"> 
    <title>Login | BookSmart</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <h2 class="mt-1">Login</h2>
    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

    <div class="mb-3">
        <label>Username:</label>
        <asp:TextBox ID="txtUsername" runat="server" MaxLength="64" CssClass="form-control" />
    </div>

    <div class="mb-3">
        <label>Password:</label>
        <asp:TextBox ID="txtPassword" runat="server" MaxLength="32" TextMode="Password" CssClass="form-control" />
    </div>

    <!--<asp:HiddenField ID="RecaptchaToken" runat="server" />
    <script src="https://www.google.com/recaptcha/api.js?render=6LfIy38rAAAAAPYf0HIE6uKe8fhkfG_KPQK_KY7r"></script>
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('6LfIy38rAAAAAPYf0HIE6uKe8fhkfG_KPQK_KY7r', { action: 'submit' }).then(function (token) {
                document.getElementById('<%= RecaptchaToken.ClientID %>').value = token;
        });
    });
    </script>-->

    <div style="display: flex; gap: 5px;">
        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" Width="50%" />
        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="btnRegister_Click" Width="50%" />
    </div>
    <a style="align-self: center; padding-top: 10px;" href="reset_password.aspx" Width="100%">Forgot your password?</a>
</asp:Content>