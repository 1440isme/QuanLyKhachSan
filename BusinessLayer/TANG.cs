using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TANG
    {
        Entities db;

        public TANG()
        {
            db = Entities.CreateEntities();
        }
        public List<tb_Tang> getAll()
        {
            return db.tb_Tang.ToList();
        }
    }
}
