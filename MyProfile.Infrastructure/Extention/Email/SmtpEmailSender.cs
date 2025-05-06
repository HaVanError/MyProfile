using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MyProfile.Application.DTOs;
using MyProfile.Application.Interface;
using MyProfile.Application.Messege;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace MyProfile.Infrastructure.Extention.Email
{
    public class SmtpEmailSender :IEmailServices
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly string _smtpUser = "namchibi18@gmail.com";
        private readonly string _smtpPassword = "utpc xeno eunt ztpa"; // vào kia mở lấy khóa dán vô

        public async Task SendEmailAsync(EmailFile request)
        {
            try
            {
                var message = new MimeMessage();

                // Người gửi
                message.From.Add(MailboxAddress.Parse(request.SenderEmail));

                // Người nhận (là bạn)
                message.To.Add(new  MailboxAddress("PhuongName", "nuahavan@gmail.com"));

                // Tiêu đề
                message.Subject = request.Subject;

                // Cấu hình nội dung email
                var builder = new BodyBuilder
                {
                    HtmlBody = request.Message
                };

                // Nếu có file đính kèm
                if (request.FileContent != null)
                {
                    builder.Attachments.Add(request.FileName, request.FileContent, MimeKit.ContentType.Parse(request.ContentType));
                }

                message.Body = builder.ToMessageBody();

                // Gửi email qua SMTP
                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpServer, 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpUser, _smtpPassword);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                throw new Exception("Lỗi khi gửi email: " + ex.Message);
            }
        }



    }
}
