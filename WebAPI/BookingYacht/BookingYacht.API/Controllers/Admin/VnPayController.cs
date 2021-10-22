using BookingYacht.Business.Enum;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.ViewModels;
using BookingYacht.Business.VNPay;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Admin
{
    [Route(AdminRoute)]
    [ApiController]
    [ApiExplorerSettings(GroupName = Role)]
    public class VnPayController : BaseAdminController
    {
        IConfiguration _configuration;
        private readonly IOrdersService _service;
        private readonly IManageBusinessAccountService _manageBusinessAccountService;

        public VnPayController(IConfiguration configuration, IOrdersService service, IManageBusinessAccountService manageBusinessAccountService)
        {
            _configuration = configuration;
            _service = service;
            _manageBusinessAccountService = manageBusinessAccountService;
        }

        // GET: api/<VnPayController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] BusinessPayment businessPayment)
        {
            
            string url = _configuration["VnPay:Url"];
            string returnUrl =Request.Scheme+"://" +Request.Host+ _configuration["VnPay:ReturnAdminPath"];
            var model = await _manageBusinessAccountService.GetBusiness(businessPayment.IdBusiness);
            string tmnCode = model.VnpTmnCode;
            string hashSecret = model.VnpHashSecret;
            VnPayLibrary pay = new VnPayLibrary();

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount",  businessPayment.Amount.ToString()+"00"); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", businessPayment.Ip); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", model.Id.ToString()); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Success(paymentUrl);
        }

        // GET api/<VnPayController>/5
        [HttpGet("PaymentConfirm")]
        public async Task<IActionResult> Confirm()
        {
            if (Request.Query.Count>0)
            {
                Guid IdBusiness = Guid.Parse(Request.Query["VnpOrderInfo"]);
                var model = await _manageBusinessAccountService.GetBusiness(IdBusiness);
                string vnp_HashSecret = model.VnpHashSecret; //Chuoi bi mat
                var vnpayData = Request.Query;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData.Keys)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("Vnp"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }

                string orderId =  Request.Query["VnpTxnRef"];
                long vnpayTranId = Convert.ToInt64(Request.Query["VnpTransactionNo"]);
                string vnp_ResponseCode = Request.Query["VnpResponseCode"];
                string vnp_TransactionStatus = Request.Query["VnpTransactionStatus"];
                String vnp_SecureHash = Request.Query["VnpSecureHash"];
                String TerminalID = Request.Query["VnpTmnCode"];
                long vnp_Amount = Convert.ToInt64(Request.Query["VnpAmount"]) / 100;
                String bankCode = Request.Query["VnpBankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        //Thanh toan thanh cong
                        return Success("Success");
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        return Fail(vnp_ResponseCode);
                    }
                }
                
            }
            return Fail("Error in processing");

        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] BusinessPayment model)
        {
            foreach(Guid id in model.IdOrders)
            {
                await _service.UpdateStatus(id, Status.COMPLETELY_ORDER);
            }
            return Success();
        }

    }
}
