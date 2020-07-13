using System.Collections.Generic;
using WpfFungusApp.DBStore;

namespace WpfFungusApp.DBStore
{
    internal class DatabaseHelpers
    {
        private static Dictionary<long, string> LoadImagePaths(PetaPoco.Database database, IImagePathsStore iImagePathsStore)
        {
            Dictionary<long, string> paths = new Dictionary<long, string>();

            foreach (DBObject.ImagePath imagePath in iImagePathsStore.Enumerator)
            {
                paths.Add(imagePath.id, imagePath.path);
            }

            return paths;
        }

        public static void LoadImages(PetaPoco.Database database, IImagePathsStore iImagePathsStore, DBObject.Species species)
        {
            Dictionary<long, string> paths = LoadImagePaths(database, iImagePathsStore);

            species.Images = new List<DBObject.Image>(); 
            
            foreach (var image in database.Query<DBObject.Image>("SELECT * FROM tblImages WHERE fungus_id=@id ORDER BY display_order", new { species.id }))
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
                species.Images.Add(image);
            }
        }

        public static bool ParseImagePath(PetaPoco.Database database, IImagePathsStore iImagePathsStore, List<DBObject.Image> images)
        {
            Dictionary<long, string> paths = LoadImagePaths(database, iImagePathsStore);

            foreach (var image in images)
            {
                foreach (KeyValuePair<long, string> keyValuePair in paths)
                {
                    if (image.Path.Contains(keyValuePair.Value))
                    {
                        image.image_database_id = keyValuePair.Key;
                        image.filename = image.Path.Substring(keyValuePair.Value.Length);
                        break;
                    }
                }
            }

            return false;
        }
    }
}
