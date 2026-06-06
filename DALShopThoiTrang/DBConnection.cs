using System;
using System.Data;
using System.Data.SqlClient;

namespace DALShopThoiTrang
{
    public class DBConnection
    {
        // Thay connectionString bằng chuỗi kết nối thực tế của bạn
        private string connectionString = @"Data Source=DESKTOP-HD4B2VM;Initial Catalog=QuanLyShopThoiTrang;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;"; //KhanhNhi
        //private string connectionString = @"Server =.\SQLEXPRESS;Database=QuanLyShopThoiTrang;Integrated Security = True;";
        //private string connectionString = @"Data Source=.;Initial Catalog=QuanLyShopThoiTrang3; Integrated Security=True; TrustServerCertificate=True"; //HongNha
        //private string connectionString = @"Data Source=LAPTOP-SAHSTOKR\SQLEXPRESS;Initial Catalog=QuanLyShopThoiTrang;Integrated Security=True"; //Duyên
       // private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QuanLyShopThoiTrang;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";
        protected SqlConnection conn;

        public DBConnection()
        {
            conn = new SqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}