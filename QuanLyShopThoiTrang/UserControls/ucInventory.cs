using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using BUSShopThoiTrang;
using DTOQuanLyThoiTrang;

namespace QuanLyShopThoiTrang.UserControls
{
    public partial class ucInventory : UserControl
    {
        private KhoHangBUS _khoHangBUS = new KhoHangBUS();

        public ucInventory()
        {
            InitializeComponent();
            LoadInventory(); 
        }

        private void LoadInventory()
        {
            try
            {
                DataTable dtTonKho = _khoHangBUS.LayDanhSachTonKho();

                dgvInventory.DataSource = dtTonKho;

                CalculateKPI(dtTonKho);
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

    

        // Code cho nút Cancel
        private void btnHuy_Click(object sender, EventArgs e)
        {
            pnlAddStock.Visible = false; // Ẩn panel đi
        }

        // Code cho nút + Add
        private void btnThemDong_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaBT.Text))
            {
                MessageBox.Show(
                    "Enter variant ID.",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }
            if (!int.TryParse(txtSoLuong.Text, out int sl) || sl <= 0) { MessageBox.Show("Qty must be > 0."); return; }
            if (!decimal.TryParse(txtDonGia.Text, out decimal dg) || dg <= 0) { MessageBox.Show("Price must be > 0."); return; }

            dgvChiTiet.Rows.Add(txtMaBT.Text.Trim(), sl, dg.ToString("N0"), (sl * dg).ToString("N0"));
            txtMaBT.Clear(); txtSoLuong.Clear(); txtDonGia.Clear(); txtMaBT.Focus();
            UpdateTotal();
        }

