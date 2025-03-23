using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSan
{
    public partial class frmPhong : DevExpress.XtraEditors.XtraForm
    {
        public event EventHandler DataChanged;

        public frmPhong()
        {
            InitializeComponent();
        }
        PHONG _phong;
        LOAIPHONG _loaiphong;
        TANG _tang;
        bool _them;
        int _maphong;
        private void frmPhong_Load(object sender, EventArgs e)
        {
            _phong = new PHONG();
            _loaiphong = new LOAIPHONG();
            _tang = new TANG();
            LoadLoaiPhong();
            LoadTang();
            LoadData();
            showHideControl(true);
            _enable(false);

            cboTang.SelectedIndexChanged += cboTang_SelectedIndexChanged;
            cboLoaiPhong.SelectedIndexChanged += cboLoaiPhong_SelectedIndexChanged;

        }
        private void cboLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPhongByLoaiPhong();
        }

        private void cboTang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadPhongByTang();
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

            chkThue.Enabled = t;
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtTen.Text = "";
            chkThue.Checked = false;
            chkDisabled.Checked = false;
        }
        void LoadTang()
        {
            cboTang.SelectedIndexChanged -= cboTang_SelectedIndexChanged;
            cboTang.DataSource = _tang.getAll();
            cboTang.DisplayMember = "TENTANG";
            cboTang.ValueMember = "IDTANG";
            cboTang.SelectedIndexChanged += cboTang_SelectedIndexChanged;
        }
        void LoadLoaiPhong()
        {
            cboLoaiPhong.SelectedIndexChanged -= cboLoaiPhong_SelectedIndexChanged;
            cboLoaiPhong.DataSource = _loaiphong.getAll();
            cboLoaiPhong.DisplayMember = "TENLOAIPHONG";
            cboLoaiPhong.ValueMember = "IDLOAIPHONG";
            cboLoaiPhong.SelectedIndexChanged += cboLoaiPhong_SelectedIndexChanged;
        }
        void LoadData()
        {
            gcDanhSach.DataSource = _phong.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;

            // Add unbound columns for displaying names
            if (!gvDanhSach.Columns.Contains(gvDanhSach.Columns["TENTANG"]))
            {
                var colTenTang = gvDanhSach.Columns.AddField("TENTANG");
                colTenTang.UnboundType = DevExpress.Data.UnboundColumnType.String;
                colTenTang.Caption = "TẦNG";
                colTenTang.Visible = true;
            }

            if (!gvDanhSach.Columns.Contains(gvDanhSach.Columns["TENLOAIPHONG"]))
            {
                var colTenLoaiPhong = gvDanhSach.Columns.AddField("TENLOAIPHONG");
                colTenLoaiPhong.UnboundType = DevExpress.Data.UnboundColumnType.String;
                colTenLoaiPhong.Caption = "LOẠI PHÒNG";
                colTenLoaiPhong.Visible = true;
            }

            gvDanhSach.CustomUnboundColumnData += gvDanhSach_CustomUnboundColumnData;
        }
        void loadPhongByTang()
        {
            if (cboTang.SelectedValue != null && int.TryParse(cboTang.SelectedValue.ToString(), out int idTang))
            {
                gcDanhSach.DataSource = _phong.getByTang(idTang);
            }
            else
            {
                MessageBox.Show("Giá trị tầng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void loadPhongByLoaiPhong()
        {
            if (cboLoaiPhong.SelectedValue != null && int.TryParse(cboLoaiPhong.SelectedValue.ToString(), out int idLoaiPhong))
            {
                gcDanhSach.DataSource = _phong.getByLoaiPhong(idLoaiPhong);
            }
            else
            {
                MessageBox.Show("Giá trị loại phòng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _phong.delete(_maphong);
            }
            LoadData();

            // Gọi sự kiện DataChanged
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboLoaiPhong.SelectedValue == null || cboTang.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng và tầng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (_them)
                {
                    tb_Phong phong = new tb_Phong();
                    phong.IDLOAIPHONG = int.Parse(cboLoaiPhong.SelectedValue.ToString());
                    phong.IDTANG = int.Parse(cboTang.SelectedValue.ToString());
                    phong.TENPHONG = txtTen.Text;
                    phong.TRANGTHAI = chkThue.Checked;
                    phong.DISABLED = chkDisabled.Checked;
                    _phong.add(phong);
                }
                else
                {
                    if (_maphong == 0)
                    {
                        MessageBox.Show("Vui lòng chọn một phòng để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    tb_Phong phong = _phong.getItem(_maphong);
                    if (phong != null)
                    {
                        phong.IDLOAIPHONG = int.Parse(cboLoaiPhong.SelectedValue.ToString());
                        phong.IDTANG = int.Parse(cboTang.SelectedValue.ToString());
                        phong.TENPHONG = txtTen.Text;
                        phong.TRANGTHAI = chkThue.Checked;
                        phong.DISABLED = chkDisabled.Checked;
                        _phong.update(phong);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phòng để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var maphongValue = gvDanhSach.GetFocusedRowCellValue("IDPHONG");
                var idLoaiPhongValue = gvDanhSach.GetFocusedRowCellValue("IDLOAIPHONG");
                var idTangValue = gvDanhSach.GetFocusedRowCellValue("IDTANG");
                var tenValue = gvDanhSach.GetFocusedRowCellValue("TENPHONG");
                var trangThaiValue = gvDanhSach.GetFocusedRowCellValue("TRANGTHAI");
                var disabledValue = gvDanhSach.GetFocusedRowCellValue("DISABLED");

                if (maphongValue != null)
                {
                    _maphong = Convert.ToInt32(maphongValue);
                }

                if (idLoaiPhongValue != null)
                {
                    cboLoaiPhong.SelectedValue = idLoaiPhongValue;
                }

                if (idTangValue != null)
                {
                    cboTang.SelectedValue = idTangValue;
                }

                if (tenValue != null)
                {
                    txtTen.Text = tenValue.ToString();
                }

                if (trangThaiValue != null)
                {
                    chkThue.Checked = bool.Parse(trangThaiValue.ToString());
                }

                if (disabledValue != null)
                {
                    chkDisabled.Checked = bool.Parse(disabledValue.ToString());
                }
            }
        }

        private void gvDanhSach_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                var phong = (tb_Phong)gvDanhSach.GetRow(e.ListSourceRowIndex);
                if (phong != null)
                {
                    if (e.Column.FieldName == "TENTANG")
                    {
                        var tang = _tang.getItem(phong.IDTANG);
                        e.Value = tang != null ? tang.TENTANG : string.Empty;
                    }
                    else if (e.Column.FieldName == "TENLOAIPHONG")
                    {
                        var loaiPhong = _loaiphong.getItem(phong.IDLOAIPHONG);
                        e.Value = loaiPhong != null ? loaiPhong.TENLOAIPHONG : string.Empty;
                    }
                }
            }
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DISABLED" && e.CellValue != null && bool.TryParse(e.CellValue.ToString(), out bool isDisabled) && isDisabled)
            {
                Image img = Properties.Resources._1398917_circle_close_cross_incorrect_invalid_icon1;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
        }
    }
}