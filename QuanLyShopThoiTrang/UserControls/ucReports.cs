using BUSShopThoiTrang;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyShopThoiTrang.UserControls
{
    public partial class ucReports : UserControl
    {
        // Đồng bộ dùng đúng 1 tên biến này thôi nha
        private ThongKeBUS _thongKeBUS = new ThongKeBUS();

        public ucReports()
        {
            InitializeComponent();
            LoadDuLieuDashboard();
        }

        private void LoadDuLieuDashboard()
        {
            try
            {
                LoadBieuDoDoanhThu();
                LoadAOV();
                LoadReturnRate();
                LoadTopSanPham();
                LoadBaoCaoTonKho();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═════════════════════════════════════════════════════
        // 1. KHU VỰC DOANH THU & BIỂU ĐỒ
        // ═════════════════════════════════════════════════════
        private void LoadBieuDoDoanhThu()
        {
            DataTable dtDoanhThu = _thongKeBUS.LayDoanhThuTheoNgay();

            chartDoanhThu.Series.Clear();
            Series series = new Series("Doanh Thu");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(171, 114, 129);

            decimal tongDoanhThu = 0;

            foreach (DataRow row in dtDoanhThu.Rows)
            {
                string ngay = row["Ngay"].ToString();
                decimal tien = Convert.ToDecimal(row["DoanhThu"]);
                tongDoanhThu += tien;

                series.Points.AddXY(ngay, tien);
            }

            chartDoanhThu.Series.Add(series);
        }

        // ═════════════════════════════════════════════════════
        // 2. KHU VỰC SẢN PHẨM BÁN CHẠY
        // ═════════════════════════════════════════════════════
        private void LoadTopSanPham()
        {
            DataTable dtTopSP = _thongKeBUS.LayTop5BanChay();
            flpTopSanPham.Controls.Clear();

            int rank = 1;
            foreach (DataRow row in dtTopSP.Rows)
            {
                Panel card = new Panel();
                card.Size = new Size(180, 220);
                card.BackColor = Color.White;
                card.Margin = new Padding(10);
                card.BorderStyle = BorderStyle.FixedSingle;

                Label lblRank = new Label();
                lblRank.Text = $"#{rank} Top Sales";
                lblRank.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                lblRank.ForeColor = Color.White;
                lblRank.BackColor = Color.FromArgb(50, 50, 50);
                lblRank.Dock = DockStyle.Top;

                Label lblTen = new Label();
                lblTen.Text = row["TenSanPham"].ToString();
                lblTen.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lblTen.Dock = DockStyle.Bottom;
                lblTen.Height = 40;

                Label lblBan = new Label();
                lblBan.Text = "Đã bán: " + row["SoLuongBan"].ToString();
                lblBan.Dock = DockStyle.Bottom;
                lblBan.ForeColor = Color.DimGray;

                card.Controls.Add(lblBan);
                card.Controls.Add(lblTen);
                card.Controls.Add(lblRank);

                flpTopSanPham.Controls.Add(card);
                rank++;
            }
        }

        // ═════════════════════════════════════════════════════
        // 3. KHU VỰC CẢNH BÁO TỒN KHO & ĐỊNH MỨC
        // ═════════════════════════════════════════════════════
        private void LoadBaoCaoTonKho()
        {
            DataTable dtTonKho = _thongKeBUS.LayBaoCaoTonKho();

            if (!dtTonKho.Columns.Contains("TrangThai"))
            {
                dtTonKho.Columns.Add("TrangThai", typeof(string));
            }

            dgvTonKho.AutoGenerateColumns = false;
            dgvTonKho.DataSource = dtTonKho;
        }

        private void dgvTonKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.Value != null)
            {
                var cellSoLuong = dgvTonKho.Rows[e.RowIndex].Cells["SoLuongTon"].Value;
                var cellDinhMuc = dgvTonKho.Rows[e.RowIndex].Cells["DinhMucToiThieu"].Value;

                if (cellSoLuong != DBNull.Value && cellDinhMuc != DBNull.Value)
                {
                    int soLuong = Convert.ToInt32(cellSoLuong);
                    int dinhMuc = Convert.ToInt32(cellDinhMuc);

                    if (soLuong <= dinhMuc)
                    {
                        dgvTonKho.Rows[e.RowIndex].Cells["TrangThai"].Value = "CRITICAL";
                        dgvTonKho.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 238);
                        dgvTonKho.Rows[e.RowIndex].Cells["TrangThai"].Style.ForeColor = Color.DarkRed;
                        dgvTonKho.Rows[e.RowIndex].Cells["TrangThai"].Style.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    }
                    else
                    {
                        dgvTonKho.Rows[e.RowIndex].Cells["TrangThai"].Value = "HEALTHY";
                        dgvTonKho.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        dgvTonKho.Rows[e.RowIndex].Cells["TrangThai"].Style.ForeColor = Color.SeaGreen;
                    }
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        // ═════════════════════════════════════════════════════
        // 4. KHU VỰC KPI: AOV VÀ RETURN RATE
        // ═════════════════════════════════════════════════════
        private void LoadAOV()
        {
            try
            {
                DataTable dtDoanhThu = _thongKeBUS.LayDoanhThuTheoNgay();
                decimal tongDoanhThu = 0;

                foreach (DataRow row in dtDoanhThu.Rows)
                {
                    tongDoanhThu += Convert.ToDecimal(row["DoanhThu"]);
                }

                decimal aov = 0;
                if (dtDoanhThu.Rows.Count > 0)
                {
                    aov = tongDoanhThu / dtDoanhThu.Rows.Count;
                }

                lblAOV.Text = aov.ToString("N0") + " ₫";
            }
            catch
            {
                lblAOV.Text = "0 ₫";
            }
        }

        private void LoadReturnRate()
        {
            try
            {
                // Gọi hàm lấy Tỉ lệ hoàn hàng thật từ DB
                DataTable dtReturn = _thongKeBUS.LayTiLeHoanHang();
                if (dtReturn.Rows.Count > 0)
                {
                    double tiLe = Convert.ToDouble(dtReturn.Rows[0]["TiLeHoan"]);
                    lblReturn.Text = tiLe.ToString("0.0") + "%";
                }
            }
            catch
            {
                lblReturn.Text = "0.0%";
            }
        }

        private void lblTongDoanhThu_Click(object sender, EventArgs e)
        {

        }
    }
}