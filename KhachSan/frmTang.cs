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
    public partial class frmTang : DevExpress.XtraEditors.XtraForm
    {
        public event EventHandler DataChanged;

        public frmTang()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public frmTang(int right)
        {
            InitializeComponent();
            this._right = right;
        }
        int _right;
        TANG _tang;
        bool _them;
        int _idtang;

        private void frmTang_Load(object sender, EventArgs e)
        {
            _tang = new TANG();
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
            chkDisabled.Enabled = t;
        }

        void _reset()
        {
            txtTen.Text = "";
            chkDisabled.Checked = false;
        }

        void LoadData()
        {
            try
            {
                gcDanhSach.DataSource = _tang.getAll();
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
            if (_idtang != 0 && MessageBox.Show("Bạn có chắc chắn muốn xóa tầng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _tang.delete(_idtang);
                    LoadData();
                    _reset();

                    DataChanged?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (_idtang == 0)
            {
                MessageBox.Show("Vui lòng chọn một tầng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Tên tầng không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_them)
                {
                    tb_Tang tang = new tb_Tang();
                    tang.TENTANG = txtTen.Text;
                    tang.DISABLED = chkDisabled.Checked;
                    _tang.add(tang);
                }
                else
                {
                    if (_idtang == 0)
                    {
                        MessageBox.Show("Vui lòng chọn một tầng để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    tb_Tang tang = _tang.getItem(_idtang);
                    if (tang != null)
                    {
                        tang.TENTANG = txtTen.Text;
                        tang.DISABLED = chkDisabled.Checked;
                        _tang.update(tang);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy tầng để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                _them = false;
                LoadData();
                _enable(false);
                showHideControl(true);
                DataChanged?.Invoke(this, EventArgs.Empty); // Đảm bảo gọi sự kiện
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
                _idtang = Convert.ToInt32(gvDanhSach.GetFocusedRowCellValue("IDTANG"));

                var ten = gvDanhSach.GetFocusedRowCellValue("TENTANG");
                var disabled = gvDanhSach.GetFocusedRowCellValue("DISABLED");

                txtTen.Text = ten != null ? ten.ToString() : string.Empty;
                chkDisabled.Checked = disabled != null && bool.Parse(disabled.ToString());
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