using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class KHACHHANG
    {
        Entities db;
        public KHACHHANG()
        {
            db = Entities.CreateEntities();
        }
        public tb_KhachHang getItem(int idkh)
        {
            return db.tb_KhachHang.FirstOrDefault(p => p.IDKH == idkh);
        }
        public List<tb_KhachHang> getAll()
        {
            return db.tb_KhachHang.ToList();
        }
        public void add(tb_KhachHang item)
        {
            try
            {
                db.tb_KhachHang.Add(item);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);

            }
        }
        public void update(tb_KhachHang item)
        {
            try
            {
                tb_KhachHang _khachhang = db.tb_KhachHang.FirstOrDefault(p => p.IDKH == item.IDKH);
                _khachhang.HOTEN = item.HOTEN;
                _khachhang.GIOITINH = item.GIOITINH;
                _khachhang.CCCD = item.CCCD;
                _khachhang.DIENTHOAI = item.DIENTHOAI;
                _khachhang.EMAIL = item.EMAIL;
                _khachhang.DIACHI = item.DIACHI;
                _khachhang.DISABLED = item.DISABLED;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
        public void delete(int idkh)
        {
            tb_KhachHang _khachhang = db.tb_KhachHang.FirstOrDefault(p => p.IDKH == idkh);
            _khachhang.DISABLED = true;
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
