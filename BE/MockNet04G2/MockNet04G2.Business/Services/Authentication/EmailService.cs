using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MockNet04G2.Business.Services.Authentication
{
    public class EmailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string newPassword)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("vuduyhung.mbageas@gmail.com", "Trai tim Mo Mo"),
                Subject = subject,               
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);
            string emailBody = $@"
            <div style='font-family: Arial, sans-serif; color: #333;'>
                <h2 style='color: #4CAF50;'>Yêu cầu đặt lại mật khẩu thành công!</h2>
                <p>Xin chào,</p>
                <p>Chúng tôi đã nhận được yêu cầu đặt lại mật khẩu cho tài khoản <strong>Trai tim Mo Mo</strong> của bạn.</p>
                <p>Mật khẩu mới của bạn là: <strong>{newPassword}</strong></p>
                <p>Vui lòng sử dụng mật khẩu này để đăng nhập và đảm bảo thay đổi mật khẩu sau khi đăng nhập để bảo mật tài khoản của bạn.</p>
                <br />
                <p>Chúc bạn một ngày tốt lành,</p>
                <p><strong>Trai tim Mo Mo Team</strong></p>
            </div>";          
            mailMessage.Body = emailBody;
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.Credentials = new NetworkCredential("vuduyhung.mbageas@gmail.com", "vvmi bvlv zeyu wglp");
                client.EnableSsl = true;
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
