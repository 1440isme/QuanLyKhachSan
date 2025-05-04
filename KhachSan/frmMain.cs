using BusinessLayer;
using DataLayer;
using DevExpress.Utils.Drawing;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using USERMANAGEMENT;

namespace KhachSan
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        public frmMain(tb_SYS_USER user)
        {
            InitializeComponent();
            this._user = user;
            this.Text = "PHẦN MỀM QUẢN LÝ KHÁCH SẠN - " + user.FULLNAME;
            this.Icon = Properties.Resources.Hotel1;
        }
        tb_SYS_USER _user;
        TANG _tang = new TANG();
        SYS_FUNC _func = new SYS_FUNC();
        SYS_GROUP _sysGroup;
        SYS_RIGHT _sysRight;
        PHONG _phong = new PHONG();
        DATPHONG _datphong = new DATPHONG();
        GalleryItem item = null;

        private void frmMain_Load(object sender, EventArgs e)
        {
            _tang = new TANG();
            _func = new SYS_FUNC();
            _phong = new PHONG();
            _sysGroup = new SYS_GROUP();
            _sysRight = new SYS_RIGHT();
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
                navGroup.Expanded = true;
            }
        }
        public void showRoom()
        {
            gControl.Gallery.Groups.Clear();

            _tang = new TANG();
            _phong = new PHONG();

            var lsTang = _tang.getAll();
            gControl.Gallery.ItemImageLayout = ImageLayoutMode.ZoomInside;
            gControl.Gallery.ImageSize = new Size(64, 64);
            gControl.Gallery.ShowItemText = true;
            gControl.Gallery.ShowGroupCaption = true;
            foreach (var t in lsTang)
            {
                if (t.DISABLED == true) continue;

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
            this.Close();
        }

        private void navMain_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            string _funcCode = e.Link.Item.Tag.ToString();

            var _group = _sysGroup.getGroupByMember(_user.IDUSER);
            var _uRight = _sysRight.getRight(_user.IDUSER, _funcCode);
            if (_group!=null)
            {
                var _groupRight = _sysRight.getRight(_group.GROUP, _funcCode);
                if (_uRight.USER_RIGHT < _groupRight.USER_RIGHT)
                    _uRight.USER_RIGHT = _groupRight.USER_RIGHT;
            }
            if (_uRight.USER_RIGHT == 0)
            {
                XtraMessageBox.Show("Không có quyền thao tác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                switch (_funcCode)
                {
                    case "CONGTY":
                        {
                            frmCongTy _frm = new frmCongTy(_user, _uRight.USER_RIGHT.Value);
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
                    case "PHONG_THIETBI":
                        {
                            frmPhongThietBi _frm = new frmPhongThietBi();
                            _frm.ShowDialog();
                            break;
                        }
                    case "DATPHONG":
                        {
                            frmDatPhong _frm = new frmDatPhong();
                            _frm.IDUSER = _user.IDUSER; // Truyền IDUSER
                            _frm.ShowDialog();
                            break;
                        }
                    case "NGUOIDUNG":
                        {
                            USERMANAGEMENT.formMain _frm = new USERMANAGEMENT.formMain();
                            _frm.ShowDialog();
                            break;
                        }
                    case "NGANHANG":
                        {
                            frmThongTinNganHang _frm = new frmThongTinNganHang();
                            _frm.ShowDialog();
                            break;
                        }
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
            try
            {
                if (_phong.checkExist(int.Parse(item.Value.ToString())))
                {
                    MessageBox.Show("Phòng đã được đặt. Vui lòng chọn phòng khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                frmDatPhongDon frm = new frmDatPhongDon();
                frm._idPhong = int.Parse(item.Value.ToString());
                frm._them = true;
                frm.IDUSER = _user.IDUSER; 
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChuyenPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_phong.checkExist(int.Parse(item.Value.ToString())))
            {
                MessageBox.Show("Phòng chưa đặt nên không được chuyển. Vui lòng chọn phòng đã đặt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var groupInfo = _datphong.GetRoomGroupBookingInfo(int.Parse(item.Value.ToString()));
            if (groupInfo.IsGroupBooking)
            {
                MessageBox.Show($"Đơn hàng này đặt theo đoàn (Đoàn: {groupInfo.GroupName}).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmChuyenPhong frm = new frmChuyenPhong();
            frm._idPhong = int.Parse(item.Value.ToString());
            frm.IDUSER = _user.IDUSER; 
            frm.ShowDialog();
        }

        private void btnSPDV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_phong.checkExist(int.Parse(item.Value.ToString())))
            {
                MessageBox.Show("Phòng chưa được đặt. Vui lòng chọn phòng đã được đặt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var groupInfo = _datphong.GetRoomGroupBookingInfo(int.Parse(item.Value.ToString()));
            if (groupInfo.IsGroupBooking)
            {
                MessageBox.Show($"Đơn hàng này đặt theo đoàn (Mã đơn: {groupInfo.IDDP}).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmDatPhongDon frm = new frmDatPhongDon();
            frm._idPhong = int.Parse(item.Value.ToString());
            frm._them = false;
            frm.IDUSER = _user.IDUSER; // Truyền IDUSER
            frm.ShowDialog();
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_phong.checkExist(int.Parse(item.Value.ToString())))
            {
                MessageBox.Show("Phòng chưa được đặt. Vui lòng chọn phòng đã được đặt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var groupInfo = _datphong.GetRoomGroupBookingInfo(int.Parse(item.Value.ToString()));
            if (groupInfo.IsGroupBooking)
            {
                MessageBox.Show($"Đơn hàng này đặt theo đoàn (Mã đơn: {groupInfo.IDDP}).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmDatPhongDon frm = new frmDatPhongDon();
            frm._idPhong = int.Parse(item.Value.ToString());
            frm._them = false;
            frm.IDUSER = _user.IDUSER; // Truyền IDUSER
            frm.ShowDialog();
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            frmBaoCao frm = new frmBaoCao(_user);
            frm.ShowDialog();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}