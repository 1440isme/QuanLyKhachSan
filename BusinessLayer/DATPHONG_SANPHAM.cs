using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public class DATPHONG_SANPHAM
    {
        private Entities db;

        public DATPHONG_SANPHAM()
        {
            db = Entities.CreateEntities();
        }

        public List<OBJ_DPSP> getAllByDatPhong(int idDP)
        {
            var lst = db.tb_DatPhong_SanPham
                .Where(x => x.IDDP == idDP)
                .Join(db.tb_Phong,
                    dpsp => dpsp.IDPHONG,
                    p => p.IDPHONG,
                    (dpsp, p) => new { DatPhongSanPham = dpsp, Phong = p })
                .Join(db.tb_SanPham,
                    dp => dp.DatPhongSanPham.IDSP,
                    s => s.IDSP,
                    (dp, s) => new OBJ_DPSP
                    {
                        IDDPSP = dp.DatPhongSanPham.IDDPSP,
                        IDDP = dp.DatPhongSanPham.IDDP,
                        IDPHONG = dp.DatPhongSanPham.IDPHONG,
                        TENPHONG = dp.Phong != null ? dp.Phong.TENPHONG : "Không xác định",
                        IDSP = dp.DatPhongSanPham.IDSP,
                        TENSP = s != null ? s.TENSP : "Không xác định",
                        SOLUONG = dp.DatPhongSanPham.SOLUONG.HasValue ? dp.DatPhongSanPham.SOLUONG.Value : 0,
                        DONGIA = dp.DatPhongSanPham.DONGIA.HasValue ? dp.DatPhongSanPham.DONGIA.Value : 0,
                        THANHTIEN = dp.DatPhongSanPham.THANHTIEN.HasValue ? dp.DatPhongSanPham.THANHTIEN.Value : 0
                    })
                .Distinct() // Thêm Distinct để loại bỏ trùng lặp nếu có
                .ToList();

            return lst;
        }

        public List<tb_DatPhong_SanPham> getAllByPhong(int idDP, int idDPCT)
        {
            return db.tb_DatPhong_SanPham
                .Where(x => x.IDDP == idDP && x.IDDPCT == idDPCT )
                .ToList();
        }

        public void update(tb_DatPhong_SanPham dpsp)
        {
            var sp = db.tb_DatPhong_SanPham.FirstOrDefault(x => x.IDDPSP == dpsp.IDDPSP);
            if (sp == null)
            {
                throw new Exception("Không tìm thấy sản phẩm với IDDPSP = " + dpsp.IDDPSP);
            }

            // Kiểm tra dữ liệu đầu vào
            if (dpsp.SOLUONG <= 0)
            {
                throw new Exception("Số lượng phải lớn hơn 0.");
            }
            if (dpsp.THANHTIEN != dpsp.SOLUONG * dpsp.DONGIA)
            {
                throw new Exception("Thành tiền không hợp lệ.");
            }

            try
            {
                sp.SOLUONG = dpsp.SOLUONG;
                sp.DONGIA = dpsp.DONGIA;
                sp.THANHTIEN = dpsp.THANHTIEN;
                sp.NGAY = dpsp.NGAY;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi cập nhật sản phẩm: " + ex.Message);
            }
        }

        public void add(tb_DatPhong_SanPham _dpsp)
        {
            // Kiểm tra dữ liệu đầu vào
            if (_dpsp.SOLUONG <= 0)
            {
                throw new Exception("Số lượng phải lớn hơn 0.");
            }
            if (_dpsp.THANHTIEN != _dpsp.SOLUONG * _dpsp.DONGIA)
            {
                throw new Exception("Thành tiền không hợp lệ.");
            }

            // Kiểm tra đặt phòng và chi tiết đặt phòng
            var datPhong = db.tb_DatPhong.FirstOrDefault(x => x.IDDP == _dpsp.IDDP && x.DISABLED == false);
            if (datPhong == null)
            {
                throw new Exception("Đặt phòng không tồn tại hoặc đã bị vô hiệu hóa.");
            }
            var datPhongCT = db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDPCT == _dpsp.IDDPCT);
            if (datPhongCT == null)
            {
                throw new Exception("Chi tiết đặt phòng không tồn tại hoặc đã bị vô hiệu hóa.");
            }
            var sanPham = db.tb_SanPham.FirstOrDefault(x => x.IDSP == _dpsp.IDSP && x.DISABLED == false);
            if (sanPham == null)
            {
                throw new Exception("Sản phẩm không tồn tại hoặc đã bị vô hiệu hóa.");
            }

            try
            {
                
                db.tb_DatPhong_SanPham.Add(_dpsp);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi thêm sản phẩm: " + ex.Message);
            }
        }

        public void deleteAll(int _idDP)
        {
            var lstSP = db.tb_DatPhong_SanPham.Where(x => x.IDDP == _idDP).ToList();
            if (!lstSP.Any())
            {
                return; // Không có gì để xóa
            }

            try
            {
                db.tb_DatPhong_SanPham.RemoveRange(lstSP); // Xóa toàn bộ bản ghi
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi xóa tất cả sản phẩm: " + ex.Message);
            }
        }

        public void deleteAllByPhong(int _idDP, int _idPhong)
        {
            var lstSP = db.tb_DatPhong_SanPham
                .Where(x => x.IDDP == _idDP && x.IDPHONG == _idPhong)
                .ToList();
            if (!lstSP.Any())
            {
                return; // Không có gì để xóa
            }

            try
            {
                
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi xóa sản phẩm theo phòng: " + ex.Message);
            }
        }
    }
}