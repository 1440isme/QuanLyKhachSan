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
    public partial class frmShowGroup : DevExpress.XtraEditors.XtraForm
    {
        public frmShowGroup()
        {
            InitializeComponent();
        }
        public string _macty;
        public string _madvi;
        public int _idUser;
        SYS_GROUP _sysgroup;
        VIEW_USER_IN_GROUP _vUserInGroup;
        frmUser objUser = (frmUser)Application.OpenForms["frmUser"];

        private void frmShowGroup_Load(object sender, EventArgs e)
        {
            _sysgroup = new SYS_GROUP();
            _vUserInGroup = new VIEW_USER_IN_GROUP();
            loadGroup();
        }
        void loadGroup()
        {
            gcNhom.DataSource = _vUserInGroup.getGroupByDonVi(_madvi, _macty);
            gvNhom.OptionsBehavior.Editable = false;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_vUserInGroup.checkGroupByUser(_idUser, int.Parse(gvNhom.GetFocusedRowCellValue("IDUSER").ToString())))
            {
                MessageBox.Show("Người dùng đã tồn tại trong nhóm. Vui lòng chọn nhóm khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            tb_SYS_GROUP gr = new tb_SYS_GROUP();
            gr.GROUP = int.Parse(gvNhom.GetFocusedRowCellValue("IDUSER").ToString());
            gr.MEMBER = _idUser;
            _sysgroup.add(gr);
            objUser.loadGroupByUser(_idUser);
            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}