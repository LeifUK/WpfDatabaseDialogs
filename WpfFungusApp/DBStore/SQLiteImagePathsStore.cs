namespace WpfFungusApp.DBStore
{
    class SQLiteImagePathsStore : ImagePathsStore, IImagePathsStore
    {
        public SQLiteImagePathsStore(PetaPoco.Database database) : base(database)
        {

        }

        public override void CreateTable()
        {
            _database.Execute("CREATE TABLE tblImagesDatabase (id INTEGER PRIMARY KEY, path TEXT UNIQUE);");
        }
    }
}
