using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFungusApp.DBStore
{
    class SQLServerImagePathsStore : ImagePathsStore, IImagePathsStore
    {
        public SQLServerImagePathsStore(PetaPoco.Database database) : base(database)
        {

        }

        public override void CreateTable()
        {
            _database.Execute("CREATE TABLE tblImagesDatabase (id INTEGER PRIMARY KEY, path VARCHAR(100) UNIQUE);");
        }
    }
}
