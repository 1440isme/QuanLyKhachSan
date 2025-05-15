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
        public List<tb_Tang> getAllActive()
        {
            return db.tb_Tang.Where(s => s.DISABLED == false).ToList();
        }

        public tb_Tang getItem(int idtang)
        {
            return db.tb_Tang.FirstOrDefault(p => p.IDTANG == idtang);
        }

        public void add(tb_Tang item)
        {
            try
            {
                db.tb_Tang.Add(item);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }

        public void update(tb_Tang item)
        {
            try
            {
                tb_Tang _tang = db.tb_Tang.FirstOrDefault(p => p.IDTANG == item.IDTANG);
                if (_tang == null)
                {
                    throw new Exception("Không tìm thấy tầng để cập nhật.");
                }
                _tang.TENTANG = item.TENTANG;
                _tang.DISABLED = item.DISABLED;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }

        public void delete(int idtang)
        {
            try
            {
                tb_Tang _tang = db.tb_Tang.FirstOrDefault(p => p.IDTANG == idtang);
                if (_tang == null)
                {
                    throw new Exception("Không tìm thấy tầng để xóa.");
                }
                _tang.DISABLED = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
    }
}
