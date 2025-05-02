using BusinessLayer;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VietQRHelper;
using QRCoder;
using System.IO;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace KhachSan
{
    public partial class frmDatPhong : DevExpress.XtraEditors.XtraForm
    {
        public int IDUSER { get; set; }
        public frmDatPhong()
        {
            InitializeComponent();
            DataTable tb = myFunctions.laydulieu("SELECT A.IDPHONG, A.TENPHONG, C.DONGIA, A.IDTANG, B.TENTANG FROM tb_Phong A, tb_Tang B, tb_LoaiPhong C WHERE A.IDTANG = B.IDTANG AND A.TRANGTHAI = 0 AND A.DISABLED = 0 AND A.IDLOAIPHONG = C.IDLOAIPHONG");
            gcPhong.DataSource = tb;
            gcPhong.AllowDrop = true;
            gcDatPhong.DataSource = tb.Clone();

            gvSanPham.CustomDrawRowIndicator += gv_CustomDrawRowIndicator;
            gvSPDV.CustomDrawRowIndicator += gv_CustomDrawRowIndicator;
            gvDatPhong.CustomDrawRowIndicator += gv_CustomDrawRowIndicator;
            gvDanhSach.CustomDrawRowIndicator += gv_CustomDrawRowIndicator;
            gvPhong.CustomDrawRowIndicator += gvPhong_CustomDrawRowIndicator; // Thêm lại sự kiện
            gvDatPhong.RowCountChanged += gvDatPhong_RowCountChanged; // Thêm lại sự kiện
            gvDanhSach.CustomDrawCell += gvDanhSach_CustomDrawCell; // Thêm lại sự kiện

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];
        bool _them;
        int _idphong = 0;
        int _idDP = 0;
        string _tenPhong;
        string _macty;
        string _madvi;
        List<OBJ_DPSP> lstDPSP;
        DATPHONG _datphong;
        DATPHONG_CHITIET _datphongchitiet;
        DATPHONG_SANPHAM _datphongsp;
        KHACHHANG _khachhang;
        SANPHAM _sanpham;
        PHONG _phong;
        GridHitInfo downHitInfo = null;

        private void frmDatPhong_Load(object sender, EventArgs e)
        {
            _datphong = new DATPHONG();
            _khachhang = new KHACHHANG();
            _sanpham = new SANPHAM();
            _datphongchitiet = new DATPHONG_CHITIET();
            _datphongsp = new DATPHONG_SANPHAM();
            _phong = new PHONG();
            lstDPSP = new List<OBJ_DPSP>();

            dtTuNgay.Value = myFunctions.GetFirstDayInMonth(DateTime.Now.Year, DateTime.Now.Month);
            dtDenNgay.Value = DateTime.Now;
            _macty = myFunctions._macty;
            _madvi = myFunctions._madvi;

            loadKH();
            loadSP();
            loadDanhSach();

            cboTrangThai.DataSource = TRANGTHAI.getList();
            cboTrangThai.DisplayMember = "_display";
            cboTrangThai.ValueMember = "_value";
            cboTrangThai.SelectedValue = 1;

            showHideControl(true);
            _enable(false);
            gvPhong.ExpandAllGroups();
            tabDanhSach.SelectedTabPage = pageDanhSach;
            dtNgayDat.ValueChanged -= dtNgayDat_ValueChanged;
            dtNgayTra.ValueChanged -= dtNgayTra_ValueChanged;
            dtNgayDat.ValueChanged += dtNgayDat_ValueChanged;
            dtNgayTra.ValueChanged += dtNgayTra_ValueChanged;
        }

        private void dtNgayDat_ValueChanged(object sender, EventArgs e) => UpdateTotalAmount();
        private void dtNgayTra_ValueChanged(object sender, EventArgs e) => UpdateTotalAmount();

        void loadDanhSach()
        {
            gcDanhSach.DataSource = _datphong.getAll(dtTuNgay.Value, dtDenNgay.Value.AddDays(1), _macty, _madvi);
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        public void loadKH()
        {
            cboKhachHang.DataSource = _khachhang.getAll();
            cboKhachHang.DisplayMember = "HOTEN";
            cboKhachHang.ValueMember = "IDKH";
        }
        public void setKH(int idkh)
        {
            var _kh = _khachhang.getItem(idkh);
            cboKhachHang.SelectedValue = _kh.IDKH;
            cboKhachHang.Text = _kh.HOTEN;
        }

        void loadSP()
        {
            gcSanPham.DataSource = _sanpham.getAll();
            gvSanPham.OptionsBehavior.Editable = false;
        }

        void addReset()
        {
            DataTable tb = myFunctions.laydulieu("SELECT A.IDPHONG, A.TENPHONG, C.DONGIA, A.IDTANG, B.TENTANG FROM tb_Phong A, tb_Tang B, tb_LoaiPhong C WHERE A.IDTANG = B.IDTANG AND A.TRANGTHAI = 0 AND A.DISABLED = 0 AND A.IDLOAIPHONG = C.IDLOAIPHONG");
            gcPhong.DataSource = tb;
            gcPhong.AllowDrop = true;
            gcDatPhong.DataSource = tb.Clone();
            gvPhong.ExpandAllGroups();
            ClearProductList();
        }

        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
            btnPrint.Visible = t;
        }

        void _enable(bool t)
        {
            cboKhachHang.Enabled = t;
            btnAddNew.Enabled = t;
            //dtNgayDat.Enabled = t;
            dtNgayTra.Enabled = t;
            //cboTrangThai.Enabled = t;
            chkTheoDoan.Enabled = t;
            numSoNguoi.Enabled = t;
            txtGhiChu.Enabled = t;
            gcPhong.Enabled = t;
            gcSanPham.Enabled = t;
            gcDatPhong.Enabled = t;
            gcSPDV.Enabled = t;
            txtThanhTien.Enabled = false;
        }

        void _reset()
        {
            dtNgayDat.Value = DateTime.Now;
            dtNgayTra.Value = DateTime.Now.AddDays(1);
            numSoNguoi.Value = 1;
            chkTheoDoan.Checked = true;
            cboTrangThai.SelectedValue = false;
            txtGhiChu.Text = "";
            txtThanhTien.Text = "0";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            dtNgayDat.Enabled = false;
            cboTrangThai.Enabled = false;
            showHideControl(false);
            _enable(true);
            _reset();
            addReset();
            tabDanhSach.SelectedTabPage = pageChiTiet;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_idDP == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn đặt phòng để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (bool.Parse(cboTrangThai.SelectedValue.ToString()))
            {
                MessageBox.Show("Phiếu đã hoàn tất không được chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _them = false;
            dtNgayDat.Enabled = false;
            _enable(true);
            showHideControl(false);
            tabDanhSach.SelectedTabPage = pageChiTiet;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_idDP == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn đặt phòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var lstDPCT = _datphongchitiet.getAllByDatPhong(_idDP);
                foreach (var item in lstDPCT)
                {
                    _phong.updateStatus(item.IDPHONG, false);
                }
                _datphong.delete(_idDP, IDUSER);
                loadDanhSach();
                objMain.showRoom();
                _idDP = 0;
                _reset();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedValue == null || string.IsNullOrEmpty(cboKhachHang.SelectedValue.ToString()))
            {
                MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (gvDatPhong.RowCount == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            for (int i = 0; i < gvDatPhong.RowCount; i++)
            {
                int idPhong = int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString());
                if (_datphong.IsRoomBooked(idPhong, dtNgayDat.Value, dtNgayTra.Value, _them ? (int?)null : _idDP))
                {
                    string tenPhong = gvDatPhong.GetRowCellValue(i, "TENPHONG").ToString();
                    MessageBox.Show($"Phòng {tenPhong} đã được đặt trong khoảng thời gian bạn chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            SaveData();
            _them = false;
            _enable(false);
            showHideControl(true);
            loadDanhSach();
            objMain.showRoom();
            MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enable(false);
            if (_idDP > 0)
            {
                LoadBookingDetails();
            }
            tabDanhSach.SelectedTabPage = pageDanhSach;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (_idDP == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn đặt phòng để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            SaveData();
            UpdateTotalAmount();
            var dp = _datphong.getItem(_idDP);
            decimal _tongtien = decimal.Parse(txtThanhTien.Text);
            dp.SOTIEN = double.Parse(txtThanhTien.Text);
            _datphong.update(dp, IDUSER);
            _datphong.updateStatus(_idDP, IDUSER);

            var lstDPCT = _datphongchitiet.getAllByDatPhong(_idDP);
            foreach (var item in lstDPCT)
            {
                _phong.updateStatus(item.IDPHONG, false);
            }

            XuatReport(_idDP.ToString(), "rpDatPhong", "ĐƠN ĐẶT PHÒNG CHI TIẾT", (decimal)_tongtien);
            cboTrangThai.SelectedValue = true;
            objMain.showRoom();
        }

        private void XuatReport(string _khoa, string _rpName, string _rpTitle, decimal _tongtien)
        {

            if (_khoa != null)
            {
                string qrImagePath = CreateQRCodeNganHang(_tongtien);
                Form frm = new Form();
                CrystalReportViewer crv = new CrystalReportViewer();
                crv.ShowGroupTreeButton = false;
                crv.ShowParameterPanelButton = false;
                crv.ToolPanelView = ToolPanelViewType.None;
                //TableLogOnInfo thongtin;
                ReportDocument doc = new ReportDocument();
                doc.Load(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\" + _rpName + ".rpt"));
                doc.SetParameterValue("DuongDanQR", qrImagePath);

                //thongtin = doc.Database.Tables[0].LogOnInfo;
                //thongtin.ConnectionInfo.ServerName = myFunctions._srv;
                //thongtin.ConnectionInfo.DatabaseName = myFunctions._db;
                //thongtin.ConnectionInfo.UserID = myFunctions._us;
                //thongtin.ConnectionInfo.Password = myFunctions._pw;
                ConnectionInfo connInfo = new ConnectionInfo()
                {
                    ServerName = myFunctions._srv,
                    DatabaseName = myFunctions._db,
                    UserID = myFunctions._us,
                    Password = myFunctions._pw
                };

                // Áp dụng cho tất cả các bảng trong report chính
                foreach (Table table in doc.Database.Tables)
                {
                    TableLogOnInfo logonInfo = table.LogOnInfo;
                    logonInfo.ConnectionInfo = connInfo;
                    table.ApplyLogOnInfo(logonInfo);
                }

                try
                {
                    doc.SetParameterValue("@IDDP", _khoa);
                    doc.SetParameterValue("@IDUSER", IDUSER);
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

        public string CreateQRCodeNganHang(decimal _tongtien)
        {
            NGANHANG nganhang = new NGANHANG();
            var bank = nganhang.getAll().FirstOrDefault();
            if (bank == null) return null;

            string bin = "";
            switch (bank.TenNganHang?.Trim())
            {
                case "VietinBank": bin = "970415"; break;
                case "Vietcombank": bin = "970436"; break;
                case "BIDV": bin = "970418"; break;
                case "Agribank": bin = "970405"; break;
                default: return null;
            }

            var qrPay = QRPay.InitVietQR(
                bankBin: bin,
                bankNumber: bank.SoTaiKhoan,
                amount: _tongtien.ToString("0"),
                purpose: string.IsNullOrWhiteSpace(bank.NoiDung) ? "Thanh Toán Hóa Đơn" : bank.NoiDung
            );

            var content = qrPay.Build();
            var qrGenerator = new QRCoder.QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(content, QRCoder.QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCoder.QRCode(qrData);
            Bitmap qrImage = qrCode.GetGraphic(16, Color.Black, Color.White, true);

            // Lưu vào file tạm (Temp folder)
            string tempPath = Path.Combine(Path.GetTempPath(), "QRCode_" + Guid.NewGuid() + ".png");
            qrImage.Save(tempPath, System.Drawing.Imaging.ImageFormat.Png);

            return tempPath; // Trả về đường dẫn ảnh QR
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
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
                            THEODOAN = chkTheoDoan.Checked,
                            IDKH = int.Parse(cboKhachHang.SelectedValue.ToString()),
                            GHICHU = txtGhiChu.Text,
                            DISABLED = false,
                            IDUSER = IDUSER,
                            MACTY = _macty,
                            MADVI = _madvi,
                            CREATED_DATE = DateTime.Now
                        };
                    var addedDP = _datphong.add(dp);
                    _idDP = addedDP.IDDP;

                    for (int i = 0; i < gvDatPhong.RowCount; i++)
                        {
                            int idPhong = int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString());
                            int soNgayO = Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1);
                            double donGia = double.Parse(gvDatPhong.GetRowCellValue(i, "DONGIA").ToString());

                            tb_DatPhong_CT dpct = new tb_DatPhong_CT
                            {
                                IDDP = _idDP,
                                IDPHONG = idPhong,
                                SONGAYO = soNgayO,
                                DONGIA = (int)donGia,
                                THANHTIEN = (int)(donGia * soNgayO),
                                NGAY = DateTime.Now
                            };
                            var addedDPCT = _datphongchitiet.add(dpct); 
                            _phong.updateStatus(idPhong, true);

                            foreach (var sp in lstDPSP.Where(x => x.IDPHONG == idPhong && x.SOLUONG > 0))
                            {
                                tb_DatPhong_SanPham dpsp = new tb_DatPhong_SanPham
                                {
                                    IDDP = _idDP,
                                    IDDPCT = addedDPCT.IDDPCT,
                                    IDPHONG = sp.IDPHONG,
                                    IDSP = sp.IDSP,
                                    SOLUONG = sp.SOLUONG,
                                    DONGIA = sp.DONGIA,
                                    THANHTIEN = sp.THANHTIEN,
                                    NGAY = DateTime.Now
                                };
                                _datphongsp.add(dpsp);
                            }
                        }

                        UpdateTotalAmount();
                    addedDP.SOTIEN = double.Parse(txtThanhTien.Text);
                    _datphong.update(addedDP, IDUSER);
                }
                    else // Chỉnh sửa
                    {
                        var dp = _datphong.getItem(_idDP);
                        if (dp == null)
                        {
                            throw new Exception("Không tìm thấy thông tin đặt phòng để cập nhật.");
                        }

                        var originalRooms = _datphongchitiet.getAllByDatPhong(_idDP).Select(x => x.IDPHONG).ToList();
                        var currentRooms = Enumerable.Range(0, gvDatPhong.RowCount)
                            .Select(i => int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString())).ToList();

                        dp.NGAYDATPHONG = dtNgayDat.Value;
                        dp.NGAYTRAPHONG = dtNgayTra.Value;
                        dp.SONGUOIO = (int)numSoNguoi.Value;
                        dp.STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString());
                        dp.THEODOAN = chkTheoDoan.Checked;
                        dp.IDKH = int.Parse(cboKhachHang.SelectedValue.ToString());
                        dp.GHICHU = txtGhiChu.Text;
                        dp.UPDATE_BY = IDUSER;
                        dp.UPDATE_DATE = DateTime.Now;
                    _datphong.update(dp, IDUSER);

                    _datphongchitiet.deleteAll(_idDP);
                        _datphongsp.deleteAll(_idDP);

                        for (int i = 0; i < gvDatPhong.RowCount; i++)
                        {
                            int idPhong = int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString());
                            int soNgayO = Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1);
                            double donGia = double.Parse(gvDatPhong.GetRowCellValue(i, "DONGIA").ToString());

                            tb_DatPhong_CT dpct = new tb_DatPhong_CT
                            {
                                IDDP = _idDP,
                                IDPHONG = idPhong,
                                SONGAYO = soNgayO,
                                DONGIA = (int)donGia,
                                THANHTIEN = (int)(donGia * soNgayO),
                                NGAY = DateTime.Now
                            };
                            var addedDPCT = _datphongchitiet.add(dpct);
                            _phong.updateStatus(idPhong, true);

                            foreach (var sp in lstDPSP.Where(x => x.IDPHONG == idPhong && x.SOLUONG > 0))
                            {
                                tb_DatPhong_SanPham dpsp = new tb_DatPhong_SanPham
                                {
                                    IDDP = _idDP,
                                    IDDPCT = addedDPCT.IDDPCT,
                                    IDPHONG = sp.IDPHONG,
                                    IDSP = sp.IDSP,
                                    SOLUONG = sp.SOLUONG,
                                    DONGIA = sp.DONGIA,
                                    THANHTIEN = sp.THANHTIEN,
                                    NGAY = DateTime.Now
                                };
                                _datphongsp.add(dpsp);
                            }
                        }

                        var removedRooms = originalRooms.Where(x => !currentRooms.Contains(x)).ToList();
                        foreach (var roomId in removedRooms)
                        {
                            _phong.updateStatus(roomId, false);
                        }

                        UpdateTotalAmount();
                        dp.SOTIEN = double.Parse(txtThanhTien.Text);
                        _datphong.update(dp, IDUSER);
                }
                }
                catch (Exception ex)
                {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }
        
        private void LoadBookingDetails()
        {
            var dp = _datphong.getItem(_idDP);
            if (dp != null)
            {
                cboKhachHang.SelectedValue = dp.IDKH;
                dtNgayDat.Value = dp.NGAYDATPHONG.Value;
                dtNgayTra.Value = dp.NGAYTRAPHONG.Value;
                numSoNguoi.Value = dp.SONGUOIO ?? 1;
                cboTrangThai.SelectedValue = dp.STATUS;
                chkTheoDoan.Checked = dp.THEODOAN ?? false;
                txtGhiChu.Text = dp.GHICHU ?? "";
                txtThanhTien.Text = dp.SOTIEN?.ToString("N0") ?? "0";
                loadDP();
                LoadBookingProducts();
                UpdateTotalAmount();
            }
        }

        private void loadDP()
        {
            gcDatPhong.DataSource = myFunctions.laydulieu("SELECT A.IDPHONG, A.TENPHONG, C.DONGIA, A.IDTANG, B.TENTANG FROM tb_Phong A, tb_Tang B, tb_LoaiPhong C, tb_DatPhong_CT D WHERE A.IDTANG = B.IDTANG AND A.IDLOAIPHONG = C.IDLOAIPHONG AND A.IDPHONG = D.IDPHONG AND D.IDDP = '" + _idDP + "'");
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
            double tongTienPhong = 0;
            double tongTienSP = 0;

            if (lstDPSP != null)
            {
                tongTienSP = (double)lstDPSP.Sum(sp => sp.THANHTIEN);
            }

            int soNgayO = Math.Max((dtNgayTra.Value.Date - dtNgayDat.Value.Date).Days, 1);

            for (int i = 0; i < gvDatPhong.RowCount; i++)
            {
                double donGia = double.Parse(gvDatPhong.GetRowCellValue(i, "DONGIA").ToString());
                tongTienPhong += donGia * soNgayO;
            }

            txtThanhTien.Text = (tongTienPhong + tongTienSP).ToString("N0");
        }

        private void gvSanPham_DoubleClick(object sender, EventArgs e)
        {
            if (_idphong == 0)
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
                var existingItem = lstDPSP.FirstOrDefault(x => x.IDSP == idSP && x.IDPHONG == _idphong);

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
                        IDPHONG = _idphong,
                        TENPHONG = _tenPhong,
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

        private void gvDatPhong_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            downHitInfo = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
            {
                downHitInfo = hitInfo;
                DataRow row = view.GetDataRow(hitInfo.RowHandle);
                if (row != null)
                {
                    _idphong = int.Parse(row["IDPHONG"].ToString());
                    _tenPhong = row["TENPHONG"].ToString();
                }
            }
        }

        private void gvDatPhong_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2, downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);
                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    DataRow row = view.GetDataRow(downHitInfo.RowHandle);
                    if (row != null)
                    {
                        view.GridControl.DoDragDrop(row, DragDropEffects.Move);
                    }
                    downHitInfo = null;
                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }

        private void gvPhong_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            downHitInfo = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
            {
                downHitInfo = hitInfo;
                DataRow row = view.GetDataRow(hitInfo.RowHandle);
                if (row != null)
                {
                    _tenPhong = row["TENPHONG"].ToString();
                }
            }
        }

        private void gvPhong_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2, downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);
                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    DataRow row = view.GetDataRow(downHitInfo.RowHandle);
                    if (row != null)
                    {
                        view.GridControl.DoDragDrop(row, DragDropEffects.Move);
                    }
                    downHitInfo = null;
                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }

        private void gcPhong_DragDrop(object sender, DragEventArgs e)
        {
            GridControl grid = sender as GridControl;
            DataTable table = grid.DataSource as DataTable;
            DataRow row = e.Data.GetData(typeof(DataRow)) as DataRow;
            if (row != null && table != null && row.Table != table)
            {
                table.ImportRow(row);
                row.Delete();

                if (!_them && _idDP > 0)
                {
                    int removedRoomId = int.Parse(row["IDPHONG"].ToString());
                    lstDPSP.RemoveAll(sp => sp.IDPHONG == removedRoomId);
                    gcSPDV.DataSource = null;
                    gcSPDV.DataSource = lstDPSP;
                    gcSPDV.RefreshDataSource();
                    UpdateTotalAmount();
                }
            }
        }

        private void gcPhong_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(DataRow)) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idDP = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDDP").ToString());
                LoadBookingDetails();
            }
        }

        private void gvDanhSach_DoubleClick(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idDP = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDDP").ToString());
                LoadBookingDetails();
                tabDanhSach.SelectedTabPage = pageChiTiet;
            }
        }

        private void dtTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            loadDanhSach();
        }

        private void dtDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            loadDanhSach();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            frm.ShowDialog();
        }

        private void gv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                int _Width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_Width, sender as GridView); }));
            }
        }

        private void gvPhong_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvPhong.IsGroupRow(e.RowHandle))
            {
                if (e.Info.IsRowIndicator)
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1;
                        e.Info.DisplayText = (e.RowHandle + 1).ToString();
                    }
                    SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                    Int32 _Width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvPhong); }));
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", e.RowHandle * -1);
                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _Width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvPhong); }));
            }
        }

        private void gvDatPhong_RowCountChanged(object sender, EventArgs e)
        {
            UpdateTotalAmount();
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DISABLED" && bool.Parse(e.CellValue.ToString()))
            {
                Image img = Properties.Resources.del_icon_32px;
                e.Graphics.DrawImage(img, e.Bounds.X + 12, e.Bounds.Y - 3);
                e.Handled = true;
            }
        }

        private void gvPhong_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            string caption = info.Column.Caption == string.Empty ? info.Column.ToString() : info.Column.Caption;
            info.GroupText = $"{caption}: {info.GroupValueText} ({view.GetChildRowCount(e.RowHandle)} phòng trống)";
        }

        bool cal(int _Width, GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < _Width ? _Width : _View.IndicatorWidth;
            return true;
        }

        private void gvDanhSach_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (e.Column.FieldName == "STATUS")
            {
                bool trangThai = Convert.ToBoolean(gvDanhSach.GetRowCellValue(e.RowHandle, "STATUS"));
                if (trangThai)
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
                else
                {
                    e.Appearance.BackColor = Color.MistyRose;
                }
            }
        }
    }
}