namespace WpfFungusApp.DBStore
{
    class MySQLImagePathsStore : ImagePathsStore, IImagePathsStore
    {
        public MySQLImagePathsStore(PetaPoco.Database database) : base(database)
        {

        }

        public override void CreateTable()
        {
            _database.Execute("CREATE TABLE tblImagesDatabase (id INTEGER IDENTITY PRIMARY KEY, path VARCHAR(255) UNIQUE);");
        }
    }
}
