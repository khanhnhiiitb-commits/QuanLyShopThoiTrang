using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DTOQuanLyThoiTrang;
using DTOShopThoiTrang;

namespace QuanLyShopThoiTrang.Forms
{
    public partial class FormHoaDon : Form
    {
        private HoaDonDTO hoaDon;
        private List<ChiTietHDDTO> danhSachChiTiet;
        private KhachHangDTO khachHang;

        // Constructor nhận dữ liệu từ BanHangForm truyền sang
        public FormHoaDon(HoaDonDTO hd, List<ChiTietHDDTO> ct, KhachHangDTO kh)
        {
            InitializeComponent();
            this.hoaDon = hd;
            this.danhSachChiTiet = ct;
            this.khachHang = kh;

            // Cấu hình Form hiển thị kiểu Pop-up
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hóa Đơn Bán Lẻ";
        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            TaoMauHoaDon();
        }

        private void TaoMauHoaDon()
        {
            rtbHoaDon.Clear();
            rtbHoaDon.SelectionAlignment = HorizontalAlignment.Center;
            rtbHoaDon.AppendText("SHOP THỜI TRANG MUSE\n"); // Bạn có thể đổi tên shop
            rtbHoaDon.AppendText("Địa chỉ: Nguyễn Đình Chiểu, Quận 3, TP.HCM\n");
            rtbHoaDon.AppendText("Điện thoại: 0123.456.789\n");
            rtbHoaDon.AppendText("---------------------------------------------------------------------\n\n");

            rtbHoaDon.AppendText("                 HÓA ĐƠN THANH TOÁN\n\n");

            rtbHoaDon.SelectionAlignment = HorizontalAlignment.Left;
            rtbHoaDon.AppendText($"Số HĐ: {hoaDon.MaHD}\n");
            rtbHoaDon.AppendText($"Ngày lập: {hoaDon.NgayLap.ToString("dd/MM/yyyy HH:mm")}\n");
            rtbHoaDon.AppendText($"Thu ngân: {hoaDon.MaNV}\n");

            if (khachHang != null)
            {
                rtbHoaDon.AppendText($"Khách hàng: {khachHang.TenKH} - ĐT: {khachHang.SoDienThoai}\n");
            }
            else
            {
                rtbHoaDon.AppendText("Khách hàng: Khách vãng lai\n");
            }

            rtbHoaDon.AppendText($"Phương thức: {hoaDon.PhuongThucThanhToan}\n");
            rtbHoaDon.AppendText("------------------------------------------------------------------------\n");
            rtbHoaDon.AppendText(String.Format("{0,-20} {1,10} {2,15}\n", "Sản phẩm", "SL", "Thành tiền"));
            rtbHoaDon.AppendText("------------------------------------------------------------------------\n");

            decimal tongTienHang = 0;
            foreach (var item in danhSachChiTiet)
            {
                decimal thanhTien = item.SoLuongBan * item.DonGiaBan;
                tongTienHang += thanhTien;

                // Cắt ngắn tên biến thể nếu quá dài để bill không bị vỡ khung
                string tenSP = item.MaBienThe; // Lý tưởng nhất là truyền thêm tên SP vào DTO, tạm dùng Mã BT
                if (tenSP.Length > 20) tenSP = tenSP.Substring(0, 17) + "...";

                rtbHoaDon.AppendText(String.Format("{0,-20} {1,10} {2,15:N0}đ\n", tenSP, item.SoLuongBan, thanhTien));
            }

            rtbHoaDon.AppendText("-----------------------------------------------------------------------\n");

            // Xử lý logic thuế & giảm giá giống hệt bên BanHangForm
            decimal thueVAT = tongTienHang * 0.1m;
            decimal tongCong = tongTienHang + thueVAT; // Nếu có giảm giá 10% KH thì tính thêm ở đây

            rtbHoaDon.AppendText(String.Format("{0,-25} {1,20:N0}đ\n", "Cộng tiền hàng:", tongTienHang));
            rtbHoaDon.AppendText(String.Format("{0,-25} {1,20:N0}đ\n", "Thuế VAT (10%):", thueVAT));

            if (khachHang != null) 
            {
                decimal giamGia = tongCong * 0.1m;
                tongCong -= giamGia;
                rtbHoaDon.AppendText(String.Format("{0,-25} {1,20:N0}đ\n", "Chiết khấu TV (-10%):", giamGia));
            }

            rtbHoaDon.AppendText("------------------------------------------------------------------------\n");

            rtbHoaDon.SelectionFont = new Font(rtbHoaDon.Font, FontStyle.Bold);
            rtbHoaDon.AppendText(String.Format("{0,-25} {1,20:N0}đ\n", "TỔNG CỘNG:", tongCong));

            rtbHoaDon.SelectionAlignment = HorizontalAlignment.Center;
            rtbHoaDon.AppendText("\nCảm ơn quý khách và hẹn gặp lại!\n");
        }

        // Chức năng in thật ra máy in 
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += (s, ev) =>
            {
                ev.Graphics.DrawString(rtbHoaDon.Text, new Font("Courier New", 10), Brushes.Black, new PointF(10, 10));
            };

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                printDocument.Print();
            }
        }

        
    }
}