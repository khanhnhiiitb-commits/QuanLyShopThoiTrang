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
            flpDanhSachSP.Controls.Clear();

            // Yêu cầu BUS lấy danh sách SẢN PHẨM GỐC (Bảng SanPham)
            List<SanPhamDTO> listSanPham = sanPhamBUS.LayTatCaSanPham();

            foreach (var sp in listSanPham)
            {
                ProductCardControl card = new ProductCardControl();
                card.SetData(sp);

                // Đăng ký sự kiện click
                card.OnProductSelected += Card_OnProductSelected;

                flpDanhSachSP.Controls.Add(card);
            }
        }

        private void Card_OnProductSelected(SanPhamDTO spDuocChon)
        {
            // 1. Truyền thẳng đối tượng spDuocChon vào thay vì dùng spDuocChon.MaSP
            FormChonBienThe frmPopup = new FormChonBienThe(spDuocChon);

            // 2. Mở form lên dưới dạng hộp thoại bắt buộc (ShowDialog)
            if (frmPopup.ShowDialog() == DialogResult.OK)
            {
                // 3. Gọi đúng tên biến mới mà chúng ta đã thiết lập bên form Popup
                BienTheDTO bienTheDaChon = frmPopup.BienTheDaChon;
                int soLuong = frmPopup.SoLuong;

                // 4. TIẾN HÀNH THÊM VÀO GIỎ HÀNG BÊN PHẢI MÀN HÌNH
                ThemVaoGioHang(spDuocChon, bienTheDaChon, soLuong);
            }
        }

        // Hàm xử lý việc đưa sản phẩm lên lưới (DataGridView) giỏ hàng bên phải
        private void ThemVaoGioHang(SanPhamDTO sp, BienTheDTO bt, int soLuong)
        {
            foreach (ProductCartControl item in flpCurrentOrder.Controls)
            {
                // Nếu mã biến thể vừa chọn đã tồn tại trong một thẻ nào đó
                if (item.BienTheHienTai.MaBienThe == bt.MaBienThe)
                {
                    item.TangSoLuong(soLuong); // Chỉ tăng số lượng
                    TinhTongTienHoaDon();      // Tính lại tổng tiền
                    return;                    // Dừng hàm tại đây, KHÔNG tạo thẻ mới nữa
                }
            }
            ProductCartControl itemTrongGio = new ProductCartControl();
            itemTrongGio.SetData(sp, bt, soLuong);
            
            MessageBox.Show($"Đã thêm thành công!\nSản phẩm: {sp.TenSP}\nMàu: {bt.MauSac} - Size: {bt.KichCo}\nSố lượng: {soLuong}", "Thông báo");

            itemTrongGio.OnQuantityChanged += (bienThe, soLuongMoi) =>
            {
                if (soLuongMoi == 0)
                {
                    flpCurrentOrder.Controls.Remove(itemTrongGio);
                }
                TinhTongTienHoaDon();
            };
            flpCurrentOrder.Controls.Add(itemTrongGio);

            // Tính lại tổng tiền sau khi thêm món mới
            TinhTongTienHoaDon();

        }

        private void TinhTongTienHoaDon()
        {
            decimal subTotal = 0;

            // Quét tất cả các thẻ đang nằm trong khung chứa giỏ hàng (flpCurrentOrder)
            foreach (ProductCartControl item in flpCurrentOrder.Controls)
            {
                subTotal += item.ThanhTien;
            }

            // Cập nhật lên nhãn Subtotal (Bạn nhớ đổi tên label cho khớp với UI nhé)
            lblSubtotal.Text = "$" + subTotal.ToString("N0");

            // Giả sử Thuế VAT là 10% (0.1)
            decimal tax = subTotal * 0.1m;
            lblTax.Text = "$" + tax.ToString("N0");

            // Tính tổng cuối cùng
            decimal total = subTotal + tax;
            lblTotal.Text = "$" + total.ToString("N0");
        }
    }
}
