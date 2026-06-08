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
using GUIShopThoiTrang;
using QuanLyShopThoiTrang.Forms;


namespace QuanLyShopThoiTrang
{
    public partial class BanHangForm : Form
    {
        private SanPhamBUS sanPhamBUS = new SanPhamBUS();
        private GiaoDichBUS giaoDichBUS = new GiaoDichBUS();
        private NhanVienDTO nhanVienHienTai;
        private string _maLoaiHienTai = "All";
        private string _phuongThucThanhToan = "";
        private KhachHangDTO khachHangHienTai = null;
        private Button btnDangChon = null;
        decimal discount = 0;

        public BanHangForm(NhanVienDTO nhanVienHienTai)
        {
            InitializeComponent();
            this.nhanVienHienTai = nhanVienHienTai;
        }
        private void ActivateCategoryButton(object btnSender)
        {
            if (btnSender != null)
            {
                Button nutDuocClick = (Button)btnSender;
                if (nutDuocClick == btnDangChon) return;
                if (btnDangChon != null)
                {
                    btnDangChon.Paint -= VeVienDuoiNut; 
                    btnDangChon.ForeColor = Color.Black; 
                    btnDangChon.Font = new Font(btnDangChon.Font, FontStyle.Regular); 
                    btnDangChon.Invalidate(); 
                }
                btnDangChon = nutDuocClick;
                btnDangChon.Paint += VeVienDuoiNut; 
                btnDangChon.ForeColor = Color.DarkRed; 
                btnDangChon.Font = new Font(btnDangChon.Font, FontStyle.Bold); 
                btnDangChon.Invalidate(); 
            }
        }

        private void VeVienDuoiNut(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            int doDayVien = 3; 
            using (SolidBrush coVe = new SolidBrush(Color.DarkRed))
            {
                e.Graphics.FillRectangle(coVe, 0, btn.Height - doDayVien, btn.Width, doDayVien);
            }
        }
        private void btnCash_Click(object sender, EventArgs e)
        {
            _phuongThucThanhToan = "Tiền mặt";
            btnCash.BackColor = Color.LightGreen; 
            btnMomo.BackColor = Color.WhiteSmoke;
        }
        private void btnMomoClick(object sender, EventArgs e)
        {
            _phuongThucThanhToan = "Chuyển khoản";
            btnMomo.BackColor = Color.LightGreen;
            btnCash.BackColor = Color.WhiteSmoke;
        }
        private void BanHangForm_Load(object sender, EventArgs e)
        {
            if (nhanVienHienTai != null)
            {
                lblTenNhanVien.Text = "Xin chào, " + nhanVienHienTai.TenNV;
            }
            else
            {
                lblTenNhanVien.Text = "Chưa đăng nhập";
            }
            if (cboGia.Items.Count > 0)
                cboGia.SelectedIndex = 0; 

            ThucHienLocSanPham();

        }
        private void LoadSanPhamDong(List<SanPhamDTO> listSanPham)
        {
            flpDanhSachSP.Controls.Clear();
            flpDanhSachSP.SuspendLayout();
            foreach (var sp in listSanPham)
            {
                ProductCardControl card = new ProductCardControl();
                card.SetData(sp);
                card.OnProductSelected += Card_OnProductSelected;
                flpDanhSachSP.Controls.Add(card);
            }
            flpDanhSachSP.ResumeLayout();
        }

