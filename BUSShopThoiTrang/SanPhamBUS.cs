using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALShopThoiTrang;
using DTOQuanLyThoiTrang;
using DTOShopThoiTrang;

namespace BUSShopThoiTrang
{
    public class SanPhamBUS
    {
        private SanPhamRepository _sanPhamRepo = new SanPhamRepository();

        // NHÓM 1: CÁC NGHIỆP VỤ CƠ BẢN (CRUD)
        public SanPhamDTO LaySanPhamGoc(string maSP)
        {
            // Kiểm tra tính hợp lệ của dữ liệu đầu vào
            if (string.IsNullOrEmpty(maSP))
            {
                return null;
            }

            return _sanPhamRepo.LaySanPhamGoc(maSP);
        }
        public List<BienTheDTO> LayTatCaBienTheSanPham()
        {
            // Nếu sau này bạn cần lọc thêm điều kiện (ví dụ: chỉ lấy hàng đang kinh doanh),
            // bạn có thể viết logic kiểm tra (If/Else) ở đây trước khi gọi DAL.

            return _sanPhamRepo.LayTatCaBienTheSanPham();
        }
        // 1. Lấy danh sách sản phẩm
        public List<SanPhamDTO> LayDanhSachSanPham()
        {
            return _sanPhamRepo.LayDanhSachSanPham();
        }

        // 2. Thêm sản phẩm
        public string ThemSanPham(SanPhamDTO sp)
        {
            // Kiểm tra dữ liệu đầu vào (Validation)
            if (string.IsNullOrWhiteSpace(sp.MaSP))
                return "Mã sản phẩm không được để trống!";

            if (string.IsNullOrWhiteSpace(sp.TenSP))
                return "Tên sản phẩm không được để trống!";

            if (string.IsNullOrWhiteSpace(sp.MaLoai))
                return "Vui lòng chọn loại sản phẩm!";

            if (sp.GiaNhap < 0 || sp.GiaBan < 0)
                return "Giá nhập và giá bán không được là số âm!";

            if (sp.GiaBan < sp.GiaNhap)
                return "Giá bán không hợp lý (phải lớn hơn hoặc bằng giá nhập)!";

            try
            {
                if (_sanPhamRepo.ThemSanPham(sp))
                    return "Thành công";

                return "Thêm sản phẩm thất bại!";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY") || ex.Message.Contains("duplicate"))
                    return "Mã sản phẩm này đã tồn tại trong hệ thống. Vui lòng nhập mã khác!";

                return "Lỗi hệ thống: " + ex.Message;
            }
        }

        // 3. Sửa sản phẩm
        public string SuaSanPham(SanPhamDTO sp)
        {
            if (string.IsNullOrWhiteSpace(sp.MaSP))
                return "Mã sản phẩm không hợp lệ!";

            if (string.IsNullOrWhiteSpace(sp.TenSP))
                return "Tên sản phẩm không được để trống!";

            if (sp.GiaNhap < 0 || sp.GiaBan < 0)
                return "Giá nhập và giá bán không được là số âm!";

            if (sp.GiaBan < sp.GiaNhap)
                return "Giá bán không hợp lý (phải lớn hơn hoặc bằng giá nhập)!";

            try
            {
                if (_sanPhamRepo.SuaSanPham(sp))
                    return "Thành công";

                return "Không tìm thấy sản phẩm để cập nhật!";
            }
            catch (Exception ex)
            {
                return "Lỗi hệ thống: " + ex.Message;
            }
        }

        // 4. Xóa sản phẩm
        public string XoaSanPham(string maSP)
        {
            if (string.IsNullOrWhiteSpace(maSP))
                return "Vui lòng chọn một sản phẩm cần xóa!";

            try
            {
                if (_sanPhamRepo.XoaSanPham(maSP))
                    return "Thành công";

                return "Không tìm thấy sản phẩm để xóa!";
            }
            catch (Exception ex)
            {
                // Bắt lỗi khóa ngoại (Foreign Key) 
                if (ex.Message.Contains("REFERENCE constraint") || ex.Message.Contains("FOREIGN KEY"))
                    return "Không thể xóa! Sản phẩm này đã phát sinh dữ liệu trong hóa đơn hoặc phiếu nhập kho.";

                return "Lỗi hệ thống: " + ex.Message;
            }
        }

        // 5. Tìm kiếm sản phẩm theo tên (Dùng Query SQL ở DAL)
        public List<SanPhamDTO> TimKiemSanPhamTheoTen(string tuKhoa)
        {
            if (string.IsNullOrWhiteSpace(tuKhoa))
            {
                return _sanPhamRepo.LayDanhSachSanPham();
            }

            tuKhoa = tuKhoa.Trim();
            return _sanPhamRepo.TimKiemSanPhamTheoTen(tuKhoa);
        }

        // NHÓM 2: CÁC NGHIỆP VỤ NÂNG CAO SỬ DỤNG LINQ (RAM Xử Lý)

        // 6. Lọc sản phẩm theo một khoảng giá
        public List<SanPhamDTO> LocSanPhamTheoKhoangGia(decimal giaTu, decimal giaDen)
        {
            List<SanPhamDTO> tatCaSP = _sanPhamRepo.LayDanhSachSanPham();

            var ketQua = tatCaSP.Where(sp => sp.GiaBan >= giaTu && sp.GiaBan <= giaDen)
                                .OrderBy(sp => sp.GiaBan)
                                .ToList();
            return ketQua;
        }

        // 7. Lọc ra Top 5 sản phẩm có giá trị nhập kho cao nhất 
        public List<SanPhamDTO> LayTop5SanPhamGiaTriCao()
        {
            List<SanPhamDTO> tatCaSP = _sanPhamRepo.LayDanhSachSanPham();

            var ketQua = tatCaSP.OrderByDescending(sp => sp.GiaNhap)
                                .Take(5)
                                .ToList();
            return ketQua;
        }

        // 8. Tính tổng tiền vốn (Giá nhập) của tất cả sản phẩm đang có 
        public decimal TinhTongVonNhapHang()
        {
            List<SanPhamDTO> tatCaSP = _sanPhamRepo.LayDanhSachSanPham();

            decimal tongVon = tatCaSP.Sum(sp => sp.GiaNhap);

            return tongVon;
        }
        
        public List<SanPhamDTO> LayTatCaSanPham()
        {
            return _sanPhamRepo.LayTatCaSanPham();
        }

        public List<BienTheDTO> LayBienTheTheoMaSP(string maSP) => _sanPhamRepo.LayBienTheTheoMaSP(maSP);
    }
}

