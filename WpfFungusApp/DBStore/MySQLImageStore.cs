namespace WpfFungusApp.DBStore
{
    internal class MySQLImageStore : ImageStore
    {
        public MySQLImageStore(PetaPoco.Database database) : base(database)
        {
        }
            
        public override void CreateTable() 
        {
            _database.Execute("CREATE TABLE tblImages ( " +
                    "id INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, " +
                    "fungus_id INTEGER NOT NULL, " +
                    "image_database_id INTEGER NULL, " +
                    "filename VARCHAR(1000) NOT NULL, " +
                    "description VARCHAR(1000) NULL, " +
                    "copyright VARCHAR(1000) NULL, " +
                    "display_order INTEGER NULL, " +
                    "FOREIGN KEY(fungus_id) REFERENCES tblFungi(id)," +
                    "FOREIGN KEY(image_database_id) REFERENCES tblImagesDatabase(id));");
        }
    }
}
