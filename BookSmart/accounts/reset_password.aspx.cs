using System;
using System.Net.Mail;
using BookSmart.Objects;
using BookSmart.Objects.Functions;

namespace BookSmart.accounts {
    public partial class reset_password : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            string flag = Request.QueryString["expired"];
            if (flag != null)
                lblMessage.Text = "Code expired. Try again.";
        }

        protected void btnSendMail(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(txtEmail.Text)) {
                SendRecoveryEmail(txtEmail.Text);
            } else {
                lblMessage.Text = "Please provide your email address.";
            }
        }
        protected void btnSendSMS(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(txtPhone.Text)) {
                // Handle phone recovery
                //SendRecoverySMS(txtPhone.Text);
                lblMessage.Text = "Feature not available.";
            } else {
                lblMessage.Text = "Please provide your phone number.";
            }
        }





        private void SendRecoveryEmail(string userEmail) {
            UserObj Yourself = UserObj.getUserByEmail(userEmail);
            if (Yourself != null) {
                try {
                    string code = Yourself.GenerateVerificationCode("PASSWORD_RESET");
                    string resetLink = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + $"accounts/confirm_reset.aspx?code={code}";

                    MailMessage message = MailManager.WriteEmail(userEmail, "Password Reset Request",
                        $"Dear user,\n\nWe received a request to reset your password.\n" +
                        $"Please click the following link to reset it:\n\n{resetLink}\n\n" +
                        "If you did not request this, you can ignore this email.");

                    SmtpClient smtp = MailManager.getEmailClient();

                    smtp.Send(message);
                    lblMessage.Text = "Recovery email has been sent! It will expire in 3 minutes.";
                } catch (Exception ex) {
                    lblMessage.Text = "Error sending email: " + ex.Message;
                }
            } else {
                lblMessage.Text = "This email address isn't part of our registered users.";
            }
        }
    }
}