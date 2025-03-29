using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_PARAM
    {
        Entities db;
        public SYS_PARAM()
        {
            db =  Entities.CreateEntities();
        }
        public tb_Param GetParam()
        {
            return db.tb_Param.FirstOrDefault();
        }
    }
}
