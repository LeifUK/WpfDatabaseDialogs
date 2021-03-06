﻿namespace WpfFungusApp.DBStore
{
    internal class SQLServerSpeciesStore : SpeciesStore
    {
        public SQLServerSpeciesStore(PetaPoco.Database database) : base(database)
        {

        }

        public override void CreateTable()
        {
            _database.Execute("CREATE TABLE tblFungi ( " +
                "id INTEGER NOT NULL IDENTITY PRIMARY KEY, " +
                "species VARCHAR(1000) NOT NULL, " +
                "synonyms VARCHAR(1000) NULL, " +
                "common_name VARCHAR(1000) NULL, " +
                "fruiting_body VARCHAR(1000) NULL, " +
                "cap VARCHAR(1000) NULL, " +
                "hymenium VARCHAR(1000) NULL, " +
                "gills VARCHAR(1000) NULL, " +
                "pores VARCHAR(1000) NULL, " +
                "spines VARCHAR(1000) NULL, " +
                "stem VARCHAR(1000) NULL, " +
                "flesh VARCHAR(1000) NULL, " +
                "smell VARCHAR(1000) NULL, " +
                "taste VARCHAR(1000) NULL, " +
                "season VARCHAR(1000) NULL, " +
                "distribution VARCHAR(1000) NULL, " +
                "habitat VARCHAR(1000) NULL, " +
                "spore_print VARCHAR(1000) NULL, " +
                "microscopic_features VARCHAR(1000) NULL, " +
                "edibility VARCHAR(1000) NULL, " +
                "notes VARCHAR(1000) NULL);");
        }
    }
}
