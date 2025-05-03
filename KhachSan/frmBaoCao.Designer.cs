namespace KhachSan
{
    partial class frmBaoCao
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
            this.splBaoCao = new DevExpress.XtraEditors.SplitContainerControl();
            this.lstDanhSach = new DevExpress.XtraEditors.ImageListBoxControl();
            this.btnThoat = new DevExpress.XtraEditors.SimpleButton();
            this.btnThucHien = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splBaoCao.Panel1)).BeginInit();
            this.splBaoCao.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splBaoCao.Panel2)).BeginInit();
            this.splBaoCao.Panel2.SuspendLayout();
            this.splBaoCao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstDanhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // splBaoCao
            // 
            this.splBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splBaoCao.Location = new System.Drawing.Point(0, 0);
            this.splBaoCao.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splBaoCao.Name = "splBaoCao";
            // 
            // splBaoCao.Panel1
            // 
            this.splBaoCao.Panel1.Controls.Add(this.lstDanhSach);
            this.splBaoCao.Panel1.Text = "Panel1";
            // 
            // splBaoCao.Panel2
            // 
            this.splBaoCao.Panel2.Controls.Add(this.btnThoat);
            this.splBaoCao.Panel2.Controls.Add(this.btnThucHien);
            this.splBaoCao.Panel2.Text = "Panel2";
            this.splBaoCao.Size = new System.Drawing.Size(1390, 808);
            this.splBaoCao.SplitterPosition = 574;
            this.splBaoCao.TabIndex = 0;
            // 
            // lstDanhSach
            // 
            this.lstDanhSach.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lstDanhSach.Appearance.Options.UseFont = true;
            this.lstDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDanhSach.Location = new System.Drawing.Point(0, 0);
            this.lstDanhSach.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstDanhSach.Name = "lstDanhSach";
            this.lstDanhSach.Size = new System.Drawing.Size(574, 808);
            this.lstDanhSach.TabIndex = 0;
            // 
            // btnThoat
            // 
            this.btnThoat.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnThoat.Appearance.Options.UseFont = true;
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.ImageOptions.Image = global::KhachSan.Properties.Resources.cancel_32x32;
            this.btnThoat.Location = new System.Drawing.Point(378, 687);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(178, 63);
            this.btnThoat.TabIndex = 8;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnThucHien
            // 
            this.btnThucHien.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btnThucHien.Appearance.Options.UseFont = true;
            this.btnThucHien.ImageOptions.Image = global::KhachSan.Properties.Resources.apply_32x32;
            this.btnThucHien.Location = new System.Drawing.Point(176, 687);
            this.btnThucHien.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnThucHien.Name = "btnThucHien";
            this.btnThucHien.Size = new System.Drawing.Size(178, 63);
            this.btnThucHien.TabIndex = 7;
            this.btnThucHien.Text = "Thực hiện";
            this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
            // 
            // frmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1390, 808);
            this.Controls.Add(this.splBaoCao);
            this.IconOptions.Image = global::KhachSan.Properties.Resources.Hotel;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBaoCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo";
            this.Load += new System.EventHandler(this.frmBaoCao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splBaoCao.Panel1)).EndInit();
            this.splBaoCao.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splBaoCao.Panel2)).EndInit();
            this.splBaoCao.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splBaoCao)).EndInit();
            this.splBaoCao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstDanhSach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splBaoCao;
        private DevExpress.XtraEditors.ImageListBoxControl lstDanhSach;
        private DevExpress.XtraEditors.SimpleButton btnThoat;
        private DevExpress.XtraEditors.SimpleButton btnThucHien;
    }
}