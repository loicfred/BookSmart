using System;
using BookSmart.Objects;

namespace BookSmart.accounts {
    public partial class confirm_reset : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (Request.QueryString["code"] == null) {
                Response.Redirect("reset_password.aspx");
                return;
            }
            Email_Verification Reset = Email_Verification.getVerificationByCode(Request.QueryString["code"], "PASSWORD_RESET");
            if (Reset == null) {
                Response.Redirect("reset_password.aspx?expired=1");
                return;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e) {
            Email_Verification Reset = Email_Verification.getVerificationByCode(Request.QueryString["code"], "PASSWORD_RESET");
            if (Reset != null) {
                if (UserObj.IsValidPassword(txtNewPassword.Text)) {
                    if (txtNewPassword.Text == txtConfirmPassword.Text) {
                        Reset.getUser().Password = txtNewPassword.Text;
                        Reset.getUser().Update();
                        Email_Verification.ClearUserCodes(Reset.getUser());
                        Response.Redirect("login.aspx?reset=1");
                    }
                } else {
                    lblMessage.Text = "Your password should be 8 of length and contain numbers and at least 1 uppercase, lowercase and special character.";
                }
            }
        }
    }
}