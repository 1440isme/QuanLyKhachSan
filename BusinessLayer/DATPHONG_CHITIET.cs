using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class DATPHONG_CHITIET
    {
        private Entities db;

        public DATPHONG_CHITIET()
        {
            db = Entities.CreateEntities();
        }

        public tb_DatPhong_CT getItem(int _iddpct)
        {
            return db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDPCT == _iddpct);
        }

        public tb_DatPhong_CT getItem(int _idDP, int idPhong)
        {
            return db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDP == _idDP && x.IDPHONG == idPhong);
        }

        public List<tb_DatPhong_CT> getAllByDatPhong(int _idDP)
        {
            return db.tb_DatPhong_CT.Where(x => x.IDDP == _idDP).ToList();
        }

        public tb_DatPhong_CT getIDDPByPhong(int _idPhong)
        {
            return db.tb_DatPhong_CT
                .OrderByDescending(x => x.NGAY)
                .FirstOrDefault(x => x.IDPHONG == _idPhong);
        }

        public tb_DatPhong_CT add(tb_DatPhong_CT dpct)
        {
            try
            {
                db.tb_DatPhong_CT.Add(dpct);
                db.SaveChanges();
                return dpct;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm đơn đặt phòng: " + ex.Message);
            }
        }

        public void update(tb_DatPhong_CT _dpct)
        {
            var dpct = db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDP == _dpct.IDDP && x.IDPHONG == _dpct.IDPHONG);
            if (dpct == null)
            {
                throw new Exception("Không tìm thấy chi tiết đặt phòng để cập nhật.");
            }

            // Kiểm tra dữ liệu đầu vào
            if (_dpct.SONGAYO <= 0)
            {
                throw new Exception("Số ngày ở phải lớn hơn 0.");
            }

            // Lấy đơn giá từ tb_LoaiPhong
            var phong = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == _dpct.IDPHONG);
            if (phong == null)
            {
                throw new Exception("Phòng không tồn tại hoặc đã bị vô hiệu hóa.");
            }
            var loaiPhong = db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG == phong.IDLOAIPHONG);
            if (loaiPhong == null)
            {
                throw new Exception("Loại phòng không tồn tại cho phòng này.");
            }
            _dpct.DONGIA = loaiPhong.DONGIA.HasValue ? (int)loaiPhong.DONGIA.Value : 0;
            _dpct.THANHTIEN = _dpct.SONGAYO * _dpct.DONGIA;

            try
            {
                dpct.NGAY = _dpct.NGAY;
                dpct.DONGIA = _dpct.DONGIA;
                dpct.SONGAYO = _dpct.SONGAYO;
                dpct.THANHTIEN = _dpct.THANHTIEN;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi cập nhật chi tiết đặt phòng: " + ex.Message);
            }
        }

        public void delete(int _idDP, int _idPhong)
        {
            var _dpct = db.tb_DatPhong_CT.FirstOrDefault(p => p.IDDP == _idDP && p.IDPHONG == _idPhong);
            if (_dpct == null)
            {
                throw new Exception("Không tìm thấy chi tiết đặt phòng để xóa.");
            }

            try
            {
                db.tb_DatPhong_CT.Remove(_dpct);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi xóa chi tiết đặt phòng: " + ex.Message);
            }
        }

        public void deleteAll(int _idDP)
        {
            var lstDPCT = db.tb_DatPhong_CT.Where(x => x.IDDP == _idDP).ToList();
            if (!lstDPCT.Any())
            {
                return;
            }

            try
            {
                db.tb_DatPhong_CT.RemoveRange(lstDPCT);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi xóa tất cả chi tiết đặt phòng: " + ex.Message);
            }
        }
    }
}