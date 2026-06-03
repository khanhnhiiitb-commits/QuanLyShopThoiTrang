using BUSShopThoiTrang;
using DTOShopThoiTrang;
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
    public partial class ucStaff : UserControl
    {
        TaiKhoanBUS tkBUS = new TaiKhoanBUS();
        public ucStaff()
        {
            InitializeComponent();
        }

        private void ucStaff_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            
        }
        private void LoadNhanVien()
        {
            dgvNhanVien.DataSource = tkBUS.LayDanhSachNhanVien();

            dgvNhanVien.Columns["matKhau"].Visible = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            NhanVienDTO nv = new NhanVienDTO();

            nv.MaNV = txtMaNV.Text;
            nv.TenNV = txtTenNV.Text;
            nv.SoDienThoai = txtSDT.Text;
            nv.ChucVu = cboChucVu.Text;
            nv.GioiTinh = cboGioiTinh.Text;
            nv.TrangThai = "Đang làm việc";
            nv.PhanQuyen = "Staff";
            nv.MatKhau = txtMatKhau.Text;

            if (tkBUS.ThemNhanVien(nv))
            {
                MessageBox.Show("Thêm thành công");
                LoadNhanVien();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            NhanVienDTO nv = new NhanVienDTO();

            nv.MaNV = txtMaNV.Text;
            nv.TenNV = txtTenNV.Text;
            nv.SoDienThoai = txtSDT.Text;
            nv.ChucVu = cboChucVu.Text;
            nv.PhanQuyen = "Staff";
            nv.GioiTinh = cboGioiTinh.Text;
           
            nv.MatKhau = txtMatKhau.Text;

            if (tkBUS.SuaNhanVien(nv))
            {
                MessageBox.Show("Cập nhật thành công");
                LoadNhanVien();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            dgvNhanVien.DataSource = tkBUS.TimNhanVien(txtTimKiem.Text);
        }

        private void btnKhoa_Click(object sender, EventArgs e)
        {
            if (tkBUS.KhoaNhanVien(txtMaNV.Text))
            {
                MessageBox.Show("Đã khóa nhân viên");
                LoadNhanVien();
            }
        }

        private void btnMoKhoa_Click(object sender, EventArgs e)
        {
            if (tkBUS.MoKhoaNhanVien(txtMaNV.Text))
            {
                MessageBox.Show("Đã mở khóa nhân viên");
                LoadNhanVien();
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row =
                dgvNhanVien.Rows[e.RowIndex];

            txtMaNV.Text =
                row.Cells["maNV"].Value.ToString();

            txtTenNV.Text =
                row.Cells["tenNV"].Value.ToString();

            txtSDT.Text =
                row.Cells["soDienThoai"].Value.ToString();

            cboChucVu.Text =
                row.Cells["chucVu"].Value.ToString();

            cboGioiTinh.Text =
                row.Cells["gioiTinh"].Value.ToString();
         
            txtMatKhau.Text =
                row.Cells["matKhau"].Value.ToString();
        }
    }
}
