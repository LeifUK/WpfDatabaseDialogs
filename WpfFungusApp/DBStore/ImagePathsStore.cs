using System.Collections.Generic;

namespace WpfFungusApp.DBStore
{
    internal abstract class ImagePathsStore : IImagePathsStore
    {
        public ImagePathsStore(PetaPoco.Database database)
        {
            _database = database;
            UseTableNameFix = false;
        }

        protected readonly PetaPoco.Database _database;
        // Postgres converts the table name in queries to lower case unless enclosed in quotes
        public bool UseTableNameFix { get; protected set; }

        public abstract void CreateTable();

        public void Update(DBObject.ImagePath imagePath)
        {
            _database.Update("tblImagesDatabase", "id", imagePath);
        }

        public void Insert(DBObject.ImagePath imagePath)
        {
            imagePath.id = System.Convert.ToInt64(_database.Insert("tblImagesDatabase", "id", imagePath));
        }

        public void Delete(DBObject.ImagePath imagePath)
        {
            _database.Delete("tblImagesDatabase", "id", imagePath);
        }

        public IEnumerable<DBObject.ImagePath> Enumerator
        {
            get
            {
                return _database.Query<DBObject.ImagePath>(
                    UseTableNameFix ? "SELECT * FROM \"tblImagesDatabase\"" : "SELECT * FROM tblImagesDatabase");
            }
        }

        public Dictionary<long, string> LoadImagePaths()
        {
            Dictionary<long, string> paths = new Dictionary<long, string>();

            foreach (DBObject.ImagePath imagePath in Enumerator)
            {
                paths.Add(imagePath.id, imagePath.path);
            }

            return paths;
        }
    }
}
