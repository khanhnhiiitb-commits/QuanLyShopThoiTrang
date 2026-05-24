using BUSShopThoiTrang;
using QuanLyShopThoiTrang;
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
using DTOShopThoiTrang;

namespace GUIShopThoiTrang
{
    public partial class DangNhapForm : Form
    {
        public DangNhapForm()
        {
            InitializeComponent();
        }
        TaiKhoanBUS tkBUS = new TaiKhoanBUS();

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            NhanVienDTO nv = tkBUS.DangNhap(username, password);

            if (nv != null)
            {
                MessageBox.Show("Đăng nhập thành công!");

                // ADMIN
                if (nv.PhanQuyen == "Admin")
                {
                    QuanTriForm f = new QuanTriForm();

                    this.Hide();

                    f.ShowDialog();

                    this.Show();
                }

                // STAFF
                else if (nv.PhanQuyen == "Staff")
                {
                    BanHangForm f = new BanHangForm();

                    this.Hide();

                    f.ShowDialog();

                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }
    }
}
