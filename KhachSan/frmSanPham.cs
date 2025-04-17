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
    public partial class frmSanPham : DevExpress.XtraEditors.XtraForm
    {
        public frmSanPham()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        SANPHAM _sanpham;
        bool _them;
        int _idsp;
        private void frmSanPham_Load(object sender, EventArgs e)
        {
            _sanpham = new SANPHAM();
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
            chkDisabled.Enabled = t;
            numDonGia.Maximum = decimal.MaxValue;
        }

        void _reset()
        {
            txtTen.Text = "";
            numDonGia.Value = 0;
            chkDisabled.Checked = false;
        }

        void LoadData()
        {
            try
            {
                gcDanhSach.DataSource = _sanpham.getAllWithDisabled(); 
                gvDanhSach.OptionsBehavior.Editable = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enable(true);
            _reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enable(true);
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_idsp != 0 && MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _sanpham.delete(_idsp);
                    LoadData();
                    _reset();

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (_idsp == 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Tên sản phẩm không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_them)
                {
                    tb_SanPham sanpham = new tb_SanPham();
                    sanpham.TENSP = txtTen.Text;
                    sanpham.DONGIA = (double?)numDonGia.Value;
                    sanpham.DISABLED = chkDisabled.Checked;
                    _sanpham.add(sanpham);
                }
                else
                {
                    if (_idsp != 0)
                    {
                        tb_SanPham sanpham = _sanpham.getItem(_idsp);
                        if (sanpham != null)
                        {
                            sanpham.TENSP = txtTen.Text;
                            sanpham.DONGIA = (double?)numDonGia.Value;
                            sanpham.DISABLED = chkDisabled.Checked;
                            _sanpham.update(sanpham);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sản phẩm để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn một sản phẩm để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                _idsp = Convert.ToInt32(gvDanhSach.GetFocusedRowCellValue("IDSP"));

                var ten = gvDanhSach.GetFocusedRowCellValue("TENSP");
                var dongia = gvDanhSach.GetFocusedRowCellValue("DONGIA");
                var disabled = gvDanhSach.GetFocusedRowCellValue("DISABLED");

                txtTen.Text = ten != null ? ten.ToString() : string.Empty;
                numDonGia.Value = dongia != null ? decimal.Parse(dongia.ToString()) : 0;
                chkDisabled.Checked = disabled != null && bool.Parse(disabled.ToString());
            }
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DISABLED" && e.CellValue != null && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources.del_icon_32px;
                e.Graphics.DrawImage(img, e.Bounds.X + 12, e.Bounds.Y - 3);
                e.Handled = true;
            }
        }
    }
}