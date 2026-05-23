using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOQuanLyThoiTrang
{
    public class ChiTietPDTDTO
    {
        private string maPDT;
        private string maBienThe;
        private int soLuong;
        private string hinhThuc;
        public string MaPDT { get; set; }
        public string MaBienThe { get; set; }
        public int SoLuong { get; set; }
        public string HinhThuc { get; set; }

        public ChiTietPDTDTO() { }
        public ChiTietPDTDTO(string maPDT, string maBienThe, int soLuong, string hinhThuc)
        {
            MaPDT = maPDT;
            MaBienThe = maBienThe;
            SoLuong = soLuong;
            HinhThuc = hinhThuc;
        }
    }
}
