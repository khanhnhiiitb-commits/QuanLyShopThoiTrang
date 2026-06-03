using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using DALShopThoiTrang;
using DTOQuanLyThoiTrang;
using DTOShopThoiTrang;
using UtilsShopThoiTrang;

namespace BUSShopThoiTrang
{
    public class GiaoDichBUS
    {
        private GiaoDichRepository giaoDichDAL;
        private KhoHangRepository khoHangDAL;

        public GiaoDichBUS()
        {
            giaoDichDAL = new GiaoDichRepository();
            khoHangDAL = new KhoHangRepository();
        }

        //Lập hóa đơn bán hàng 
        public bool LapHoaDon(HoaDonDTO hoaDon, List<ChiTietHDDTO> danhSachChiTiet)
        {
            if (danhSachChiTiet == null || danhSachChiTiet.Count == 0)
            {
                throw new Exception("Hóa đơn phải có ít nhất một sản phẩm để thanh toán!");
            }

            if (hoaDon.TongTien <= 0)
            {
                throw new Exception("Tổng tiền hóa đơn không hợp lệ!");
            }
            foreach (var item in danhSachChiTiet)
            {
                int tonKhoHienTai = khoHangDAL.LaySoLuongTon(item.MaBienThe);

                if (tonKhoHienTai < item.SoLuongBan)
                {
                    throw new Exception($"Biến thể mã {item.MaBienThe} không đủ số lượng trong kho! (Chỉ còn: {tonKhoHienTai})");
                }
            }
            return giaoDichDAL.ThemHoaDon(hoaDon, danhSachChiTiet);
        }
        public bool ThanhToanGiaoDich(HoaDonDTO hd, List<ChiTietHDDTO> listChiTiet)
        {
            return giaoDichDAL.ThanhToanGiaoDich(hd, listChiTiet);
        }
        //Xử lý thanh toán MoMo
        public async Task<string> YeuCauThanhToanMoMo(string maHD, decimal tongTien)
        {
            if (string.IsNullOrEmpty(maHD) || tongTien <= 0)
            {
                throw new Exception("Dữ liệu thanh toán MoMo không hợp lệ.");
            }

            // Gọi thẳng sang hàm API vừa viết
            string payUrl = await UtilsMoMoAPI.CreatePaymentRequest(maHD, tongTien);
            return payUrl;
        }

        //Lập phiếu đổi trả 
        // Lưu ý: Cần truyền thêm DateTime ngayLapHoaDonGoc để kiểm tra chính sách
        public bool LapPhieuDoiTra(PhieuDoiTraDTO phieuDoiTra, List<ChiTietPDTDTO> danhSachChiTietDoiTra, DateTime ngayLapHoaDonGoc)
        {
            if (danhSachChiTietDoiTra == null || danhSachChiTietDoiTra.Count == 0)
            {
                throw new Exception("Phiếu đổi trả phải chứa ít nhất một sản phẩm!");
            }
            TimeSpan khoangCachNgay = phieuDoiTra.NgayDoiTra - ngayLapHoaDonGoc;
            if (khoangCachNgay.TotalDays > 7)
            {
                throw new Exception("Đã quá hạn 7 ngày theo chính sách. Không thể thực hiện đổi trả cho hóa đơn này!");
            }

            
            return giaoDichDAL.ThemPhieuDoiTra(phieuDoiTra, danhSachChiTietDoiTra);
        }
        public DataTable LayDanhSachChiTietDeTraHang(string maHD)
        {
            return giaoDichDAL.LayDanhSachChiTietDeTraHang(maHD);
        }

        public DataTable LayChiTietHoaDon(string maHD)
        {
            return giaoDichDAL.LayChiTietHoaDon(maHD);
        }
    }
}