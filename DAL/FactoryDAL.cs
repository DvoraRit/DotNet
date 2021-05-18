using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDAL
    {
        static IDAL dal = null;
        public static IDAL GetDAL()
        {
            if (dal == null)
                dal =  new DAL_XML();
            return dal;
        }
    }
}
