using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DATPHONG_CHITIET
    {
        Entities db;
        public DATPHONG_CHITIET()
        {
            db = Entities.CreateEntities();

        }
        public tb_DatPhong_CT getItem(int _id)
        {
            return db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDPCT == _id);
        }
        public List<tb_DatPhong_CT> getAllByDatPhong(int _idDP)
        {
            return db.tb_DatPhong_CT.Where(x => x.IDDP == _idDP).ToList();
        }
        public tb_DatPhong_CT add(tb_DatPhong_CT _dpct)
        {
            try
            {
                db.tb_DatPhong_CT.Add(_dpct);
                db.SaveChanges();
                return _dpct;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
        public void update(tb_DatPhong_CT _dpct)
        {
            tb_DatPhong_CT dpct = db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDP == _dpct.IDDP);
            dpct.IDDP = _dpct.IDDP;
            dpct.IDPHONG = _dpct.IDPHONG;
            dpct.IDDPCT = _dpct.IDDPCT;
            dpct.NGAY = _dpct.NGAY;
            dpct.DONGIA = _dpct.DONGIA;
            dpct.SONGAYO = _dpct.SONGAYO;
            dpct.THANHTIEN = _dpct.THANHTIEN;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);

            }
        }
        public void delete(int _idDP, int _idPhong)
        {
            tb_DatPhong_CT _dpct = db.tb_DatPhong_CT.FirstOrDefault(p => p.IDDP == _idDP && p.IDPHONG == _idPhong);

            try
            {
                db.tb_DatPhong_CT.Remove(_dpct);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
        public void deleteAll(int _idDP)
        {
            List<tb_DatPhong_CT> lstDPCT = db.tb_DatPhong_CT.Where(x => x.IDDP == _idDP).ToList();
            try
            {
                db.tb_DatPhong_CT.RemoveRange(lstDPCT);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
    }
}
