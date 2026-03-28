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

<img width="750" alt="image" src="https://github.com/user-attachments/assets/55bf63b7-d8fc-4426-802c-e93dff6049d9" />

## Booklist
This is page where all books are listed and are grouped by category.
Changing the category also changes the book list dynamically.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/f739f581-0efc-4689-b476-30ecfc055583" />

## Book
This is the page which displays all data of a book and where users can also add books to cart. There are no payment feature available.
The data is loading dynamically using url parameters as book id and the comments are fetched as well ordered by date.
A user must be logged in to be able to comment.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/b92b4734-baa0-4b90-a3fd-d05750d65f3b" />

## Edit Profile
A logged in user can also view and modify their profile information, they are able to view their current cart content and download their order history as a PDF file.
They can also view and delete their comments on any book page.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/e5d1a7d1-34b9-4240-a8a9-d6949228aeea" />
<img width="750" alt="image" src="https://github.com/user-attachments/assets/2e7d43b0-0ff5-43e7-8ca5-c3ff04631613" />
<img width="750" alt="image" src="https://github.com/user-attachments/assets/9c2e181c-7f9a-46ac-9ccb-5daea3c13ea7" />
<img width="750" alt="image" src="https://github.com/user-attachments/assets/e7d456a5-5ed9-4578-9ca1-fda0569342b3" />

## Contact Form
This is a where users can feel a form to submit their complaint. The complaint is then received by email.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/edb347b8-8b25-4db7-84d5-ea0136b3d6f4" />
<img width="500" alt="image" src="https://github.com/user-attachments/assets/59603911-37ab-479d-b693-d23e864b7783" />

## About Page
This page provides information to users about the owners of the website.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/86883f0a-3ef9-4925-badf-c494a738770a" />

# Authentication
The web application supports full authentication features, this include log-in, sign-in & password reset.

## Register & Verification
The user can register with a new account, once they register they will receive a verification email to make sure the email belongs to them. They must enter the verification code received to successfully register.

<img width="750" height="437" alt="image" src="https://github.com/user-attachments/assets/71b5d3fa-5f6e-45ed-874a-350732f70b55" />
<img width="400" height="408" alt="image" src="https://github.com/user-attachments/assets/c23e3340-01bb-4f13-9b39-4e47ea5ca2d1" />


## Log-in / Reset Password
The user can enter his email (or username) and password to authenticate. The passwords are hashed and the result of the log-in stores the current user in the session.
The user can also reset their password using either email (phone is non-functional). Once they submit, they will receive their email to enter a view password.
The reset password url of the email contains a url parameter which links the password reset to the user account. The email has a validity of 10 minutes.
It is similar to verification.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/e00b9d81-ed21-4802-a2a3-83a5546a6a94" />

# Admin
These are pages accessible only when authenticated as an admin user.

## Management Pages
These pages are automatically generated with a self-made algorithms which transforms classes into html forms.
The functions make use of reflection to go through the properties/fields of the class, verify their datatype, then writes the form input fields and ids based on the properties data.

<img width="750" alt="image" src="https://github.com/user-attachments/assets/f7db0c97-7bd2-44ec-bbfa-a567af81d635" />

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

<img width="750" alt="image" src="https://github.com/user-attachments/assets/d5391e80-c3c9-4498-839e-aa8297044cf5" />
<img width="750" alt="image" src="https://github.com/user-attachments/assets/066e385f-fb42-4d51-bee1-634169aa3de8" />



**Disclaimer**
- Note that the website information is entirely fictituous and does not belongs to any real organization.
