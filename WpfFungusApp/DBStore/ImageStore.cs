using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace WpfFungusApp.DBStore
{
    abstract class ImageStore : IImageStore
    {
        public ImageStore(PetaPoco.Database database)
        {
            _database = database;
            UseTableNameFix = false;
        }

        protected readonly PetaPoco.Database _database;
        // Postgres converts the table name in queries to lower case unless enclosed in quotes
        public bool UseTableNameFix { get; protected set; }

        public abstract void CreateTable();

        public bool Exists(DBObject.ImagePath imagePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("SELECT * FROM ");
            stringBuilder.Append(UseTableNameFix ? "\"tblImages\"" : "tblImages");
            stringBuilder.Append(" WHERE image_database_id=");
            stringBuilder.Append(imagePath.id);
            return (_database.Query<DBObject.ImagePath>(stringBuilder.ToString()).Count() > 0);
        }

        public void Insert(DBObject.Image image)
        {
            image.id = System.Convert.ToInt64(_database.Insert("tblImages", "id", image));
        }

        public void Update(DBObject.Image image)
        {
            _database.Update("tblImages", "id", image);
        }

        public void Delete(DBObject.Image image)
        {
            _database.Delete("tblImages", "id", image);
        }

        public List<DBObject.Image> LoadImages(long speciesId, Dictionary<long, string> paths)
        {
            List<DBObject.Image> images = new List<DBObject.Image>();

            string query = UseTableNameFix ?
                "SELECT * FROM \"tblImages\" WHERE fungus_id=@speciesId ORDER BY display_order" :
                "SELECT * FROM tblImages WHERE fungus_id=@speciesId ORDER BY display_order";
            var iterator = _database.Query<DBObject.Image>(query, new { speciesId });
            foreach (var image in iterator)
            {
                if (!string.IsNullOrEmpty(image.filename))
                {
                    if (image.filename[0] == '\\')
                    {
                        image.filename = image.filename.Substring(1);
                    }
                }

                if (paths.ContainsKey(image.image_database_id))
                {
                    image.Path = System.IO.Path.Combine(paths[image.image_database_id], image.filename);
                }
                else
                {
                    image.Path = image.filename;
                }
                images.Add(image);
            }

            return images;
        }
    }
}
