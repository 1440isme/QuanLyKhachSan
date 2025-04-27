using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;

namespace KhachSan
{
    public partial class frmThongTinNganHang : DevExpress.XtraEditors.XtraForm
    {
        public frmThongTinNganHang()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public event EventHandler DataChanged;
        NGANHANG _nganhang;

        private Dictionary<string, string> bankBins = new Dictionary<string, string>
        {
            { "VietinBank", "970415" },
            { "Vietcombank", "970436" },
            { "BIDV", "970418" },
            { "Agribank", "970405" }
        };

        private void frmThongTinNganHang_Load(object sender, EventArgs e)
        {
            _nganhang = new NGANHANG();
            cbb_NganHang.Properties.Items.AddRange(new string[] { "VietinBank", "Vietcombank", "BIDV", "Agribank" });

            var data = _nganhang.getAll().FirstOrDefault();
            if (data != null)
            {
                cbb_NganHang.EditValue = data.TenNganHang;
                txt_STK.Text = data.SoTaiKhoan;
                txt_TenTK.Text = data.TenTaiKhoan;
                txt_NoiDung.Text = string.IsNullOrWhiteSpace(data.NoiDung) ? "Thanh Toán Hóa Đơn" : data.NoiDung;
            }
            else
            {
                cbb_NganHang.EditValue = "VietinBank";
                txt_NoiDung.Text = "Thanh Toán Hóa Đơn";
            }
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string bankName = cbb_NganHang.EditValue?.ToString();
            string soTaiKhoan = txt_STK.Text.Trim();
            string tenTaiKhoan = txt_TenTK.Text.Trim();
            string noiDung = txt_NoiDung.Text.Trim();

            if (string.IsNullOrEmpty(bankName) || string.IsNullOrEmpty(soTaiKhoan) || string.IsNullOrEmpty(tenTaiKhoan))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin ngân hàng, số tài khoản và tên tài khoản.", "Thiếu thông tin");
                return;
            }

            if (!bankBins.ContainsKey(bankName))
            {
                MessageBox.Show("Ngân hàng không hợp lệ.", "Lỗi");
                return;
            }

            // Tạo mới hoặc cập nhật
            tb_ThongTinNganHang item = new tb_ThongTinNganHang
            {
                TenNganHang = bankName,
                SoTaiKhoan = soTaiKhoan,
                TenTaiKhoan = tenTaiKhoan,
                NoiDung = noiDung
            };

            _nganhang.update(item);
            MessageBox.Show("Đã lưu thông tin ngân hàng.");
        }
    }
}