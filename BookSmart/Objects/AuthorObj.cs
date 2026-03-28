using System.Collections.Generic;
using System.Data.SqlClient;

namespace BookSmart.Objects {
    public class AuthorObj {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Country { get; set; }

        public AuthorObj() { }
        public AuthorObj(SqlDataReader row) {
            DatabaseManager.ConstructObjectFromRow(this, row);
        }

        public static List<AuthorObj> getAuthors() {
            List<AuthorObj> L = new List<AuthorObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectAll(C, "Author")) {
                    while (row.Read())
                        L.Add(new AuthorObj(row));
                }
            }
            return L;
        }
        public static AuthorObj getAuthorById(int id) {
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectByID(C, "Author", id)) {
                    return row.Read() ? new AuthorObj(row) : null;
                }
            }
        }

        public void Insert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Insert(this, conn, "Author");
            }
        }
        public void Update() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Update(this, conn, "Author", "ID");
            }
        }
        public void UpdateElseInsert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.UpdateElseInsert(this, conn, "Author", "ID");
            }
        }
        public void Delete() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Delete(this,conn, "Author", "ID");
            }
        }
    }
}