using MockNet04G2.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using MockNet04G2.Core.Common.Enums;
using MockNet04G2.Business.DTOs.Payments.Request;
using MockNet04G2.Core.Repositories.Interfaces;
using MockNet04G2.Core.Models;
using MockNet04G2.Core.UnitOfWork;

namespace MockNet04G2.Business.Services.Payment
{
    public class CreatePaymentService
    {
        private readonly string accessKey = "bAbusuVpcdguvDX7";
        private readonly string partnerCode = "MOMO5ZIJ20230613";
        private readonly string secrectKey = "w6HsnH2Yu0UTDSfNo1ZxVZkPTESottzM";
        private readonly IDonateRepository _donateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePaymentService(IDonateRepository donateRepository, IUnitOfWork unitOfWork)
        {
            _donateRepository = donateRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<string, string>> ExecuteAsync(string redirectUrl, PaymentRequest paymentRequest)
        {
            using (var _httpClient = new HttpClient())
            {
                var response = new ApiResponse<string, string>();
                var url = "https://test-payment.momo.vn/v2/gateway/api/create";
                var orderId = Guid.NewGuid().ToString();
                var requestId = Guid.NewGuid().ToString();
                var notifyUrl = $"https://311a-42-112-110-134.ngrok-free.app/api/Payment/ReturnPayment" +
                    $"?orderId={orderId}&requestId={requestId}" +
                    $"&userId={paymentRequest.UserId}" +
                    $"&campaignId={paymentRequest.CampaignId}" +
                    $"&amount={paymentRequest.Amount}";

                var rawSignature = $"accessKey={accessKey}" +
                    $"&amount={paymentRequest.Amount}" +
                    $"&extraData=" +
                    $"&ipnUrl={notifyUrl}" +
                    $"&orderId={orderId}" +
                    $"&orderInfo=Quyên góp từ thiện" +
                    $"&partnerCode={partnerCode}" +
                    $"&redirectUrl={redirectUrl}" +
                    $"&requestId={requestId}" +
                    $"&requestType=captureWallet";
                var signature = HmacSha256Encrypt(secrectKey, rawSignature);

                var requestBody = new
                {
                    partnerCode = partnerCode,
                    requestId = requestId,
                    amount = paymentRequest.Amount,
                    orderId = orderId,
                    orderInfo = "Quyên góp từ thiện",
                    redirectUrl = redirectUrl,
                    ipnUrl = notifyUrl,
                    returnUrl = notifyUrl,
                    notifyUrl = notifyUrl,
                    requestType = "captureWallet",
                    extraData = "",
                    lang = "vi",
                    signature = signature,
                };
                var jsonContent = JsonSerializer.Serialize(requestBody);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage responseMessage = await _httpClient.PostAsync(url, httpContent);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string responseBody = await responseMessage.Content.ReadAsStringAsync();
                        response.Status = StatusResponseEnum.Success; // Update status to Success
                        response.Body = responseBody;
                    }
                    else
                    {
                        string errorResponse = await responseMessage.Content.ReadAsStringAsync();
                        response.Status = StatusResponseEnum.BadRequest; // Update status to Error
                        response.Error = errorResponse;
                    }
                }
                catch (Exception ex)
                {
                    response.Status = StatusResponseEnum.BadRequest; // Update status to Error
                    response.Error = ex.Message;
                }
                return response;
            }
        }

        public async Task<ApiResponse<string, string>> ReturnPayment(string orderId, string requestId, int userId, int campaignId, int amount)
        {
            using (var _httpClient = new HttpClient())
            {
                var response = new ApiResponse<string, string>();
                var url = "https://test-payment.momo.vn/v2/gateway/api/query";

                var rawSignature = $"accessKey={accessKey}" +
                    $"&orderId={orderId}" +
                    $"&partnerCode={partnerCode}" +
                    $"&requestId={requestId}";
                var signature = HmacSha256Encrypt(secrectKey, rawSignature);

                var requestBody = new
                {
                    partnerCode = partnerCode,
                    requestId = requestId,
                    orderId = orderId,
                    lang = "vi",
                    signature = signature,
                };
                var jsonContent = JsonSerializer.Serialize(requestBody);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage responseMessage = await _httpClient.PostAsync(url, httpContent);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string responseBody = await responseMessage.Content.ReadAsStringAsync();
                        response.Status = StatusResponseEnum.Success; // Update status to Success
                        response.Body = responseBody;

                        using (JsonDocument doc = JsonDocument.Parse(responseBody))
                        {
                            JsonElement root = doc.RootElement;
                            if (root.TryGetProperty("message", out JsonElement messageElement))
                            {
                                string message = messageElement.GetString();
                                if (message == "Thành công.")
                                {
                                    var donate = new Core.Models.Donate()
                                    {
                                        UserId = userId,
                                        CampaignId = campaignId,
                                        Amount = amount,
                                        Date = DateTime.Now
                                    };
                                    _donateRepository.Add(donate);
                                    await _unitOfWork.SaveChangesAsync();
                                }
                            }
                        }
                    }
                    else
                    {
                        string errorResponse = await responseMessage.Content.ReadAsStringAsync();
                        response.Status = StatusResponseEnum.BadRequest; // Update status to Error
                        response.Error = errorResponse;
                    }
                }
                catch (Exception ex)
                {
                    response.Status = StatusResponseEnum.BadRequest; // Update status to Error
                    response.Error = ex.Message;
                }
                return response;
            }
        }

        public static string HmacSha256Encrypt(string key, string message)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = hmacsha256.ComputeHash(messageBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
