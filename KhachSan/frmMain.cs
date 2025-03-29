using BusinessLayer;
using DataLayer;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace KhachSan
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
        }
        TANG _tang = new TANG();
        FUNC _func = new FUNC();
        PHONG _phong = new PHONG();
        GalleryItem item = null;
        private void frmMain_Load(object sender, EventArgs e)
        {
            _tang = new TANG();
            _func = new FUNC();
            _phong = new PHONG();
            leftMenu();
            showRoom();
        }
        void leftMenu()
        {
            int i = 0;
            var _lsParent = _func.getParent();
            foreach (var _pr in _lsParent)
            {
                NavBarGroup navGroup = new NavBarGroup(_pr.DESCRIPTION);
                navGroup.Tag = _pr.FUNC_CODE;
                navGroup.Name = _pr.FUNC_CODE;
                navGroup.ImageOptions.LargeImageIndex = i;
                i++;
                navMain.Groups.Add(navGroup);

                var _lsChild = _func.getChild(_pr.FUNC_CODE);
                foreach (var _ch in _lsChild)
                {
                    NavBarItem navItem = new NavBarItem(_ch.DESCRIPTION);
                    navItem.Tag = _ch.FUNC_CODE;
                    navItem.Name = _ch.FUNC_CODE;
                    navItem.ImageOptions.SmallImageIndex = 0;
                    navGroup.ItemLinks.Add(navItem);
                }
            }
        }
        void showRoom()
        {
            gControl.Gallery.Groups.Clear(); // Clear existing groups

            // Khởi tạo lại để lấy dữ liệu mới nhất
            _tang = new TANG();
            _phong = new PHONG();

            var lsTang = _tang.getAll();
            gControl.Gallery.ItemImageLayout = ImageLayoutMode.ZoomInside;
            gControl.Gallery.ImageSize = new Size(64, 64);
            gControl.Gallery.ShowItemText = true;
            gControl.Gallery.ShowGroupCaption = true;
            foreach (var t in lsTang)
            {
                if (t.DISABLED == true) continue; // Skip disabled floors

                var galleryItem = new GalleryItemGroup();
                galleryItem.Caption = t.TENTANG;
                galleryItem.CaptionAlignment = GalleryItemGroupCaptionAlignment.Stretch;
                List<tb_Phong> lsPhong = _phong.getByTang(t.IDTANG);
                foreach (var p in lsPhong)
                {
                    var gc_item = new GalleryItem();
                    gc_item.Caption = p.TENPHONG;
                    gc_item.Value = p.IDPHONG;
                    if (p.TRANGTHAI == true)
                    {
                        gc_item.ImageOptions.Image = imageList3.Images[0];
                    }
                    else
                    {
                        gc_item.ImageOptions.Image = imageList3.Images[1];
                    }
                    galleryItem.Items.Add(gc_item);
                }
                gControl.Gallery.Groups.Add(galleryItem);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void navMain_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            string _funcCode = e.Link.Item.Tag.ToString();
            switch (_funcCode)
            {
                case "CONGTY":
                    {
                        frmCongTy _frm = new frmCongTy();
                        _frm.ShowDialog();
                        break;
                    }
                case "DONVI":
                    {
                        frmDonVi _frm = new frmDonVi();
                        _frm.ShowDialog();
                        break;
                    }
                case "LOAIPHONG":
                    {
                        frmLoaiPhong _frm = new frmLoaiPhong();
                        _frm.ShowDialog();
                        break;
                    }
                case "KHACHHANG":
                    {
                        frmKhachHang _frm = new frmKhachHang();
                        _frm.ShowDialog();
                        break;
                    }
                case "TANG":
                    {
                        frmTang _frm = new frmTang();
                        _frm.DataChanged += FrmTang_DataChanged;
                        _frm.ShowDialog();
                        break;
                    }
                case "PHONG":
                    {
                        frmPhong _frm = new frmPhong();
                        _frm.DataChanged += FrmPhong_DataChanged;

                        _frm.ShowDialog();
                        break;
                    }
                case "SANPHAM":
                    {
                        frmSanPham _frm = new frmSanPham();
                        _frm.ShowDialog();
                        break;
                    }
                case "THIETBI":
                    {
                        frmThietBi _frm = new frmThietBi();
                        _frm.ShowDialog();
                        break;
                    }
            }
        }
        private void FrmTang_DataChanged(object sender, EventArgs e)
        {
            showRoom();
        }
        private void FrmPhong_DataChanged(object sender, EventArgs e)
        {
            showRoom();
        }

        private void popupMenu1_Popup(object sender, EventArgs e)
        {
            Point point = gControl.PointToClient(Control.MousePosition);
            RibbonHitInfo hitInfo = gControl.CalcHitInfo(point);
            if (hitInfo.InGalleryItem || hitInfo.HitTest == RibbonHitTest.GalleryImage)
            {
                item = hitInfo.GalleryItem;
            }
        }

        private void btnDatPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var gc_item = new GalleryItem();
            string id = item.Value.ToString();
            MessageBox.Show(id);
        }
    }
}