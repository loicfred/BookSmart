using System;
using BookSmart.Objects;

namespace BookSmart.accounts {
    public partial class verify_email : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            Email_Verification Mail = Email_Verification.getVerificationByCode(Request.QueryString["code"], "VERIFY_EMAIL");
            if (Mail != null) {
                Mail.getUser().hasVerifiedEmail = true;
                Mail.getUser().Update();
            } else {
                notice.Text = "The email verification link has expired. Try again.";
            }
        }
    }
}