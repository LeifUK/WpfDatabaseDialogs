namespace WpfFungusApp.DBStore
{
    internal class SQLiteConfigurationStore : ConfigurationStore
    {
        public SQLiteConfigurationStore(PetaPoco.Database database) : base(database)
        {
        }

        public override void CreateTable()
        {
            _database.Execute("CREATE TABLE tblConfiguration (name TEXT NOT NULL, value TEXT NOT NULL, PRIMARY KEY(name));");
        }
    }
}
