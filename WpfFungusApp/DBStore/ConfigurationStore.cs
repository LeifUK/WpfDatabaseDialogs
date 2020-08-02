using System.Linq;

namespace WpfFungusApp.DBStore
{
    abstract class ConfigurationStore : IConfigurationStore
    {
        public ConfigurationStore(PetaPoco.Database database)
        {
            _database = database;
        }

        protected readonly PetaPoco.Database _database;

        public abstract void CreateTable();

        private void CreateIfNotExists(DBObject.Configuration configuration)
        {
            if (_database.Query<DBObject.Configuration>("SELECT * FROM \"tblConfiguration\" WHERE name='" + configuration.name + "'").Count() == 0)
            {
                _database.Insert("tblConfiguration", "name", configuration);
            }
        }

        public void Initialise()
        {
            DBObject.Configuration configuration = new DBObject.Configuration();

            _database.BeginTransaction();

            configuration.name = "copyright";
            configuration.value = "Joe Bloggs";
            CreateIfNotExists(configuration);

            configuration.name = "export folder";
            configuration.value = "c:\\";
            CreateIfNotExists(configuration);

            configuration.name = "overwrite";
            configuration.value = "false";
            CreateIfNotExists(configuration);

            _database.CompleteTransaction();
        }

        public string Copyright
        {
            get
            {
                DBObject.Configuration configuration = _database.Query<DBObject.Configuration>("SELECT * FROM \"tblConfiguration\" WHERE name='copyright'").First();
                return configuration != null ? configuration.value : "";
            }
            set
            {
                _database.Update("tblConfiguration", "name", new DBObject.Configuration() { name = "copyright", value = value });
            }
        }

        public string ExportFolder
        {
            get
            {
                DBObject.Configuration configuration = _database.Query<DBObject.Configuration>("SELECT * FROM \"tblConfiguration\" WHERE name='export folder'").First();
                return configuration != null ? configuration.value : "";
            }
            set
            {
                _database.Update("tblConfiguration", "name", new DBObject.Configuration() { name = "export folder", value = value });
            }
        }

        public bool OverwriteImages
        {
            get
            {
                DBObject.Configuration configuration = _database.Query<DBObject.Configuration>("SELECT * FROM \"tblConfiguration\" WHERE name='overwrite'").First();
                return configuration != null ? configuration.value == "1" : false;
            }
            set
            {
                _database.Update("tblConfiguration", "name", new DBObject.Configuration() { name = "overwrite", value = (value ? "1" : "0") });
            }
        }
    }
}
