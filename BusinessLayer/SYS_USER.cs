using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_USER
    {
        Entities db;
        public SYS_USER() 
        {
            db = Entities.CreateEntities();
        }
        public List<tb_SYS_USER> getAll()
        {
            return db.tb_SYS_USER.ToList();
        }
        public List<tb_SYS_USER> getUserByDVI(string macty, string madvi)
        {
            return db.tb_SYS_USER.Where(x => x.MACTY == macty && x.MADVI == madvi).ToList();
        }
    }
}
