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
            return db.tb_SYS_USER.Where(x => x.MACTY == macty && x.MADVI == madvi && x.DISABLED == false).OrderByDescending(x=>x.ISGROUP).ToList();
        }
        public bool checkUserExist(string macty, string madvi, string username)
        {
            var user = db.tb_SYS_USER.FirstOrDefault(x => x.MACTY == macty && x.MADVI == madvi && x.USERNAME == username);
            if (user != null)
                return true;
            return false;
        }
        public tb_SYS_USER add(tb_SYS_USER user)
        {
            try
            {
                db.tb_SYS_USER.Add(user);
                db.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm mới người dùng: " + ex.Message);
            }

        }
        public tb_SYS_USER update(tb_SYS_USER user)
        {
            var _us = db.tb_SYS_USER.FirstOrDefault(x => x.IDUSER == user.IDUSER);
            _us.USERNAME = user.USERNAME;
            _us.FULLNAME = user.FULLNAME;
            _us.ISGROUP = user.ISGROUP;
            _us.DISABLED = user.DISABLED;
            _us.MACTY = user.MACTY;
            _us.MADVI = user.MADVI;
            _us.PASSWD = user.PASSWD;
            _us.LAST_PWD_CHANGED = DateTime.Now;

            try
            {
                db.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm mới người dùng: " + ex.Message);
            }

        }

    }
}
