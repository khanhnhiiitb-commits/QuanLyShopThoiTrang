using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUSShopThoiTrang;
using DTOQuanLyThoiTrang;
using DTOShopThoiTrang;


namespace QuanLyShopThoiTrang
{
    public partial class BanHangForm : Form
    {
        private SanPhamBUS sanPhamBUS = new SanPhamBUS();
        public BanHangForm()
        {
            InitializeComponent();
        }

        private void BanHangForm_Load(object sender, EventArgs e)
        {
            LoadSanPhamDong();
        }
        private void LoadSanPhamDong()
        {
            // BƯỚC A: Clean - Xóa sạch các thẻ cũ trong rổ (nếu có)
            flpDanhSachSP.Controls.Clear();

            // BƯỚC B: Gọi BUS lấy dữ liệu thực tế
            // Giả sử BUS này trả về List<BienTheDTO> (chứa thông tin biến thể và sp gốc)
            List<BienTheDTO> listBienThe = sanPhamBUS.LayTatCaBienTheSanPham();

            // BƯỚC C: Vòng lặp "Thần thánh" để tạo thẻ
            foreach (var bt in listBienThe)
            {
                // 1. Tạo mới một thẻ control
                ProductCardControl card = new ProductCardControl();

                // 2. Cung cấp dữ liệu cho thẻ
                // (Giả sử bạn có hàm LaySanPhamGoc từ mã SP)
                SanPhamDTO spGoc = sanPhamBUS.LaySanPhamGoc(bt.MaSP);
                card.SetData(bt, spGoc);

                // 3. Đăng ký sự kiện click (giỏ hàng)
                card.OnProductSelected += Card_OnProductSelected;

                // 4. CHÍNH SÁCH CHỐT: Ném vào FlowLayoutPanel
                flpDanhSachSP.Controls.Add(card);
            }
        }

        private void Card_OnProductSelected(object sender, EventArgs e)
        {
            // Nhận diện thẻ vừa bấm
            ProductCardControl card = (ProductCardControl)sender;

            // Xử lý thêm sản phẩm này vào DataGridView Giỏ hàng bên phải...
            MessageBox.Show("Đã thêm sản phẩm vào giỏ hàng!");
        }
    }
}
