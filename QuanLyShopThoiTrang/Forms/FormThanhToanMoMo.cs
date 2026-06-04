using System;
using System.Drawing;
using System.Windows.Forms;
using QRCoder; 

namespace QuanLyShopThoiTrang.Forms
{
    public partial class FormThanhToanMoMo : Form
    {
        public FormThanhToanMoMo(string payUrl, decimal tongTien)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            TaoMaQR(payUrl);

            // Tùy chọn: Hiện thêm Label thông báo số tiền để khách dễ nhìn
            lblSoTien.Text = $"Số tiền: {tongTien.ToString("N0")}đ";
        }

        private void TaoMaQR(string url)
        {
            try
            {
                // Dùng QRCoder để chuyển đổi chuỗi URL thành hình ảnh
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                // Trích xuất ra đối tượng Bitmap (hình ảnh) với kích thước pixel là 10
                Bitmap qrCodeImage = qrCode.GetGraphic(4);

                // Đổ hình vào PictureBox
                picQRCode.Image = qrCodeImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tạo mã QR: " + ex.Message);
            }
        }

        // Khi nhân viên bấm nút Xác nhận đã nhận tiền
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        // Khi khách đổi ý không quét nữa
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}