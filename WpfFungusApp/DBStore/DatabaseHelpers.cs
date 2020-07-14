using System.Collections.Generic;
using System;

namespace WpfFungusApp.DBStore
{
    internal class DatabaseHelpers
    {
        //public static void NewDatabaseView(IDatabaseHost databaseHost, DatabaseType databaseType, string folder, string dbName)
        //{
        //    switch (databaseType)
        //    {
        //        case DatabaseType.SQLite:
        //            string path = System.IO.Path.Combine(folder, dbName + ".sqlite");
        //            databaseHost.Database = new PetaPoco.Database("Data Source=" + path + ";Version=3;", "System.Data.SQLite");
        //            databaseHost.Database.OpenSharedConnection();
        //            databaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(databaseHost.Database);
        //            databaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(databaseHost.Database);
        //            databaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(databaseHost.Database, this);
        //            databaseHost.IImageStore = new DBStore.SQLiteImageStore(databaseHost.Database);
        //            break;
        //        case DatabaseType.MicrosoftSqlServer:
        //            // Connect to the master DB to create the requested database

        //            databaseHost.Database = new PetaPoco.Database(@"Data Source=.\SQLEXPRESS; Integrated security = SSPI; database = master", "System.Data.SqlClient");
        //            databaseHost.Database.OpenSharedConnection();
        //            string filename = System.IO.Path.Combine(folder, dbName);
        //            databaseHost.Database.Execute("CREATE DATABASE " + dbName + " ON PRIMARY (Name=" + dbName + ", filename = \"" + filename + ".mdf\")log on (name=" + dbName + "_log, filename=\"" + filename + ".ldf\")");
        //            databaseHost.Database.CloseSharedConnection();

        //            // Connect to the new database

        //            databaseHost.Database = new PetaPoco.Database(@"Data Source=.\SQLEXPRESS; Integrated security = SSPI; database = " + dbName, "System.Data.SqlClient");
        //            databaseHost.Database.OpenSharedConnection();

        //            databaseHost.IConfigurationStore = new DBStore.SQLServerConfigurationStore(databaseHost.Database);
        //            databaseHost.ISpeciesStore = new DBStore.SQLServerSpeciesStore(databaseHost.Database);
        //            databaseHost.IImagePathsStore = new DBStore.SQLServerImagePathsStore(databaseHost.Database);
        //            databaseHost.IImageStore = new DBStore.SQLServerImageStore(databaseHost.Database);
        //            break;
        //        default:
        //            throw new Exception("Unsupported database type");
        //    }

        //    databaseHost.IConfigurationStore.CreateTable();
        //    databaseHost.IConfigurationStore.Initialise();
        //    databaseHost.ISpeciesStore.CreateTable();
        //    databaseHost.IImagePathsStore.CreateTable();
        //    databaseHost.IImageStore.CreateTable();
        //}

        //public static void OpenDatabase(IDatabaseHost databaseHost, DatabaseType databaseType, string filepath)
        //{
        //    switch (databaseType)
        //    {
        //        case DatabaseType.SQLite:
        //            databaseHost.Database = new PetaPoco.Database("Data Source=" + filepath + ";Version=3;", "System.Data.SQLite");
        //            break;
        //        case DatabaseType.MicrosoftSqlServer:
        //            string dbName = System.IO.Path.GetFileNameWithoutExtension(filepath);
        //            databaseHost.Database = new PetaPoco.Database(@"Data Source=.\SQLEXPRESS; Integrated security = SSPI; database = " + dbName, "System.Data.SqlClient");
        //            break;
        //        default:
        //            throw new System.Exception("Unsupported database type");
        //    }

        //    databaseHost.Database.OpenSharedConnection();
        //    databaseHost.IConfigurationStore = new DBStore.SQLiteConfigurationStore(databaseHost.Database);
        //    databaseHost.ISpeciesStore = new DBStore.SQLiteSpeciesStore(databaseHost.Database);
        //    databaseHost.IImagePathsStore = new DBStore.SQLiteImagePathsStore(databaseHost.Database);
        //    databaseHost.IImageStore = new DBStore.SQLiteImageStore(databaseHost.Database);
        //}

        private static Dictionary<long, string> LoadImagePaths(IImagePathsStore iImagePathsStore)
        {
            Dictionary<long, string> paths = new Dictionary<long, string>();

            foreach (DBObject.ImagePath imagePath in iImagePathsStore.Enumerator)
            {
                paths.Add(imagePath.id, imagePath.path);
            }

            return paths;
        }

        public static void LoadImages(IDatabaseHost iDatabaseHost, DBObject.Species species)
        {
            Dictionary<long, string> paths = LoadImagePaths(iDatabaseHost.IImagePathsStore);

            species.Images = new List<DBObject.Image>();

            var iterator = iDatabaseHost.Database.Query<DBObject.Image>("SELECT * FROM tblImages WHERE fungus_id=@id ORDER BY display_order", new { species.id });
            foreach (var image in iterator)
            {
                if (!string.IsNullOrEmpty(image.filename))
                {
                    if (image.filename[0] == '\\')
                    {
                        image.filename = image.filename.Substring(1);
                    }
                }

                if (paths.ContainsKey(image.image_database_id))
                {
                    image.Path = System.IO.Path.Combine(paths[image.image_database_id], image.filename);
                }
                else
                {
                    image.Path = image.filename;
                }
                species.Images.Add(image);
            }
        }

        public static bool ParseImagePath(IImagePathsStore iImagePathsStore, List<DBObject.Image> images)
        {
            Dictionary<long, string> paths = LoadImagePaths(iImagePathsStore);

            foreach (var image in images)
            {
                foreach (KeyValuePair<long, string> keyValuePair in paths)
                {
                    if (image.Path.Contains(keyValuePair.Value))
                    {
                        image.image_database_id = keyValuePair.Key;
                        image.filename = image.Path.Substring(keyValuePair.Value.Length);
                        break;
                    }
                }
            }

            return false;
        }
    }
}
