using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;

namespace BookSmart {
    public class DatabaseManager {

        public static SqlConnection getSQLConnection() {
            var strcon = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(strcon);
            connection.Open();
            return connection;
        }

        public static SqlDataReader SelectByID(SqlConnection connection, string table, long ID) {
            SqlCommand CMD = new SqlCommand(@"SELECT * FROM " + table + " WHERE ID = @ID", connection);
            CMD.Parameters.AddWithValue("@ID", ID);
            return CMD.ExecuteReader();
        }
        public static SqlDataReader SelectAll(SqlConnection connection, string table) {
            return new SqlCommand(@"SELECT * FROM " + table, connection).ExecuteReader();
        }


        public static int Delete(Object item, SqlConnection connection, string table, params string[] primarykeys) {
            PropertyInfo[] attributes = item.GetType().GetProperties();
            string wherestring = "";
            foreach (string pk in primarykeys)
                wherestring += (pk == primarykeys[0] ? "" : " AND ") + pk + " = @" + pk;
            SqlCommand CMD = new SqlCommand(@"DELETE FROM " + table + " WHERE " + wherestring, connection);
            foreach (PropertyInfo P in attributes)
                if (primarykeys.Contains(P.Name)) CMD.Parameters.AddWithValue("@" + P.Name, P.GetValue(item) ?? DBNull.Value);
            return CMD.ExecuteNonQuery();
        }

        public static int Insert(Object item, SqlConnection connection, string table) {
            PropertyInfo[] attributes = item.GetType().GetProperties();
            string columnstring = "";
            string valuestring = "";
            foreach (PropertyInfo P in attributes) {
                if (!P.Name.Equals("ID")) {
                    columnstring += P.Name;
                    valuestring += "@" + P.Name;
                    if (P != attributes[attributes.Length - 1]) {
                        columnstring += ",";
                        valuestring += ",";
                    }
                }
            }
            SqlCommand SQL = new SqlCommand(@"INSERT INTO " + table + " (" + columnstring + ") VALUES (" + valuestring + ")", connection);
            foreach (PropertyInfo P in attributes) {
                if (P.PropertyType == typeof(byte[]))
                    SQL.Parameters.Add("@" + P.Name, SqlDbType.Binary).Value = P.GetValue(item) ?? DBNull.Value;
                else
                    SQL.Parameters.AddWithValue("@" + P.Name, P.GetValue(item) ?? DBNull.Value);
            }
            return SQL.ExecuteNonQuery();
        }

        public static int Update(Object item, SqlConnection connection, string table, params string[] primarykeys) {
            PropertyInfo[] attributes = item.GetType().GetProperties();
            string setstring = "";
            string wherestring = "";
            foreach (PropertyInfo P in attributes) {
                if (primarykeys.Contains(P.Name)) continue;
                setstring += P.Name + " = @" + P.Name + (P != attributes[attributes.Length - 1] ? "," : "");
            }
            foreach (string pk in primarykeys)
                wherestring += (pk == primarykeys[0] ? "" : " AND ") + pk + " = @" + pk;
            SqlCommand SQL = new SqlCommand(@"UPDATE " + table + " SET " + setstring + " WHERE " + wherestring, connection);
            foreach (PropertyInfo P in attributes) {
                if (P.PropertyType == typeof(byte[]))
                    SQL.Parameters.Add("@" + P.Name, SqlDbType.Binary).Value = P.GetValue(item) ?? DBNull.Value;
                else
                    SQL.Parameters.AddWithValue("@" + P.Name, P.GetValue(item) ?? DBNull.Value);
            }
            return SQL.ExecuteNonQuery();
        }
        public static int IncrementAmount(Object item, SqlConnection connection, string table, string column, params string[] primarykeys) {
            PropertyInfo[] attributes = item.GetType().GetProperties();
            string wherestring = "";
            foreach (string pk in primarykeys)
                wherestring += (pk == primarykeys[0] ? "" : " AND ") + pk + " = @" + pk;
            SqlCommand SQL = new SqlCommand(@"UPDATE " + table + " SET " + column + " = " + column + " + 1 WHERE " + wherestring, connection);
            foreach (PropertyInfo P in attributes)
                if (primarykeys.Contains(P.Name)) SQL.Parameters.AddWithValue("@" + P.Name, P.GetValue(item) ?? DBNull.Value);
            return SQL.ExecuteNonQuery();
        }
        public static int UpdateElseInsert(Object item, SqlConnection connection, string table, params string[] primarykeys) {
            int updatedrows = Update(item, connection, table, primarykeys);
            return updatedrows > 0 ? updatedrows : Insert(item, connection, table);
        }


        public static void ConstructObjectFromRow(Object obj, SqlDataReader row) {
            foreach (PropertyInfo field in obj.GetType().GetProperties()) {
                try {
                    if (row[field.Name] != DBNull.Value) {
                        field.SetValue(obj, Convert.ChangeType(row[field.Name], field.PropertyType));
                    }
                } catch (IndexOutOfRangeException) {}
            }
        }
    }

}