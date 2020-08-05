namespace WpfFungusApp.DBStore
{
    internal class MySQLSpeciesStore : SpeciesStore
    {
        public MySQLSpeciesStore(PetaPoco.Database database) : base(database)
        {

        }

        public override void CreateTable()
        {
            _database.Execute("CREATE TABLE tblFungi ( " +
                "id INTEGER NOT NULL AUTO_INCREMENT PRIMARY KEY, " +
                "species VARCHAR(1000) NOT NULL, " +
                "synonyms VARCHAR(1000) NULL, " +
                "common_name VARCHAR(200) NULL, " +
                "fruiting_body VARCHAR(1000) NULL, " +
                "cap VARCHAR(500) NULL, " +
                "hymenium VARCHAR(500) NULL, " +
                "gills VARCHAR(500) NULL, " +
                "pores VARCHAR(500) NULL, " +
                "spines VARCHAR(500) NULL, " +
                "stem VARCHAR(500) NULL, " +
                "flesh VARCHAR(500) NULL, " +
                "smell VARCHAR(200) NULL, " +
                "taste VARCHAR(200) NULL, " +
                "season VARCHAR(200) NULL, " +
                "distribution VARCHAR(200) NULL, " +
                "habitat VARCHAR(200) NULL, " +
                "spore_print VARCHAR(200) NULL, " +
                "microscopic_features VARCHAR(1000) NULL, " +
                "edibility VARCHAR(1000) NULL, " +
                "notes VARCHAR(1000) NULL);");
        }
    }
}
