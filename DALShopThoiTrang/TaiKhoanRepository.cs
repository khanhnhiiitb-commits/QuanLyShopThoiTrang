using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTOShopThoiTrang; 

namespace DALShopThoiTrang
{
    public class TaiKhoanRepository : DBConnection
    {
        public NhanVienDTO KiemTraDangNhap(string username, string password)
        {
            NhanVienDTO nv = null;
            try
            {
                OpenConnection();
                string query = "SELECT * FROM NhanVien WHERE maNV = @maNV AND matKhau = @matKhau AND trangThai = N'Đang làm việc'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maNV", username);
                    cmd.Parameters.AddWithValue("@matKhau", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nv = new NhanVienDTO
                            {
                                MaNV = reader["maNV"].ToString(),
                                TenNV = reader["tenNV"].ToString(),
                                SoDienThoai = reader["soDienThoai"].ToString(),
                                ChucVu = reader["chucVu"].ToString(),
                                TrangThai = reader["trangThai"].ToString(),
                                PhanQuyen = reader["phanQuyen"].ToString(),
                                GioiTinh = reader["gioiTinh"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi kết nối CSDL: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return nv; 
        }
        public DataTable LayDanhSachNhanVien()
        {
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();

                string query = "SELECT * FROM NhanVien";

                SqlDataAdapter adapter =
                    new SqlDataAdapter(query, conn);

                adapter.Fill(dt);
            }
            finally
            {
                CloseConnection();
            }

            return dt;
        }
        public bool ThemNhanVien(NhanVienDTO nv)
        {
            try
            {
                OpenConnection();

                string query = @"INSERT INTO NhanVien
VALUES
(
    @maNV,
    @tenNV,
    @soDienThoai,
    @chucVu,
    N'Đang làm việc',
    @matKhau,
    @phanQuyen,
    @gioiTinh
)";

                SqlCommand cmd =
                    new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@maNV", nv.MaNV);
                cmd.Parameters.AddWithValue("@tenNV", nv.TenNV);
                cmd.Parameters.AddWithValue("@soDienThoai", nv.SoDienThoai);
                cmd.Parameters.AddWithValue("@chucVu", nv.ChucVu);
                
                cmd.Parameters.AddWithValue("@matKhau", nv.MatKhau);
                cmd.Parameters.AddWithValue("@phanQuyen", nv.PhanQuyen);
                cmd.Parameters.AddWithValue("@gioiTinh", nv.GioiTinh);

                return cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool SuaNhanVien(NhanVienDTO nv)
        {
            try
            {
                OpenConnection();

                string query = @"UPDATE NhanVien
                        SET tenNV=@tenNV,
                            soDienThoai=@soDienThoai,
                            chucVu=@chucVu,
                            matKhau=@matKhau,
                            phanQuyen=@phanQuyen,
                            gioiTinh=@gioiTinh
                        WHERE maNV=@maNV";

                SqlCommand cmd =
                    new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@maNV", nv.MaNV);
                cmd.Parameters.AddWithValue("@tenNV", nv.TenNV);
                cmd.Parameters.AddWithValue("@soDienThoai", nv.SoDienThoai);
                cmd.Parameters.AddWithValue("@chucVu", nv.ChucVu);
                
                cmd.Parameters.AddWithValue("@matKhau", nv.MatKhau);
                cmd.Parameters.AddWithValue("@phanQuyen", nv.PhanQuyen);
                cmd.Parameters.AddWithValue("@gioiTinh", nv.GioiTinh);

                return cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public DataTable TimNhanVien(string tuKhoa)
        {
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();

                string query = @"SELECT *
                         FROM NhanVien
                         WHERE maNV LIKE @tuKhoa
                            OR tenNV LIKE @tuKhoa";

                SqlCommand cmd =
                    new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue(
                    "@tuKhoa",
                    "%" + tuKhoa + "%");

                SqlDataAdapter adapter =
                    new SqlDataAdapter(cmd);

                adapter.Fill(dt);
            }
            finally
            {
                CloseConnection();
            }

            return dt;
        }
        public bool KhoaNhanVien(string maNV)
        {
            try
            {
                OpenConnection();

                string query =
                    "UPDATE NhanVien SET trangThai = N'Nghỉ việc' WHERE maNV = @maNV";

                SqlCommand cmd =
                    new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@maNV", maNV);

                return cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool MoKhoaNhanVien(string maNV)
        {
            try
            {
                OpenConnection();

                string query =
                    "UPDATE NhanVien SET trangThai = N'Đang làm việc' WHERE maNV = @maNV";

                SqlCommand cmd =
                    new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@maNV", maNV);

                return cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
