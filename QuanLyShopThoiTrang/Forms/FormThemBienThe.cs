using System;
using System.Windows.Forms;
using DTOQuanLyThoiTrang;
using BUSShopThoiTrang;

namespace QuanLyShopThoiTrang.Forms
{
    public partial class FormThemBienThe : Form
    {
        private SanPhamBUS _sanPhamBUS = new SanPhamBUS();
        private string _maSanPhamGoc;

        public FormThemBienThe(string maSP)
        {
            InitializeComponent();
            _maSanPhamGoc = maSP;

            // Hiển thị lên tiêu đề cho người dùng dễ nhìn
            this.Text = "Thêm biến thể cho Sản phẩm: " + _maSanPhamGoc;
        }

        // Code cho nút Hủy
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Code cho nút Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaBT.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã biến thể!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BienTheDTO btMoi = new BienTheDTO
                {
                    MaBienThe = txtMaBT.Text.Trim(),
                    MaSP = _maSanPhamGoc, // Sử dụng mã SP đã được truyền vào
                    MauSac = txtMauSac.Text.Trim(),
                    KichCo = txtKichCo.Text.Trim(),
                    DinhMucToiThieu = (int)numDinhMuc.Value,
                    SoLuongTon = 0 // Biến thể mới sinh ra tồn kho luôn bằng 0
                };

                // Gọi hàm thêm ở tầng BUS (Bạn tự bổ sung hàm này tương tự như lúc làm Nhập kho)
                if (_sanPhamBUS.ThemBienTheMoi(btMoi))
                {
                    MessageBox.Show("✅ Thêm biến thể thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Gắn cờ hiệu là Form này đã hoàn thành xuất sắc nhiệm vụ
                    this.DialogResult = DialogResult.OK;
                    this.Close(); // Đóng pop-up
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}