using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSan
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        SYS_PARAM _sysParam;
        SYS_USER _sysUser;
        private void frmLogin_Load(object sender, EventArgs e)
        {
            _sysUser = new SYS_USER();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open("sysparam.ini", FileMode.Open, FileAccess.Read);
            _sysParam = (SYS_PARAM)bf.Deserialize(fs);
            fs.Close();
            myFunctions._macty = _sysParam.macty;
            myFunctions._madvi = _sysParam.madvi;

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim()=="")
            {
                XtraMessageBox.Show("Tên đăng nhập không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            bool us = _sysUser.checkUserExist(_sysParam.macty, _sysParam.madvi, txtUsername.Text.Trim());
            if (!us)
            {
                XtraMessageBox.Show("Tên đăng nhập không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string pass = Encryptor.Encrypt(txtPassword.Text.Trim(), "qwert@123!poiuy", true);
            tb_SYS_USER user = _sysUser.getItem(txtUsername.Text.Trim(),_sysParam.macty, _sysParam.madvi );
            if (user.PASSWD.Equals(pass))
            {
                frmMain frm = new frmMain(user);
                frm.ShowDialog();
                this.Hide();

            }
            else
            {
                XtraMessageBox.Show("Mật khẩu đăng nhập không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}