using DTOQuanLyThoiTrang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALShopThoiTrang
{
    public class GiaoDichRepository : DBConnection
    {
        // Lấy danh sách hóa đơn
        public DataTable LayDanhSachHoaDon()
        {
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();

                string query = @"
                    SELECT hd.maHD,
                           kh.tenKH,
                           nv.tenNV,
                           hd.ngayLap,
                           hd.tongTien
                    FROM HoaDon hd
                    INNER JOIN NhanVien nv ON hd.maNV = nv.maNV
                    LEFT JOIN KhachHang kh ON hd.maKH = kh.maKH
                    ORDER BY hd.ngayLap DESC";

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
                throw new Exception("Lỗi khi lấy danh sách hóa đơn: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return dt;
        }

        // Thêm hóa đơn
        public bool ThemHoaDon(HoaDonDTO hd)
        {
            try
            {
                OpenConnection();

                string query = @"
                    INSERT INTO HoaDon(maHD, maKH, maNV, ngayLap, tongTien)
                    VALUES(@maHD, @maKH, @maNV, @ngayLap, @tongTien)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maHD", hd.MaHD);
                    cmd.Parameters.AddWithValue("@maKH", hd.MaKH);
                    cmd.Parameters.AddWithValue("@maNV", hd.MaNV);
                    cmd.Parameters.AddWithValue("@ngayLap", hd.NgayLap);
                    cmd.Parameters.AddWithValue("@tongTien", hd.TongTien);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm hóa đơn: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // Thêm chi tiết hóa đơn
        public bool ThemChiTietHoaDon(ChiTietHDDTO ct)
        {
            try
            {
                OpenConnection();

                string query = @"
                    INSERT INTO ChiTietHD(maHD, maBienThe, soLuongBan, donGiaBan)
                    VALUES(@maHD, @maBienThe, @soLuongBan, @donGiaBan)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maHD", ct.MaHD);
                    cmd.Parameters.AddWithValue("@maBienThe", ct.MaBienThe);
                    cmd.Parameters.AddWithValue("@soLuongBan", ct.SoLuongBan);
                    cmd.Parameters.AddWithValue("@donGiaBan", ct.DonGiaBan);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chi tiết hóa đơn: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // Lấy chi tiết hóa đơn
        public DataTable LayChiTietHoaDon(string maHD)
        {
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();

                string query = @"
                    SELECT ct.maHD,
                           sp.tenSP,
                           bt.mauSac,
                           bt.kichCo,
                           ct.soLuongBan,
                           ct.donGiaBan,
                           (ct.soLuongBan * ct.donGiaBan) AS ThanhTien
                    FROM ChiTietHD ct
                    INNER JOIN BienTheSP bt ON ct.maBienThe = bt.maBienThe
                    INNER JOIN SanPham sp ON bt.maSP = sp.maSP
                    WHERE ct.maHD = @maHD";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maHD", maHD);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy chi tiết hóa đơn: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return dt;
        }

        // Tìm hóa đơn theo mã
        public DataTable TimHoaDon(string maHD)
        {
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();

                string query = @"
                    SELECT hd.maHD,
                           kh.tenKH,
                           nv.tenNV,
                           hd.ngayLap,
                           hd.tongTien
                    FROM HoaDon hd
                    INNER JOIN NhanVien nv ON hd.maNV = nv.maNV
                    LEFT JOIN KhachHang kh ON hd.maKH = kh.maKH
                    WHERE hd.maHD LIKE @maHD";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maHD", "%" + maHD + "%");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm hóa đơn: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return dt;
        }

        // Xóa hóa đơn
        public bool XoaHoaDon(string maHD)
        {
            try
            {
                OpenConnection();

                // Xóa chi tiết hóa đơn trước
                string queryCT = "DELETE FROM ChiTietHD WHERE maHD = @maHD";

                using (SqlCommand cmd = new SqlCommand(queryCT, conn))
                {
                    cmd.Parameters.AddWithValue("@maHD", maHD);
                    cmd.ExecuteNonQuery();
                }

                // Xóa hóa đơn
                string queryHD = "DELETE FROM HoaDon WHERE maHD = @maHD";

                using (SqlCommand cmd = new SqlCommand(queryHD, conn))
                {
                    cmd.Parameters.AddWithValue("@maHD", maHD);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa hóa đơn: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
