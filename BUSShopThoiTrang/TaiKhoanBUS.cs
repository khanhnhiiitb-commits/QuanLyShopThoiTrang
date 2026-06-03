using DALShopThoiTrang;
using DTOShopThoiTrang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSShopThoiTrang
{
    public class TaiKhoanBUS
    {
        TaiKhoanRepository tkRepo = new TaiKhoanRepository();
        public static NhanVienDTO TaiKhoanHienTai;
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
        public DataTable LayDanhSachNhanVien()
        {
            return tkRepo.LayDanhSachNhanVien();
        }

        public bool ThemNhanVien(NhanVienDTO nv)
        {
            return tkRepo.ThemNhanVien(nv);
        }

        public bool SuaNhanVien(NhanVienDTO nv)
        {
            return tkRepo.SuaNhanVien(nv);
        }

        public DataTable TimNhanVien(string tuKhoa)
        {
            return tkRepo.TimNhanVien(tuKhoa);
        }

        public bool KhoaNhanVien(string maNV)
        {
            return tkRepo.KhoaNhanVien(maNV);
        }

        public bool MoKhoaNhanVien(string maNV)
        {
            return tkRepo.MoKhoaNhanVien(maNV);
        }
        public bool CapNhatTaiKhoan(NhanVienDTO nv)
        {
            return tkRepo.CapNhatTaiKhoan(nv);
        }
    }
}

