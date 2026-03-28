using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BookSmart.Objects {
    public class OrderObj {
        private UserObj User { get; set; }
        private List<OrderItem> Books { get; set; }

        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public OrderObj() { }
        public OrderObj(SqlDataReader row) {
            DatabaseManager.ConstructObjectFromRow(this, row);
            Books = getItems();
        }

        public string getItemsAsString() {
            StringBuilder sb = new StringBuilder();
            foreach (OrderItem book in Books)
                sb.Append("• " + book.Book.Title + " x" + book.Amount + "<br/>");
            return sb.ToString();
        }

        public double getTotalCost() {
            double total = 0;
            foreach (OrderItem i in Books)
                total += i.Book.Price * i.Amount;
            return total;
        }

        public List<OrderItem> getItems() {
            Books = new List<OrderItem>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Order_Item WHERE OrderID = @OrderID", C);
                CMD.Parameters.AddWithValue("@OrderID", ID);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    while (row.Read())
                        Books.Add(new OrderItem(row));
                }
            }
            return Books;
        }

        public static List<OrderObj> getOrders() {
            List<OrderObj> L = new List<OrderObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectAll(C, "Orders")) {
                    while (row.Read())
                        L.Add(new OrderObj(row));
                }
            }
            return L;
        }

        public static OrderObj getOrderById(int id) {
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectByID(C, "Orders", id)) {
                    return row.Read() ? new OrderObj(row) : null;
                }
            }
        }

        public void Insert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Insert(this, conn, "Orders");
            }
        }
        public void Update() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Update(this, conn, "Orders", "ID");
            }
        }
        public void UpdateElseInsert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.UpdateElseInsert(this, conn, "Orders", "ID");
            }
        }

        public UserObj getUser() {
            return User == null ? User = UserObj.getUserById(UserID) : User;
        }
    }

    public class OrderItem {
        public BookObj Book { get; set; }
        public int Amount { get; set; }

        public OrderItem(SqlDataReader row) {
            Book = BookObj.getBookById(int.Parse(row["BookID"].ToString()));
            Amount = int.Parse(row["Amount"].ToString());
        }
    }
}