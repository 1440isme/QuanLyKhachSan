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
    public partial class frmThietBi : DevExpress.XtraEditors.XtraForm
    {
        public frmThietBi()
        {
            InitializeComponent();
        }
        THIETBI _thietbi;
        bool _them;
        int _idtb;

        private void frmThietBi_Load(object sender, EventArgs e)
        {
            _thietbi = new THIETBI();
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
                gcDanhSach.DataSource = _thietbi.getAll();
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
            if (_idtb != 0 && MessageBox.Show("Bạn có chắc chắn muốn xóa thiết bị này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _thietbi.delete(_idtb);
                    LoadData();
                    _reset();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (_idtb == 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                MessageBox.Show("Tên thiết bị không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_them)
                {
                    tb_ThietBi thietbi = new tb_ThietBi();
                    thietbi.TENTB = txtTen.Text;
                    thietbi.DONGIA = (double?)numDonGia.Value;
                    thietbi.DISABLED = chkDisabled.Checked;
                    _thietbi.add(thietbi);
                }
                else
                {
                    if (_idtb != 0)
                    {
                        tb_ThietBi thietbi = _thietbi.getItem(_idtb);
                        if (thietbi != null)
                        {
                            thietbi.TENTB = txtTen.Text;
                            thietbi.DONGIA = (double?)numDonGia.Value;
                            thietbi.DISABLED = chkDisabled.Checked;
                            _thietbi.update(thietbi);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy thiết bị để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn một thiết bị để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                _idtb = Convert.ToInt32(gvDanhSach.GetFocusedRowCellValue("IDTB"));

                var ten = gvDanhSach.GetFocusedRowCellValue("TENTB");
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
                Image img = Properties.Resources._1398917_circle_close_cross_incorrect_invalid_icon1;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
        }
    }
}