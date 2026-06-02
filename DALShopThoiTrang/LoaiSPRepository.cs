using DTOQuanLyThoiTrang;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALShopThoiTrang
{
    public class LoaiSPRepository : DBConnection
    {
        public List<LoaiSPDTO> LayTatCaLoaiSP()
        {
            var list = new List<LoaiSPDTO>();
            var query = "SELECT maLoai, tenLoai FROM LoaiSP";

            try
            {
                OpenConnection();
                using (var cmd = new SqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LoaiSPDTO
                            {
                                MaLoai = reader["maLoai"].ToString(),
                                TenLoai = reader["tenLoai"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi truy vấn LoaiSP: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return list;
        }
        // 1. Thêm loại sản phẩm
        public bool ThemLoaiSP(LoaiSPDTO loai)
        {
            var query = "INSERT INTO LoaiSP (maLoai, tenLoai) VALUES (@maLoai, @tenLoai)";
            try
            {
                OpenConnection();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maLoai", loai.MaLoai);
                    cmd.Parameters.AddWithValue("@tenLoai", loai.TenLoai);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DAL Thêm: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // 2. Sửa loại sản phẩm
        public bool SuaLoaiSP(LoaiSPDTO loai)
        {
            var query = "UPDATE LoaiSP SET tenLoai = @tenLoai WHERE maLoai = @maLoai";
            try
            {
                OpenConnection();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maLoai", loai.MaLoai);
                    cmd.Parameters.AddWithValue("@tenLoai", loai.TenLoai);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DAL Sửa: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // 3. Xóa loại sản phẩm
        public bool XoaLoaiSP(string maLoai)
        {
            var query = "DELETE FROM LoaiSP WHERE maLoai = @maLoai";
            try
            {
                OpenConnection();
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maLoai", maLoai);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi DAL Xóa: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
