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

namespace KhachSan
{
    public partial class frmKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public frmKhachHang()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public frmKhachHang(int right)
        {
            InitializeComponent();
            this._right = right;
        }
        int _right;
        frmDatPhong objDP = (frmDatPhong) Application.OpenForms["frmDatPhong"];
        frmDatPhongDon objDPDon = (frmDatPhongDon)Application.OpenForms["frmDatPhongDon"];
        KHACHHANG _khachhang;
        bool _them;
        int _idkh;
        public string kh_dp;


        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            _khachhang = new KHACHHANG();
            LoadData();
            showHideControl(true);
            _enable(false);
        }
        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }
        void _enable(bool t)
        {
            txtTen.Enabled = t;
            txtCCCD.Enabled = t;
            txtDienThoai.Enabled = t;
            txtDiaChi.Enabled = t;
            txtEmail.Enabled = t;
            chkNam.Enabled = t;
            chkDisabled.Enabled = t;
        }

        void _reset()
        {
            txtTen.Text = "";
            txtCCCD.Text = "";
            txtDienThoai.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            chkNam.Checked = false;
            chkDisabled.Checked = false;
        }

        void LoadData()
        {
            try
            {
                gcDanhSach.DataSource = _khachhang.getAll();
                gvDanhSach.OptionsBehavior.Editable = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                XtraMessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            _them = true;
            showHideControl(false);
            _enable(true);
            _reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                XtraMessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            _them = false;
            _enable(true);
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                XtraMessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            if (_idkh != 0 && MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _khachhang.delete(_idkh);
                    LoadData();
                    _reset(); // Reset form sau khi xóa
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (_idkh == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Tên khách hàng không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_them)
                {
                    tb_KhachHang kh = new tb_KhachHang();
                    kh.HOTEN = txtTen.Text;
                    kh.CCCD = txtCCCD.Text;
                    kh.DIENTHOAI = txtDienThoai.Text;
                    kh.EMAIL = txtEmail.Text;
                    kh.DIACHI = txtDiaChi.Text;
                    kh.GIOITINH = chkNam.Checked;
                    kh.DISABLED = chkDisabled.Checked;
                    _khachhang.add(kh); 
                }
                else
                {
                    if (_idkh != 0)
                    {
                        tb_KhachHang kh = _khachhang.getItem(_idkh);
                        if (kh != null)
                        {
                            kh.HOTEN = txtTen.Text;
                            kh.CCCD = txtCCCD.Text;
                            kh.DIENTHOAI = txtDienThoai.Text;
                            kh.EMAIL = txtEmail.Text;
                            kh.DIACHI = txtDiaChi.Text;
                            kh.GIOITINH = chkNam.Checked;
                            kh.DISABLED = chkDisabled.Checked;
                            _khachhang.update(kh);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy khách hàng để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn một khách hàng để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                _them = false;
                LoadData();
                _enable(false);
                showHideControl(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enable(false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idkh = Convert.ToInt32(gvDanhSach.GetFocusedRowCellValue("IDKH"));

                var ten = gvDanhSach.GetFocusedRowCellValue("HOTEN");
                var dienThoai = gvDanhSach.GetFocusedRowCellValue("DIENTHOAI");
                var cccd = gvDanhSach.GetFocusedRowCellValue("CCCD");
                var email = gvDanhSach.GetFocusedRowCellValue("EMAIL");
                var diaChi = gvDanhSach.GetFocusedRowCellValue("DIACHI");
                var gioiTinh = gvDanhSach.GetFocusedRowCellValue("GIOITINH");
                var disabled = gvDanhSach.GetFocusedRowCellValue("DISABLED");

                txtTen.Text = ten != null ? ten.ToString() : string.Empty;
                txtDienThoai.Text = dienThoai != null ? dienThoai.ToString() : string.Empty;
                txtCCCD.Text = cccd != null ? cccd.ToString() : string.Empty;
                txtEmail.Text = email != null ? email.ToString() : string.Empty;
                txtDiaChi.Text = diaChi != null ? diaChi.ToString() : string.Empty;
                chkNam.Checked = gioiTinh != null && bool.Parse(gioiTinh.ToString());
                chkDisabled.Checked = disabled != null && bool.Parse(disabled.ToString());
            }
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DISABLED" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources.del_icon_32px;
                e.Graphics.DrawImage(img, e.Bounds.X + 7, e.Bounds.Y - 2);
                e.Handled = true;
            }
        }

        private void gvDanhSach_DoubleClick(object sender, EventArgs e)
        {
            if (gvDanhSach.GetFocusedRowCellValue("IDKH") != null)
            {
                if (kh_dp == "DatPhongDon")
                {
                    objDPDon.loadKH();
                    objDPDon.setKH(int.Parse(gvDanhSach.GetFocusedRowCellValue("IDKH").ToString()));
                }
                else
                {
                    objDP.loadKH();
                    objDP.setKH(int.Parse(gvDanhSach.GetFocusedRowCellValue("IDKH").ToString()));
                }    
                
                this.Close();
            }
        }

        private void gcDanhSach_Click(object sender, EventArgs e)
        {

        }
    }
}