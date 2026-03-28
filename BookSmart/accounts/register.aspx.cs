using System;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Security;
using BookSmart.Objects;
using BookSmart.Objects.Functions;

namespace BookSmart.accounts {
    public partial class register : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btnRegister_Click(object sender, EventArgs e) {
            string username = Server.HtmlEncode(Request.Unvalidated[txtUsername.UniqueID]).Trim();
            string password = Server.HtmlEncode(Request.Unvalidated[txtPassword.UniqueID]).Trim();
            string confirmPassword = Server.HtmlEncode(Request.Unvalidated[txtConfirmPassword.UniqueID]).Trim();
            string email = Server.HtmlEncode(Request.Unvalidated[txtEmail.UniqueID]).Trim();
            string Fname = Server.HtmlEncode(Request.Unvalidated[txtFname.UniqueID]).Trim();
            string Lname = Server.HtmlEncode(Request.Unvalidated[txtLname.UniqueID]).Trim();

            if (UserObj.getUserByName(username) != null)
                lblMessage.Text = "This username is already used!";
            else if (UserObj.getUserByEmail(email) != null)
                lblMessage.Text = "This email is already used!";
            else if (!MailManager.IsValidEmail(email))
                lblMessage.Text = "This email is invalid.";
            else if (!UserObj.IsValidPassword(password))
                lblMessage.Text = "Your password should be 8 of length and contain numbers and at least 1 uppercase, lowercase and special character.";
            else if (password != confirmPassword)
                lblMessage.Text = "Passwords do not match.";
            else if (Fname.Length == 0)
                lblMessage.Text = "First name may not be empty.";
            else if (Lname.Length == 0)
                lblMessage.Text = "Last name may not be empty.";
            else
                try {
                    SendMail();
                    ViewState["RegisterUsername"] = username;
                    ViewState["RegisterPassword"] = password;
                    ViewState["RegisterConfirmPassword"] = confirmPassword;
                    ViewState["RegisterEmail"] = email;
                    ViewState["RegisterFname"] = Fname;
                    ViewState["RegisterLname"] = Lname;
                    lblMessage.Text = "Verification email has been sent! It will expire in 5 minutes.";
                } catch (Exception ex) {
                    lblMessage.Text = "Error sending email: " + ex.Message;
                }
        }



        protected void btnResend_Click(object sender, EventArgs e) {
            try {
                SendMail();
                lblMessage.Text = "Verification email has been resent! It will expire in 5 minutes.";
            } catch (Exception ex) {
                lblMessage.Text = "Error sending email: " + ex.Message;
            }
        }
        protected void btnConfirm_Click(object sender, EventArgs e) {
            Email_Verification EV = Email_Verification.getVerificationByID(Session["TemporaryID"].ToString(), "REGISTER");
            if (DateTime.UtcNow > EV.Expiry) {
                lblMessage.Text = "Code expired. Please resend again.";
                return;
            }
            if (C1.Text + C2.Text + C3.Text + C4.Text + C5.Text + C6.Text != EV.Code) {
                lblMessage.Text = "Wrong code. Try again.";
                return;
            }
            UserObj U = new UserObj();
            U.Username = ViewState["RegisterUsername"].ToString();
            U.Password = ViewState["RegisterPassword"].ToString();
            U.Email = ViewState["RegisterEmail"].ToString();
            U.FirstName = ViewState["RegisterFname"].ToString();
            U.LastName = ViewState["RegisterLname"].ToString();
            U.RegistrationDate = DateTime.Now;
            U.Role = "USER";
            U.Insert();
            EV.Delete();

            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand("SELECT * FROM Users WHERE Username = @Username AND Email = @Email", C);
                CMD.Parameters.AddWithValue("@Username", U.Username);
                CMD.Parameters.AddWithValue("@Email", U.Email);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    U = new UserObj(row);
                }
            }
            if (U != null) {
                FormsAuthentication.SetAuthCookie(U.Role, false);
                Session.Clear();
                Session["LoggedUser"] = U;
                Session["Role"] = U.Role;
                Response.Redirect("/default.aspx");
            } else {
                details_input.Visible = true;
                code_verification.Visible = false;
                lblMessage.Text = "Registration failed.";
            }
        }



        private void SendMail() {
            Email_Verification EV = new Email_Verification();
            EV.Action = "REGISTER";
            EV.UserID = new Random().Next(-99999999, 99999999); // a temporary id
            EV.Code = new Random().Next(100000, 1000000) + "";
            EV.Expiry = DateTime.UtcNow.AddMinutes(5);
            EV.Insert();

            details_input.Visible = false;
            code_verification.Visible = true;
            Session["TemporaryID"] = EV.UserID;

            string username = Server.HtmlEncode(Request.Unvalidated[txtUsername.UniqueID]).Trim();
            string email = Server.HtmlEncode(Request.Unvalidated[txtEmail.UniqueID]).Trim();
            string Fname = Server.HtmlEncode(Request.Unvalidated[txtFname.UniqueID]).Trim();
            string Lname = Server.HtmlEncode(Request.Unvalidated[txtLname.UniqueID]).Trim();

            MailMessage message = MailManager.WriteEmail(email, "Account Registration",
                $"Dear " + Fname + " " + Lname + ",\n\nIf you are currently registering using this email for the account @" + username + ".\n" +
                $"Here is your verification code:\n\n{EV.Code}\n\n" +
                "If you didn't register using this email address, you can ignore this email.");

            MailManager.getEmailClient().Send(message);
        }
    }
}