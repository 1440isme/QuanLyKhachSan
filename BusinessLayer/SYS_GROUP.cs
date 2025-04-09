using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_GROUP
    {
        Entities db;
        public SYS_GROUP()
        {
            db = Entities.CreateEntities();
        }
        public tb_SYS_GROUP getGroupByMember(int idUser)
        {
            return db.tb_SYS_GROUP.FirstOrDefault(x => x.MEMBER == idUser);
        }

        public void add(tb_SYS_GROUP group)
        {
            try
            {
                db.tb_SYS_GROUP.Add(group);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm mới nhóm: " + ex.Message);
            }
        }
        public void delGroup(int idUser, int idGroup)
        {
            var gr = db.tb_SYS_GROUP.FirstOrDefault(x => x.MEMBER == idUser && x.GROUP == idGroup);
            if (gr != null)
            {
                db.tb_SYS_GROUP.Remove(gr);

                db.SaveChanges();
            }
        }
    }
}
