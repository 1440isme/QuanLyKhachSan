using BusinessLayer;
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
    public partial class ucDonVi : UserControl
    {
        public ucDonVi()
        {
            InitializeComponent();
        }
        CONGTY _congty;
        DONVI _donvi;
        private void ucDonVi_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            _donvi = new DONVI();
            loadCongTy();
            cboCongTy.Enabled = false;
            cboCongTy.SelectedValueChanged += CboCongTy_SelectedValueChanged;
            loadDonVi();
            if (myFunctions._madvi == "~")
                cboDonVi.Enabled = true;
            else
            {
                cboDonVi.SelectedValue = myFunctions._madvi;
                cboDonVi.Enabled = false;
            }    
                

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
            cboCongTy.SelectedValue = myFunctions._macty;
        }
        void loadDonVi()
        {
            cboDonVi.DataSource = _donvi.getAll(cboCongTy.SelectedValue.ToString());
            cboDonVi.DisplayMember = "TENDVI";
            cboDonVi.ValueMember = "MADVI";
          
        }

        
    }
}
