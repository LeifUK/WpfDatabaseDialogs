using System.Collections.Generic;

namespace WpfFungusApp.DBStore
{ 
    internal interface IImagePathsStore
    {
        void CreateTable();
        void Update(DBObject.ImagePath imagePath);
        void Insert(DBObject.ImagePath imagePath);
        void Delete(DBObject.ImagePath imagePath);
        IEnumerable<DBObject.ImagePath> Enumerator { get; }
    }
}
