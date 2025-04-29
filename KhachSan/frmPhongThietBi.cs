using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
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
    public partial class frmPhongThietBi : DevExpress.XtraEditors.XtraForm
    {
        public frmPhongThietBi()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            gvDanhSach.RowHeight = 25;
            gvDanhSach.Appearance.Row.Font = new Font("Tahoma", 9F); // chỉnh font dòng
            gvDanhSach.Appearance.HeaderPanel.Font = new Font("Tahoma", 9F, FontStyle.Bold); // header
            gvDanhSach.Appearance.Row.Options.UseFont = true;
            gvDanhSach.Appearance.HeaderPanel.Options.UseFont = true;

        }
        PHONG_THIETBI _phongtb;
        THIETBI _thietbi;
        PHONG _phong;
        bool _them;
        int _maphongtb;
        int _idPhongSelected;
        int _idTBSelected;

        private void PhongThietBi_Load(object sender, EventArgs e)
        {
            _phongtb = new PHONG_THIETBI();
            _thietbi = new THIETBI();
            _phong = new PHONG();

            LoadData();
            LoadPhong();
            LoadThietBi();
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
            cboPhong.Enabled = t;
            cboThietBi.Enabled = t;
            numSoLuong.Enabled = t;
        }
        void _reset()
        {

            numSoLuong.Value = 0;
        }
        void LoadPhong()
        {
            cboPhong.DataSource = _phong.getAll();
            cboPhong.DisplayMember = "TENPHONG";
            cboPhong.ValueMember = "IDPHONG";
        }
        void LoadThietBi()
        {
            cboThietBi.DataSource = _thietbi.getAll();
            cboThietBi.DisplayMember = "TENTB";
            cboThietBi.ValueMember = "IDTB";
        }
        void LoadData()
        {
            var list = _phongtb.getAll();
            gcDanhSach.DataSource = list;
            gvDanhSach.OptionsBehavior.Editable = false;

            gvDanhSach.Columns.Clear(); // Xóa sạch cột cũ để tránh lỗi

            gvDanhSach.Columns.AddField("IDPHONG").Visible = false; // Phải có IDPHONG để xử lý
            gvDanhSach.Columns.AddField("IDTB").Visible = false;    // Phải có IDTB để xử lý
            gvDanhSach.Columns.AddField("SOLUONG").Caption = "SỐ LƯỢNG";
            gvDanhSach.Columns["SOLUONG"].VisibleIndex = 2;

            var colTenPhong = gvDanhSach.Columns.AddField("TENPHONG");
            colTenPhong.UnboundType = DevExpress.Data.UnboundColumnType.String;
            colTenPhong.Caption = "PHÒNG";
            colTenPhong.Visible = true;
            colTenPhong.VisibleIndex = 0;

            var colTenTB = gvDanhSach.Columns.AddField("TENTB");
            colTenTB.UnboundType = DevExpress.Data.UnboundColumnType.String;
            colTenTB.Caption = "THIẾT BỊ";
            colTenTB.Visible = true;
            colTenTB.VisibleIndex = 1;

            gvDanhSach.CustomUnboundColumnData -= gvDanhSach_CustomUnboundColumnData_1;
            gvDanhSach.CustomUnboundColumnData += gvDanhSach_CustomUnboundColumnData_1;
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
            if (MessageBox.Show("Bạn có chắc chắn muốn đánh dấu phòng này là trống không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    tb_Phong_ThietBi phong = _phongtb.getItem(_idPhongSelected, _idTBSelected);
                    if (phong != null && phong.IDTB == _idTBSelected)
                    {
                        phong.SOLUONG = 0;
                        _phongtb.update(phong);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phòng thiết bị để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboPhong.SelectedValue == null || cboThietBi.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại phòng và thiết bị.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (_them)
                {
                    tb_Phong_ThietBi phong = new tb_Phong_ThietBi();
                    phong.IDPHONG = int.Parse(cboPhong.SelectedValue.ToString());
                    phong.IDTB = int.Parse(cboThietBi.SelectedValue.ToString());
                    phong.SOLUONG = (int)numSoLuong.Value;
                    _phongtb.add(phong);
                }
                else
                {
                    if (_idPhongSelected == 0 || _idTBSelected == 0)
                    {
                        MessageBox.Show("Vui lòng chọn một phòng thiết bị để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    tb_Phong_ThietBi phong = _phongtb.getItem(_idPhongSelected, _idTBSelected);
                    if (phong != null)
                    {
                        phong.SOLUONG = (int)numSoLuong.Value;
                        _phongtb.update(phong);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy phòng thiết bị để cập nhật.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
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
                var maphongValue = gvDanhSach.GetFocusedRowCellValue("IDPHONG");
                var idTBValue = gvDanhSach.GetFocusedRowCellValue("IDTB");
                var soluongValue = gvDanhSach.GetFocusedRowCellValue("SOLUONG");

                if (maphongValue != null)
                {
                    _idPhongSelected = Convert.ToInt32(maphongValue);
                    cboPhong.SelectedValue = maphongValue;
                }

                if (idTBValue != null)
                {
                    _idTBSelected = Convert.ToInt32(idTBValue);
                    cboThietBi.SelectedValue = idTBValue;
                }

                if (soluongValue != null)
                {
                    numSoLuong.Value = Convert.ToInt32(soluongValue);
                }
            }
        }

        private void gvDanhSach_CustomUnboundColumnData_1(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                var phongtb = (tb_Phong_ThietBi)gvDanhSach.GetRow(e.ListSourceRowIndex);
                if (phongtb != null)
                {
                    if (e.Column.FieldName == "TENPHONG")
                    {
                        var phong = _phong.getItem(phongtb.IDPHONG);
                        e.Value = phong != null ? phong.TENPHONG : string.Empty;
                    }
                    else if (e.Column.FieldName == "TENTB")
                    {
                        var thietbi = _thietbi.getItem(phongtb.IDTB);
                        e.Value = thietbi != null ? thietbi.TENTB : string.Empty;
                    }
                }
            }
        }
    }
}