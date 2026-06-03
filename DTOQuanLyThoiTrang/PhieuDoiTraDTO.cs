using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOQuanLyThoiTrang
{
    public class PhieuDoiTraDTO
    {
        private string maPDT;
        private string maHD;
        private string maNV;
        private DateTime ngayLap;
        private string ghiChu;
        private decimal tongTienHoan;
        public string MaPDT { get; set; }
            public string MaHD { get; set; }
            public string MaNV { get; set; }
            public DateTime NgayLap { get; set; }
            public string GhiChu { get; set; }
            public decimal TongTienHoan { get; set; }
        public PhieuDoiTraDTO() { }
        public PhieuDoiTraDTO(string maPDT, string maHD, string maNV, DateTime ngayLap, string ghiChu, decimal tongTienHoan)
        {
            this.MaPDT = maPDT;
            this.MaHD = maHD;
            this.MaNV = maNV;
            this.ngayLap = ngayLap;
            this.ghiChu = ghiChu;
            this.TongTienHoan = tongTienHoan;
        }
    }
}
