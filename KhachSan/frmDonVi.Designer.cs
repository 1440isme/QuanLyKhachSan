namespace KhachSan
{
    partial class frmDonVi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnThem = new System.Windows.Forms.ToolStripButton();
            this.btnSua = new System.Windows.Forms.ToolStripButton();
            this.btnXoa = new System.Windows.Forms.ToolStripButton();
            this.btnLuu = new System.Windows.Forms.ToolStripButton();
            this.btnBoQua = new System.Windows.Forms.ToolStripButton();
            this.btnThoat = new System.Windows.Forms.ToolStripButton();
            this.gcDanhSach = new DevExpress.XtraGrid.GridControl();
            this.gvDanhSach = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DISABLE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MADVI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TENDVI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DIENTHOAI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FAX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.EMAIL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DIACHI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cboCTY = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkDisabled = new System.Windows.Forms.CheckBox();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDienThoai = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhSach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhSach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnThem,
            this.btnSua,
            this.btnXoa,
            this.btnLuu,
            this.btnBoQua,
            this.btnThoat});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(878, 54);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnThem
            // 
            this.btnThem.Image = global::KhachSan.Properties.Resources._299068_add_sign_icon;
            this.btnThem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(42, 51);
            this.btnThem.Text = "Thêm";
            this.btnThem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Image = global::KhachSan.Properties.Resources._274899_option_settings_technology_adjustments_tools_icon;
            this.btnSua.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(36, 51);
            this.btnSua.Text = "Sửa";
            this.btnSua.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Image = global::KhachSan.Properties.Resources._9004715_cross_delete_remove_cancel_icon;
            this.btnXoa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(36, 51);
            this.btnXoa.Text = "Xoá";
            this.btnXoa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Image = global::KhachSan.Properties.Resources._285657_floppy_guardar_save_icon;
            this.btnLuu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(36, 51);
            this.btnLuu.Text = "Lưu";
            this.btnLuu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnBoQua
            // 
            this.btnBoQua.Image = global::KhachSan.Properties.Resources._9004750_arrow_right_next_direction_icon;
            this.btnBoQua.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(48, 51);
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Image = global::KhachSan.Properties.Resources._10588214_power_button_icon;
            this.btnThoat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(42, 51);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // gcDanhSach
            // 
            this.gcDanhSach.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcDanhSach.Location = new System.Drawing.Point(0, 54);
            this.gcDanhSach.MainView = this.gvDanhSach;
            this.gcDanhSach.Name = "gcDanhSach";
            this.gcDanhSach.Size = new System.Drawing.Size(878, 381);
            this.gcDanhSach.TabIndex = 2;
            this.gcDanhSach.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDanhSach});
            // 
            // gvDanhSach
            // 
            this.gvDanhSach.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.DISABLE,
            this.MADVI,
            this.TENDVI,
            this.DIENTHOAI,
            this.FAX,
            this.EMAIL,
            this.DIACHI});
            this.gvDanhSach.GridControl = this.gcDanhSach;
            this.gvDanhSach.Name = "gvDanhSach";
            this.gvDanhSach.OptionsView.ShowGroupPanel = false;
            this.gvDanhSach.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvDanhSach_CustomDrawCell);
            this.gvDanhSach.Click += new System.EventHandler(this.gvDanhSach_Click);
            // 
            // DISABLE
            // 
            this.DISABLE.Caption = "DEL";
            this.DISABLE.FieldName = "DISABLE";
            this.DISABLE.MaxWidth = 70;
            this.DISABLE.MinWidth = 25;
            this.DISABLE.Name = "DISABLE";
            this.DISABLE.Visible = true;
            this.DISABLE.VisibleIndex = 0;
            this.DISABLE.Width = 35;
            // 
            // MADVI
            // 
            this.MADVI.Caption = "MÃ ĐƠN VỊ";
            this.MADVI.FieldName = "MADVI";
            this.MADVI.MaxWidth = 70;
            this.MADVI.MinWidth = 50;
            this.MADVI.Name = "MADVI";
            this.MADVI.Width = 70;
            // 
            // TENDVI
            // 
            this.TENDVI.Caption = "TÊN ĐƠN VỊ";
            this.TENDVI.FieldName = "TENDVI";
            this.TENDVI.MaxWidth = 300;
            this.TENDVI.Name = "TENDVI";
            this.TENDVI.Visible = true;
            this.TENDVI.VisibleIndex = 1;
            // 
            // DIENTHOAI
            // 
            this.DIENTHOAI.Caption = "ĐIỆN THOẠI";
            this.DIENTHOAI.FieldName = "DIENTHOAI";
            this.DIENTHOAI.MaxWidth = 120;
            this.DIENTHOAI.MinWidth = 80;
            this.DIENTHOAI.Name = "DIENTHOAI";
            this.DIENTHOAI.Visible = true;
            this.DIENTHOAI.VisibleIndex = 2;
            this.DIENTHOAI.Width = 80;
            // 
            // FAX
            // 
            this.FAX.Caption = "FAX";
            this.FAX.FieldName = "FAX";
            this.FAX.MaxWidth = 100;
            this.FAX.MinWidth = 80;
            this.FAX.Name = "FAX";
            this.FAX.Visible = true;
            this.FAX.VisibleIndex = 3;
            this.FAX.Width = 100;
            // 
            // EMAIL
            // 
            this.EMAIL.Caption = "EMAIL";
            this.EMAIL.FieldName = "EMAIL";
            this.EMAIL.MaxWidth = 300;
            this.EMAIL.MinWidth = 100;
            this.EMAIL.Name = "EMAIL";
            this.EMAIL.Visible = true;
            this.EMAIL.VisibleIndex = 4;
            this.EMAIL.Width = 100;
            // 
            // DIACHI
            // 
            this.DIACHI.Caption = "ĐỊA CHỈ";
            this.DIACHI.FieldName = "DIACHI";
            this.DIACHI.Name = "DIACHI";
            this.DIACHI.Visible = true;
            this.DIACHI.VisibleIndex = 5;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cboCTY);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.txtMa);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.chkDisabled);
            this.groupControl1.Controls.Add(this.txtDiaChi);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.txtEmail);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.txtFax);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.txtDienThoai);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.txtTen);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 435);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(878, 236);
            this.groupControl1.TabIndex = 3;
            this.groupControl1.Text = "Thông tin";
            // 
            // cboCTY
            // 
            this.cboCTY.FormattingEnabled = true;
            this.cboCTY.Location = new System.Drawing.Point(176, 47);
            this.cboCTY.Name = "cboCTY";
            this.cboCTY.Size = new System.Drawing.Size(528, 21);
            this.cboCTY.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(29, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Công ty/Chi nhánh";
            // 
            // txtMa
            // 
            this.txtMa.Location = new System.Drawing.Point(127, 89);
            this.txtMa.Name = "txtMa";
            this.txtMa.Size = new System.Drawing.Size(210, 21);
            this.txtMa.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(29, 89);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Mã DVI";
            // 
            // chkDisabled
            // 
            this.chkDisabled.AutoSize = true;
            this.chkDisabled.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDisabled.Location = new System.Drawing.Point(742, 90);
            this.chkDisabled.Name = "chkDisabled";
            this.chkDisabled.Size = new System.Drawing.Size(88, 23);
            this.chkDisabled.TabIndex = 10;
            this.chkDisabled.Text = "Disabled";
            this.chkDisabled.UseVisualStyleBackColor = true;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(127, 169);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(703, 21);
            this.txtDiaChi.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Địa chỉ";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(639, 127);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(191, 21);
            this.txtEmail.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(588, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Email";
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(381, 127);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(191, 21);
            this.txtFax.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(345, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fax";
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Location = new System.Drawing.Point(127, 127);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(212, 21);
            this.txtDienThoai.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Điện thoại";
            // 
            // txtTen
            // 
            this.txtTen.Location = new System.Drawing.Point(381, 90);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(323, 21);
            this.txtTen.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(343, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tên";
            // 
            // frmDonVi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 671);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.gcDanhSach);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmDonVi";
            this.Text = "Danh mục Đơn vị";
            this.Load += new System.EventHandler(this.frmDonVi_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhSach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhSach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnThem;
        private System.Windows.Forms.ToolStripButton btnSua;
        private System.Windows.Forms.ToolStripButton btnXoa;
        private System.Windows.Forms.ToolStripButton btnLuu;
        private System.Windows.Forms.ToolStripButton btnBoQua;
        private System.Windows.Forms.ToolStripButton btnThoat;
        private DevExpress.XtraGrid.GridControl gcDanhSach;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDanhSach;
        private DevExpress.XtraGrid.Columns.GridColumn DISABLE;
        private DevExpress.XtraGrid.Columns.GridColumn MADVI;
        private DevExpress.XtraGrid.Columns.GridColumn TENDVI;
        private DevExpress.XtraGrid.Columns.GridColumn DIENTHOAI;
        private DevExpress.XtraGrid.Columns.GridColumn FAX;
        private DevExpress.XtraGrid.Columns.GridColumn EMAIL;
        private DevExpress.XtraGrid.Columns.GridColumn DIACHI;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.TextBox txtMa;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkDisabled;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDienThoai;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCTY;
        private System.Windows.Forms.Label label7;
    }
}