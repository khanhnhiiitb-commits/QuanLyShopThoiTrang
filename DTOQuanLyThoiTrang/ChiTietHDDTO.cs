using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOQuanLyThoiTrang
{
    public class ChiTietHDDTO
    {
        private string maHD;
        private string maBienThe;
        private int soLuongBan;
        private decimal donGiaBan;

        public string MaHD { get; set; }
        public string MaBienThe { get; set; }
        public int SoLuongBan { get; set; }
        public decimal DonGiaBan { get; set; }

        public ChiTietHDDTO() { }

        public ChiTietHDDTO(string maHD, string maBienThe,
                            int soLuongBan, decimal donGiaBan)
        {
            this.MaHD = maHD;
            this.MaBienThe = maBienThe;
            this.SoLuongBan = soLuongBan;
            this.DonGiaBan = donGiaBan;
        }
    }
}
