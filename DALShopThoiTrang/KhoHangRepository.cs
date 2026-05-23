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
    public class KhoHangRepository : DBConnection
    {
        // Lấy toàn bộ tồn kho (kết hợp thông tin sản phẩm + biến thể)
        public DataTable LayDanhSachTonKho()
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                string query = @"
                SELECT bt.maBienThe  AS MaBienThe,
                       sp.maSP       AS MaSanPham,
                       sp.tenSP      AS TenSanPham,
                       bt.mauSac     AS MauSac,
                       bt.kichCo     AS KichCo,
                       bt.soLuongTon AS SoLuongTon,
                       bt.dinhMucToiThieu AS DinhMucToiThieu,
                       sp.giaNhap    AS GiaNhap,
                       sp.giaBan     AS GiaBan
                FROM BienTheSP bt
                INNER JOIN SanPham sp ON bt.maSP = sp.maSP
                ORDER BY sp.tenSP, bt.mauSac, bt.kichCo";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách tồn kho: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }

        // Lấy danh sách hàng tồn kho dưới định mức tối thiểu
        public DataTable LayHangDuoiDinhMuc()
        {
            DataTable dt = new DataTable();
            try
            {
                OpenConnection();
                string query = @"
                SELECT bt.maBienThe       AS MaBienThe,
                       sp.tenSP           AS TenSanPham,
                       bt.mauSac          AS MauSac,
                       bt.kichCo          AS KichCo,
                       bt.soLuongTon      AS SoLuongTon,
                       bt.dinhMucToiThieu AS DinhMucToiThieu
                FROM BienTheSP bt
                INNER JOIN SanPham sp ON bt.maSP = sp.maSP
                WHERE bt.soLuongTon <= bt.dinhMucToiThieu
                ORDER BY bt.soLuongTon ASC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy hàng dưới định mức: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return dt;
        }

        // Thêm phiếu nhập hàng (header + chi tiết + cập nhật tồn kho trong 1 transaction)
        public bool ThemPhieuNhap(PhieuNhapDTO phieu, List<ChiTietPNDTO> danhSachChiTiet)
        {
            try
            {
                OpenConnection();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Thêm phiếu nhập
                        string queryPN = @"
                        INSERT INTO PhieuNhap (maPN, maNV, ngayNhap, tongTienNhap, ghiChu)
                        VALUES (@maPN, @maNV, @ngayNhap, @tongTienNhap, @ghiChu)";

                        using (SqlCommand cmd = new SqlCommand(queryPN, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@maPN", phieu.MaPN);
                            cmd.Parameters.AddWithValue("@maNV", phieu.MaNV);
                            cmd.Parameters.AddWithValue("@ngayNhap", phieu.NgayNhap);
                            cmd.Parameters.AddWithValue("@tongTienNhap", phieu.TongTienNhap);
                            cmd.Parameters.AddWithValue("@ghiChu", phieu.GhiChu ?? "");
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Thêm chi tiết + cập nhật tồn kho
                        foreach (var ct in danhSachChiTiet)
                        {
                            string queryChiTiet = @"
                            INSERT INTO ChiTietPN (maPN, maBienThe, soLuongNhap, donGiaNhap)
                            VALUES (@maPN, @maBienThe, @soLuongNhap, @donGiaNhap)";

                            using (SqlCommand cmd = new SqlCommand(queryChiTiet, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@maPN", ct.MaPN);
                                cmd.Parameters.AddWithValue("@maBienThe", ct.MaBienThe);
                                cmd.Parameters.AddWithValue("@soLuongNhap", ct.SoLuongNhap);
                                cmd.Parameters.AddWithValue("@donGiaNhap", ct.DonGiaNhap);
                                cmd.ExecuteNonQuery();
                            }

                            // Cập nhật số lượng tồn kho
                            string queryTonKho = @"
                            UPDATE BienTheSP
                            SET soLuongTon = soLuongTon + @soLuongNhap
                            WHERE maBienThe = @maBienThe";

                            using (SqlCommand cmd = new SqlCommand(queryTonKho, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@soLuongNhap", ct.SoLuongNhap);
                                cmd.Parameters.AddWithValue("@maBienThe", ct.MaBienThe);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm phiếu nhập: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        // Lấy danh sách tất cả phiếu nhập
        public List<PhieuNhapDTO> LayDanhSachPhieuNhap()
        {
            List<PhieuNhapDTO> list = new List<PhieuNhapDTO>();
            try
            {
                OpenConnection();
                string query = @"
                SELECT maPN, maNV, ngayNhap, tongTienNhap, ghiChu
                FROM PhieuNhap
                ORDER BY ngayNhap DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PhieuNhapDTO
                        {
                            MaPN = reader["maPN"].ToString(),
                            MaNV = reader["maNV"].ToString(),
                            NgayNhap = Convert.ToDateTime(reader["ngayNhap"]),
                            TongTienNhap = Convert.ToDecimal(reader["tongTienNhap"]),
                            GhiChu = reader["ghiChu"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách phiếu nhập: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return list;
        }

        // Lấy chi tiết của một phiếu nhập theo mã phiếu
        public List<ChiTietPNDTO> LayChiTietPhieuNhap(string maPN)
        {
            List<ChiTietPNDTO> list = new List<ChiTietPNDTO>();
            try
            {
                OpenConnection();
                string query = @"
                SELECT maPN, maBienThe, soLuongNhap, donGiaNhap
                FROM ChiTietPN
                WHERE maPN = @maPN";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maPN", maPN);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ChiTietPNDTO
                            {
                                MaPN = reader["maPN"].ToString(),
                                MaBienThe = reader["maBienThe"].ToString(),
                                SoLuongNhap = Convert.ToInt32(reader["soLuongNhap"]),
                                DonGiaNhap = Convert.ToDecimal(reader["donGiaNhap"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy chi tiết phiếu nhập: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return list;
        }

        // Cập nhật định mức tối thiểu của một biến thể sản phẩm
        public bool CapNhatDinhMuc(string maBienThe, int dinhMucMoi)
        {
            try
            {
                OpenConnection();
                string query = @"
                UPDATE BienTheSP
                SET dinhMucToiThieu = @dinhMuc
                WHERE maBienThe = @maBienThe";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@dinhMuc", dinhMucMoi);
                    cmd.Parameters.AddWithValue("@maBienThe", maBienThe);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật định mức: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public int LaySoLuongTon(string maBienThe)
        {
            int soLuong = 0;
            try
            {
                OpenConnection();
                string query = "SELECT soLuongTon FROM BienTheSP WHERE maBienThe = @maBienThe";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@maBienThe", maBienThe);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        soLuong = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi kiểm tra số lượng tồn kho: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return soLuong;
        }
    }
}

