using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOQuanLyThoiTrang;
using DTOShopThoiTrang;

namespace DALShopThoiTrang
{ 
    public class SanPhamRepository : DBConnection
    {
        // 1. Lấy danh sách toàn bộ sản phẩm
        public List<SanPhamDTO> LayDanhSachSanPham()
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            try
            {
                OpenConnection();
                string query = "SELECT maSP, maLoai, tenSP, giaNhap, giaBan, moTa FROM SanPham";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new SanPhamDTO
                        {
                            MaSP = reader["maSP"].ToString(),
                            MaLoai = reader["maLoai"].ToString(),
                            TenSP = reader["tenSP"].ToString(),
                            GiaNhap = Convert.ToDecimal(reader["giaNhap"]),
                            GiaBan = Convert.ToDecimal(reader["giaBan"]),
                            MoTa = reader["moTa"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách sản phẩm: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return list;
        }

        // 2. Thêm sản phẩm mới
        public bool ThemSanPham(SanPhamDTO sp)
        {
            try
            {
                OpenConnection();
                string query = @"
                INSERT INTO SanPham (maSP, maLoai, tenSP, giaNhap, giaBan, moTa) 
                VALUES (@maSP, @maLoai, @tenSP, @giaNhap, @giaBan, @moTa)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", sp.MaSP);
                    cmd.Parameters.AddWithValue("@maLoai", sp.MaLoai);
                    cmd.Parameters.AddWithValue("@tenSP", sp.TenSP);
                    cmd.Parameters.AddWithValue("@giaNhap", sp.GiaNhap);
                    cmd.Parameters.AddWithValue("@giaBan", sp.GiaBan);
                    // Xử lý trường hợp MoTa bị null
                    cmd.Parameters.AddWithValue("@moTa", string.IsNullOrEmpty(sp.MoTa) ? (object)DBNull.Value : sp.MoTa);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm sản phẩm: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // 3. Cập nhật thông tin sản phẩm
        public bool SuaSanPham(SanPhamDTO sp)
        {
            try
            {
                OpenConnection();
                string query = @"
                UPDATE SanPham 
                SET maLoai = @maLoai, tenSP = @tenSP, giaNhap = @giaNhap, giaBan = @giaBan, moTa = @moTa
                WHERE maSP = @maSP";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", sp.MaSP);
                    cmd.Parameters.AddWithValue("@maLoai", sp.MaLoai);
                    cmd.Parameters.AddWithValue("@tenSP", sp.TenSP);
                    cmd.Parameters.AddWithValue("@giaNhap", sp.GiaNhap);
                    cmd.Parameters.AddWithValue("@giaBan", sp.GiaBan);
                    cmd.Parameters.AddWithValue("@moTa", string.IsNullOrEmpty(sp.MoTa) ? (object)DBNull.Value : sp.MoTa);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi sửa sản phẩm: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // 4. Xóa sản phẩm
        public bool XoaSanPham(string maSP)
        {
            try
            {
                OpenConnection();
                // Vì DTO không có cột 'TrangThai', nên mình dùng DELETE FROM. 
                // Lưu ý: Nếu sản phẩm này đã có trong ChiTietHoaDon, SQL Server sẽ báo lỗi khóa ngoại.
                string query = "DELETE FROM SanPham WHERE maSP = @maSP";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", maSP);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa sản phẩm: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // 5. Tìm kiếm sản phẩm theo tên
        public List<SanPhamDTO> TimKiemSanPhamTheoTen(string tuKhoa)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            try
            {
                OpenConnection();
                string query = "SELECT maSP, maLoai, tenSP, giaNhap, giaBan, moTa FROM SanPham WHERE tenSP LIKE @tuKhoa";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@tuKhoa", "%" + tuKhoa + "%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SanPhamDTO
                            {
                                MaSP = reader["maSP"].ToString(),
                                MaLoai = reader["maLoai"].ToString(),
                                TenSP = reader["tenSP"].ToString(),
                                GiaNhap = Convert.ToDecimal(reader["giaNhap"]),
                                GiaBan = Convert.ToDecimal(reader["giaBan"]),
                                MoTa = reader["moTa"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tìm kiếm sản phẩm: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return list;
        }

        public SanPhamDTO LaySanPhamGoc(string maSP)
        {
            SanPhamDTO sp = null;
            try
            {
                OpenConnection();
                string query = "SELECT * FROM SanPham WHERE maSP = @maSP";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maSP", maSP);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Nếu tìm thấy sản phẩm
                        {
                            sp = new SanPhamDTO
                            {
                                MaSP = reader["maSP"].ToString(),
                                MaLoai = reader["maLoai"].ToString(),
                                TenSP = reader["tenSP"].ToString(),
                                GiaNhap = Convert.ToDecimal(reader["giaNhap"]),
                                GiaBan = Convert.ToDecimal(reader["giaBan"]),
                                MoTa = reader["moTa"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy thông tin sản phẩm gốc: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return sp; 
        }

        // Hàm lấy toàn bộ danh sách biến thể sản phẩm
        public List<BienTheDTO> LayTatCaBienTheSanPham()
        {
            List<BienTheDTO> danhSachBienThe = new List<BienTheDTO>();
            try
            {
                OpenConnection();

                // Mẹo: Đối với màn hình bán hàng, bạn có thể thêm "WHERE soLuongTon > 0" 
                // để ẩn đi các sản phẩm đã hết hàng. Ở đây mình tạm lấy hết.
                string query = "SELECT * FROM BienTheSP";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) // Lặp qua từng dòng dữ liệu
                        {
                            BienTheDTO bt = new BienTheDTO
                            {
                                MaBienThe = reader["maBienThe"].ToString(),
                                MaSP = reader["maSP"].ToString(),
                                MauSac = reader["mauSac"].ToString(),
                                KichCo = reader["kichCo"].ToString(),
                                DinhMucToiThieu = Convert.ToInt32(reader["dinhMucToiThieu"]),
                                SoLuongTon = Convert.ToInt32(reader["soLuongTon"]),
                                MoTa = reader["moTa"].ToString()
                            };
                            danhSachBienThe.Add(bt); // Thêm vào danh sách
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách biến thể: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return danhSachBienThe;
        }
        public List<SanPhamDTO> LayTatCaSanPham()
        {
            List<SanPhamDTO> danhSachSanPham = new List<SanPhamDTO>();

            // Câu lệnh SQL lấy tất cả dữ liệu từ bảng gốc
            string query = "SELECT maSP, maLoai, tenSP, giaNhap, giaBan, moTa, hinhAnh FROM SanPham";

            try
            {
                // BƯỚC 2: Gọi hàm mở kết nối đã được viết sẵn bên DBConnection
                OpenConnection();

                // BƯỚC 3: Dùng thẳng biến 'conn' được thừa kế
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SanPhamDTO sp = new SanPhamDTO();
                            sp.MaSP = reader["maSP"].ToString();
                            sp.MaLoai = reader["maLoai"].ToString();
                            sp.TenSP = reader["tenSP"].ToString();
                            sp.GiaNhap = Convert.ToDecimal(reader["giaNhap"]);
                            sp.GiaBan = Convert.ToDecimal(reader["giaBan"]);

                            // Xử lý an toàn khi cột bị Null
                            sp.MoTa = reader["moTa"] != DBNull.Value ? reader["moTa"].ToString() : "";

                            try
                            {
                                sp.HinhAnh = reader["hinhAnh"] != DBNull.Value ? reader["hinhAnh"].ToString() : "";
                            }
                            catch
                            {
                                sp.HinhAnh = "";
                            }

                            danhSachSanPham.Add(sp);
                        }
                    }
                }
            }
            finally
            {
                // BƯỚC 4: Cuối cùng luôn đóng kết nối để tránh treo Database
                CloseConnection();
            }

            return danhSachSanPham;
        }
        public List<BienTheDTO> LayBienTheTheoMaSP(string maSP)
        {
            List<BienTheDTO> list = new List<BienTheDTO>();
            string query = "SELECT * FROM BienTheSP WHERE maSP = @maSP";

            OpenConnection();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@maSP", maSP);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new BienTheDTO
                        {
                            MaBienThe = reader["maBienThe"].ToString(),
                            MaSP = reader["maSP"].ToString(),
                            MauSac = reader["mauSac"].ToString(),
                            KichCo = reader["kichCo"].ToString(),
                            SoLuongTon = Convert.ToInt32(reader["soLuongTon"])
                        });
                    }
                }
            }
            CloseConnection();
            return list;
        }
    }
}
