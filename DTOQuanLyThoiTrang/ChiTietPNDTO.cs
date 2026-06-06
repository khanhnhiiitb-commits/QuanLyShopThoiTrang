using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOQuanLyThoiTrang
{
    public class ChiTietPNDTO     
    {
        private string _maPN;
        private string _maBienThe;
        private int _soLuongNhap;
        private decimal _donGiaNhap;

        public string MaPN { get => _maPN; set => _maPN = value; }
        public string MaBienThe { get => _maBienThe; set => _maBienThe = value; }
        public int SoLuongNhap { get => _soLuongNhap; set => _soLuongNhap = value; }
        public decimal DonGiaNhap { get => _donGiaNhap; set => _donGiaNhap = value; }
        public decimal ThanhTien { get; set; }

        public ChiTietPNDTO() { }

        public ChiTietPNDTO(string maPN, string maBienThe,
                            int soLuongNhap, decimal donGiaNhap, decimal thanhTien)
        {
            _maPN = maPN;
            _maBienThe = maBienThe;
            _soLuongNhap = soLuongNhap;
            _donGiaNhap = donGiaNhap;
            ThanhTien = thanhTien;
        }
    }
}

