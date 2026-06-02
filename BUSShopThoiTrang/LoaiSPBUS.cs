using DALShopThoiTrang;
using DTOQuanLyThoiTrang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSShopThoiTrang
{
    public class LoaiSPBUS
    {
        private LoaiSPRepository _loaiSPRepo = new LoaiSPRepository();

        public List<LoaiSPDTO> LayTatCaLoaiSP()
        {
            var data = _loaiSPRepo.LayTatCaLoaiSP();

            // Ứng dụng LINQ: Sắp xếp tên loại sản phẩm theo bảng chữ cái A-Z 
            // để người dùng dễ tìm kiếm khi mở ComboBox
            return data.OrderBy(loai => loai.TenLoai).ToList();
        }
        // 1. Thêm
        public string ThemLoaiSP(LoaiSPDTO loai)
        {
            if (string.IsNullOrWhiteSpace(loai.MaLoai)) return "Mã loại không được để trống!";
            if (string.IsNullOrWhiteSpace(loai.TenLoai)) return "Tên loại không được để trống!";

            try
            {
                return _loaiSPRepo.ThemLoaiSP(loai) ? "Thành công" : "Thêm thất bại!";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY") || ex.Message.Contains("duplicate"))
                    return "Mã danh mục này đã tồn tại!";
                return "Lỗi hệ thống: " + ex.Message;
            }
        }

        // 2. Sửa
        public string SuaLoaiSP(LoaiSPDTO loai)
        {
            if (string.IsNullOrWhiteSpace(loai.MaLoai)) return "Không xác định được mã loại cần sửa!";
            if (string.IsNullOrWhiteSpace(loai.TenLoai)) return "Tên loại không được để trống!";

            try
            {
                return _loaiSPRepo.SuaLoaiSP(loai) ? "Thành công" : "Không tìm thấy danh mục để sửa!";
            }
            catch (Exception ex)
            {
                return "Lỗi hệ thống: " + ex.Message;
            }
        }

        // 3. Xóa
        public string XoaLoaiSP(string maLoai)
        {
            if (string.IsNullOrWhiteSpace(maLoai)) return "Vui lòng chọn danh mục cần xóa!";

            try
            {
                return _loaiSPRepo.XoaLoaiSP(maLoai) ? "Thành công" : "Không tìm thấy danh mục để xóa!";
            }
            catch (Exception ex)
            {
                // Bắt lỗi khóa ngoại: Rất quan trọng vì LoaiSP liên kết trực tiếp với bảng SanPham
                if (ex.Message.Contains("REFERENCE") || ex.Message.Contains("FOREIGN KEY"))
                    return "Không thể xóa! Đang có sản phẩm thuộc danh mục này.";

                return "Lỗi hệ thống: " + ex.Message;
            }
        }
    }
}
