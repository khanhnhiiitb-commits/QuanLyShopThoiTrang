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

            try
            {
                string imagePath = System.Windows.Forms.Application.StartupPath + "\\Images\\" + spGoc.HinhAnh;

                if (System.IO.File.Exists(imagePath))
                {
                    // Trường hợp 1: Có file và load thành công
                    picAnhSP.Image = System.Drawing.Image.FromFile(imagePath);
                    picAnhSP.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    // Trường hợp 2: Báo rõ đường dẫn đang bị thiếu file
                    MessageBox.Show("MÁY TÍNH BÁO KHÔNG TÌM THẤY FILE TẠI:\n\n" + imagePath, "LỖI THIẾU FILE");
                    picAnhSP.Image = null;
                }
            }
            catch (Exception ex)
            {
                // Trường hợp 3: Bắt lỗi nếu file tồn tại nhưng bị hỏng hoặc C# không đọc được
                MessageBox.Show("CÓ TÌM THẤY FILE NHƯNG MỞ BỊ LỖI!\nChi tiết: " + ex.Message, "LỖI ĐỌC ẢNH");
                picAnhSP.Image = null;
            }
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
