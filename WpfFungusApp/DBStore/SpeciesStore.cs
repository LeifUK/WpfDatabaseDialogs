using System.Collections.Generic;

namespace WpfFungusApp.DBStore
{
    abstract class SpeciesStore : ISpeciesStore
    {
        public SpeciesStore(PetaPoco.Database database)
        {
            _database = database;
            UseTableNameFix = false;
        }

        // Postgres converts the table name in queries to lower case unless enclosed in quotes
        public bool UseTableNameFix { get; protected set; }
        protected readonly PetaPoco.Database _database;

        public abstract void CreateTable();

        public void Insert(DBObject.Species species)
        {
            species.id = System.Convert.ToInt64(_database.Insert("tblFungi", "id", species));
        }

        public void Update(DBObject.Species species)
        {
            _database.Update("tblFungi", "id", species);
        }

        public void Delete(DBObject.Species species)
        {
            _database.Delete("tblFungi", "id", species);
        }

        public IEnumerable<DBObject.Species> Enumerator
        {
            get
            {
                return _database.Query<DBObject.Species>(
                    UseTableNameFix ?
                    "SELECT * FROM \"tblFungi\" ORDER BY species" :
                    "SELECT * FROM tblFungi ORDER BY species"); 

            }
        }
    }
}
