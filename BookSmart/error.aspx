<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="error.aspx.cs" Inherits="BookSmart.error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MASTERHEAD" runat="server">
    <style>
        .centered {
            height: 80%;
            display: flex;
            justify-content: center;
            align-items: center;
            font-size: 2rem;
            color: #333;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MASTERCONTENT" runat="server">
    <div class="centered" id="errorMessage">Unknown error</div>
    <script>
        function getErrorMessageFromUrl() {
            const params = new URLSearchParams(window.location.search);
            const errorCode = params.get('error');
            let message = 'Unknown error';
            switch(errorCode) {
                case '404':
                    message = 'Page not found (404)';
                    break;
                case '500':
                    message = 'Internal server error (500)';
                    break;
                case '403':
                    message = 'Access forbidden (403)';
                    break;
                case '400':
                    message = 'Bad request (400)';
                    break;
                default:
                    if (errorCode) {
                        message = `Error code: ${errorCode}`;
                    }
                    break;
            }
            return message;
        }
        document.getElementById('errorMessage').textContent = getErrorMessageFromUrl();
    </script>
</asp:Content>
