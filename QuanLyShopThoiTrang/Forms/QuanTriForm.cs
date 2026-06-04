using QuanLyShopThoiTrang.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyShopThoiTrang
{
    public partial class QuanTriForm : Form
    {
        public QuanTriForm()
        {
            InitializeComponent();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {

            // 1. Dọn dẹp sạch sẽ bức tường (xóa các giao diện cũ đang hiển thị nếu có)
            pnlContentQuanTri.Controls.Clear();

            // 2. Lấy bức tranh ucInventory trong kho ra
            UserControls.ucInventory uc = new UserControls.ucInventory();

            // 3. Kéo giãn bức tranh cho vừa khít bức tường
            uc.Dock = DockStyle.Fill;

            // 4. Treo lên tường!
            pnlContentQuanTri.Controls.Add(uc);
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            // 1. Dọn dẹp sạch sẽ bức tường (xóa các giao diện cũ đang hiển thị nếu có)
            pnlContentQuanTri.Controls.Clear();

            // 2. Lấy bức tranh ucInventory trong kho ra
            UserControls.ucProducts uc = new UserControls.ucProducts();

            // 3. Kéo giãn bức tranh cho vừa khít bức tường
            uc.Dock = DockStyle.Fill;

            // 4. Treo lên tường!
            pnlContentQuanTri.Controls.Add(uc);
        }
        private void btnReports_Click(object sender, EventArgs e)
        {
            
        }

        private void btnReports_Click_1(object sender, EventArgs e)
        {
// 1. Xóa cái màn hình cũ đang hiển thị đi
            pnlContentQuanTri.Controls.Clear();

            // 2. Khởi tạo màn hình Báo Cáo (ucReports) bạn vừa làm
            ucReports uc = new ucReports();

            // 3. Cho nó phình to ra lấp đầy cái khoảng trống của Panel
            uc.Dock = DockStyle.Fill;

            // 4. Nhét nó vào cái Panel ở giữa màn hình
            pnlContentQuanTri.Controls.Add(uc);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            pnlContentQuanTri.Controls.Clear();

            UserControls.ucStaff uc = new UserControls.ucStaff();

            uc.Dock = DockStyle.Fill;

            pnlContentQuanTri.Controls.Add(uc);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlContentQuanTri.Controls.Clear();

            UserControls.ucSettings uc = new UserControls.ucSettings();

            uc.Dock = DockStyle.Fill;

            pnlContentQuanTri.Controls.Add(uc);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            // 1. Xóa cái màn hình cũ đang hiển thị đi
            pnlContentQuanTri.Controls.Clear();

            // 2. Khởi tạo màn hình Báo Cáo (ucReports) bạn vừa làm
            ucDashboard uc = new ucDashboard();

            // 3. Cho nó phình to ra lấp đầy cái khoảng trống của Panel
            uc.Dock = DockStyle.Fill;

            // 4. Nhét nó vào cái Panel ở giữa màn hình
            pnlContentQuanTri.Controls.Add(uc);
        }
    }
    }

