using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KhachSan
{
    public class myFunctions
    {
        public static string _macty;
        public static string _madvi;
        public static string _srv;
        public static string _us;
        public static string _pw;
        public static string _db;
        static SqlConnection con = new SqlConnection();
        public static void taoketnoi()
        {
            con.ConnectionString = Program.connoi;
            try
            {
                con.Open();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void dongketnoi()
        {
            con.Close();
        }
        public static DataTable laydulieu(string qr)
        {

            taoketnoi();
            DataTable databl = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter();
            dap.SelectCommand = new SqlCommand(qr, con);
            dap.Fill(databl);
            dongketnoi();
            return databl;
        }
        public static DateTime GetFirstDayInMonth(int year, int month)
        {

            return new DateTime(year, month, 1);
        }
        
    }
}