        private void Card_OnProductSelected(SanPhamDTO spDuocChon)
        {
            FormChonBienThe frmPopup = new FormChonBienThe(spDuocChon);
            if (frmPopup.ShowDialog() == DialogResult.OK)
            {

                BienTheDTO bienTheDaChon = frmPopup.BienTheDaChon;
                int soLuong = frmPopup.SoLuong;
                ThemVaoGioHang(spDuocChon, bienTheDaChon, soLuong);
            }
        }
        private void ThemVaoGioHang(SanPhamDTO sp, BienTheDTO bt, int soLuong)
        {
            foreach (ProductCartControl item in flpCurrentOrder.Controls)
            {
                if (item.BienTheHienTai.MaBienThe == bt.MaBienThe)
                {
                    item.TangSoLuong(soLuong);
                    TinhTongTienHoaDon();      
                    return;                   
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
            TinhTongTienHoaDon();

        }
        private void TinhTongTienHoaDon()
        {
            decimal subTotal = 0;
            foreach (ProductCartControl item in flpCurrentOrder.Controls)
            {
                subTotal += item.ThanhTien;
            }
            lblSubtotal.Text = subTotal.ToString("N0") + "đ";
            // Thuế VAT là 10% (0.1)
            decimal tax = subTotal * 0.1m;
            lblTax.Text = tax.ToString("N0") + "đ";
            decimal total = subTotal + tax - discount*subTotal;
            lblTotal.Text = total.ToString("N0") + "đ";
        }

        private void lblReturnsExchanges_Click(object sender, EventArgs e)
        {
            if (nhanVienHienTai != null)
            {
                FormDoiTra frmTraHang = new FormDoiTra(nhanVienHienTai.MaNV);
                frmTraHang.ShowDialog();
            }
            else
            {
                MessageBox.Show("Lỗi phiên đăng nhập. Không xác định được nhân viên thao tác!");
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            ActivateCategoryButton(sender);
            _maLoaiHienTai = "All"; 
            ThucHienLocSanPham();
        }

        private void btnShirt_Click(object sender, EventArgs e)
        {
            ActivateCategoryButton(sender);
            _maLoaiHienTai = "L01";
            ThucHienLocSanPham();
        }

        private void btnTShirt_Click(object sender, EventArgs e)
        {
            ActivateCategoryButton(sender);
            _maLoaiHienTai = "L03";
            ThucHienLocSanPham();
        }

        private void btnPants_Click(object sender, EventArgs e)
        {
            ActivateCategoryButton(sender);
            _maLoaiHienTai = "L02";
            ThucHienLocSanPham();
        }

        private void btnJeans_Click(object sender, EventArgs e)
        {
            ActivateCategoryButton(sender);
            _maLoaiHienTai = "L04";
            ThucHienLocSanPham();
        }

        private void btnJacket_Click(object sender, EventArgs e)
        {
            ActivateCategoryButton(sender);
            _maLoaiHienTai = "L05";
            ThucHienLocSanPham();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            ThucHienLocSanPham();
        }
        private void ThucHienLocSanPham()
        {
            // 1. Lấy từ khóa từ TextBox
            string tuKhoa = txtTimKiem.Text;

            string maLoai = _maLoaiHienTai;

            decimal? giaTu = null;
            decimal? giaDen = null;

            switch (cboGia.SelectedIndex)
            {
                case 1: 
                    giaDen = 200000;
                    break;
                case 2: 
                    giaTu = 200000;
                    giaDen = 500000;
                    break;
                case 3: 
                    giaTu = 500000;
                    break;
                default:
                    giaTu = null;
                    giaDen = null;
                    break;
            }

            var dsKetQua = sanPhamBUS.TimKiemNangCao(tuKhoa, maLoai, giaTu, giaDen);
            LoadSanPhamDong(dsKetQua);
        }

        private void cboGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            ThucHienLocSanPham();
        }

        private async void btnFinalizeTransaction_Click(object sender, EventArgs e)
        {
            if (flpCurrentOrder.Controls.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống, không thể thanh toán!", "Cảnh báo");
                return;
            }

            if (string.IsNullOrEmpty(_phuongThucThanhToan))
            {
                MessageBox.Show("Vui lòng chọn phương thức thanh toán (Card hoặc Cash)!", "Thông báo");
                return;
            }

           
            decimal tongTienThucTe = 0;
            foreach (ProductCartControl item in flpCurrentOrder.Controls)
            {
                tongTienThucTe += item.ThanhTien;
            }
            decimal tongTienHoaDon = tongTienThucTe + (tongTienThucTe * 0.1m);
            string maHoaDonMoi = "HD" + DateTime.Now.ToString("ddHHmmss");
          
            if (_phuongThucThanhToan == "Tiền mặt")
            {
                FormThanhToanTienMat frmCash = new FormThanhToanTienMat(tongTienHoaDon);
                if (frmCash.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            else if (_phuongThucThanhToan == "Chuyển khoản")
            {
                try
                {
                    btnFinalizeTransaction.Enabled = false;

                    string payUrl = await UtilsShopThoiTrang.UtilsMoMoAPI.CreatePaymentRequest(maHoaDonMoi, tongTienHoaDon);

                    FormThanhToanMoMo frmQR = new FormThanhToanMoMo(payUrl, tongTienHoaDon);
                    DialogResult rs = frmQR.ShowDialog();

                    if (rs != DialogResult.Yes)
                    {
                        btnFinalizeTransaction.Enabled = true;
                        return; 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối đến hệ thống MoMo:\n" + ex.Message, "Lỗi API");
                    btnFinalizeTransaction.Enabled = true;
                    return;
                }
                finally
                {
                    btnFinalizeTransaction.Enabled = true;
                }
            }
       
            HoaDonDTO hd = new HoaDonDTO();
            hd.MaHD = "HD" + DateTime.Now.ToString("ddHHmmss");
            hd.MaNV = nhanVienHienTai.MaNV;
            hd.MaKH = "KH01"; 
            hd.NgayLap = DateTime.Now;
            hd.PhuongThucThanhToan = _phuongThucThanhToan;
            hd.TongTien = tongTienHoaDon;
            List<ChiTietHDDTO> danhSachChiTiet = new List<ChiTietHDDTO>();
            foreach (ProductCartControl item in flpCurrentOrder.Controls)
            {
                ChiTietHDDTO ct = new ChiTietHDDTO();
                ct.MaHD = hd.MaHD;
                ct.MaBienThe = item.BienTheHienTai.MaBienThe;
                ct.SoLuongBan = item.SoLuongMua;
                ct.DonGiaBan = item.SanPhamHienTai.GiaBan;
                danhSachChiTiet.Add(ct);
            }
            try
            {
                bool ketQua = giaoDichBUS.ThanhToanGiaoDich(hd, danhSachChiTiet);
                if (ketQua)
                {
                    // THAY THẾ MESSAGEBOX BẰNG FORM HÓA ĐƠN
                    FormHoaDon frmInHoaDon = new FormHoaDon(hd, danhSachChiTiet, khachHangHienTai);
                    frmInHoaDon.ShowDialog(); // Sẽ mở Pop-up hóa đơn lên. Đóng hóa đơn mới reset giỏ hàng.

                    // Reset lại giao diện sau khi thanh toán và xem bill xong
                    flpCurrentOrder.Controls.Clear();
                    _phuongThucThanhToan = "";
                    btnCash.BackColor = Color.WhiteSmoke;
                    btnMomo.BackColor = Color.WhiteSmoke;

                    khachHangHienTai = null; // Reset khách hàng
                    lblCustomer.Text = "Customer name";
                    label.Text = ""; // Reset % giảm giá

                    TinhTongTienHoaDon();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Hệ Thống");
            }
        }
        private string ShowInputDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 160,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false
            };
            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 350 };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 340 };
            Button confirmation = new Button() { Text = "Xác nhận", Left = 260, Width = 100, Top = 85, DialogResult = DialogResult.OK };

            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation; // Nhấn Enter là tự ấn Xác nhận

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text.Trim() : "";
        }
        private void lblCustomer_Click(object sender, EventArgs e)
        {
            string sdt = ShowInputDialog("Nhập số điện thoại khách hàng:", "Tra cứu thành viên");

            if (string.IsNullOrEmpty(sdt)) return;
            KhachHangDTO kh = giaoDichBUS.TimKhachHangTheoSDT(sdt);

            if (kh != null)
            {
                khachHangHienTai = kh;
                lblCustomer.Text = kh.TenKH;
                lblMembershipDiscount.Text = "10%"; 

                MessageBox.Show($"Chào mừng khách hàng {kh.TenKH} trở lại!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult result = MessageBox.Show("Số điện thoại này chưa đăng ký. Bạn có muốn thêm thành viên mới không?",
                                                      "Khách hàng mới", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Hỏi tên khách hàng
                    string tenKH = ShowInputDialog("Nhập TÊN khách hàng mới:", "Thêm thành viên");
                    if (!string.IsNullOrEmpty(tenKH))
                    {
                        KhachHangDTO khMoi = new KhachHangDTO();
                        khMoi.MaKH = giaoDichBUS.TaoMaKhachHangMoi(); 
                        khMoi.TenKH = tenKH;
                        khMoi.SoDienThoai = sdt;

                        if (giaoDichBUS.ThemKhachHang(khMoi))
                        {
                            khachHangHienTai = khMoi;
                            lblCustomer.Text = khMoi.TenKH;
                            lblMembershipDiscount.Text = "10%";
                            discount = 0.1m; 
                            MessageBox.Show($"Đăng ký thành viên thành công! Mã KH: {khMoi.MaKH}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TinhTongTienHoaDon();

                        }
                    }
                }
            }
        }

        private void lblLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {           
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất khỏi ca làm việc không?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                Application.Restart();
                }
            
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
