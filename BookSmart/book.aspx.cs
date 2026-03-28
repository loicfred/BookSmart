using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookSmart.Objects;

namespace BookSmart {
    public partial class book : System.Web.UI.Page {
        private int BookId {
            get { return (int) (ViewState["BookId"] ?? 0); }
            set { ViewState["BookId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (int.TryParse(Request.QueryString["id"], out var id)) {
                    BookId = id;
                    LoadBook();
                } else {
                    Response.Redirect("/booklist.aspx");
                }
            }
        }
        protected void Page_LoadComplete(object sender, EventArgs e) {
            LoadReviews();
        }

        protected void btnComment_Click(object sender, EventArgs e) {
            if (Session["LoggedUser"] == null)
                Response.Redirect("/accounts/login.aspx");
            else {
                String comment = Server.HtmlEncode(Request.Unvalidated[txtComment.UniqueID]).Trim();
                if (comment.Length == 0) {
                    lblMessage.Text = "The comment must not be empty.";
                } else {
                    ReviewObj R = new ReviewObj();
                    R.Comment = comment;
                    R.BookID = BookId;
                    R.TimeCreated = DateTime.Now;
                    R.UserID = ((UserObj) Session["LoggedUser"]).ID;
                    R.Insert();
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        private void LoadBook() {
            BookObj B = BookObj.getBookById(BookId);
            Title = B.Title + " | BookSmart";

            lblTitle.Text = B.Title;
            lblAuthor.Text = B.getAuthor().Name;
            lblCategory.Text = B.getCategory().Name;
            lblDescription.Text = B.Description;
            lblPages.Text = B.Pages + "";
            lblLanguage.Text = B.Language;
            lblPrice.Text = B.Price + "";
            btnAddToCart.Enabled = B.isAvailable();
            if (B.Image != null) imgBook.ImageUrl = $"/Assets/ImageHandler.ashx?bookid={B.ID}";
            pnlBookDetails.Visible = true;
            B.AddView();
        }

        protected void lnkPostback_Click(object sender, EventArgs e) {
            LoadReviews();
        }

        private void LoadReviews() {
            List<ReviewObj> reviews = ReviewObj.getReviewsOfBook(BookId);
            if (reviews.Count == 0) {
                lblNoReviews.Visible = true;
            } else {
                rptReviews.DataSource = reviews.Select(r => new {
                    r.ID,
                    r.TimeCreated,
                    r.getUser().Username,
                    AvatarUrl = r.getUser().Avatar != null ? $"/Assets/ImageHandler.ashx?userid={r.UserID}" : "/Assets/default-pfp.png",
                    CanDelete = Session["Role"]?.ToString() == "ADMIN" || ((UserObj) Session["LoggedUser"])?.ID == r.UserID,
                    Comment = r.Comment.Replace("\n", "<br />"),
                }).ToList();
                rptReviews.DataBind();
            }
        }

        protected void btnDeleteComment_Click(object sender, EventArgs e) {
            Button btn = (Button) sender;
            ReviewObj R = ReviewObj.getReviewById(int.Parse(btn.CommandArgument));
            if (R != null) R.Delete();
        }

        protected void btnAddToCart_Click(object sender, EventArgs e) {
            if (Session["LoggedUser"] != null) {
                UserObj U = Session["LoggedUser"] as UserObj;
                U.AddToCart(BookId, 1);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "var myModal = new bootstrap.Modal(document.getElementById('successModal')); myModal.show();", true);
            } else {
                Response.Redirect("/accounts/login.aspx");
            }
        }
    }
}