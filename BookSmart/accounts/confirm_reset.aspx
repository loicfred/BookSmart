<%@ Page Title="New Password | BookSmart" Language="C#" ValidateRequest="false" MasterPageFile="~/accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="confirm_reset.aspx.cs" Inherits="BookSmart.accounts.confirm_reset" %>

<asp:Content ContentPlaceHolderID="MASTERHEAD" runat="server"> 
    <title>New Password | BookSmart</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <h3 class="mt-1">Confirm new password</h3>

    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

    <div class="mb-3">
        <label>New Password:</label>
        <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control" />
    </div>

    <div class="mb-3">
        <label>Confirm Password:</label>
        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" />
    </div>
        
    <div style="display: flex; gap: 5px;">
        <asp:Button ID="btnLogin" runat="server" Text="Save Password" CssClass="btn btn-primary" OnClick="btnLogin_Click" Width="100%" />
    </div>
</asp:Content>