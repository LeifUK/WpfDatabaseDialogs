using System;
using System.Linq;

namespace WpfFungusApp.DBStore
{
    internal class SQLiteDatabase
    {
        public static void CreateDatabase(IDatabaseHost databaseHost, string folder, string dbName)
        {
            string path = System.IO.Path.Combine(folder, dbName + ".sqlite");
            databaseHost.Database = new PetaPoco.Database("Data Source=" + path + ";Version=3;", "System.Data.SQLite");
            databaseHost.Database.OpenSharedConnection();
        }

        public static void OpenDatabase(IDatabaseHost databaseHost, string filepath)
        {
            databaseHost.Database = new PetaPoco.Database("Data Source=" + filepath + ";Version=3;", "System.Data.SQLite");
            databaseHost.Database.OpenSharedConnection();
        }
    }
}
