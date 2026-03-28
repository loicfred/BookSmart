using System;
using System.Data.SqlClient;

namespace BookSmart.Objects {
    public class Email_Verification {
        private UserObj User { get; set; }
        public string Code { get; set; }
        public int UserID { get; set; }
        public string Action { get; set; }
        public DateTime Expiry { get; set; }

        public Email_Verification() {
            ClearOldCodes();
        }
        public Email_Verification(SqlDataReader row) {
            DatabaseManager.ConstructObjectFromRow(this, row);
        }

        public static Email_Verification getVerificationByID(string id, string action) {
            ClearOldCodes();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Email_Verification WHERE UserID = @UserID AND Action = @Action", C);
                CMD.Parameters.AddWithValue("@UserID", id);
                CMD.Parameters.AddWithValue("@Action", action);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    return row.Read() ? new Email_Verification(row) : null;
                }
            }
        }

        public static Email_Verification getVerificationByCode(string code, string action) {
            ClearOldCodes();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Email_Verification WHERE Code = @Code AND Action = @Action", C);
                CMD.Parameters.AddWithValue("@Code", code);
                CMD.Parameters.AddWithValue("@Action", action);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    return row.Read() ? new Email_Verification(row) : null;
                }
            }
        }

        public void Insert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Insert(this, conn, "Email_Verification");
            }
        }

        public void Delete() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Delete(this, conn, "Email_Verification", "UserID");
            }
        }

        public UserObj getUser() {
            return User == null ? User = UserObj.getUserById(UserID) : User;
        }

        public static void ClearOldCodes() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {

                new SqlCommand(@"DELETE FROM Email_Verification WHERE expiry < GETUTCDATE();", conn).ExecuteNonQuery();
            }
        }
        public static void ClearUserCodes(UserObj user) {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"DELETE FROM Email_Verification WHERE UserID = @UserID", conn);
                CMD.Parameters.AddWithValue("@UserID", user.ID);
                CMD.ExecuteNonQuery();
            }
        }

    }
}