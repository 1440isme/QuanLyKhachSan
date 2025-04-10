using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class DATPHONG
    {
        private Entities db;

        public DATPHONG()
        {
            db = Entities.CreateEntities();
        }

        public tb_DatPhong getItem(int id)
        {
            return db.tb_DatPhong.FirstOrDefault(x => x.IDDP == id);
        }

        public List<tb_DatPhong> getAll()
        {
            return db.tb_DatPhong.Where(x => x.DISABLED == false).ToList();
        }

        public List<OBJ_DATPHONG> getAll(DateTime tungay, DateTime denngay, string macty, string madvi)
        {
            var lstDP = db.tb_DatPhong
                .Join(db.tb_KhachHang,
                    dp => dp.IDKH,
                    kh => kh.IDKH,
                    (dp, kh) => new { DatPhong = dp, KhachHang = kh })
                .Where(x => x.DatPhong.NGAYDATPHONG <= denngay && x.DatPhong.NGAYTRAPHONG >= tungay
                         && x.DatPhong.MACTY == macty && x.DatPhong.MADVI == madvi && x.DatPhong.DISABLED == false)
                .Select(x => new OBJ_DATPHONG
                {
                    IDDP = x.DatPhong.IDDP,
                    IDKH = x.DatPhong.IDKH,
                    HOTEN = x.KhachHang != null ? x.KhachHang.HOTEN : "Không xác định",
                    IDUSER = x.DatPhong.IDUSER,
                    NGAYDATPHONG = x.DatPhong.NGAYDATPHONG.HasValue ? x.DatPhong.NGAYDATPHONG.Value : DateTime.MinValue,
                    NGAYTRAPHONG = x.DatPhong.NGAYTRAPHONG.HasValue ? x.DatPhong.NGAYTRAPHONG.Value : DateTime.MinValue,
                    MACTY = x.DatPhong.MACTY,
                    MADVI = x.DatPhong.MADVI,
                    SONGUOIO = x.DatPhong.SONGUOIO.HasValue ? x.DatPhong.SONGUOIO.Value : 0,
                    SOTIEN = x.DatPhong.SOTIEN.HasValue ? x.DatPhong.SOTIEN.Value : 0,
                    STATUS = x.DatPhong.STATUS.HasValue ? x.DatPhong.STATUS.Value : false,
                    THEODOAN = x.DatPhong.THEODOAN.HasValue ? x.DatPhong.THEODOAN.Value : false,
                    DISABLED = x.DatPhong.DISABLED.HasValue ? x.DatPhong.DISABLED.Value : false,
                    GHICHU = x.DatPhong.GHICHU
                })
                .ToList();

            return lstDP;
        }

        public tb_DatPhong add(tb_DatPhong dp)
        {
            // Kiểm tra dữ liệu đầu vào
            if (dp.NGAYDATPHONG > dp.NGAYTRAPHONG)
            {
                throw new Exception("Ngày đặt phòng phải nhỏ hơn hoặc bằng ngày trả phòng.");
            }
            if (dp.SONGUOIO <= 0)
            {
                throw new Exception("Số người ở phải lớn hơn 0.");
            }

            // Kiểm tra khách hàng tồn tại
            var khachHang = db.tb_KhachHang.FirstOrDefault(x => x.IDKH == dp.IDKH && x.DISABLED == false);
            if (khachHang == null)
            {
                throw new Exception("Khách hàng không tồn tại hoặc đã bị vô hiệu hóa.");
            }

            // Kiểm tra công ty và đơn vị
            if (!string.IsNullOrEmpty(dp.MACTY))
            {
                var congTy = db.tb_CongTy.FirstOrDefault(x => x.MACTY == dp.MACTY && x.DISABLE == false);
                if (congTy == null)
                {
                    throw new Exception("Công ty không tồn tại hoặc đã bị vô hiệu hóa.");
                }
            }
            if (!string.IsNullOrEmpty(dp.MADVI))
            {
                var donVi = db.tb_DonVi.FirstOrDefault(x => x.MADVI == dp.MADVI && x.DISABLE == false);
                if (donVi == null)
                {
                    throw new Exception("Đơn vị không tồn tại hoặc đã bị vô hiệu hóa.");
                }
            }

            try
            {
                dp.DISABLED = false;
                dp.CREATED_DATE = DateTime.Now;
                db.tb_DatPhong.Add(dp);
                db.SaveChanges();
                return dp;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi thêm đặt phòng: " + ex.Message);
            }
        }

        public void updateStatus(int idDP, int updateBy)
        {
            var dp = db.tb_DatPhong.FirstOrDefault(x => x.IDDP == idDP);
            if (dp == null)
            {
                throw new Exception("Không tìm thấy đặt phòng với IDDP = " + idDP);
            }
            if (dp.DISABLED == true)
            {
                throw new Exception("Đặt phòng đã bị hủy, không thể cập nhật trạng thái.");
            }

            dp.STATUS = true;
            dp.UPDATE_BY = updateBy; // Sử dụng updateBy được truyền vào
            dp.UPDATE_DATE = DateTime.Now;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi cập nhật trạng thái: " + ex.Message);
            }
        }

        public tb_DatPhong update(tb_DatPhong dp, int updateBy)
        {
            var existing = db.tb_DatPhong.FirstOrDefault(x => x.IDDP == dp.IDDP);
            if (existing == null)
            {
                throw new Exception("Không tìm thấy đặt phòng để cập nhật.");
            }

            // Kiểm tra dữ liệu đầu vào
            if (dp.NGAYDATPHONG > dp.NGAYTRAPHONG)
            {
                throw new Exception("Ngày đặt phòng phải nhỏ hơn hoặc bằng ngày trả phòng.");
            }
            if (dp.SONGUOIO <= 0)
            {
                throw new Exception("Số người ở phải lớn hơn 0.");
            }

            try
            {
                existing.NGAYDATPHONG = dp.NGAYDATPHONG;
                existing.NGAYTRAPHONG = dp.NGAYTRAPHONG;
                existing.SOTIEN = dp.SOTIEN;
                existing.SONGUOIO = dp.SONGUOIO;
                existing.IDUSER = dp.IDUSER;
                existing.MACTY = dp.MACTY;
                existing.MADVI = dp.MADVI;
                existing.STATUS = dp.STATUS;
                existing.THEODOAN = dp.THEODOAN;
                existing.GHICHU = dp.GHICHU;
                existing.UPDATE_BY = updateBy; // Sử dụng updateBy được truyền vào
                existing.UPDATE_DATE = DateTime.Now;
                db.SaveChanges();
                return existing;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi cập nhật đặt phòng: " + ex.Message);
            }
        }

        public void delete(int madp, int updateBy)
        {
            var dp = db.tb_DatPhong.FirstOrDefault(x => x.IDDP == madp);
            if (dp == null)
            {
                throw new Exception("Không tìm thấy đặt phòng với IDDP = " + madp);
            }

            // Cập nhật DISABLED cho các bản ghi liên quan
            var relatedDetails = db.tb_DatPhong_CT.Where(x => x.IDDP == madp).ToList();
            var relatedProducts = db.tb_DatPhong_SanPham.Where(x => x.IDDP == madp).ToList();

            try
            {
                dp.DISABLED = true;
                dp.UPDATE_BY = updateBy; // Sử dụng updateBy được truyền vào
                dp.UPDATE_DATE = DateTime.Now;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi xóa đặt phòng: " + ex.Message);
            }
        }
        public bool IsRoomBooked(int idPhong, DateTime ngayDat, DateTime ngayTra, int? excludeIdDP = null)
        {
            // Kiểm tra các đơn đặt phòng chưa hoàn tất (STATUS = false, DISABLED = false)
            // và thời gian đặt giao nhau với khoảng thời gian (ngayDat, ngayTra)
            var conflictingBooking = db.tb_DatPhong
                .Join(db.tb_DatPhong_CT,
                    dp => dp.IDDP,
                    dpct => dpct.IDDP,
                    (dp, dpct) => new { DatPhong = dp, DatPhongCT = dpct })
                .Where(x => x.DatPhongCT.IDPHONG == idPhong
                         && x.DatPhong.DISABLED == false
                         && x.DatPhong.STATUS == false
                         && (excludeIdDP == null || x.DatPhong.IDDP != excludeIdDP)
                         && x.DatPhong.NGAYDATPHONG <= ngayTra
                         && x.DatPhong.NGAYTRAPHONG >= ngayDat)
                .FirstOrDefault();

            return conflictingBooking != null;
        }
    }
}