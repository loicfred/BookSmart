using System.Data.SqlClient;

namespace BookSmart.Objects {
    public class CartItemObj {
        private BookObj Book { get; set; }
        public int UserID { get; set; }
        public int BookID { get; set; }
        public int Amount { get; set; }
        public CartItemObj() { }
        public CartItemObj(SqlDataReader row) {
            DatabaseManager.ConstructObjectFromRow(this, row);
        }

        public BookObj getBook() {
            return Book == null ? Book = BookObj.getBookById(BookID) : Book;
        }
    }
}