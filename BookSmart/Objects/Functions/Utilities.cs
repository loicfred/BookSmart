using System;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookSmart.Objects.Functions {
    public class Utilities {

        public static void DisableCache() {
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
        }

        public static void Adm_LoadValues(Object obj, Control Page) {
            if (obj != null && Page != null)
                foreach (PropertyInfo P in obj.GetType().GetProperties()) {
                    Object ctrl = Page.FindControl("Input_" + P.Name);
                    if (ctrl != null && P.GetValue(obj) != null) {
                        if (ctrl.GetType() == typeof(TextBox))
                            ((TextBox) ctrl).Text = P.GetValue(obj).ToString();
                        else if (ctrl.GetType() == typeof(CheckBox))
                            ((CheckBox) ctrl).Checked = (bool) P.GetValue(obj);
                        else if (ctrl.GetType() == typeof(ListControl))
                            ((ListControl) ctrl).SelectedValue = P.GetValue(obj).ToString();
                        else if (ctrl.GetType() == typeof(DropDownList))
                            ((DropDownList) ctrl).SelectedValue = P.GetValue(obj).ToString();
                        else if (ctrl.GetType() == typeof(Calendar))
                            ((Calendar) ctrl).VisibleDate = ((Calendar) ctrl).SelectedDate = (DateTime) P.GetValue(obj);
                    }
                }
        }
        public static void Adm_SaveValues(Object obj, Control Page) {
            if (obj != null && Page != null)
                foreach (PropertyInfo P in obj.GetType().GetProperties()) {
                    if (P.Name == "ID")
                        continue;
                    Object ctrl = Page.FindControl("Input_" + P.Name);
                    if (ctrl != null) {
                        if (ctrl.GetType() == typeof(TextBox)) {
                            if (P.PropertyType == typeof(System.Int32))
                                P.SetValue(obj, int.Parse(((TextBox) ctrl).Text.Trim()));
                            else if (P.PropertyType == typeof(System.Double))
                                P.SetValue(obj, double.Parse(((TextBox) ctrl).Text.Trim()));
                            else  
                                P.SetValue(obj, ((TextBox) ctrl).Text.Trim());
                        } else if (ctrl.GetType() == typeof(CheckBox))
                            P.SetValue(obj, ((CheckBox) ctrl).Checked);
                        else if (ctrl.GetType() == typeof(ListControl))
                            P.SetValue(obj, ((ListControl) ctrl).SelectedValue);
                        else if (ctrl.GetType() == typeof(DropDownList)) {
                            if (P.PropertyType == typeof(System.Int32))
                                P.SetValue(obj, int.Parse(((DropDownList) ctrl).SelectedValue));
                            else if (P.PropertyType == typeof(System.Double))
                                P.SetValue(obj, double.Parse(((DropDownList) ctrl).SelectedValue));
                            else
                                P.SetValue(obj, ((DropDownList) ctrl).SelectedValue);
                        } else if (ctrl.GetType() == typeof(Calendar))
                            P.SetValue(obj, ((Calendar) ctrl).SelectedDate);
                        else if (ctrl.GetType() == typeof(FileUpload) && ((FileUpload) ctrl).HasFile)
                            P.SetValue(obj, ((FileUpload) ctrl).FileBytes);
                    }
                }
        }
    }
}