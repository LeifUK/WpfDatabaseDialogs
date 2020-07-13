using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfFungusApp.DBStore
{
    internal abstract class ImagePathsStore : IImagePathsStore
    {
        public ImagePathsStore(PetaPoco.Database database)
        {
            _database = database;
        }

        protected readonly PetaPoco.Database _database;

        public abstract void CreateTable();

        public void Update(DBObject.ImagePath imagePath)
        {
            _database.Update("tblImagesDatabase", "id", imagePath);
        }

        public void Insert(DBObject.ImagePath imagePath)
        {
            _database.Insert("tblImagesDatabase", "id", imagePath);
            imagePath.id = _database.Query<Int64>("select last_insert_rowid()").First();
        }

        public void Delete(DBObject.ImagePath imagePath)
        {
            _database.Delete("tblImagesDatabase", "id", imagePath);
        }

        public IEnumerable<DBObject.ImagePath> Enumerator
        {
            get
            {
                return _database.Query<DBObject.ImagePath>("SELECT * FROM tblImagesDatabase");
            }
        }
    }
}
