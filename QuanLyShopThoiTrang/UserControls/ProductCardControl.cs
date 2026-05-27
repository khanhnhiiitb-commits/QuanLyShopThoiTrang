using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTOQuanLyThoiTrang;
using DTOShopThoiTrang;

namespace QuanLyShopThoiTrang
{
    public partial class ProductCardControl : UserControl
    {
        public ProductCardControl()
        {
            InitializeComponent();
        }
        // Khai báo sự kiện trả về đúng đối tượng SanPhamDTO
        public event Action<SanPhamDTO> OnProductSelected;

        // Biến cục bộ để lưu sản phẩm hiện tại của thẻ
        private SanPhamDTO _sanPhamHienTai;

        public void SetData(SanPhamDTO spGoc)
        {
            _sanPhamHienTai = spGoc; // Lưu lại để dùng khi click

            // Gán dữ liệu lên giao diện thẻ
            lblTenSP.Text = spGoc.TenSP;
            lblGiaSP.Text = spGoc.GiaBan.ToString("N0") + "đ";
        }

        // Bấm nút Add thì gọi sự kiện này
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (OnProductSelected != null)
            {
                OnProductSelected(_sanPhamHienTai); // Ném đúng sản phẩm gốc ra ngoài Form chính
            }
        }

    }
}
