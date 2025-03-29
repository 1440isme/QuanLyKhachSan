using DataLayer;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace KhachSan
{
    static class Program
    {
        public static string connoi; // Biến kết nối toàn cục

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");

            if (File.Exists("connectdb.dba"))
            {
                string conStr = "";

                try
                {
                    connect cp;
                    using (FileStream fs = new FileStream("connectdb.dba", FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        BinaryFormatter bf = new BinaryFormatter();
                        cp = (connect)bf.Deserialize(fs);
                    } // File tự động đóng khi ra khỏi `using`

                    string servername = Encryptor.Decrypt(cp.servername, "qwertyuiop", true);
                    string username = Encryptor.Decrypt(cp.username, "qwertyuiop", true);
                    string pass = Encryptor.Decrypt(cp.passwd, "qwertyuiop", true);
                    string database = Encryptor.Decrypt(cp.database, "qwertyuiop", true);

                    conStr = $"Data Source={servername};Initial Catalog={database};User ID={username};Password={pass};";
                    connoi = conStr;

                    myFunctions._srv = servername;
                    myFunctions._us = username;
                    myFunctions._pw = pass;
                    myFunctions._db = database;

                    // Kiểm tra kết nối SQL
                    using (SqlConnection con = new SqlConnection(conStr))
                    {
                        con.Open(); // Mở kết nối
                    } // Kết nối tự động đóng
                    DevExpress.XtraEditors.WindowsFormsSettings.DefaultSettingsCompatibilityMode = DevExpress.XtraEditors.SettingsCompatibilityMode.Latest;
                    DevExpress.XtraEditors.WindowsFormsSettings.AllowRoundedWindowCorners = DevExpress.Utils.DefaultBoolean.True; // Bo góc toàn bộ

                    Application.Run(new frmMain()); // Chạy chương trình chính
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("File kết nối cơ sở dữ liệu không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
