using DALShopThoiTrang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BUSShopThoiTrang
{
    public class ThongKeBUS
    {
        // Khai báo DUY NHẤT 1 biến kết nối xuống tầng DAL
        private ThongKeRepository _thongKeRepo = new ThongKeRepository();

        // ==============================================================
        // CÁC HÀM CŨ CỦA BẠN (Dùng cho các Form khác)
        // ==============================================================
        public DataTable ThongKeDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            if (tuNgay > denNgay)
                throw new Exception("Ngày bắt đầu không được lớn hơn ngày kết thúc!");
            return _thongKeRepo.ThongKeDoanhThu(tuNgay, denNgay);
        }

        public DataTable ThongKeSanPhamBanChay(DateTime tuNgay, DateTime denNgay, int top = 10)
        {
            if (tuNgay > denNgay)
                throw new Exception("Ngày bắt đầu không được lớn hơn ngày kết thúc!");
            return _thongKeRepo.ThongKeSanPhamBanChay(tuNgay, denNgay, top);
        }

        public DataTable LayHangTonKhoDuoiDinhMuc()
        {
            return _thongKeRepo.LayHangTonKhoDuoiDinhMuc();
        }

        // ==============================================================
        // CÁC HÀM MỚI DÀNH CHO DASHBOARD (ucReports)
        // ==============================================================
        public DataTable LayDoanhThuTheoNgay()
        {
            return _thongKeRepo.LayDoanhThuTheoNgay();
        }

        public DataTable LayTop5BanChay()
        {
            return _thongKeRepo.LayTop5BanChay();
        }

        public DataTable LayBaoCaoTonKho()
        {
            return _thongKeRepo.LayBaoCaoTonKho();
        }

        public DataTable LayTiLeHoanHang()
        {
            return _thongKeRepo.LayTiLeHoanHang();
        }
    }
}