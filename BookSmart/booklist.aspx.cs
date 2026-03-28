using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BookSmart.Objects;

namespace BookSmart {
    public partial class booklist : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                CategoryDropdown.Items.Add(new ListItem("-=- Select Category -=-", "0"));
                foreach (CategoryObj C in CategoryObj.getCategories())
                    CategoryDropdown.Items.Add(new ListItem(C.Name, C.ID + ""));
            }
        }

        private void RenderBooks(List<BookObj> books) {
            rptBooks.DataSource = books.Where(b => !b.isDisabled)
              .Select(b => new {
                  b.ID,
                  b.Title,
                  PriceAndTitle = $"${b.Price} | {b.Title}",
                  AuthorAndCategory = b.getAuthor().Name + " • " + b.getCategory().Name,
                  DescriptionShort = b.Description.Length > 96 ? b.Description.Substring(0, 96) + "..." : b.Description,
                  ViewUrl = $"/book.aspx?id={b.ID}",
                  EditUrl = $"/admin/manage/book.aspx?id={b.ID}",
                  CanEdit = Session["Role"]?.ToString() == "ADMIN",
                  b.Quantity
              }).ToList();
            rptBooks.DataBind();
        }

        protected void btnRefresh_Click(object sender, EventArgs e) {
            string categoryIdStr = Request.QueryString["c"];
            string searchIdStr = Request.QueryString["s"];

            int categoryId;
            if (int.TryParse(categoryIdStr, out categoryId)) {
                Title = CategoryObj.getCategoryById(categoryId).Name + " | BookSmart";
                List<BookObj> books = BookObj.getBooksByCategory(categoryId);
                RenderBooks(books);
            } else if (searchIdStr != null) {
                Title = searchIdStr + " | BookSmart";
                List<BookObj> books = BookObj.getBooksByName(searchIdStr);
                RenderBooks(books);
            } else {
                List<BookObj> books = BookObj.getBooks();
                RenderBooks(books);
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e) {
            try {
                Button btn = (Button) sender;
                if (Session["LoggedUser"] != null) {
                    UserObj U = Session["LoggedUser"] as UserObj;
                    U.AddToCart(int.Parse(btn.CommandArgument), 1);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "var myModal = new bootstrap.Modal(document.getElementById('successModal')); myModal.show();", true);
                } else {
                    Response.Redirect("/accounts/login.aspx");
                }
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        protected void CategoryItems_SelectedIndexChanged(object sender, EventArgs e) {
            if (CategoryDropdown.SelectedValue == "0")
                Response.Redirect("/booklist.aspx");
            Response.Redirect("/booklist.aspx?c=" + CategoryDropdown.SelectedValue);
        }
    }
}