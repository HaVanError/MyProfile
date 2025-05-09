using MyProfile.Application.DTOs;
using MyProfile.Application.Interface;
using MyProfile.Application.Messege;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProfile.Application.Services
{
    public class SendMail : ISendMail
    {
        private readonly IEmailServices _emailSender;
        public SendMail(IEmailServices emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendMailAsysn(ContactRequest request)
        {
            byte[] fileBytes = null;
            if (request.File != null)
            {
                using (var ms = new MemoryStream())
                {
                    await request.File.CopyToAsync(ms);
                    fileBytes = ms.ToArray();
                }
            }

            // Tạo request gửi email
            var emailRequest = new EmailFile
            {
                SenderEmail = request.SenderEmail,
                Subject = request.Subject,
                Message = request.Message,
                FileContent = fileBytes,
                FileName = request.File?.FileName,
                ContentType = request.File?.ContentType
            };

            // Gọi tầng Infrastructure để gửi email
            await _emailSender.SendEmailAsync(emailRequest);
        }
    }
}
