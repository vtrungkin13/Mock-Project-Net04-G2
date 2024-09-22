using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MockNet04G2.Business.Services.User;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using MockNet04G2.Business.Services.Payment;
using MockNet04G2.Business.DTOs.Payments.Request;

namespace MockNet04G2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private readonly CreatePaymentService _createPaymentService;

        public PaymentController(CreatePaymentService createPaymentService)
        {
            _createPaymentService = createPaymentService;
        }

        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment(string redirectUrl, PaymentRequest paymentRequest)
        {
            var result = await _createPaymentService.ExecuteAsync(redirectUrl, paymentRequest);
            return HandleApiResponse(result);
        }

        [HttpPost("ReturnPayment")]
        public async Task<IActionResult> ReturnPayment(string orderId, string requestId, int userId, int campaignId, int amount)
        {
            var result = await _createPaymentService.ReturnPayment(orderId, requestId, userId, campaignId, amount);
            return HandleApiResponse(result);
        }
    }
}
