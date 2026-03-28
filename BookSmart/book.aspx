<%@ Page Title="Book Details | BookSmart" Language="C#" ValidateRequest="false" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="book.aspx.cs" Inherits="BookSmart.book" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MASTERHEAD" runat="server">
    <title>Book Details | BookSmart</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container mt-4" style="padding-bottom: 80px;">
        <asp:Panel ID="pnlBookDetails" runat="server" Visible="false">
            <div class="row">
                <div class="col-md-5">
                    <asp:Image ID="imgBook" runat="server" style="width: 500px; padding: 0 15px 15px 15px" CssClass="img-fluid" />
                </div>
                <div class="col-md-7">
                    <h2><asp:Label ID="lblTitle" runat="server" /></h2>
                    <h5 class="text-muted">
                        <asp:Label ID="lblAuthor" runat="server" /> • 
                        <asp:Label ID="lblCategory" runat="server" />
                    </h5>
                    <p><asp:Label ID="lblDescription" runat="server" /></p>
                    <p><strong>Pages:</strong> <asp:Label ID="lblPages" runat="server" /></p>
                    <p><strong>Language:</strong> <asp:Label ID="lblLanguage" runat="server" /></p>
                    <p><strong>Price:</strong> $<asp:Label ID="lblPrice" runat="server" /></p>
                    <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" OnClick="btnAddToCart_Click" CssClass="btn btn-primary" />
                    
                    <hr />

                    <asp:Panel ID="CommentSection" runat="server" class="mt-4">
                        <h4>Reviews</h4>
                        <div style="display: flex; gap: 2px;">
                           <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
                           <asp:TextBox ID="txtComment" autocomplete="off" CssClass="form-control me-1" placeholder="Add a comment..." runat="server"></asp:TextBox>
                            <asp:Button ID="btnComment" CssClass="btn btn-primary" runat="server" Text="Send" OnClick="btnComment_Click" />
                        </div>
                        <div class="list-group" style="margin-top: 20px;">
                            <asp:UpdatePanel ID="UpdatePanelBooks" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="lnkPostback" runat="server" OnClick="lnkPostback_Click" Style="display: none;" />
                                    <asp:Repeater ID="rptReviews" runat="server">
                                        <ItemTemplate>
                                            <div class="list-group-item d-flex gap-2 mb-3" style="border: 1px solid #ddd; border-radius: 8px; padding: 10px; margin: 5px;">
                                                <asp:Image ID="imgAvatar" ImageUrl='<%# Eval("AvatarUrl") %>' runat="server" Width="40" Height="40" Style="border-radius: 50%; object-fit: cover;" />
                                                <div style="margin-left: 10px;">
                                                    <div style="display: flex; align-items: center; gap: 10px; flex-wrap: wrap;">
                                                        <strong>@<%# Eval("Username") %></strong>
                                                        <span class="text-muted"><%# Eval("TimeCreated", "{0:dd/MM/yyyy HH:mm}") %></span>
                                                        <asp:Button ID="btnDeleteComment" runat="server" Text="X" CssClass="btn btn-sm btn-danger" Style="font-size: 8px" CommandArgument='<%# Eval("ID") %>' OnClick="btnDeleteComment_Click" Visible='<%# Eval("CanDelete") %>' />
                                                    </div>
                                                    <div><%# Eval("Comment") %></div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <script type="text/javascript">
                                        window.onload = function () {
                                            __doPostBack('<%= lnkPostback.UniqueID %>', '');
                                        };
                                    </script>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelBooks">
                                <ProgressTemplate>
                                    <div class="text-center my-3">
                                        <div class="spinner-border text-primary" role="status"></div>
                                        <p>Loading comments...</p>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <asp:Label ID="lblNoReviews" runat="server" Text="No reviews yet." Visible="false" CssClass="text-muted" />
                    </asp:Panel>
                </div>
            </div>
        </asp:Panel>
        <asp:Label ID="lblNotFound" runat="server" CssClass="text-danger" Visible="false" Text="Book not found." />

        <div class="modal fade" id="successModal" tabindex="-1">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header bg-success text-white">
                        <h5 class="modal-title">Success</h5>
                    </div>
                    <div class="modal-body" style="display: flex; align-items: center;">
                        Item added to cart successfully!
                        <button type="button" class="btn btn-primary" style="margin-left: auto;" data-bs-dismiss="modal">OK</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>