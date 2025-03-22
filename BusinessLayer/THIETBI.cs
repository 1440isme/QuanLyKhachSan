using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class THIETBI
    {
        Entities db;
        public THIETBI()
        {
            db = Entities.CreateEntities();
        }
        public List<tb_ThietBi> getAll()
        {
            return db.tb_ThietBi.ToList();
        }
        public tb_ThietBi getItem(int idtb)
        {
            return db.tb_ThietBi.FirstOrDefault(p => p.IDTB == idtb);
        }
        public void add(tb_ThietBi item)
        {
            try
            {
                db.tb_ThietBi.Add(item);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);

            }
        }
        public void update(tb_ThietBi item)
        {
            try
            {
                tb_ThietBi _tb = db.tb_ThietBi.FirstOrDefault(p => p.IDTB == item.IDTB);
                _tb.IDTB = item.IDTB;
                _tb.TENTB = item.TENTB;
                _tb.DISABLED = item.DISABLED;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
        public void delete(int idtb)
        {
            tb_ThietBi _tb = db.tb_ThietBi.FirstOrDefault(p => p.IDTB == idtb);
            _tb.DISABLED = true;
            try
            {

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
    }
}
