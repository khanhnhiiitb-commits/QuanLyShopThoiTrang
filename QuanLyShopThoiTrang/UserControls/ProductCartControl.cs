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
    public partial class ProductCartControl : UserControl
    {

        private SanPhamDTO _sanPham;
        private BienTheDTO _bienThe;
        private int _soLuong;
        public BienTheDTO BienTheHienTai => _bienThe;
        public decimal ThanhTien => _sanPham.GiaBan * _soLuong;
        public ProductCartControl()
        {
            InitializeComponent();
        }
        // HÀM ĐỔ DỮ LIỆU
        public void SetData(SanPhamDTO sp, BienTheDTO bt, int soLuong)
        {
            _sanPham = sp;
            _bienThe = bt;
            _soLuong = soLuong;

            // 1. Gán thông tin cơ bản
            lblTenSP.Text = sp.TenSP;
            lblBienThe.Text = $"{bt.MauSac} - Size {bt.KichCo}";
            lblSoLuong.Text = soLuong.ToString(); 

            decimal thanhTien = sp.GiaBan * soLuong;
            lblGiaSP.Text =  thanhTien.ToString("N0") + "đ" ;

            // 3. Xử lý load hình ảnh an toàn
            try
            {
                // Nối đường dẫn vào thư mục Images trong bin/Debug
                string imagePath = System.Windows.Forms.Application.StartupPath + "\\Images\\" + sp.HinhAnh;

                if (System.IO.File.Exists(imagePath))
                {
                    picSP.Image = System.Drawing.Image.FromFile(imagePath);
                    picSP.SizeMode = PictureBoxSizeMode.Zoom; // Căn ảnh gọn vào khung
                }
            }
            catch
            {
                // Nếu có lỗi (ví dụ file ảnh bị hỏng), hệ thống sẽ bỏ qua và giữ khung trắng
            }
        }
        // Khai báo sự kiện để báo cho Form chính biết khi số lượng thay đổi
        public event Action<BienTheDTO, int> OnQuantityChanged;

        // Sự kiện cho nút GIẢM (-)
        private void btnGiam_Click(object sender, EventArgs e)
        {
            if (_soLuong > 1)
            {
                _soLuong--;
                CapNhatGiaoDienSuaSoLuong();
            }
        }

        // Sự kiện cho nút TĂNG (+)
        private void btnTang_Click(object sender, EventArgs e)
        {
            // Bạn có thể check thêm điều kiện với _bienThe.SoLuongTon nếu muốn chặn không cho tăng quá số lượng trong kho
            _soLuong++;
            CapNhatGiaoDienSuaSoLuong();
        }

        // Hàm phụ để tính toán lại tại chỗ và bắn tín hiệu ra Form chính
        private void CapNhatGiaoDienSuaSoLuong()
        {
            lblSoLuong.Text = _soLuong.ToString();

            decimal thanhTienMoi = _sanPham.GiaBan * _soLuong;
            lblGiaSP.Text =thanhTienMoi.ToString("N0") + "đ";

            // Bắn sự kiện ra ngoài Form chính để tính lại tổng tiền hóa đơn (Subtotal, Total...)
            if (OnQuantityChanged != null)
            {
                OnQuantityChanged(_bienThe, _soLuong);
            }
        }
        public void TangSoLuong(int soLuongThem)
        {
            _soLuong += soLuongThem;
            CapNhatGiaoDienSuaSoLuong(); // Gọi lại hàm cập nhật chữ và tiền ở bước trước
        }
    }
}
