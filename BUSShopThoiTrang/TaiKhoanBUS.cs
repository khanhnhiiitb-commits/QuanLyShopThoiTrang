using DALShopThoiTrang;
using DTOShopThoiTrang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSShopThoiTrang
{
    public class TaiKhoanBUS
    {
        TaiKhoanRepository tkRepo = new TaiKhoanRepository();

        // Kiểm tra đăng nhập
        public NhanVienDTO DangNhap(string username, string password)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            return tkRepo.KiemTraDangNhap(username, password);
        }
    }
}

