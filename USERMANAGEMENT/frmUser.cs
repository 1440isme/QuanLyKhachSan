using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USERMANAGEMENT
{
    public partial class frmUser : DevExpress.XtraEditors.XtraForm
    {
        public frmUser()
        {
            InitializeComponent();
        }
        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];
        public string _macty;
        public string _madvi;
        public int _idUser;
        public string _username;
        public string _fullname;
        public bool _them;
        SYS_USER _sysuser;
        SYS_GROUP _sysgroup;
        tb_SYS_USER _user;
        VIEW_USER_IN_GROUP _vUserInGroup;
        private void frmUser_Load(object sender, EventArgs e)
        {
            _sysuser = new SYS_USER();
            _sysgroup = new SYS_GROUP();
            if (!_them)
            {
                var user = _sysuser.getItem(_idUser);
                txtUsername.Text = user.USERNAME;
                _macty = user.MACTY;
                _madvi = user.MADVI;
                txtHoTen.Text = user.FULLNAME;
                chkDisabled.Checked = user.DISABLED.Value;
                txtUsername.ReadOnly = true;
                txtPass.Text = Encryptor.Decrypt(user.PASSWD, "qwert@123!poiuy", true);
                txtRePass.Text = Encryptor.Decrypt(user.PASSWD, "qwert@123!poiuy", true);
                loadGroupByUser(_idUser);
            }
            else
            {
                txtUsername.Text = "";
                txtHoTen.Text = "";
                txtPass.Text = "";
                txtRePass.Text = "";
                chkDisabled.Checked = false;
            }
        }
        public void loadGroupByUser(int idUser)
        {
            _vUserInGroup = new VIEW_USER_IN_GROUP();
            gcThanhVien.DataSource = _vUserInGroup.getGroupByUser(_madvi, _macty, idUser);
            gvThanhVien.OptionsBehavior.Editable = false;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "")
            {
                XtraMessageBox.Show("Chưa nhập tên người dùng. Tên người dùng nhập không dấu ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.SelectAll();
                txtUsername.Focus();
                return;

            }
            if (!txtPass.Text.Equals(txtRePass.Text) )
            {
                XtraMessageBox.Show("Mật khẩu không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.SelectAll();
                txtUsername.Focus();
                return;

            }
            saveData();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void saveData()
        {
            if (_them)
            {
                bool checkedUser = _sysuser.checkUserExist(_macty, _madvi, txtUsername.Text.Trim());
                if (checkedUser)
                {
                    XtraMessageBox.Show("Tên người dùng đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.SelectAll();
                    txtUsername.Focus();
                    return;
                }
                _user = new tb_SYS_USER();
                _user.USERNAME = txtUsername.Text.Trim();
                _user.FULLNAME = txtHoTen.Text;
                _user.PASSWD = Encryptor.Encrypt(txtPass.Text.Trim(), "qwert@123!poiuy", true);
                _user.ISGROUP = false;
                _user.DISABLED = false;
                _user.MACTY = _macty;
                _user.MADVI = _madvi;
                _sysuser.add(_user);


            }
            else
            {
                _user = _sysuser.getItem(_idUser);
                _user.FULLNAME = txtHoTen.Text;
                _user.PASSWD = Encryptor.Encrypt(txtPass.Text.Trim(), "qwert@123!poiuy", true);
                _user.ISGROUP = false;
                _user.DISABLED = chkDisabled.Checked;
                _user.MACTY = _macty;
                _user.MADVI = _madvi;
                _sysuser.update(_user);

            }
            objMain.loadUser(_macty, _madvi);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmShowGroup frm = new frmShowGroup();
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm._idUser = _idUser;
            frm.ShowDialog();
        }
        

        private void btnBot_Click(object sender, EventArgs e)
        {
            _sysgroup.delGroup(_idUser,int.Parse(gvThanhVien.GetFocusedRowCellValue("IDUSER").ToString()));
            loadGroupByUser(_idUser);
        }
    }
}