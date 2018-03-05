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
using System.Diagnostics;
using MahApps.Metro.Controls;

namespace FancySaiEditor.Database
{
    /// <summary>
    /// Handles all interactions with MySql and SQLite Database.
    /// </summary>
    public static class DatabaseConnection
    {
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
                    if(Properties.Settings.Default.MysqlServer == "" || Properties.Settings.Default.MysqlWorldDatabase == "" || Properties.Settings.Default.MysqlUsername == "" || Properties.Settings.Default.MysqlPassword == "")
                    {
                        new SelectMysqlDatabase().ShowDialog();
                    }

                    OpenMysqlDatabase(Properties.Settings.Default.MysqlServer, Properties.Settings.Default.MysqlPort, Properties.Settings.Default.MysqlWorldDatabase, Properties.Settings.Default.MysqlUsername, Properties.Settings.Default.MysqlPassword);
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

        private static void OpenMysqlDatabase(string server, string port, string database, string username, string password)
        {
            mySqlConnection = new MySqlConnection($"SERVER={server}; DATABASE={database}; UID={username}; PASSWORD={password}");
            mySqlConnection.Open();
        }

        public static void CheckMysqlConnection(string server, string port, string database, string username, string password)
        {
            try
            {
                OpenMysqlDatabase(server, port, database, username, password);
                mySqlConnection.Close();
                MessageBox.Show("Connection successful!");
            }
            catch(MySqlException)
            {
                MessageBox.Show("Connection was not successful!");
            }
        }

        #region Data Functions

        #region MySql

        public static async Task<DataTable> SelectMysqlData(string tableName, string selectColumnName, string searchTerm)
        {
            DataTable data = new DataTable();
            try
            {
                using (MySqlCommand query = mySqlConnection.CreateCommand())
                {
                    query.CommandText = "SELECT " + "*" + " FROM " + tableName + " WHERE " + selectColumnName + "=" + searchTerm + " LIMIT 100";
                    using (MySqlDataAdapter dataAdp = new MySqlDataAdapter(query))
                    {
                        await dataAdp.FillAsync(data);
                        foreach (DataColumn c in data.Columns)
                            c.ReadOnly = true;
                        return data;
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show("Database Error!\nError: " + e.Message);
            }
            return data;
        }

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

        /// <summary>
        /// Selects data specified by the parameters from world database with custom WHERE statement.
        /// <para>
        /// Example for whereStatement: 'entry=12345 AND groupid=0'
        /// </para>
        /// </summary>
        public static async Task SelectMySqlData(NodeData data, string whereStatement)
        {
            try
            {
                data.Clear();
                using (MySqlCommand query = mySqlConnection.CreateCommand())
                {
                    string fieldNames = DetermineFieldNames(data);

                    query.CommandText = "SELECT " + fieldNames + " FROM " + data.SelectTableName + " WHERE " + whereStatement + " LIMIT 100";
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
        /// Selects data specified by the parameters from world database.
        /// </summary>
        /// <param name="selectColumnName">Name of database column to search for.</param>
        /// <param name="searchTerm"></param>
        /// <param name="data">NodeData object to be filled with data from world database containing the columns which should be selected.</param>
        public static async Task SelectSqliteData(string selectColumnName, string searchTerm, NodeData data)
        {
            try
            {
                data.Clear();
                using (SQLiteCommand query = new SQLiteCommand(sqliteConnection))
                {
                    string fieldNames = DetermineFieldNames(data);
                    //Check if the select field is a text or a number field
                    using (DataTable schema = sqliteConnection.GetSchema("Tables"))
                    {
                        String[] columnsRestrictions = new String[4];
                        columnsRestrictions[2] = data.SelectTableName;
                        columnsRestrictions[3] = selectColumnName;
                        DataTable columnsTable = sqliteConnection.GetSchema("Columns", columnsRestrictions);

                        Debug.Assert(columnsTable.Rows.Count == 1, "More or less than one column or table matches the restrictions!");
                        string columnType = columnsTable.Rows[0]["DATA_TYPE"].ToString();

                        if (columnType == "char" || columnType == "varchar" || columnType == "tinytext" || columnType == "text" || columnType == "mediumtext" || columnType == "longtext")
                            searchTerm = " LIKE '%" + searchTerm + "%'";
                        else
                            searchTerm = "=" + searchTerm;
                    }

                    query.CommandText = "SELECT " + fieldNames + " FROM " + data.SelectTableName + " WHERE " + selectColumnName + searchTerm + " LIMIT 100";
                    using (SQLiteDataAdapter dataAdp = new SQLiteDataAdapter(query))
                    {
                        dataAdp.Fill(data); //TODO: Make this async
                        foreach (DataColumn c in data.Columns)
                            c.ReadOnly = true;
                        return;
                    }
                }
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("Database Error!\nError: " + e.Message);
            }
            return;
        }

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
                    query.CommandText = "SELECT general FROM node_tooltips WHERE Type LIKE '" + type + "';";
                    using (DbDataReader dataReader = await query.ExecuteReaderAsync())
                    {
                        if (await dataReader.ReadAsync())
                        {
                            return dataReader["general"].ToString();
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

        public static string GetNodeParamTooltip(NodeType type, ParamId paramId)
        {
            try
            {
                using (SQLiteCommand query = new SQLiteCommand(sqliteConnection))
                {
                    string field = $"param{ ((int)paramId + 1).ToString()}";
                    query.CommandText = $"SELECT {field} FROM node_tooltips WHERE Type LIKE '{type}';";
                    using (DbDataReader dataReader = query.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            if(dataReader[field].ToString() != "")
                                return dataReader[field].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Database Error!\nError: " + e.Message);
            }

            return null;
        }

        #endregion

        #endregion
    }
}
