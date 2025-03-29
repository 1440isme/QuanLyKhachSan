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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Windows.Forms;
using CrystalDecisions.Shared;


namespace KhachSan
{
    public partial class frmCongTy : DevExpress.XtraEditors.XtraForm
    {
        public frmCongTy()
        {
            InitializeComponent();
        }
        CONGTY _congty;
        bool _them;
        string _macty;
        private void frmCongTy_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            LoadData();
            showHideControl(true);
            txtMaCTy.Enabled = false;
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
            txtDienThoai.Enabled = t;
            txtFax.Enabled = t;
            txtEmail.Enabled = t;
            txtDiaChi.Enabled = t;
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtTen.Text = "";
            txtDienThoai.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            chkDisabled.Checked = false;
        }
        void LoadData()
        {
            gcDanhSach.DataSource = _congty.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            txtMaCTy.Enabled = true;
            showHideControl(false);
            _enable(true);
            _reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enable(true);
            txtMaCTy.Enabled = false;
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa công ty này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _congty.delete(_macty);

            }
            LoadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_CongTy cty = new tb_CongTy();
                cty.MACTY = txtMaCTy.Text;
                cty.TENCTY = txtTen.Text;
                cty.DIENTHOAI = txtDienThoai.Text;
                cty.FAX = txtFax.Text;
                cty.EMAIL = txtEmail.Text;
                cty.DIACHI = txtDiaChi.Text;
                cty.DISABLE = chkDisabled.Checked;
                _congty.add(cty);
            }
            else
            {
                tb_CongTy cty = _congty.getItem(_macty);

                cty.TENCTY = txtTen.Text;
                cty.DIENTHOAI = txtDienThoai.Text;
                cty.FAX = txtFax.Text;
                cty.EMAIL = txtEmail.Text;
                cty.DIACHI = txtDiaChi.Text;
                cty.DISABLE = chkDisabled.Checked;
                _congty.update(cty);
            }
            _them = false;
            LoadData();
            _enable(false);
            showHideControl(true);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;

            showHideControl(true);
            _enable(false);
            txtMaCTy.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _macty = gvDanhSach.GetFocusedRowCellValue("MACTY").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENCTY").ToString();
                txtMaCTy.Text = gvDanhSach.GetFocusedRowCellValue("MACTY").ToString();
                txtDienThoai.Text = gvDanhSach.GetFocusedRowCellValue("DIENTHOAI").ToString();
                txtFax.Text = gvDanhSach.GetFocusedRowCellValue("FAX").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("EMAIL").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DIACHI").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLE").ToString());

            }
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DISABLE" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources._1398917_circle_close_cross_incorrect_invalid_icon1;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            XuatReport("rpCongTy", "DANH MỤC CÔNG TY");
        }

        private void XuatReport(string _rpName, string _rpTitle)
        {
            if (_macty!= null)
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
                    doc.SetParameterValue("macty", _macty.ToString());
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
    }
}