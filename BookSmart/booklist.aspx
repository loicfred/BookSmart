<%@ Page Title="Books | BookSmart" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="booklist.aspx.cs" Inherits="BookSmart.booklist" %>

<asp:Content ContentPlaceHolderID="MASTERHEAD" runat="server">
    <style>
        .book-card {
            overflow: hidden;
        }

            .book-card .overlay {
                position: absolute;
                bottom: 0;
                background: rgba(0, 0, 0, 0.6);
                color: #fff;
                width: 100%;
                height: 100%;
                top: 0;
                left: 0;
                opacity: 0;
                transition: opacity 0.3s ease;
                display: flex;
                justify-content: center;
                align-items: center;
            }

            .book-card:hover .overlay {
                opacity: 1;
            }

        .add-to-cart-btn {
            padding: 10px 20px;
            font-size: 18px;
        }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <asp:UpdatePanel ID="UpdatePanelBooks" runat="server">
        <ContentTemplate>
            <div class="container" style="padding-bottom: 45px;">
                <div class="container mt-3">
                    <div style="display: flex;">
                        <h4>Books:</h4>
                        <asp:DropDownList ID="CategoryDropdown" style="border-radius: 5px; height: 32px; margin-left: 10px; width: 40%;" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CategoryItems_SelectedIndexChanged"></asp:DropDownList>
                        <asp:Button ID="btnRefresh" Style="margin-left: auto" runat="server" Text="Refresh Books" OnClick="btnRefresh_Click" CssClass="btn btn-primary mb-3" />
                    </div>
                    <div class="row">
                        <asp:Repeater ID="rptBooks" runat="server">
                            <ItemTemplate>
                                <div class="col-md-4 mb-4">
                                    <div class="card">
                                        <asp:Image ID="imgBook" runat="server" CssClass="card-img-top"
                                            AlternateText='<%# Eval("Title") %>'
                                            ImageUrl='<%# "/Assets/ImageHandler.ashx?bookid=" + Eval("ID") %>'
                                            Style="height: 550px;" loading="lazy" />

                                        <div class="card-body" style="height: 220px; max-height: 220px; display: flex; flex-direction: column;">
                                            <h5 class="card-title"><%# Eval("PriceAndTitle") %></h5>
                                            <h6 class="card-title" style="color: gray; margin-bottom: -1px;"><%# Eval("AuthorAndCategory") %></h6>
                                            <p class="card-text"><%# Eval("DescriptionShort") %></p>

                                            <div style="display: flex; gap: 8px; margin-top: auto; align-items: center;">
                                                <asp:HyperLink ID="hlView" runat="server" CssClass="btn btn-primary"
                                                    NavigateUrl='<%# Eval("ViewUrl") %>' Text="View Details" />

                                                <asp:Button ID="btnAddToCart" runat="server" CssClass="btn btn-primary"
                                                    CommandArgument='<%# Eval("ID") %>' Text="Add To Cart" OnClick="btnAddToCart_Click" />

                                                <asp:HyperLink ID="hlEdit" runat="server" CssClass="btn btn-danger"
                                                    NavigateUrl='<%# Eval("EditUrl") %>' Text="Edit" Visible='<%# Eval("CanEdit") %>' />

                                                <span style="font-size: 16px; font-weight: bold; margin-left: auto;">Stock: <%# Eval("Quantity") %></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                window.onload = function () {
                    __doPostBack('<%= btnRefresh.UniqueID %>', '');
                };
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelBooks">
        <ProgressTemplate>
            <div class="text-center my-3">
                <div class="spinner-border text-primary" role="status"></div>
                <p>Loading books...</p>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

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
</asp:Content>
