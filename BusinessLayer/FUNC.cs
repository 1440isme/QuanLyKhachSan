using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class FUNC
    {
        Entities db;
        public FUNC()
        {
            db = Entities.CreateEntities();
        }
        public List<tb_Func> getParent()
        {
            return db.tb_Func.Where(x => x.ISGROUP == true && x.MENU == true).OrderBy(s => s.SORT).ToList();
        }
        public List<tb_Func> getChild(string parent)
        {
            return db.tb_Func.Where(x => x.ISGROUP == false && x.MENU == true && x.PARENT == parent).OrderBy(s => s.SORT).ToList();
        }
    }
}
