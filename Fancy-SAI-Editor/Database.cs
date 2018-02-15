using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SQLite;
using System.Data.Common;

namespace NodeAI
{
    /// <summary>
    /// Handles all interactions with MySql and SQLite Database.
    /// </summary>
    public static class Database
    {
        static Database()
        {

        }

        private static MySqlConnection mySqlConnection;
        private static SQLiteConnection sqliteConnection;

        /// <summary>
        /// Initializes the connections with the databases.
        /// </summary>
        public static void InitializeDatabase()
        {
            try
            {
                //Init Mysql connection
                try
                {
                    mySqlConnection = new MySqlConnection(
                        "SERVER=" + Properties.Settings.Default.MysqlServer + "; " +
                        "DATABASE=" + Properties.Settings.Default.MysqlWorldDatabase + ";" +
                        " PASSWORD=" + Properties.Settings.Default.MysqlPassword);
                    mySqlConnection.Open();


                }
                catch (MySqlException e)
                {
                    MessageBox.Show("Can't connect to Mysql Database!\nError: " + e.Message);
                }
                //Init Sqlite Connection
                try
                {
                    sqliteConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.SQLiteDatabase + "; Version=3;");
                    sqliteConnection.Open();
                }
                catch (SQLiteException e)
                {
                    MessageBox.Show("Can't connect to SQLite Database!\nError: " + e.Message);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Unknown error in InitializeDatabase\nError: " + e.Message);
            }
        }

        #region Data Functions

        #region MySql

        /// <summary>
        /// Selects data specified by the parameters from world database.
        /// </summary>
        /// <param name="selectColumnName">Name of database column to search for.</param>
        /// <param name="searchTerm"></param>
        /// <param name="data">NodeData object to be filled with data from world database containing the columns which should be selected.</param>
        public static async Task SelectMySqlData(string selectColumnName, string searchTerm, NodeData data)
        {
            try
            {
                data.Clear();
                using (MySqlCommand query = mySqlConnection.CreateCommand())
                {
                    string fieldNames = DetermineFieldNames(data);
                    //Check if the select field is a text or a number field
                    query.CommandText = "SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME ='" + data.SelectTableName + "' AND COLUMN_NAME='" + selectColumnName + "'";
                    using (DbDataReader dataReader = await query.ExecuteReaderAsync())
                    {
                        await dataReader.ReadAsync();
                        if (!dataReader.HasRows)
                            return;
                        string columnType = dataReader.GetString(0);
                        if (columnType == "char" || columnType == "varchar" || columnType == "tinytext" || columnType == "text" || columnType == "mediumtext" || columnType == "longtext")
                            searchTerm = " LIKE '%" + searchTerm + "%'";
                        else
                            searchTerm = "=" + searchTerm;
                        dataReader.Close();
                    }

                    query.CommandText = "SELECT " + fieldNames + " FROM " + data.SelectTableName + " WHERE " + selectColumnName + searchTerm + " LIMIT 100";
                    using (MySqlDataAdapter dataAdp = new MySqlDataAdapter(query))
                    {
                        
                        await dataAdp.FillAsync(data);
                        foreach (DataColumn c in data.Columns)
                            c.ReadOnly = true;
                        return;
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Database Error!\nError: " + e.Message);
            }
            return;
        }

        public static async Task SelectBroadcastText(string searchTerm, BroadcastTextData data)
        {
            try
            {
                data.Clear();
                using (MySqlCommand query = mySqlConnection.CreateCommand())
                {
                    string fieldNames = DetermineFieldNames(data);

                    query.CommandText = "SELECT " + fieldNames + " FROM " + data.SelectTableName + " WHERE MaleText LIKE '%" + searchTerm + "%' OR FemaleText LIKE '%" + searchTerm + "%' LIMIT 100";
                    using (MySqlDataAdapter dataAdp = new MySqlDataAdapter(query))
                    {

                        await dataAdp.FillAsync(data);
                        foreach (DataColumn c in data.Columns)
                            c.ReadOnly = true;
                        return;
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Database Error!\nError: " + e.Message);
            }
            return;
        }

        /// <summary>
        /// Trys to determine the fields to select with the names of the columns in the data type
        /// </summary>
        /// <param name="data">Instance of the DataTable which is going to be filled with data from Database</param>
        /// <returns>Returns a string which can be added to a <c>SELECT</c> Statement</returns>
        private static string DetermineFieldNames(DataTable data)
        {
            string fieldNames = "";
            try
            {
                foreach (DataColumn column in data.Columns)
                {
                    fieldNames += column.ColumnName + ",";
                }
                fieldNames = fieldNames.TrimEnd(',');

                if (fieldNames.Length == 0)
                    throw new ArgumentNullException();

                return fieldNames;
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("No fields to select!\n" +
                                "Something is wrong with the type of the DataTable!");
            }
            finally
            {
                fieldNames += "*"; //Add this to select all fields to prevent DB Errors
            }
            return fieldNames;
        }

        #endregion

        #region SQLite

        /// <summary>
        /// Trys to find a tooltip for the given NodeType.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static async Task<string> FindNodeTooltip(NodeType type)
        {
            try
            {
                using (SQLiteCommand query = new SQLiteCommand(sqliteConnection))
                {
                    query.CommandText = "SELECT tooltip FROM node_tooltips WHERE Type LIKE '" + type + "';";
                    using (DbDataReader dataReader = await query.ExecuteReaderAsync())
                    {
                        if (await dataReader.ReadAsync())
                        {
                            return dataReader["tooltip"].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Database Error!\nError: " + e.Message);
            }

            return "No tooltip available!";
        }

        #endregion

        #endregion
    }
}