        // Code cho nút - Remove
        private void btnXoaDong_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow != null && !dgvChiTiet.CurrentRow.IsNewRow)
            {
                dgvChiTiet.Rows.RemoveAt(dgvChiTiet.CurrentRow.Index);
                UpdateTotal();
            }
        }

        // Code cho nút Save
        // Code cho nút Save
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPN.Text) || string.IsNullOrWhiteSpace(txtMaNV.Text))
            { MessageBox.Show("Enter receipt ID and staff ID."); return; }
            if (dgvChiTiet.Rows.Count == 0)
            { MessageBox.Show("Add at least 1 variant."); return; }

            try
            {
                var chiTiet = new List<ChiTietPNDTO>();
                decimal tongTienPhieu = 0; // Biến giữ tổng tiền của cả phiếu nhập
                string maPhieu = txtMaPN.Text.Trim();

                // GỘP CHUNG VÀO 1 VÒNG LẶP DUY NHẤT CHO TỐI ƯU
                foreach (DataGridViewRow r in dgvChiTiet.Rows)
                {
                    if (r.IsNewRow) continue;

                    // Lấy Đơn giá và Số lượng
                    decimal.TryParse(r.Cells[2].Value?.ToString()?.Replace(",", ""), out decimal dg);
                    int.TryParse(r.Cells[1].Value?.ToString(), out int sl);

                    // 1. Tính Thành tiền cho từng dòng
                    decimal thanhTien = sl * dg;

                    // 2. Cộng dồn vào Tổng tiền của cả phiếu
                    tongTienPhieu += thanhTien;

                    // 3. Đóng gói dòng chi tiết
                    chiTiet.Add(new ChiTietPNDTO
                    {
                        MaPN = maPhieu,
                        MaBienThe = r.Cells[0].Value.ToString(),
                        SoLuongNhap = sl,
                        DonGiaNhap = dg,
                        ThanhTien = thanhTien // ĐÃ GÁN ĐẦY ĐỦ ĐỂ TRÁNH LỖI NULL
                    });
                }

                // 4. Khởi tạo đối tượng PhieuNhap sau khi đã có tổng tiền
                var phieu = new PhieuNhapDTO
                {
                    MaPN = maPhieu,
                    MaNV = txtMaNV.Text.Trim(),
                    NgayNhap = dtpNgay.Value,
                    GhiChu = txtGhiChu.Text.Trim(),
                    TongTienNhap = tongTienPhieu // Gắn tổng tiền vào Phiếu
                };

                // 5. Gọi BUS để lưu xuống DB
                if (_khoHangBUS.NhapHang(phieu, chiTiet))
                {
                    MessageBox.Show("✅ Stock imported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvChiTiet.Rows.Clear();
                    txtMaPN.Clear();
                    txtMaNV.Clear();
                    txtGhiChu.Clear();
                    lblTongTien.Text = "Total: 0 ₫";
                    pnlAddStock.Visible = false; // Xong thì đóng Panel
                    LoadInventory(); // Load lại kho
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // Hàm tính tổng tiền
        private void UpdateTotal()
        {
            decimal tong = 0;
            foreach (DataGridViewRow r in dgvChiTiet.Rows)
            {
                if (r.IsNewRow) continue;

                // Lấy giá trị cột Đơn giá (Cột số 3 -> Index là 2) và Số lượng (Cột số 2 -> Index là 1)
                decimal.TryParse(r.Cells[2].Value?.ToString()?.Replace(",", ""), out decimal dg);
                int.TryParse(r.Cells[1].Value?.ToString(), out int sl);

                tong += sl * dg;
            }
            lblTongTien.Text = "Total: " + tong.ToString("N0") + " ₫";
        }
        // Xử lý khi bấm nút All Stock
        private void btnAllStock_Click(object sender, EventArgs e)
        {
            LoadInventory();
        }

        // Xử lý khi bấm nút Low Stock
        private void btnLowStock_Click(object sender, EventArgs e)
        {
            try
            {
                dgvInventory.DataSource = _khoHangBUS.LayHangDuoiDinhMuc();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // Nút màu hồng to ở ngoài màn hình chính: Chỉ làm nhiệm vụ MỞ BẢNG
        private void btnAddStock_Click(object sender, EventArgs e)
        {
            pnlAddStock.Visible = true;
            pnlAddStock.BringToFront();
            txtMaPN.Text = _khoHangBUS.PhatSinhMaPhieuNhap();
        }

        // Sự kiện Paint này vô dụng, cứ để rỗng hoặc xóa sạch ruột đi
        private void pnlAddStock_Paint(object sender, PaintEventArgs e)
        {
        }
        private void CalculateKPI(DataTable dt)
        {
            // 1. Kiểm tra nếu chưa có dữ liệu thì không làm gì cả
            if (dt == null || dt.Rows.Count == 0) return;

            // 2. Khởi tạo các biến để hứng kết quả
            int totalSKU = dt.Rows.Count; // Đếm tổng số dòng (SKU)
            int lowStockCount = 0;
            decimal totalValue = 0;

            // 3. Dùng vòng lặp quét qua từng dòng dữ liệu đang có trên lưới
            foreach (DataRow row in dt.Rows)
            {
                // Lấy dữ liệu an toàn (tránh lỗi nếu lỡ bị null)
                int.TryParse(row["SoLuongTon"].ToString(), out int soLuong);
                int.TryParse(row["DinhMucToiThieu"].ToString(), out int dinhMuc);
                decimal.TryParse(row["GiaNhap"].ToString(), out decimal giaNhap);

                // Đếm số lượng hàng dưới định mức
                if (soLuong <= dinhMuc)
                {
                    lowStockCount++;
                }

                // Cộng dồn tổng giá trị kho (Số lượng * Giá nhập)
                totalValue += soLuong * giaNhap;
            }

            // 4. Hiển thị kết quả lên các Label (Có format số cho đẹp)
            lblTotalSKU.Text = totalSKU.ToString("N0") + " Đơn vị";
            lblLowStock.Text = lowStockCount.ToString("N0") + " Mặt hàng";
            lblValue.Text = totalValue.ToString("N0") + " ₫";
        }

        private void lblValue_Click(object sender, EventArgs e)
        {

        }

       
    }
}