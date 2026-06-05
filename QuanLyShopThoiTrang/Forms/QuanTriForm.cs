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
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                Button currentButton = (Button)btnSender;
                pnlNav.Height = currentButton.Height;
                pnlNav.Top = currentButton.Top;
                pnlNav.Left = currentButton.Left;
                
                pnlNav.BringToFront();
            }
        }
        private void btnInventory_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlContentQuanTri.Controls.Clear();
            UserControls.ucInventory uc = new UserControls.ucInventory();
            uc.Dock = DockStyle.Fill;
            pnlContentQuanTri.Controls.Add(uc);
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlContentQuanTri.Controls.Clear();
            UserControls.ucProducts uc = new UserControls.ucProducts();
            uc.Dock = DockStyle.Fill;
            pnlContentQuanTri.Controls.Add(uc);
        }
        private void btnReports_Click(object sender, EventArgs e)
        {
            
        }

        private void btnReports_Click_1(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlContentQuanTri.Controls.Clear();
            ucReports uc = new ucReports();
            uc.Dock = DockStyle.Fill;
            pnlContentQuanTri.Controls.Add(uc);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlContentQuanTri.Controls.Clear();
            UserControls.ucStaff uc = new UserControls.ucStaff();
            uc.Dock = DockStyle.Fill;
            pnlContentQuanTri.Controls.Add(uc);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlContentQuanTri.Controls.Clear();
            UserControls.ucSettings uc = new UserControls.ucSettings();
            uc.Dock = DockStyle.Fill;
            pnlContentQuanTri.Controls.Add(uc);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender);
            pnlContentQuanTri.Controls.Clear();
            ucDashboard uc = new ucDashboard();
            uc.Dock = DockStyle.Fill;
            pnlContentQuanTri.Controls.Add(uc);
        }
    }
    }

