using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFungusApp.DBStore
{
    class SQLiteImagePathsStore : ImagePathsStore, IImagePathsStore
    {
        public SQLiteImagePathsStore(PetaPoco.Database database) : base(database)
        {

        }

        public override void CreateTable()
        {
            _database.Execute("CREATE TABLE tblImagesDatabase (id INTEGER PRIMARY KEY, path TEXT UNIQUE);");
        }
    }
}
