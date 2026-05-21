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
        ThongKeRepository tkRepo = new ThongKeRepository();

        // Thống kê doanh thu
        public DataTable ThongKeDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            if (tuNgay > denNgay)
            {
                throw new Exception("Ngày bắt đầu không được lớn hơn ngày kết thúc!");
            }

            return tkRepo.ThongKeDoanhThu(tuNgay, denNgay);
        }

        // Thống kê sản phẩm bán chạy
        public DataTable ThongKeSanPhamBanChay(DateTime tuNgay, DateTime denNgay, int top = 10)
        {
            if (tuNgay > denNgay)
            {
                throw new Exception("Ngày bắt đầu không được lớn hơn ngày kết thúc!");
            }

            return tkRepo.ThongKeSanPhamBanChay(tuNgay, denNgay, top);
        }

        // Lấy danh sách hàng tồn kho dưới định mức
        public DataTable LayHangTonKhoDuoiDinhMuc()
        {
            return tkRepo.LayHangTonKhoDuoiDinhMuc();
        }
    }
}
