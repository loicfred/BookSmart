<%@ Page Title="Reset Password | BookSmart" Language="C#" ValidateRequest="false" MasterPageFile="~/accounts/Accounts.Master" AutoEventWireup="true" CodeBehind="reset_password.aspx.cs" Inherits="BookSmart.accounts.reset_password" %>

<asp:Content ContentPlaceHolderID="MASTERHEAD" runat="server"> 
    <title>Reset Password | BookSmart</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <h2 class="mb-1">Reset Password</h2>

    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

    <ul class="nav nav-tabs mb-3" id="resetTab" role="tablist">
        <li class="nav-item" role="presentation">
            <button class="nav-link active" id="email-tab" data-bs-toggle="tab" data-bs-target="#email" type="button" role="tab">Email</button>
        </li>
        <li class="nav-item" role="presentation">
            <button class="nav-link" id="phone-tab" data-bs-toggle="tab" data-bs-target="#phone" type="button" role="tab">Phone</button>
        </li>
    </ul>

    <div class="tab-content" id="resetTabContent">
        <div class="tab-pane fade show active" id="email" role="tabpanel">
            <div class="mb-3">
                <label>Email Address:</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
            </div>
            <asp:Button ID="btnSendReset" runat="server" Text="Send Mail" CssClass="btn btn-primary w-100" OnClick="btnSendMail" />
        </div>
        <div class="tab-pane fade" id="phone" role="tabpanel">
            <div class="mb-3">
                <label>Phone Number:</label>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
            </div>
            <asp:Button ID="Button1" runat="server" Text="Send SMS" CssClass="btn btn-primary w-100" OnClick="btnSendSMS" />
        </div>
    </div>
</asp:Content>