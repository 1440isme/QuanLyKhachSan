namespace USERMANAGEMENT
{
    partial class frmUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUser));
            this.tabUser = new DevExpress.XtraTab.XtraTabControl();
            this.pageUser = new DevExpress.XtraTab.XtraTabPage();
            this.chkDisabled = new System.Windows.Forms.CheckBox();
            this.txtRePass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pageGroup = new DevExpress.XtraTab.XtraTabPage();
            this.btnBot = new DevExpress.XtraEditors.SimpleButton();
            this.btnThem = new DevExpress.XtraEditors.SimpleButton();
            this.gcThanhVien = new DevExpress.XtraGrid.GridControl();
            this.gvThanhVien = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.IDUSER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.USERNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FULLNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tabUser)).BeginInit();
            this.tabUser.SuspendLayout();
            this.pageUser.SuspendLayout();
            this.pageGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcThanhVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvThanhVien)).BeginInit();
            this.SuspendLayout();
            // 
            // tabUser
            // 
            this.tabUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabUser.Location = new System.Drawing.Point(0, 0);
            this.tabUser.Name = "tabUser";
            this.tabUser.SelectedTabPage = this.pageUser;
            this.tabUser.Size = new System.Drawing.Size(584, 226);
            this.tabUser.TabIndex = 0;
            this.tabUser.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.pageUser,
            this.pageGroup});
            // 
            // pageUser
            // 
            this.pageUser.Controls.Add(this.chkDisabled);
            this.pageUser.Controls.Add(this.txtRePass);
            this.pageUser.Controls.Add(this.label3);
            this.pageUser.Controls.Add(this.txtPass);
            this.pageUser.Controls.Add(this.lblPass);
            this.pageUser.Controls.Add(this.txtHoTen);
            this.pageUser.Controls.Add(this.label2);
            this.pageUser.Controls.Add(this.txtUsername);
            this.pageUser.Controls.Add(this.label1);
            this.pageUser.Name = "pageUser";
            this.pageUser.Size = new System.Drawing.Size(582, 201);
            this.pageUser.Text = "Thông tin";
            // 
            // chkDisabled
            // 
            this.chkDisabled.AutoSize = true;
            this.chkDisabled.Font = new System.Drawing.Font("Tahoma", 10F);
            this.chkDisabled.Location = new System.Drawing.Point(178, 161);
            this.chkDisabled.Name = "chkDisabled";
            this.chkDisabled.Size = new System.Drawing.Size(99, 21);
            this.chkDisabled.TabIndex = 10;
            this.chkDisabled.Text = "Vô hiệu hoá";
            this.chkDisabled.UseVisualStyleBackColor = true;
            // 
            // txtRePass
            // 
            this.txtRePass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRePass.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtRePass.Location = new System.Drawing.Point(178, 118);
            this.txtRePass.Name = "txtRePass";
            this.txtRePass.Size = new System.Drawing.Size(268, 24);
            this.txtRePass.TabIndex = 9;
            this.txtRePass.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(41, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Nhập lại mật khẩu";
            // 
            // txtPass
            // 
            this.txtPass.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPass.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPass.Location = new System.Drawing.Point(178, 84);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(268, 24);
            this.txtPass.TabIndex = 7;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPass.Location = new System.Drawing.Point(41, 89);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(68, 19);
            this.lblPass.TabIndex = 6;
            this.lblPass.Text = "Mật khẩu";
            // 
            // txtHoTen
            // 
            this.txtHoTen.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtHoTen.Location = new System.Drawing.Point(178, 51);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(268, 24);
            this.txtHoTen.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(41, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Họ tên";
            // 
            // txtUsername
            // 
            this.txtUsername.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsername.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtUsername.Location = new System.Drawing.Point(178, 21);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(268, 24);
            this.txtUsername.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(41, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tên đăng nhập";
            // 
            // pageGroup
            // 
            this.pageGroup.Controls.Add(this.btnBot);
            this.pageGroup.Controls.Add(this.btnThem);
            this.pageGroup.Controls.Add(this.gcThanhVien);
            this.pageGroup.Name = "pageGroup";
            this.pageGroup.Size = new System.Drawing.Size(582, 201);
            this.pageGroup.Text = "Nhóm";
            // 
            // btnBot
            // 
            this.btnBot.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnBot.Appearance.Options.UseFont = true;
            this.btnBot.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBot.ImageOptions.Image")));
            this.btnBot.Location = new System.Drawing.Point(493, 160);
            this.btnBot.Name = "btnBot";
            this.btnBot.Size = new System.Drawing.Size(73, 30);
            this.btnBot.TabIndex = 9;
            this.btnBot.Text = "Bớt";
            this.btnBot.Click += new System.EventHandler(this.btnBot_Click);
            // 
            // btnThem
            // 
            this.btnThem.Appearance.Font = new System.Drawing.Font("Tahoma", 9F);
            this.btnThem.Appearance.Options.UseFont = true;
            this.btnThem.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnThem.ImageOptions.Image")));
            this.btnThem.Location = new System.Drawing.Point(414, 160);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(73, 30);
            this.btnThem.TabIndex = 8;
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // gcThanhVien
            // 
            this.gcThanhVien.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcThanhVien.Location = new System.Drawing.Point(0, 0);
            this.gcThanhVien.MainView = this.gvThanhVien;
            this.gcThanhVien.Name = "gcThanhVien";
            this.gcThanhVien.Size = new System.Drawing.Size(582, 154);
            this.gcThanhVien.TabIndex = 1;
            this.gcThanhVien.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvThanhVien});
            // 
            // gvThanhVien
            // 
            this.gvThanhVien.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.IDUSER,
            this.USERNAME,
            this.FULLNAME});
            this.gvThanhVien.GridControl = this.gcThanhVien;
            this.gvThanhVien.Name = "gvThanhVien";
            this.gvThanhVien.OptionsView.ShowGroupPanel = false;
            // 
            // IDUSER
            // 
            this.IDUSER.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.IDUSER.AppearanceHeader.Options.UseFont = true;
            this.IDUSER.Caption = "ID";
            this.IDUSER.FieldName = "IDUSER";
            this.IDUSER.MaxWidth = 50;
            this.IDUSER.Name = "IDUSER";
            this.IDUSER.Visible = true;
            this.IDUSER.VisibleIndex = 0;
            this.IDUSER.Width = 30;
            // 
            // USERNAME
            // 
            this.USERNAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.USERNAME.AppearanceHeader.Options.UseFont = true;
            this.USERNAME.Caption = "TÊN NHÓM";
            this.USERNAME.FieldName = "USERNAME";
            this.USERNAME.MaxWidth = 100;
            this.USERNAME.MinWidth = 80;
            this.USERNAME.Name = "USERNAME";
            this.USERNAME.Visible = true;
            this.USERNAME.VisibleIndex = 1;
            this.USERNAME.Width = 80;
            // 
            // FULLNAME
            // 
            this.FULLNAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.FULLNAME.AppearanceHeader.Options.UseFont = true;
            this.FULLNAME.Caption = "MÔ TẢ";
            this.FULLNAME.FieldName = "FULLNAME";
            this.FULLNAME.MaxWidth = 120;
            this.FULLNAME.MinWidth = 100;
            this.FULLNAME.Name = "FULLNAME";
            this.FULLNAME.Visible = true;
            this.FULLNAME.VisibleIndex = 2;
            this.FULLNAME.Width = 100;
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.ImageOptions.Image")));
            this.btnDong.Location = new System.Drawing.Point(481, 236);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(86, 39);
            this.btnDong.TabIndex = 7;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnLuu.Appearance.Options.UseFont = true;
            this.btnLuu.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.ImageOptions.Image")));
            this.btnLuu.Location = new System.Drawing.Point(386, 236);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(86, 39);
            this.btnLuu.TabIndex = 6;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // frmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 284);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.tabUser);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Người dùng";
            this.Load += new System.EventHandler(this.frmUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabUser)).EndInit();
            this.tabUser.ResumeLayout(false);
            this.pageUser.ResumeLayout(false);
            this.pageUser.PerformLayout();
            this.pageGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcThanhVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvThanhVien)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabUser;
        private DevExpress.XtraTab.XtraTabPage pageUser;
        private DevExpress.XtraTab.XtraTabPage pageGroup;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraGrid.GridControl gcThanhVien;
        private DevExpress.XtraGrid.Views.Grid.GridView gvThanhVien;
        private DevExpress.XtraGrid.Columns.GridColumn IDUSER;
        private DevExpress.XtraGrid.Columns.GridColumn USERNAME;
        private DevExpress.XtraGrid.Columns.GridColumn FULLNAME;
        private DevExpress.XtraEditors.SimpleButton btnBot;
        private DevExpress.XtraEditors.SimpleButton btnThem;
        private System.Windows.Forms.CheckBox chkDisabled;
        private System.Windows.Forms.TextBox txtRePass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
    }
}