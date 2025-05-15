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
    public partial class frmLoaiPhong : DevExpress.XtraEditors.XtraForm
    {
        public frmLoaiPhong()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public frmLoaiPhong(int right)
        {
            InitializeComponent();
            this._right = right;
        }
        int _right;
        LOAIPHONG _loaiphong;
        bool _them;
        int _idloaiphong;
        private void frmLoaiPhong_Load(object sender, EventArgs e)
        {
            _loaiphong = new LOAIPHONG();
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
            numDonGia.Enabled = t;
            numSoNguoi.Enabled = t;
            numSoGiuong.Enabled = t;
            chkDisabled.Enabled = t;

            // Đặt giới hạn tối đa cho các điều khiển số
            numDonGia.Maximum = decimal.MaxValue;
            numSoNguoi.Maximum = int.MaxValue;
            numSoGiuong.Maximum = int.MaxValue;
        }

        void _reset()
        {
            txtTen.Text = "";
            numDonGia.Value = 0;
            numSoNguoi.Value = 0;
            numSoGiuong.Value = 0;
            chkDisabled.Checked = false;
        }

        void LoadData()
        {
            try
            {
                gcDanhSach.DataSource = _loaiphong.getAll();
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
            if (_idloaiphong != 0 && MessageBox.Show("Bạn có chắc chắn muốn xóa loại phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _loaiphong.delete(_idloaiphong);
                    LoadData();
                    _reset(); // Reset form sau khi xóa
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (_idloaiphong == 0)
            {
                MessageBox.Show("Vui lòng chọn một loại phòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Tên loại phòng không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_them)
                {
                    tb_LoaiPhong lph = new tb_LoaiPhong();
                    lph.TENLOAIPHONG = txtTen.Text;
                    lph.DONGIA = (double?)numDonGia.Value;
                    lph.SONGUOI = (int?)numSoNguoi.Value;
                    lph.SOGIUONG = (int?)numSoGiuong.Value;
                    lph.DISABLED = chkDisabled.Checked;
                    _loaiphong.add(lph); // IDLOAIPHONG sẽ được database tự động tạo
                }
                else
                {
                    if (_idloaiphong != 0)
                    {
                        tb_LoaiPhong lph = _loaiphong.getItem(_idloaiphong);
                        if (lph != null)
                        {
                            lph.TENLOAIPHONG = txtTen.Text;
                            lph.DONGIA = (double?)numDonGia.Value;
                            lph.SONGUOI = (int?)numSoNguoi.Value;
                            lph.SOGIUONG = (int?)numSoGiuong.Value;
                            lph.DISABLED = chkDisabled.Checked;
                            _loaiphong.update(lph);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy loại phòng để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn một loại phòng để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                _idloaiphong = Convert.ToInt32(gvDanhSach.GetFocusedRowCellValue("IDLOAIPHONG")); // Lấy ID từ cột IDLOAIPHONG
                var tenLoaiPhong = gvDanhSach.GetFocusedRowCellValue("TENLOAIPHONG");
                var donGia = gvDanhSach.GetFocusedRowCellValue("DONGIA");
                var soNguoi = gvDanhSach.GetFocusedRowCellValue("SONGUOI");
                var soGiuong = gvDanhSach.GetFocusedRowCellValue("SOGIUONG");
                var disabled = gvDanhSach.GetFocusedRowCellValue("DISABLED");

                if (tenLoaiPhong != null) txtTen.Text = tenLoaiPhong.ToString();
                if (donGia != null) numDonGia.Value = Convert.ToDecimal(donGia);
                if (soNguoi != null) numSoNguoi.Value = Convert.ToInt32(soNguoi);
                if (soGiuong != null) numSoGiuong.Value = Convert.ToInt32(soGiuong);
                if (disabled != null) chkDisabled.Checked = Convert.ToBoolean(disabled);
            }
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DISABLED" && e.CellValue != null && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources.del_icon_32px;
                e.Graphics.DrawImage(img, e.Bounds.X + 7, e.Bounds.Y - 2);
                e.Handled = true;
            }
        }
    }
}