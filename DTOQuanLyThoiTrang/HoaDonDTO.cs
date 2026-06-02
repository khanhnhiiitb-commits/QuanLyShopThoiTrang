using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOQuanLyThoiTrang
{
    public class HoaDonDTO
    {
        private string maHD;
        private string maKH;
        private string maNV;
        private DateTime ngayLap;
        private decimal tongTien;
        private string phuongthucThanhToan; 

        public string PhuongThucThanhToan
        {
            get { return phuongthucThanhToan; }
            set { phuongthucThanhToan = value; }
        }   
        public string MaHD { get; set; }
        public string MaKH { get; set; }
        public string MaNV { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }

        public HoaDonDTO() { }

        public HoaDonDTO(string maHD, string maKH, string maNV,
                         DateTime ngayLap, decimal tongTien)
        {
            this.MaHD = maHD;
            this.MaKH = maKH;
            this.MaNV = maNV;
            this.NgayLap = ngayLap;
            this.TongTien = tongTien;
            this.PhuongThucThanhToan = "Chưa xác định";
        }
    }
}
