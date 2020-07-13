namespace WpfFungusApp.DBStore
{
    internal class SQLiteImageStore : ImageStore
    {
        public SQLiteImageStore(PetaPoco.Database database) : base(database)
        {
        }
            
        public override void CreateTable() 
        {
            _database.Execute("CREATE TABLE tblImages ( " +
                    "id INTEGER NOT NULL PRIMARY KEY, " +
                    "fungus_id INTEGER NOT NULL, " +
                    "image_database_id INTEGER NULL, " +
                    "filename TEXT NOT NULL, " +
                    "description TEXT NULL, " +
                    "copyright TEXT NULL, " +
                    "display_order INTEGER NULL, " +
                    "FOREIGN KEY(fungus_id) REFERENCES tblFungi(id)," +
                    "FOREIGN KEY(image_database_id) REFERENCES tblImagesDatabase(id));");
        }
    }
}
