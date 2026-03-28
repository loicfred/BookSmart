using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BookSmart.Objects;

namespace BookSmart {
    public class BookObj {
        private CategoryObj Category { get; set; }
        private AuthorObj Author { get; set; }
        private PublisherObj Publisher { get; set; }


        public int ID { get; set; }
        public int CategoryID { get; set; }
        public int AuthorID { get; set; }
        public int PublisherID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public DateTime PublishDate { get; set; }
        public double Price { get; set; }
        public int Pages { get; set; }
        public int Quantity { get; set; }
        public byte[] Image { get; set; }
        public bool isDisabled { get; set; }

        public BookObj() { }
        public BookObj(SqlDataReader row) {
            DatabaseManager.ConstructObjectFromRow(this, row);
        }

        public string getImage() {
            return Image != null ? Convert.ToBase64String(Image) : null;
        }

        public bool isAvailable() {
            return Quantity > 0;
        }

        public static List<BookObj> getBooks() {
            List<BookObj> L = new List<BookObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectAll(C, "Book")) {
                    while (row.Read())
                        L.Add(new BookObj(row));
                }
            }
            return L;
        }
        public static List<BookObj> getBooksByCategory(int categoryid) {
            List<BookObj> L = new List<BookObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Book WHERE CategoryID = @CategoryID", C);
                CMD.Parameters.AddWithValue("@CategoryID", categoryid);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    while (row.Read())
                        L.Add(new BookObj(row));
                }
            }
            return L;
        }
        public static List<BookObj> getBooksByName(string name) {
            List<BookObj> L = new List<BookObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Book WHERE Title LIKE @Title", C);
                CMD.Parameters.AddWithValue("@Title", '%' + name + '%');
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    while (row.Read())
                        L.Add(new BookObj(row));
                }
            }
            return L;
        }
        public static List<BookObj> getMostPopularBooks() {
            List<BookObj> L = new List<BookObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                SqlCommand CMD = new SqlCommand(@"SELECT * FROM Book WHERE Quantity > 0 AND isDisabled = 0 ORDER BY Views DESC;", C);
                using (SqlDataReader row = CMD.ExecuteReader()) {
                    while (row.Read()) {
                        L.Add(new BookObj(row));
                        if (L.Count == 3)
                            return L;
                    }
                }
            }
            return L;
        }

        public static BookObj getBookById(int id) {
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectByID(C, "Book", id)) {
                    return row.Read() ? new BookObj(row) : null;
                }
            }
        }
        public void AddView() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.IncrementAmount(this, conn, "Book", "Views", "ID");
            }
        }
        public void Insert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Insert(this, conn, "Book");
            }
        }
        public void Update() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Update(this, conn, "Book", "ID");
            }
        }
        public void UpdateElseInsert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.UpdateElseInsert(this, conn, "Book", "ID");
            }
        }
        public void Delete() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Delete(this,conn, "Book", "ID");
            }
        }

        public CategoryObj getCategory() {
            return Category == null ? Category = CategoryObj.getCategoryById(CategoryID) : Category;
        }
        public AuthorObj getAuthor() {
            return Author == null ? Author = AuthorObj.getAuthorById(AuthorID) : Author;
        }
        public PublisherObj getPublisher() {
            return Publisher == null ? Publisher = PublisherObj.getPublisherById(PublisherID) : Publisher;
        }
    }
}