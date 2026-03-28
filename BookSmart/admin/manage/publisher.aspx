<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="publisher.aspx.cs" Inherits="BookSmart.admin.manage.publisher" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <div class="container mt-3">
    <h2 class="mb-2 text-left">Update or insert a new publisher.</h2>
    <asp:Label ID="lblError" runat="server" CssClass="text-danger" />

    <div class="row">
        <div class="col-md-8">
            <div class="row g-3">
                <div class="col-md-6">
                    <label class="form-label">ID</label>
                    <asp:TextBox ID="Input_ID" runat="server" Text="(Auto)" Enabled="false" CssClass="form-control" />
                </div>

                <div class="col-md-6">
                    <label class="form-label">Name</label>
                    <asp:TextBox ID="Input_Name" runat="server" MaxLength="256" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label class="form-label">Address</label>
                    <asp:TextBox ID="Input_Address" runat="server" MaxLength="256" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label class="form-label">Website</label>
                    <asp:TextBox ID="Input_Website" runat="server" MaxLength="256" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="Input_Email" runat="server" TextMode="Email" MaxLength="256" CssClass="form-control" />
                </div>

                <div class="col-md-12">
                    <label class="form-label">Phone</label>
                    <asp:TextBox ID="Input_Phone" runat="server" TextMode="Phone" CssClass="form-control" />
                </div>
                
                <div style="display: flex; flex-wrap: wrap;">
                    <div class="buttonmargin">
                        <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn btn-primary" OnClick="btnNew_Click" />
                    </div>
                    <div style="padding: 5px;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                    </div>
                    <div style="padding: 5px;">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="btnDelete_Click" />
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-4">
            <h5>List of publishers</h5>
            <asp:DropDownList ID="OtherItems" CssClass="col-md-12" runat="server" AutoPostBack="True" OnSelectedIndexChanged="OtherItems_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>
</div>

</asp:Content>