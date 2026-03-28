<%@ Page Title="" Language="C#" MasterPageFile="~/accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="verify_email.aspx.cs" Inherits="BookSmart.accounts.verify_email" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MASTERHEAD" runat="server">
    <title>Email Verified | BookSmart</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <asp:Label ID="notice" runat="server" style="text-align: center;" Text="Your email address has been successfully verified! You may close this window."></asp:Label>
    <img src="/Assets/Gray_Checkmark.png" width="100" height="100" style="align-self: center;" />
</asp:Content>
