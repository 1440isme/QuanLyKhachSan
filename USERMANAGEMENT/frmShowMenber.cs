using BusinessLayer;
using DataLayer;
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

namespace USERMANAGEMENT
{
    public partial class frmShowMenber : DevExpress.XtraEditors.XtraForm
    {
        public frmShowMenber()
        {
            InitializeComponent();
        }
        public string _macty;
        public string _madvi;
        public int _idGroup;
        SYS_GROUP _sysgroup;
        VIEW_USER_NOTIN_GROUP _vNotGroup;
        frmGroup objGroup = (frmGroup)Application.OpenForms["frmGroup"];
        private void frmShowMenber_Load(object sender, EventArgs e)
        {
            _sysgroup = new SYS_GROUP();
            _vNotGroup = new VIEW_USER_NOTIN_GROUP();
            loadUserNotInGroup();
        }
        void loadUserNotInGroup()
        {
            gcThanhVien.DataSource = _vNotGroup.getUserNotInGroup(_madvi, _macty);
            gvThanhVien.OptionsBehavior.Editable = false;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            tb_SYS_GROUP gr = new tb_SYS_GROUP();
            gr.GROUP = _idGroup;
            gr.MEMBER = int.Parse(gvThanhVien.GetFocusedRowCellValue("IDUSER").ToString());
            _sysgroup.add(gr);
            objGroup.loadUserInGroup(_idGroup);
            this.Close();

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}