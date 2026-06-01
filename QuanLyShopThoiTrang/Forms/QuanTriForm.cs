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
        

        private void pnlContentQuanTri_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
