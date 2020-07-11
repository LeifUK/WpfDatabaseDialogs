using System;
using System.Linq;

namespace WpfFungusApp.DBStore
{
    class ImageStore : IImageStore
    {
        public ImageStore(PetaPoco.Database database)
        {
            _database = database;
        }

        private readonly PetaPoco.Database _database;

        public void CreateTable()
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

        public bool Exists(DBObject.ImagePath imagePath)
        {
            return (_database.Query<DBObject.ImagePath>("SELECT * FROM tblImages WHERE image_database_id=" + imagePath.id).Count() > 0);
        }

        public void Insert(DBObject.Image image)
        {
            _database.Insert("tblImages", "id", image);
            image.id = _database.Query<Int64>("select last_insert_rowid()").First();
        }

        public void Update(DBObject.Image image)
        {
            _database.Update("tblImages", "id", image);
        }

        public void Delete(DBObject.Image image)
        {
            _database.Delete("tblImages", "id", image);
        }
    }
}
