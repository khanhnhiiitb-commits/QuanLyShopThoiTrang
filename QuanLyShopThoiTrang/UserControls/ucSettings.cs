using BUSShopThoiTrang;
using DTOShopThoiTrang;
using GUIShopThoiTrang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyShopThoiTrang.UserControls
{
    public partial class ucSettings : UserControl
    {
        TaiKhoanBUS tkBUS = new TaiKhoanBUS();
        public ucSettings()
        {
            InitializeComponent();
        }

        private void ucSettings_Load(object sender, EventArgs e)
        {
            NhanVienDTO nv =
       TaiKhoanBUS.TaiKhoanHienTai;

            txtMaQTV.Text = nv.MaNV;
            txtTenQTV.Text = nv.TenNV;
            txtSDT.Text = nv.SoDienThoai;
            cboGioiTinh.Text = nv.GioiTinh;

            txtMaQTV.Enabled = false;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            NhanVienDTO nv = new NhanVienDTO();

            nv.MaNV = txtMaQTV.Text;
            nv.TenNV = txtTenQTV.Text;
            nv.SoDienThoai = txtSDT.Text;
            nv.GioiTinh = cboGioiTinh.Text;
            nv.MatKhau = txtMK.Text;

            if (tkBUS.CapNhatTaiKhoan(nv))
            {
                MessageBox.Show("Cập nhật thành công");

                TaiKhoanBUS.TaiKhoanHienTai = nv;
            }
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult rs =
        MessageBox.Show(
            "Bạn có muốn đăng xuất?",
            "Thông báo",
            MessageBoxButtons.YesNo);

            if (rs == DialogResult.Yes)
            {
                DangNhapForm login = new DangNhapForm();

                login.Show();

                this.FindForm().Hide();
            }
        }
    }
}
