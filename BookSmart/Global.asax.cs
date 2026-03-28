using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using BookSmart.Objects;

namespace BookSmart {
    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {
        }

        protected void Session_Start(object sender, EventArgs e) {
            FormsAuthentication.SignOut();
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {
            HttpApplication app = (HttpApplication) sender;
            if (app.Request.IsAuthenticated && app.User.Identity is FormsIdentity) {
                FormsIdentity identity = (FormsIdentity) app.User.Identity;
                UserObj U = UserObj.getUserByName(identity.Name);
                if (U != null) {
                    string role = U.Role;
                    if (role != null)
                        app.Context.User = new GenericPrincipal(identity, new string[] { role });
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e) {
            Response.Redirect("/error.aspx");
        }

        protected void Session_End(object sender, EventArgs e) {

        }

        protected void Application_End(object sender, EventArgs e) {

        }
    }
}