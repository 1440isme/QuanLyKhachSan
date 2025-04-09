﻿using DataLayer;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Data.SqlClient;
using System.Drawing;
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
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultSettingsCompatibilityMode = DevExpress.XtraEditors.SettingsCompatibilityMode.Latest;
            DevExpress.XtraEditors.WindowsFormsSettings.AllowRoundedWindowCorners = DevExpress.Utils.DefaultBoolean.True; // Bo góc toàn bộ
            if (File.Exists("connectdb.dba"))
            {
                string conStr = "";
                BinaryFormatter bf = new BinaryFormatter();

                FileStream fs = new FileStream("connectdb.dba", FileMode.Open, FileAccess.Read, FileShare.Read);
                connect cp = (connect)bf.Deserialize(fs);
                

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
                SqlConnection con = new SqlConnection(conStr);
                try
                {
                    con.Open();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
                fs.Close();

                if (File.Exists("sysparam.ini"))
                {
                    Application.Run(new frmLogin());
                }
                else
                {
                    Application.Run(new frmSetParam());
                }
                //Application.Run(new frmMain()); // Chạy chương trình chính
            }
            else
            {
                Application.Run(new frmKetNoiDB());
                //MessageBox.Show("File kết nối cơ sở dữ liệu không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
