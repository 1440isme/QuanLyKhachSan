using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_REPORT
    {
        Entities db;
        public SYS_REPORT()
        {
            db =Entities.CreateEntities();
        }
        public tb_SYS_REPORT getItem(int rep_code)
        {
            return db.tb_SYS_REPORT.FirstOrDefault(x => x.REP_CODE == rep_code);
        }
        public List<tb_SYS_REPORT> getList()
        {
            return db.tb_SYS_REPORT.ToList();
        }
        public List<tb_SYS_REPORT> getListByRight(List<tb_SYS_RIGHT_REP> lst)
        {
            List<int> rep = lst.Select(ls => ls.REP_CODE).ToList();
            return db.tb_SYS_REPORT.Where(x => rep.Contains(x.REP_CODE)).OrderBy(x=>x.REP_CODE).ToList();
        }
    }
}
