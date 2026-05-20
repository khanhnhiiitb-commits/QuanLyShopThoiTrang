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
    }
}
