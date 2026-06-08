namespace QuanLyShopThoiTrang.UserControls
{
    partial class ucReports
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAOV = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblReturn = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label4 = new System.Windows.Forms.Label();
            this.flpTopSanPham = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnXuatBaoCao = new System.Windows.Forms.Button();
            this.chartTron = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTron)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.AutoSize = true;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongDoanhThu.Location = new System.Drawing.Point(49, 65);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(122, 19);
            this.lblTongDoanhThu.TabIndex = 2;
            this.lblTongDoanhThu.Text = "Tổng doanh thu";
            this.lblTongDoanhThu.Click += new System.EventHandler(this.lblTongDoanhThu_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblAOV);
            this.panel1.Location = new System.Drawing.Point(739, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(206, 74);
            this.panel1.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(3, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "AVERAGE ORDER ";
            // 
            // lblAOV
            // 
            this.lblAOV.AutoSize = true;
            this.lblAOV.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAOV.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblAOV.Location = new System.Drawing.Point(64, 35);
            this.lblAOV.Name = "lblAOV";
            this.lblAOV.Size = new System.Drawing.Size(59, 20);
            this.lblAOV.TabIndex = 0;
            this.lblAOV.Text = "label2";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Controls.Add(this.lblReturn);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(974, 10);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(186, 74);
            this.panel2.TabIndex = 4;
            // 
            // lblReturn
            // 
            this.lblReturn.AutoSize = true;
            this.lblReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblReturn.Location = new System.Drawing.Point(59, 38);
            this.lblReturn.Name = "lblReturn";
            this.lblReturn.Size = new System.Drawing.Size(59, 20);
            this.lblReturn.TabIndex = 1;
            this.lblReturn.Text = "label4";
            this.lblReturn.Click += new System.EventHandler(this.lblReturn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(14, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "RETURN RATE";
            // 
            // chartDoanhThu
            // 
            chartArea1.AxisX.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)(((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.StaggeredLabels)));
            chartArea1.AxisX.LabelStyle.Format = "dd/MM";
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.LabelStyle.Format = "#,##0";
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea1);
            legend1.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend1);
            this.chartDoanhThu.Location = new System.Drawing.Point(19, 86);
            this.chartDoanhThu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartDoanhThu.Name = "chartDoanhThu";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            this.chartDoanhThu.Series.Add(series1);
            this.chartDoanhThu.Size = new System.Drawing.Size(627, 289);
            this.chartDoanhThu.TabIndex = 5;
            this.chartDoanhThu.Text = "chart1";
            this.chartDoanhThu.Click += new System.EventHandler(this.chartDoanhThu_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 377);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 19);
            this.label4.TabIndex = 2;
            this.label4.Text = "Sản phẩm bán chạy";
            // 
            // flpTopSanPham
            // 
            this.flpTopSanPham.AutoScroll = true;
            this.flpTopSanPham.Location = new System.Drawing.Point(29, 398);
            this.flpTopSanPham.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flpTopSanPham.Name = "flpTopSanPham";
            this.flpTopSanPham.Size = new System.Drawing.Size(617, 313);
            this.flpTopSanPham.TabIndex = 6;
            this.flpTopSanPham.WrapContents = false;
            this.flpTopSanPham.Paint += new System.Windows.Forms.PaintEventHandler(this.flpTopSanPham_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(686, 377);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(222, 19);
            this.label5.TabIndex = 7;
            this.label5.Text = "Báo cáo tồn kho và Định mức";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label6.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.label6.Location = new System.Drawing.Point(24, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(323, 62);
            this.label6.TabIndex = 9;
            this.label6.Text = "THỐNG KÊ DOANH THU";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnXuatBaoCao
            // 
            this.btnXuatBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatBaoCao.BackColor = System.Drawing.Color.RosyBrown;
            this.btnXuatBaoCao.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatBaoCao.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnXuatBaoCao.Location = new System.Drawing.Point(1201, 716);
            this.btnXuatBaoCao.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXuatBaoCao.Name = "btnXuatBaoCao";
            this.btnXuatBaoCao.Size = new System.Drawing.Size(124, 33);
            this.btnXuatBaoCao.TabIndex = 10;
            this.btnXuatBaoCao.Text = "Báo Cáo";
            this.btnXuatBaoCao.UseVisualStyleBackColor = false;
            this.btnXuatBaoCao.Click += new System.EventHandler(this.btnXuatBaoCao_Click);
            // 
            // chartTron
            // 
            chartArea2.Name = "ChartArea1";
            this.chartTron.ChartAreas.Add(chartArea2);
            legend2.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend1";
            this.chartTron.Legends.Add(legend2);
            this.chartTron.Location = new System.Drawing.Point(669, 107);
            this.chartTron.Name = "chartTron";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series2.CustomProperties = "DoughnutRadius=50";
            series2.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series2.IsValueShownAsLabel = true;
            series2.Label = "#PERCENT{P0}";
            series2.LabelForeColor = System.Drawing.Color.White;
            series2.Legend = "Legend1";
            series2.LegendText = "#VALX";
            series2.Name = "Series1";
            this.chartTron.Series.Add(series2);
            this.chartTron.Size = new System.Drawing.Size(593, 250);
            this.chartTron.TabIndex = 11;
            this.chartTron.Text = "chart1";
            this.chartTron.Click += new System.EventHandler(this.chartTron_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLyShopThoiTrang.ReportTongHop.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(669, 399);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(632, 312);
            this.reportViewer1.TabIndex = 12;
            this.reportViewer1.Load += new System.EventHandler(this.reportViewer1_Load);
            // 
            // ucReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.btnXuatBaoCao);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.chartTron);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.flpTopSanPham);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chartDoanhThu);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTongDoanhThu);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ucReports";
            this.Size = new System.Drawing.Size(1325, 783);
            this.Load += new System.EventHandler(this.ucReports_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTron)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTongDoanhThu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAOV;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblReturn;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel flpTopSanPham;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnXuatBaoCao;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTron;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}
