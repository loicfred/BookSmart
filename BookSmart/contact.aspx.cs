using System;
using System.Net.Mail;
using BookSmart.Objects;
using BookSmart.Objects.Functions;

namespace BookSmart {
    public partial class contact : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack && Session["LoggedUser"] != null) {
                UserObj U = Session["LoggedUser"] as UserObj;
                txtContactName.Text = U.FirstName + " " + U.LastName + " (@" + U.Username + ")";
                txtContactEmail.Text = U.Email;
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e) {
            string message = Server.HtmlEncode(Request.Unvalidated[txtContactMessage.UniqueID]).Trim();
            string name = Server.HtmlEncode(Request.Unvalidated[txtContactName.UniqueID]).Trim();
            string email = Server.HtmlEncode(Request.Unvalidated[txtContactEmail.UniqueID]).Trim();
            if (Session["LoggedUser"] != null) {
                UserObj U = Session["LoggedUser"] as UserObj;
                name = U.FirstName + " " + U.LastName + " (@" + U.Username + ")";
                email = U.Email;
            }
            if (name.Length == 0) {
                lblMessage.Text = "Name shouldn't be empty.";
            } else if (message.Length < 128) {
                lblMessage.Text = "Your message should contain at least 128 characters.";
            } else if (message.Length > 1024) {
                lblMessage.Text = "Your message should contain less than 1024 characters.";
            } else if (!MailManager.IsValidEmail(email)) {
                lblMessage.Text = "Your email address is invalid.";
            } else {
                MailMessage mail = MailManager.WriteEmail(MailManager.SystemEmail, "Support Request", $"From " + name + " - " + email + "\n\n" + message);
                MailManager.getEmailClient().Send(mail);
                lblMessage.Text = "Message sent!";
            }
        }
    }
}