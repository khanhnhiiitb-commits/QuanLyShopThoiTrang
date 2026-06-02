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
        public int SoLuongMua => _soLuong;
        public SanPhamDTO SanPhamHienTai => _sanPham;
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
            lblGiaSP.Text = thanhTien.ToString("N0") + "đ";

            // 2. Xử lý load hình ảnh thông minh (An toàn)
            try
            {
                // Ưu tiên 1: Lấy tên ảnh riêng của biến thể (Ví dụ: áo màu đen)
                // Ưu tiên 2: Nếu biến thể không có ảnh riêng, tự động lùi về dùng ảnh gốc của sản phẩm

                string tenAnh = !string.IsNullOrEmpty(bt.HinhAnh) ? bt.HinhAnh : sp.HinhAnh;

                string imagePath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Images", tenAnh);

                if (System.IO.File.Exists(imagePath))
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(imagePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        picSP.Image = System.Drawing.Image.FromStream(fs);
                    }
                    picSP.SizeMode = PictureBoxSizeMode.Zoom; // Căn ảnh gọn vào khung
                }
                else
                {
                    picSP.Image = null;
                }
            }
            catch
            {
                picSP.Image = null;
            }
        }
        // Khai báo sự kiện để báo cho Form chính biết khi số lượng thay đổi
        public event Action<BienTheDTO, int> OnQuantityChanged;

        // Sự kiện cho nút GIẢM (-)
        private void btnGiam_Click(object sender, EventArgs e)
        {
            _soLuong--; // Giảm số lượng đi 1

            if (_soLuong == 0)
            {
                // 1. Bắn sự kiện ra Form chính truyền số lượng = 0 
                // (Để Form chính biết mà trừ đi phần tiền của món này và xóa khỏi danh sách lưu trữ)
                if (OnQuantityChanged != null)
                {
                    OnQuantityChanged(_bienThe, 0);
                }

                //Tự động gỡ bản thân cái thẻ này khỏi giao diện giỏ hàng
                if (this.Parent != null)
                {
                    this.Parent.Controls.Remove(this);
                }

                //Giải phóng hoàn toàn khỏi bộ nhớ
                this.Dispose();
            }
            else
            {
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
