using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace USERMANAGEMENT
{
    public partial class frmPhanQuyenChucNang : DevExpress.XtraEditors.XtraForm
    {
        public frmPhanQuyenChucNang()
        {
            InitializeComponent();
        }
        public int _idUser;
        public string _macty;
        public string _madvi;
        SYS_USER _sysuser;
        

        SYS_RIGHT _sysRight;
        public frmPhanQuyenChucNang(int idUser) 
        {
            InitializeComponent();
            _idUser = idUser; 
        }
        private void frmPhanQuyenChucNang_Load(object sender, EventArgs e)
        {
            _sysuser = new SYS_USER();
            _sysRight = new SYS_RIGHT();
            loadUser();
            loadFuncByUser();
            gvChucNang.RowStyle += gvChucNang_RowStyle;
        }

        private void gvChucNang_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                bool isRed = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, view.Columns["ISGROUP"]));
                if (isRed)
                {
                    e.Appearance.BackColor = Color.DeepSkyBlue;
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.Font = new Font("Tahoma",12, FontStyle.Bold);
                }
                
            }
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
        void loadFuncByUser()
        {
            VIEW_FUNC_SYS_RIGHT _vfuncRight = new VIEW_FUNC_SYS_RIGHT();
            gcChucNang.DataSource = _vfuncRight.getFuncByUser(_idUser);
            gvChucNang.OptionsBehavior.Editable = false;
            for (int i = 0; i < gvUsers.RowCount; i++)
            {
                if (int.Parse(gvUsers.GetRowCellValue(i, "IDUSER").ToString())== _idUser)
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
                    _sysRight.update(_idUser, gvChucNang.GetRowCellValue(i, "FUNC_CODE").ToString(), 0);
                }
            }
            loadFuncByUser();
        }

        private void btnChiXem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChucNang.RowCount; i++)
            {
                if (gvChucNang.IsRowSelected(i))
                {
                    _sysRight.update(_idUser, gvChucNang.GetRowCellValue(i, "FUNC_CODE").ToString(), 1);
                }
            }
            loadFuncByUser();
        }

        private void btnToanfQuyen_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChucNang.RowCount; i++)
            {
                if (gvChucNang.IsRowSelected(i))
                {
                    _sysRight.update(_idUser, gvChucNang.GetRowCellValue(i, "FUNC_CODE").ToString(), 2);
                }
            }
            loadFuncByUser();
        }

        private void gvUsers_Click(object sender, EventArgs e)
        {
            _idUser = int.Parse(gvUsers.GetFocusedRowCellValue("IDUSER").ToString());
            loadFuncByUser();
        }
    }
}