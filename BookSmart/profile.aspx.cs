using BookSmart.admin.manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BookSmart.Objects;
using BookSmart.Objects.Functions;

namespace BookSmart {
    public partial class profile : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["LoggedUser"] == null)
                Response.Redirect("/accounts/login.aspx");
        }
        protected void Page_LoadComplete(object sender, EventArgs e) {
            UserObj user = Session["LoggedUser"] as UserObj;
            LoadUserData(user);
            LoadOrders(user);
            LoadCart(user);
            LoadReviews(user);
        }

        private void LoadUserData(UserObj user) {
            lblImage.ImageUrl = user.Avatar != null ? $"/Assets/ImageHandler.ashx?userid={user.ID}" : "/Assets/default-pfp.png";
            ;
            lblFullName.Text = user.FirstName + " " + user.LastName;
            lblEmail.Text = user.Email;
            lblPhone.Text = user.Phone;
            lblDateJoined.Text = user.RegistrationDate.ToString("dd/MM/yyyy");
            lblOrderCount.Text = user.getAllOrders().Count + "";

            txtUsername.Text = user.Username;
            txtFname.Text = user.FirstName;
            txtLname.Text = user.LastName;
            txtEmail.Text = user.Email;
            txtPhone.Text = user.Phone;
            receivePromotion.Checked = user.hasPromotionEnabled;

            if (user.hasVerifiedEmail) {
                txtEmailVerified.Text = "Verified";
                verifyEmail.Visible = false;
            }
            if (user.hasVerifiedPhone) {
                txtPhoneVerified.Text = "Verified";
                verifyPhone.Visible = false;
            }
        }

        private void LoadOrders(UserObj user) {
            List<OrderObj> orders = user.getAllOrders();
            phOrdersTable.Controls.Clear();
            if (orders.Count > 0) {
                System.Web.UI.HtmlControls.HtmlTable table = new System.Web.UI.HtmlControls.HtmlTable();
                table.Attributes["class"] = "table table-striped";

                HtmlTableRow header = new HtmlTableRow();

                header.Cells.Add(new HtmlTableCell { InnerText = "Order ID" });
                header.Cells.Add(new HtmlTableCell { InnerText = "Date" });
                header.Cells.Add(new HtmlTableCell { InnerText = "Status" });
                header.Cells.Add(new HtmlTableCell { InnerText = "Items" });

                table.Rows.Add(header);

                foreach (OrderObj order in orders) {
                    HtmlTableRow row = new HtmlTableRow();

                    row.Cells.Add(new HtmlTableCell { InnerHtml = order.ID.ToString() });
                    row.Cells.Add(new HtmlTableCell { InnerHtml = order.OrderDate.ToString("yyyy-MM-dd") });
                    row.Cells.Add(new HtmlTableCell { InnerHtml = order.Status });
                    row.Cells.Add(new HtmlTableCell { InnerHtml = order.getItemsAsString() });

                    table.Rows.Add(row);
                }
                phOrdersTable.Controls.Add(table);
            } else if (orders.Count == 0)
                phOrdersTable.Controls.Add(new Label { Text = "You have no orders.", CssClass = "text-muted" });
        }

        private void LoadCart(UserObj user) {
            List<CartItemObj> items = user.GetCartItems();
            phCartTable.Controls.Clear();
            if (items.Count > 0) {
                System.Web.UI.HtmlControls.HtmlTable table = new System.Web.UI.HtmlControls.HtmlTable();
                table.Attributes["class"] = "table table-striped";

                HtmlTableRow header = new HtmlTableRow();

                header.Cells.Add(new HtmlTableCell { InnerText = "Item" });
                header.Cells.Add(new HtmlTableCell { InnerText = "Quantity" });
                header.Cells.Add(new HtmlTableCell { InnerText = "Total Price" });

                table.Rows.Add(header);
                double total = 0;
                foreach (CartItemObj item in items) {
                    HtmlTableRow row = new HtmlTableRow();

                    row.Cells.Add(new HtmlTableCell { InnerHtml = item.getBook().Title });
                    row.Cells.Add(new HtmlTableCell { InnerHtml = item.Amount.ToString() });
                    row.Cells.Add(new HtmlTableCell { InnerHtml = "$" + (item.getBook().Price * item.Amount) });
                    total += (item.getBook().Price * item.Amount);
                    table.Rows.Add(row);
                }
                HtmlTableRow totalR = new HtmlTableRow();
                totalR.Cells.Add(new HtmlTableCell { InnerHtml = "<b>Total:</b>" });
                totalR.Cells.Add(new HtmlTableCell { InnerHtml = "" });
                totalR.Cells.Add(new HtmlTableCell { InnerHtml = "$" + total });

                table.Rows.Add(totalR);
                phCartTable.Controls.Add(table);
            } else if (items.Count == 0)
                phCartTable.Controls.Add(new Label { Text = "Your cart is empty.", CssClass = "text-muted" });
        }

        private void LoadReviews(UserObj user) {
            List<ReviewObj> reviews = user.getReviewsOfUser();
            if (reviews.Count == 0) {
                lblNoReviews.Visible = true;
            } else {
                lblReviews.Text = $"Reviews: ({reviews.Count})";
                rptReviews.DataSource = reviews.Select(r => new
                {
                    r.ID,
                    r.TimeCreated,
                    r.getUser().Username,
                    AvatarUrl = r.getUser().Avatar != null ? $"/Assets/ImageHandler.ashx?userid={r.UserID}" : "/Assets/default-pfp.png",
                    BookTitle = r.getBook().Title,
                    BookLink = $"/book.aspx?id={r.getBook().ID}",
                    CanDelete = ((UserObj) Session["LoggedUser"])?.ID == r.UserID,
                    Comment = r.Comment.Replace("\n", "<br />"),
                }).ToList();
                rptReviews.DataBind();
            }
        }

        protected void btnSaveProfile_Click(object sender, EventArgs e) {
            if (Session["LoggedUser"] == null) {
                Response.Redirect("/accounts/login.aspx");
            } else {
                UserObj user = Session["LoggedUser"] as UserObj;
                string Username = Server.HtmlEncode(Request.Unvalidated[txtUsername.UniqueID]).Trim();
                string Fname = Server.HtmlEncode(Request.Unvalidated[txtFname.UniqueID]).Trim();
                string Lname = Server.HtmlEncode(Request.Unvalidated[txtLname.UniqueID]).Trim();
                string newPassword = Server.HtmlEncode(Request.Unvalidated[txtNewPassword.UniqueID]).Trim();
                string ConfirmPassword = Server.HtmlEncode(Request.Unvalidated[txtConfirmPassword.UniqueID]).Trim();
                string Email = Server.HtmlEncode(Request.Unvalidated[txtEmail.UniqueID]).Trim();
                string Phone = Server.HtmlEncode(Request.Unvalidated[txtPhone.UniqueID]).Trim();
                if (newPassword.Length > 0 && ConfirmPassword.Length > 0) {
                    if (!UserObj.IsValidPassword(newPassword)) {
                        lblMessage.Text = "Your password should be 8 of length and contain numbers and at least 1 uppercase, lowercase and special character.";
                        Response.Redirect("/profile.aspx?tab=3");
                    }
                    if (newPassword != ConfirmPassword) {
                        lblMessage.Text = "Passwords do not match.";
                        Response.Redirect("/profile.aspx?tab=3");
                    }
                }

                UserObj existingUser = UserObj.getUserByName(Username);
                if (existingUser != null && existingUser.ID != user.ID) {
                    lblMessage.Text = "This username is already used!";
                    Response.Redirect("/profile.aspx?tab=3");
                }

                if (!MailManager.IsValidEmail(Email)) {
                    lblMessage.Text = "This email is invalid.";
                    Response.Redirect("/profile.aspx?tab=3");
                }


                existingUser = UserObj.getUserByEmail(Email);
                if (existingUser != null && existingUser.ID != user.ID) {
                    lblMessage.Text = "This email is already used!";
                    Response.Redirect("/profile.aspx?tab=3");
                }

                if (Phone.Length > 0 && !IsValidPhoneNumber(Phone)) {
                    lblMessage.Text = "This phone number isn't valid!";
                    Response.Redirect("/profile.aspx?tab=3");
                }

                user.Email = Email;
                user.Phone = Phone;
                user.Username = Username;
                user.FirstName = Fname;
                user.LastName = Lname;
                user.Password = newPassword.Length > 0 && ConfirmPassword.Length > 0 ? newPassword : user.Password;
                user.hasVerifiedEmail = user.hasVerifiedEmail && Email == user.Email;
                user.hasVerifiedPhone = user.hasVerifiedPhone && Phone == user.Phone;
                user.hasVerifiedPhone = receivePromotion.Checked;

                byte[] imageBytes = fileUploadImage.HasFile ? fileUploadImage.FileBytes : null;
                if (fileUploadImage.HasFile)
                    user.Avatar = imageBytes;

                user.Update();
                Response.Redirect("/profile.aspx?tab=3");
            }
        }

        protected void btnVerifyEmail(object sender, EventArgs e) {
            try {
                UserObj user = Session["LoggedUser"] as UserObj;
                string code = user.GenerateVerificationCode("VERIFY_EMAIL");
                string resetLink = $"https://localhost:44374/accounts/verify_email.aspx?code={code}";

                MailMessage message = MailManager.WriteEmail(user.Email, "Email Verification",
                    $"Dear user,\n\nIf you are currently verifying this email for the account @" + user.Username + ".\n" +
                    $"Click the following link to reset it:\n\n{resetLink}\n\n" +
                    "If you did not request this, you can ignore this email.");

                SmtpClient smtp = MailManager.getEmailClient();

                smtp.Send(message);
                lblMessage.Text = "Verification email has been sent! It will expire in 3 minutes.";
            } catch (Exception ex) {
                lblMessage.Text = "Error sending email: " + ex.Message;
            }
        }

        protected void btnVerifyPhone(object sender, EventArgs e) {

        }

        protected void btnDownloadPDF_Click(object sender, EventArgs e) {
            UserObj user = Session["LoggedUser"] as UserObj;
            PDFGenerator.GenerateOrderReport(user);
        }

        public static bool IsValidPhoneNumber(string number) {
            string pattern = @"^\+\d{3}(?:\s\d{4}\s\d{4}|\s\d{3}\s\d{3}\s\d{4})$";
            return Regex.IsMatch(number, pattern);
        }

        protected void BtnDelete_Click(object sender, EventArgs e) {
            Button btn = (Button) sender;
            ReviewObj R = ReviewObj.getReviewById(int.Parse(btn.CommandArgument));
            if (R != null) R.Delete();
        }
        protected void btnClearCart_Click(object sender, EventArgs e) {
            UserObj user = Session["LoggedUser"] as UserObj;
            user.ClearCart();
        }
    }
}