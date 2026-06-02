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
        public bool ThemHoaDon(HoaDonDTO hd, List<ChiTietHDDTO> danhSachChiTiet)
        {
            bool isSuccess = false;
            try
            {
                OpenConnection();
                SqlTransaction transaction = conn.BeginTransaction(); // Bắt đầu khóa giao dịch

                try
                {
                    // 1. LƯU HÓA ĐƠN GỐC
                    string queryHD = @"
                        INSERT INTO HoaDon(maHD, maKH, maNV, ngayLap, tongTien)
                        VALUES(@maHD, @maKH, @maNV, @ngayLap, @tongTien)";

                    using (SqlCommand cmdHD = new SqlCommand(queryHD, conn, transaction))
                    {
                        cmdHD.Parameters.AddWithValue("@maHD", hd.MaHD);
                        // Xử lý trường hợp khách vãng lai (không có mã khách hàng)
                        cmdHD.Parameters.AddWithValue("@maKH", string.IsNullOrEmpty(hd.MaKH) ? (object)DBNull.Value : hd.MaKH);
                        cmdHD.Parameters.AddWithValue("@maNV", hd.MaNV);
                        cmdHD.Parameters.AddWithValue("@ngayLap", hd.NgayLap);
                        cmdHD.Parameters.AddWithValue("@tongTien", hd.TongTien);
                        cmdHD.ExecuteNonQuery();
                    }

                    // 2. LƯU CHI TIẾT VÀ TRỪ TỒN KHO
                    string queryCT = @"
                        INSERT INTO ChiTietHD(maHD, maBienThe, soLuongBan, donGiaBan)
                        VALUES(@maHD, @maBienThe, @soLuongBan, @donGiaBan)";
                    string queryUpdateKho = "UPDATE BienTheSP SET soLuongTon = soLuongTon - @soLuongBan WHERE maBienThe = @maBienThe";

                    foreach (var ct in danhSachChiTiet)
                    {
                        using (SqlCommand cmdCT = new SqlCommand(queryCT, conn, transaction))
                        {
                            cmdCT.Parameters.AddWithValue("@maHD", hd.MaHD); // Lấy mã HD gốc
                            cmdCT.Parameters.AddWithValue("@maBienThe", ct.MaBienThe);
                            cmdCT.Parameters.AddWithValue("@soLuongBan", ct.SoLuongBan);
                            cmdCT.Parameters.AddWithValue("@donGiaBan", ct.DonGiaBan);
                            cmdCT.ExecuteNonQuery();
                        }

                        using (SqlCommand cmdKho = new SqlCommand(queryUpdateKho, conn, transaction))
                        {
                            cmdKho.Parameters.AddWithValue("@soLuongBan", ct.SoLuongBan);
                            cmdKho.Parameters.AddWithValue("@maBienThe", ct.MaBienThe);
                            cmdKho.ExecuteNonQuery();
                        }
                    }

                    // Hoàn tất lưu dữ liệu
                    transaction.Commit();
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Hủy bỏ toàn bộ nếu có lỗi
                    throw new Exception("Lỗi khi thêm hóa đơn, đã hoàn tác: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kết nối: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return isSuccess;
        }

        // Xóa hóa đơn an toàn với Transaction
        public bool XoaHoaDon(string maHD)
        {
            bool isSuccess = false;
            try
            {
                OpenConnection();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {

                    // 1. Xóa chi tiết hóa đơn trước
                    string queryCT = "DELETE FROM ChiTietHD WHERE maHD = @maHD";
                    using (SqlCommand cmd = new SqlCommand(queryCT, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@maHD", maHD);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Xóa hóa đơn gốc
                    string queryHD = "DELETE FROM HoaDon WHERE maHD = @maHD";
                    using (SqlCommand cmd = new SqlCommand(queryHD, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@maHD", maHD);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Lỗi khi xóa hóa đơn, đã hoàn tác: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kết nối: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return isSuccess;
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
        // Lập phiếu đổi trả an toàn với SqlTransaction
        public bool ThemPhieuDoiTra(PhieuDoiTraDTO pdt, List<ChiTietPDTDTO> danhSachChiTiet)
        {
            bool isSuccess = false;
            try
            {
                OpenConnection();
                SqlTransaction transaction = conn.BeginTransaction(); // Khởi tạo Transaction

                try
                {
                    // 1. THÊM DỮ LIỆU VÀO BẢNG PHIEUDOITRA
                    string queryPDT = @"
                        INSERT INTO PhieuDoiTra (maPDT, maHD, maNV, ngayDoiTra, lyDo, tongTienHoan)
                        VALUES (@maPDT, @maHD, @maNV, @ngayDoiTra, @lyDo, @tongTienHoan)";

                    using (SqlCommand cmdPDT = new SqlCommand(queryPDT, conn, transaction))
                    {
                        cmdPDT.Parameters.AddWithValue("@maPDT", pdt.MaPDT);
                        cmdPDT.Parameters.AddWithValue("@maHD", pdt.MaHD);
                        cmdPDT.Parameters.AddWithValue("@maNV", pdt.MaNV);
                        cmdPDT.Parameters.AddWithValue("@ngayDoiTra", pdt.NgayDoiTra);
                        cmdPDT.Parameters.AddWithValue("@lyDo", string.IsNullOrEmpty(pdt.LyDo) ? (object)DBNull.Value : pdt.LyDo);
                        cmdPDT.Parameters.AddWithValue("@tongTienHoan", pdt.TongTienHoan);

                        cmdPDT.ExecuteNonQuery();
                    }
                    string queryCT = @"
                        INSERT INTO ChiTietPDT (maPDT, maBienThe, soLuong, hinhThuc)
                        VALUES (@maPDT, @maBienThe, @soLuong, @hinhThuc)";

                    foreach (var ct in danhSachChiTiet)
                    {
                        using (SqlCommand cmdCT = new SqlCommand(queryCT, conn, transaction))
                        {
                            cmdCT.Parameters.AddWithValue("@maPDT", pdt.MaPDT);
                            cmdCT.Parameters.AddWithValue("@maBienThe", ct.MaBienThe);
                            cmdCT.Parameters.AddWithValue("@soLuong", ct.SoLuong);
                            cmdCT.Parameters.AddWithValue("@hinhThuc", ct.HinhThuc);

                            cmdCT.ExecuteNonQuery();
                        }
                        string queryKho = "";

                        // Nếu là hàng khách trả lại -> Cộng lại số lượng vào kho 
                        if (ct.HinhThuc == "Trả hàng" || ct.HinhThuc == "Nhận trả")
                        {
                            queryKho = "UPDATE BienTheSP SET soLuongTon = soLuongTon + @soLuong WHERE maBienThe = @maBienThe";
                        }
                        // Nếu là hàng mới xuất kho đổi cho khách -> Trừ bớt số lượng trong kho
                        else if (ct.HinhThuc == "Đổi hàng" || ct.HinhThuc == "Đổi mới")
                        {
                            queryKho = "UPDATE BienTheSP SET soLuongTon = soLuongTon - @soLuong WHERE maBienThe = @maBienThe";
                        }

                        if (!string.IsNullOrEmpty(queryKho))
                        {
                            using (SqlCommand cmdKho = new SqlCommand(queryKho, conn, transaction))
                            {
                                cmdKho.Parameters.AddWithValue("@soLuong", ct.SoLuong);
                                cmdKho.Parameters.AddWithValue("@maBienThe", ct.MaBienThe);

                                cmdKho.ExecuteNonQuery();
                            }
                        }
                    }

                    // Chốt giao dịch thành công
                    transaction.Commit();
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Hoàn tác toàn bộ nếu xuất hiện lỗi giữa chừng
                    throw new Exception("Lỗi khi xử lý lưu phiếu đổi trả, hệ thống đã hoàn tác dữ liệu: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return isSuccess;
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
        public bool ThanhToanGiaoDich(HoaDonDTO hd, List<ChiTietHDDTO> listChiTiet)
        {
            OpenConnection();
            // Khởi tạo Transaction để đảm bảo an toàn dữ liệu
            using (SqlTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    // 1. Lưu Hóa Đơn
                    string sqlHD = "INSERT INTO HoaDon (maHD, maKH, maNV, ngayLap, tongTien, phuongThucThanhToan) VALUES (@maHD, @maKH, @maNV, @ngayLap, @tongTien, @phuongThuc)";
                    SqlCommand cmdHD = new SqlCommand(sqlHD, conn, trans);
                    cmdHD.Parameters.AddWithValue("@maHD", hd.MaHD);
                    cmdHD.Parameters.AddWithValue("@maKH", hd.MaKH);
                    cmdHD.Parameters.AddWithValue("@maNV", hd.MaNV);
                    cmdHD.Parameters.AddWithValue("@ngayLap", hd.NgayLap);
                    cmdHD.Parameters.AddWithValue("@tongTien", hd.TongTien);
                    cmdHD.Parameters.AddWithValue("@phuongThuc", hd.PhuongThucThanhToan);
                    cmdHD.ExecuteNonQuery();

                    // 2. Lưu từng Chi tiết Hóa Đơn & Trừ Tồn Kho
                    foreach (var ct in listChiTiet)
                    {
                        // Thêm chi tiết
                        string sqlCT = "INSERT INTO ChiTietHD (maHD, maBienThe, soLuongBan, donGiaBan) VALUES (@maHD, @maBienThe, @sl, @donGia)";
                        SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
                        cmdCT.Parameters.AddWithValue("@maHD", ct.MaHD);
                        cmdCT.Parameters.AddWithValue("@maBienThe", ct.MaBienThe);
                        cmdCT.Parameters.AddWithValue("@sl", ct.SoLuongBan);
                        cmdCT.Parameters.AddWithValue("@donGia", ct.DonGiaBan);
                        cmdCT.ExecuteNonQuery();

                        // Trừ tồn kho trong bảng BienTheSP
                        string sqlKho = "UPDATE BienTheSP SET soLuongTon = soLuongTon - @sl WHERE maBienThe = @maBienThe";
                        SqlCommand cmdKho = new SqlCommand(sqlKho, conn, trans);
                        cmdKho.Parameters.AddWithValue("@sl", ct.SoLuongBan);
                        cmdKho.Parameters.AddWithValue("@maBienThe", ct.MaBienThe);
                        cmdKho.ExecuteNonQuery();
                    }

                    // Nếu mọi thứ trót lọt, chốt lưu dữ liệu!
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    // Có lỗi thì quay xe (Rollback)
                    trans.Rollback();
                    throw new Exception("Lỗi khi lưu hóa đơn: " + ex.Message);
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

    }


}
