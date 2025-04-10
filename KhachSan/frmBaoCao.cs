﻿using BusinessLayer;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Customization;
using KhachSan.MyControls;
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
    public partial class frmBaoCao : DevExpress.XtraEditors.XtraForm
    {
        public frmBaoCao()
        {
            InitializeComponent();
        }
        public frmBaoCao(tb_SYS_USER user)
        {
            InitializeComponent();
            this._user = user;
        }
        tb_SYS_USER _user;
        SYS_USER _sysUser;
        SYS_REPORT _sysReport;
        SYS_RIGHT_REP _sysRightRep;
        Panel _panel;
        ucTuNgay _uTuNgay;
        ucCongTy _uCongTy;
        ucDonVi _uDonVi;
        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            _sysReport = new SYS_REPORT();
            _sysUser = new SYS_USER();
            _sysRightRep = new SYS_RIGHT_REP();
            var reportList = _sysReport.getListByRight(_sysRightRep.getListByUser(_user.IDUSER));
            lstDanhSach.DataSource = reportList;
            lstDanhSach.DisplayMember = "DESCRIPTION";
            lstDanhSach.ValueMember = "REP_CODE";

            if (reportList.Count == 0)
            {
                MessageBox.Show("Bạn không có quyền truy cập báo cáo nào.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; 
            }

            lstDanhSach.SelectedIndex = 0; 
            lstDanhSach.SelectedIndexChanged += LstDanhSach_SelectedIndexChanged;
            loadUserControl();

        }

        private void LstDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadUserControl();
        }

        void loadUserControl()
        {
            int rep_code = 0;
            if (lstDanhSach.SelectedValue != null)
            {
                rep_code = int.Parse(lstDanhSach.SelectedValue.ToString());
            }
            else
            {
                rep_code = 0; 
            }

            tb_SYS_REPORT rep = _sysReport.getItem(rep_code);

            if (_panel != null)
            {
                _panel.Dispose();
            }
            _panel = new Panel();
            _panel.Dock = DockStyle.Top;
            _panel.MinimumSize = new Size(_panel.Width, 500);
            List<Control> _ctrl = new List<Control>();

            
            if (rep != null)
            {
                if (rep.TUNGAY == true)
                {
                    _uTuNgay = new ucTuNgay();
                    _uTuNgay.Dock = DockStyle.Top;
                    _ctrl.Add(_uTuNgay);
                }
                if (rep.MACTY == true)
                {
                    _uCongTy = new ucCongTy();
                    _uCongTy.Dock = DockStyle.Top;
                    _ctrl.Add(_uCongTy);
                }
                if (rep.MADVI == true)
                {
                    _uDonVi = new ucDonVi();
                    _uDonVi.Dock = DockStyle.Top;
                    _ctrl.Add(_uDonVi);
                }
            }
            else
            {
              
                MessageBox.Show("Không tìm thấy báo cáo được chọn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            _ctrl.Reverse();
            _panel.Controls.AddRange(_ctrl.ToArray());
            this.splBaoCao.Panel2.Controls.Add(_panel);
        }

        private void btnThucHien_Click(object sender, EventArgs e)
        {
            try
            {
                tb_SYS_REPORT rp = _sysReport.getItem(int.Parse(lstDanhSach.SelectedValue.ToString()));
                Form frm = new Form();
                CrystalReportViewer crv = new CrystalReportViewer();
                crv.ShowGroupTreeButton = false;
                crv.ShowParameterPanelButton = false;
                crv.ToolPanelView = ToolPanelViewType.None;
                TableLogOnInfo thongtin;
                ReportDocument doc = new ReportDocument();
                doc.Load(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\" + rp.REP_NAME + ".rpt"));
                thongtin = doc.Database.Tables[0].LogOnInfo;
                thongtin.ConnectionInfo.ServerName = myFunctions._srv;
                thongtin.ConnectionInfo.DatabaseName = myFunctions._db;
                thongtin.ConnectionInfo.UserID = myFunctions._us;
                thongtin.ConnectionInfo.Password = myFunctions._pw;
                doc.Database.Tables[0].ApplyLogOnInfo(thongtin);
                if (rp.TUNGAY == true)
                {
                    if (_uTuNgay == null || _uTuNgay.dtTuNgay == null || _uTuNgay.dtDenNgay == null)
                    {
                        MessageBox.Show("Vui lòng chọn ngày hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    doc.SetParameterValue("NGAYD", _uTuNgay.dtTuNgay.Value);
                    doc.SetParameterValue("NGAYC", _uTuNgay.dtDenNgay.Value);
                }
                if (rp.MACTY == true)
                {
                    if (_uCongTy == null || _uCongTy.cboCongTy == null || _uCongTy.cboCongTy.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng chọn công ty.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    doc.SetParameterValue("MACTY", _uCongTy.cboCongTy.SelectedValue.ToString());
                }

                crv.Dock = DockStyle.Fill;
                crv.ReportSource = doc;
                frm.Controls.Add(crv);
                crv.Refresh();
                frm.Text = rp.DESCRIPTION;
                frm.WindowState = FormWindowState.Maximized;
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}