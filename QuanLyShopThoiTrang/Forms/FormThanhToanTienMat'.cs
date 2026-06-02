using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyShopThoiTrang
{
    public partial class FormThanhToanTienMat : Form
    {
        private decimal _tongTienCanThu;

        public FormThanhToanTienMat(decimal tongTien)
        {
            InitializeComponent();
            _tongTienCanThu = tongTien;

            // Hiển thị tổng tiền lên màn hình
            lblTongTien.Text = _tongTienCanThu.ToString("N0") + "đ";

            // Khóa nút xác nhận lúc đầu, bao giờ khách đưa đủ tiền mới mở
            btnXacNhan.Enabled = false;
        }

        // Sự kiện khi nhân viên gõ tiền vào ô TextBox
        private void txtTienKhachDua_TextChanged(object sender, EventArgs e)
        {
            string input = txtTienKhachDua.Text.Replace(",", "").Replace(".", "");

            if (decimal.TryParse(input, out decimal tienDua))
            {
                decimal tienThoi = tienDua - _tongTienCanThu;

                if (tienThoi >= 0)
                {
                    lblTienThoi.Text = tienThoi.ToString("N0") + "đ";
                    lblTienThoi.ForeColor = Color.Green;
                    btnXacNhan.Enabled = true; // Khách đưa đủ tiền -> Mở nút chốt đơn
                }
                else
                {
                    lblTienThoi.Text = "Khách đưa chưa đủ tiền!";
                    lblTienThoi.ForeColor = Color.Red;
                    btnXacNhan.Enabled = false; // Khóa nút
                }
            }
            else
            {
                lblTienThoi.Text = "0đ";
                btnXacNhan.Enabled = false;
            }
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}