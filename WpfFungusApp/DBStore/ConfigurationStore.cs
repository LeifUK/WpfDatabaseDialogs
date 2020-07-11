using System.Linq;

namespace WpfFungusApp.DBStore
{
    class ConfigurationStore : IConfigurationStore
    {
        public ConfigurationStore(PetaPoco.Database database)
        {
            _database = database;
        }

        private readonly PetaPoco.Database _database;

        public void CreateTable()
        {
            _database.Execute("CREATE TABLE tblConfiguration (name TEXT NOT NULL, value TEXT NOT NULL, PRIMARY KEY(name));");
        }

        private void CreateIfNotExists(DBObject.Configuration configuration)
        {
            if (_database.Query<DBObject.Configuration>("SELECT * FROM tblConfiguration WHERE name='" + configuration.Name + "'").Count() == 0)
            {
                _database.Insert("tblConfiguration", "Name", configuration);
            }
        }

        public void Initialise()
        {
            DBObject.Configuration configuration = new DBObject.Configuration();

            _database.BeginTransaction();

            configuration.Name = "copyright";
            configuration.Value = "Joe Bloggs";
            CreateIfNotExists(configuration);

            configuration.Name = "export folder";
            configuration.Value = "c:\\";
            CreateIfNotExists(configuration);

            configuration.Name = "overwrite";
            configuration.Value = "false";
            CreateIfNotExists(configuration);

            _database.CompleteTransaction();
        }

        public string Copyright
        {
            get
            {
                DBObject.Configuration configuration = _database.Query<DBObject.Configuration>("SELECT * FROM tblConfiguration WHERE name='copyright'").First();
                return configuration != null ? configuration.Value : "";
            }
            set
            {
                _database.Update("tblConfiguration", "name", new DBObject.Configuration() { Name = "copyright", Value = value });
            }
        }

        public string ExportFolder
        {
            get
            {
                DBObject.Configuration configuration = _database.Query<DBObject.Configuration>("SELECT * FROM tblConfiguration WHERE name='export folder'").First();
                return configuration != null ? configuration.Value : "";
            }
            set
            {
                _database.Update("tblConfiguration", "name", new DBObject.Configuration() { Name = "export folder", Value = value });
            }
        }

        public bool OverwriteImages
        {
            get
            {
                DBObject.Configuration configuration = _database.Query<DBObject.Configuration>("SELECT * FROM tblConfiguration WHERE name='overwrite'").First();
                return configuration != null ? configuration.Value == "1" : false;
            }
            set
            {
                _database.Update("tblConfiguration", "name", new DBObject.Configuration() { Name = "overwrite", Value = (value ? "1" : "0") });
            }
        }
    }
}
