using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class OBJ_PHONG
    {
        public int IDPHONG { get; set; }
        public string TENPHONG { get; set; }
        public double ?DONGIA { get; set; }
        public bool ?STATUS { get; set; }
        public int IDTANG { get; set; }
        public int IDLOAIPHONG { get; set; }
        public bool ?DISABLED { get; set; }
        public string TENTANG { get; set; }
        public string TENLOAIPHONG { get; set; }
        public int SONGUOI { get; set; }

    }
}
