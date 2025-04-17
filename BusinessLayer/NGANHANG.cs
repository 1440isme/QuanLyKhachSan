using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class NGANHANG
    {
        Entities db;
        public NGANHANG()
        {
            db = Entities.CreateEntities();
        }
        public List<tb_ThongTinNganHang> getAll()
        {
            return db.tb_ThongTinNganHang.ToList();
        }
        public tb_ThongTinNganHang getItem(int idnganhang)
        {
            return db.tb_ThongTinNganHang.FirstOrDefault(p => p.ID == idnganhang);
        }
        public void update(tb_ThongTinNganHang item)
        {
            try
            {
                var existing = db.tb_ThongTinNganHang.FirstOrDefault();
                if (existing != null)
                {
                    existing.TenNganHang = item.TenNganHang;
                    existing.SoTaiKhoan = item.SoTaiKhoan;
                    existing.TenTaiKhoan = item.TenTaiKhoan;
                    existing.NoiDung = item.NoiDung;
                }
                else
                {
                    db.tb_ThongTinNganHang.Add(item);
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật thông tin ngân hàng: " + ex.Message);
            }
        }
    }
}
