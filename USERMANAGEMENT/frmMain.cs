﻿using BusinessLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using USERMANAGEMENT.MyComponents;


namespace USERMANAGEMENT
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
        }
        MyTreeViewCombo _treeView;
        CONGTY _congty;
        DONVI _donvi;
        SYS_USER _sysuser;
        string _macty;
        string _madvi;
        bool _isRoot;
        private void frmMain_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            _donvi = new DONVI();
            _sysuser = new SYS_USER();
            loadTreeView();
            loadUser("CTME", "KSCB");
        }
        public void loadUser(string macty, string madvi)
        {
            _sysuser = new SYS_USER();
            gcUser.DataSource = _sysuser.getUserByDVI(macty, madvi);
            gvUser.OptionsBehavior.Editable = false;
        }
        void loadTreeView()
        {
            _treeView = new MyTreeViewCombo(pnNhom.Width, 300);
            _treeView.Font = new Font("Tahoma", 10, FontStyle.Bold);
            var lstCTY = _congty.getAll();
            foreach ( var item in lstCTY )
            {
                TreeNode ParentNode = new TreeNode();
                ParentNode.Text = item.MACTY + " - " + item.TENCTY;
                ParentNode.Tag = item.MACTY;
                ParentNode.Name = item.MACTY;
                _treeView.TreeView.Nodes.Add(ParentNode);
                foreach (var dv in _donvi.getAll(item.MACTY))
                {
                    TreeNode ChildNode = new TreeNode();
                    ChildNode.Text = dv.MADVI + " - " + dv.TENDVI;
                    ChildNode.Tag = dv.MACTY+ "." + dv.MADVI;
                    ChildNode.Name = dv.MACTY + "." + dv.MADVI;
                    ParentNode.Nodes.Add(ChildNode);
                }
            }
            _treeView.TreeView.ExpandAll();
            pnNhom.Controls.Add(_treeView);
            _treeView.Width = pnNhom.Width;
            _treeView.Height = pnNhom.Height;
            _treeView.TreeView.AfterSelect += TreeView_AfterSelect;
            _treeView.TreeView.Click += TreeView_Click;
        }

        private void TreeView_Click(object sender, EventArgs e)
        {
            _treeView.dropDown.Focus();
            _treeView.SelectAll();
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _treeView.Text = _treeView.TreeView.SelectedNode.Text;
            if (_treeView.TreeView.SelectedNode.Parent == null)
            {
                _isRoot = true;
                _macty = _treeView.TreeView.SelectedNode.Tag.ToString();
                _madvi = "~";
            }
            else
            {
                _isRoot = false;
                _macty = _treeView.TreeView.SelectedNode.Name.Substring(0, 4);
                _madvi = _treeView.TreeView.SelectedNode.Name.Substring(5);
            }
            loadUser(_macty, _madvi);
            _treeView.dropDown.Close();
           
        }

        private void btnGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_treeView.Text== "")
            {
                XtraMessageBox.Show("Vui lòng chọn đơn vị. ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
                return;

            }
            frmGroup frm = new frmGroup();
            frm._them = true;
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm.ShowDialog();
        }

        private void btnUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_treeView.Text == "")
            {
                XtraMessageBox.Show("Vui lòng chọn đơn vị. ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmUser frm = new frmUser();
            frm._them = true;
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm.ShowDialog();
        }

        private void btnCapNhat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvUser.RowCount > 0 && gvUser.GetFocusedRowCellValue("ISGROUP").Equals(true))
            {
                frmGroup frm = new frmGroup();
                frm._them = false;
                frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
                frm.ShowDialog();
            }
            else
            {
                frmUser frm = new frmUser();
                frm._them = false;
                frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
                frm.ShowDialog();
            }
        }

        private void btnChucNang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPhanQuyenChucNang frm = new frmPhanQuyenChucNang();
            frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm.ShowDialog();
        }

        private void btnBaoCao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPhanQuyenBaoCao frm = new frmPhanQuyenBaoCao();
            frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm.ShowDialog();
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void gvUser_DoubleClick(object sender, EventArgs e)
        {
            if (gvUser.RowCount > 0 && gvUser.GetFocusedRowCellValue("ISGROUP").Equals(true)  )
            {
                frmGroup frm = new frmGroup();
                frm._them = false;
                frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
                frm.ShowDialog();
            }
            else
            {
                frmUser frm = new frmUser();
                frm._them = false;
                frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
                frm.ShowDialog();
            }
        }

        private void gvUser_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
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
            if (e.Column.Name == "DISABLED")
            {
                if (e.CellValue != null && bool.Parse(e.CellValue.ToString()))
                {
                    Image img = Properties.Resources.cancel_16x16;
                    e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                    e.Handled = true;
                }
            }
        }
    }
}
