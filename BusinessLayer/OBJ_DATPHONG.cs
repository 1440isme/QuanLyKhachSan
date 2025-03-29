using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class OBJ_DATPHONG
    {
        //public int ID { get; set; }
        //public string TENPHONG { get; set; }
        //public double DONGIA { get; set; }
        //public bool STATUS { get; set; }
        //public int IDTANG { get; set; }
        //public int IDLOAIPHONG { get; set; }
        //public bool DISABLED { get; set; }
        //public string TENTANG { get; set; }
        //public string TENLOAIPHONG { get; set; }
        public int IDDP { get; set; }
        public int IDKH { get; set; }
        public string HOTEN { get; set; }
        public DateTime NGAYDATPHONG { get; set; }
        public DateTime NGAYTRAPHONG { get; set; }
        public double SOTIEN { get; set; }
        public int SONGUOIO { get; set; }
        public int IDUSER { get; set; }
        public string MACTY { get; set; }
        public string MADVI { get; set; }
        public bool STATUS { get; set; }
        public bool THEODOAN { get; set; }
        public bool DISABLED { get; set; }
        public string GHICHU { get; set; }
    }
}
