using System;
using System.Linq;

namespace WpfFungusApp.DBStore
{
    internal class SQLiteDatabase
    {
        public static void NewDatabaseView(IDatabaseHost databaseHost, string folder, string dbName)
        {
            string path = System.IO.Path.Combine(folder, dbName + ".sqlite");
            databaseHost.Database = new PetaPoco.Database("Data Source=" + path + ";Version=3;", "System.Data.SQLite");
            databaseHost.Database.OpenSharedConnection();
            databaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(databaseHost.Database);
            databaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(databaseHost.Database);
            databaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(databaseHost.Database);
            databaseHost.IImageStore = new DBStore.SQLiteImageStore(databaseHost.Database);

            databaseHost.IConfigurationStore.CreateTable();
            databaseHost.IConfigurationStore.Initialise();
            databaseHost.ISpeciesStore.CreateTable();
            databaseHost.IImagePathsStore.CreateTable();
            databaseHost.IImageStore.CreateTable();
        }

        public static void OpenDatabase(IDatabaseHost databaseHost, string filepath)
        {
            databaseHost.Database = new PetaPoco.Database("Data Source=" + filepath + ";Version=3;", "System.Data.SQLite");

            databaseHost.Database.OpenSharedConnection();
            databaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(databaseHost.Database);
            databaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(databaseHost.Database);
            databaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(databaseHost.Database);
            databaseHost.IImageStore = new DBStore.SQLiteImageStore(databaseHost.Database);
        }
    }
}
