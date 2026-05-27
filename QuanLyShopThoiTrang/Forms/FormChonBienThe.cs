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
    public partial class FormChonBienThe : Form
    {
        private SanPhamBUS _bus = new SanPhamBUS();

        // Biến này để lưu giữ toàn bộ danh sách biến thể lấy từ SQL lên
        private List<BienTheDTO> _danhSachTatCaBienThe;

        public BienTheDTO BienTheDaChon { get; private set; }
        public int SoLuong { get; private set; }

        public FormChonBienThe(SanPhamDTO sp)
        {
            InitializeComponent();
            lblTenSP.Text = sp.TenSP;

            // 1. Lấy toàn bộ biến thể của cái áo/quần này từ SQL
            _danhSachTatCaBienThe = _bus.LayBienTheTheoMaSP(sp.MaSP);

            // 2. Dùng LINQ lọc lấy danh sách Màu (Distinct giúp loại bỏ các màu bị trùng lặp)
            var danhSachMau = _danhSachTatCaBienThe.Select(b => b.MauSac).Distinct().ToList();
            cboMauSac.DataSource = danhSachMau;
            
        }
        private void cboMauSac_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMauSac.SelectedItem != null)
            {
                string mauDuocChon = cboMauSac.SelectedItem.ToString();

                var danhSachSize = _danhSachTatCaBienThe
                                    .Where(b => b.MauSac == mauDuocChon)
                                    .Select(b => b.KichCo)
                                    .ToList();

                // Đổ danh sách Size tương ứng vào cboKichCo
                cboKichCo.DataSource = danhSachSize;
            }
        }
        // 3.5 BẮT SỰ KIỆN: Khi người dùng chọn/đổi Kích cỡ

        private void cboKichCo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMauSac.SelectedItem != null && cboKichCo.SelectedItem != null)
            {
                string mauChon = cboMauSac.SelectedItem.ToString();
                string sizeChon = cboKichCo.SelectedItem.ToString();

                // Lục tìm biến thể chính xác đang được chọn
                var bienTheHienTai = _danhSachTatCaBienThe.FirstOrDefault(b => b.MauSac == mauChon && b.KichCo == sizeChon);

                if (bienTheHienTai != null)
                {
                    // Thiết lập giới hạn mua tối đa bằng đúng số lượng đang có trong kho
                    if (bienTheHienTai.SoLuongTon > 0)
                    {
                        nmSoLuong.Maximum = bienTheHienTai.SoLuongTon;
                        nmSoLuong.Minimum = 1;
                        nmSoLuong.Value = 1;
                        btnXacNhan.Enabled = true; 

                        lblTonKho.Text = $"Kho còn: {bienTheHienTai.SoLuongTon} cái";
                    }
                    else
                    {
                        // Xử lý khi hết hàng
                        nmSoLuong.Maximum = 0;
                        nmSoLuong.Minimum = 0;
                        nmSoLuong.Value = 0;
                        btnXacNhan.Enabled = false; // Khóa nút Xác nhận

                        // (Tùy chọn)
                        lblTonKho.Text = "Đã hết hàng!";
                    }
                }
            }
        }

        // 4. Khi người dùng chốt đơn bấm Xác nhận
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (cboMauSac.SelectedItem == null || cboKichCo.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Màu sắc và Kích cỡ!", "Thông báo");
                return;
            }

            string mauChon = cboMauSac.SelectedItem.ToString();
            string sizeChon = cboKichCo.SelectedItem.ToString();

            // Dùng LINQ (FirstOrDefault): Lục tìm lại đúng cái đối tượng Biến Thể có cả Màu và Size vừa chọn
            BienTheDaChon = _danhSachTatCaBienThe.FirstOrDefault(b => b.MauSac == mauChon && b.KichCo == sizeChon);

            if (BienTheDaChon == null)
            {
                MessageBox.Show("Mẫu này hiện không tồn tại!", "Thông báo");
                return;
            }

            SoLuong = (int)nmSoLuong.Value;

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
