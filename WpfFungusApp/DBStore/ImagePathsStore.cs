using System.Collections.Generic;

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
            imagePath.id = (long)(int)_database.Insert("tblImagesDatabase", "id", imagePath);
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
