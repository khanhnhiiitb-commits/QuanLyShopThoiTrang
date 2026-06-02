using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOQuanLyThoiTrang
{
    public class LoaiSPDTO
    {
        public string MaLoai { get; set; }
        public string TenLoai { get; set; }

        public LoaiSPDTO() { }

        public LoaiSPDTO(string maLoai, string tenLoai)
        {
            MaLoai = maLoai;
            TenLoai = tenLoai;
        }
    }
}
