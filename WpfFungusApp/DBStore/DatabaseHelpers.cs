using System.Collections.Generic;

namespace WpfFungusApp.DBStore
{
    internal class DatabaseHelpers
    {
        private static Dictionary<long, string> LoadImagePaths(IImagePathsStore iImagePathsStore)
        {
            Dictionary<long, string> paths = new Dictionary<long, string>();

            foreach (DBObject.ImagePath imagePath in iImagePathsStore.Enumerator)
            {
                paths.Add(imagePath.id, imagePath.path);
            }

            return paths;
        }

        public static void LoadImages(IDatabaseHost iDatabaseHost, DBObject.Species species)
        {
            Dictionary<long, string> paths = LoadImagePaths(iDatabaseHost.IImagePathsStore);

            species.Images = new List<DBObject.Image>();

            var iterator = iDatabaseHost.Database.Query<DBObject.Image>("SELECT * FROM tblImages WHERE fungus_id=@id ORDER BY display_order", new { species.id });
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
                species.Images.Add(image);
            }
        }

        public static bool ParseImagePath(IImagePathsStore iImagePathsStore, List<DBObject.Image> images)
        {
            Dictionary<long, string> paths = LoadImagePaths(iImagePathsStore);

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
