using System;
using System.Data;
using System.Data.SqlClient;

namespace DALShopThoiTrang
{
    public class ThongKeRepository : DBConnection
    {
        public DataTable ThongKeDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                // Nhóm doanh thu theo từng ngày
                string query = @"
                    SELECT CAST(ngayLap AS DATE) AS Ngay, 
                           COUNT(maHD) AS SoLuongDonHang, 
                           SUM(tongTien) AS TongDoanhThu 
                    FROM HoaDon 
                    WHERE ngayLap >= @tuNgay AND ngayLap <= @denNgay 
                    GROUP BY CAST(ngayLap AS DATE) 
                    ORDER BY Ngay ASC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thiết lập giờ để lấy trọn vẹn ngày
                    cmd.Parameters.AddWithValue("@tuNgay", tuNgay.Date);
                    cmd.Parameters.AddWithValue("@denNgay", denNgay.Date.AddDays(1).AddTicks(-1));

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thống kê doanh thu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }
        public DataTable ThongKeSanPhamBanChay(DateTime tuNgay, DateTime denNgay, int top = 10)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                string query = $@"
                    SELECT TOP (@top) 
                           sp.tenSP AS TenSanPham, 
                           bt.mauSac AS MauSac, 
                           bt.kichCo AS KichCo, 
                           SUM(ct.soLuongBan) AS TongSoLuongBan 
                    FROM ChiTietHD ct 
                    INNER JOIN BienTheSP bt ON ct.maBienThe = bt.maBienThe 
                    INNER JOIN SanPham sp ON bt.maSP = sp.maSP 
                    INNER JOIN HoaDon hd ON ct.maHD = hd.maHD 
                    WHERE hd.ngayLap >= @tuNgay AND hd.ngayLap <= @denNgay
                    GROUP BY sp.tenSP, bt.mauSac, bt.kichCo 
                    ORDER BY TongSoLuongBan DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@top", top);
                    cmd.Parameters.AddWithValue("@tuNgay", tuNgay.Date);
                    cmd.Parameters.AddWithValue("@denNgay", denNgay.Date.AddDays(1).AddTicks(-1));

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thống kê sản phẩm bán chạy: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }
        public DataTable LayHangTonKhoDuoiDinhMuc()
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                string query = @"
                    SELECT sp.maSP AS MaSanPham,
                           sp.tenSP AS TenSanPham, 
                           bt.maBienThe AS MaBienThe,
                           bt.mauSac AS MauSac, 
                           bt.kichCo AS KichCo, 
                           bt.soLuongTon AS TonKhoHienTai, 
                           bt.dinhMucToiThieu AS DinhMucToiThieu
                    FROM BienTheSP bt 
                    INNER JOIN SanPham sp ON bt.maSP = sp.maSP 
                    WHERE bt.soLuongTon <= bt.dinhMucToiThieu
                    ORDER BY bt.soLuongTon ASC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy báo cáo tồn kho: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }
    }
}