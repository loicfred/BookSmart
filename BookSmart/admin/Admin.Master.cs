using System;
using System.Web.Security;
using BookSmart.Objects.Functions;

namespace BookSmart.Admin {
    public partial class Admin : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            Utilities.DisableCache();
            if (Session["Role"]?.ToString() != "ADMIN")
                Response.Redirect("/default.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e) {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("/default.aspx");
        }
    }
}