using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_RIGHT
    {
        Entities db;
        public SYS_RIGHT()
        {
            db = Entities.CreateEntities();
        }
        public tb_SYS_RIGHT getRight(int idUser, string idFunc)
        {
            return db.tb_SYS_RIGHT.FirstOrDefault(x => x.IDUSER == idUser && x.FUNC_CODE == idFunc);
        }
        public void update (int idUser, string idFunc, int Right)
        {
            tb_SYS_RIGHT sRight = db.tb_SYS_RIGHT.FirstOrDefault(x => x.IDUSER == idUser && x.FUNC_CODE == idFunc);
            try
            {
                sRight.USER_RIGHT = Right;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Lỗi: " + ex.Message);
            }
        }
    }
}
