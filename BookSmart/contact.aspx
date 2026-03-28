<%@ Page Title="" Language="C#" ValidateRequest="false" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="contact.aspx.cs" Inherits="BookSmart.contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MASTERHEAD" runat="server">
    <title>Contact Us | BookSmart</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <div class="container" style="max-width: 800px; margin: auto; padding: 40px 20px; font-family: Arial, sans-serif;">
        <h2 class="mb-4">Contact Us</h2>

        <div class="mb-3">
            <label for="txtName" class="form-label">Name</label>
            <asp:TextBox ID="txtContactName" runat="server" CssClass="form-control" placeholder="Your name" />
        </div>

        <div class="mb-3">
            <label for="txtEmail" class="form-label">Email</label>
            <asp:TextBox ID="txtContactEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Your email" />
        </div>

        <div class="mb-3">
            <label for="txtMessage" class="form-label">Message</label>
            <asp:TextBox ID="txtContactMessage" runat="server" CssClass="form-control" MaxLength="1024" TextMode="MultiLine" Rows="5" placeholder="Your message" />
        </div>

        <asp:Button ID="btnSubmit" runat="server" Text="Send Message" CssClass="btn btn-primary mb-4" OnClick="btnSubmit_Click" />
        
        <asp:Label ID="lblMessage" runat="server" CssClass="text-danger mb-3 d-block"></asp:Label>

        <hr class="my-4" />

        <div>
            <h5>Contact Information</h5>
            <p><strong>Email:</strong> support@booksmart.com</p>
            <p><strong>Phone:</strong> +230 5123 4567</p>
            <p><strong>Address:</strong> 123 Labourdonnais St, Port-Louis, Mauritius</p>
        </div>
    </div>
</asp:Content>
