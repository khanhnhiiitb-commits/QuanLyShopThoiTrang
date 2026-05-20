using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOQuanLyThoiTrang
{
    public class BienTheDTO
    {
        private string _maBienThe;
        private string _maSP;
        private string _mauSac;
        private string _kichCo;
        private int _dinhMucToiThieu;
        private int _soLuongTon;
        private string _moTa;

        public string MaBienThe { get => _maBienThe; set => _maBienThe = value; }
        public string MaSP { get => _maSP; set => _maSP = value; }
        public string MauSac { get => _mauSac; set => _mauSac = value; }
        public string KichCo { get => _kichCo; set => _kichCo = value; }
        public int DinhMucToiThieu { get => _dinhMucToiThieu; set => _dinhMucToiThieu = value; }
        public int SoLuongTon { get => _soLuongTon; set => _soLuongTon = value; }
        public string MoTa { get => _moTa; set => _moTa = value; }

        public BienTheDTO(string MaBienThe, string MaSP, string MauSac, string KichCo, int DinhMucToiThieu, int SoLuongTon, string MoTa)
        {
            _maBienThe = MaBienThe;
            _maSP = MaSP;
            _mauSac = MauSac;
            _kichCo = KichCo;
            _moTa = MoTa;
            _dinhMucToiThieu = DinhMucToiThieu;
            _soLuongTon = SoLuongTon;
        }

        public BienTheDTO()
        {
        }
    }
}
