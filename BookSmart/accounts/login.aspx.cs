using System;
using System.Web.Security;
using BookSmart.Objects;

namespace BookSmart.accounts {
    public partial class login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            string flag = Request.QueryString["reset"];
            if (flag != null)
                lblMessage.Text = "Successfully changed your password!";
        }

        protected void btnLogin_Click(object sender, EventArgs e) {
            string username = Server.HtmlEncode(Request.Unvalidated[txtUsername.UniqueID]).Trim();
            string password = Server.HtmlEncode(Request.Unvalidated[txtPassword.UniqueID]).Trim();
            UserObj Yourself = UserObj.getUserByLogin(username, password);

            if (Yourself == null)
                lblMessage.Text = "Invalid login credentials.";
            else if (Yourself.isDisabled)
                lblMessage.Text = "Your account has been blocked.";
            else {
                Session["LoggedUser"] = Yourself;
                Session["Role"] = Yourself.Role;
                Response.Redirect("/default.aspx");
                FormsAuthentication.SetAuthCookie(Yourself.Username, false);
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e) {
            Response.Redirect("register.aspx");
        }




    }
}