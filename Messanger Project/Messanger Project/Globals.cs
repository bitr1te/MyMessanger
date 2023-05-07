using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messanger_Project
{
    public class Globals
    {
        private static DataSet1 _db;

        public static DataSet1 DB 
        {
            get
            {
                if(_db == null)
                    _db = new DataSet1();
                return _db;
            }
        }
    }
}
