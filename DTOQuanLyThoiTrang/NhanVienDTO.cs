using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOShopThoiTrang
{
    public class NhanVienDTO
    {
        private string maNV;
        private string tenNV;
        private string soDienThoai;
        private string chucVu;
        private string trangThai;
        private string matKhau;
        private string phanQuyen;
        private string gioiTinh;
        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string SoDienThoai { get; set; }
        public string ChucVu { get; set; }
        public string TrangThai { get; set; }
        public string MatKhau { get; set; }
        public string PhanQuyen { get; set; }
        public string GioiTinh { get; set; }

        public NhanVienDTO() { }
        public NhanVienDTO(string maNV, string tenNV, string soDienThoai, string chucVu, string trangThai, string matKhau, string phanQuyen, string gioiTinh)
        {
            this.MaNV = maNV;
            this.TenNV = tenNV;
            this.SoDienThoai = soDienThoai;
            this.ChucVu = chucVu;
            this.TrangThai = trangThai;
            this.MatKhau = matKhau;
            this.PhanQuyen = phanQuyen;
            this.GioiTinh = gioiTinh;
        }
    }
}
