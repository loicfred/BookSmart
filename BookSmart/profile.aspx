<%@ Page Title="My Profile | BookSmart" Language="C#" ValidateRequest="false" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="BookSmart.profile" %>

<asp:Content ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <div class="container" style="padding-bottom: 45px;">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div style="display: flex; gap: 10px; align-items: center;">
            <asp:Image ID="lblImage" runat="server" Width="100px" Height="100px" Style="border-radius: 50%; padding: 5px;"/>
            <asp:Label ID="lblFullName" runat="server" CssClass="form-control-plaintext" class="mb-4" style="font-size: 24px;"></asp:Label>
        </div>

        <asp:ValidationSummary runat="server" CssClass="text-danger"/>

        <ul class="nav nav-tabs" id="profileTabs" role="tablist">
            <li class="nav-item" role="presentation">
                <button class="nav-link active" id="info-tab" data-bs-toggle="tab" data-bs-target="#info" type="button" role="tab">Profile Info</button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="cart-tab" data-bs-toggle="tab" data-bs-target="#cart" type="button" role="tab">Cart</button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="orders-tab" data-bs-toggle="tab" data-bs-target="#orders" type="button" role="tab">Orders</button>
            </li>
            <li class="nav-item" role="presentation">
                <button class="nav-link" id="edit-tab" data-bs-toggle="tab" data-bs-target="#edit" type="button" role="tab">Edit Profile</button>
            </li>
        </ul>

        <div class="tab-content p-4 shadow-sm border-bottom border-start border-end">
            <div class="tab-pane fade show active" id="info" role="tabpanel">
                <div class="mb-3 row">
                    <label class="col-sm-3 fw-bold">Email:</label>
                    <div class="col-sm-9">
                        <asp:Label ID="lblEmail" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <div class="mb-3 row">
                    <label class="col-sm-3 fw-bold">Phone:</label>
                    <div class="col-sm-9">
                        <asp:Label ID="lblPhone" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <div class="mb-3 row">
                    <label class="col-sm-3 fw-bold">Date Joined:</label>
                    <div class="col-sm-9">
                        <asp:Label ID="lblDateJoined" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>

                <div class="mb-3 row">
                    <label class="col-sm-3 fw-bold">Orders:</label>
                    <div class="col-sm-9">
                        <asp:Label ID="lblOrderCount" runat="server" CssClass="form-control-plaintext"></asp:Label>
                    </div>
                </div>
                
                <asp:Panel ID="CommentSection" runat="server" class="mt-4">
                    <div class="list-group" style="margin-top: 20px;">
                        <asp:Label ID="lblReviews" runat="server" Font-Bold="true"></asp:Label>
                        <asp:Repeater ID="rptReviews" runat="server">
                            <ItemTemplate>
                                <div class="list-group-item d-flex gap-2 mb-3" style="border: 1px solid #ddd; border-radius: 8px; padding: 10px; margin: 5px;">
                                    <asp:Image ID="imgAvatar" ImageUrl='<%# Eval("AvatarUrl") %>' runat="server" Width="40" Height="40" Style="border-radius: 50%; object-fit: cover;" />
                                    <div style="margin-left: 10px;">
                                        <div style="display: flex; align-items: center; gap: 10px; flex-wrap: wrap;">
                                            <strong>@<%# Eval("Username") %></strong>
                                            <span class="text-muted"><%# Eval("TimeCreated", "{0:dd/MM/yyyy HH:mm}") %></span>
                                            <asp:Button ID="btnDelete" runat="server" Text="X" CssClass="btn btn-sm btn-danger" Style="font-size: 8px" CommandArgument='<%# Eval("ID") %>' OnClick="BtnDelete_Click" Visible='<%# Eval("CanDelete") %>' />
                                            <a class="text-muted" style="text-decoration: none;" href='<%# Eval("BookLink") %>'>- <%# Eval("BookTitle") %></a>
                                        </div>
                                        <div><%# Eval("Comment") %></div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <asp:Label ID="lblNoReviews" runat="server" Text="No reviews yet." Visible="false" CssClass="text-muted" />
                </asp:Panel>

            </div>

            <div class="tab-pane fade" id="cart" role="tabpanel">
                <div style="display: flex; align-items: center; padding: 5px;">
                    <h5>Your Cart</h5>
                    <asp:Button Style="margin-left: auto; margin-bottom: 5px;" ID="btnClearCart" runat="server" Text="Clear Cart" OnClick="btnClearCart_Click" CssClass="btn btn-primary" />
                </div>
                <asp:PlaceHolder ID="phCartTable" runat="server"></asp:PlaceHolder>
            </div>

            <div class="tab-pane fade" id="orders" role="tabpanel">
                <div style="display: flex; align-items: center; padding: 5px;">
                    <h5>Your Orders</h5>
                    <asp:Button style="margin-left: auto; margin-bottom: 5px;" ID="btnDownloadOrderReport" runat="server" Text="Download Order Report" OnClick="btnDownloadPDF_Click" CssClass="btn btn-primary" />
                </div>
                <asp:PlaceHolder ID="phOrdersTable" runat="server"></asp:PlaceHolder>
            </div>

            <div class="tab-pane fade" id="edit" role="tabpanel">
                <h5>Edit Your Information</h5>

                <div class="mb-1">
                    <label>Username</label>
                    <div style="position: relative;">
                        <span style="position: absolute; left: 8px; top: 50%; transform: translateY(-50%); color: gray; pointer-events: none; font-family: sans-serif;">@</span>
                        <asp:TextBox ID="txtUsername" runat="server" Style="padding-left: 25px;" CssClass="form-control" />
                    </div>
                </div>

                <div style="display: flex; gap: 10px;">
                    <div class="mb-1">
                        <label>First Name</label>
                        <asp:TextBox ID="txtFname" runat="server" MaxLength="64" CssClass="form-control" />
                    </div>
                    <div class="mb-1">
                        <label>Last Name</label>
                        <asp:TextBox ID="txtLname" runat="server" MaxLength="64" CssClass="form-control" />
                    </div>
                </div>

                <div class="col-md-10">
                    <label class="form-label">Profile Picture</label>
                    <asp:FileUpload ID="fileUploadImage" runat="server" CssClass="form-control"/>
                </div>
                <br/>

                <h5>Privacy</h5>
                
                <div class="mb-1" style="display: flex; align-items: center; gap: 10px;">
                    <label>Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" MaxLength="64" CssClass="form-control" />
                    <asp:Label ID="txtEmailVerified" runat="server" CssClass="disablemobile" Text="Your email address isn't verified."></asp:Label>
                    <asp:Button ID="verifyEmail" runat="server" Text="Verify" CssClass="btn btn-primary" OnClick="btnVerifyEmail"/>
                </div>
                <div class="mb-1" style="display: flex; align-items: center; gap: 10px;">
                    <label>Phone</label>
                    <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" MaxLength="20" CssClass="form-control" />
                    <asp:Label ID="txtPhoneVerified" runat="server" CssClass="disablemobile" Text="Your phone number isn't verified."></asp:Label>
                    <asp:Button ID="verifyPhone" runat="server" Text="Verify" CssClass="btn btn-primary" OnClick="btnVerifyPhone" />
                </div>
                <div class="mb-1" style="display: flex; align-items: center; gap: 10px;">
                    <label>Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" MaxLength="256" CssClass="form-control" />
                </div>
                <div class="mb-1" style="display: flex; align-items: center; gap: 10px;">
                    <label style="width: 120px;">Postal Code</label>
                    <asp:TextBox ID="txtPostalCode" runat="server" MaxLength="20" CssClass="form-control" />
                </div>
                <div class="mb-1" style="display: flex; align-items: center; gap: 10px;">
                    <asp:Label ID="lblPromotion" runat="server" Text="Receives promotion by mail?"></asp:Label>
                    <asp:CheckBox ID="receivePromotion" runat="server" />
                </div>
                <br/>

                <h5>Security</h5>

                <div class="mb-1">
                    <label>New Password</label>
                    <asp:TextBox ID="txtNewPassword" runat="server" MaxLength="32" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="mb-1">
                    <label>Confirm Password</label>
                    <asp:TextBox ID="txtConfirmPassword" runat="server" MaxLength="32" TextMode="Password" CssClass="form-control" />
                </div>
                <br/>

                <div class="mb-3">
                    <asp:Button ID="btnSaveProfile" runat="server" Text="Save Changes" CssClass="btn btn-primary" OnClick="btnSaveProfile_Click"/>
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            const tabIndex = parseInt(new URLSearchParams(window.location.search).get("tab"));
            if (!isNaN(tabIndex)) {
                const tabs = document.querySelectorAll('#profileTabs .nav-link');
                if (tabs[tabIndex]) new bootstrap.Tab(tabs[tabIndex]).show();
            }
        });
    </script>
</asp:Content>