namespace WpfFungusApp.DBStore
{
    internal class SQLiteSpeciesStore : SpeciesStore
    {
        public SQLiteSpeciesStore(PetaPoco.Database database) : base(database)
        {

        }

        public override void CreateTable()
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
    }
}
