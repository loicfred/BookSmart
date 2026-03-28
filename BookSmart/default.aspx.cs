using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using BookSmart.Objects;

namespace BookSmart {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            List<CategoryObj> categories = CategoryObj.getCategories();

            foreach (var category in categories) {
                Button btn = new Button();
                btn.Text = category.Name;
                btn.CssClass = "btn btn-primary m-2";
                btn.CommandArgument = category.ID.ToString();
                btn.Click += CategoryButton_Click;

                CategoriesPlaceholder.Controls.Add(btn);
            }
        }

        protected void CategoryButton_Click(object sender, EventArgs e) {
            Button btn = (Button) sender;
            int categoryId = int.Parse(btn.CommandArgument);
            Response.Redirect("Booklist.aspx?c=" + categoryId);
        }

        private void RenderBooks(List<BookObj> books) {
            BooksPlaceholder.Controls.Clear();
            StringBuilder html = new StringBuilder();
            html.Append("<div class='row justify-content-center'>");

            string edit = "";
            foreach (BookObj book in books) {
                if (Session["Role"] != null && Session["Role"].ToString() == "ADMIN")
                    edit = $"<a href='/admin/manage/book.aspx?id={book.ID}' class='btn btn-danger'>Edit</a>";
                html.Append($@"
                <div class='col-md-4 mb-4 d-flex justify-content-center'>
                    <div class='card' style='width: 90%; height: 630px; display: flex; flex-direction: column;'>
                        <img src='/Assets/ImageHandler.ashx?bookid={book.ID}' style='height: 440px;' class='card-img-top' alt='{book.Title}'>

                        <div class='card-body d-flex flex-column justify-content-between' style='flex: 1;'>
                            <div>
                                <h5 class='card-title'>${book.Price} | {book.Title}</h5>
                                <h6 class='card-subtitle mb-2 text-muted'>{book.getAuthor().Name} • {book.getCategory().Name}</h6>
                                <p class='card-text'>
                                    {(book.Description.Length > 96 ? book.Description.Substring(0, 96) + "..." : book.Description)}
                                </p>
                                <div style='display: flex; gap: 5px; margin-top: auto;'>
                                   <a href='book.aspx?id={book.ID}' class='btn btn-primary'>View Details</a>
                                   {edit}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            ");
            }

            html.Append("</div>");
            BooksPlaceholder.Controls.Add(new Literal { Text = html.ToString() });
        }

        protected void lnkPostback_Click(object sender, EventArgs e) {
            RenderBooks(BookObj.getMostPopularBooks());
        }
    }
}