using System;
using System.Data;
using System.Data.SqlClient;

namespace DALShopThoiTrang
{
    public class ThongKeRepository : DBConnection
    {
        private DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi truy vấn SQL: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }

        // ==============================================================
        // CÁC HÀM CŨ CỦA BẠN (Tui giữ lại để không bị lỗi các Form khác)
        // ==============================================================
        public DataTable ThongKeDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            string sql = $"SELECT * FROM HoaDon WHERE ngayLap >= '{tuNgay:yyyy-MM-dd}' AND ngayLap <= '{denNgay:yyyy-MM-dd}'";
            return ExecuteQuery(sql);
        }

        public DataTable ThongKeSanPhamBanChay(DateTime tuNgay, DateTime denNgay, int top = 10)
        {
            string sql = $"SELECT TOP {top} sp.tenSP, SUM(ct.soLuongBan) as SoLuong FROM ChiTietHD ct JOIN BienTheSP bt ON ct.maBienThe = bt.maBienThe JOIN SanPham sp ON bt.maSP = sp.maSP JOIN HoaDon hd ON ct.maHD = hd.maHD WHERE hd.ngayLap >= '{tuNgay:yyyy-MM-dd}' AND hd.ngayLap <= '{denNgay:yyyy-MM-dd}' GROUP BY sp.tenSP ORDER BY SoLuong DESC";
            return ExecuteQuery(sql);
        }

        public DataTable LayHangTonKhoDuoiDinhMuc()
        {
            string sql = "SELECT * FROM BienTheSP WHERE soLuongTon <= dinhMucToiThieu";
            return ExecuteQuery(sql);
        }

        // ==============================================================
        // 3 HÀM MỚI DÀNH RIÊNG CHO MÀN HÌNH DASHBOARD (ucReports)
        // ==============================================================
        public DataTable LayDoanhThuTheoNgay()
        {
            string sql = @"
                SELECT CONVERT(varchar, ngayLap, 103) AS Ngay, SUM(tongTien) AS DoanhThu 
                FROM HoaDon 
                GROUP BY CONVERT(varchar, ngayLap, 103)";
            return ExecuteQuery(sql);
        }

        public DataTable LayTop5BanChay()
        {
            string sql = @"
                SELECT TOP 5 
                    sp.tenSP AS TenSanPham, 
                    SUM(ct.soLuongBan) AS SoLuongBan, 
                    SUM(ct.soLuongBan * ct.donGiaBan) AS DoanhThuSP
                FROM ChiTietHD ct 
                JOIN BienTheSP bt ON ct.maBienThe = bt.maBienThe
                JOIN SanPham sp ON bt.maSP = sp.maSP
                GROUP BY sp.tenSP
                ORDER BY SoLuongBan DESC";
            return ExecuteQuery(sql);
        }

        public DataTable LayBaoCaoTonKho()
        {
            string sql = @"
                SELECT 
                    sp.tenSP AS TenSanPham, 
                    (bt.mauSac + ' / ' + bt.kichCo) AS PhanLoai, 
                    bt.soLuongTon AS SoLuongTon, 
                    bt.dinhMucToiThieu AS DinhMucToiThieu 
                FROM BienTheSP bt
                JOIN SanPham sp ON bt.maSP = sp.maSP";
            return ExecuteQuery(sql);
        }
        public DataTable LayTiLeHoanHang()
        {
            // Lấy tổng số phiếu trả chia cho tổng số hóa đơn
            string sql = @"
        SELECT 
            CASE 
                WHEN (SELECT COUNT(*) FROM HoaDon) = 0 THEN 0
                ELSE (SELECT COUNT(*) FROM PhieuDoiTra) * 100.0 / (SELECT COUNT(*) FROM HoaDon)
            END AS TiLeHoan";
            return ExecuteQuery(sql);
        }
    }
}