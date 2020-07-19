namespace WpfFungusApp.DBStore
{
    internal class PostgreSQLConfigurationStore : ConfigurationStore
    {
        public PostgreSQLConfigurationStore(PetaPoco.Database database) : base(database)
        {
        }

        public override void CreateTable()
        {
            _database.Execute("CREATE TABLE \"tblConfiguration\" (name VARCHAR(100) PRIMARY KEY, value VARCHAR(767) NOT NULL);");
        }
    }
}
