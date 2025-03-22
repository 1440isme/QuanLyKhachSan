using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SANPHAM
    {
        Entities db;
        public SANPHAM()
        {
            db = Entities.CreateEntities();
        }
        public List<tb_SanPham> getAll()
        {
            return db.tb_SanPham.ToList();
        }
        public tb_SanPham getItem(int idsp)
        {
            return db.tb_SanPham.FirstOrDefault(p => p.IDSP == idsp);
        }
        public void add(tb_SanPham item)
        {
            try
            {
                db.tb_SanPham.Add(item);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);

            }
        }
        public void update(tb_SanPham item)
        {
            try
            {
                tb_SanPham _sp = db.tb_SanPham.FirstOrDefault(p => p.IDSP == item.IDSP);
                _sp.IDSP = item.IDSP;
                _sp.TENSP = item.TENSP;
                _sp.DISABLED = item.DISABLED;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
        public void delete(int idsp)
        {
            tb_SanPham _sp = db.tb_SanPham.FirstOrDefault(p => p.IDSP == idsp);
            _sp.DISABLED = true;
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
