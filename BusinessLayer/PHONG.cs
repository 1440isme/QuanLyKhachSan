using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PHONG
    {
        Entities db;
        public PHONG() 
        {
            db = Entities.CreateEntities();        
        }
        public tb_Phong getItem(int maphong)
        {
            return db.tb_Phong.FirstOrDefault(p => p.IDPHONG == maphong);
        }
        public List<tb_Phong> getAll()
        {
            return db.tb_Phong.ToList();
        }
        public List<tb_Phong> getByTang(int idTang)
        {
            return db.tb_Phong.Where(p => p.IDTANG == idTang).ToList();
        }
        public List<tb_Phong> getByLoaiPhong(int idLoaiPhong)
        {
            return db.tb_Phong.Where(p => p.IDLOAIPHONG == idLoaiPhong).ToList();
        }
        public void add(tb_Phong phong)
        {
            try
            {
                db.tb_Phong.Add(phong);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
        public void update(tb_Phong phong)
        {
            try
            {
                tb_Phong _phong = db.tb_Phong.FirstOrDefault(p => p.IDPHONG == phong.IDPHONG);
                _phong.TENPHONG = phong.TENPHONG;
                _phong.TRANGTHAI = phong.TRANGTHAI;
                _phong.IDTANG = phong.IDTANG;
                _phong.IDLOAIPHONG = phong.IDLOAIPHONG;
                _phong.DISABLED = phong.DISABLED;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
        public void delete(int maphong)
        {
            tb_Phong _phong = db.tb_Phong.FirstOrDefault(p => p.IDPHONG == maphong);
            _phong.DISABLED = true;

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
