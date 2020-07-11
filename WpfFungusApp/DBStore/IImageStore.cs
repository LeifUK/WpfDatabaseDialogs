namespace WpfFungusApp.DBStore
{
    internal interface IImageStore
    {
        void CreateTable();
        bool Exists(DBObject.ImagePath imagePath);
        void Insert(DBObject.Image image);
        void Update(DBObject.Image image);
        void Delete(DBObject.Image image);
    }
}
