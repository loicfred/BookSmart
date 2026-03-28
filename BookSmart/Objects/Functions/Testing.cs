using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;
using Xunit;

namespace BookSmart.Objects.Functions {
    public class Testing {

        [Fact]
        public void CheckInsertDelete() {
            List<UnitTestingObj> L = UnitTestingObj.getObjects();
            Assert.True(L.Count == 1);
            UnitTestingObj A = new UnitTestingObj();
            A.Data1 = "Test1";
            A.Data2 = "Test2";
            A.Data3 = "Test3";
            A.Insert();

            L = UnitTestingObj.getObjects();
            Assert.True(L.Count == 2);
            Assert.True(L[1].Data1 == "Test1");
            Assert.True(L[1].Data2 == "Test2");
            Assert.True(L[1].Data3 == "Test3");

            Assert.True(L[1].Delete() == 1);

            L = UnitTestingObj.getObjects();
            Assert.True(L.Count == 1);
        }

        [Fact]
        public void CheckUpdate() {
            UnitTestingObj A = UnitTestingObj.getObjectById(1);
            string ogName = A.Data1;
            A.Data1 = "Apple";
            A.Update();

            UnitTestingObj B = UnitTestingObj.getObjectById(1);
            Assert.NotEqual(ogName, B.Data1);
            B.Data1 = ogName;
            Assert.True(B.Update() == 1);

            UnitTestingObj C = UnitTestingObj.getObjectById(1);
            Assert.Equal(ogName, C.Data1);
        }

        [Fact]
        public void CheckBook() {
            BookObj B = BookObj.getBookById(1);

            Assert.Equal(1, B.ID);
            Assert.Equal(B.getAuthor().ID, B.AuthorID);
            Assert.Equal(B.getCategory().ID, B.CategoryID);
            Assert.Equal(B.getPublisher().ID, B.PublisherID);
            Assert.Equal(B.getPublisher().ID, B.PublisherID);


            Assert.NotNull(B.Title);
            Assert.NotNull(B.Description);
            Assert.NotNull(B.Language);
            Assert.True(B.PublishDate != null);
            Assert.True(B.Pages > 0);
        }

        [Fact]
        public void CheckEmail() {
            try {
                SmtpClient C = MailManager.getEmailClient();
                MailMessage M = MailManager.WriteEmail("loic.cheerkoot@umail.utm.ac.mu", "Unit Testing",
                    "If this email has been received, it means the email testing has been successful. Thanks!");
                C.Send(M);
            } catch (Exception ex) {
                Assert.Null(ex);
            }
        }

        [Fact]
        public void CheckPassword() {
            Assert.False(UserObj.IsValidPassword("ABCD"));
            Assert.False(UserObj.IsValidPassword("Abcd"));
            Assert.False(UserObj.IsValidPassword("AB12"));
            Assert.False(UserObj.IsValidPassword("ABCD1234"));
            Assert.False(UserObj.IsValidPassword("97211144"));
            Assert.False(UserObj.IsValidPassword("ABcd1234"));
            Assert.False(UserObj.IsValidPassword("ABcd@12"));
            Assert.False(UserObj.IsValidPassword("&&&%%££$/!"));
            Assert.False(UserObj.IsValidPassword("9827@1254"));
            Assert.True(UserObj.IsValidPassword("ABcd@1234"));
            Assert.True(UserObj.IsValidPassword("A81Bcd@!z"));
            Assert.True(UserObj.IsValidPassword("4d!F^6w2@"));
        }

    }


    public class UnitTestingObj {
        public int ID { get; set; }
        public string Data1 { get; set; }
        public string Data2 { get; set; }
        public string Data3 { get; set; }

        public UnitTestingObj() { }
        public UnitTestingObj(SqlDataReader row) {
            DatabaseManager.ConstructObjectFromRow(this, row);
        }

        public static List<UnitTestingObj> getObjects() {
            List<UnitTestingObj> L = new List<UnitTestingObj>();
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectAll(C, "UnitTesting")) {
                    while (row.Read())
                        L.Add(new UnitTestingObj(row));
                }
            }
            return L;
        }
        public static UnitTestingObj getObjectById(int id) {
            using (SqlConnection C = DatabaseManager.getSQLConnection()) {
                using (SqlDataReader row = DatabaseManager.SelectByID(C, "UnitTesting", id)) {
                    return row.Read() ? new UnitTestingObj(row) : null;
                }
            }
        }

        public void Insert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                DatabaseManager.Insert(this, conn, "UnitTesting");
            }
        }
        public int Update() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                return DatabaseManager.Update(this, conn, "UnitTesting", "ID");
            }
        }
        public int UpdateElseInsert() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                return DatabaseManager.UpdateElseInsert(this, conn, "UnitTesting", "ID");
            }
        }
        public int Delete() {
            using (SqlConnection conn = DatabaseManager.getSQLConnection()) {
                return DatabaseManager.Delete(this, conn, "UnitTesting", "ID");
            }
        }
    }
}