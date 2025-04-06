using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class VIEW_USER_IN_GROUP
    {
        Entities db;
        public VIEW_USER_IN_GROUP()
        {
            db = Entities.CreateEntities();
        }
        public List<V_USER_IN_GROUP> getUserInGroup(string madvi, string macty, int idGroup)
        {
            return db.V_USER_IN_GROUP.Where(x => x.MADVI == madvi && x.MACTY == macty && x.GROUP == idGroup && x.ISGROUP == false && x.DISABLED == false).ToList();
        }
        public List<tb_SYS_USER> getGroupByUser(string madvi, string macty, int idUser)
        {
            List <tb_SYS_USER> lstGroup = new List<tb_SYS_USER>();
            List <V_USER_IN_GROUP> lst = db.V_USER_IN_GROUP.Where(x => x.MEMBER == idUser && x.MACTY == macty && x.MADVI == madvi).ToList();
            tb_SYS_USER u;
            foreach (var item in lst)
            {
                u = new tb_SYS_USER();
                u = db.tb_SYS_USER.FirstOrDefault(x => x.IDUSER == item.GROUP);
                lstGroup.Add(u);
            }

            return lstGroup;
        }
        public bool checkGroupByUser(int idUser, int idGroup)
        {
            var gr = db.tb_SYS_GROUP.FirstOrDefault(x => x.MEMBER == idUser && x.GROUP == idGroup);
            if (gr != null)
                return true;
            return false;
        }
        public List<tb_SYS_USER> getGroupByDonVi(string madvi, string macty)
        {
           return db.tb_SYS_USER.Where(x => x.MADVI == madvi && x.MACTY == macty && x.ISGROUP == true).ToList();

        }
    }
}
