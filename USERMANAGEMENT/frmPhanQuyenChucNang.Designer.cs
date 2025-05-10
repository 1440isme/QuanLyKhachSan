namespace USERMANAGEMENT
{
    partial class frmPhanQuyenChucNang
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
            this.components = new System.ComponentModel.Container();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gcUsers = new DevExpress.XtraGrid.GridControl();
            this.gvUsers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.IDUSER = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ISGROUP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.FULLNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.USERNAME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcChucNang = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnCamQuyen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnChiXem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnToanfQuyen = new System.Windows.Forms.ToolStripMenuItem();
            this.gvChucNang = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.FUNC_CODE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DESCIPTION = new DevExpress.XtraGrid.Columns.GridColumn();
            this.QUYEN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cISGROUP = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChucNang)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvChucNang)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.gcUsers);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.gcChucNang);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1072, 723);
            this.splitContainerControl1.SplitterPosition = 361;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // gcUsers
            // 
            this.gcUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcUsers.Location = new System.Drawing.Point(0, 0);
            this.gcUsers.MainView = this.gvUsers;
            this.gcUsers.Name = "gcUsers";
            this.gcUsers.Size = new System.Drawing.Size(361, 723);
            this.gcUsers.TabIndex = 2;
            this.gcUsers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUsers});
            // 
            // gvUsers
            // 
            this.gvUsers.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gvUsers.Appearance.Row.Options.UseFont = true;
            this.gvUsers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.IDUSER,
            this.ISGROUP,
            this.FULLNAME,
            this.USERNAME});
            this.gvUsers.GridControl = this.gcUsers;
            this.gvUsers.Name = "gvUsers";
            this.gvUsers.RowHeight = 25;
            this.gvUsers.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvUsers_CustomDrawCell);
            this.gvUsers.Click += new System.EventHandler(this.gvUsers_Click);
            // 
            // IDUSER
            // 
            this.IDUSER.Caption = "ID";
            this.IDUSER.FieldName = "IDUSER";
            this.IDUSER.Name = "IDUSER";
            // 
            // ISGROUP
            // 
            this.ISGROUP.Caption = " ";
            this.ISGROUP.FieldName = "ISGROUP";
            this.ISGROUP.Name = "ISGROUP";
            this.ISGROUP.Visible = true;
            this.ISGROUP.VisibleIndex = 0;
            this.ISGROUP.Width = 47;
            // 
            // FULLNAME
            // 
            this.FULLNAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.FULLNAME.AppearanceHeader.Options.UseFont = true;
            this.FULLNAME.Caption = "HỌ TÊN/MÔ TẢ";
            this.FULLNAME.FieldName = "FULLNAME";
            this.FULLNAME.MaxWidth = 150;
            this.FULLNAME.Name = "FULLNAME";
            this.FULLNAME.Visible = true;
            this.FULLNAME.VisibleIndex = 2;
            // 
            // USERNAME
            // 
            this.USERNAME.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.USERNAME.AppearanceHeader.Options.UseFont = true;
            this.USERNAME.Caption = "TÊN ND/NHÓM";
            this.USERNAME.FieldName = "USERNAME";
            this.USERNAME.MaxWidth = 250;
            this.USERNAME.Name = "USERNAME";
            this.USERNAME.Visible = true;
            this.USERNAME.VisibleIndex = 1;
            // 
            // gcChucNang
            // 
            this.gcChucNang.ContextMenuStrip = this.contextMenuStrip1;
            this.gcChucNang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcChucNang.Location = new System.Drawing.Point(0, 0);
            this.gcChucNang.MainView = this.gvChucNang;
            this.gcChucNang.Name = "gcChucNang";
            this.gcChucNang.Size = new System.Drawing.Size(701, 723);
            this.gcChucNang.TabIndex = 1;
            this.gcChucNang.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvChucNang});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCamQuyen,
            this.toolStripSeparator1,
            this.btnChiXem,
            this.toolStripSeparator2,
            this.btnToanfQuyen});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(152, 88);
            // 
            // btnCamQuyen
            // 
            this.btnCamQuyen.Name = "btnCamQuyen";
            this.btnCamQuyen.Size = new System.Drawing.Size(151, 24);
            this.btnCamQuyen.Text = "Khoá quyền";
            this.btnCamQuyen.Click += new System.EventHandler(this.btnCamQuyen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
            // 
            // btnChiXem
            // 
            this.btnChiXem.Name = "btnChiXem";
            this.btnChiXem.Size = new System.Drawing.Size(151, 24);
            this.btnChiXem.Text = "Chỉ xem";
            this.btnChiXem.Click += new System.EventHandler(this.btnChiXem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(148, 6);
            // 
            // btnToanfQuyen
            // 
            this.btnToanfQuyen.Name = "btnToanfQuyen";
            this.btnToanfQuyen.Size = new System.Drawing.Size(151, 24);
            this.btnToanfQuyen.Text = "Toàn quyền";
            this.btnToanfQuyen.Click += new System.EventHandler(this.btnToanfQuyen_Click);
            // 
            // gvChucNang
            // 
            this.gvChucNang.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gvChucNang.Appearance.Row.Options.UseFont = true;
            this.gvChucNang.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.FUNC_CODE,
            this.DESCIPTION,
            this.QUYEN,
            this.cISGROUP});
            this.gvChucNang.GridControl = this.gcChucNang;
            this.gvChucNang.Name = "gvChucNang";
            this.gvChucNang.OptionsSelection.MultiSelect = true;
            this.gvChucNang.RowHeight = 25;
            // 
            // FUNC_CODE
            // 
            this.FUNC_CODE.Caption = "FUNC_CODE";
            this.FUNC_CODE.FieldName = "FUNC_CODE";
            this.FUNC_CODE.Name = "FUNC_CODE";
            // 
            // DESCIPTION
            // 
            this.DESCIPTION.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.DESCIPTION.AppearanceHeader.Options.UseFont = true;
            this.DESCIPTION.Caption = "CHỨC NĂNG";
            this.DESCIPTION.FieldName = "DESCRIPTION";
            this.DESCIPTION.MaxWidth = 250;
            this.DESCIPTION.Name = "DESCIPTION";
            this.DESCIPTION.Visible = true;
            this.DESCIPTION.VisibleIndex = 0;
            // 
            // QUYEN
            // 
            this.QUYEN.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.QUYEN.AppearanceHeader.Options.UseFont = true;
            this.QUYEN.Caption = "QUYỀN";
            this.QUYEN.FieldName = "QUYEN";
            this.QUYEN.MaxWidth = 150;
            this.QUYEN.Name = "QUYEN";
            this.QUYEN.Visible = true;
            this.QUYEN.VisibleIndex = 1;
            // 
            // cISGROUP
            // 
            this.cISGROUP.Caption = " ";
            this.cISGROUP.FieldName = "ISGROUP";
            this.cISGROUP.Name = "cISGROUP";
            // 
            // frmPhanQuyenChucNang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 723);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "frmPhanQuyenChucNang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phân quyền chức năng";
            this.Load += new System.EventHandler(this.frmPhanQuyenChucNang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcChucNang)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvChucNang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraGrid.GridControl gcChucNang;
        private DevExpress.XtraGrid.Views.Grid.GridView gvChucNang;
        private DevExpress.XtraGrid.Columns.GridColumn FUNC_CODE;
        private DevExpress.XtraGrid.Columns.GridColumn DESCIPTION;
        private DevExpress.XtraGrid.Columns.GridColumn QUYEN;
        private DevExpress.XtraGrid.GridControl gcUsers;
        private DevExpress.XtraGrid.Columns.GridColumn IDUSER;
        private DevExpress.XtraGrid.Columns.GridColumn USERNAME;
        private DevExpress.XtraGrid.Columns.GridColumn FULLNAME;
        private DevExpress.XtraGrid.Columns.GridColumn ISGROUP;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnCamQuyen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem btnChiXem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem btnToanfQuyen;
        private DevExpress.XtraGrid.Columns.GridColumn cISGROUP;
        public DevExpress.XtraGrid.Views.Grid.GridView gvUsers;
    }
}