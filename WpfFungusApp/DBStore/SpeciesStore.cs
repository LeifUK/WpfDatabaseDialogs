using System.Collections.Generic;

namespace WpfFungusApp.DBStore
{
    class SpeciesStore : ISpeciesStore
    {
        public SpeciesStore(PetaPoco.Database database)
        {
            _database = database;
        }

        private readonly PetaPoco.Database _database;

        public void CreateTable()
        {
            _database.Execute("CREATE TABLE tblFungi ( " +
                "id INTEGER NOT NULL PRIMARY KEY, " +
                "species TEXT NOT NULL, " +
                "synonyms TEXT NULL, " +
                "common_name TEXT NULL, " +
                "fruiting_body TEXT NULL, " +
                "cap TEXT NULL, " +
                "hymenium TEXT NULL, " +
                "gills TEXT NULL, " +
                "pores TEXT NULL, " +
                "spines TEXT NULL, " +
                "stem TEXT NULL, " +
                "flesh TEXT NULL, " +
                "smell TEXT NULL, " +
                "taste TEXT NULL, " +
                "season TEXT NULL, " +
                "distribution TEXT NULL, " +
                "habitat TEXT NULL, " +
                "spore_print TEXT NULL, " +
                "microscopic_features TEXT NULL, " +
                "edibility TEXT NULL, " +
                "notes TEXT NULL);");
        }

        public void Insert(DBObject.Species species)
        {
            _database.Insert("tblFungi", "id", species);
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
                return _database.Query<DBObject.Species>("SELECT * FROM tblFungi ORDER BY species");
            }
        }
    }
}
