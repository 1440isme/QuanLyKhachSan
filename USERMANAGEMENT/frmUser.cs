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

        formMain objMain = (formMain)Application.OpenForms["formMain"];
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
                chkDisabled.Checked = user.DISABLED.HasValue && user.DISABLED.Value;
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
                txtUsername.ReadOnly = false;
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
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    XtraMessageBox.Show("Tên người dùng không được để trống. Tên người dùng nhập không dấu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.SelectAll();
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtHoTen.Text))
                {
                    XtraMessageBox.Show("Họ tên không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoTen.SelectAll();
                    txtHoTen.Focus();
                    return;
                }

                if (_them && string.IsNullOrWhiteSpace(txtPass.Text))
                {
                    XtraMessageBox.Show("Mật khẩu không được để trống khi thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPass.SelectAll();
                    txtPass.Focus();
                    return;
                }

                if (!txtPass.Text.Equals(txtRePass.Text))
                {
                    XtraMessageBox.Show("Mật khẩu không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRePass.SelectAll();
                    txtRePass.Focus();
                    return;
                }

                saveData();
                XtraMessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void saveData()
        {
            if (_them)
            {
                // Kiểm tra trùng USERNAME (đã có trong SYS_USER.add, nhưng để thông báo rõ ràng hơn)
                bool checkedUser = _sysuser.checkUserExist(_macty, _madvi, txtUsername.Text.Trim());
                if (checkedUser)
                {
                    throw new Exception($"Tên người dùng '{txtUsername.Text.Trim()}' đã tồn tại cho công ty {_macty} và đơn vị {_madvi}.");
                }

                _user = new tb_SYS_USER();
                // Không gán IDUSER vì nó tự động tăng
                _user.USERNAME = txtUsername.Text.Trim();
                _user.FULLNAME = txtHoTen.Text.Trim();
                _user.PASSWD = Encryptor.Encrypt(txtPass.Text.Trim(), "qwert@123!poiuy", true);
                _user.ISGROUP = false;
                _user.DISABLED = false;
                _user.MACTY = _macty;
                _user.MADVI = _madvi;
                _user.LAST_PWD_CHANGED = DateTime.Now;
                _sysuser.add(_user);
            }
            else
            {
                _user = _sysuser.getItem(_idUser);
                if (_user == null)
                {
                    throw new Exception($"Không tìm thấy người dùng với IDUSER = {_idUser}.");
                }

                _user.FULLNAME = txtHoTen.Text.Trim();
                if (!string.IsNullOrWhiteSpace(txtPass.Text))
                {
                    _user.PASSWD = Encryptor.Encrypt(txtPass.Text.Trim(), "qwert@123!poiuy", true);
                    _user.LAST_PWD_CHANGED = DateTime.Now;
                }
                _user.DISABLED = chkDisabled.Checked;
                _user.ISGROUP = false;
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
            loadGroupByUser(_idUser); // Cập nhật danh sách nhóm sau khi thêm
        }

        private void btnBot_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvThanhVien.RowCount == 0 || gvThanhVien.FocusedRowHandle < 0)
                {
                    XtraMessageBox.Show("Vui lòng chọn một nhóm để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int groupId = int.Parse(gvThanhVien.GetFocusedRowCellValue("IDUSER").ToString());
                if (XtraMessageBox.Show($"Bạn có chắc chắn muốn xóa người dùng khỏi nhóm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _sysgroup.delGroup(_idUser, groupId);
                    loadGroupByUser(_idUser);
                    XtraMessageBox.Show("Xóa người dùng khỏi nhóm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Lỗi khi xóa người dùng khỏi nhóm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}