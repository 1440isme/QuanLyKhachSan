using BusinessLayer;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
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
    public partial class frmDatPhongDon : DevExpress.XtraEditors.XtraForm
    {
        public frmDatPhongDon()
        {
            InitializeComponent();
        }
        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];
        public bool _them;
        public int _idPhong;
        int _idDP;
        string _macty;
        string _madvi;
        double _tongtien = 0;
        DATPHONG _datphong;
        DATPHONG_CHITIET _datphongct;
        DATPHONG_SANPHAM _datphongsp;
        OBJ_PHONG _phongHienTai;
        PHONG _phong;
        SANPHAM _sanpham;
        SYS_PARAM _sysparam;
        List<OBJ_DPSP> lstDPSP;
        private void frmDatPhongDon_Load(object sender, EventArgs e)
        {
            _datphong = new DATPHONG();
            _datphongct = new DATPHONG_CHITIET();
            _datphongsp = new DATPHONG_SANPHAM();
            _phong = new PHONG();
            _sanpham = new SANPHAM();
            lstDPSP = new List<OBJ_DPSP>();
            _sysparam = new SYS_PARAM();

            _phongHienTai = _phong.getItemFull(_idPhong);
            lblPhong.Text = _phongHienTai.TENPHONG + " - Đơn giá: "+_phongHienTai.DONGIA.ToString("N0")+ " VNĐ";

            dtNgayDat.Value = DateTime.Now;
            dtNgayTra.Value = DateTime.Now.AddDays(1);

            cboTrangThai.DataSource = TRANGTHAI.getList();
            cboTrangThai.DisplayMember = "_display";
            cboTrangThai.ValueMember = "_value";
            cboTrangThai.SelectedIndex = 1;
            numSoNguoi.Value = 1;
            var _pr = _sysparam.GetParam();
            if (_pr != null)
            {
                _macty = _pr.MACTY;
                _madvi = _pr.MADVI;
            }
            else
            {
                MessageBox.Show("Failed to retrieve system parameters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            loadKH();
            loadSP();
            var dpct = _datphongct.getIDDPByPhong(_idPhong);
            if (!_them && dpct != null)
            {
                _idDP = dpct.IDDP;
                var dp = _datphong.getItem(_idDP);
                searchKH.EditValue = dp.IDKH;
                dtNgayDat.Value = dp.NGAYDATPHONG.Value;
                dtNgayTra.Value = DateTime.Now;
                cboTrangThai.SelectedValue = dp.STATUS;
                numSoNguoi.Value = dp.SONGUOIO.Value;
                txtGhiChu.Text = dp.GHICHU;
                txtThanhTien.Text = dp.SOTIEN.Value.ToString("N0");
            }
            loadSPDV();

        }
        void loadSPDV()
        {
            gcSPDV.DataSource = _datphongsp.getAllByDatPhong(_idDP);
            lstDPSP = _datphongsp.getAllByDatPhong(_idDP);
        }
        void loadSP()
        {
            gcSanPham.DataSource = _sanpham.getAll();
            gvSanPham.OptionsBehavior.Editable = false;
        }
        public void loadKH()
        {
            KHACHHANG _khachhang = new KHACHHANG();
            searchKH.Properties.DataSource = _khachhang.getAll();
            searchKH.Properties.DisplayMember = "HOTEN";
            searchKH.Properties.ValueMember = "IDKH";
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (searchKH.EditValue == null || searchKH.EditValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            saveData();
            _tongtien = (double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + _phong.getItemFull(_idPhong).DONGIA * (dtNgayTra.Value.Day - dtNgayDat.Value.Day));
            var dp = _datphong.getItem(_idDP);
            dp.SOTIEN = _tongtien;
            _datphong.update(dp);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!_them)
            {
                saveData();
                _tongtien = (double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + _phong.getItemFull(_idPhong).DONGIA * (dtNgayTra.Value.Day - dtNgayDat.Value.Day));
                var dp = _datphong.getItem(_idDP);
                dp.SOTIEN = _tongtien;
                _datphong.update(dp);
                _datphong.updateStatus(_idDP);
                _phong.updateStatus(_idPhong, false);
                XuatReport(_idDP.ToString(), "rpDatPhong", "Đơn đặt phòng");
                cboTrangThai.SelectedValue = true;
                objMain.showRoom();
            }    
            
        }
        private void XuatReport(string _khoa, string _rpName, string _rpTitle)
        {
            if (_khoa != null)
            {
                Form frm = new Form();
                CrystalReportViewer crv = new CrystalReportViewer();
                crv.ShowGroupTreeButton = false;
                crv.ShowParameterPanelButton = false;
                crv.ToolPanelView = ToolPanelViewType.None;
                TableLogOnInfo thongtin;
                ReportDocument doc = new ReportDocument();
                doc.Load(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\" + _rpName + ".rpt"));
                thongtin = doc.Database.Tables[0].LogOnInfo;
                thongtin.ConnectionInfo.ServerName = myFunctions._srv;
                thongtin.ConnectionInfo.DatabaseName = myFunctions._db;
                thongtin.ConnectionInfo.UserID = myFunctions._us;
                thongtin.ConnectionInfo.Password = myFunctions._pw;
                doc.Database.Tables[0].ApplyLogOnInfo(thongtin);
                try
                {
                    doc.SetParameterValue("@IDDP", _khoa.ToString());
                    crv.Dock = DockStyle.Fill;
                    crv.ReportSource = doc;
                    frm.Controls.Add(crv);
                    crv.Refresh();
                    frm.Text = _rpTitle;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void setKH(int idKH)
        {
            KHACHHANG _khachhang = new KHACHHANG();
            searchKH.EditValue = idKH;
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmKhachHang _frm = new frmKhachHang();
            _frm.kh_dp = "DatPhongDon";
            _frm.ShowDialog();
        }

        private void gvSanPham_DoubleClick(object sender, EventArgs e)
        {
            if (_idPhong == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (bool.Parse(cboTrangThai.SelectedValue.ToString())== true)
            {
                MessageBox.Show("Phiếu đã hoàn tất không được chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (gvSanPham.GetFocusedRowCellValue("IDSP") != null)
            {
                OBJ_DPSP sp = new OBJ_DPSP();
                sp.IDSP = int.Parse(gvSanPham.GetFocusedRowCellValue("IDSP").ToString());
                sp.TENSP = gvSanPham.GetFocusedRowCellValue("TENSP").ToString();
                sp.IDPHONG = _idPhong;
                sp.TENPHONG = _phongHienTai.TENPHONG;
                sp.DONGIA = float.Parse(gvSanPham.GetFocusedRowCellValue("DONGIA").ToString());
                sp.SOLUONG = 1;
                sp.THANHTIEN = sp.DONGIA * sp.SOLUONG;

                bool found = false;
                foreach (var item in lstDPSP)
                {
                    if (item.IDSP == sp.IDSP && item.IDPHONG == sp.IDPHONG)
                    {
                        item.SOLUONG++;
                        item.THANHTIEN = item.SOLUONG * item.DONGIA;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    lstDPSP.Add(sp);
                }
            }
            loadDPSP();
            txtThanhTien.Text = (double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + _phongHienTai.DONGIA*(dtNgayTra.Value.Day-dtNgayDat.Value.Day)).ToString("N0");

        }
        void loadDPSP()
        {
            List<OBJ_DPSP> lst = new List<OBJ_DPSP>();
            foreach (var item in lstDPSP)
            {
                lst.Add(item);
            }
            gcSPDV.DataSource = lst;
        }

        private void gvSPDV_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (bool.Parse(cboTrangThai.SelectedValue.ToString()) == true)
            {
                MessageBox.Show("Phiếu đã hoàn tất không được chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (e.Column.FieldName == "SOLUONG")
            {
                int sl = int.Parse(e.Value.ToString());
                if (sl != 0)
                {

                    double gia = double.Parse(gvSPDV.GetRowCellValue(gvSPDV.FocusedRowHandle, "DONGIA").ToString());
                    gvSPDV.SetRowCellValue(gvSPDV.FocusedRowHandle, "THANHTIEN", sl * gia);
                }
                else
                {
                    gvSPDV.SetRowCellValue(gvSPDV.FocusedRowHandle, "THANHTIEN", 0);
                }
            }
            gvSPDV.UpdateTotalSummary();
            txtThanhTien.Text = (double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + _phongHienTai.DONGIA * (dtNgayTra.Value.Day - dtNgayDat.Value.Day)).ToString("N0");
        }

        private void gvSPDV_HiddenEditor(object sender, EventArgs e)
        {
            gvSPDV.UpdateCurrentRow();
        }
        void saveData()
        {
            try
            {
                if (_them) // Thêm mới
                {
                    tb_DatPhong dp = new tb_DatPhong();
                    tb_DatPhong_CT dpct;
                    tb_DatPhong_SanPham dpsp;
                    dp.NGAYDATPHONG = dtNgayDat.Value;
                    dp.NGAYTRAPHONG = dtNgayTra.Value;
                    dp.SONGUOIO = int.Parse(numSoNguoi.Value.ToString());
                    dp.STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString());
                    dp.THEODOAN = false;
                    dp.IDKH = int.Parse(searchKH.EditValue.ToString());
                    dp.SOTIEN = double.Parse(txtThanhTien.Text);
                    dp.GHICHU = txtGhiChu.Text;
                    dp.DISABLED = false;
                    dp.IDUSER = 1;
                    dp.MACTY = _macty;
                    dp.MADVI = _madvi;
                    dp.CREATED_DATE = DateTime.Now;
                    var _dp = _datphong.add(dp);
                    _idDP = _dp.IDDP;

                    
                        dpct = new tb_DatPhong_CT();
                        dpct.IDDP = _dp.IDDP;
                        dpct.IDPHONG = _idPhong;
                        dpct.SONGAYO = (dtNgayTra.Value - dtNgayDat.Value).Days;
                        dpct.DONGIA = int.Parse(_phongHienTai.DONGIA.ToString());
                        dpct.THANHTIEN = dpct.DONGIA * dpct.SONGAYO;
                        dpct.NGAY = DateTime.Now;
                        var _dpct = _datphongct.add(dpct);
                        _phong.updateStatus(dpct.IDPHONG, true); // Phòng mới đặt -> trạng thái true
                                                                 // Xử lý sản phẩm/dịch vụ như cũ
                        if (gvSPDV.RowCount > 0)
                        {
                            for (int j = 0; j < gvSPDV.RowCount; j++)
                            {
                                if (dpct.IDPHONG == int.Parse(gvSPDV.GetRowCellValue(j, "IDPHONG").ToString()))
                                {
                                    dpsp = new tb_DatPhong_SanPham();
                                    dpsp.IDDP = _dp.IDDP;
                                    dpsp.IDDPCT = _dpct.IDDPCT;
                                    dpsp.IDPHONG = int.Parse(gvSPDV.GetRowCellValue(j, "IDPHONG").ToString());
                                    dpsp.IDSP = int.Parse(gvSPDV.GetRowCellValue(j, "IDSP").ToString());
                                    dpsp.SOLUONG = int.Parse(gvSPDV.GetRowCellValue(j, "SOLUONG").ToString());
                                    dpsp.DONGIA = float.Parse(gvSPDV.GetRowCellValue(j, "DONGIA").ToString());
                                    dpsp.THANHTIEN = dpsp.SOLUONG * dpsp.DONGIA;
                                    _datphongsp.add(dpsp);
                                }
                            }
                        }
                    }
                
                else // Chỉnh sửa
                {
                    tb_DatPhong dp = _datphong.getItem(_idDP);
                    if (dp == null)
                    {
                        MessageBox.Show("Booking not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    //// Lấy danh sách phòng ban đầu của đơn đặt phòng
                    //var originalRooms = _datphongchitiet.getAllByDatPhong(_idDP).Select(x => x.IDPHONG).ToList();
                    //// Lấy danh sách phòng hiện tại trong gvDatPhong
                    //var currentRooms = new List<int>();
                    //for (int i = 0; i < gvDatPhong.RowCount; i++)
                    //{
                    //    currentRooms.Add(int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString()));
                    //}

                    // Cập nhật thông tin đơn đặt phòng
                    dp.NGAYDATPHONG = dtNgayDat.Value;
                    dp.NGAYTRAPHONG = dtNgayTra.Value;
                    dp.SONGUOIO = int.Parse(numSoNguoi.Value.ToString());
                    dp.STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString());
                    dp.IDKH = int.Parse(searchKH.EditValue.ToString());
                    dp.SOTIEN = double.Parse(txtThanhTien.Text);
                    dp.GHICHU = txtGhiChu.Text;
                    dp.IDUSER = 1;
                    dp.UPDATE_BY = 1;
                    dp.UPDATE_DATE = DateTime.Now;
                    var _dp = _datphong.update(dp);
                    _idDP = _dp.IDDP;

                    // Xóa toàn bộ chi tiết cũ
                    _datphongct.deleteAll(_idDP);
                    _datphongsp.deleteAll(_idDP);

                    
                    tb_DatPhong_CT dpct = new tb_DatPhong_CT();
                    dpct.IDDP = _dp.IDDP;
                    dpct.IDPHONG = _idPhong;
                    dpct.SONGAYO = (dtNgayTra.Value - dtNgayDat.Value).Days;
                    dpct.DONGIA = int.Parse(_phongHienTai.DONGIA.ToString());
                    dpct.THANHTIEN = dpct.DONGIA * dpct.SONGAYO;
                    dpct.NGAY = DateTime.Now;
                    var _dpct = _datphongct.add(dpct);
                    _phong.updateStatus(dpct.IDPHONG, true); // Phòng vẫn được đặt -> trạng thái true

                    // Thêm lại sản phẩm/dịch vụ
                    if (gvSPDV.RowCount > 0)
                    {
                        for (int j = 0; j < gvSPDV.RowCount; j++)
                        {
                            if (dpct.IDPHONG == int.Parse(gvSPDV.GetRowCellValue(j, "IDPHONG").ToString()))
                            {
                                tb_DatPhong_SanPham dpsp = new tb_DatPhong_SanPham();
                                dpsp.IDDP = _dp.IDDP;
                                dpsp.IDDPCT = _dpct.IDDPCT;
                                dpsp.IDPHONG = int.Parse(gvSPDV.GetRowCellValue(j, "IDPHONG").ToString());
                                dpsp.IDSP = int.Parse(gvSPDV.GetRowCellValue(j, "IDSP").ToString());
                                dpsp.SOLUONG = int.Parse(gvSPDV.GetRowCellValue(j, "SOLUONG").ToString());
                                dpsp.DONGIA = float.Parse(gvSPDV.GetRowCellValue(j, "DONGIA").ToString());
                                dpsp.THANHTIEN = dpsp.SOLUONG * dpsp.DONGIA;
                                _datphongsp.add(dpsp);
                            }
                        }
                    }
                    

                    
                }
                
                
                objMain.showRoom(); // Cập nhật giao diện phòng trên form chính
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}