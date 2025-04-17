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
        public OBJ_PHONG getItemFull(int id)
        {
            var _p = db.tb_Phong.FirstOrDefault(p => p.IDPHONG == id);
            OBJ_PHONG phong = new OBJ_PHONG();
            phong.IDPHONG = _p.IDPHONG;
            phong.TENPHONG = _p.TENPHONG;

            bool status;
            if (Boolean.TryParse(_p.TRANGTHAI.ToString(), out status))
            {
                phong.STATUS = status;
            }
            else
            {
                // Handle the parsing error
                phong.STATUS = false; // or some default value
            }

            bool disabled;
            if (Boolean.TryParse(_p.DISABLED.ToString(), out disabled))
            {
                phong.DISABLED = disabled;
            }
            else
            {
                // Handle the parsing error
                phong.DISABLED = false; // or some default value
            }

            phong.IDTANG = _p.IDTANG;
            phong.IDLOAIPHONG = _p.IDLOAIPHONG;
            var tang = db.tb_Tang.FirstOrDefault(t => t.IDTANG == _p.IDTANG);
            phong.TENTANG = tang?.TENTANG;
            var lp = db.tb_LoaiPhong.FirstOrDefault(l => l.IDLOAIPHONG == _p.IDLOAIPHONG);
            phong.TENLOAIPHONG = lp?.TENLOAIPHONG;
            phong.DONGIA = double.Parse(lp?.DONGIA.ToString() ?? "0");
            return phong;
        }
        public List<tb_Phong> getAll()
        {
            return db.tb_Phong.ToList();
        }
        public List<OBJ_PHONG> getPhongTrongFull()
        {
            var result = (from p in db.tb_Phong
                          join lp in db.tb_LoaiPhong on p.IDLOAIPHONG equals lp.IDLOAIPHONG
                          where p.TRANGTHAI == false && p.DISABLED == false
                          select new OBJ_PHONG
                          {
                              IDPHONG = p.IDPHONG,
                              TENPHONG = p.TENPHONG,
                              STATUS = p.TRANGTHAI,
                              IDTANG = p.IDTANG,
                              IDLOAIPHONG = p.IDLOAIPHONG,
                              DISABLED = p.DISABLED,
                              DONGIA = lp.DONGIA
                          }).ToList();

            return result;
        }
        public List<tb_Phong> getByTang(int idTang)
        {
            return db.tb_Phong.Where(p => p.IDTANG == idTang && p.DISABLED == false).ToList();
        }

        public List<tb_Phong> getByLoaiPhong(int idLoaiPhong)
        {
            return db.tb_Phong.Where(p => p.IDLOAIPHONG == idLoaiPhong).ToList();
        }
        public void updateStatus(int? maphong, bool status)
        {
            tb_Phong _phong = db.tb_Phong.FirstOrDefault(p => p.IDPHONG == maphong);
            if (_phong == null)
            {
                throw new Exception("Không tìm thấy phòng để cập nhật.");
            }
            _phong.TRANGTHAI = status;
            db.SaveChanges();
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
                if (_phong == null)
                {
                    throw new Exception("Không tìm thấy phòng để cập nhật.");
                }
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
            try
            {
                tb_Phong _phong = db.tb_Phong.FirstOrDefault(p => p.IDPHONG == maphong);
                if (_phong == null)
                {
                    throw new Exception("Không tìm thấy phòng để xóa.");
                }
                _phong.DISABLED = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
        public bool checkExist(int idPhong)
        {
            var _phong = db.tb_Phong.FirstOrDefault(p => p.IDPHONG == idPhong);
            if (_phong != null && _phong.TRANGTHAI == true && _phong.DISABLED == false)
            {
                return true;
            }
            return false;
        }
    }
}
