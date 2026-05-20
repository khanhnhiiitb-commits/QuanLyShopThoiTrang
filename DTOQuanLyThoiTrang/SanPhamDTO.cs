using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOShopThoiTrang
{
    public class SanPhamDTO
    {
        private string _maSP;
        private string _maLoai;
        private string _tenSP;
        private decimal _giaNhap;
        private decimal _giaBan;
        private string _moTa;

        public string MaSP { get => _maSP; set => _maSP = value; }
        public string MaLoai { get => _maLoai; set => _maLoai = value; }
        public string TenSP { get => _tenSP; set => _tenSP = value; }
        public decimal GiaNhap { get => _giaNhap; set => _giaNhap = value; }
        public decimal GiaBan { get => _giaBan; set => _giaBan = value; }
        public string MoTa { get => _moTa; set => _moTa = value; }

        public SanPhamDTO(string maSP, string maLoai, string tenSP, decimal giaBan, decimal giaNhap, string moTa)
        {
            _maSP = maSP;
            _maLoai = maLoai;
            _tenSP = tenSP;
            _giaNhap = giaNhap;
            _giaBan = giaBan;
            _moTa = moTa;
        }

        public SanPhamDTO()
        {
        }
    }
}
