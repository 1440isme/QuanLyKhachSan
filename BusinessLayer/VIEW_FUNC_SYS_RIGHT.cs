using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class VIEW_FUNC_SYS_RIGHT
    {
        Entities db;
        public VIEW_FUNC_SYS_RIGHT()
        {
            db = Entities.CreateEntities();
        }
        public List<V_FUNC_SYS_RIGHT> getFuncByUser( int idUser)
        {
            return db.V_FUNC_SYS_RIGHT.Where(x => x.IDUSER == idUser).OrderBy(x=>x.SORT).ToList();
        }
    }
}
