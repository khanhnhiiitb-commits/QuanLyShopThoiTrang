using BUSShopThoiTrang;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OfficeOpenXml;
using System.IO;

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
                LoadBieuDoTronSanPhamBanChay();
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

            // Dọn dẹp series cũ trên giao diện
            chartDoanhThu.Series.Clear();

            // Tạo series mới
            Series series = new Series("Doanh Thu");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(171, 114, 129);

            // THÊM 2 DÒNG NÀY ĐỂ GIỮ DÁNG CHO BIỂU ĐỒ:
            // 1. Nhắc Chart biết trục X là thời gian (để nó chịu nhận format dd/MM bạn chỉnh ở ngoài)
            series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;

            // 2. Ép các cột đứng sát nhau, mập mạp, không bị kéo dãn ra do các ngày trống
            series.IsXValueIndexed = true;

            decimal tongDoanhThu = 0;

            foreach (DataRow row in dtDoanhThu.Rows)
            {
                // SỬA Ở ĐÂY: Lấy chuỗi ngày và ép chuẩn sang kiểu DateTime (tránh lỗi String was not recognized)
                string chuoiNgay = row["Ngay"].ToString().Substring(0, 10);
                DateTime ngay = DateTime.ParseExact(chuoiNgay, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                decimal tien = Convert.ToDecimal(row["DoanhThu"]);
                tongDoanhThu += tien;

                series.Points.AddXY(ngay, tien);
            }

            // Đẩy series đã hoàn thiện lên biểu đồ
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
                // 1. Tạo khung thẻ (Tăng chiều cao lên 220 để đủ chỗ chứa hình)
                Panel card = new Panel();
                card.Size = new Size(160, 220);
                card.BackColor = Color.White;
                card.Margin = new Padding(5, 5, 5, 10);
                card.BorderStyle = BorderStyle.FixedSingle;

                // 2. Nhãn Thứ hạng
                Label lblRank = new Label();
                lblRank.Text = $"#{rank} Top Sales";
                lblRank.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                lblRank.ForeColor = Color.White;
                lblRank.BackColor = Color.FromArgb(171, 114, 129);
                lblRank.Dock = DockStyle.Top;

                // ==========================================
                // 3. CODE MA THUẬT: TỰ TẠO PICTUREBOX ĐỂ SHOW HÌNH
                // ==========================================
                PictureBox pic = new PictureBox();
                pic.Height = 110; // Chiều cao tấm hình
                pic.Dock = DockStyle.Top;
                pic.SizeMode = PictureBoxSizeMode.Zoom;

                try
                {
                    // Lấy tên hình từ Database (Nhớ xem Bước 2 bên dưới)
                    string tenHinh = row["HinhAnh"].ToString();
                    string duongDan = Path.Combine(Application.StartupPath, "Images", tenHinh);

                    if (File.Exists(duongDan))
                    {
                        pic.Image = Image.FromFile(duongDan);
                    }
                }
                catch
                {
                    // Bỏ qua nếu lỗi (để trống hình)
                }
                // ==========================================

                // 4. Nhãn Tên sản phẩm
                Label lblTen = new Label();
                lblTen.Text = row["TenSanPham"].ToString();
                lblTen.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                lblTen.ForeColor = Color.Black;
                lblTen.Height = 40;
                lblTen.TextAlign = ContentAlignment.MiddleCenter;
                lblTen.Dock = DockStyle.Top;

                // 5. Nhãn Số lượng bán
                Label lblBan = new Label();
                lblBan.Text = "Đã bán: " + row["SoLuongBan"].ToString();
                lblBan.ForeColor = Color.DimGray;
                lblBan.Font = new Font("Segoe UI", 8);
                lblBan.TextAlign = ContentAlignment.MiddleCenter;
                lblBan.Dock = DockStyle.Top;

                // 6. Gắn lần lượt vào thẻ (Chú ý thứ tự ngược từ dưới lên trên)
                card.Controls.Add(lblBan);
                card.Controls.Add(lblTen);
                card.Controls.Add(pic);      // Gắn hình vào vị trí giữa
                card.Controls.Add(lblRank);  // Gắn rank lên trên cùng

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

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            if (dgvTonKho.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Chọn nơi lưu file Báo Cáo Tồn Kho";
            // Đặt tên file mặc định có luôn ngày tháng hiện tại cho xịn
            saveFileDialog.FileName = "BaoCaoTonKho_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Cấp phép xài thư viện miễn phí
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("TonKho");

                        // In Tiêu đề cột
                        for (int i = 0; i < dgvTonKho.Columns.Count; i++)
                        {
                            ws.Cells[1, i + 1].Value = dgvTonKho.Columns[i].HeaderText;
                            ws.Cells[1, i + 1].Style.Font.Bold = true;
                            ws.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray); // Tô màu xám cho dòng tiêu đề
                        }

                        // In Dữ liệu
                        for (int i = 0; i < dgvTonKho.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvTonKho.Columns.Count; j++)
                            {
                                if (dgvTonKho.Rows[i].Cells[j].Value != null)
                                {
                                    ws.Cells[i + 2, j + 1].Value = dgvTonKho.Rows[i].Cells[j].Value.ToString();
                                }
                            }
                        }

                        // Tự động giãn cột cho đẹp
                        ws.Cells[ws.Dimension.Address].AutoFitColumns();

                        // Lưu và xuất file
                        FileInfo fi = new FileInfo(saveFileDialog.FileName);
                        pck.SaveAs(fi);

                        MessageBox.Show("Đã xuất file báo cáo kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Trục trặc rồi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXuatBaoCaoo_Click(object sender, EventArgs e)
        {
            if (dgvTonKho.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Chọn nơi lưu file Báo Cáo Tồn Kho";
            // Đặt tên file mặc định có luôn ngày tháng hiện tại cho xịn
            saveFileDialog.FileName = "BaoCaoTonKho_" + DateTime.Now.ToString("ddMMyyyy") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Cấp phép xài thư viện miễn phí
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("TonKho");

                        // In Tiêu đề cột
                        for (int i = 0; i < dgvTonKho.Columns.Count; i++)
                        {
                            ws.Cells[1, i + 1].Value = dgvTonKho.Columns[i].HeaderText;
                            ws.Cells[1, i + 1].Style.Font.Bold = true;
                            ws.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            ws.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray); // Tô màu xám cho dòng tiêu đề
                        }

                        // In Dữ liệu
                        for (int i = 0; i < dgvTonKho.Rows.Count; i++)
                        {
                            for (int j = 0; j < dgvTonKho.Columns.Count; j++)
                            {
                                if (dgvTonKho.Rows[i].Cells[j].Value != null)
                                {
                                    ws.Cells[i + 2, j + 1].Value = dgvTonKho.Rows[i].Cells[j].Value.ToString();
                                }
                            }
                        }

                        // Tự động giãn cột cho đẹp
                        ws.Cells[ws.Dimension.Address].AutoFitColumns();

                        // Lưu và xuất file
                        FileInfo fi = new FileInfo(saveFileDialog.FileName);
                        pck.SaveAs(fi);

                        MessageBox.Show("Đã xuất file báo cáo kho thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Trục trặc rồi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void flpTopSanPham_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ucReports_Load(object sender, EventArgs e)
        {

        }

        private void chartDoanhThu_Click(object sender, EventArgs e)
        {

        }

        private void lblReturn_Click(object sender, EventArgs e)
        {

        }
        private void LoadBieuDoTronSanPhamBanChay()
        {
            // 1. Gọi dữ liệu từ tầng BUS của bạn về
            DataTable dtTop5 = _thongKeBUS.LayTop5BanChay();

            // 2. Trao nguyên cái bảng dữ liệu đó cho biểu đồ tròn
            chartTron.DataSource = dtTop5;

            // 3. Báo cho Chart biết lấy cột nào làm Tên (X), cột nào làm Giá trị (Y)
            chartTron.Series[0].XValueMember = "TenSanPham";
            chartTron.Series[0].YValueMembers = "SoLuongBan";

            // 4. Bấm nút "Bơm" dữ liệu lên giao diện!
            chartTron.DataBind();
            // Tắt bộ màu mặc định xanh đỏ lòe loẹt của Chart
            chartTron.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;

          
            chartTron.PaletteCustomColors = new Color[] {
        Color.FromArgb(171, 114, 129), // Hồng mận (màu chính xác của menu bên trái)
        Color.FromArgb(204, 153, 162), // Hồng pastel mượt mà
        Color.FromArgb(224, 187, 194), // Hồng nhạt
        Color.FromArgb(138, 86, 100),  // Hồng mận đậm (tạo điểm nhấn)
        Color.FromArgb(235, 212, 216)  // Hồng phấn siêu nhạt
    };
        }

        private void chartTron_Click(object sender, EventArgs e)
        {

        }
    }
    }
