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

namespace QuanLyShopThoiTrang.Forms
{
    public partial class FormDoiTra : Form
    {
        private GiaoDichBUS giaoDichBUS = new GiaoDichBUS();
        private DateTime ngayLapHoaDonGoc; // Lưu lại ngày mua để BUS kiểm tra chính sách 7 ngày
        private string maNhanVienHienTai;
        public FormDoiTra(string maNV)
        {
            InitializeComponent();
            maNhanVienHienTai = maNV;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string maHD = txtMaHD.Text.Trim();
            if (string.IsNullOrEmpty(maHD))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn!");
                return;
            }

            // Lấy thông tin hóa đơn gốc
            DataTable dtChiTiet = giaoDichBUS.LayDanhSachChiTietDeTraHang(maHD); if (dtChiTiet.Rows.Count > 0)
            {
                // Hiển thị thông tin lên Label
                ngayLapHoaDonGoc = Convert.ToDateTime(dtChiTiet.Rows[0]["ngayLap"]);
                lblNgayMua.Text = "Ngày mua: " + ngayLapHoaDonGoc.ToString("dd/MM/yyyy HH:mm");
                lblTongTien.Text = "Tổng tiền HĐ: " + Convert.ToDecimal(dtChiTiet.Rows[0]["tongTien"]).ToString("N0") + "đ";

                // Đổ chi tiết vào GridView
                dgvChiTietHD.DataSource = giaoDichBUS.LayDanhSachChiTietDeTraHang(maHD);
            }
            else
            {
                MessageBox.Show("Không tìm thấy hóa đơn này!");
                dgvChiTietHD.DataSource = null;
            }
        }

        private void btnXacNhanTra_Click(object sender, EventArgs e)
        {
            if (dgvChiTietHD.Rows.Count == 0) return;

            // BƯỚC A: Tạo dữ liệu Phiếu Đổi Trả DTO
            PhieuDoiTraDTO pdt = new PhieuDoiTraDTO
            {
                MaPDT = "PDT" + DateTime.Now.ToString("HHmmss"),// Tạo mã ngẫu nhiên theo thời gian
                MaHD = txtMaHD.Text.Trim(),
                MaNV = maNhanVienHienTai,
                NgayLap = DateTime.Now,
                GhiChu = txtLyDo.Text.Trim(),
                TongTienHoan = 0 // Sẽ cộng dồn ở bước sau
            };

            List<ChiTietPDTDTO> listChiTietTra = new List<ChiTietPDTDTO>();

            // BƯỚC B: Quét qua DataGridView để tìm các món được nhân viên tích CheckBox
            foreach (DataGridViewRow row in dgvChiTietHD.Rows)
            {
                // Kiểm tra xem cột CheckBox (tên là colChonTra) có được tích không
                bool isChecked = Convert.ToBoolean(row.Cells["colChonTra"].Value);

                if (isChecked)
                {
                    // Lấy mã biến thể và đơn giá từ dòng hiện tại
                    string maBienThe = row.Cells["maBienThe"].Value.ToString();
                    int soLuongTra = Convert.ToInt32(row.Cells["soLuongBan"].Value); // Tạm thời trả hết số lượng của dòng đó
                    decimal donGia = Convert.ToDecimal(row.Cells["donGiaBan"].Value);

                    // Tính tiền hoàn lại cho khách
                    pdt.TongTienHoan += (soLuongTra * donGia);

                    // Thêm vào danh sách trả
                    listChiTietTra.Add(new ChiTietPDTDTO
                    {
                        MaPDT = pdt.MaPDT,
                        MaBienThe = maBienThe,
                        SoLuong = soLuongTra,
                        HinhThuc = "Trả hàng" // Ghi đúng chữ "Trả hàng" để tầng DAL biết đường cộng lại kho
                    });
                }
            }

            // Kiểm tra xem có chọn món nào chưa
            if (listChiTietTra.Count == 0)
            {
                MessageBox.Show("Vui lòng tích chọn ít nhất 1 sản phẩm để trả!");
                return;
            }

            // BƯỚC C: Gọi BUS để chốt vào CSDL
            try
            {
                // Hàm này sẽ tự động ném lỗi nếu quá 7 ngày
                bool kq = giaoDichBUS.LapPhieuDoiTra(pdt, listChiTietTra, ngayLapHoaDonGoc);
                if (kq)
                {
                    MessageBox.Show($"Trả hàng thành công! Vui lòng hoàn lại cho khách {pdt.TongTienHoan.ToString("N0")}đ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); // Đóng form
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
