using BusinessLayer;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace KhachSan
{
    public partial class frmDatPhongDon : DevExpress.XtraEditors.XtraForm
    {
        public int IDUSER { get; set; }
        public bool _them;
        public int _idPhong;
        private int _idDP;
        private string _macty;
        private string _madvi;
        private double _tongtien;
        private DATPHONG _datphong;
        private DATPHONG_CHITIET _datphongct;
        private DATPHONG_SANPHAM _datphongsp;
        private OBJ_PHONG _phongHienTai;
        private PHONG _phong;
        private SANPHAM _sanpham;
        private List<OBJ_DPSP> lstDPSP;
        private frmMain objMain = (frmMain)Application.OpenForms["frmMain"];

        public frmDatPhongDon()
        {
            InitializeComponent();
        }

        private void frmDatPhongDon_Load(object sender, EventArgs e)
        {
            // Khởi tạo các đối tượng
            _datphong = new DATPHONG();
            _datphongct = new DATPHONG_CHITIET();
            _datphongsp = new DATPHONG_SANPHAM();
            _phong = new PHONG();
            _sanpham = new SANPHAM();
            lstDPSP = new List<OBJ_DPSP>();

            // Load thông tin phòng
            _phongHienTai = _phong.getItemFull(_idPhong);
            lblPhong.Text = $"{_phongHienTai.TENPHONG} - Đơn giá: {_phongHienTai.DONGIA:N0} VNĐ";

            // Thiết lập giá trị mặc định
            dtNgayDat.Value = DateTime.Now;
            dtNgayTra.Value = DateTime.Now.AddDays(1);
            cboTrangThai.DataSource = TRANGTHAI.getList();
            cboTrangThai.ValueMember = "_value";
            cboTrangThai.DisplayMember = "_display";
            cboTrangThai.SelectedIndex = 1;
            numSoNguoi.Value = 1;
            _macty = myFunctions._macty;
            _madvi = myFunctions._madvi;

            // Load danh sách khách hàng và sản phẩm
            loadKH();
            loadSP();

            // Xử lý dữ liệu khi chỉnh sửa hoặc thêm mới
            if (_them)
            {
                ClearProductList();
            }
            else
            {
                var dpct = _datphongct.getIDDPByPhong(_idPhong);
                if (dpct != null)
                {
                    _idDP = dpct.IDDP;
                    var dp = _datphong.getItem(_idDP);
                    if (dp != null)
                    {
                        searchKH.EditValue = dp.IDKH;
                        dtNgayDat.Value = dp.NGAYDATPHONG.Value;
                        dtNgayTra.Value = dp.NGAYTRAPHONG.Value;
                        cboTrangThai.SelectedValue = dp.STATUS;
                        numSoNguoi.Value = dp.SONGUOIO ?? 1;
                        txtGhiChu.Text = dp.GHICHU ?? "";
                        LoadBookingProducts(); // Load danh sách sản phẩm từ DB
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin đặt phòng cho phòng này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearProductList();
                }
            }

            // Gắn sự kiện
            dtNgayDat.ValueChanged -= dtNgay_ValueChanged;
            dtNgayTra.ValueChanged -= dtNgay_ValueChanged;
            dtNgayDat.ValueChanged += dtNgay_ValueChanged;
            dtNgayTra.ValueChanged += dtNgay_ValueChanged;

            UpdateTotalAmount();
        }

        private void dtNgay_ValueChanged(object sender, EventArgs e)
        {
            UpdateTotalAmount();
        }

        private void LoadBookingProducts()
        {
            if (_idDP <= 0) return;

            lstDPSP.Clear();
            lstDPSP = _datphongsp.getAllByDatPhong(_idDP) ?? new List<OBJ_DPSP>();
            gcSPDV.DataSource = null;
            gcSPDV.DataSource = lstDPSP;
            gcSPDV.RefreshDataSource();
        }

        private void ClearProductList()
        {
            lstDPSP.Clear();
            gcSPDV.DataSource = null;
            gcSPDV.DataSource = lstDPSP;
            gcSPDV.RefreshDataSource();
        }

        private void UpdateTotalAmount()
        {
            int soNgayO = Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1);
            double tongDV = (double)lstDPSP.Sum(sp => sp.THANHTIEN);
            _tongtien = tongDV + _phongHienTai.DONGIA * soNgayO;
            txtThanhTien.Text = _tongtien.ToString("N0");
        }

        private void loadSP()
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

        public void setKH(int idKH)
        {
            KHACHHANG _khachhang = new KHACHHANG();
            searchKH.EditValue = idKH;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (searchKH.EditValue == null || string.IsNullOrEmpty(searchKH.EditValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveData();
            _them = false;
            UpdateTotalAmount();
            MessageBox.Show("Lưu thành công! Bạn có thể tiếp tục in hoặc đóng form.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_idDP == 0)
            {
                MessageBox.Show("Vui lòng lưu phiếu đặt phòng trước khi in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveData();
            UpdateTotalAmount();
            var dp = _datphong.getItem(_idDP);
            dp.SOTIEN = _tongtien;
            _datphong.update(dp, IDUSER);
            _datphong.updateStatus(_idDP, IDUSER);
            _phong.updateStatus(_idPhong, false);
            XuatReport(_idDP.ToString(), "rpDatPhong", "Đơn đặt phòng");
            cboTrangThai.SelectedValue = true;
            objMain.showRoom();
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
               
                try
                {
                    doc.SetParameterValue("@IDDP", _khoa);
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
            if (bool.Parse(cboTrangThai.SelectedValue.ToString()))
            {
                MessageBox.Show("Phiếu đã hoàn tất không được chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (gvSanPham.GetFocusedRowCellValue("IDSP") != null)
            {
                int idSP = int.Parse(gvSanPham.GetFocusedRowCellValue("IDSP").ToString());
                var existingItem = lstDPSP.FirstOrDefault(x => x.IDSP == idSP && x.IDPHONG == _idPhong);

                if (existingItem != null)
                {
                    existingItem.SOLUONG++;
                    existingItem.THANHTIEN = existingItem.SOLUONG * existingItem.DONGIA;
                }
                else
                {
                    OBJ_DPSP sp = new OBJ_DPSP
                    {
                        IDSP = idSP,
                        TENSP = gvSanPham.GetFocusedRowCellValue("TENSP").ToString(),
                        IDPHONG = _idPhong,
                        TENPHONG = _phongHienTai.TENPHONG,
                        DONGIA = float.Parse(gvSanPham.GetFocusedRowCellValue("DONGIA").ToString()),
                        SOLUONG = 1,
                        THANHTIEN = float.Parse(gvSanPham.GetFocusedRowCellValue("DONGIA").ToString())
                    };
                    lstDPSP.Add(sp);
                }

                gcSPDV.DataSource = null;
                gcSPDV.DataSource = lstDPSP;
                gcSPDV.RefreshDataSource();
                UpdateTotalAmount();
            }
        }

        private void gvSPDV_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (bool.Parse(cboTrangThai.SelectedValue.ToString()))
            {
                MessageBox.Show("Phiếu đã hoàn tất không được chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (e.Column.FieldName == "SOLUONG")
            {
                int sl = int.Parse(e.Value.ToString());
                var rowHandle = gvSPDV.FocusedRowHandle;
                if (sl > 0)
                {
                    double gia = double.Parse(gvSPDV.GetRowCellValue(rowHandle, "DONGIA").ToString());
                    gvSPDV.SetRowCellValue(rowHandle, "THANHTIEN", sl * gia);
                }
                else
                {
                    lstDPSP.RemoveAt(rowHandle);
                }
                gcSPDV.RefreshDataSource();
                UpdateTotalAmount();
            }
        }

        private void SaveData()
        {
            try
            {
                if (_them) // Thêm mới
                {
                    tb_DatPhong dp = new tb_DatPhong
                    {
                        NGAYDATPHONG = dtNgayDat.Value,
                        NGAYTRAPHONG = dtNgayTra.Value,
                        SONGUOIO = (int)numSoNguoi.Value,
                        STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString()),
                        THEODOAN = false,
                        IDKH = int.Parse(searchKH.EditValue.ToString()),
                        SOTIEN = _tongtien,
                        GHICHU = txtGhiChu.Text,
                        DISABLED = false,
                        IDUSER = IDUSER,
                        MACTY = _macty,
                        MADVI = _madvi,
                        CREATED_DATE = DateTime.Now
                    };
                    var addedDP = _datphong.add(dp);
                    _idDP = addedDP.IDDP;

                    tb_DatPhong_CT dpct = new tb_DatPhong_CT
                    {
                        IDDP = _idDP,
                        IDPHONG = _idPhong,
                        SONGAYO = Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1),
                        DONGIA = (int)_phongHienTai.DONGIA,
                        THANHTIEN = (int)_phongHienTai.DONGIA * Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1),
                        NGAY = DateTime.Now
                    };
                    var addedDPCT = _datphongct.add(dpct);
                    _phong.updateStatus(_idPhong, true);

                    foreach (var sp in lstDPSP)
                    {
                        if (sp.SOLUONG > 0)
                        {
                            tb_DatPhong_SanPham dpsp = new tb_DatPhong_SanPham
                            {
                                IDDP = _idDP,
                                IDDPCT = addedDPCT.IDDPCT,
                                IDPHONG = sp.IDPHONG,
                                IDSP = sp.IDSP,
                                SOLUONG = sp.SOLUONG,
                                DONGIA = sp.DONGIA,
                                THANHTIEN = sp.THANHTIEN
                            };
                            _datphongsp.add(dpsp);
                        }
                    }
                }
                else // Chỉnh sửa
                {
                    var dp = _datphong.getItem(_idDP);
                    if (dp == null)
                    {
                        throw new Exception("Không tìm thấy thông tin đặt phòng để cập nhật.");
                    }
                    dp.NGAYDATPHONG = dtNgayDat.Value;
                    dp.NGAYTRAPHONG = dtNgayTra.Value;
                    dp.SONGUOIO = (int)numSoNguoi.Value;
                    dp.STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString());
                    dp.IDKH = int.Parse(searchKH.EditValue.ToString());
                    dp.SOTIEN = _tongtien;
                    dp.GHICHU = txtGhiChu.Text;
                    dp.UPDATE_BY = IDUSER;
                    dp.UPDATE_DATE = DateTime.Now;
                    _datphong.update(dp, IDUSER);

                    // Xóa toàn bộ chi tiết và sản phẩm cũ
                    _datphongct.deleteAll(_idDP);
                    _datphongsp.deleteAll(_idDP);

                    tb_DatPhong_CT dpct = new tb_DatPhong_CT
                    {
                        IDDP = _idDP,
                        IDPHONG = _idPhong,
                        SONGAYO = Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1),
                        DONGIA = (int)_phongHienTai.DONGIA,
                        THANHTIEN = (int)_phongHienTai.DONGIA * Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1),
                        NGAY = DateTime.Now
                    };
                    var addedDPCT = _datphongct.add(dpct);
                    _phong.updateStatus(_idPhong, true);

                    foreach (var sp in lstDPSP)
                    {
                        if (sp.SOLUONG > 0)
                        {
                            tb_DatPhong_SanPham dpsp = new tb_DatPhong_SanPham
                            {
                                IDDP = _idDP,
                                IDDPCT = addedDPCT.IDDPCT,
                                IDPHONG = sp.IDPHONG,
                                IDSP = sp.IDSP,
                                SOLUONG = sp.SOLUONG,
                                DONGIA = sp.DONGIA,
                                THANHTIEN = sp.THANHTIEN
                            };
                            _datphongsp.add(dpsp);
                        }
                    }
                }
                objMain.showRoom();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}