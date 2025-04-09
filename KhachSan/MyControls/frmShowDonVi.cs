using BusinessLayer;
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

namespace KhachSan.MyControls
{
    public partial class frmShowDonVi : DevExpress.XtraEditors.XtraForm
    {
        public frmShowDonVi()
        {
            InitializeComponent();
        }
        public frmShowDonVi(TextBox txtDonVi)
        {
            InitializeComponent();
            this._txtDonVi = txtDonVi;
        }
        TextBox _txtDonVi;
        CONGTY _congty;
        DONVI _donvi;
        private void frmShowDonVi_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            _donvi = new DONVI();
            loadCongTy();
            loadDonVi();
            cboCongTy.SelectedValueChanged += CboCongTy_SelectedValueChanged;
            cboCongTy.SelectedValue = myFunctions._macty;
        }

        private void CboCongTy_SelectedValueChanged(object sender, EventArgs e)
        {
            loadDonVi();
        }
        void loadCongTy()
        {
            cboCongTy.DataSource = _congty.getAll();
            cboCongTy.DisplayMember = "TENCTY";
            cboCongTy.ValueMember = "MACTY";
        }
        void loadDonVi()
        {
            gcDanhSach.DataSource = _donvi.getAll(cboCongTy.SelectedValue.ToString());
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        private void btnThucHien_Click(object sender, EventArgs e)
        {

        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}