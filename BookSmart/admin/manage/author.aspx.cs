using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using BookSmart.Objects;
using BookSmart.Objects.Functions;

namespace BookSmart.admin.manage {
    public partial class author : System.Web.UI.Page {
        AuthorObj currentItem = new AuthorObj();

        protected void Page_Load(object sender, EventArgs e) {
            if (Request.QueryString["id"] != null) {
                currentItem = AuthorObj.getAuthorById(int.Parse(Request.QueryString["id"]));
                if (!IsPostBack)
                    LoadDetails();
            }
            if (!IsPostBack) {
                OtherItems.Items.Add(new ListItem("- Select -", "-"));
                foreach (AuthorObj C in AuthorObj.getAuthors())
                    OtherItems.Items.Add(new ListItem(C.Name, C.ID + ""));
            }
        }
        private void LoadDetails() {
            Utilities.Adm_LoadValues(currentItem, Master.FindControl("MASTERCONTENT"));
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
            Response.Redirect("/admin/manage/author.aspx");
        }
        protected void btnDelete_Click(object sender, EventArgs e) {
            try {
                if (currentItem != null)
                    currentItem.Delete();
                Response.Redirect("/admin/manage/author.aspx");
            } catch (Exception) {
                lblError.Text = "An error occured while deleting the item.";
            }
        }


        protected void OtherItems_SelectedIndexChanged(object sender, EventArgs e) {
            Response.Redirect("/admin/manage/author.aspx?id=" + OtherItems.SelectedValue);
        }
    }
}