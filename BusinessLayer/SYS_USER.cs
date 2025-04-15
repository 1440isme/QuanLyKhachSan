using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class SYS_USER
    {
        Entities db;
        public SYS_USER()
        {
            db = Entities.CreateEntities();
        }

        public tb_SYS_USER getItem(int idUser)
        {
            return db.tb_SYS_USER.FirstOrDefault(x => x.IDUSER == idUser);
        }

        public tb_SYS_USER getItem(string username, string macty, string madvi)
        {
            return db.tb_SYS_USER.FirstOrDefault(x => x.USERNAME == username && x.MACTY == macty && x.MADVI == madvi);
        }

        public List<tb_SYS_USER> getAll()
        {
            return db.tb_SYS_USER.ToList();
        }

        public List<tb_SYS_USER> getUserByDVI(string macty, string madvi)
        {
            return db.tb_SYS_USER.Where(x => x.MACTY == macty && x.MADVI == madvi).ToList();
        }

        public List<tb_SYS_USER> getUserByDViFunc(string macty, string madvi)
        {
            return db.tb_SYS_USER.Where(x => x.MACTY == macty && x.MADVI == madvi && x.DISABLED == false).OrderByDescending(x => x.ISGROUP).ToList();
        }

        public bool checkUserExist(string macty, string madvi, string username)
        {
            var user = db.tb_SYS_USER.FirstOrDefault(x => x.MACTY == macty && x.MADVI == madvi && x.USERNAME == username);
            return user != null;
        }

        public tb_SYS_USER add(tb_SYS_USER user)
        {
            // Kiểm tra trùng USERNAME với MACTY và MADVI
            if (checkUserExist(user.MACTY, user.MADVI, user.USERNAME))
            {
                throw new Exception($"Tên đăng nhập '{user.USERNAME}' đã tồn tại cho công ty {user.MACTY} và đơn vị {user.MADVI}.");
            }

            // Kiểm tra MACTY
            if (!string.IsNullOrEmpty(user.MACTY))
            {
                var congTy = db.tb_CongTy.FirstOrDefault(x => x.MACTY == user.MACTY && (x.DISABLE == false || x.DISABLE == null));
                if (congTy == null)
                {
                    throw new Exception($"Công ty với mã {user.MACTY} không tồn tại hoặc đã bị vô hiệu hóa.");
                }
            }

            //// Kiểm tra MADVI
            //if (!string.IsNullOrEmpty(user.MADVI))
            //{
            //    var donVi = db.tb_DonVi.FirstOrDefault(x => x.MADVI == user.MADVI && (x.DISABLE == false || x.DISABLE == null));
            //    if (donVi == null)
            //    {
            //        throw new Exception($"Đơn vị với mã {user.MADVI} không tồn tại hoặc đã bị vô hiệu hóa.");
            //    }
            //}

            try
            {
                // Kiểm tra trùng lặp trong tb_SYS_RIGHT trước khi thêm mới
                var existingRight = db.tb_SYS_RIGHT.FirstOrDefault(x => x.IDUSER == user.IDUSER && x.FUNC_CODE == "CONGTY");
                if (existingRight != null)
                {
                    throw new Exception($"Quyền với IDUSER = {user.IDUSER} và FUNC_CODE = 'CONGTY' đã tồn tại.");
                }

                // Không cần gán IDUSER vì nó tự động tăng
                db.tb_SYS_USER.Add(user);
                db.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                // Hiển thị chi tiết lỗi, bao gồm inner exception
                string errorMessage = "Lỗi thêm mới người dùng: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\nChi tiết: " + ex.InnerException.Message;
                }
                throw new Exception(errorMessage, ex);
            }
        }

        public tb_SYS_USER update(tb_SYS_USER user)
        {
            var _us = db.tb_SYS_USER.FirstOrDefault(x => x.IDUSER == user.IDUSER);
            if (_us == null)
            {
                throw new Exception($"Không tìm thấy người dùng với IDUSER = {user.IDUSER}.");
            }

            // Kiểm tra trùng USERNAME với MACTY và MADVI (trừ chính bản ghi đang cập nhật)
            var existingUser = db.tb_SYS_USER.FirstOrDefault(x => x.MACTY == user.MACTY && x.MADVI == user.MADVI && x.USERNAME == user.USERNAME && x.IDUSER != user.IDUSER);
            if (existingUser != null)
            {
                throw new Exception($"Tên đăng nhập '{user.USERNAME}' đã tồn tại cho công ty {user.MACTY} và đơn vị {user.MADVI}.");
            }

            // Kiểm tra MACTY
            if (!string.IsNullOrEmpty(user.MACTY))
            {
                var congTy = db.tb_CongTy.FirstOrDefault(x => x.MACTY == user.MACTY && (x.DISABLE == false || x.DISABLE == null));
                if (congTy == null)
                {
                    throw new Exception($"Công ty với mã {user.MACTY} không tồn tại hoặc đã bị vô hiệu hóa.");
                }
            }

            // Kiểm tra MADVI
            if (!string.IsNullOrEmpty(user.MADVI))
            {
                var donVi = db.tb_DonVi.FirstOrDefault(x => x.MADVI == user.MADVI && (x.DISABLE == false || x.DISABLE == null));
                if (donVi == null)
                {
                    throw new Exception($"Đơn vị với mã {user.MADVI} không tồn tại hoặc đã bị vô hiệu hóa.");
                }
            }

            try
            {
                _us.USERNAME = user.USERNAME;
                _us.FULLNAME = user.FULLNAME;
                _us.ISGROUP = user.ISGROUP;
                _us.DISABLED = user.DISABLED;
                _us.MACTY = user.MACTY;
                _us.MADVI = user.MADVI;
                _us.PASSWD = user.PASSWD;
                _us.LAST_PWD_CHANGED = DateTime.Now;

                db.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                // Hiển thị chi tiết lỗi, bao gồm inner exception
                string errorMessage = "Lỗi cập nhật người dùng: " + ex.Message;
                if (ex.InnerException != null)
                {
                    errorMessage += "\nChi tiết: " + ex.InnerException.Message;
                }
                throw new Exception(errorMessage, ex);
            }
        }
    }
}