using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFungusApp.DBStore
{
    internal class SQLiteStore
    {
        public long LastId(PetaPoco.Database database)
        {
            return database.Query<Int64>("select last_insert_rowid()").First();
        }
    }
}
