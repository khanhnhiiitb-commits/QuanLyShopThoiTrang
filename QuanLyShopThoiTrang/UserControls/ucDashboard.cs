using BUSShopThoiTrang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyShopThoiTrang.UserControls
{
    public partial class ucDashboard : UserControl
    {
        private ThongKeBUS _thongKeBus = new ThongKeBUS();
        private TaiKhoanBUS _taiKhoanBus = new TaiKhoanBUS();
        public ucDashboard()
        {
            InitializeComponent();
        }
        // SỰ KIỆN KHI MỞ GIAO DIỆN 
        private void ucDashboard_Load(object sender, EventArgs e)
        {
            LoadTongDoanhThu();
            LoadCanhBaoTonKho();
            LoadChartDoanhThu();
            LoadChartTopSanPham();
            LoadDanhSachNhanVien();
        }
        // 1. TÍNH TOÁN & HIỂN THỊ TỔNG DOANH THU LÊN LABEL2
        private void LoadTongDoanhThu()
        {
            try
            {
                // Gọi hàm lấy doanh thu theo ngày của bạn
                var dtDoanhThu = _thongKeBus.LayDoanhThuTheoNgay();
                decimal tongDoanhThu = 0;

                // Chạy vòng lặp cộng dồn cột DoanhThu của tất cả các ngày
                foreach (DataRow row in dtDoanhThu.Rows)
                {
                    if (row["DoanhThu"] != DBNull.Value)
                    {
                        tongDoanhThu += Convert.ToDecimal(row["DoanhThu"]);
                    }
                }

                // Gán vào label2 kèm format có dấu phẩy (VD: 15,000,000 VNĐ)
                label2.Text = tongDoanhThu.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải tổng doanh thu: " + ex.Message);
            }
        }

        // 2. ĐẾM SỐ LƯỢNG MẶT HÀNG TỒN KHO LÊN LABEL6
        private void LoadCanhBaoTonKho()
        {
            try
            {
                // Dùng hàm có sẵn của bạn để lấy mặt hàng dưới định mức
                var dtTonKho = _thongKeBus.LayHangTonKhoDuoiDinhMuc();

                // Đếm số dòng (tương ứng với số lượng mặt hàng sắp hết)
                int soLuongCanhBao = dtTonKho.Rows.Count;

                // Gán vào label6
                label6.Text = soLuongCanhBao.ToString() + " mặt hàng";

                // (Tùy chọn): Đổ luôn danh sách này vào DataGridView lưới bên dưới
                // dgvStaff.DataSource = dtTonKho; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải cảnh báo tồn kho: " + ex.Message);
            }
        }

        // 3. ĐỔ DỮ LIỆU VÀO BIỂU ĐỒ CỘT (DOANH THU)
        private void LoadChartDoanhThu()
        {
            try
            {
                var dtDoanhThu = _thongKeBus.LayDoanhThuTheoNgay();

                // Lưu ý: Đổi "chart1" thành tên thật cái biểu đồ cột của bạn trên Design
                chart1.DataSource = dtDoanhThu;

                // Gắn cột Ngay vào trục X, cột DoanhThu vào trục Y
                chart1.Series["Doanh thu"].XValueMember = "Ngay";
                chart1.Series["Doanh thu"].YValueMembers = "DoanhThu";

                chart1.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải biểu đồ doanh thu: " + ex.Message);
            }
        }

        // 4. ĐỔ DỮ LIỆU VÀO BIỂU ĐỒ TRÒN (TOP SẢN PHẨM BÁN CHẠY)
        private void LoadChartTopSanPham()
        {
            try
            {
                var dtTopSP = _thongKeBus.LayTop5BanChay();

                // Lưu ý: Đổi "chart2" thành tên thật cái biểu đồ tròn của bạn trên Design
                chart2.DataSource = dtTopSP;

                // Gắn Tên Sản Phẩm để phân ranh giới, gắn Số Lượng Bán để vẽ độ to của miếng bánh
                chart2.Series["Series1"].XValueMember = "TenSanPham";
                chart2.Series["Series1"].YValueMembers = "SoLuongBan";

                // Hiện số lên từng miếng bánh
                chart2.Series["Series1"].IsValueShownAsLabel = true;

                chart2.DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải biểu đồ top sản phẩm: " + ex.Message);
            }
        }

        private void LoadDanhSachNhanVien()
        {
            try
            {
                // Gọi hàm từ TaiKhoanBUS của bạn
                var dtStaff = _taiKhoanBus.LayDanhSachNhanVien();
                dgvStaff.DataSource = dtStaff;

                // --- BẢO MẬT: ẨN CÁC CỘT KHÔNG CẦN THIẾT ---
                if (dgvStaff.Columns["matKhau"] != null)
                    dgvStaff.Columns["matKhau"].Visible = false;

                // --- TÚT LẠI LƯỚI CHO ĐẸP ---
                dgvStaff.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvStaff.ReadOnly = true;
                dgvStaff.AllowUserToAddRows = false;
                dgvStaff.RowHeadersVisible = false;
                dgvStaff.BackgroundColor = System.Drawing.Color.White;
                dgvStaff.BorderStyle = BorderStyle.None;
                dgvStaff.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvStaff.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách nhân viên: " + ex.Message);
            }
        }
    }
}

