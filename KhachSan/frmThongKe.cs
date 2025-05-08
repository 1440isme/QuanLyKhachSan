using BusinessLayer;
using DataLayer;
using DevExpress.XtraCharts;
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
using DevExpress.XtraCharts.Native;

namespace KhachSan
{
    public partial class frmThongKe : DevExpress.XtraEditors.XtraForm
    {
        public frmThongKe(tb_SYS_USER user)
        {
            InitializeComponent();
            this._user = user;
        }

        tb_SYS_USER _user;
        SYS_USER _sysUser;
        THONGKE _thongke;

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            _thongke = new THONGKE();

            cbBieuDo.Items.AddRange(new string[]
            {
                "Tỷ lệ doanh thu theo phòng",
                "Tỷ lệ doanh thu theo sản phẩm",
                "Tăng tưởng doanh thu theo thời gian",
                "Lượng khách theo thời gian"
            });
            cbBieuDo.SelectedIndex = -1;

            comboBox2.Items.AddRange(new string[]
            {
                "Ngày",
                "Tuần",
                "Tháng",
                "Quý",
                "Năm"
            });
            comboBox2.SelectedIndex = -1;

            dtTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, 1);
            dtDenNgay.Value = DateTime.Now;
        }

        private void btnTao_Click(object sender, EventArgs e)
        {
            string selected = cbBieuDo.SelectedItem.ToString();
            if (string.IsNullOrEmpty(selected))
            {
                MessageBox.Show("Vui lòng chọn loại biểu đồ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selected.Contains("theo thời gian") && comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn đơn vị thời gian (Ngày, Tháng, Quý, Năm).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime tuNgay = dtTuNgay.Value;
            DateTime denNgay = dtDenNgay.Value;


            chartDoanhThu.Series.Clear();
            chartDoanhThu.Height = 500;
            chartDoanhThu.Width = 1000;
            chartDoanhThu.Titles.Clear();

            if (selected.Contains("Tỷ lệ doanh thu theo phòng"))
            {
                Series pieSeries = new Series("Tỷ lệ doanh thu theo phòng", ViewType.Pie3D);

                ChartTitle chartTitle = new ChartTitle();
                chartTitle.Text = "Tỷ lệ doanh thu theo phòng";
                chartTitle.Font = new Font("Tahoma", 14, FontStyle.Bold);
                chartTitle.TextColor = Color.FromArgb(0, 0, 0);
                chartTitle.Alignment = StringAlignment.Center;
                chartDoanhThu.Titles.Add(chartTitle);

                var lst = _thongke.DoanhThuTheoPhong(tuNgay, denNgay);
                foreach (var item in lst)
                {
                    pieSeries.Points.Add(new SeriesPoint(item.TENPHONG, item.THANHTIEN));
                }
                pieSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                pieSeries.Label.TextPattern = "{A}: {VP : p0}";
                chartDoanhThu.Series.Add(pieSeries);

                chartDoanhThu.Legend.Font = new Font("Tahoma", 10);
                chartDoanhThu.Legend.MarkerSize = new Size(10, 10);
                chartDoanhThu.Legend.Padding.All = 5;
                chartDoanhThu.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                chartDoanhThu.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
            }
            else if (selected.Contains("Tỷ lệ doanh thu theo sản phẩm"))
            {
                Series barSeries = new Series("Tổng thu sản phẩm", ViewType.Bar);

                ChartTitle chartTitle = new ChartTitle();
                chartTitle.Text = "Tỷ lệ doanh thu theo sản phẩm";
                chartTitle.Font = new Font("Tahoma", 14, FontStyle.Bold);
                chartTitle.TextColor = Color.FromArgb(0, 0, 0);
                chartTitle.Alignment = StringAlignment.Center;
                chartDoanhThu.Titles.Add(chartTitle);

                var lst = _thongke.DoanhThuTheoSanPham(tuNgay, denNgay);
                foreach (var item in lst)
                {
                    barSeries.Points.Add(new SeriesPoint(item.TENSP, item.THANHTIEN));
                }

                barSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                barSeries.Label.TextPattern = "{V:n0}";

                ((BarSeriesLabel)barSeries.Label).Position = BarSeriesLabelPosition.Top;
                ((BarSeriesLabel)barSeries.Label).Font = new Font("Tahoma", 9, FontStyle.Bold);

                chartDoanhThu.Series.Add(barSeries);

                XYDiagram diagram = chartDoanhThu.Diagram as XYDiagram;
                if (diagram != null)
                {
                    diagram.AxisX.Title.Text = "Sản phẩm";
                    diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    diagram.AxisX.Label.TextPattern = "{A}";

                    diagram.AxisY.Title.Text = "Doanh thu (VNĐ)";
                    diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    diagram.AxisY.Label.TextPattern = "{V:n0}";
                }

                chartDoanhThu.Legend.Font = new Font("Tahoma", 10);
                chartDoanhThu.Legend.MarkerSize = new Size(10, 10);
                chartDoanhThu.Legend.Padding.All = 5;
                chartDoanhThu.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                chartDoanhThu.Legend.AlignmentVertical = LegendAlignmentVertical.Top;

            }    
            else if (selected.Contains("Tăng tưởng doanh thu"))
            {
                string donViThoiGian = comboBox2.SelectedItem.ToString();

                ChartTitle chartTitle = new ChartTitle();
                chartTitle.Text = $"Tăng trưởng doanh thu theo {donViThoiGian.ToLower()}";
                chartTitle.Font = new Font("Tahoma", 14, FontStyle.Bold);
                chartTitle.TextColor = Color.FromArgb(0, 0, 0);
                chartTitle.Alignment = StringAlignment.Center;
                chartDoanhThu.Titles.Add(chartTitle);

                Series lineSeries = new Series("Tổng doanh thu theo thời gian", ViewType.Line);
                var lst = _thongke.DoanhThuDonViTheoThoiGian(tuNgay, denNgay, donViThoiGian);
                if (lst == null || !lst.Any())
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (var item in lst)
                {
                    if (item.NGAYDATPHONG == null || item.THANHTIEN == null)
                    {
                        MessageBox.Show("Dữ liệu không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    lineSeries.Points.Add(new SeriesPoint(item.NGAYDATPHONG, item.THANHTIEN));
                }

                lineSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                lineSeries.Label.TextPattern = "{V:n0}";
                chartDoanhThu.Series.Add(lineSeries);

                chartDoanhThu.Legend.Font = new Font("Tahoma", 10);
                chartDoanhThu.Legend.MarkerSize = new Size(10, 10);
                chartDoanhThu.Legend.Padding.All = 5;
                chartDoanhThu.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                chartDoanhThu.Legend.AlignmentVertical = LegendAlignmentVertical.Top;

                XYDiagram diagram = chartDoanhThu.Diagram as XYDiagram;
                if (diagram == null)
                {
                    MessageBox.Show("Biểu đồ không được cấu hình đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                diagram.AxisX.Title.Text = donViThoiGian;
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                switch (donViThoiGian.ToLower())
                {
                    case "ngày":
                        diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day;
                        diagram.AxisX.Label.TextPattern = "{A:dd/MM}";
                        break;
                    case "tuần":
                        diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Week;
                        diagram.AxisX.Label.TextPattern = "'Tuần' {A:dd/MM}";
                        break;
                    case "tháng":
                        diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month;
                        diagram.AxisX.Label.TextPattern = "{A:MM/yyyy}";
                        break;
                    case "quý":
                        diagram.AxisX.Label.TextPattern = "'Quý' Q/yyyy";
                        break;
                    case "năm":
                        diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Year;
                        diagram.AxisX.Label.TextPattern = "{A:yyyy}";
                        break;
                    default:
                        diagram.AxisX.Label.TextPattern = "{A}";
                        break;
                }

                diagram.AxisY.Title.Text = "Thành tiền (VNĐ)";
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Label.TextPattern = "{V:n0}";
            }
            else if (selected.Contains("Lượng khách theo thời gian"))
            {
                string donViThoiGian = comboBox2.SelectedItem.ToString();

                ChartTitle chartTitle = new ChartTitle();
                chartTitle.Text = $"Số người ở theo {donViThoiGian.ToLower()}";
                chartTitle.Font = new Font("Tahoma", 14, FontStyle.Bold);
                chartTitle.TextColor = Color.FromArgb(0, 0, 0);
                chartTitle.Alignment = StringAlignment.Center;
                chartDoanhThu.Titles.Add(chartTitle);

                Series areaSeries = new Series("Tổng số người ở theo thời gian", ViewType.Area);

                var lst = _thongke.SoNguoiOTheoThoiGian(tuNgay, denNgay, donViThoiGian);
                if (lst == null || !lst.Any())
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var item in lst)
                {
                    if (item.NGAYDATPHONG == null || item.NGUOIO == null)
                    {
                        MessageBox.Show("Dữ liệu không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    areaSeries.Points.Add(new SeriesPoint(item.NGAYDATPHONG, item.NGUOIO));
                }

                areaSeries.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                areaSeries.Label.TextPattern = "{V} người";
                chartDoanhThu.Series.Add(areaSeries);

                chartDoanhThu.Legend.Font = new Font("Tahoma", 10);
                chartDoanhThu.Legend.MarkerSize = new Size(10, 10);
                chartDoanhThu.Legend.Padding.All = 5;
                chartDoanhThu.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
                chartDoanhThu.Legend.AlignmentVertical = LegendAlignmentVertical.Top;

                XYDiagram diagram = chartDoanhThu.Diagram as XYDiagram;
                if (diagram == null)
                {
                    MessageBox.Show("Biểu đồ không được cấu hình đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                diagram.AxisX.Title.Text = donViThoiGian;
                diagram.AxisX.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;

                switch (donViThoiGian.ToLower())
                {
                    case "ngày":
                        diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Day;
                        diagram.AxisX.Label.TextPattern = "{A:dd/MM}";
                        break;
                    case "tuần":
                        diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Week;
                        diagram.AxisX.Label.TextPattern = "'Tuần' {A:dd/MM}";
                        break;
                    case "tháng":
                        diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Month;
                        diagram.AxisX.Label.TextPattern = "{A:MM/yyyy}";
                        break;
                    case "quý":
                        diagram.AxisX.Label.TextPattern = "'Quý' Q/yyyy";
                        break;
                    case "năm":
                        diagram.AxisX.DateTimeScaleOptions.MeasureUnit = DateTimeMeasureUnit.Year;
                        diagram.AxisX.Label.TextPattern = "{A:yyyy}";
                        break;
                    default:
                        diagram.AxisX.Label.TextPattern = "{A}";
                        break;
                }

                diagram.AxisY.Title.Text = "Số người";
                diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                diagram.AxisY.Label.TextPattern = "{V} người";
            }
        }
        

        private void btnXuat_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Image|*.png";
                sfd.Title = "Xuất ảnh biểu đồ";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    chartDoanhThu.ExportToImage(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    XtraMessageBox.Show("Xuất ảnh thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbBieuDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selected = cbBieuDo.SelectedItem.ToString();

            if (selected.Contains("theo thời gian"))
            {
                comboBox2.Visible = true;
                lbDonVi.Visible = true;
                comboBox2.Enabled = true;
            }
            else
            {
                comboBox2.Visible = false;
                lbDonVi.Visible = false;
                comboBox2.Enabled = false;
            }
        }
    }
}