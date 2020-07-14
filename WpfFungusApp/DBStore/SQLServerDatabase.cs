using System;
using System.Linq;

namespace WpfFungusApp.DBStore
{
    internal class SQLServerDatabase
    {
        public static void NewDatabaseView(IDatabaseHost databaseHost, string folder, string dbName)
        {
            // Connect to the master DB to create the requested database

            databaseHost.Database = new PetaPoco.Database(@"Data Source=.\SQLEXPRESS; Integrated security = SSPI; database = master", "System.Data.SqlClient");
            databaseHost.Database.OpenSharedConnection();
            string filename = System.IO.Path.Combine(folder, dbName);
            databaseHost.Database.Execute("CREATE DATABASE " + dbName + " ON PRIMARY (Name=" + dbName + ", filename = \"" + filename + ".mdf\")log on (name=" + dbName + "_log, filename=\"" + filename + ".ldf\")");
            databaseHost.Database.CloseSharedConnection();

            // Connect to the new database

            databaseHost.Database = new PetaPoco.Database(@"Data Source=.\SQLEXPRESS; Integrated security = SSPI; database = " + dbName, "System.Data.SqlClient");
            databaseHost.Database.OpenSharedConnection();

            databaseHost.IConfigurationStore = new DBStore.SQLServerConfigurationStore(databaseHost.Database);
            databaseHost.ISpeciesStore = new DBStore.SQLServerSpeciesStore(databaseHost.Database);
            databaseHost.IImagePathsStore = new DBStore.SQLServerImagePathsStore(databaseHost.Database);
            databaseHost.IImageStore = new DBStore.SQLServerImageStore(databaseHost.Database);

            databaseHost.IConfigurationStore.CreateTable();
            databaseHost.IConfigurationStore.Initialise();
            databaseHost.ISpeciesStore.CreateTable();
            databaseHost.IImagePathsStore.CreateTable();
            databaseHost.IImageStore.CreateTable();
        }

        public static void OpenDatabase(IDatabaseHost databaseHost, string filepath)
        {
            string dbName = System.IO.Path.GetFileNameWithoutExtension(filepath);
            databaseHost.Database = new PetaPoco.Database(@"Data Source=.\SQLEXPRESS; Integrated security = SSPI; database = " + dbName, "System.Data.SqlClient");

            databaseHost.Database.OpenSharedConnection();
            databaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(databaseHost.Database);
            databaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(databaseHost.Database);
            databaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(databaseHost.Database);
            databaseHost.IImageStore = new DBStore.SQLiteImageStore(databaseHost.Database);
        }
    }
}
