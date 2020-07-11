using System.Collections.Generic;

namespace WpfFungusApp.DBStore
{
    interface ISpeciesStore
    {
        void CreateTable();
        void Insert(DBObject.Species species);
        void Update(DBObject.Species species);
        void Delete(DBObject.Species species);
        IEnumerable<DBObject.Species> Enumerator { get; }
    }
}
