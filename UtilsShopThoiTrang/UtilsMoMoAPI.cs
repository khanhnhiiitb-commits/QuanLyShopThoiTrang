using System;
using System.Text;
using System.Security.Cryptography;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UtilsShopThoiTrang
{
    public class UtilsMoMoAPI
    {
        //Cấu hình thông số kết nối (Dùng tài khoản Test Sandbox của MoMo)
        private static readonly string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
        private static readonly string partnerCode = "MOMO5RGX20191128";
        private static readonly string accessKey = "M8brj9K6E22vXoDB";
        private static readonly string secretKey = "nqQiVSgDMy809JoPF6OzP5OdBUB550Y4";

        //Hàm tạo mã hóa bảo mật HMAC SHA256 theo chuẩn yêu cầu của MoMo
        private static string ComputeHmacSha256(string message, string secretKey)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(secretKey);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                string hex = BitConverter.ToString(hashmessage);
                hex = hex.Replace("-", "").ToLower();
                return hex;
            }
        }

        //Gửi yêu cầu lên MoMo và lấy đường link thanh toán (QR Code)
        public static async Task<string> CreatePaymentRequest(string maHD, decimal tongTien)
        {
            // Các thông số phụ trợ
            string orderInfo = "Thanh toán hóa đơn mua hàng Shop Thời Trang";
            string returnUrl = "https://momo.vn/return"; // Bỏ qua web trung gian 
            string notifyUrl = "https://momo.vn/notify";
            string amountStr = tongTien.ToString("0"); // Ép kiểu số nguyên không có phần thập phân
            string requestId = Guid.NewGuid().ToString(); // Tạo ID ngẫu nhiên cho mỗi giao dịch
            string extraData = "";

            //Tạo chuỗi Raw Hash chính xác theo cấu trúc quy định của MoMo
            string rawHash = $"accessKey={accessKey}&amount={amountStr}&extraData={extraData}&ipnUrl={notifyUrl}&orderId={maHD}" +
                $"&orderInfo={orderInfo}&partnerCode={partnerCode}&redirectUrl={returnUrl}&requestId={requestId}&requestType=captureWallet";

            //Ký chữ ký điện tử (Signature)
            string signature = ComputeHmacSha256(rawHash, secretKey);

            //Gói dữ liệu lại thành đối tượng JSON
            var message = new
            {
                partnerCode = partnerCode,
                partnerName = "Shop Thoi Trang",
                storeId = "MomoTestStore",
                requestId = requestId,
                amount = amountStr,
                orderId = maHD,
                orderInfo = orderInfo,
                redirectUrl = returnUrl,
                ipnUrl = notifyUrl,
                lang = "vi",
                extraData = extraData,
                requestType = "captureWallet",
                signature = signature
            };

            //Gửi đi bằng HTTP Request
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(endpoint, content);
                var responseString = await response.Content.ReadAsStringAsync();

                //Đọc dữ liệu MoMo trả về (Tìm lấy cái payUrl)
                JObject jmessage = JObject.Parse(responseString);

                if (jmessage["payUrl"] != null)
                {
                    return jmessage["payUrl"].ToString();
                }
                else
                {
                    throw new Exception("Lỗi gọi API MoMo: " + responseString);
                }
            }
        }
    }
}