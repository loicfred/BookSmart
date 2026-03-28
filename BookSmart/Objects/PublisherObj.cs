using System.Collections.Generic;
using System.Data.SqlClient;

namespace BookSmart.Objects {
    public class PublisherObj {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }


        public PublisherObj() { }
        public PublisherObj(SqlDataReader row) {
            DatabaseManager.ConstructObjectFromRow(this, row);
        }

        public static List<PublisherObj> getPublishers() {
            List<PublisherObj> L = new List<PublisherObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectAll(C, "Publisher")) {
                    while (row.Read())
                        L.Add(new PublisherObj(row));
                }
            }
            return L;
        }

        public static PublisherObj getPublisherById(int id) {
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectByID(C, "Publisher", id)) {
                    return row.Read() ? new PublisherObj(row) : null;
                }
            }
        }

        public void Insert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Insert(this, conn, "Publisher");
            }
        }
        public void Update() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Update(this, conn, "Publisher", "ID");
            }
        }
        public void UpdateElseInsert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.UpdateElseInsert(this, conn, "Publisher", "ID");
            }
        }
        public void Delete() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Delete(this, conn, "Publisher", "ID");
            }
        }
    }
}