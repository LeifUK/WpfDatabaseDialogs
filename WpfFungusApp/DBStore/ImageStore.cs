using System.Linq;

namespace WpfFungusApp.DBStore
{
    abstract class ImageStore : IImageStore
    {
        public ImageStore(PetaPoco.Database database)
        {
            _database = database;
        }

        protected readonly PetaPoco.Database _database;

        public abstract void CreateTable();

        public bool Exists(DBObject.ImagePath imagePath)
        {
            return (_database.Query<DBObject.ImagePath>("SELECT * FROM \"tblImages\" WHERE image_database_id=" + imagePath.id).Count() > 0);
        }

        public void Insert(DBObject.Image image)
        {
            image.id = (long)(int)_database.Insert("tblImages", "id", image);
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
