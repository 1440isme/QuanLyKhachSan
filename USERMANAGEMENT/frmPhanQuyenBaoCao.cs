using BusinessLayer;
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
    public partial class frmPhanQuyenBaoCao : DevExpress.XtraEditors.XtraForm
    {
        public frmPhanQuyenBaoCao()
        {
            InitializeComponent();
        }
        public int _idUser;
        public string _macty;
        public string _madvi;
        SYS_USER _sysuser;
        SYS_RIGHT_REP _sysRightRep;
        private void frmPhanQuyenBaoCaocs_Load(object sender, EventArgs e)
        {
            _sysuser = new SYS_USER();
            _sysRightRep = new SYS_RIGHT_REP();
            loadUser();
            loadRepByUser();
        }
        void loadUser()
        {
            if (_macty == null && _madvi == null)
            {
                gcUsers.DataSource = _sysuser.getUserByDViFunc("CTME", "~");
                gvUsers.OptionsBehavior.Editable = false;
            }
            else
            {
                gcUsers.DataSource = _sysuser.getUserByDViFunc(_macty, _madvi);
                gvUsers.OptionsBehavior.Editable = false;
            }
        }
        void loadRepByUser()
        {
            VIEW_REP_SYS_RIGHT_REP _vfuncRight = new VIEW_REP_SYS_RIGHT_REP();
            gcChucNang.DataSource = _vfuncRight.getRepByUser(_idUser);
            gvChucNang.OptionsBehavior.Editable = false;
            for (int i = 0; i < gvUsers.RowCount; i++)
            {
                if (int.Parse(gvUsers.GetRowCellValue(i, "IDUSER").ToString()) == _idUser)
                {
                    gvUsers.ClearSelection();
                    gvUsers.FocusedRowHandle = i;
                }
            }
            gvUsers.ClearSelection();

        }

        private void gvUsers_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "ISGROUP")
            {
                if (e.CellValue != null && bool.TryParse(e.CellValue.ToString(), out bool isGroup))
                {
                    Image img = isGroup ? Properties.Resources.usergroup_16x16 : Properties.Resources.employee_16x16;
                    e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                    e.Handled = true;
                }
            }
        }

        private void btnCamQuyen_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChucNang.RowCount; i++)
            {
                if (gvChucNang.IsRowSelected(i))
                {
                    _sysRightRep.update(_idUser, int.Parse(gvChucNang.GetRowCellValue(i, "REP_CODE").ToString()), false);
                }
            }
            loadRepByUser();
        }

        private void btnToanfQuyen_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChucNang.RowCount; i++)
            {
                if (gvChucNang.IsRowSelected(i))
                {
                    _sysRightRep.update(_idUser, int.Parse(gvChucNang.GetRowCellValue(i, "REP_CODE").ToString()), true );
                }
            }
            loadRepByUser();
        }

        private void gvUsers_Click(object sender, EventArgs e)
        {
            _idUser = int.Parse(gvUsers.GetFocusedRowCellValue("IDUSER").ToString());
            loadRepByUser();
        }
    }
}