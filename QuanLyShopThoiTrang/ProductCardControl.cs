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
        
        
            // Khai báo sự kiện khi bấm nút chọn mua (để báo cho Form cha biết)
            public event EventHandler OnProductSelected;

            // Lưu lại thông tin sản phẩm để dùng
            private BienTheDTO productData;

            

            // HÀM CHÍNH: Đổ dữ liệu vào thẻ
            public void SetData(BienTheDTO bt, SanPhamDTO spGoc)
            {
                productData = bt;

                // Đổ tên và màu sắc
                lblTenSP.Text = $"{spGoc.TenSP}";

                // Đổ giá (Định nghĩa dạng tiền tệ)
                lblGiaSP.Text = "$" + spGoc.GiaBan.ToString("N0") ;

                /* Xử lý hình ảnh (Xem thêm lưu ý về hình ảnh phía dưới)
                if (!string.IsNullOrEmpty(bt.MoTa)) // Giả sử MoTa lưu đường dẫn ảnh
                {
                    picAnhSP.Image = Image.FromFile(bt.MoTa);
                }*/
            }

            private void btnChonMua_Click(object sender, EventArgs e)
            {
                // Khi bấm nút, kích hoạt sự kiện để Form cha xử lý thêm vào giỏ hàng
                OnProductSelected?.Invoke(this, e);
            }
        
    }
}
