using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace TodoList
{
    class DataBase
    {
        public SQLiteConnection myConection;

        public DataBase()
        {
            myConection = new  SQLiteConnection("Data Source=database.sqlite3");

            if (!File.Exists("database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
            }

        }

        public void OpenConecction()
        {
            if(myConection.State != System.Data.ConnectionState.Open)
            {
                myConection.Open();
            }
        }

        public void CloseConecction()
        {
            if (myConection.State != System.Data.ConnectionState.Closed)
            {
                myConection.Close();
            }
        }
    }
}
