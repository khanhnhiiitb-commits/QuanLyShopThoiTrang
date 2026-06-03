using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOQuanLyThoiTrang
{
    public class KhachHangDTO
    {
       private string maKH;
       private string tenKH;
       private string soDienThoai;
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public string SoDienThoai { get; set; }

        public KhachHangDTO() { }
        public KhachHangDTO(string maKH, string tenKH, string soDienThoai)
        {
            this.MaKH = maKH;
            this.TenKH = tenKH;
            this.SoDienThoai = soDienThoai;
        }


    }
}
