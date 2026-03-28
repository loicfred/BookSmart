using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BookSmart.Objects {

    public class UserObj {
        public int ID { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public DateTime RegistrationDate { get; set; }
        public byte[] Avatar { get; set; }
        public bool hasVerifiedEmail { get; set; }
        public bool hasVerifiedPhone { get; set; }
        public bool hasPromotionEnabled { get; set; }
        public bool isDisabled { get; set; }


        public List<ReviewObj> getReviewsOfUser() {
            List<ReviewObj> L = new List<ReviewObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Review WHERE UserID = @UserID ORDER BY TimeCreated DESC;", C);
                CMD.Parameters.AddWithValue("@UserID", ID);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    while (row.Read())
                        L.Add(new ReviewObj(row));
                }
            }
            return L;
        }

        public List<OrderObj> getThreeMonthsOrders() {
            List<OrderObj> L = new List<OrderObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Orders WHERE UserID = @UserID AND OrderDate >= DATEADD(MONTH, -3, GETDATE());", C);
                CMD.Parameters.AddWithValue("@UserID", ID);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    while (row.Read())
                        L.Add(new OrderObj(row));
                }
            }
            return L;
        }

        public List<OrderObj> getAllOrders() {
            List<OrderObj> L = new List<OrderObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Orders WHERE UserID = @UserID;", C);
                CMD.Parameters.AddWithValue("@UserID", ID);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    while (row.Read())
                        L.Add(new OrderObj(row));
                }
            }
            return L;
        }
        public List<OrderObj> getCompletedOrders() {
            List<OrderObj> L = new List<OrderObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Orders WHERE Status = 'COMPLETE' AND UserID = @UserID;", C);
                CMD.Parameters.AddWithValue("@UserID", ID);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    while (row.Read())
                        L.Add(new OrderObj(row));
                }
            }
            return L;
        }
        public List<OrderObj> getPendingOrders() {
            List<OrderObj> L = new List<OrderObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Orders WHERE Status = 'PENDING' AND UserID = @UserID;", C);
                CMD.Parameters.AddWithValue("@UserID", ID);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    while (row.Read())
                        L.Add(new OrderObj(row));
                }
            }
            return L;
        }

        public UserObj() { }
        public UserObj(SqlDataReader row) {
            DatabaseManager.ConstructObjectFromRow(this, row);
        }
        public string getAvatar() {
            return Avatar != null ? Convert.ToBase64String(Avatar) : null;
        }

        public static List<UserObj> getUsers() {
            List<UserObj> L = new List<UserObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectAll(C, "Users")) {
                    while (row.Read())
                        L.Add(new UserObj(row));
                }
            }
            return L;
        }
        public static UserObj getUserById(int id) {
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectByID(C, "Users", id)) {
                    return row.Read() ? new UserObj(row) : null;
                }
            }
        }
        public static UserObj getUserByLogin(string name, string password) {
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Users WHERE (Username = @Username OR Email = @Email) AND Password = @Password;", C);
                CMD.Parameters.AddWithValue("@Username", name);
                CMD.Parameters.AddWithValue("@Email", name);
                CMD.Parameters.AddWithValue("@Password", password);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    return row.Read() ? new UserObj(row) : null;
                }
            }
        }
        public static UserObj getUserByName(string name) {
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Users WHERE Username = @Username;", C);
                CMD.Parameters.AddWithValue("@Username", name);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    return row.Read() ? new UserObj(row) : null;
                }
            }
        }
        public static UserObj getUserByEmail(string email) {
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Users WHERE Email = @Email;", C);
                CMD.Parameters.AddWithValue("@Email", email);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    return row.Read() ? new UserObj(row) : null;
                }
            }
        }

        public void AddToCart(int bookId, int Amount) {
            CartItemObj C = new CartItemObj {
                UserID = ID, BookID = bookId, Amount = Amount
            };
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                if (DatabaseManager.IncrementAmount(C, conn, "Cart_Item", "Amount", "UserID", "BookID") == 0) {
                    DatabaseManager.Insert(C, conn, "Cart_Item");
                }
            }
        }
        public List<CartItemObj> GetCartItems() {
            List<CartItemObj> L = new List<CartItemObj>();
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Cart_Item WHERE UserID = @UserID;", conn);
                CMD.Parameters.AddWithValue("@UserID", ID);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    while (row.Read())
                        L.Add(new CartItemObj(row));
                }
            }
            return L;
        }

        public void ClearCart() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Delete(new CartItemObj {
                    UserID = ID,
                }, conn, "Cart_Item", "UserID");
            }
        }

        public void Insert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Insert(this, conn, "Users");
            }
        }
        public void Update() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Update(this, conn, "Users", "ID");
            }
        }
        public void UpdateElseInsert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.UpdateElseInsert(this, conn, "Users", "ID");
            }
        }

        public static bool IsValidPassword(string password) {
            if (string.IsNullOrEmpty(password))
                return false;

            if (password.Length < 8)
                return false;

            bool hasUpperCase = Regex.IsMatch(password, "[A-Z]");
            bool hasLowerCase = Regex.IsMatch(password, "[a-z]");
            bool hasDigit = Regex.IsMatch(password, "[0-9]");
            bool hasSpecialChar = Regex.IsMatch(password, "[^a-zA-Z0-9]");

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

        public string GenerateVerificationCode(string action) {
            Email_Verification.ClearUserCodes(this);
            string code = Guid.NewGuid().ToString();

            Email_Verification EV = new Email_Verification();
            EV.Action = action;
            EV.UserID = ID;
            EV.Code = code;
            EV.Expiry = DateTime.UtcNow.AddMinutes(3);
            EV.Insert();
            return code;
        }
    }
}