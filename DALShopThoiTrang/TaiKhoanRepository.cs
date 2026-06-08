using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DTOShopThoiTrang;
using DALShopThoiTrang.EF; // Sửa lại dòng này theo đúng tên thư mục EF của bạn

namespace DALShopThoiTrang
{
    public class TaiKhoanRepository
    {
        private ShopThoiTrangEntities db = new ShopThoiTrangEntities();
        public NhanVienDTO KiemTraDangNhap(string username, string password)
        {
            var nv = db.NhanViens.FirstOrDefault(x => x.maNV == username && x.matKhau == password && x.trangThai == "Đang làm việc");
            if (nv != null)
            {
                return new NhanVienDTO
                {
                    MaNV = nv.maNV,
                    TenNV = nv.tenNV,
                    SoDienThoai = nv.soDienThoai,
                    ChucVu = nv.chucVu,
                    TrangThai = nv.trangThai,
                    PhanQuyen = nv.phanQuyen,
                    GioiTinh = nv.gioiTinh
                };
            }
            return null;
        }

        public List<NhanVienDTO> LayDanhSachNhanVien()
        {
            return db.NhanViens.Where(x => x.phanQuyen == "Staff").Select(nv => new NhanVienDTO
            {
                MaNV = nv.maNV,
                TenNV = nv.tenNV,
                SoDienThoai = nv.soDienThoai,
                ChucVu = nv.chucVu,
                TrangThai = nv.trangThai,
                PhanQuyen = nv.phanQuyen,
                GioiTinh = nv.gioiTinh
            }).ToList();
        }

        public bool ThemNhanVien(NhanVienDTO nvDTO)
        {
            try
            {
                var nvMoi = new NhanVien
                {
                    maNV = nvDTO.MaNV,
                    tenNV = nvDTO.TenNV,
                    soDienThoai = nvDTO.SoDienThoai,
                    chucVu = nvDTO.ChucVu,
                    trangThai = "Đang làm việc",
                    matKhau = nvDTO.MatKhau,
                    phanQuyen = nvDTO.PhanQuyen,
                    gioiTinh = nvDTO.GioiTinh
                };
                db.NhanViens.Add(nvMoi);
                return db.SaveChanges() > 0;
            }
            catch { return false; }
        }

        public bool SuaNhanVien(NhanVienDTO nvDTO)
        {
            try
            {
                var nvCu = db.NhanViens.FirstOrDefault(x => x.maNV == nvDTO.MaNV);
                if (nvCu != null)
                {
                    nvCu.tenNV = nvDTO.TenNV;
                    nvCu.soDienThoai = nvDTO.SoDienThoai;
                    nvCu.chucVu = nvDTO.ChucVu;
                    nvCu.matKhau = nvDTO.MatKhau;
                    nvCu.phanQuyen = nvDTO.PhanQuyen;
                    nvCu.gioiTinh = nvDTO.GioiTinh;
                    return db.SaveChanges() > 0;
                }
                return false;
            }
            catch { return false; }
        }

        public List<NhanVienDTO> TimNhanVien(string tuKhoa)
        {
            return db.NhanViens.Where(nv => nv.phanQuyen == "Staff" &&
                                          (nv.maNV.Contains(tuKhoa) ||
                                           nv.tenNV.Contains(tuKhoa) ||
                                           nv.soDienThoai.Contains(tuKhoa)))
                              .Select(nv => new NhanVienDTO
                              {
                                  MaNV = nv.maNV,
                                  TenNV = nv.tenNV,
                                  SoDienThoai = nv.soDienThoai,
                                  ChucVu = nv.chucVu,
                                  TrangThai = nv.trangThai,
                                  PhanQuyen = nv.phanQuyen,
                                  GioiTinh = nv.gioiTinh
                              }).ToList();
        }

        public bool KhoaNhanVien(string maNV)
        {
            var nv = db.NhanViens.FirstOrDefault(x => x.maNV == maNV);
            if (nv != null) { nv.trangThai = "Nghỉ việc"; return db.SaveChanges() > 0; }
            return false;
        }

        public bool MoKhoaNhanVien(string maNV)
        {
            var nv = db.NhanViens.FirstOrDefault(x => x.maNV == maNV);
            if (nv != null) { nv.trangThai = "Đang làm việc"; return db.SaveChanges() > 0; }
            return false;
        }

        public bool CapNhatTaiKhoan(NhanVienDTO nvDTO)
        {
            var nvCu = db.NhanViens.FirstOrDefault(x => x.maNV == nvDTO.MaNV);
            if (nvCu != null)
            {
                nvCu.tenNV = nvDTO.TenNV;
                nvCu.soDienThoai = nvDTO.SoDienThoai;
                nvCu.gioiTinh = nvDTO.GioiTinh;
                nvCu.matKhau = nvDTO.MatKhau;
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}