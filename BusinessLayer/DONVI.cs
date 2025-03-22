using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DONVI
    {
        Entities db;
        public DONVI()
        {
            db = Entities.CreateEntities();
        }
        public tb_DonVi getItem(string madvi)
        {
            return db.tb_DonVi.FirstOrDefault(p => p.MADVI == madvi);
        }
        public List<tb_DonVi> getAll()
        {
            return db.tb_DonVi.ToList();
        }
        public List<tb_DonVi> getAll(string macty)
        {
            return db.tb_DonVi.Where(p => p.MACTY == macty).ToList();
        }
        public void add(tb_DonVi dvi)
        {
            try
            {
                db.tb_DonVi.Add(dvi);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
        public void update(tb_DonVi dvi)
        {
            try
            {
                tb_DonVi _dvi = db.tb_DonVi.FirstOrDefault(p => p.MADVI == dvi.MADVI);
                _dvi.TENDVI = dvi.TENDVI;
                _dvi.DIENTHOAI = dvi.DIENTHOAI;
                _dvi.FAX = dvi.FAX;
                _dvi.EMAIL = dvi.EMAIL;
                _dvi.DIACHI = dvi.DIACHI;
                _dvi.MACTY = dvi.MACTY;
                _dvi.DISABLE = dvi.DISABLE;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
        public void delete(string madvi)
        {            
            tb_DonVi _dvi = db.tb_DonVi.FirstOrDefault(p => p.MADVI == madvi);
            _dvi.DISABLE = true;

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
