using System;
using System.Threading.Tasks;

namespace UtilsShopThoiTrang
{
    public class UtilsMoMoAPI
    {
        // Hàm giả lập API MoMo
        public static async Task<string> CreatePaymentRequest(string maHD, decimal tongTien)
        {
            // 1. Tạo chuỗi thông tin chuyển khoản giả lập
            // (Bạn có thể đổi SĐT thành số thật của bạn để demo cho vui)
            string thongTinThanhToan = $"MoMo | Tai khoan: 0987654321 | Ma HD: {maHD} | So tien: {tongTien:N0} VNĐ";

            // 2. Chuyển đổi chuỗi thành chuẩn URL (để không bị lỗi khoảng trắng)
            string urlEncodedData = Uri.EscapeDataString(thongTinThanhToan);

            // 3. Sử dụng API mã nguồn mở (QR Server) để biến chuỗi trên thành hình ảnh mã QR
            string fakePayUrl = $"https://api.qrserver.com/v1/create-qr-code/?size=400x400&data={urlEncodedData}";

            // 4. Giả lập độ trễ mạng khoảng 1.5 giây để nhìn có vẻ như đang kết nối đến server MoMo thật
            await Task.Delay(1500);

            // Trả về đường link ảnh QR
            return fakePayUrl;
        }
    }
}