namespace QuanLyShopThoiTrang
{
    partial class ProductCartControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTenSP = new System.Windows.Forms.Label();
            this.lblBienThe = new System.Windows.Forms.Label();
            this.lblGiaSP = new System.Windows.Forms.Label();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.btnTru = new System.Windows.Forms.Button();
            this.btnCong = new System.Windows.Forms.Button();
            this.picSP = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSP)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenSP.Location = new System.Drawing.Point(159, 14);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(34, 22);
            this.lblTenSP.TabIndex = 1;
            this.lblTenSP.Text = "Tên";
            // 
            // lblBienThe
            // 
            this.lblBienThe.AutoSize = true;
            this.lblBienThe.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienThe.Location = new System.Drawing.Point(159, 45);
            this.lblBienThe.Name = "lblBienThe";
            this.lblBienThe.Size = new System.Drawing.Size(65, 22);
            this.lblBienThe.TabIndex = 2;
            this.lblBienThe.Text = "Biến thể";
            // 
            // lblGiaSP
            // 
            this.lblGiaSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaSP.Location = new System.Drawing.Point(317, 117);
            this.lblGiaSP.Name = "lblGiaSP";
            this.lblGiaSP.Size = new System.Drawing.Size(101, 22);
            this.lblGiaSP.TabIndex = 3;
            this.lblGiaSP.Text = "...";
            this.lblGiaSP.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblSoLuong.Location = new System.Drawing.Point(201, 117);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(61, 21);
            this.lblSoLuong.TabIndex = 6;
            this.lblSoLuong.Text = "...";
            this.lblSoLuong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTru
            // 
            this.btnTru.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnTru.FlatAppearance.BorderSize = 0;
            this.btnTru.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTru.Image = global::QuanLyShopThoiTrang.Properties.Resources.remove_20dp_FFFFFF_FILL0_wght400_GRAD0_opsz20;
            this.btnTru.Location = new System.Drawing.Point(163, 115);
            this.btnTru.Name = "btnTru";
            this.btnTru.Size = new System.Drawing.Size(32, 22);
            this.btnTru.TabIndex = 5;
            this.btnTru.UseVisualStyleBackColor = false;
            // 
            // btnCong
            // 
            this.btnCong.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnCong.FlatAppearance.BorderSize = 0;
            this.btnCong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCong.Image = global::QuanLyShopThoiTrang.Properties.Resources.add_20dp_FFFFFF_FILL0_wght400_GRAD0_opsz20;
            this.btnCong.Location = new System.Drawing.Point(268, 115);
            this.btnCong.Name = "btnCong";
            this.btnCong.Size = new System.Drawing.Size(32, 22);
            this.btnCong.TabIndex = 4;
            this.btnCong.UseVisualStyleBackColor = false;
            // 
            // picSP
            // 
            this.picSP.Location = new System.Drawing.Point(3, 3);
            this.picSP.Name = "picSP";
            this.picSP.Size = new System.Drawing.Size(150, 144);
            this.picSP.TabIndex = 0;
            this.picSP.TabStop = false;
            // 
            // ProductCartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.btnTru);
            this.Controls.Add(this.btnCong);
            this.Controls.Add(this.lblGiaSP);
            this.Controls.Add(this.lblBienThe);
            this.Controls.Add(this.lblTenSP);
            this.Controls.Add(this.picSP);
            this.Name = "ProductCartControl";
            this.Size = new System.Drawing.Size(432, 150);
            ((System.ComponentModel.ISupportInitialize)(this.picSP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picSP;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.Label lblBienThe;
        private System.Windows.Forms.Label lblGiaSP;
        private System.Windows.Forms.Button btnCong;
        private System.Windows.Forms.Button btnTru;
        private System.Windows.Forms.Label lblSoLuong;
    }
}
