using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BookSmart.Objects {
    public class ReviewObj {
        private BookObj Book { get; set; }
        private UserObj User { get; set; }

        public int ID { get; set; }
        public int BookID { get; set; }
        public int UserID { get; set; }
        public string Comment { get; set; }
        public DateTime TimeCreated { get; set; }

        public ReviewObj() { }
        public ReviewObj(SqlDataReader row) {
            DatabaseManager.ConstructObjectFromRow(this, row);
        }

        public static ReviewObj getReviewById(int id) {
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectByID(C, "Review", id)) {
                    if (row.Read())
                        return new ReviewObj(row);
                }
            }
            return null;
        }
        public static List<ReviewObj> getReviewsOfBook(int bookid) {
            List<ReviewObj> L = new List<ReviewObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Review WHERE BookID = @BookID ORDER BY TimeCreated DESC;", C);
                CMD.Parameters.AddWithValue("@BookID", bookid);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    while (row.Read())
                        L.Add(new ReviewObj(row));
                }
            }
            return L;
        }


        public void Insert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Insert(this, conn, "Review");
            }
        }
        public void Update() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Update(this, conn, "Review", "ID");
            }
        }
        public void Delete() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Delete(this, conn, "Review", "ID");
            }
        }
        public void UpdateElseInsert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.UpdateElseInsert(this, conn, "Review", "ID");
            }
        }


        public UserObj getUser() {
            return User == null ? User = UserObj.getUserById(UserID) : User;
        }
        public BookObj getBook() {
            return Book == null ? Book = BookObj.getBookById(BookID) : Book;
        }
    }
}