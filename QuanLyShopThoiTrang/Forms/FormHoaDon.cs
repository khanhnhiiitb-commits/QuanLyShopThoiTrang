using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using BUSShopThoiTrang;
using DALShopThoiTrang;
using DTOQuanLyThoiTrang;
using DTOShopThoiTrang;
using Microsoft.Reporting.WinForms;
using BUSShopThoiTrang;

namespace QuanLyShopThoiTrang.Forms
{
    public partial class FormHoaDon : Form
    {
        private HoaDonDTO hoaDon;
        private List<ChiTietHDDTO> danhSachChiTiet;
        private KhachHangDTO khachHang;
        private GiaoDichBUS giaoDichBUS = new GiaoDichBUS();
        public FormHoaDon(HoaDonDTO hd, List<ChiTietHDDTO> ct, KhachHangDTO kh)
        {
            InitializeComponent();
            this.hoaDon = hd;
            this.danhSachChiTiet = ct;
            this.khachHang = kh;

            // Cấu hình Form hiển thị kiểu Pop-up
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hóa Đơn Bán Lẻ";
        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            DataTable dt = giaoDichBUS.LayDuLieuInHoaDon(hoaDon.MaHD);
            reportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSetHoaDon", dt); // Tên trùng với file .rdlc
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();
        }

        // Hàm hỗ trợ chuyển đổi List sang DataTable
        private DataTable ChuyenDoiSangDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            var props = typeof(T).GetProperties();
            foreach (var prop in props) dataTable.Columns.Add(prop.Name);
            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++) values[i] = props[i].GetValue(item, null);
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            // Đây là nơi bạn đặt logic in hóa đơn, ví dụ:
            this.reportViewer1.PrintDialog();
        }




    }
}