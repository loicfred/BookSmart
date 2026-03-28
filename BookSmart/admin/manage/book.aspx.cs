using System;
using System.Web.UI.WebControls;
using BookSmart.Objects;
using BookSmart.Objects.Functions;

namespace BookSmart.admin.manage {
    public partial class book : System.Web.UI.Page {
        BookObj currentItem = new BookObj();

        protected void Page_Load(object sender, EventArgs e) {
            if (Request.QueryString["id"] != null) {
                currentItem = BookObj.getBookById(int.Parse(Request.QueryString["id"]));
                if (!IsPostBack)
                    LoadDetails();
            }
            if (!IsPostBack) {
                foreach (CategoryObj C in CategoryObj.getCategories())
                    Input_CategoryID.Items.Add(new ListItem(C.Name, C.ID + ""));
                foreach (AuthorObj C in AuthorObj.getAuthors())
                    Input_AuthorID.Items.Add(new ListItem(C.Name, C.ID + ""));
                foreach (PublisherObj C in PublisherObj.getPublishers())
                    Input_PublisherID.Items.Add(new ListItem(C.Name, C.ID + ""));
                OtherItems.Items.Add(new ListItem("- Select -", "-"));
                foreach (BookObj C in BookObj.getBooks())
                    OtherItems.Items.Add(new ListItem(C.Title, C.ID + ""));
            }
        }

        private void LoadDetails() {
            Utilities.Adm_LoadValues(currentItem, Master.FindControl("MASTERCONTENT"));
            if (currentItem.Image != null)
                imgPreview.ImageUrl = $"/Assets/ImageHandler.ashx?bookid={currentItem.ID}";
        }
        protected void btnSave_Click(object sender, EventArgs e) {
            try {
                Utilities.Adm_SaveValues(currentItem, Master.FindControl("MASTERCONTENT"));
                currentItem.UpdateElseInsert();
                Response.Redirect(Request.RawUrl);
            } catch (Exception) {
                lblError.Text = "An error occured while saving the item.";
            }
        }
        protected void btnNew_Click(object sender, EventArgs e) {
            Response.Redirect("/admin/manage/book.aspx");
        }
        protected void btnDelete_Click(object sender, EventArgs e) {
            try {
                if (currentItem != null)
                    currentItem.Delete();
                Response.Redirect("/admin/manage/book.aspx");
            } catch (Exception) {
                lblError.Text = "An error occured while deleting the item.";
            }
        }


        protected void btnEditCategory_Click(object sender, EventArgs e) {
            Response.Redirect("/admin/manage/category.aspx?id=" + currentItem.CategoryID);
        }

        protected void btnEditAuthor_Click(object sender, EventArgs e) {
            Response.Redirect("/admin/manage/author.aspx?id=" + currentItem.AuthorID);
        }

        protected void btnEditPublisher_Click(object sender, EventArgs e) {
            Response.Redirect("/admin/manage/publisher.aspx?id=" + currentItem.PublisherID);
        }

        protected void OtherItems_SelectedIndexChanged(object sender, EventArgs e) {
            Response.Redirect("/admin/manage/book.aspx?id=" + OtherItems.SelectedValue);
        }
    }
}