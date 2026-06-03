using System;
using System.Data;
using System.Data.SqlClient;
using DTOQuanLyThoiTrang;
using DTOShopThoiTrang; 

namespace DALShopThoiTrang
{
    public class KhachHangRepository : DBConnection
    {
        // 1. Tìm khách hàng theo số điện thoại
        public KhachHangDTO TimKhachHangTheoSDT(string sdt)
        {
            KhachHangDTO kh = null;
            try
            {
                OpenConnection();
                string sql = "SELECT * FROM KhachHang WHERE soDienThoai = @sdt";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    kh = new KhachHangDTO()
                    {
                        MaKH = reader["maKH"].ToString(),
                        TenKH = reader["tenKH"].ToString(),
                        SoDienThoai = reader["soDienThoai"].ToString()
                    };
                }
            }
            catch (Exception ex) { throw new Exception("Lỗi tìm khách hàng: " + ex.Message); }
            finally { CloseConnection(); }
            return kh;
        }

        // 2. Tự động sinh mã Khách Hàng mới (Ví dụ: KH05 -> KH06)
        public string TaoMaKhachHangMoi()
        {
            string maMoi = "KH01";
            try
            {
                OpenConnection();
                // Lấy mã lớn nhất hiện tại
                string sql = "SELECT TOP 1 maKH FROM KhachHang ORDER BY maKH DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();

                if (result != null && result.ToString() != "")
                {
                    string maCu = result.ToString(); // VD: "KH05"
                    // Cắt bỏ 2 chữ "KH", lấy phần số, ép sang kiểu int và +1
                    int so = int.Parse(maCu.Substring(2)) + 1;
                    // Ghép lại thành chuỗi mới, "D2" giúp format thành 2 chữ số (06 thay vì 6)
                    maMoi = "KH" + so.ToString("D2");
                }
            }
            catch (Exception ex) { throw new Exception("Lỗi tạo mã KH: " + ex.Message); }
            finally { CloseConnection(); }
            return maMoi;
        }

        // 3. Thêm khách hàng mới
        public bool ThemKhachHang(KhachHangDTO kh)
        {
            try
            {
                OpenConnection();
                string sql = "INSERT INTO KhachHang (maKH, tenKH, soDienThoai) VALUES (@maKH, @tenKH, @sdt)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@maKH", kh.MaKH);
                cmd.Parameters.AddWithValue("@tenKH", kh.TenKH);
                cmd.Parameters.AddWithValue("@sdt", kh.SoDienThoai);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex) { throw new Exception("Lỗi thêm khách hàng: " + ex.Message); }
            finally { CloseConnection(); }
        }
    }
}