using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProfile.Application.DTOs;
using MyProfile.Application.Interface;

namespace MyProject.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ISendMail _emailSender;

        public ContactController(ISendMail emailSender)
        {
            _emailSender = emailSender;
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail([FromForm] ContactRequest request)
        {
            if (request.File != null && request.File.ContentType != "application/pdf")
            {
                return BadRequest("File phải là PDF.");
            }

            // Chuyển request qua Application Layer để xử lý
            await _emailSender.SendMailAsysn(request);

            return Ok("Đã gửi email thành công.");
        }
        }
}
