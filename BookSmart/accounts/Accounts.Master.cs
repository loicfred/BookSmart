using System;
using BookSmart.Objects.Functions;

namespace BookSmart.accounts {
    public partial class Accounts : System.Web.UI.MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            Utilities.DisableCache();
        }
    }
}