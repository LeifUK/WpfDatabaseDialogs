using System.Collections.Generic;

namespace WpfFungusApp.DBStore
{
    internal class DatabaseHelpers
    {
        public static bool ParseImagePath(IImagePathsStore iImagePathsStore, List<DBObject.Image> images)
        {
            Dictionary<long, string> paths = iImagePathsStore.LoadImagePaths();

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
