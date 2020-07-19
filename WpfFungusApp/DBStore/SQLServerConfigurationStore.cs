namespace WpfFungusApp.DBStore
{
    internal class SQLServerConfigurationStore : ConfigurationStore
    {
        public SQLServerConfigurationStore(PetaPoco.Database database) : base(database)
        {
        }

        public override void CreateTable()
        {
            _database.Execute("CREATE TABLE tblConfiguration (name VARCHAR(100) NOT NULL PRIMARY KEY, value VARCHAR(767) NOT NULL);");
        }
    }
}
