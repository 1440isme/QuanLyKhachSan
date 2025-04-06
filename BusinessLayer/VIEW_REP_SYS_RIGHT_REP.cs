using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class VIEW_REP_SYS_RIGHT_REP
    {
        Entities db;
        public VIEW_REP_SYS_RIGHT_REP()
        {
            db = Entities.CreateEntities();
        }
        public List<V_REP_SYS_RIGHT_REP> getRep()
        {
            return db.V_REP_SYS_RIGHT_REP.ToList();
        }
        public List<V_REP_SYS_RIGHT_REP> getRepByUser(int idUser)
        {
            return db.V_REP_SYS_RIGHT_REP.Where(x => x.IDUSER == idUser).ToList();
        }
    }
}
