using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.Data;
using System.IO;

namespace SmetaApplication.DbContexts
{
    public class DBConnection
    {
        public static string path = "";
        public static SQLiteConnection connection;
        public static SQLiteCommand command;
        public static SQLiteDataAdapter adapter;
        private static SQLiteTransaction transaction;

        public static bool IsConnect = false;

        public static void SetDefaultSettings()
        {
            path = "Data Source=" +  Directory.GetCurrentDirectory() + @"\AppData\SmetaDb.db";
            //path = "Data Source=" + @"D:\SmetaDb.db";
            //MessageBox.Show(File.Exists(Directory.GetCurrentDirectory() + @"\AppData\SmetaDb.db").ToString());
            //MessageBox.Show(Directory.GetCurrentDirectory() + @"\AppData\SmetaDb.db");
            try
            {
                connection = new SQLiteConnection(path, true);
                connection.Close();
                connection.Open();
                command = new SQLiteCommand(connection);
                adapter = new SQLiteDataAdapter(command);
                adapter.InsertCommand = command;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        public static void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        public static void CommitTransaction()
        {
            transaction.Commit();
            transaction = null;
        }

        public static void RollbackTransaction()
        {
            transaction.Rollback();
            transaction = null;
        }

        public static DataTable GetTableByQuery(string query)
        {
            try
            {
                DataTable table = new DataTable();
                command.CommandText = query;
                //adapter.InsertCommand = command;
                adapter.Fill(table);
                return table;
            }
            catch
            {
                MessageBox.Show("Ошибка");
                return null;
            }
        }

        public static long Save(string query)
        {
            command.CommandText = query;
            command.ExecuteNonQuery();
            return connection.LastInsertRowId;
        }

        public static int Update(string query)
        {
            command.CommandText = query;
            return command.ExecuteNonQuery();
        }

        public static int Delete(string query)
        {
            command.CommandText = query;
            return command.ExecuteNonQuery();
        }

        public static void SqlQuery(string query)
        {
            command.CommandText = query;
            command.ExecuteNonQuery();
            //MessageBox.Show(command.ExecuteNonQuery().ToString());
        }
    }
}
