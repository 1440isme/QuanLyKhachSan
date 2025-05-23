﻿using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PHONG_THIETBI
    {
        Entities db;
        public PHONG_THIETBI()
        {
            db = Entities.CreateEntities();
        }

        public tb_Phong_ThietBi getItem(int idPhong, int idTB)
        {
            return db.tb_Phong_ThietBi.FirstOrDefault(p => p.IDPHONG == idPhong && p.IDTB == idTB);
        }

        public List<tb_Phong_ThietBi> getAll()
        {
            return db.tb_Phong_ThietBi.ToList();
        }

        public List<tb_Phong_ThietBi> getByPhong(int idPhong)
        {
            return db.tb_Phong_ThietBi.Where(p => p.IDPHONG == idPhong).ToList();
        }

        public List<tb_Phong_ThietBi> getByThietBi(int idThietBi)
        {
            return db.tb_Phong_ThietBi.Where(p => p.IDPHONG == idThietBi).ToList();
        }

        public void add(tb_Phong_ThietBi phong)
        {
            try
            {
                db.tb_Phong_ThietBi.Add(phong);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }

        public void update(tb_Phong_ThietBi phong)
        {
            try
            {
                tb_Phong_ThietBi _phong = db.tb_Phong_ThietBi.FirstOrDefault(p => p.IDPHONG == phong.IDPHONG && p.IDTB == phong.IDTB);
                if (_phong == null)
                {
                    throw new Exception("Không tìm thấy phòng thiết bị để cập nhật.");
                }
                _phong.SOLUONG = phong.SOLUONG;
                _phong.DISABLED = phong.DISABLED;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }

        public void delete(int idPhong, int idTB)
        {
            try
            {
                tb_Phong_ThietBi _phong = db.tb_Phong_ThietBi.FirstOrDefault(p => p.IDPHONG == idPhong && p.IDTB == idTB);
                if (_phong == null)
                {
                    throw new Exception("Không tìm thấy phòng thiết bị để xóa.");
                }
                _phong.DISABLED = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
    }
}
