using DTOQuanLyThoiTrang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALShopThoiTrang;   

namespace BUSShopThoiTrang
{
    public class KhoHangBUS
    {
        private readonly KhoHangRepository _khoHangRepo = new KhoHangRepository();

        // Lấy toàn bộ danh sách tồn kho
        public DataTable LayDanhSachTonKho()
        {
            return _khoHangRepo.LayDanhSachTonKho();
        }

        // Lấy danh sách hàng tồn kho dưới định mức tối thiểu
        public DataTable LayHangDuoiDinhMuc()
        {
            return _khoHangRepo.LayHangDuoiDinhMuc();
        }

        // Nhập hàng: validate rồi gọi repository
        public bool NhapHang(PhieuNhapDTO phieu, List<ChiTietPNDTO> danhSachChiTiet)
        {
            if (phieu == null)
                throw new ArgumentNullException("phieu", "Thông tin phiếu nhập không được để trống.");

            if (string.IsNullOrWhiteSpace(phieu.MaPN))
                throw new Exception("Mã phiếu nhập không được để trống.");

            if (string.IsNullOrWhiteSpace(phieu.MaNV))
                throw new Exception("Mã nhân viên không được để trống.");

            if (danhSachChiTiet == null || danhSachChiTiet.Count == 0)
                throw new Exception("Phiếu nhập phải có ít nhất một sản phẩm.");

            foreach (var ct in danhSachChiTiet)
            {
                if (ct.SoLuongNhap <= 0)
                    throw new Exception($"Số lượng nhập của biến thể '{ct.MaBienThe}' phải lớn hơn 0.");

                if (ct.DonGiaNhap <= 0)
                    throw new Exception($"Đơn giá nhập của biến thể '{ct.MaBienThe}' phải lớn hơn 0.");
            }

            // Tính lại tổng tiền nhập
            decimal tongTien = 0;
            foreach (var ct in danhSachChiTiet)
                tongTien += ct.SoLuongNhap * ct.DonGiaNhap;
            phieu.TongTienNhap = tongTien;

            return _khoHangRepo.ThemPhieuNhap(phieu, danhSachChiTiet);
        }

        // Lấy danh sách tất cả phiếu nhập
        public List<PhieuNhapDTO> LayDanhSachPhieuNhap()
        {
            return _khoHangRepo.LayDanhSachPhieuNhap();
        }

        // Lấy chi tiết một phiếu nhập
        public List<ChiTietPNDTO> LayChiTietPhieuNhap(string maPN)
        {
            if (string.IsNullOrWhiteSpace(maPN))
                throw new Exception("Mã phiếu nhập không được để trống.");

            return _khoHangRepo.LayChiTietPhieuNhap(maPN);
        }

        // Cập nhật định mức tối thiểu
        public bool CapNhatDinhMuc(string maBienThe, int dinhMucMoi)
        {
            if (string.IsNullOrWhiteSpace(maBienThe))
                throw new Exception("Mã biến thể không được để trống.");

            if (dinhMucMoi < 0)
                throw new Exception("Định mức tối thiểu không được âm.");

            return _khoHangRepo.CapNhatDinhMuc(maBienThe, dinhMucMoi);
        }
    }
}

