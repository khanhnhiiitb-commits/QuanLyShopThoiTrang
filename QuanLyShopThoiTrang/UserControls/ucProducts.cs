using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUSShopThoiTrang;
using DTOQuanLyThoiTrang;
using DTOShopThoiTrang;
using QuanLyShopThoiTrang.Forms;

namespace QuanLyShopThoiTrang.UserControls
{
    public partial class ucProducts : UserControl
    {
        private SanPhamBUS _spBus = new SanPhamBUS();
        private LoaiSPBUS _loaiSPBus = new LoaiSPBUS();
        private string duongDanAnhDaChon = "";
        public ucProducts()
        {
            InitializeComponent();
        }

        private void ucProducts_Load(object sender, EventArgs e)
        {
            LoadTreeViewDanhMuc();

            LoadDanhSachSanPham();
        }
        private void LoadTreeViewDanhMuc()
        {
            treeView1.Nodes.Clear();

            var dsLoaiSP = _loaiSPBus.LayTatCaLoaiSP();
            foreach (var loai in dsLoaiSP)
            {
                TreeNode node = new TreeNode(loai.TenLoai);
                node.Tag = loai; // Giấu object DTO vào Tag
                treeView1.Nodes.Add(node);
            }

            LoadComboBox(); // Đồng bộ dữ liệu sang ComboBox
        }
        private void LoadDanhSachSanPham()
        {
            // Sử dụng var hứng kết quả
            var dsSanPham = _spBus.LayTatCaSanPham();
            dgvDanhsachSP.DataSource = dsSanPham;
            if (dgvDanhsachSP.Columns["HinhAnh"] != null)
            {
                dgvDanhsachSP.Columns["HinhAnh"].Visible = false;
            }
        }
        private void LoadComboBox()
        {
            try
            {
                var dsLoaiSP = _loaiSPBus.LayTatCaLoaiSP();

                if (dsLoaiSP != null && dsLoaiSP.Count > 0)
                {
                    // Nạp vào ComboBox lọc (Ở giữa) - Giả sử tên là cbBoxDanhmuc
                    cbBoxDanhmuc.DataSource = dsLoaiSP.ToList();
                    cbBoxDanhmuc.DisplayMember = "TenLoai";
                    cbBoxDanhmuc.ValueMember = "MaLoai";

                    // Nạp vào ComboBox thông tin chi tiết (Bên phải)
                    // Lưu ý: Đổi txtDanhmuc thành ComboBox trên UI và đặt tên là cbBoxDanhmucInfo
                    // Nếu bạn chưa đổi, thì hãy comment 3 dòng dưới này lại tạm thời
                    /*
                    cbBoxDanhmucInfo.DataSource = dsLoaiSP.ToList();
                    cbBoxDanhmucInfo.DisplayMember = "TenLoai";
                    cbBoxDanhmucInfo.ValueMember = "MaLoai";
                    */
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh mục: " + ex.Message);
            }
        }

        private void dgvDanhsachSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvDanhsachSP.Rows[e.RowIndex];
            var sp = row.DataBoundItem as SanPhamDTO;

            if (sp != null)
            {
                // 1. Gán các thông tin cơ bản
                txtMaSP.Text = sp.MaSP;
                txtTenSP.Text = sp.TenSP;
                txtGia.Text = sp.GiaBan.ToString("N0");
                txtGiaNhap.Text = sp.GiaNhap.ToString("N0"); // Thêm dòng này
                txtMota.Text = sp.MoTa;

                // Ô Danh mục: Tạm thời hiển thị Mã Loại (L05). 
                // (Nếu sau này bạn muốn hiện "Áo Khoác" thay vì "L05", bạn sẽ cần đổi ô này thành ComboBox)
                txtDanhmuc.Text = sp.MaLoai;

                txtMota.Text = sp.MoTa;
                // 2. Load biến thể và Tính tổng TỒN KHO bằng LINQ
                var dsBienThe = _spBus.LayBienTheTheoMaSP(sp.MaSP);
                dgvBienThe.DataSource = dsBienThe;
                if (dgvBienThe.Columns["HinhAnh"] != null)
                {
                    dgvBienThe.Columns["HinhAnh"].Visible = false;
                }

                // Dùng LINQ Sum để cộng tất cả thuộc tính SoLuongTon của danh sách biến thể lại
                var tongTon = dsBienThe.Sum(bt => bt.SoLuongTon);
                txtTonkho.Text = tongTon.ToString();

                // 3. XỬ LÝ LOAD ẢNH SẢN PHẨM
                string duongDanAnh = Path.Combine(Application.StartupPath, "Images", sp.HinhAnh);
                string anhMacDinh = Path.Combine(Application.StartupPath, "Images", "no_image.png");

                if (File.Exists(duongDanAnh))
                {
                    pictureBox1.Image = Image.FromFile(duongDanAnh);
                }
                else if (File.Exists(anhMacDinh)) // Nếu không tìm thấy ảnh SP, lấy ảnh mặc định
                {
                    pictureBox1.Image = Image.FromFile(anhMacDinh);
                }
                else
                {
                    pictureBox1.Image = null;
                }
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadDanhSachSanPham();

            // Clear luôn lưới biến thể bên phải cho sạch
            dgvBienThe.DataSource = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var tuKhoa = txtSearch.Text.Trim();
            var ketQuaTimKiem = _spBus.TimKiemNangCao(tuKhoa, "", null, null);
            dgvDanhsachSP.DataSource = ketQuaTimKiem;
            if (dgvDanhsachSP.Columns["HinhAnh"] != null)
            {
                dgvDanhsachSP.Columns["HinhAnh"].Visible = false;
            }
        }

        private void cbBoxDanhmuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            var maLoaiDuocChon = cbBoxDanhmuc.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(maLoaiDuocChon)) return;

