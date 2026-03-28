using System.Collections.Generic;
using System.Data.SqlClient;

namespace BookSmart.Objects {
    public class CategoryObj {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryObj() { }
        public CategoryObj(SqlDataReader row) {
            DatabaseManager.ConstructObjectFromRow(this, row);
        }
        public static List<CategoryObj> getCategories() {
            List<CategoryObj> L = new List<CategoryObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectAll(C, "Category")) {
                    while (row.Read())
                        L.Add(new CategoryObj(row));
                }
            }
            return L;
        }
        public static CategoryObj getCategoryById(int id) {
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectByID(C, "Category", id)) {
                    return row.Read() ? new CategoryObj(row) : null;
                }
            }
        }
        public void Insert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Insert(this, conn, "Category");
            }
        }
        public void Update() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Update(this, conn, "Category", "ID");
            }
        }
        public void UpdateElseInsert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.UpdateElseInsert(this, conn, "Category", "ID");
            }
        }
        public void Delete() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Delete(this, conn, "Category", "ID");
            }
        }
    }
}