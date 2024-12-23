using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using websiteTUTHIEN.Models;
using websiteTUTHIEN.Models.VnPay;
using websiteTUTHIEN.Services.VnPay;

namespace websiteTUTHIEN.Controllers
{
    public class QuyenGopController : Controller
    {
        private readonly WebsiteTuthienContext _context;
        private IVnPayService _vnPayService;
        public QuyenGopController(WebsiteTuthienContext context, IVnPayService vnPayService){
            _context = context;
            _vnPayService = vnPayService;
        }


        [HttpPost]
        public IActionResult CreatePaymentUrlVnpay(PaymentInformationModel model){
            var url = _vnPayService.CreatePaymentUrl(model,HttpContext);
            return Redirect(url);
        }

        [HttpGet]
        public IActionResult PaymentCallbackVnpay(){
            var response = _vnPayService.PaymentExecute(Request.Query);
            return Json(response);
        }

    }
}