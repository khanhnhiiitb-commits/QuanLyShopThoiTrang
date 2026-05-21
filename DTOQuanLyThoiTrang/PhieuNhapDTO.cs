using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOQuanLyThoiTrang
{
    public class PhieuNhapDTO
    {
        private string _maPN;
        private string _maNV;
        private DateTime _ngayNhap;
        private decimal _tongTienNhap;
        private string _ghiChu;

        public string MaPN { get => _maPN; set => _maPN = value; }
        public string MaNV { get => _maNV; set => _maNV = value; }
        public DateTime NgayNhap { get => _ngayNhap; set => _ngayNhap = value; }
        public decimal TongTienNhap { get => _tongTienNhap; set => _tongTienNhap = value; }
        public string GhiChu { get => _ghiChu; set => _ghiChu = value; }

        public PhieuNhapDTO() { }

        public PhieuNhapDTO(string maPN, string maNV, DateTime ngayNhap,
                            decimal tongTienNhap, string ghiChu = "")
        {
            _maPN = maPN;
            _maNV = maNV;
            _ngayNhap = ngayNhap;
            _tongTienNhap = tongTienNhap;
            _ghiChu = ghiChu;
        }
    }
}

