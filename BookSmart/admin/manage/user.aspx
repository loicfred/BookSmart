<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="BookSmart.admin.manage.user" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <div class="container mt-3">
    <h2 class="mb-2 text-left">Update or insert a new user.</h2>
        <asp:Label ID="lblError" runat="server" CssClass="text-danger" />

    <div class="row">
        <div class="col-md-8">
            <div class="row g-3">

                <div class="col-md-2">
                    <label class="form-label">ID</label>
                    <asp:TextBox ID="Input_ID" runat="server" Text="(Auto)" Enabled="false" CssClass="form-control" />
                </div>

                <div class="col-md-4">
                    <label class="form-label">First Name</label>
                    <asp:TextBox ID="Input_FirstName" runat="server" MaxLength="64" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Last Name</label>
                    <asp:TextBox ID="Input_LastName" runat="server" MaxLength="64" CssClass="form-control" />
                </div>

                
                <div class="col-md-4">
                    <label class="form-label">Username</label>
                    <asp:TextBox ID="Input_Username" runat="server" MaxLength="64" CssClass="form-control" />
                </div>
                
                <div class="col-md-4">
                    <label class="form-label">Password</label>
                    <asp:TextBox ID="Input_Password" runat="server" MaxLength="32" TextMode="Password" CssClass="form-control" />
                </div>

                <div class="col-md-4">
                    <label class="form-label">Role</label>
                    <asp:DropDownList ID="Input_Role" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>


                <div class="col-md-6">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="Input_Email" runat="server" TextMode="Email" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Phone</label>
                    <asp:TextBox ID="Input_Phone" runat="server" TextMode="Phone" CssClass="form-control" />
                </div>
                

                <div class="col-md-6">
                    <label class="form-label">Address</label>
                    <asp:TextBox ID="Input_Address" runat="server" MaxLength="256" CssClass="form-control" />
                </div>

                <div class="col-md-3">
                    <label class="form-label">PostalCode</label>
                    <asp:TextBox ID="Input_PostalCode" runat="server" MaxLength="20" CssClass="form-control" />
                </div>

                <div class="col-md-3">
                    <label class="form-label">Registration Date</label>
                    <asp:Calendar ID="Input_RegistrationDate" runat="server" CssClass="form-control"></asp:Calendar>
                </div>


                <div class="col-md-4">
                    <label class="form-label">has Verified Email</label>
                    <asp:CheckBox ID="Input_hasVerifiedEmail" runat="server" CssClass="form-control"></asp:CheckBox>
                </div>
                <div class="col-md-4">
                    <label class="form-label">has Verified Phone</label>
                    <asp:CheckBox ID="Input_hasVerifiedPhone" runat="server" CssClass="form-control"></asp:CheckBox>
                </div>
                <div class="col-md-4">
                    <label class="form-label">has Promotion Enabled</label>
                    <asp:CheckBox ID="Input_hasPromotionEnabled" runat="server" CssClass="form-control"></asp:CheckBox>
                </div>

                <div class="col-md-8">
                    <label class="form-label">Avatar</label>
                    <asp:FileUpload ID="Input_Avatar" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label class="form-label">Disable Account</label>
                    <asp:CheckBox ID="Input_isDisabled" runat="server" CssClass="form-control"></asp:CheckBox>
                </div>
                
                <div style="display: flex; flex-wrap: wrap;">
                    <div class="buttonmargin">
                        <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" OnClick="btnNew_Click" />
                    </div>

                    <div style="padding: 5px;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4">
            <h5>List of books</h5>
            <asp:DropDownList ID="OtherItems" CssClass="col-md-12" runat="server" AutoPostBack="True" OnSelectedIndexChanged="OtherItems_SelectedIndexChanged"></asp:DropDownList>

            <h5>Preview Avatar</h5>
            <div class="border rounded p-2 text-center">
                <asp:Image ID="imgPreview" runat="server" CssClass="img-fluid" Style="max-height: 200px; border-radius: 50%;" />
            </div>
        </div>
    </div>
</div>

</asp:Content>

