using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOQuanLyThoiTrang
{
    internal class PhieuDoiTraDTO
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
        
    }
}
