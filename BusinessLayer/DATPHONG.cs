using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DATPHONG
    {
        Entities db;
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
            return db.tb_DatPhong.ToList();
        }
        public List<OBJ_DATPHONG> getAll(DateTime tungay, DateTime denngay,string macty, string madvi)
        {
            var lstDP =  db.tb_DatPhong.Where(x => x.NGAYDATPHONG>=tungay && x.NGAYTRAPHONG <denngay && x.MACTY == macty && x.MADVI == madvi).ToList();
            List<OBJ_DATPHONG> lst = new List<OBJ_DATPHONG>();
            OBJ_DATPHONG dp;
            foreach (var item in lstDP)
            {
                dp = new OBJ_DATPHONG();
                dp.IDDP = item.IDDP;
                dp.IDKH = item.IDKH;
                var kh = db.tb_KhachHang.FirstOrDefault(x => x.IDKH == item.IDKH);
                dp.HOTEN = kh.HOTEN;
                dp.IDUSER = item.IDUSER;
                dp.NGAYDATPHONG = item.NGAYDATPHONG.Value;
                dp.NGAYTRAPHONG = item.NGAYTRAPHONG.Value;
                dp.MACTY = item.MACTY;
                dp.MADVI = item.MADVI;
                dp.SONGUOIO = item.SONGUOIO.Value;
                dp.SOTIEN = item.SOTIEN.Value;
                dp.STATUS = item.STATUS.Value;   
                dp.THEODOAN = item.THEODOAN.Value;
                dp.DISABLED = item.DISABLED.Value;
                dp.GHICHU = item.GHICHU;
                lst.Add(dp);

            }
            return lst;
        }

        public tb_DatPhong add(tb_DatPhong dp)
        {
            db.tb_DatPhong.Add(dp);
            db.SaveChanges();
            return dp;
        }
        public void updateStatus(int idDP)
        {
            tb_DatPhong dp = db.tb_DatPhong.FirstOrDefault(x => x.IDDP == idDP);
            dp.STATUS = true;
            try
            {
                db.SaveChanges();
                
            }
            catch (Exception ex)
            {

                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }

        public tb_DatPhong update(tb_DatPhong dp)
        {
            var existing = db.tb_DatPhong.FirstOrDefault(x => x.IDDP == dp.IDDP);
            if (existing != null)
            {
                db.Entry(existing).CurrentValues.SetValues(dp);
                db.SaveChanges();
            }
            return existing;
        }

        public void delete(int madp)
        {
            var dp = db.tb_DatPhong.FirstOrDefault(x => x.IDDP == madp);
            dp.DISABLED = true;
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
