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
        private readonly string _smtpUser = "namchibi18@gmail.com"; // đây là email của server làm góc 
        private readonly string _smtpPassword = "utpc xeno eunt ztpa"; // vào kia mở lấy khóa dán vô

        public async Task SendEmailAsync(EmailFile request)
        {
            try
            {
                var message = new MimeMessage();

                // Người gửi
                message.From.Add(MailboxAddress.Parse(request.SenderEmail));

                // Người nhận (là bạn)
                message.To.Add(new  MailboxAddress("PhuongNam", "nuahavan@gmail.com"));

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
                message.ReplyTo.Add(MailboxAddress.Parse(request.SenderEmail));
                // do gmail sẽ chặn các email ko đúng chuẩn nên phải sử dụng 1 email server
                // từ đó lấy replay_to để biết email nào gửi còn email server (namchibi18)

                // Gmail không cho phép gửi email với From khác email SMTP (_smtpUser).
                // Vì request.SenderEmail là email người dùng nên không thể dùng làm From.

                // sẽ chịu trách nhiệm gửi email cho email chủ sỡ hữu (nếu face gmail thì gg sẽ cho nó vào spam )
                // Gửi email qua SMTP
                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpServer, 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_smtpUser, _smtpPassword);
                await client.SendAsync(message);

                // Phần gửi email cảm ơn người gửi
                var thankMessage = new MimeMessage();
                thankMessage.From.Add( new MailboxAddress("HaVanNua",_smtpUser));
                thankMessage.To.Add(MailboxAddress.Parse( request.SenderEmail));
                thankMessage.Subject = "Cảm Ơn Anh Chị Đã Liên Hệ Với HaVanNua!";

                var thankBuilder = new BodyBuilder
                {
                    //HtmlBody = "<p>Chào Anh Chị,</p>" +
                    //"<p>Cảm ơn Anh Chị đã liên hệ với HaVanNua. " +
                    //"Chúng tôi đã nhận được email của Anh Chị và sẽ phản hồi trong thời gian sớm nhất.</p>" +
                    //"<p>Trân trọng,<br/>HaVanNua</p>"



                    HtmlBody = $@"
    <div style='font-family: Arial, sans-serif; background-color:#f4f4f4; padding:20px;'>
        <div style='max-width:600px; margin:0 auto; background:#ffffff; border-radius:10px;
                    padding:25px; box-shadow:0 4px 12px rgba(0,0,0,0.1);'>

            <h2 style='color:#4CAF50; text-align:center; margin-bottom:20px;'>
                Cảm ơn bạn đã liên hệ ❤️!
            </h2>

            <p style='font-size:16px; color:#333; line-height:1.6;'>
                Chào Anh Chị,
            </p>

            <p style='font-size:16px; color:#333; line-height:1.6;'>
                Chúng tôi đã nhận được email của anh chị và sẽ phản hồi <b>trong thời gian sớm nhất</b>.
                Rất cảm ơn Anh Chị đã dành thời gian liên hệ với chúng tôi.
            </p>

            <p style='font-size:16px; color:#333; line-height:1.6;'>
                Nếu bạn cần có liên hệ qua zalo nút phía dưới 📞.
            </p>
            <div style='margin-top:25px; text-align:center;'>
                <a href='https://zalo.me/0349889594' style='background:#4CAF50; color:#fff; padding:12px 22px;
                                  border-radius:6px; text-decoration:none; 
                                  font-weight:bold; font-size:15px;'>
                    ZALO LIÊN HỆ
                </a>
            </div>

            <hr style='margin:30px 0; border:0; border-top:1px solid #ddd;' />

            <p style='font-size:14px; color:#777; text-align:center;'>
                Trân trọng ❤️❤️❤️,<br/>
                <b>Hà Văn Nữa</b>
            </p>
        </div>
    </div>"
                };
                thankMessage.Body = thankBuilder.ToMessageBody();

                // Gửi email cảm ơn
                await client.SendAsync(thankMessage);

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
