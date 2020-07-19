namespace WpfFungusApp.DBStore
{
    class SQLServerImagePathsStore : ImagePathsStore, IImagePathsStore
    {
        public SQLServerImagePathsStore(PetaPoco.Database database) : base(database)
        {

        }

        public override void CreateTable()
        {
            _database.Execute("CREATE TABLE tblImagesDatabase (id INTEGER IDENTITY PRIMARY KEY, path VARCHAR(255) UNIQUE);");
        }
    }
}
