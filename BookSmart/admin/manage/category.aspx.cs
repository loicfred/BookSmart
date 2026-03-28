using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using BookSmart.Objects;
using BookSmart.Objects.Functions;

namespace BookSmart.admin.manage {
    public partial class category : System.Web.UI.Page {
        private CategoryObj currentItem = new CategoryObj();
        protected void Page_Load(object sender, EventArgs e) {
            if (Request.QueryString["id"] != null) {
                currentItem = CategoryObj.getCategoryById(int.Parse(Request.QueryString["id"]));
                if (!IsPostBack)
                    LoadDetails();
            }
            if (!IsPostBack) {
                OtherItems.Items.Add(new ListItem("- Select -", "-"));
                foreach (CategoryObj C in CategoryObj.getCategories())
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
            Response.Redirect("/admin/manage/category.aspx");
        }
        protected void btnDelete_Click(object sender, EventArgs e) {
            try {
                if (currentItem != null)
                    currentItem.Delete();
                Response.Redirect("/admin/manage/category.aspx");
            } catch (Exception) {
                lblError.Text = "An error occured while deleting the item.";
            }
        }

        protected void OtherItems_SelectedIndexChanged(object sender, EventArgs e) {
            Response.Redirect("/admin/manage/category.aspx?id=" + OtherItems.SelectedValue);
        }
    }
}