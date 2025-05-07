using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class THONGKE
    {
        Entities db;

        public THONGKE()
        {
            db = Entities.CreateEntities();
        }

        public List<OBJ_DOANHTHUDV> DoanhThuDonViTheoThoiGian(DateTime ngayd, DateTime ngayc, string donViThoiGian)
        {
            var dailyData = db.FN_DOANHTHU_THEOTG(ngayd, ngayc, donViThoiGian).ToList();
            List<OBJ_DOANHTHUDV> result = new List<OBJ_DOANHTHUDV>();

            if (donViThoiGian.Equals("Ngày", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var day in dailyData)
                {
                    result.Add(new OBJ_DOANHTHUDV
                    {
                        NGAYDATPHONG = day.NGAYDATPHONG,
                        THANHTIEN = day.THANHTIEN
                    });
                }
            }
            else if (donViThoiGian.Equals("Tuần", StringComparison.OrdinalIgnoreCase))
            {
                var groups = dailyData
                                .GroupBy(x => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                    x.NGAYDATPHONG.Value,
                                    CalendarWeekRule.FirstDay,
                                    DayOfWeek.Monday           
                                )).OrderBy(g => g.Key);

                foreach (var grp in groups)
                {
                    DateTime groupWeekStart = grp.Min(x => x.NGAYDATPHONG.Value);
                    DateTime groupWeekEnd = groupWeekStart.AddDays(6);
                    DateTime effectiveStart = groupWeekStart < ngayd ? ngayd : groupWeekStart;
                    DateTime effectiveEnd = groupWeekEnd > ngayc ? ngayc : groupWeekEnd;
                    result.Add(new OBJ_DOANHTHUDV
                    {
                        NGAYDATPHONG = effectiveStart,
                        THANHTIEN = grp.Sum(x => x.THANHTIEN)
                    });
                }
            }
            else if (donViThoiGian.Equals("Tháng", StringComparison.OrdinalIgnoreCase))
            {
                var groups = dailyData
                                .GroupBy(x => new { Year = x.NGAYDATPHONG.Value.Year, Month = x.NGAYDATPHONG.Value.Month })
                                .OrderBy(g => new DateTime(g.Key.Year, g.Key.Month, 1));

                foreach (var grp in groups)
                {
                    DateTime groupMonthStart = new DateTime(grp.Key.Year, grp.Key.Month, 1);
                    DateTime groupMonthEnd = groupMonthStart.AddMonths(1).AddDays(-1);
                    DateTime effectiveStart = groupMonthStart < ngayd ? ngayd : groupMonthStart;
                    DateTime effectiveEnd = groupMonthEnd > ngayc ? ngayc : groupMonthEnd;

                    result.Add(new OBJ_DOANHTHUDV
                    {
                        NGAYDATPHONG = effectiveStart,
                        THANHTIEN = grp.Sum(x => x.THANHTIEN)
                    });
                }
            }
            else if (donViThoiGian.Equals("Quý", StringComparison.OrdinalIgnoreCase))
            {
                var groups = dailyData
                                .GroupBy(x => new { Year = x.NGAYDATPHONG.Value.Year, Quarter = (x.NGAYDATPHONG.Value.Month - 1) / 3 + 1 })
                                .OrderBy(g => new DateTime(g.Key.Year, (g.Key.Quarter - 1) * 3 + 1, 1));
                foreach (var grp in groups)
                {
                    DateTime groupQuarterStart = new DateTime(grp.Key.Year, (grp.Key.Quarter - 1) * 3 + 1, 1);
                    DateTime groupQuarterEnd = groupQuarterStart.AddMonths(3).AddDays(-1);
                    DateTime effectiveStart = groupQuarterStart < ngayd ? ngayd : groupQuarterStart;
                    DateTime effectiveEnd = groupQuarterEnd > ngayc ? ngayc : groupQuarterEnd;
                    result.Add(new OBJ_DOANHTHUDV
                    {
                        NGAYDATPHONG = effectiveStart,
                        THANHTIEN = grp.Sum(x => x.THANHTIEN)
                    });
                }
            }
            else if (donViThoiGian.Equals("Năm", StringComparison.OrdinalIgnoreCase))
            {
                var groups = dailyData.GroupBy(x => x.NGAYDATPHONG.Value.Year).OrderBy(g => g.Key);

                foreach (var grp in groups)
                {
                    DateTime groupYearStart = new DateTime(grp.Key, 1, 1);
                    DateTime groupYearEnd = new DateTime(grp.Key, 12, 31);
                    DateTime effectiveStart = groupYearStart < ngayd ? ngayd : groupYearStart;
                    DateTime effectiveEnd = groupYearEnd > ngayc ? ngayc : groupYearEnd;

                    result.Add(new OBJ_DOANHTHUDV
                    {
                        NGAYDATPHONG = effectiveStart,
                        THANHTIEN = grp.Sum(x => x.THANHTIEN)
                    });
                }
            }
            else
            {
                throw new ArgumentException("Đơn vị thời gian không hợp lệ. Vui lòng chọn 'Ngày', 'Tuần', 'Tháng', 'Quý' hoặc 'Năm'.");
            }
            return result;
        }

        public List<OBJ_DOANHTHUPHONG> DoanhThuTheoPhong(DateTime ngayd, DateTime ngayc)
        {
            OBJ_DOANHTHUPHONG dp;
            List<OBJ_DOANHTHUPHONG> lstDoanhThuPhong = new List<OBJ_DOANHTHUPHONG>();
            var lstPhong = db.FN_DOANHTHU_THEOPHONG(ngayd, ngayc).ToList();

            foreach (var phong in lstPhong)
            {
                dp = new OBJ_DOANHTHUPHONG();
                dp.IDPHONG = phong.IDPHONG;
                dp.TENPHONG = phong.TENPHONG;
                dp.THANHTIEN = phong.THANHTIEN;

                lstDoanhThuPhong.Add(dp);
            }
            return lstDoanhThuPhong;
        }
    }
}
