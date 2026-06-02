namespace QuanLyShopThoiTrang.UserControls
{
    partial class ucInventory
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnAllStock = new System.Windows.Forms.Button();
            this.btnLowStock = new System.Windows.Forms.Button();
            this.btnAddStock = new System.Windows.Forms.Button();
            this.dgvInventory = new System.Windows.Forms.DataGridView();
            this.pnlAddStock = new System.Windows.Forms.Panel();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.BT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnXoaDong = new System.Windows.Forms.Button();
            this.btnThemDong = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.txtSoLuong = new System.Windows.Forms.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMaBT = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.txtMaNV = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaPN = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblLowStock = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalSKU = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).BeginInit();
            this.pnlAddStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label2.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.label2.Location = new System.Drawing.Point(15, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 58);
            this.label2.TabIndex = 7;
            this.label2.Text = "INVENTORY OVERVIEW";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAllStock
            // 
            this.btnAllStock.BackColor = System.Drawing.Color.RosyBrown;
            this.btnAllStock.FlatAppearance.BorderSize = 0;
            this.btnAllStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllStock.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAllStock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAllStock.Location = new System.Drawing.Point(124, 61);
            this.btnAllStock.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAllStock.Name = "btnAllStock";
            this.btnAllStock.Size = new System.Drawing.Size(88, 46);
            this.btnAllStock.TabIndex = 8;
            this.btnAllStock.Text = "All Stock";
            this.btnAllStock.UseVisualStyleBackColor = false;
            this.btnAllStock.Click += new System.EventHandler(this.btnAllStock_Click);
            // 
            // btnLowStock
            // 
            this.btnLowStock.BackColor = System.Drawing.Color.RosyBrown;
            this.btnLowStock.FlatAppearance.BorderSize = 0;
            this.btnLowStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLowStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLowStock.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLowStock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLowStock.Location = new System.Drawing.Point(266, 61);
            this.btnLowStock.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLowStock.Name = "btnLowStock";
            this.btnLowStock.Size = new System.Drawing.Size(105, 46);
            this.btnLowStock.TabIndex = 9;
            this.btnLowStock.Text = "Low Stock";
            this.btnLowStock.UseVisualStyleBackColor = false;
            this.btnLowStock.Click += new System.EventHandler(this.btnLowStock_Click);
            // 
            // btnAddStock
            // 
            this.btnAddStock.BackColor = System.Drawing.Color.RosyBrown;
            this.btnAddStock.FlatAppearance.BorderSize = 0;
            this.btnAddStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddStock.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAddStock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddStock.Location = new System.Drawing.Point(632, 61);
            this.btnAddStock.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAddStock.Name = "btnAddStock";
            this.btnAddStock.Size = new System.Drawing.Size(147, 46);
            this.btnAddStock.TabIndex = 10;
            this.btnAddStock.Text = "Add New Stock";
            this.btnAddStock.UseVisualStyleBackColor = false;
            this.btnAddStock.Click += new System.EventHandler(this.btnAddStock_Click);
            // 
            // dgvInventory
            // 
            this.dgvInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInventory.Location = new System.Drawing.Point(21, 205);
            this.dgvInventory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvInventory.Name = "dgvInventory";
            this.dgvInventory.RowHeadersWidth = 51;
            this.dgvInventory.RowTemplate.Height = 24;
            this.dgvInventory.Size = new System.Drawing.Size(871, 368);
            this.dgvInventory.TabIndex = 11;
            // 
            // pnlAddStock
            // 
            this.pnlAddStock.Controls.Add(this.lblTongTien);
            this.pnlAddStock.Controls.Add(this.btnHuy);
            this.pnlAddStock.Controls.Add(this.btnLuu);
            this.pnlAddStock.Controls.Add(this.dgvChiTiet);
            this.pnlAddStock.Controls.Add(this.groupBox2);
            this.pnlAddStock.Controls.Add(this.groupBox1);
            this.pnlAddStock.Controls.Add(this.label5);
            this.pnlAddStock.Location = new System.Drawing.Point(21, 51);
            this.pnlAddStock.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlAddStock.Name = "pnlAddStock";
            this.pnlAddStock.Size = new System.Drawing.Size(875, 541);
            this.pnlAddStock.TabIndex = 12;
            this.pnlAddStock.Visible = false;
            this.pnlAddStock.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAddStock_Paint);
            // 
            // lblTongTien
            // 
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTien.Location = new System.Drawing.Point(585, 478);
            this.lblTongTien.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(67, 16);
            this.lblTongTien.TabIndex = 20;
            this.lblTongTien.Text = "Total: 0 ₫";
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.RosyBrown;
            this.btnHuy.Image = global::QuanLyShopThoiTrang.Properties.Resources.cancel_24dp_1F1F1F_FILL0_wght400_GRAD0_opsz24;
            this.btnHuy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHuy.Location = new System.Drawing.Point(133, 469);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 32);
            this.btnHuy.TabIndex = 19;
            this.btnHuy.Text = " Cancel";
            this.btnHuy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.RosyBrown;
            this.btnLuu.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnLuu.Image = global::QuanLyShopThoiTrang.Properties.Resources.save_24dp_1F1F1F_FILL0_wght400_GRAD0_opsz24;
            this.btnLuu.Location = new System.Drawing.Point(36, 469);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(72, 32);
            this.btnLuu.TabIndex = 18;
            this.btnLuu.Text = "Save";
            this.btnLuu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLuu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BT,
            this.SL,
            this.DG,
            this.TT});
            this.dgvChiTiet.GridColor = System.Drawing.Color.RosyBrown;
            this.dgvChiTiet.Location = new System.Drawing.Point(26, 219);
            this.dgvChiTiet.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.RowTemplate.Height = 24;
            this.dgvChiTiet.Size = new System.Drawing.Size(746, 246);
            this.dgvChiTiet.TabIndex = 17;
            // 
            // BT
            // 
            this.BT.HeaderText = "Variant";
            this.BT.MinimumWidth = 6;
            this.BT.Name = "BT";
            this.BT.Width = 125;
            // 
            // SL
            // 
            this.SL.HeaderText = "Quantity";
            this.SL.MinimumWidth = 6;
            this.SL.Name = "SL";
            this.SL.Width = 125;
            // 
            // DG
            // 
            this.DG.HeaderText = "Unit Price";
            this.DG.MinimumWidth = 6;
            this.DG.Name = "DG";
            this.DG.Width = 125;
            // 
            // TT
            // 
            this.TT.HeaderText = "Total";
            this.TT.MinimumWidth = 6;
            this.TT.Name = "TT";
            this.TT.Width = 125;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.RosyBrown;
            this.groupBox2.Controls.Add(this.btnXoaDong);
            this.groupBox2.Controls.Add(this.btnThemDong);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtDonGia);
            this.groupBox2.Controls.Add(this.txtSoLuong);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtMaBT);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Location = new System.Drawing.Point(20, 134);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(752, 70);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Add Variant";
            // 
            // btnXoaDong
            // 
            this.btnXoaDong.BackColor = System.Drawing.Color.Firebrick;
            this.btnXoaDong.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnXoaDong.Location = new System.Drawing.Point(659, 21);
            this.btnXoaDong.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Size = new System.Drawing.Size(78, 28);
            this.btnXoaDong.TabIndex = 16;
            this.btnXoaDong.Text = "- Remove";
            this.btnXoaDong.UseVisualStyleBackColor = false;
            this.btnXoaDong.Click += new System.EventHandler(this.btnXoaDong_Click);
            // 
            // btnThemDong
            // 
            this.btnThemDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnThemDong.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnThemDong.Location = new System.Drawing.Point(558, 22);
            this.btnThemDong.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnThemDong.Name = "btnThemDong";
            this.btnThemDong.Size = new System.Drawing.Size(74, 26);
            this.btnThemDong.TabIndex = 15;
            this.btnThemDong.Text = "+ Add";
            this.btnThemDong.UseVisualStyleBackColor = false;
            this.btnThemDong.Click += new System.EventHandler(this.btnThemDong_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label9.Location = new System.Drawing.Point(314, 25);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 16);
            this.label9.TabIndex = 14;
            this.label9.Text = "Price:";
            // 
            // txtDonGia
            // 
            this.txtDonGia.Location = new System.Drawing.Point(357, 24);
            this.txtDonGia.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(132, 21);
            this.txtDonGia.TabIndex = 11;
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(224, 23);
            this.txtSoLuong.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(76, 21);
            this.txtSoLuong.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label10.Location = new System.Drawing.Point(180, 25);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "Qty:";
            // 
            // txtMaBT
            // 
            this.txtMaBT.Location = new System.Drawing.Point(72, 22);
            this.txtMaBT.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMaBT.Name = "txtMaBT";
            this.txtMaBT.Size = new System.Drawing.Size(86, 21);
            this.txtMaBT.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label11.Location = new System.Drawing.Point(13, 24);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 16);
            this.label11.TabIndex = 11;
            this.label11.Text = "Variant ID:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.RosyBrown;
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtGhiChu);
            this.groupBox1.Controls.Add(this.dtpNgay);
            this.groupBox1.Controls.Add(this.txtMaNV);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMaPN);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(20, 40);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(752, 70);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import Info";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(502, 25);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Notes:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(338, 27);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Date:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(568, 27);
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(169, 21);
            this.txtGhiChu.TabIndex = 11;
            // 
            // dtpNgay
            // 
            this.dtpNgay.Location = new System.Drawing.Point(393, 25);
            this.dtpNgay.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(96, 21);
            this.dtpNgay.TabIndex = 13;
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(240, 24);
            this.txtMaNV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(76, 21);
            this.txtMaNV.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(171, 27);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Staff ID:";
            // 
            // txtMaPN
            // 
            this.txtMaPN.Location = new System.Drawing.Point(75, 24);
            this.txtMaPN.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMaPN.Name = "txtMaPN";
            this.txtMaPN.Size = new System.Drawing.Size(86, 21);
            this.txtMaPN.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(3, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Receipt ID:";
            // 
            // label5
            // 
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label5.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.label5.Location = new System.Drawing.Point(17, 7);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 31);
            this.label5.TabIndex = 9;
            this.label5.Text = "IMPORT STOCK";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblLowStock);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Location = new System.Drawing.Point(302, 135);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(140, 56);
            this.panel3.TabIndex = 14;
            // 
            // lblLowStock
            // 
            this.lblLowStock.AutoSize = true;
            this.lblLowStock.ForeColor = System.Drawing.Color.Red;
            this.lblLowStock.Location = new System.Drawing.Point(48, 28);
            this.lblLowStock.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLowStock.Name = "lblLowStock";
            this.lblLowStock.Size = new System.Drawing.Size(41, 13);
            this.lblLowStock.TabIndex = 4;
            this.lblLowStock.Text = "label14";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(2, 7);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 15);
            this.label8.TabIndex = 1;
            this.label8.Text = "LOW STOCK ALERTS";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblTotalSKU);
            this.panel1.Location = new System.Drawing.Point(87, 135);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(140, 56);
            this.panel1.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "TOTAL SKU UNITS";
            // 
            // lblTotalSKU
            // 
            this.lblTotalSKU.AutoSize = true;
            this.lblTotalSKU.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblTotalSKU.Location = new System.Drawing.Point(42, 28);
            this.lblTotalSKU.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotalSKU.Name = "lblTotalSKU";
            this.lblTotalSKU.Size = new System.Drawing.Size(41, 13);
            this.lblTotalSKU.TabIndex = 3;
            this.lblTotalSKU.Text = "label13";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.lblValue);
            this.panel2.Location = new System.Drawing.Point(530, 135);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(140, 56);
            this.panel2.TabIndex = 16;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(14, 7);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(109, 15);
            this.label13.TabIndex = 0;
            this.label13.Text = "INVENTORY VALUE";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(47, 28);
            this.lblValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(41, 13);
            this.lblValue.TabIndex = 3;
            this.lblValue.Text = "label13";
            // 
            // ucInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.btnAddStock);
            this.Controls.Add(this.btnLowStock);
            this.Controls.Add(this.btnAllStock);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlAddStock);
            this.Controls.Add(this.dgvInventory);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ucInventory";
            this.Size = new System.Drawing.Size(894, 592);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInventory)).EndInit();
            this.pnlAddStock.ResumeLayout(false);
            this.pnlAddStock.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAllStock;
        private System.Windows.Forms.Button btnLowStock;
        private System.Windows.Forms.Button btnAddStock;
        private System.Windows.Forms.DataGridView dgvInventory;
        private System.Windows.Forms.Panel pnlAddStock;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox txtMaNV;
        private System.Windows.Forms.TextBox txtMaPN;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.MaskedTextBox txtSoLuong;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMaBT;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.DataGridViewTextBoxColumn BT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SL;
        private System.Windows.Forms.DataGridViewTextBoxColumn DG;
        private System.Windows.Forms.DataGridViewTextBoxColumn TT;
        private System.Windows.Forms.Button btnXoaDong;
        private System.Windows.Forms.Button btnThemDong;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblLowStock;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalSKU;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblValue;
    }
}
