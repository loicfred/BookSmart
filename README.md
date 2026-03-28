# BookSmart
This project is a bookstore web application made for my **Web Application Development** module assignment at university.
I was introduced to **ASP.NET** for the first time and developed a sophisticated web application with dynamic pages using data from **Microsoft SQL Server** database.
I could experiment working with **Master Pages**, **Server Controls** and **Code-behind** in C#.

**Applied Skills:**
- C# ASP.NET Web Application (Server Controls, Master Page, Code-Behind, ASPX...)
- NuGet Packages Usage
- Boostrap UI Front-End Design
- Authentication (Login, Signin, SSO, Password Reset, Email Verification...)
- SQL Server Database
  
## Home
This is the first page that greets new users once entering the website.
The first 3 most popular books are fetched from the database based from which has the highest view count.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/63687cce-f002-4fa3-92f3-850e14cc295c" />


## Booklist
This is page where all books are listed and are grouped by category.
Changing the category also changes the book list dynamically.

<img width="750" alt="aaaaa" src="https://github.com/user-attachments/assets/133e43f5-2a9a-4768-b422-834ef434169e" />

## Book
This is the page which displays all data of a book and where users can also add books to cart. There are no payment feature available.
The data is loading dynamically using url parameters as book id and the comments are fetched as well ordered by date.
A user must be logged in to be able to comment.

<img width="750" height="640" alt="bbbbb" src="https://github.com/user-attachments/assets/b0bdc2a3-dc4c-49a9-9e74-7225e5e6b13b" />

## Edit Profile
A logged in user can also view and modify their profile information, they are able to view their current cart content and download their order history as a PDF file.
They can also view and delete their comments on any book page.

<img width="750" alt="aaa" src="https://github.com/user-attachments/assets/e92416ac-0c40-45fa-b937-3bdad566903a" />
<img width="750" alt="bbb" src="https://github.com/user-attachments/assets/b26b5634-57f1-4690-8aff-fa713d50ff10" />
<img width="750" alt="ccc" src="https://github.com/user-attachments/assets/10cb70d8-4492-408e-8a70-393066b4e6b8" />
<img width="750" alt="ddd" src="https://github.com/user-attachments/assets/aec49d84-f651-4036-86cc-e9cfb0c909e0" />

## Report Generator
This is the order history report which can be generated when downloading.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/9fb08d15-12a8-431c-a915-8df2f6ca26b6" />

## Contact Form
This is a where users can feel a form to submit their complaint. The complaint is then received by email.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/cc7ae214-c7dc-4156-bef5-67657d6eb692" />
<img width="500" alt="image" src="https://github.com/user-attachments/assets/21ec10f1-e4ac-48ec-88fc-f874604557cd" />

## About Page
This page provides information to users about the owners of the website.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/a756060c-e573-4720-ad36-987bb92dac9c" />

# Authentication
The web application supports full authentication features, this include log-in, sign-in & password reset.

## Register & Verification
The user can register with a new account, once they register they will receive a verification email to make sure the email belongs to them. They must enter the verification code received to successfully register.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/5379ad44-8299-41a7-84d1-70053096a7e9" />
<img width="400" alt="image" src="https://github.com/user-attachments/assets/0f1c900a-ba3c-4bfb-bac0-6a90dda011d2" />


## Log-in / Reset Password
The user can enter his email (or username) and password to authenticate. The passwords are hashed and the result of the log-in stores the current user in the session.
The user can also reset their password using either email (phone is non-functional). Once they submit, they will receive their email to enter a view password.
The reset password url of the email contains a url parameter which links the password reset to the user account. The email has a validity of 10 minutes.
It is similar to verification.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/e9cd4f5b-7704-4b70-8e78-d4d168134943" />

# Admin
These are pages accessible only when authenticated as an admin user.

## Management Pages
These pages are automatically generated with a self-made algorithms which transforms classes into html forms.
The functions make use of reflection to go through the properties/fields of the class, verify their datatype, then writes the form input fields and ids based on the properties data.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/79e3d07e-39f8-48c9-bc38-9f6815065f07" />

Example on how it is done:
```c#
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
```

## SQL
All SQLs are built using reflection, which means every SQL operation is written once in `DatabaseManager.cs`.
Ex. Update();
```c#
        public static int Update(Object item, SqlConnection connection, string table, params string[] primarykeys) {
            PropertyInfo[] attributes = item.GetType().GetProperties();
            string setstring = "";
            string wherestring = "";
            foreach (PropertyInfo P in attributes) {
                if (primarykeys.Contains(P.Name)) continue;
                setstring += P.Name + " = @" + P.Name + (P != attributes[attributes.Length - 1] ? "," : "");
            }
            foreach (string pk in primarykeys)
                wherestring += (pk == primarykeys[0] ? "" : " AND ") + pk + " = @" + pk;
            SqlCommand SQL = new SqlCommand(@"UPDATE " + table + " SET " + setstring + " WHERE " + wherestring, connection);
            foreach (PropertyInfo P in attributes) {
                if (P.PropertyType == typeof(byte[]))
                    SQL.Parameters.Add("@" + P.Name, SqlDbType.Binary).Value = P.GetValue(item) ?? DBNull.Value;
                else
                    SQL.Parameters.AddWithValue("@" + P.Name, P.GetValue(item) ?? DBNull.Value);
            }
            return SQL.ExecuteNonQuery();
        }
```

# Mobile-Friendly
The GUI has been made so that it is compatible with both Desktop and Mobile.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/e6687769-8393-4324-b019-3559e73bbff1" />
<img width="750" alt="image" src="https://github.com/user-attachments/assets/65945b22-1847-4fca-80a1-be869d2ac7d9" />



**Disclaimer**
- Note that the website information is entirely fictituous and does not belongs to any real organization.