            var tatCaSP = _spBus.LayTatCaSanPham();

            // Ứng dụng LINQ lọc trực tiếp trên RAM
            if (maLoaiDuocChon == "ALL") // Nếu có option "Tất cả"
            {
                dgvDanhsachSP.DataSource = tatCaSP;
            }
            else
            {
                var dsLoc = tatCaSP.Where(sp => sp.MaLoai == maLoaiDuocChon).ToList();
                dgvDanhsachSP.DataSource = dsLoc;
            }
            if (dgvDanhsachSP.Columns["HinhAnh"] != null)
            {
                dgvDanhsachSP.Columns["HinhAnh"].Visible = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var spMoi = new SanPhamDTO
                {
                    MaSP = txtMaSP.Text.Trim(),
                    TenSP = txtTenSP.Text.Trim(),
                    MaLoai = "L01", // Thay bằng: cboDanhMucInfo.SelectedValue.ToString(),
                    GiaBan = string.IsNullOrEmpty(txtGia.Text) ? 0 : Convert.ToDecimal(txtGia.Text),
                    GiaNhap = string.IsNullOrEmpty(txtGiaNhap.Text) ? 0 : Convert.ToDecimal(txtGiaNhap.Text),
                    MoTa = txtMota.Text.Trim()
                };

                var ketQua = _spBus.ThemSanPham(spMoi);

                if (ketQua == "Thành công")
                {
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachSanPham();
                }
                else
                {
                    MessageBox.Show(ketQua, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vui lòng kiểm tra lại định dạng dữ liệu.\nChi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            try
            {
                var spSua = new SanPhamDTO
                {
                    MaSP = txtMaSP.Text.Trim(),
                    TenSP = txtTenSP.Text.Trim(),
                    MaLoai = "L01", // Thay bằng giá trị thực tế của ComboBox
                    GiaBan = Convert.ToDecimal(txtGia.Text),
                    GiaNhap = Convert.ToDecimal(txtGiaNhap.Text),
                    MoTa = txtMota.Text.Trim()
                };

                var ketQua = _spBus.SuaSanPham(spSua);

                if (ketQua == "Thành công")
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachSanPham();
                }
                else
                {
                    MessageBox.Show(ketQua, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi định dạng nhập liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            var maSP = txtMaSP.Text.Trim();

            if (string.IsNullOrEmpty(maSP))
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xóa!", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var luaChon = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm [{maSP}] không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (luaChon == DialogResult.Yes)
            {
                var ketQua = _spBus.XoaSanPham(maSP);

                if (ketQua == "Thành công")
                {
                    MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear giao diện sau khi xóa
                    txtMaSP.Clear();
                    txtTenSP.Clear();
                    txtGia.Clear();
                    dgvBienThe.DataSource = null;

                    LoadDanhSachSanPham();
                }
                else
                {
                    MessageBox.Show(ketQua, "Lỗi khi xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }       

        private void btnThem_Click(object sender, EventArgs e)
        {
            var loaiMoi = new LoaiSPDTO(txtMaDM.Text.Trim(), txtTenDM.Text.Trim());
            var ketQua = _loaiSPBus.ThemLoaiSP(loaiMoi);

            if (ketQua == "Thành công")
            {
                LoadTreeViewDanhMuc();
                txtMaDM.Clear();
                txtTenDM.Clear();
                txtMaDM.Enabled = true; // Mở khóa lại ô Mã để nhập cái mới
            }
            else
            {
                MessageBox.Show(ketQua, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (txtMaDM.Enabled == false)
            {
                txtMaDM.Clear();
                txtTenDM.Clear();
                txtMaDM.Enabled = true;
                txtMaDM.Focus(); // Đưa con trỏ chuột vào ô nhập
                return; // Dừng hàm lại, chờ người dùng nhập
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var maXoa = txtMaDM.Text.Trim();
            if (string.IsNullOrEmpty(maXoa)) return;

            var xacNhan = MessageBox.Show($"Bạn có chắc muốn xóa danh mục [{maXoa}]?", "Xác nhận", MessageBoxButtons.YesNo);
            if (xacNhan == DialogResult.Yes)
            {
                var ketQua = _loaiSPBus.XoaLoaiSP(maXoa);
                if (ketQua == "Thành công")
                {
                    LoadTreeViewDanhMuc();
                    txtMaDM.Clear();
                    txtTenDM.Clear();
                    txtMaDM.Enabled = true;
                }
                else
                {
                    MessageBox.Show(ketQua, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var loaiCapNhat = new LoaiSPDTO(txtMaDM.Text.Trim(), txtTenDM.Text.Trim());
            var ketQua = _loaiSPBus.SuaLoaiSP(loaiCapNhat);

            if (ketQua == "Thành công")
            {
                LoadTreeViewDanhMuc();
                MessageBox.Show("Cập nhật thành công!");
            }
            else
            {
                MessageBox.Show(ketQua, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var loaiDangChon = e.Node.Tag as LoaiSPDTO;
            if (loaiDangChon != null)
            {
                txtMaDM.Text = loaiDangChon.MaLoai;
                txtTenDM.Text = loaiDangChon.TenLoai;
                txtMaDM.Enabled = false; // Mã thì không được sửa, khóa ô này lại
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                ofd.Title = "Chọn ảnh cho sản phẩm";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        duongDanAnhDaChon = ofd.FileName;
                        pictureBox1.Image = Image.FromFile(duongDanAnhDaChon);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        public void LoadDanhSachBienThe(string maSP)
        {
            var dsBienThe = _spBus.LayBienTheTheoMaSP(maSP);
            dgvBienThe.DataSource = dsBienThe;
            if (dgvBienThe.Columns["HinhAnh"] != null)
            {
                dgvBienThe.Columns["HinhAnh"].Visible = false;
            }
        }
        private void btnThemBTSP_Click(object sender, EventArgs e)
        {
            if (dgvDanhsachSP.CurrentRow == null || dgvDanhsachSP.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn một Sản phẩm trên lưới trước khi thêm biến thể!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string maSanPhamDangChon = dgvDanhsachSP.CurrentRow.Cells["MaSP"].Value?.ToString();         
            FormThemBienThe frmPopUp = new FormThemBienThe(maSanPhamDangChon);

            // 4. Hiển thị form bằng ShowDialog() để nó khóa màn hình chính lại
            // Nếu form Pop-up trả về cờ hiệu OK (tức là đã lưu thành công)
            if (frmPopUp.ShowDialog() == DialogResult.OK)
            {
                // Thực hiện Load lại danh sách biến thể của sản phẩm đó để hiển thị dòng dữ liệu mới thêm
                LoadDanhSachBienThe(maSanPhamDangChon);
            }
        }

        private void btnXoaBienThe_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn dòng nào trên lưới chưa
            if (dgvBienThe.CurrentRow == null || dgvBienThe.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn một biến thể cần xóa!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maBienTheDangChon = dgvBienThe.CurrentRow.Cells["MaBienThe"].Value?.ToString();
            // 3. Hiển thị hộp thoại cảnh báo (Xác nhận kép)
            DialogResult canhBao = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa vĩnh viễn biến thể [{maBienTheDangChon}] không?\nHành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2); // Đưa trỏ chuột mặc định vào nút No để tránh click nhầm

            // 4. Nếu người dùng chọn Yes thì mới tiến hành xóa
            if (canhBao == DialogResult.Yes)
            {
                try
                {
                    // Gọi hàm xóa từ tầng BUS
                    if (_spBus.XoaBienThe(maBienTheDangChon))
                    {
                        MessageBox.Show("✅ Đã xóa biến thể thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 5. Tải lại DataGridView để dòng vừa xóa biến mất khỏi màn hình
                        string maSPGoc = txtMaSP.Text; // Lấy mã Sản phẩm gốc đang hiển thị
                        LoadDanhSachBienThe(maSPGoc);
                    }
                }
                catch (Exception ex)
                {
                    // Nếu dính lỗi Khóa ngoại từ DAL ném lên, nó sẽ hiện thông báo ở đây
                    MessageBox.Show(ex.Message, "Lỗi xóa dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
