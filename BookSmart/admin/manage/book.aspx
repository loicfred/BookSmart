<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="book.aspx.cs" Inherits="BookSmart.admin.manage.book" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <div class="container mt-3">
    <h2 class="mb-2 text-left">Update or insert a new book.</h2>
    <asp:Label ID="lblError" runat="server" CssClass="text-danger" />

    <div class="row">
        <div class="col-md-8">
            <div class="row g-3">
                <div class="col-md-2">
                    <label class="form-label">ID</label>
                    <asp:TextBox ID="Input_ID" runat="server" Text="(Auto)" Enabled="false" CssClass="form-control" />
                </div>
                <div class="col-md-6">
                    <label class="form-label">Title</label>
                    <asp:TextBox ID="Input_Title" runat="server" MaxLength="256" CssClass="form-control" />
                </div>

                <div class="col-md-4">
                    <label class="form-label">Price</label>
                    <asp:TextBox ID="Input_Price" runat="server" CssClass="form-control" MaxLength="6" />
                </div>

                <div class="col-md-8">
                    <label class="form-label">Description</label>
                    <asp:TextBox ID="Input_Description" runat="server" TextMode="MultiLine" Rows="10" MaxLength="1000" CssClass="form-control" />
                </div>

                <div class="col-md-4">
                    <label class="form-label">Publish Date</label>
                    <asp:Calendar ID="Input_PublishDate" runat="server" CssClass="form-control"></asp:Calendar>
                </div>

                <div class="col-md-4">
                    <label class="form-label">Language</label>
                    <asp:TextBox ID="Input_Language" runat="server" MaxLength="128" CssClass="form-control" />
                </div>

                <div class="col-md-4">
                    <label class="form-label">Stock Quantity</label>
                    <asp:TextBox ID="Input_Quantity" runat="server" CssClass="form-control" TextMode="Number" Min="0" />
                </div>

                <div class="col-md-4">
                    <label class="form-label">Pages</label>
                    <asp:TextBox ID="Input_Pages" runat="server" CssClass="form-control" TextMode="Number" Min="0" />
                </div>

                

                <div class="col-md-4">
                    <label class="form-label">Category</label>
                    <asp:DropDownList ID="Input_CategoryID" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <div class="col-md-4">
                    <label class="form-label">Author</label>
                    <asp:DropDownList ID="Input_AuthorID" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <div class="col-md-4">
                    <label class="form-label">Publisher</label>
                    <asp:DropDownList ID="Input_PublisherID" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>

                <div class="col-md-8">
                    <label class="form-label">Book Image</label>
                    <asp:FileUpload ID="Input_Image" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label class="form-label">Disable Book</label>
                    <asp:CheckBox ID="Input_isDisabled" runat="server" CssClass="form-control"></asp:CheckBox>
                </div>
                
                <div style="display: flex; flex-wrap: wrap;">
                    <div style="padding: 5px;">
                        <asp:Button ID="btnEditCategory" runat="server" Text="View Category" CssClass="btn btn-primary" OnClick="btnEditCategory_Click" />
                    </div>                    
                    <div style="padding: 5px;">
                        <asp:Button ID="btnEditAuthor" runat="server" Text="View Author" CssClass="btn btn-primary" OnClick="btnEditAuthor_Click" />
                    </div>                  
                    <div style="padding: 5px;">
                        <asp:Button ID="btnEditPublisher" runat="server" Text="View Publisher" CssClass="btn btn-primary" OnClick="btnEditPublisher_Click" />
                    </div>

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
            <h5>List of books</h5>
            <asp:DropDownList ID="OtherItems" CssClass="col-md-12" runat="server" AutoPostBack="True" OnSelectedIndexChanged="OtherItems_SelectedIndexChanged"></asp:DropDownList>
        
            <h5>Preview Image</h5>
            <div class="border rounded p-2 text-center">
                <asp:Image ID="imgPreview" runat="server" CssClass="img-fluid" Style="max-height: 600px;" />
            </div>
        </div>
    </div>
</div>

</asp:Content>
