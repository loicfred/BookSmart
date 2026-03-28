<%@ Page Title="Home | BookSmart" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="BookSmart.Default" %>

<asp:Content ContentPlaceHolderID="MASTERHEAD" runat="server">
    <style>
        body {
            background-color: #111111;
        }

        @media screen and (max-width: 480px) {
            .card {
                height: auto;
            }
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="background">
        <div style="background-color: #00000077; padding: 20px;">
            <div class="text-center">
                <h1 style="color: white;" class="display-4">Welcome to the store</h1>
                <p style="color: white;" class="lead">Welcome to Booksmart. In this shop you will find the best books at the best prices!</p>
            </div>

            <div class="container text-center mt-4">
                <asp:PlaceHolder ID="CategoriesPlaceholder" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </div>
    <div style="background-color: #CCCCCC; height: 2px;"></div>
    <div style=" padding: 20px;" runat="server">
        <h2 style="color: white; justify-self: center; text-align: center; padding-bottom: 20px;">Most Popular Books</h2>
        <asp:UpdatePanel ID="UpdatePanelBooks" runat="server">
            <ContentTemplate>
                <div class="container" style="padding-bottom: 45px;">
                    <div class="container mt-4">
                        <asp:LinkButton ID="lnkPostback" runat="server" OnClick="lnkPostback_Click" Style="display: none;" />
                        <asp:PlaceHolder ID="BooksPlaceholder" runat="server"></asp:PlaceHolder>
                        <script type="text/javascript">
                            window.onload = function () {
                                __doPostBack('<%= lnkPostback.UniqueID %>', '');
                            };
                        </script>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelBooks">
            <ProgressTemplate>
                <div class="text-center my-3" style="background-color: #111111;">
                    <div class="spinner-border text-primary" role="status"></div>
                    <p style="color: white;">Loading books...</p>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
