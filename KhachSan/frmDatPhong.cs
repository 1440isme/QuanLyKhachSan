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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];
        bool _them;
        int _idphong = 0;
        int _idDP = 0;
        int _rowDatPhong = 0;
        string _tenPhong;
        string _macty;
        string _madvi;
        List<OBJ_DPSP> lstDPSP;
        SYS_PARAM _sysparam;
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
        }

        void loadDanhSach()
        {
            gcDanhSach.DataSource = _datphong.getAll(dtTuNgay.Value, dtDenNgay.Value.AddDays(1), _macty, _madvi);
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        public void loadKH()
        {
            _khachhang = new KHACHHANG();
            cboKhachHang.DataSource = _khachhang.getAll();
            cboKhachHang.DisplayMember = "HOTEN";
            cboKhachHang.ValueMember = "IDKH";
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
            lstDPSP.Clear();
            gcSPDV.DataSource = null;
            gcSPDV.DataSource = lstDPSP;
            gcSPDV.RefreshDataSource();
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
            dtNgayDat.Enabled = t;
            dtNgayTra.Enabled = t;
            cboTrangThai.Enabled = t;
            chkTheoDoan.Enabled = t;
            numSoNguoi.Enabled = t;
            numSoNguoi.Maximum = int.MaxValue;
            txtGhiChu.Enabled = t;
            gcPhong.Enabled = t;
            gcSanPham.Enabled = t;
            gcDatPhong.Enabled = t;
            gcSPDV.Enabled = t;
            txtThanhTien.Enabled = t;
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
            showHideControl(false);
            _enable(true);
            _reset();
            addReset();
            tabDanhSach.SelectedTabPage = pageChiTiet;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (bool.Parse(cboTrangThai.SelectedValue.ToString()) == true)
            {
                MessageBox.Show("Phiếu đã hoàn tất không được chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (_idDP == 0)
            {
                MessageBox.Show("Vui lòng chọn đơn đặt phòng để chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _them = false;
            _enable(true);
            showHideControl(false);
            tabDanhSach.SelectedTabPage = pageChiTiet;
            lstDPSP.Clear();
            gcSPDV.DataSource = null;
            gcSPDV.RefreshDataSource();

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
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboKhachHang.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (gvDatPhong.RowCount == 0)
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Kiểm tra thời gian đặt phòng có giao nhau với đơn đặt phòng khác hay không
                for (int i = 0; i < gvDatPhong.RowCount; i++)
                {
                    int idPhong = int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString());
                    if (_datphong.IsRoomBooked(idPhong, dtNgayDat.Value, dtNgayTra.Value, _them ? (int?)null : _idDP))
                    {
                        string tenPhong = gvDatPhong.GetRowCellValue(i, "TENPHONG").ToString();
                        MessageBox.Show($"Phòng {tenPhong} đã được đặt trong khoảng thời gian bạn chọn. Vui lòng chọn thời gian khác hoặc phòng khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Đồng bộ lstDPSP với gvSPDV trước khi lưu
                SyncListDPSPWithGrid();

                saveData();
                loadDanhSach();
                objMain.showRoom();
                _them = false;
                _enable(false);
                showHideControl(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SyncListDPSPWithGrid()
        {
            // Làm mới lstDPSP và đồng bộ với dữ liệu từ gvSPDV
            lstDPSP.Clear();
            for (int i = 0; i < gvSPDV.RowCount; i++)
            {
                OBJ_DPSP sp = new OBJ_DPSP
                {
                    IDSP = int.Parse(gvSPDV.GetRowCellValue(i, "IDSP").ToString()),
                    TENSP = gvSPDV.GetRowCellValue(i, "TENSP").ToString(),
                    IDPHONG = int.Parse(gvSPDV.GetRowCellValue(i, "IDPHONG").ToString()),
                    TENPHONG = gvSPDV.GetRowCellValue(i, "TENPHONG").ToString(),
                    DONGIA = float.Parse(gvSPDV.GetRowCellValue(i, "DONGIA").ToString()),
                    SOLUONG = int.Parse(gvSPDV.GetRowCellValue(i, "SOLUONG").ToString()),
                    THANHTIEN = float.Parse(gvSPDV.GetRowCellValue(i, "THANHTIEN").ToString())
                };
                lstDPSP.Add(sp);
            }
        }

        void saveData()
        {
            try
            {
                if (_them) // Thêm mới
                {
                    tb_DatPhong dp = new tb_DatPhong();
                    dp.NGAYDATPHONG = dtNgayDat.Value;
                    dp.NGAYTRAPHONG = dtNgayTra.Value;
                    dp.SONGUOIO = int.Parse(numSoNguoi.Value.ToString());
                    dp.STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString());
                    dp.THEODOAN = chkTheoDoan.Checked;
                    dp.IDKH = int.Parse(cboKhachHang.SelectedValue.ToString());
                    dp.GHICHU = txtGhiChu.Text;
                    dp.DISABLED = false;
                    dp.IDUSER = IDUSER;
                    dp.MACTY = _macty;
                    dp.MADVI = _madvi;
                    dp.CREATED_DATE = DateTime.Now;
                    var _dp = _datphong.add(dp);
                    _idDP = _dp.IDDP;

                    double tongTienPhong = 0;
                    for (int i = 0; i < gvDatPhong.RowCount; i++)
                    {
                        tb_DatPhong_CT dpct = new tb_DatPhong_CT();
                        dpct.IDDP = _dp.IDDP;
                        dpct.IDPHONG = int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString());
                        dpct.NGAY = DateTime.Now;
                        var _dpct = _datphongchitiet.add(dpct);
                        _phong.updateStatus(dpct.IDPHONG, true);
                        int soNgay = (dtNgayTra.Value - dtNgayDat.Value).Days;
                        if (soNgay <= 0) soNgay = 1;
                        double donGia = double.Parse(gvDatPhong.GetRowCellValue(i, "DONGIA").ToString());
                        tongTienPhong += donGia * soNgay;

                        // Lưu sản phẩm từ lstDPSP vào database
                        foreach (var sp in lstDPSP)
                        {
                            if (dpct.IDPHONG == sp.IDPHONG)
                            {
                                tb_DatPhong_SanPham dpsp = new tb_DatPhong_SanPham();
                                dpsp.IDDP = _dp.IDDP;
                                dpsp.IDDPCT = _dpct.IDDPCT;
                                dpsp.IDPHONG = sp.IDPHONG;
                                dpsp.IDSP = sp.IDSP;
                                dpsp.SOLUONG = sp.SOLUONG;
                                dpsp.DONGIA = sp.DONGIA;
                                dpsp.THANHTIEN = sp.THANHTIEN;
                                dpsp.NGAY = DateTime.Now;
                                _datphongsp.add(dpsp);
                            }
                        }
                    }

                    double tongTienSP = gvSPDV.RowCount > 0 ? double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue?.ToString() ?? "0") : 0;
                    _dp.SOTIEN = tongTienPhong + tongTienSP;
                    _datphong.update(_dp, IDUSER);
                    txtThanhTien.Text = _dp.SOTIEN.Value.ToString("N0");
                }
                else // Chỉnh sửa
                {
                    tb_DatPhong dp = _datphong.getItem(_idDP);
                    if (dp == null)
                    {
                        MessageBox.Show("Booking not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var originalRooms = _datphongchitiet.getAllByDatPhong(_idDP).Select(x => x.IDPHONG).ToList();
                    var currentRooms = new List<int>();
                    for (int i = 0; i < gvDatPhong.RowCount; i++)
                    {
                        currentRooms.Add(int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString()));
                    }

                    dp.NGAYDATPHONG = dtNgayDat.Value;
                    dp.NGAYTRAPHONG = dtNgayTra.Value;
                    dp.SONGUOIO = int.Parse(numSoNguoi.Value.ToString());
                    dp.STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString());
                    dp.THEODOAN = chkTheoDoan.Checked;
                    dp.IDKH = int.Parse(cboKhachHang.SelectedValue.ToString());
                    dp.GHICHU = txtGhiChu.Text;
                    dp.IDUSER = IDUSER;
                    var _dp = _datphong.update(dp, IDUSER);
                    _idDP = _dp.IDDP;

                    _datphongchitiet.deleteAll(_idDP);
                    _datphongsp.deleteAll(_idDP);

                    double tongTienPhong = 0;
                    for (int i = 0; i < gvDatPhong.RowCount; i++)
                    {
                        tb_DatPhong_CT dpct = new tb_DatPhong_CT();
                        dpct.IDDP = _dp.IDDP;
                        dpct.IDPHONG = int.Parse(gvDatPhong.GetRowCellValue(i, "IDPHONG").ToString());
                        dpct.NGAY = DateTime.Now;
                        var _dpct = _datphongchitiet.add(dpct);
                        _phong.updateStatus(dpct.IDPHONG, true);
                        int soNgay = (dtNgayTra.Value - dtNgayDat.Value).Days;
                        if (soNgay <= 0) soNgay = 1;
                        double donGia = double.Parse(gvDatPhong.GetRowCellValue(i, "DONGIA").ToString());
                        tongTienPhong += donGia * soNgay;

                        // Lưu sản phẩm từ lstDPSP vào database
                        foreach (var sp in lstDPSP)
                        {
                            if (dpct.IDPHONG == sp.IDPHONG)
                            {
                                tb_DatPhong_SanPham dpsp = new tb_DatPhong_SanPham();
                                dpsp.IDDP = _dp.IDDP;
                                dpsp.IDDPCT = _dpct.IDDPCT;
                                dpsp.IDPHONG = sp.IDPHONG;
                                dpsp.IDSP = sp.IDSP;
                                dpsp.SOLUONG = sp.SOLUONG;
                                dpsp.DONGIA = sp.DONGIA;
                                dpsp.THANHTIEN = sp.THANHTIEN;
                                dpsp.NGAY = DateTime.Now;
                                _datphongsp.add(dpsp);
                            }
                        }
                    }

                    var removedRooms = originalRooms.Where(x => !currentRooms.Contains(x)).ToList();
                    foreach (var roomId in removedRooms)
                    {
                        _phong.updateStatus(roomId, false);
                    }

                    double tongTienSP = gvSPDV.RowCount > 0 ? double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue?.ToString() ?? "0") : 0;
                    _dp.SOTIEN = tongTienPhong + tongTienSP;
                    _datphong.update(_dp, IDUSER);
                    txtThanhTien.Text = _dp.SOTIEN.Value.ToString("N0");

                    // Làm mới lstDPSP và giao diện sau khi lưu
                    loadSPDV();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving data: " + ex.Message);
            }
        }
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enable(false);
            if (_idDP > 0)
            {
                var dp = _datphong.getItem(_idDP);
                cboKhachHang.SelectedValue = dp.IDKH;
                dtNgayDat.Value = dp.NGAYDATPHONG.Value;
                dtNgayTra.Value = dp.NGAYTRAPHONG.Value;
                numSoNguoi.Value = dp.SONGUOIO.Value;
                cboTrangThai.SelectedValue = dp.STATUS;
                txtGhiChu.Text = dp.GHICHU;
                txtThanhTien.Text = dp.SOTIEN.Value.ToString("N0");
                loadDP();
                loadSPDV();
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
            XuatReport(_idDP.ToString(), "rpDatPhong", "ĐƠN ĐẶT PHÒNG CHI TIẾT");
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
                        _tenPhong = row["TENPHONG"].ToString();
                        if (!string.IsNullOrEmpty(_tenPhong))
                        {
                            view.GridControl.DoDragDrop(row, DragDropEffects.Move);
                        }
                    }
                    downHitInfo = null;
                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
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
                        _tenPhong = row["TENPHONG"].ToString();
                        if (!string.IsNullOrEmpty(_tenPhong))
                        {
                            view.GridControl.DoDragDrop(row, DragDropEffects.Move);
                        }
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
                    loadDPSP();
                }
            }
        }

        private void gcPhong_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataRow)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        bool cal(Int32 _Width, GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < _Width ? _Width : _View.IndicatorWidth;
            return true;
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

        private void gvPhong_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            string caption = info.Column.Caption;
            if (info.Column.Caption == string.Empty)
                caption = info.Column.ToString();
            info.GroupText = string.Format("{0}: {1} ({2} phòng trống)", caption, info.GroupValueText, view.GetChildRowCount(e.RowHandle));
        }

        private void gcSanPham_DoubleClick(object sender, EventArgs e)
        {
            if (_idphong == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (bool.Parse(cboTrangThai.SelectedValue.ToString()) == true)
            {
                MessageBox.Show("Phiếu đã hoàn tất không được chỉnh sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (gvSanPham.GetFocusedRowCellValue("IDSP") != null)
            {
                OBJ_DPSP sp = new OBJ_DPSP();
                sp.IDSP = int.Parse(gvSanPham.GetFocusedRowCellValue("IDSP").ToString());
                sp.TENSP = gvSanPham.GetFocusedRowCellValue("TENSP").ToString();
                sp.IDPHONG = _idphong;
                sp.TENPHONG = _tenPhong;
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
                loadDPSP();
                UpdateTongTien();
            }
        }

        void loadDPSP()
        {
            gcSPDV.DataSource = null;
            gcSPDV.DataSource = lstDPSP;
            gcSPDV.RefreshDataSource();
        }

        void loadSPDV()
        {
            lstDPSP.Clear(); // Xóa sạch trước khi load lại
            if (_idDP > 0)
            {
                lstDPSP = _datphongsp.getAllByDatPhong(_idDP);
            }
            gcSPDV.DataSource = null;
            gcSPDV.DataSource = lstDPSP;
            gcSPDV.RefreshDataSource();
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
            UpdateTongTien();
            SyncListDPSPWithGrid();
        }

        private void gvDatPhong_RowCountChanged(object sender, EventArgs e)
        {
            UpdateTongTien();
        }

        private void UpdateTongTien()
        {
            double tongTienPhong = 0;
            double tongTienSP = 0;
            if (gvDatPhong.RowCount > 0)
            {
                int soNgay = (dtNgayTra.Value - dtNgayDat.Value).Days;
                if (soNgay <= 0) soNgay = 1;
                for (int i = 0; i < gvDatPhong.RowCount; i++)
                {
                    double donGia = double.Parse(gvDatPhong.GetRowCellValue(i, "DONGIA").ToString());
                    tongTienPhong += donGia * soNgay;
                }
            }
            if (gvSPDV.RowCount > 0)
            {
                tongTienSP = double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue?.ToString() ?? "0");
            }
            txtThanhTien.Text = (tongTienPhong + tongTienSP).ToString("N0");
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

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            frm.ShowDialog();
        }

        public void setKH(int idkh)
        {
            var _kh = _khachhang.getItem(idkh);
            cboKhachHang.SelectedValue = _kh.IDKH;
            cboKhachHang.Text = _kh.HOTEN;
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idDP = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDDP").ToString());
                var dp = _datphong.getItem(_idDP);
                cboKhachHang.SelectedValue = dp.IDKH;
                dtNgayDat.Value = dp.NGAYDATPHONG.Value;
                dtNgayTra.Value = dp.NGAYTRAPHONG.Value;
                numSoNguoi.Value = dp.SONGUOIO.Value;
                cboTrangThai.SelectedValue = dp.STATUS;
                txtGhiChu.Text = dp.GHICHU;
                txtThanhTien.Text = dp.SOTIEN.Value.ToString("N0");
                loadDP();
                loadSPDV();
            }
        }

        void loadDP()
        {
            _rowDatPhong = 0;
            gcDatPhong.DataSource = myFunctions.laydulieu("SELECT A.IDPHONG, A.TENPHONG, C.DONGIA, A.IDTANG, B.TENTANG FROM tb_Phong A, tb_tang B, tb_LoaiPhong C, tb_DatPhong_CT D WHERE A.IDTANG = B.IDTANG AND A.IDLOAIPHONG = C.IDLOAIPHONG AND A.IDPHONG = D.IDPHONG AND D.IDDP = '" + _idDP + "'");
            _rowDatPhong = gvDatPhong.RowCount;
        }

        private void dtTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                loadDanhSach();
            }
        }

        private void dtTuNgay_Leave(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                loadDanhSach();
            }
        }

        private void dtDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                loadDanhSach();
            }
        }

        private void dtDenNgay_Leave(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                loadDanhSach();
            }
        }

        private void gvDanhSach_DoubleClick(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idDP = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDDP").ToString());
                var dp = _datphong.getItem(_idDP);
                cboKhachHang.SelectedValue = dp.IDKH;
                dtNgayDat.Value = dp.NGAYDATPHONG.Value;
                dtNgayTra.Value = dp.NGAYTRAPHONG.Value;
                numSoNguoi.Value = dp.SONGUOIO.Value;
                cboTrangThai.SelectedValue = dp.STATUS;
                txtGhiChu.Text = dp.GHICHU;
                txtThanhTien.Text = dp.SOTIEN.Value.ToString("N0");
                loadDP();
                loadSPDV();
                tabDanhSach.SelectedTabPage = pageChiTiet;
            }
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DISABLED" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources._1398917_circle_close_cross_incorrect_invalid_icon1;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
        }
    }
}