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
        private DateTime ngayDoiTra;
        private string lyDo;
        private decimal tongTienHoan;
        public string MaPDT { get; set; }
            public string MaHD { get; set; }
            public string MaNV { get; set; }
            public DateTime NgayDoiTra { get; set; }
            public string LyDo { get; set; }
            public decimal TongTienHoan { get; set; }
        public PhieuDoiTraDTO() { }
        public PhieuDoiTraDTO(string maPDT, string maHD, string maNV, DateTime ngayDoiTra, string lyDo, decimal tongTienHoan)
        {
            this.MaPDT = maPDT;
            this.MaHD = maHD;
            this.MaNV = maNV;
            this.NgayDoiTra = ngayDoiTra;
            this.LyDo = lyDo;
            this.TongTienHoan = tongTienHoan;
        }
    }
}
