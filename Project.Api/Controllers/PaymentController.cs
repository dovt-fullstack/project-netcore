using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Api.DTO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using Project.Api.Models;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        public PaymentController(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();

        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentDTO model)
        {
            try
            {
                var requestData = new
                {
                    total = model.Total,
                    user = model.UserId
                };
                var apiUrl = $"http://103.166.182.195:4444/create-checkout-vnpay?user={model.UserId}&total={model.Total}";

                var response = await _httpClient.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return Ok(responseContent);
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                var error = 1;
                throw;
            }

        }
    }
}
