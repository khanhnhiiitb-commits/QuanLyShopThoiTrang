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

            if (txtMaNV.Text.Trim().ToUpper() == "NV01")
            {
                MessageBox.Show(
                    "Mã nhân viên NV01 đã được sử dụng cho tài khoản Admin!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

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

            if (txtMaNV.Text.Trim().ToUpper() == "NV01")
            {
                MessageBox.Show(
                    "Không được phép chỉnh sửa tài khoản Admin!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

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
            // Kiểm tra dòng hợp lệ (không phải header, không phải dòng trống)
            if (e.RowIndex < 0 || dgvNhanVien.Rows[e.RowIndex].IsNewRow) return;

            DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

            // Sử dụng Convert.ToString để tránh lỗi NullReferenceException
            txtMaNV.Text = Convert.ToString(row.Cells["maNV"].Value);
            txtTenNV.Text = Convert.ToString(row.Cells["tenNV"].Value);
            txtSDT.Text = Convert.ToString(row.Cells["soDienThoai"].Value);
            cboChucVu.Text = Convert.ToString(row.Cells["chucVu"].Value);
            cboGioiTinh.Text = Convert.ToString(row.Cells["gioiTinh"].Value);

            // Gán password (đã ẩn cột matKhau trên lưới nên vẫn lấy được giá trị)
            txtMatKhau.Text = Convert.ToString(row.Cells["matKhau"].Value);
        }
    }
}
