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
    public partial class ucTuNgay : UserControl
    {
        public ucTuNgay()
        {
            InitializeComponent();
        }

        private void ucTuNgay_Load(object sender, EventArgs e)
        {
            dtTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtDenNgay.Value = DateTime.Now;
        }

        private void dtTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtTuNgay.Select();
                return;
            }
        }

        private void dtTuNgay_Leave(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtTuNgay.Select();
                return;
            }
        }

        private void dtDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtTuNgay.Select();
                return;
            }
        }

        private void dtDenNgay_Leave(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtTuNgay.Select();
                return;
            }
        }
    }
}
