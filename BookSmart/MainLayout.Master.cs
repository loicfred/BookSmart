using System;
using System.Web.Security;
using BookSmart.Objects;
using BookSmart.Objects.Functions;

namespace BookSmart {
    public partial class MainLayout : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            Utilities.DisableCache();
            if (Session["LoggedUser"] != null) {
                phRegister.Visible = false;
                phProfile.Visible = true;
                UserObj U = (UserObj) Session["LoggedUser"];
                btnLogout.Visible = true;
                pfpavatar.ImageUrl = U.Avatar != null ? $"/Assets/ImageHandler.ashx?userid={U.ID}" : "/Assets/default-pfp.png";
            } else {
                phRegister.Visible = true;
                phProfile.Visible = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e) {

            string query = Server.HtmlEncode(Request.Unvalidated[txtSearch.UniqueID]);

            if (!string.IsNullOrEmpty(query))
                Server.Transfer("/booklist.aspx?s=" + Server.UrlEncode(query));
                Response.Redirect("/booklist.aspx?s=" + Server.UrlEncode(query));
        }

        protected void btnLogout_Click(object sender, EventArgs e) {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("/default.aspx");
        }
    }
}