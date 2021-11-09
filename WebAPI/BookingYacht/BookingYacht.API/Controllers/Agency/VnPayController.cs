using BookingYacht.Business.Enum;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Business.VNPay;
using BookingYacht.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookingYacht.API.Controllers.Agency
{
    [Route(AgencyRoute)]
    [ApiController]
    [ApiExplorerSettings(GroupName = Role)]
    public class VnPayController : BaseAgencyController
    {
        IConfiguration _configuration;
        private readonly IOrdersService _service;
        private readonly ITicketService _ticketService;

        public VnPayController(IConfiguration configuration, IOrdersService service, ITicketService ticketService)
        {
            _configuration = configuration;
            _service = service;
            _ticketService = ticketService;
        }

        // GET: api/<VnPayController>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] OrderRequest orderRequest)
        {
            string url = _configuration["VnPay:Url"];
            string returnUrl = Request.Scheme + "://" + Request.Host + _configuration["VnPay:ReturnPath"];
            string tmnCode = _configuration["VnPay:TmnCode"];
            string hashSecret = _configuration["VnPay:HashSecret"];
            OrdersViewModel model = await _service.Get(orderRequest.IdOrder);
            var ticketList = await _ticketService.SearchTicketsNavigation(new TicketSearchModel()
                { IdOrder = orderRequest.IdOrder });
            double totalPrice = 0;
            foreach (Ticket ticket in ticketList)
            {
                totalPrice += ticket.IdTicketTypeNavigation.Price * (100 -
                                                                     ticket.IdTicketTypeNavigation.ServiceFeePercentage
                                                                         .Value - ticket.IdTicketTypeNavigation
                                                                         .CommissionFeePercentage.Value);
            }

            VnPayLibrary pay = new VnPayLibrary();

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode",
                tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount",
                totalPrice.ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_CreateDate",
                model.OrderDate.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", orderRequest.Ip); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo",
                model.AgencyName + " thanh toan hoa don " + model.Id); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType",
                "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl",
                returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", orderRequest.IdOrder.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Success(paymentUrl);
        }

        // GET api/<VnPayController>/5
        [HttpGet("PaymentConfirm")]
        public async Task<IActionResult> Confirm()
        {
            if (Request.Query.Count > 0)
            {
                string vnp_HashSecret = _configuration["VnPay:HashSecret"]; //Chuoi bi mat
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

                Guid orderId = Guid.Parse(Request.Query["VnpTxnRef"]);
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
                        await _service.UpdateStatus(orderId, Status.COMPLETELY_PAYMENT);
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
    }
}