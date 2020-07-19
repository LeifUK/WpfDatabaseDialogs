namespace WpfFungusApp.DBStore
{
    class PostgreSQLImagePathsStore : ImagePathsStore, IImagePathsStore
    {
        public PostgreSQLImagePathsStore(PetaPoco.Database database) : base(database)
        {

        }

        public override void CreateTable()
        {
            _database.Execute("CREATE TABLE \"tblImagesDatabase\" (id INTEGER GENERATED AS IDENTITY PRIMARY KEY, path VARCHAR(255) UNIQUE);");
        }
    }
}
