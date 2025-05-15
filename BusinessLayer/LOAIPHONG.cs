using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class LOAIPHONG
    {
        Entities db;
        public LOAIPHONG()
        {
            db = Entities.CreateEntities();
        }
        public tb_LoaiPhong getItem(int maloaiphong)
        {
            return db.tb_LoaiPhong.FirstOrDefault(p => p.IDLOAIPHONG == maloaiphong);
        }
        public List<tb_LoaiPhong> getAll()
        {
            return db.tb_LoaiPhong.ToList();
        }
        public List<tb_LoaiPhong> getAllActive()
        {
            return db.tb_LoaiPhong.Where(s => s.DISABLED == false).ToList();
        }
        public void add(tb_LoaiPhong item)
        {
            try
            {
                db.tb_LoaiPhong.Add(item);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);

            }
        }
        public void update(tb_LoaiPhong item)
        {
            try
            {
                tb_LoaiPhong _loaiphong = db.tb_LoaiPhong.FirstOrDefault(p => p.IDLOAIPHONG == item.IDLOAIPHONG);
                _loaiphong.TENLOAIPHONG = item.TENLOAIPHONG;
                _loaiphong.DONGIA = item.DONGIA;
                _loaiphong.SONGUOI = item.SONGUOI;
                _loaiphong.SOGIUONG = item.SOGIUONG;
                _loaiphong.DISABLED = item.DISABLED;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
        public void delete(int maloaiphong)
        {
            tb_LoaiPhong _loaiphong = db.tb_LoaiPhong.FirstOrDefault(p => p.IDLOAIPHONG == maloaiphong);
            _loaiphong.DISABLED = true;
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
