using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using BookSmart.Objects;
using BookSmart.Objects.Functions;

namespace BookSmart.admin.manage {
    public partial class user : System.Web.UI.Page {
        UserObj currentItem = new UserObj();

        protected void Page_Load(object sender, EventArgs e) {
            if (Request.QueryString["id"] != null) {
                currentItem = UserObj.getUserById(int.Parse(Request.QueryString["id"]));
                if (!IsPostBack)
                    LoadDetails();
            }
            if (!IsPostBack) {
                Input_Role.Items.Add(new ListItem("USER", "USER"));
                Input_Role.Items.Add(new ListItem("MOD", "MOD"));
                Input_Role.Items.Add(new ListItem("ADMIN", "ADMIN"));
                OtherItems.Items.Add(new ListItem("- Select -", "-"));
                foreach (UserObj C in UserObj.getUsers())
                    OtherItems.Items.Add(new ListItem("@" + C.Username + " - " + C.Email + " - " + C.FirstName + " " + C.LastName, C.ID + ""));
            }
        }

        private void LoadDetails() {
            Utilities.Adm_LoadValues(currentItem, Master.FindControl("MASTERCONTENT"));
            if (currentItem.Avatar != null)
                imgPreview.ImageUrl = $"/Assets/ImageHandler.ashx?userid={currentItem.ID}";
            else
                imgPreview.ImageUrl = "/Assets/default-pfp.png";
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
            Response.Redirect("/admin/manage/user.aspx");
        }


        protected void OtherItems_SelectedIndexChanged(object sender, EventArgs e) {
            Response.Redirect("/admin/manage/user.aspx?id=" + OtherItems.SelectedValue);
        }
    }
}